using System;
using System.Windows;
using System.Windows.Controls;
using UNO.Client.Services;

namespace UNO.Views
{
    public partial class CreateRoom : Window
    {
        public CreateRoom()
        {
            InitializeComponent();
        }

        private void btnCreateRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 👉 Khởi động server trước nếu chưa chạy
                StartServerIfNeeded();

                // Sau đó tạo client để kết nối tới server
                SocketClient client = new SocketClient();
                client.Connect();

                string playerName = txtName.Text.Trim();
                string selectedMode = (cbbCount.SelectedItem as ComboBoxItem)?.Content.ToString();

                string roomID;
                bool created = client.CreateRoom(playerName, selectedMode, out roomID);

                if (created)
                {
                    WaitingRoom waitingRoom = new WaitingRoom(roomID, selectedMode, playerName, client);
                    waitingRoom.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Unable to create the room. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while starting the game:\n" + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static bool serverStarted = false;

        private void StartServerIfNeeded()
        {
            if (!serverStarted)
            {
                serverStarted = true;

                Thread serverThread = new Thread(() =>
                {
                    var server = new UNO.Server.SocketServer();
                    server.Start(8888);
                });

                serverThread.IsBackground = true;
                serverThread.Start();

                // Cho server một chút thời gian để khởi động
                Thread.Sleep(500);
            }
        }


    }
}
