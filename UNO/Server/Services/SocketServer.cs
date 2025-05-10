using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UNO.Server
{
    public class SocketServer
    {
        private TcpListener _listener;
        private Thread _listenThread;
        private bool _isRunning;
        private Dictionary<string, GameRoom> gameRooms;

        public SocketServer()
        {
            gameRooms = new Dictionary<string, GameRoom>();
        }

        public void Start(int port = 8888)
        {
            if (_isRunning) return;

            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            _listener.Start();
            _isRunning = true;

            _listenThread = new Thread(ListenForClients)
            {
                IsBackground = true
            };
            _listenThread.Start();

            Console.WriteLine($"Server started on port {port}");
        }

        public void Stop()
        {
            _isRunning = false;
            _listener.Stop();
            _listenThread?.Join();
            Console.WriteLine("Server stopped.");
        }

        private void ListenForClients()
        {
            while (_isRunning)
            {
                try
                {
                    TcpClient client = _listener.AcceptTcpClient();
                    Console.WriteLine("Client connected.");

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

        private void HandleClientComm(object clientObj)
        {
            TcpClient client = clientObj as TcpClient;

            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];

                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

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

        private void ProcessClientMessage(string message, TcpClient client, NetworkStream stream)
        {
            if (string.IsNullOrEmpty(message)) return;

            string[] parts = message.Split('|');
            string command = parts[0];

            switch (command)
            {
                case "CREATE":
                    if (parts.Length >= 3)
                    {
                        string playerName = parts[1];
                        string mode = parts[2];
                        string roomID = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();

                        GameRoom room = new GameRoom(roomID);
                        room.AddPlayer(new Player { Name = playerName, Stream = stream });

                        gameRooms[roomID] = room;
                        SendToClient(stream, $"ROOM_CREATED|{roomID}");
                    }
                    else
                    {
                        SendToClient(stream, "INVALID_CREATE_FORMAT");
                    }
                    break;

                case "JOIN":
                    if (parts.Length >= 3)
                    {
                        string playerName = parts[1];
                        string roomID = parts[2];

                        if (!gameRooms.ContainsKey(roomID))
                        {
                            SendToClient(stream, "ROOM_NOT_FOUND");
                            return;
                        }

                        GameRoom room = gameRooms[roomID];
                        bool joined = room.AddPlayer(new Player { Name = playerName, Stream = stream });

                        string response = joined ? "JOINED" : "ROOM_FULL";
                        SendToClient(stream, response);
                    }
                    else
                    {
                        SendToClient(stream, "INVALID_JOIN_FORMAT");
                    }
                    break;

                case "GET_PLAYERS":
                    if (parts.Length >= 2)
                    {
                        string roomID = parts[1];
                        if (gameRooms.TryGetValue(roomID, out GameRoom room))
                        {
                            var playerNames = room.GetPlayerNames();
                            string playerList = "PLAYER_LIST|" + string.Join(",", playerNames);
                            SendToClient(stream, playerList);
                        }
                        else
                        {
                            SendToClient(stream, "ROOM_NOT_FOUND");
                        }
                    }
                    else
                    {
                        SendToClient(stream, "INVALID_GET_PLAYERS_FORMAT");
                    }
                    break;

                default:
                    SendToClient(stream, "UNKNOWN_COMMAND");
                    break;
            }
        }

        private void SendToClient(NetworkStream stream, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public NetworkStream Stream { get; set; }
    }

    public class GameRoom
    {
        public string RoomID { get; set; }
        private List<Player> players = new List<Player>();
        private const int MaxPlayers = 4;

        public GameRoom(string roomID)
        {
            RoomID = roomID;
        }

        public bool AddPlayer(Player player)
        {
            if (players.Count >= MaxPlayers)
                return false;

            players.Add(player);
            return true;
        }

        public List<string> GetPlayerNames()
        {
            List<string> names = new List<string>();
            foreach (var p in players)
                names.Add(p.Name);
            return names;
        }
    }
}
