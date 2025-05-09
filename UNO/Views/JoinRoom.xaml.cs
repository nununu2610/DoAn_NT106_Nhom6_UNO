using System;
using System.Net.Sockets;
using System.Windows;
using UNO.Client.Services;
using UNO.Views.Game;

namespace UNO.Views
{
    public partial class JoinRoom : Window
    {
        public JoinRoom(bool isJoinRoom)
        {
            InitializeComponent();

            if (isJoinRoom)
            {
                Console.WriteLine("Joining room...");
            }
        }

        // Logic tham gia phòng
        private bool JoinRoomLogic(string playerName, string roomIP)
        {
            try
            {
                SocketClient client = new SocketClient();

                // Thử kết nối đến server
                client.Connect(roomIP, 12345);
                Console.WriteLine("Connected to server at " + roomIP + ":12345");

                // Gửi yêu cầu tham gia phòng
                bool success = client.JoinRoom(playerName, roomIP);
                Console.WriteLine("Join room response: " + success);

                return success;
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Lỗi khi kết nối đến server:\n" + ex.Message, "Lỗi kết nối", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tham gia phòng:\n" + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }


        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            string playerName = txtNameJoin.Text;
            string roomIP = txtIP.Text;

            bool result = JoinRoomLogic(playerName, roomIP);

            if (result)
            {
                MessageBox.Show("Tham gia phòng thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                WaitingRoom waitingRoom = new WaitingRoom(playerName, roomIP); // Truyền tên người chơi và IP phòng
                waitingRoom.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Không thể tham gia phòng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
