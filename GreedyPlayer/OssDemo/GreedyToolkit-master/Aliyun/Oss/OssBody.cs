using System.IO;

namespace GreedyToolkit.Aliyun.Oss
{
    public abstract class OssBody
    {
        public abstract void Send(Stream stream);
    }
}