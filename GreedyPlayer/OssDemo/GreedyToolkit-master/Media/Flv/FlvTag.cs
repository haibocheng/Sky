using System.Text;
using GreedyToolkit.Extension;

namespace GreedyToolkit.Media.Flv
{
    public abstract class FlvTag : FlvBlock
    {
        public abstract TagType TagType { get; }
        public TagHeader Header { get; protected set; }
        public abstract TagData Data { get; }

        public override void ReadBlock(FlvContext context)
        {
            this.Start = context.CurrentPostion;
            var br = context.Reader;
            var byts = br.ReadBytes(10);
            this.Header = new TagHeader(byts);
            this.Size += 11;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Start:{0}", Start);
            sb.AppendLine();
            sb.AppendFormat("Size:{0}", Size);

            return sb.ToString();
        }
    }

    public class TagSizeBlock : FlvBlock
    {
        public uint PreTagSize { get; private set; }

        public override void ReadBlock(FlvContext context)
        {
            var br = context.Reader;
            this.Start = context.CurrentPostion;
            this.Size = 4;
            this.PreTagSize = br.ReadBytes(4).ToUInt32(false);
            context.CurrentPostion += 4;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Start:{0}", Start);
            sb.AppendLine();
            sb.AppendFormat("PreTagSize:{0}", PreTagSize);
            sb.AppendLine();
            sb.AppendFormat("Size:{0}", Size);

            return sb.ToString();
        }
    }

    public enum TagType
    {
        None = 0x00,
        Audio = 0x08,
        Video = 0x09,
        Meta = 0x12
    }
}