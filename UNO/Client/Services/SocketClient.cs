using System;
using System.Net.Sockets;
using System.Text;

namespace UNO.Client.Services
{
    public class SocketClient
    {
        private TcpClient client;
        private NetworkStream stream;

        // Kết nối tới server
        public void Connect(string ip, int port)
        {
            try
            {
                client = new TcpClient();
                client.Connect(ip, port);
                stream = client.GetStream();
                Console.WriteLine("Connected to server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to server: " + ex.Message);
            }
        }

        // Tham gia phòng game
        public bool JoinRoom(string playerName, string roomIP)
        {
            try
            {
                if (client == null || !client.Connected)
                {
                    Console.WriteLine("Not connected to the server.");
                    return false;
                }

                string joinMessage = $"JOIN|{playerName}|{roomIP}";
                byte[] messageBytes = Encoding.UTF8.GetBytes(joinMessage);
                stream.Write(messageBytes, 0, messageBytes.Length);

                // Nhận phản hồi từ server
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine("Server response: " + response);
                return response.Contains("OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine("JoinRoom error: " + ex.Message);
                return false;
            }
        }

        // Gửi thông điệp tới server
        public void SendMessage(string message)
        {
            try
            {
                if (client != null && client.Connected)
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                }
                else
                {
                    Console.WriteLine("Not connected to server.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending message: " + ex.Message);
            }
        }

        // Ngắt kết nối khỏi server
        public void Disconnect()
        {
            try
            {
                stream?.Close();
                client?.Close();
                Console.WriteLine("Disconnected from server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error disconnecting: " + ex.Message);
            }
        }
    }
}
