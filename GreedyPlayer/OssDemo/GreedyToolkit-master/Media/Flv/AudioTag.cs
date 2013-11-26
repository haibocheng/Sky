
namespace GreedyToolkit.Media.Flv
{
    public class AudioTag : FlvTag
    {
        private AudioData data;

        public override TagType TagType
        {
            get
            {
                return TagType.Audio;
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
            data = new AudioData(dat);
            this.Size += this.Header.DataLength;
            context.CurrentPostion += this.Size;
        }
    }
}
