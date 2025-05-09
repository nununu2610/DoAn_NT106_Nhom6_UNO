using System;
using System.Net.Sockets;

namespace UNO.Client.Services
{
    public class SocketClient
    {
        private TcpClient client;

        public SocketClient()
        {
            client = new TcpClient();
        }

        public void Connect()
        {
            // Giả sử kết nối đến server
            client.Connect("localhost", 8888);
        }

        public bool CreateRoom(string playerName, string selectedMode, out string roomID)
        {
            roomID = Guid.NewGuid().ToString(); // Tạo roomID mới
            // Logic tạo phòng trên server
            return true;
        }

        public bool JoinRoom(string playerName, string roomIP)
        {
            // Logic tham gia phòng
            return true;
        }

        public void StartGame(string roomID)
        {
            // Logic bắt đầu game
        }
    }
}
