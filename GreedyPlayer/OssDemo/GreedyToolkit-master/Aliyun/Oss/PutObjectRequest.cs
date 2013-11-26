using System.IO;
using GreedyToolkit.Extension;

namespace GreedyToolkit.Aliyun.Oss
{
    public class PutObjectRequest : OssRequest
    {
        private Stream stream;
        private string name;
        private long start;
        private long length;

        public PutObjectRequest(string bucketName, string name, Stream stream)
            : base(bucketName)
        {
            init(name, stream, 0, stream.Length);
        }

        public PutObjectRequest(string bucketName, string name, Stream stream, long start, long length)
            : base(bucketName)
        {
            init(name, stream, start, length);

        }

        private void init(string name, Stream stream, long start, long length)
        {
            this.ContentLength = stream.Length.ToString();
            this.stream = stream;
            this.name = name;
            var br = new BinaryReader(stream);
            this.ContentType = Mime.GetMimeType(br.GetExtension());
            this.start = start;
            this.length = length;
        }

        internal override OssVerb Verb
        {
            get { return OssVerb.PUT; }
        }

        internal override OssBody Body
        {
            get { return new StreamBody(stream, this.start, this.length); }
        }

        protected override OssResource Resource
        {
            get { return new OssResource() { ObjectName = string.Format("/{0}/{1}", BucketName, name) }; }
        }

        internal override string Url
        {
            get
            {
                return string.Format("http://{0}.oss.aliyuncs.com/{1}", this.BucketName, this.name);
            }
        }
    }
}