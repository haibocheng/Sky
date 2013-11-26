using System.Collections.Generic;

namespace GreedyToolkit
{
    public sealed class Mime
    {
        private static readonly IDictionary<string, string> dic = new Dictionary<string, string>()
        {
            {"DAT","application/octet-stream"},
            {"FLV","flv-application/octet-stream"},
            {"JPG","image/jpeg"},
	        {"GIF","image/gif"},
	        {"BMP","image/bmp"},
	        {"PNG","image/png"},
            {"EXE","application/octet-stream"},
            {"DLL","application/x-msdownload"},
	        {"RAR","application/x-rar-compressed"}, 
	        {"ZIP","application/zip"}, 
	        {"XML","text/xml"},
	        {"HTML","image/bmp"},
		    {"JS","application/x-javascript"},
            {"TXT","text/plain"},
            {"PDF","application/pdf"},
            {"CHM","application/x-chm"},
            {"DOC","application/msword"},
            {"XLS","application/vnd.ms-excel"},
            {"DOCX","application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {"XLSX","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"}
        };

        public static string GetMimeType(string key)
        {
            key = key.ToUpper();
            return dic.ContainsKey(key) ? dic[key] : string.Empty;
        }
    }
}
