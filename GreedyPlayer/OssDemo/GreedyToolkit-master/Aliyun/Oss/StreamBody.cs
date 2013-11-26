using System;
using System.IO;

namespace GreedyToolkit.Aliyun.Oss
{
    public class StreamBody : OssBody
    {
        private Stream outputStream;
        private long start;
        private long length;

        public StreamBody(Stream stream, long start, long length)
        {
            this.outputStream = stream;
            this.start = start;
            this.length = length;
        }

        public override void Send(Stream stream)
        {
            var bufferSize = 2048;
            var buffer = new byte[bufferSize];
            long rest;
            var accumulate = 0L;
            var nextReadCount = bufferSize;
            outputStream.Seek(start, SeekOrigin.Begin);

            using (var bw = new BinaryWriter(stream))
            {
                while ((rest = length - accumulate) > 0)
                {
                    if (rest <= bufferSize)
                    {
                        nextReadCount = Convert.ToInt32(rest);
                    }

                    outputStream.Read(buffer, 0, nextReadCount);
                    bw.Write(buffer, 0, nextReadCount);
                    accumulate += nextReadCount;
                }
            }

            //using (var sw = new StreamWriter(stream))
            //{
            //    var sr = new StreamReader(this.outputStream);
            //    sw.Write(sr.ReadToEnd());
            //}
        }
    }
}
