using System;
using System.Net;

namespace GreedyToolkit.Aliyun.Oss
{
    public class OssClient
    {
        private string id;
        private string key;

        public OssClient(string appId, string secretKey)
        {
            this.id = appId;
            this.key = secretKey;
        }

        private void Load(HttpWebRequest req, WebHeaderCollection headers)
        {
            //req.Headers = headers;
            //req.ContentLength = long.Parse(headers[HttpRequestHeader.ContentLength]);
            req.ContentType = headers[HttpRequestHeader.ContentType];
            req.Date = DateTime.Parse(headers[HttpRequestHeader.Date]);
            req.Headers.Set("Authorization", headers["Authorization"]);

            //req.Proxy = new WebProxy("127.0.0.1:8888");

        }

        private HttpWebResponse Requst(OssRequest oRequest)
        {
            var req = WebRequest.Create(oRequest.Url) as HttpWebRequest;
            this.Load(req, oRequest.GetHeaders(id, key));
            req.Method = oRequest.Verb.ToString();
            //req.AllowAutoRedirect = true;
            using (var stream = req.GetRequestStream())
            {
                oRequest.Body.Send(stream);
            }
            return req.GetResponse() as HttpWebResponse;
        }

        public PutObjectResponse PutObject(PutObjectRequest request)
        {
            return new PutObjectResponse(Requst(request));
        }
    }
}
