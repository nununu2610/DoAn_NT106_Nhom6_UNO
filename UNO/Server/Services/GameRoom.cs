using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;

namespace UNO.Server.Services
{
    public class GameRoom
    {
        public string RoomID { get; private set; }
        public List<TcpClient> Players { get; private set; }
        public List<string> PlayerNames { get; private set; }
        public bool IsFull => Players.Count >= 4;

        public GameRoom(string roomID)
        {
            RoomID = roomID;
            Players = new List<TcpClient>();
            PlayerNames = new List<string>();
        }

        public bool AddPlayer(TcpClient client, string playerName)
        {
            if (IsFull) return false;

            Players.Add(client);
            PlayerNames.Add(playerName);
            return true;
        }

        public List<string> GetPlayerNames()
        {
            return PlayerNames.ToList();
        }
    }
}
