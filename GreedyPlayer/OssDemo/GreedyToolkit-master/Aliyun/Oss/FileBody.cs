using System.IO;

namespace GreedyToolkit.Aliyun.Oss
{
    public class FileBody : OssBody
    {
        private byte[] bytes;

        public FileBody(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public override void Send(Stream stream)
        {
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}
