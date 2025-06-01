using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpWebServer
{
    public class HttpRequest
    {
        public string Method { get; set; }
        public string Path { get; set; }
    }

    public static class HttpRequestParser
    {
        public static HttpRequest Parse(string requestLine)
        {
            var tokens = requestLine.Split(' ');
            return new HttpRequest
            {
                Method = tokens[0],
                Path = Uri.UnescapeDataString(tokens[1])
            };
        }
    }
}
