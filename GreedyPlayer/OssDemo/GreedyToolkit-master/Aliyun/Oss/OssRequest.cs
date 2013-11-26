using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace GreedyToolkit.Aliyun.Oss
{
    public abstract class OssRequest
    {
        internal string ContentLength
        {
            get
            {
                return this.Headers[HttpRequestHeader.ContentLength];
            }
            set
            {
                this.Headers.Set(HttpRequestHeader.ContentLength, value);
            }
        }

        internal string ContentType
        {
            get
            {
                return this.Headers[HttpRequestHeader.ContentType];
            }
            set
            {
                this.Headers.Set(HttpRequestHeader.ContentType, value);
            }
        }
        internal virtual string Url
        {
            get
            {
                return string.Format("http://{0}.oss.aliyuncs.com", this.BucketName);
            }
        }

        internal abstract OssVerb Verb { get; }

        internal abstract OssBody Body { get; }

        protected abstract OssResource Resource { get; }

        protected WebHeaderCollection Headers { get; set; }

        internal string BucketName { get; private set; }



        public OssRequest(string bucketName)
        {
            this.Headers = new WebHeaderCollection();
            this.BucketName = bucketName;
        }


        public virtual WebHeaderCollection GetHeaders(string appId, string secretKey)
        {
            //var date = DateTime.Now.ToUniversalTime().ToString();
            this.Headers.Add(HttpRequestHeader.Date, DateTime.Now.ToUniversalTime().ToString("r"));
            this.Headers.Add(HttpRequestHeader.Authorization, string.Format("OSS {0}:{1}", appId, Sign(secretKey)));
            return this.Headers;
        }

        private string Sign(string secretKey)
        {
            var source = string.Format("{0}\n{1}\n{2}\n{3}\n{4}{5}", this.Verb.ToString(),
                this.Headers[HttpRequestHeader.ContentMd5],
                this.ContentType,
                this.Headers[HttpRequestHeader.Date], "", this.Resource);
            var hmacSha1 = new HMACSHA1(Encoding.UTF8.GetBytes(secretKey));
            return Convert.ToBase64String(hmacSha1.ComputeHash(Encoding.UTF8.GetBytes(source)));
        }
    }
}