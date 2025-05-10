using System;
using System.Net.Sockets;
using System.Text;

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
            roomID = "";

            try
            {
                NetworkStream stream = client.GetStream();
                string message = $"CREATE|{playerName}|{selectedMode}";
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // Nhận phản hồi từ server
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                if (response.StartsWith("ROOM_CREATED|"))
                {
                    roomID = response.Split('|')[1];
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool JoinRoom(string playerName, string roomID)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                string message = $"JOIN|{playerName}|{roomID}";
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // Nhận phản hồi từ server
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                return response.StartsWith("JOIN_SUCCESS");
            }
            catch
            {
                return false;
            }
        }

        public void StartGame(string roomID)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                string message = $"START_GAME|{roomID}";
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                // Ghi log hoặc xử lý lỗi nếu cần
                Console.WriteLine("StartGame Error: " + ex.Message);
            }
        }


    }
}
