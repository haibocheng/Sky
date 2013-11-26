using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace GreedyToolkit.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// 二元分割字符串
        /// </summary>
        /// <param name="source">原字符串</param>
        /// <param name="separator">分割符号，按从左往右的顺序以第一次出现的位置为基准</param>
        /// <returns>分割后的对象</returns>
        public static Pair<string> BiSplit(this string source, char separator)
        {
            int num = source.IndexOf(separator);
            string str = source.Substring(0, num);
            string str1 = source.Substring(num + 1);
            return new Pair<string>(str, str1);
        }

        /// <summary>
        /// 判断是否为空或不存在
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        /// <summary>
        /// 判断是否仅有空白或不存在
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// 判断是否属于不存在或空或有空白字符的情况
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrWhiteSpace(this string source)
        {
            if (source.IsNullOrEmpty())
            {
                return true;
            }
            return source.IsNullOrWhiteSpace();
        }

        /// <summary>
        /// 根据指定的格式填充
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Fill(this string source, params object[] args)
        {
            return string.Format(source, args);
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="strConcat"></param>
        /// <returns></returns>
        public static string ConcatEx(this string source, string strConcat)
        {
            return string.Concat(source, strConcat);
        }

        /// <summary>
        /// 截字,并且按指定后缀连接
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length">截字长度,包含后缀的长度</param>
        /// <param name="tail">后缀符号</param>
        /// <returns></returns>
        public static string Shorten(this string source, int length, string tail = "...")
        {
            if (length > tail.Length)
            {
                if (source.Length <= length)
                {
                    return source;
                }
                else
                {
                    return source.Substring(0, length - tail.Length).ConcatEx(tail);
                }
            }
            else
            {
                return tail;
            }
        }

        /// <summary> 
        /// MD5 16位加密 
        /// </summary> 
        /// <param name="BitMode">加密位，默认16bit:false 32bit:true</param>
        /// <returns></returns> 
        public static string ToMd5(this string source, bool BitMode = false)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] byt = md5.ComputeHash(Encoding.UTF8.GetBytes(source));
            string md5String = string.Empty;
            if (BitMode)
            {
                md5String = BitConverter.ToString(byt);
            }
            else
            {
                md5String = BitConverter.ToString(byt, 4, 8);
            }
            md5String = md5String.Replace("-", "");
            return md5String;
        }

        #region Contains

        /// <summary>
        /// 从指定位置开始检查是否包含指定字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value">待查找的字符串</param>
        /// <param name="startIndex">起始位置</param>
        /// <returns></returns>
        public static bool Contains(this string source, string value, int startIndex)
        {
            return source.IndexOf(value, startIndex) != -1;
        }

        /// <summary>
        /// 从指定的位置判断是否包含指定字符串数组的全部内容,其内容可重复检索
        /// </summary>
        /// <param name="source"></param>
        /// <param name="values">待检查的字符串数组</param>
        /// <param name="startIndex">起始位置</param>
        /// <returns></returns>
        public static bool ContainsAll(this string source, IEnumerator<string> values, int startIndex = 0)
        {
            bool flag = true;
            using (values)
            {
                while (values.MoveNext())
                {
                    string current = values.Current;
                    flag = source.Contains(current, startIndex);
                    if (flag)
                    {
                        continue;
                    }
                    return flag;
                }
            }
            return flag;
        }

        /// <summary>
        /// 从指定的位置判断是否包含指定字符串数组的全部内容,其内容按数组顺序依次检索
        /// </summary>
        /// <param name="source"></param>
        /// <param name="values">待检查的字符串数组</param>
        /// <param name="startIndex">起始位置</param>
        /// <returns></returns>
        public static bool ContainsAllSequentially(this string source, IEnumerator<string> values, int startIndex = 0)
        {
            bool flag = true;
            using (values)
            {
                while (values.MoveNext())
                {
                    string current = values.Current;
                    if (source.Contains(current, startIndex))
                    {
                        startIndex = startIndex + current.Length;
                    }
                    else
                    {
                        flag = false;
                        return flag;
                    }
                }
            }
            return flag;
        }

        /// <summary>
        /// 从指定的位置判断是否包含指定字符串数组的任何一项内容
        /// </summary>
        /// <param name="source"></param>
        /// <param name="values">待检查的字符串数组</param>
        /// <param name="startIndex">起始位置</param>
        /// <returns></returns>
        public static bool ContainsAny(this string source, IEnumerator<string> values, int startIndex = 0)
        {
            bool flag = false;
            using (values)
            {
                while (values.MoveNext())
                {
                    string current = values.Current;
                    if (!source.Contains(current, startIndex))
                    {
                        continue;
                    }
                    flag = true;
                    return flag;
                }
            }
            return flag;
        }

        #endregion
    }
}
