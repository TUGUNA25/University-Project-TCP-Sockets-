using TcpWebServer;

class Program
{
    static void Main(string[] args)
    {
        int port = 8080;
        var server = new Server(port);
        server.Start();
    }
}
