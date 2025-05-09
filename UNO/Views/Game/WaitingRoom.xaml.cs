using System;
using System.Windows;

namespace UNO.Views
{
    public partial class WaitingRoom : Window
    {
        private string playerName;
        private string roomIP;

        // Constructor nhận tên người chơi và IP phòng
        public WaitingRoom(string playerName, string roomIP)
        {
            InitializeComponent();
            this.playerName = playerName;
            this.roomIP = roomIP;

            // Hiển thị thông tin người chơi và IP phòng
            txtMode.Text = $"Player: {playerName}";
            txtRoomIP.Text = $"Room IP: {roomIP}";
        }
    }
}
