using System;
using System.Linq;

namespace GreedyToolkit.Extension
{
    public static class ByteArrayExtension
    {
        public static byte[] PadLeft(this byte[] arr, int totalLength)
        {
            var len = arr.Length;
            var leak = totalLength - len;
            var list = arr.ToList();
            while (leak-- > 0)
            {
                list.Insert(0, 0x0);
            }
            return list.ToArray();
        }

        //public static byte[] Reverse(this IEnumerable<byte> arr)
        //{

        //    Array.Reverse(arr);
        //    return arr;
        //}

        public static uint ToUInt32(this byte[] arr, int start)
        {
            return BitConverter.ToUInt32(arr, 0);
        }


        public static uint ToUInt32(this byte[] arr, bool littleEndian = true)
        {
            if (arr.Length < 4)
            {
                arr = arr.PadLeft(4);
            }
            if (!littleEndian)
            {
                arr = arr.Reverse().ToArray();
            }
            return BitConverter.ToUInt32(arr, 0);
        }

        public static ushort ToUInt16(this byte[] arr, bool littleEndian = true)
        {
            if (arr.Length < 2)
            {
                arr = arr.PadLeft(2);
            }
            if (!littleEndian)
            {
                arr = arr.Reverse().ToArray();
            }
            return BitConverter.ToUInt16(arr, 0);
        }

        public static double ToDouble(this byte[] arr, bool littleEndian = true)
        {
            if (arr.Length < 8)
            {
                arr = arr.PadLeft(8);
            }
            if (!littleEndian)
            {
                arr = arr.Reverse().ToArray();
            }
            return BitConverter.ToDouble(arr, 0);
        }
    }
}
