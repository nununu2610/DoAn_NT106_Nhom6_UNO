using System;
using System.Collections.Generic;
using System.Windows;
using UNO.Client.Services;

namespace UNO.Views
{
    public partial class WaitingRoom : Window
    {
        private string roomID;
        private string playerName;
        private List<string> players;
        private SocketClient client;

        // Cập nhật constructor để nhận 4 tham số
        public WaitingRoom(string roomID, string mode, string playerName, SocketClient client)
        {
            InitializeComponent();
            this.roomID = roomID;
            this.playerName = playerName;
            this.client = client;

            lblRoomInfo.Text = $"Room ID: {roomID}\nPlayer: {playerName}\nMode: {mode}";
            players = new List<string> { playerName };
            UpdatePlayerList();
        }

        private void UpdatePlayerList()
        {
            lstPlayers.Items.Clear();
            foreach (var player in players)
            {
                lstPlayers.Items.Add(player);
            }
        }

        public void AddPlayer(string newPlayerName)
        {
            // Thêm người chơi mới vào danh sách và cập nhật UI
            players.Add(newPlayerName);
            UpdatePlayerList();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra điều kiện để bắt đầu trò chơi (ví dụ: ít nhất 2 người chơi)
                if (players.Count >= 2)
                {
                    // Bắt đầu trò chơi (gửi yêu cầu tới server hoặc bắt đầu trò chơi trên client)
                    client.StartGame(roomID);
                    MessageBox.Show("Game started!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Not enough players to start the game.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while starting the game: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
