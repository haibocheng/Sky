using System.Collections.Generic;
using System.IO;
using System.Text;
using GreedyToolkit.Extension;

namespace GreedyToolkit.Media.Flv
{
    public class MetaData : TagData
    {
        public List<KeyValuePair<string, object>> Values { get; private set; }
        private uint size;
        private uint offset;

        public MetaData(FlvContext context, uint size)
        {
            this.size = size;
            this.offset = 0;
            this.Values = new List<KeyValuePair<string, object>>();

            var br = context.Reader;
            this.AddElement(ReadElement(br).ToString(), ReadElement(br));//读完onMetaData
            //while (offset < size)
            //{
            //    var byts = br.ReadBytes(3);
            //    if (byts[0] == 0 && byts[1] == 0 && byts[2] == 9)
            //    {
            //        offset += 3;
            //        break;
            //    }
            //    br.BaseStream.Seek(-3, SeekOrigin.Current);
            //    AddElement("#" + offset, ReadElement(br));
            //}
        }

        private void AddElement(string key, object o)
        {
            Values.Add(new KeyValuePair<string, object>(key, o));
        }

        private double ReadDouble(BinaryReader br)
        {
            var number = br.ReadBytes(8).ToDouble(false);
            offset += 8;
            return number;
        }

        private byte ReadByte(BinaryReader br)
        {
            offset++;
            return br.ReadByte();
        }

        private string ReadString(BinaryReader br)
        {
            var len = br.ReadBytes(2).ToUInt16(false);
            offset += 2;
            var str = Encoding.Default.GetString(br.ReadBytes(len));
            offset += len;
            return str;

        }

        private object ReadObject(BinaryReader br)
        {
            var obj = new MetaObject();
            while (offset < size)
            {
                var byts = br.ReadBytes(3);
                if (byts[0] == 0 && byts[1] == 0 && byts[2] == 9)
                {
                    offset += 3;
                    break;
                }
                br.BaseStream.Seek(-3, SeekOrigin.Current);
                string key = ReadString(br);
                if (key[0] == 0)
                    break;
                obj[key] = ReadElement(br);
            }
            return obj;
        }

        private ushort ReadUShort(BinaryReader br)
        {
            var number = br.ReadBytes(2).ToUInt16(false);
            offset += 2;
            return number;
        }

        private MetaObject ReadArray(BinaryReader br)
        {
            var array = new MetaObject();
            br.ReadBytes(4);
            offset += 4;
            while (offset < size)
            {
                var byts = br.ReadBytes(3);
                if (byts[0] == 0 && byts[1] == 0 && byts[2] == 9)
                {
                    offset += 3;
                    break;
                }
                br.BaseStream.Seek(-3, SeekOrigin.Current);
                string key = ReadString(br);
                //if (key[0] == 0)
                //    break;
                array[key] = ReadElement(br);
            }
            return array;
            //var len = br.ReadBytes(4).ToUInt32(false);
            //offset += 4;
            //var array = new MetaObject();
            //for (uint i = 0; i < len; i++)
            //{
            //    string key = ReadString(br);
            //    array[key] = ReadElement(br);
            //}
            //br.ReadBytes(3); // 00 00 09表示object的结束
            //offset += 3;
            //return array;
        }

        private MetaArray ReadStrictArray(BinaryReader br)
        {
            var len = br.ReadBytes(4).ToUInt32(false);
            offset += 4;
            var array = new MetaArray();
            for (uint i = 0; i < len; i++)
            {
                array.Add(ReadElement(br));
            }
            return array;
        }

        private double ReadDate(BinaryReader br)
        {
            var d = ReadDouble(br);
            br.ReadBytes(2);
            offset += 2;
            return d;
        }

        private string ReadLongString(BinaryReader br)
        {
            var len = br.ReadBytes(4).ToUInt32(false);
            offset += 4;
            var byts = br.ReadBytes((int)len);
            offset += len;
            return Encoding.Default.GetString(byts);
        }

        private object ReadElement(BinaryReader br)
        {
            var type = (MessageType)br.ReadByte();
            offset++;
            switch (type)
            {
                case MessageType.Number: // Number - 8
                    return ReadDouble(br);
                case MessageType.Boolean: // Boolean - 1
                    return ReadByte(br);
                case MessageType.String: // String - 2+n
                    return ReadString(br);
                case MessageType.Object: // Object
                    return ReadObject(br);
                case MessageType.MovieClip: // MovieClip
                    return ReadString(br);
                case MessageType.Null: // Null
                    break;
                case MessageType.Undefined: // Undefined
                    break;
                case MessageType.Reference: // Reference - 2
                    return ReadUShort(br);
                case MessageType.ECMAArray: // ECMA array
                    return ReadArray(br);
                case MessageType.StrictArray: // Strict array
                    return ReadStrictArray(br);
                case MessageType.Date: // Date - 8+2
                    return ReadDate(br);
                case MessageType.LongString: // Long string - 4+n
                    return ReadLongString(br);
            }
            return null;
        }

        public override string ToString()
        {
            var ie = Values.GetEnumerator();
            var sb = new StringBuilder();
            while (ie.MoveNext())
            {
                sb.AppendFormat("{0}: {1}", ie.Current.Key, ie.Current.Value);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }

    public class MetaObject
    {
        public static int Indent = 0;
        private Dictionary<string, object> values = new Dictionary<string, object>();
        public object this[string key]
        {
            get
            {
                object o;
                values.TryGetValue(key, out o);
                return o;
            }
            set
            {
                if (!values.ContainsKey(key))
                {
                    values.Add(key, value);
                }
            }
        }

        public bool Contains(string key)
        {
            return values.ContainsKey(key);
        }

        public Dictionary<string, object>.KeyCollection Keys
        {
            get
            {
                return values.Keys;
            }
        }
        
        public override string ToString()
        {
            string str = "{\r\n";
            MetaObject.Indent += 2;
            foreach (KeyValuePair<string, object> kv in values)
            {
                str += new string(' ', MetaObject.Indent) + kv.Key + ": " + kv.Value + "\r\n";
            }
            MetaObject.Indent -= 2;
            //if (str.Length > 1)
            //    str = str.Substring(0, str.Length - 1);
            str += "}";
            return str;
        }
    }

    public class MetaArray
    {
        private List<object> values = new List<object>();
        public object this[int index]
        {
            get
            {
                if (index >= 0 && index < values.Count)
                    return values[index];
                return null;
            }
        }

        public int Length
        {
            get
            {
                return values.Count;
            }
        }



        public void Add(object o)
        {
            values.Add(o);
        }


        public override string ToString()
        {
            string str = "[";
            int n = 0;
            foreach (object o in values)
            {
                //if (n % 10 == 0)
                //    str += "\r\n";
                n++;
                str += o + ",";
            }
            if (str.Length > 1)
                str = str.Substring(0, str.Length - 1);
            str += "]";
            return str;
        }
    }

    public enum MessageType
    {
        Number = 0,//double
        Boolean = 1,//UI8
        String = 2,//SCRIPT DATA STRING 16位
        Object = 3,//SCRIPT DATA OBJECT
        MovieClip = 4,// (reserved, not supported)
        Null = 5,
        Undefined = 6,
        Reference = 7,//UI16
        ECMAArray = 8,//SCRIPT DATA ECMA ARRAY
        ObjectEndMarker = 9,
        StrictArray = 10,//SCRIPT DATA STRICT ARRAY
        Date = 11,//SCRIPT DATA DATE
        LongString = 12//SCRIPT DATA LONG STRING 32位
    }
}
