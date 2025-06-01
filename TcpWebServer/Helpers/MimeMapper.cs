using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpWebServer.Helpers
{
    public static class MimeMapper
    {
        private static readonly Dictionary<string, string> Mappings = new()
    {
        { ".html", "text/html" },
        { ".css", "text/css" },
        { ".js", "application/javascript" }
    };

        public static bool IsSupported(string extension) => Mappings.ContainsKey(extension.ToLower());

        public static string GetMimeType(string extension)
        {
            return Mappings.TryGetValue(extension.ToLower(), out var mime) ? mime : "application/octet-stream";
        }
    }

}
