using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreedyToolkit.Extension
{
    public static class StringArrayExtension
    {
        /// <summary>
        /// 将字符串数组连接
        /// </summary>
        /// <param name="array"></param>
        /// <param name="connector">分隔符</param>
        /// <param name="multiline">>是否为多行,如果是则在元素之间增加一个换行 默认为false</param>
        /// <param name="trim">是否需要去掉首尾空格 默认为false</param>
        /// <param name="removeBlankEntry">是否去掉空元素,默认为true</param>
        /// <returns></returns>
        public static string Concat(this IEnumerable<string> array, char connector, bool multiline = false, bool trim = false, bool removeBlankEntry = true)
        {
            return Concat(array, connector.ToString(), multiline, trim, removeBlankEntry);
        }

        /// <summary>
        /// 将字符串数组连接
        /// </summary>
        /// <param name="array"></param>
        /// <param name="connector">分隔符</param>
        /// <param name="multiline">>是否为多行,如果是则在元素之间增加一个换行 默认为false</param>
        /// <param name="trim">是否需要去掉首尾空格 默认为false</param>
        /// <param name="removeBlankEntry">是否去掉空元素,默认为true</param>
        /// <returns></returns>
        public static string Concat(this IEnumerable<string> array, string connector = "", bool multiline = false, bool trim = false, bool removeBlankEntry = true)
        {
            string item;
            var enumerable = array as string[] ?? array.ToArray();
            int count = enumerable.Count() - 1;
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < count; i++)
            {
                item = enumerable[i];
                if (trim)
                {
                    item = item.Trim();
                }
                if (!removeBlankEntry || !item.IsNullOrEmptyOrWhiteSpace())
                {
                    stringBuilder.Append(item);
                    if (!connector.IsNullOrEmptyOrWhiteSpace())
                    {
                        stringBuilder.Append(connector);
                    }
                    if (multiline)
                    {
                        stringBuilder.Append(Environment.NewLine);
                    }
                }
            }
            item = enumerable[count];
            if (trim)
            {
                item = item.Trim();
            }
            if (!removeBlankEntry || !item.IsNullOrEmptyOrWhiteSpace())
            {
                stringBuilder.Append(item);
            }
            return stringBuilder.ToString();
        }
    }
}
