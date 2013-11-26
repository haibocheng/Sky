using System;
using System.Globalization;
using System.IO;

namespace GreedyToolkit.Extension
{
    public static class FileExtension
    {
        public static string GetExtension(this FileStream fs)
        {
            return new BinaryReader(fs).GetExtension();
        }

        public static string GetExtension(this BinaryReader br)
        {
            var bx = string.Empty;
            //using (br)
            //{
            byte buffer;
            buffer = br.ReadByte();
            bx = buffer.ToString(CultureInfo.InvariantCulture);
            buffer = br.ReadByte();
            bx += buffer.ToString(CultureInfo.InvariantCulture);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            //}
            var ext = Enum.GetName(typeof(FileExtensionName), int.Parse(bx));
            if (ext.IsNullOrEmpty())
            {
                ext = "DAT";
            }
            return ext;
        }
    }
}