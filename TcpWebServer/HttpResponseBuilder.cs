using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpWebServer.Helpers;

namespace TcpWebServer
{
    public static class HttpResponseBuilder
    {
        public static string BuildResponse(HttpRequest request)
        {
            if (request.Method != "GET")
                return BuildErrorResponse(405, "Method Not Allowed");

            if (request.Path.Contains(".."))
                return BuildErrorResponse(403, "Forbidden");

            string filePath = "WebRoot" + request.Path;
            if (string.IsNullOrWhiteSpace(request.Path) || request.Path == "/")
                filePath = "WebRoot/index.html";

            if (!File.Exists(filePath))
                return BuildErrorResponse(404, "Not Found");

            string extension = Path.GetExtension(filePath);
            if (!MimeMapper.IsSupported(extension))
                return BuildErrorResponse(403, "Forbidden");

            var content = File.ReadAllText(filePath);
            var mimeType = MimeMapper.GetMimeType(extension);

            return
                $"HTTP/1.1 200 OK\r\n" +
                $"Content-Type: {mimeType}\r\n" +
                $"Content-Length: {content.Length}\r\n\r\n" +
                content;
        }

        private static string BuildErrorResponse(int code, string message)
        {
            string body =
                $"<html><head><title>{code} {message}</title></head>" +
                $"<body><h1>Error {code}: {message}</h1></body></html>";

            return
                $"HTTP/1.1 {code} {message}\r\n" +
                "Content-Type: text/html\r\n" +
                $"Content-Length: {body.Length}\r\n\r\n" +
                body;
        }
    }

}
