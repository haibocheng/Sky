using System;
using System.IO;
using GreedyToolkit.Extension;

namespace GreedyToolkit.Media.Flv
{
    public class FlvContext : IDisposable
    {
        public BinaryReader Reader { get; private set; }
        public uint CurrentPostion { get; set; }

        public FlvContext(string path)
        {
            Verify(path);
            this.CurrentPostion = 0;
        }

        public FlvContext(Stream stream)
        {
            Verify(stream);
            this.CurrentPostion = 0;
        }

        /// <summary>
        /// 验证flv文件有效性
        /// </summary>
        /// <param name="path"></param>
        private void Verify(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("the flv file is not exist");
            }
            this.Reader = new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read));
            if (this.Reader.GetExtension().IsNullOrEmpty())
            {
                throw new InvalidDataException("the file is not a flv");
            }
        }

        private void Verify(Stream stream)
        {
            this.Reader = new BinaryReader(stream);
            this.Reader.BaseStream.Seek(0, SeekOrigin.Begin);
            if (this.Reader.GetExtension().IsNullOrEmpty())
            {
                throw new InvalidDataException("the file is not a flv");
            }
        }

        public void Dispose()
        {
            if (Reader != null)
            {
                Reader.Close();
            }
        }
    }
}