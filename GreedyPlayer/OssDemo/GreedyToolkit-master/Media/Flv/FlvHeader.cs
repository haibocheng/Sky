using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace GreedyToolkit.Media.Flv
{
    public class FlvHeader : FlvBlock
    {
        public string Signature { get; protected set; }
        public string Version { get; protected set; }
        public int Reserve5 { get; protected set; }
        public bool HasAudio { get; protected set; }
        public int Reserve7 { get; protected set; }
        public bool HasVideo { get; protected set; }


        public FlvHeader()
        {
            this.Start = 0;
            this.Size = 9;
        }

        public override void ReadBlock(FlvContext context)
        {
            var br = context.Reader;
            this.Start = context.CurrentPostion;
            this.Signature = new string(br.ReadChars(3));
            this.Version = Convert.ToInt32(br.ReadByte()).ToString(CultureInfo.InvariantCulture);
            var byt = br.ReadByte();

            this.Reserve5 = byt & 0xF8;
            this.HasAudio = (byt & 0x04) > 0;
            this.Reserve7 = byt & 0x02;
            this.HasVideo = (byt & 0x01) > 0;
            this.Size = BitConverter.ToUInt32(br.ReadBytes(4).Reverse().ToArray(), 0);
            context.CurrentPostion += this.Size;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Start:{0}", Start);
            sb.AppendLine();
            sb.AppendFormat("Signature:{0}", Signature);
            sb.AppendLine();
            sb.AppendFormat("Version:{0}", Version);
            sb.AppendLine();
            sb.AppendFormat("Reserve5:{0}", Reserve5);
            sb.AppendLine();
            sb.AppendFormat("HasAudio:{0}", HasAudio);
            sb.AppendLine();
            sb.AppendFormat("Reserve7:{0}", Reserve7);
            sb.AppendLine();
            sb.AppendFormat("HasVideo:{0}", HasVideo);
            sb.AppendLine();
            sb.AppendFormat("Size:{0}", Size);

            return sb.ToString();
        }
    }
}
