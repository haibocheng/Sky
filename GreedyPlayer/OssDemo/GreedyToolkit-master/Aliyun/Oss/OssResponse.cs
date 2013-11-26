using System.Net;

namespace GreedyToolkit.Aliyun.Oss
{
    public abstract class OssResponse
    {
        protected WebHeaderCollection Headers { get; private set; }

        public string ContentLength
        {
            get
            {
                return this.Headers[HttpResponseHeader.ContentLength];
            }
        }

        public string Connection
        {
            get
            {
                return this.Headers[HttpResponseHeader.Connection];
            }
        }

        public string Date
        {
            get
            {
                return this.Headers[HttpResponseHeader.Date];
            }
        }

        public string ETag
        {
            get
            {
                return this.Headers[HttpResponseHeader.ETag];
            }
        }

        public string Server
        {
            get
            {
                return this.Headers[HttpResponseHeader.Server];
            }
        }

        public string RequestId
        {
            get
            {
                return this.Headers["x-oss-request-id"];
            }
        }

        public OssResponse(HttpWebResponse response)
        {
            using (response)
            {
                this.Headers = response.Headers;
            }
        }
    }
}