using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpWebServer
{
    public class ClientHandler
    {
        private readonly TcpClient _client;

        public ClientHandler(TcpClient client)
        {
            _client = client;
        }

        public void Process()
        {
            using var stream = _client.GetStream();
            using var reader = new StreamReader(stream);
            using var writer = new StreamWriter(stream) { AutoFlush = true };

            var requestLine = reader.ReadLine();
            if (string.IsNullOrEmpty(requestLine))
                return;

            var request = HttpRequestParser.Parse(requestLine);
            var response = HttpResponseBuilder.BuildResponse(request);

            writer.Write(response);
            _client.Close();
        }
    }

}
