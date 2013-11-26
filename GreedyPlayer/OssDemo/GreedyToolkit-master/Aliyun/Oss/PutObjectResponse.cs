using System.Net;

namespace GreedyToolkit.Aliyun.Oss
{
    public class PutObjectResponse : OssResponse
    {
        public PutObjectResponse(HttpWebResponse response)
            : base(response)
        {

        }
    }
}
