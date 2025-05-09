using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace UNO.Server.Services
{
    // Đặt lớp GameRoom trong Server
    public class GameRoom
    {
        public string RoomIP { get; set; }  // IP của phòng
        private List<TcpClient> players;    // Danh sách người chơi trong phòng
        private List<string> playerNames;   // Danh sách tên người chơi
        private int maxPlayers = 4;         // Giới hạn người chơi trong phòng

        public GameRoom(string roomIP)
        {
            RoomIP = roomIP;
            players = new List<TcpClient>();
            playerNames = new List<string>(); // Khởi tạo danh sách tên người chơi
        }

        // Kiểm tra phòng có đầy đủ người chơi hay không
        public bool IsFull => players.Count >= maxPlayers;

        // Thêm người chơi vào phòng
        public bool AddPlayer(TcpClient client, string playerName)
        {
            if (players.Count < maxPlayers)
            {
                players.Add(client);
                playerNames.Add(playerName);  // Thêm tên người chơi
                                              // Thông báo cho các người chơi khác trong phòng
                BroadcastMessage($"Player {playerName} has joined the room.");

                SendMessageToPlayer(client, "You have joined the room."); // Gửi thông điệp cho người chơi mới

                if (IsFull)
                {
                    StartGame();  // Nếu phòng đầy đủ người chơi thì bắt đầu trò chơi
                }

                return true;  // Trả về true nếu người chơi được thêm thành công
            }
            else
            {
                SendMessageToPlayer(client, "The room is full.");
                return false;  // Trả về false nếu phòng đầy
            }
        }

        // Gửi thông điệp tới tất cả người chơi trong phòng
        public void BroadcastMessage(string message)
        {
            foreach (var player in players)
            {
                SendMessageToPlayer(player, message);  // Sử dụng phương thức đã viết để gửi thông điệp cho từng người chơi
            }
        }

        // Bắt đầu trò chơi
        private void StartGame()
        {
            // Các bước để bắt đầu game, chẳng hạn như chia bài
            foreach (var player in players)
            {
                SendCardToPlayer(player);  // Gửi bài cho từng người chơi
            }
            // Gửi thông điệp cho tất cả người chơi trong phòng để thông báo bắt đầu trò chơi
            BroadcastMessage("Game is starting! Prepare your cards.");
            // Các bước để bắt đầu game, chia bài, v.v.
        }

        private void SendMessageToPlayer(TcpClient player, string message)
        {
            try
            {
                if (player.Connected)  // Kiểm tra xem client còn kết nối hay không
                {
                    NetworkStream stream = player.GetStream();
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    stream.Write(messageBytes, 0, messageBytes.Length);
                }
                else
                {
                    Console.WriteLine("Client is not connected.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message to player: {ex.Message}");
            }
        }

        // Giả lập gửi bài cho người chơi, tùy theo logic của bạn
        private void SendCardToPlayer(TcpClient player)
        {
            try
            {
                if (player.Connected)  // Kiểm tra xem client còn kết nối hay không
                {
                    NetworkStream stream = player.GetStream();
                    string cardMessage = "Card data goes here.";  // Ví dụ về dữ liệu bài
                    byte[] cardMessageBytes = Encoding.UTF8.GetBytes(cardMessage);
                    stream.Write(cardMessageBytes, 0, cardMessageBytes.Length);
                }
                else
                {
                    Console.WriteLine("Client is not connected.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending card to player: {ex.Message}");
            }
        }
    }
}
