using System;
using System.IO;

namespace GreedyToolkit.Media.Flv
{
    public class FlvInfo
    {
        public FlvHeader Header { get; private set; }
        public FlvBody Body { get; private set; }

        private readonly FlvContext context;

        public FlvInfo(string path)
        {
            context = new FlvContext(path);
            Analyze(true);
        }

        public FlvInfo(Stream stream)
        {
            context = new FlvContext(stream);
            Analyze(false);
        }

        /// <summary>
        /// 解析flv
        /// </summary>
        private void Analyze(bool needRealse)
        {
            try
            {
                //解析file head部分
                this.Header = new FlvHeader();
                this.Header.ReadBlock(context);

                //解析file body部分    
                this.Body = new FlvBody();
                this.Body.ReadBlock(context);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (needRealse)
                {
                    context.Dispose();
                }
            }
        }
    }
}
