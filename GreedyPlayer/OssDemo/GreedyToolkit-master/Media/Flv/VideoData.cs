using System.Text;

namespace GreedyToolkit.Media.Flv
{
    public class VideoData : TagData
    {
        public FrameType Type { get; private set; }
        public Coder Coder { get; private set; }

        public VideoData(byte[] data)
        {
            var key = data[0];
            this.Type = (FrameType)((key & 0xF0) >> 4);
            this.Coder = (Coder)(key & 0x0F);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("FrameType:{0}", Type);
            sb.AppendLine();
            sb.AppendFormat("Coder:{0}", Coder);
            return sb.ToString();
        }
    }

    public enum FrameType
    {
        KeyFrame = 1,//(for AVC, a seekable frame)
        InterFrame = 2,// (for AVC, a non-seekable frame)
        DisposableInterFrame = 3,//(H.263 only)
        GeneratedKeyFrame = 4,//(reserved for server use only)
        CommandFrame = 5//VideoInfo
    }


    public enum Coder
    {
        JPEG = 1,
        SorensonH263 = 2,
        ScreenVideo = 3,
        On2VP6 = 4,
        On2VP6WithAlphaChannel = 5,
        ScreenVideoVersion2 = 6,
        AVC = 7
    }
}
