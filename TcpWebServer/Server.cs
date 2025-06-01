using System.Net.Sockets;
using System.Net;


namespace TcpWebServer
{
    public class Server
    {
        private readonly int _port;

        public Server(int port)
        {
            _port = port;
        }

        public void Start()
        {
            var listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();
            Console.WriteLine($"Server started on port {_port}");

            while (true)
            {
                var client = listener.AcceptTcpClient();
                var handler = new ClientHandler(client);
                var thread = new Thread(new ThreadStart(handler.Process));
                thread.Start();
            }
        }
    }
}
