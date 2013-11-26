using System.Collections.Generic;

namespace GreedyToolkit.Media.Flv
{
    public class FlvBody : FlvBlock
    {
        public IList<FlvTag> Tags { get; set; }

        public override void ReadBlock(FlvContext context)
        {
            this.Start = context.CurrentPostion;
            var br = context.Reader;
            this.Tags = new List<FlvTag>();
            var flag = true;
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                if (flag)
                {
                    var tagSizeBlock = new TagSizeBlock();
                    tagSizeBlock.ReadBlock(context);
                    this.Size = this.Size + tagSizeBlock.Size + tagSizeBlock.PreTagSize;
                }
                else
                {
                    var key = br.ReadByte();
                    var tag = this.GetTag(key);
                    tag.ReadBlock(context);
                    this.Tags.Add(tag);
                }
                flag = !flag;
            }
        }

        private FlvTag GetTag(byte byt)
        {
            var type = (TagType)byt;
            if (type == TagType.Audio)
            {
                return new AudioTag();
            }
            if (type == TagType.Video)
            {
                return new VideoTag();
            }
            if (type == TagType.Meta)
            {
                return new MetaTag();
            }
            return null;
        }
    }
}