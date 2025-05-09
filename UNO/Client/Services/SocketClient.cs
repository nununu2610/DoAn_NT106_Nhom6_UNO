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
                Console.WriteLine($"Connected to server at {ip}:{port}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to server: {ex.Message}");
            }
        }

        // Tham gia phòng game
        // Gửi thông điệp tới server và nhận phản hồi
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

                // Đảm bảo buffer được đọc đầy đủ dữ liệu
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                StringBuilder responseBuilder = new StringBuilder();

                while (true)
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    responseBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                    // Dừng lại khi nhận đầy đủ phản hồi từ server
                    if (bytesRead < buffer.Length)
                    {
                        break;
                    }
                }

                string response = responseBuilder.ToString();
                Console.WriteLine($"Server response: '{response}'");

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
