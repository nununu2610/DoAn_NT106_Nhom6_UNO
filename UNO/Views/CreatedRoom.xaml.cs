using System;
using System.Windows;
using UNO.Client.Services;

namespace UNO.Views
{
    public partial class CreatedRoom : Window
    {
        private string roomIP;
        private string selectedMode;
        private string playerName;
        private SocketClient client;

        public CreatedRoom(string roomIP, string selectedMode, string playerName, SocketClient client)
        {
            InitializeComponent();
            this.roomIP = roomIP;
            this.selectedMode = selectedMode;
            this.playerName = playerName;
            this.client = client;

            // Hiển thị thông tin phòng trong giao diện
            txtIP1.Text = $"Room IP: {roomIP}";
            txtMode.Text = $"Mode: {selectedMode}";
        }

      

        private void btnCreateRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Chuyển sang cửa sổ WaitingRoom sau khi nhấn nút Play
                WaitingRoom waitingRoom = new WaitingRoom(roomIP, selectedMode, playerName, client);
                waitingRoom.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while starting the game:\n" + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
