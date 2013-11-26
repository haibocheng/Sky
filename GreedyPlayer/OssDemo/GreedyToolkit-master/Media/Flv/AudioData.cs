using System.Text;

namespace GreedyToolkit.Media.Flv
{
    public class AudioData : TagData
    {
        public SoundFormat Format { get; private set; }
        public SoundRate Rate { get; private set; }
        public SoundBit Bit { get; private set; }
        public SoundType Type { get; private set; }

        public AudioData(byte[] data)
        {
            var key = data[0];
            this.Format = (SoundFormat)((key & 0xF0) >> 4);
            this.Rate = (SoundRate)((key & 0x0A) >> 2);
            this.Bit = (SoundBit)((key & 0x02) >> 1);
            this.Type = (SoundType)(key & 0x01);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("SoundFormat:{0}", Format);
            sb.AppendLine();
            sb.AppendFormat("SoundRate:{0}", Rate);
            sb.AppendLine();
            sb.AppendFormat("SoundBit:{0}", Bit);
            sb.AppendLine();
            sb.AppendFormat("SoundType:{0}", Type);

            return sb.ToString();
        }
    }

    public enum SoundFormat
    {
        LinearPCM = 0,//platform endian
        ADPCM = 1,
        MP3 = 2,
        LittleLinearPCM = 3,//little endian
        Nellymoser16 = 4,// 16-kHz mono 
        Nellymoser8 = 5,// 8-kHz mono  
        Nellymoser = 6,
        G711ALawLogarithmicPCM = 7,
        G711MuLawLogarithmicPCM = 8,
        Reserved = 9,
        AAC = 10,
        MP3_8K = 14,//8-Khz  
        DeviceSpecificSound = 15
    }

    public enum SoundRate
    {
        Snd5KHz = 0,
        Snd11KHz = 1,
        Snd22KHz = 2,
        Snd44KHz = 3
    }

    public enum SoundBit
    {
        Snd8Bit = 0,
        Snd16Bit = 1
    }

    public enum SoundType
    {
        Mono = 0,
        Stereo = 1
    }
}
