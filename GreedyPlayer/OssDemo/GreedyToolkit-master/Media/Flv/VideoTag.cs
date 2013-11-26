
namespace GreedyToolkit.Media.Flv
{
    public class VideoTag : FlvTag
    {
        private VideoData data;

        public override TagType TagType
        {
            get
            {
                return TagType.Video;
            }
        }

        public override TagData Data
        {
            get
            {
                return data;
            }
        }

        public override void ReadBlock(FlvContext context)
        {
            base.ReadBlock(context);
            var br = context.Reader;
            var dat = br.ReadBytes((int)this.Header.DataLength);
            data = new VideoData(dat);
            this.Size += this.Header.DataLength;
            context.CurrentPostion += this.Size;
        }
    }
}