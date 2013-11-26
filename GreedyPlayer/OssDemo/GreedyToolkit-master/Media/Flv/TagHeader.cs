using System.Linq;
using System.Text;
using GreedyToolkit.Extension;

namespace GreedyToolkit.Media.Flv
{
    public class TagHeader
    {
        public uint DataLength { get; private set; }
        public uint Timestamp { get; private set; }
        public uint SteamId { get; private set; }

        public TagHeader(byte[] data)
        {
            this.DataLength = data.Take(3).ToArray().ToUInt32(false);
            this.Timestamp = data.Skip(3).Take(3).ToArray().ToUInt32(false);
            var timestampEx = data.Skip(6).Take(1).ToArray().ToUInt32(false) << 24;
            this.Timestamp |= timestampEx;
            this.SteamId = data.Skip(7).Take(3).ToArray().ToUInt32(false);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Data Length:{0}", DataLength);
            sb.AppendLine();
            sb.AppendFormat("Timestamp:{0}", Timestamp);
            sb.AppendLine();
            sb.AppendFormat("SteamId:{0}", SteamId);

            return sb.ToString();
        }
    }
}
