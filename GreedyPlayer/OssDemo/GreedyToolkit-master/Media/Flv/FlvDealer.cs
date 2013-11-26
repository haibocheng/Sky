using System;
using System.IO;
using System.Linq;
using System.Text;

namespace GreedyToolkit.Media.Flv
{
    public class FlvDealer
    {
        private FlvInfo info;
        private BinaryReader reader;
        private bool hasKeyFrames;
        private uint startPosition;
        private uint endPosition;
        private const double INTERNAL = 120d;


        public uint StartPosition
        {
            get
            {
                return startPosition;
            }
        }

        public uint EndPosition
        {
            get
            {
                return endPosition;
            }
        }

        public FlvDealer(Stream inputStream)
        {
            this.info = new FlvInfo(inputStream);
            this.reader = new BinaryReader(inputStream);
        }

        public MetaObject Segments { get; private set; }

        public byte[] Deal()
        {
            var metaTag = info.Body.Tags[0] as MetaTag;
            var metaData = metaTag.Data as MetaData;
            var obj = metaData.Values.First(i => i.Key.Equals("onMetaData")).Value as MetaObject;
            var keyFrames = GetKeyFrames(obj);
            this.Segments = GetSegments(keyFrames["filepositions"] as MetaArray, keyFrames["times"] as MetaArray);

            this.reader.BaseStream.Seek(0, SeekOrigin.Begin);
            //分割头文件部分 flv header + pretag size + meta tag
            var size = (int)(info.Header.Size + metaTag.Size + 4);
            var blocks = reader.ReadBytes(size);

            var metaTagSize = metaTag.Size;
            var metaTagDataSize = metaTag.Header.DataLength;
            var metaLength = (uint)obj.Keys.Count;
            byte[] byts;
            using (var ms = new MemoryStream())
            {
                ms.Write(blocks, 0, blocks.Length);
                ms.Seek(-3, SeekOrigin.End);
                //using (var writer = new BinaryWriter(ms))
                //{
                //在尾部（0x00 0x00 0x09之前附加内容）
                //writer.Seek(-3, SeekOrigin.End);
                uint length;
                if (!hasKeyFrames)
                {
                    length = this.AppendKeyValuePair(ms, "keyframes", keyFrames);
                    metaTagSize += length;
                    metaTagDataSize += length;
                    metaLength += 1;
                }

                length = this.AppendKeyValuePair(ms, "segments", this.Segments);
                metaTagSize += length;
                metaTagDataSize += length;
                metaLength += 1;

                length = this.AppendKeyValuePair(ms, "copyright", "GreedyInt.Ltd.");
                metaTagSize += length;
                metaTagDataSize += length;
                metaLength += 1;

                ms.WriteByte(0x00);
                ms.WriteByte(0x00);
                ms.WriteByte(0x09);

                //metaTagSize += 3;
                //metaTagDataSize += 3;
                byts = BitConverter.GetBytes(metaTagSize).Reverse().ToArray();
                ms.Write(byts, 0, byts.Length);
                //改变meta tag header中的第2－4个字节，增大tag的size值
                ms.Seek(14, SeekOrigin.Begin);
                ms.Write(BitConverter.GetBytes(metaTagDataSize).Reverse().ToArray(), 1, 3);
                //改变meta tag data中第二个AMF数组中的长度
                ms.Seek(38, SeekOrigin.Begin);
                byts = BitConverter.GetBytes(metaLength).Reverse().ToArray();
                ms.Write(byts, 0, byts.Length);
                //}
                byts = ms.ToArray();
            }
            return byts;
        }

        private uint WriteString(Stream st, string content)
        {
            var len = (short)content.Length;
            var byts = BitConverter.GetBytes(len).Reverse().ToArray();
            st.Write(byts, 0, byts.Length);
            byts = Encoding.UTF8.GetBytes(content);
            st.Write(byts, 0, byts.Length);
            return (uint)(len + 2);
        }

        private uint WriteDouble(Stream st, double content)
        {
            var byts = BitConverter.GetBytes(content).Reverse().ToArray();
            st.Write(byts, 0, byts.Length);
            return 8;
        }

        private uint WriteStrictArray(Stream st, MetaArray content)
        {
            var count = (uint)content.Length;
            var byts = BitConverter.GetBytes(count).Reverse().ToArray();
            st.Write(byts, 0, byts.Length);
            var total = 4u;
            for (int i = 0; i < count; i++)
            {
                total += WriteElement(st, content[i]);
            }
            return total;
        }

        private uint WriteObjct(Stream st, MetaObject content)
        {
            var keys = content.Keys;
            var total = 0u;
            foreach (var key in keys)
            {
                total += this.WriteString(st, key);
                total += this.WriteElement(st, content[key]);
            }
            st.WriteByte(0x00);
            st.WriteByte(0x00);
            st.WriteByte(0x09);
            return total + 3;
        }

        private uint AppendKeyValuePair(Stream st, string key, dynamic value)
        {
            var total = 0u;
            total += this.WriteString(st, key);
            total += this.WriteElement(st, value);
            return total;
        }

        private uint WriteElement(Stream st, dynamic obj)
        {
            var type = obj.GetType();
            if (type == typeof(double))
            {
                st.WriteByte((byte)MessageType.Number);
                return this.WriteDouble(st, obj) + 1;
            }
            if (type == typeof(string))
            {
                st.WriteByte((byte)MessageType.String);
                return this.WriteString(st, obj) + 1;
            }
            if (type == typeof(MetaArray))
            {
                st.WriteByte((byte)MessageType.StrictArray);
                return this.WriteStrictArray(st, obj) + 1;
            }
            if (type == typeof(MetaObject))
            {
                st.WriteByte((byte)MessageType.Object);
                return this.WriteObjct(st, obj) + 1;
            }
            return 0;
        }

        private MetaObject GetKeyFrames(MetaObject obj)
        {
            var vTags = info.Body.Tags.Where(t => t.TagType == TagType.Video && ((t as VideoTag).Data as VideoData).Type == FrameType.KeyFrame);
            this.startPosition = info.Body.Tags[0].Start + info.Body.Tags[0].Size + 4;
            //this.endPosition = info.Body.Tags.Last().Start + info.Body.Tags.Last().Size + 4;
            this.endPosition = info.Body.Start + info.Body.Size;

            if (obj.Contains("keyframes"))
            {
                hasKeyFrames = true;
                return obj["keyframes"] as MetaObject;
            }

            hasKeyFrames = false;
            var filepositions = new MetaArray();
            var times = new MetaArray();
            foreach (var tag in vTags)
            {
                filepositions.Add(Convert.ToDouble(tag.Start));
                times.Add(tag.Header.Timestamp / 1000d);
            }
            var keyFrames = new MetaObject();
            keyFrames["filepositions"] = filepositions;
            keyFrames["times"] = times;
            return keyFrames;
        }

        private MetaObject GetSegments(MetaArray filepositions, MetaArray times)
        {
            var timeLine = 0d;
            var segments = new MetaObject();

            segments["extension"] = ".dat";
            var positions = new MetaArray();
            var files = new MetaArray();

            if (filepositions.Length != times.Length)
            {
                throw new InvalidDataException("keyframes data is invalid");
            }


            for (int i = 0, len = times.Length; i < len; i++)
            {
                var time = Convert.ToDouble(times[i]);
                if (time - timeLine >= INTERNAL)
                {
                    positions.Add(Convert.ToDouble(filepositions[i]));
                    files.Add(positions.Length.ToString());
                    timeLine = time;
                }
            }

            positions.Add(Convert.ToDouble(this.endPosition));
            files.Add(positions.Length.ToString());

            segments["positions"] = positions;
            segments["files"] = files;
            return segments;
        }
    }
}
