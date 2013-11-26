
namespace GreedyToolkit.Media.Flv
{
    class MetaTag : FlvTag
    {
        private MetaData data;

        public override TagType TagType
        {
            get
            {
                return TagType.Meta;
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
            //var br = context.Reader;
            //var dat = br.ReadBytes((int)this.Header.DataLength);
            //data = new MetaData(dat);
            data = new MetaData(context, this.Header.DataLength);
            this.Size += this.Header.DataLength;
            context.CurrentPostion += this.Size;
        }
    }
}
