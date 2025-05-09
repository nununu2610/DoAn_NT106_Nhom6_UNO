using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UNO.Server.Services; // Đảm bảo namespace chứa GameRoom

namespace UNO.Server
{
    public class SocketServer
    {
        private TcpListener _listener;
        private Thread _listenThread;
        private bool _isRunning;
        private Dictionary<string, GameRoom> gameRooms; // Quản lý game rooms bằng Dictionary

        public SocketServer()
        {
            gameRooms = new Dictionary<string, GameRoom>();
        }

        // Khởi động server
        public void Start(int port)
        {
            if (_isRunning) return;

            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            _isRunning = true;

            _listenThread = new Thread(ListenForClients)
            {
                IsBackground = true
            };
            _listenThread.Start();

            Console.WriteLine($"Server started on port {port}");
        }

        // Dừng server
        public void Stop()
        {
            _isRunning = false;
            _listener.Stop();
            _listenThread?.Join();
            Console.WriteLine("Server stopped.");
        }

        // Lắng nghe các client kết nối
        private void ListenForClients()
        {
            while (_isRunning)
            {
                try
                {
                    TcpClient client = _listener.AcceptTcpClient();
                    Console.WriteLine("Client connected.");

                    // Khởi tạo một thread riêng cho mỗi client
                    Thread clientThread = new Thread(HandleClientComm)
                    {
                        IsBackground = true
                    };
                    clientThread.Start(client);
                }
                catch (SocketException ex)
                {
                    if (_isRunning)
                        Console.WriteLine("Socket exception: " + ex.Message);
                    break;
                }
            }
        }

        // Xử lý thông tin từ client
        private void HandleClientComm(object clientObj)
        {
            TcpClient client = clientObj as TcpClient;

            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];

                while (true) // <-- giữ kết nối
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; // client ngắt

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received from client: " + message);

                    ProcessClientMessage(message, client, stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client error: " + ex.Message);
            }
            finally
            {
                client.Close();
            }
        }


        // Xử lý thông điệp của client (JOIN, các lệnh khác)
        // Xử lý thông điệp của client (JOIN, các lệnh khác)
        private void ProcessClientMessage(string message, TcpClient client, NetworkStream stream)
        {
            if (message.StartsWith("JOIN"))
            {
                string[] parts = message.Split('|');
                if (parts.Length == 3)
                {
                    string playerName = parts[1];
                    string roomIP = parts[2];

                    // Tìm hoặc tạo phòng
                    GameRoom room = GetOrCreateGameRoom(roomIP);
                    if (!room.IsFull)
                    {
                        byte[] okResponse = Encoding.UTF8.GetBytes("OK");
                        stream.Write(okResponse, 0, okResponse.Length);  // Phản hồi thành công

                        // Sau đó thêm player vào phòng
                        room.AddPlayer(client, playerName);
                    }
                    else
                    {
                        byte[] failResponse = Encoding.UTF8.GetBytes("ROOM_FULL");
                        stream.Write(failResponse, 0, failResponse.Length);  // Phản hồi phòng đầy
                    }
                }
                else
                {
                    byte[] failResponse = Encoding.UTF8.GetBytes("INVALID_FORMAT");
                    stream.Write(failResponse, 0, failResponse.Length);  // Phản hồi lỗi định dạng
                }
            }
        }





        // Tìm phòng hoặc tạo mới nếu không có
        private GameRoom GetOrCreateGameRoom(string roomIP)
        {
            if (!gameRooms.ContainsKey(roomIP))
            {
                var room = new GameRoom(roomIP);
                gameRooms.Add(roomIP, room);
            }

            return gameRooms[roomIP];
        }
    }
}
