using System;
using System.Windows;
using UNO.Client.Services;

namespace UNO.Views
{
    public partial class JoinRoom : Window
    {
        public JoinRoom()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy tên người chơi và RoomID từ giao diện người dùng
                string playerName = txtNameJoin.Text.Trim();
                string roomID = txtIP.Text.Trim();  // RoomID chứ không phải IP

                // Kiểm tra nếu người chơi hoặc RoomID bị bỏ trống
                if (string.IsNullOrEmpty(playerName) || string.IsNullOrEmpty(roomID))
                {
                    MessageBox.Show("Please enter both name and room ID.");
                    return;
                }

                // Tạo đối tượng client và kết nối đến server
                SocketClient client = new SocketClient();
                client.Connect(); // Connect tới server

                // Tham gia phòng đã tạo
                bool success = client.JoinRoom(playerName, roomID);
                if (success)
                {
                    string defaultMode = "2 Players"; // Hoặc lấy từ server nếu có thể
                    WaitingRoom waitingRoom = new WaitingRoom(roomID, defaultMode, playerName, client);
                    waitingRoom.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
