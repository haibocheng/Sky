
namespace GreedyToolkit.Media.Flv
{
    public abstract class FlvBlock
    {
        public uint Start { get; protected set; }
        public uint Size { get; protected set; }

        public abstract void ReadBlock(FlvContext context);
    }
}
