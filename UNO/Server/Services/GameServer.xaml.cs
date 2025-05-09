// Trong GameServer.cs
namespace UNO.Server.Services
{
    // Thêm từ khóa partial nếu bạn đã sử dụng ở phần khai báo UI
    public partial class GameServer
    {
        private SocketServer _server;

        public GameServer()
        {
            _server = new SocketServer();
        }

        public void Start()
        {
            int port = 12345; // Port mặc định
            _server.Start(port);
        }

        public void Stop()
        {
            _server.Stop();
        }
    }
}
