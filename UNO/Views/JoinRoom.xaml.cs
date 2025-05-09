using System;
using System.Windows;
using UNO.Client.Services;

namespace UNO.Views
{
    public partial class JoinRoom : Window
    {
        // Thêm tham số isJoinRoom vào constructor để sử dụng trong logic của lớp JoinRoom
        public JoinRoom(bool isJoinRoom)
        {
            InitializeComponent();

            // Chỉ cần lưu tham số này để sử dụng nếu cần
            // Tham số này có thể dùng để điều chỉnh giao diện hoặc logic bên trong JoinRoom
            if (isJoinRoom)
            {
                // Nếu là tham gia phòng, có thể thực hiện các thao tác khác
                Console.WriteLine("Joining room...");
            }
        }

        // Sự kiện khi nhấn nút Join
      

        // Logic tham gia phòng (Gọi SocketClient để thực hiện tham gia phòng)
        private bool JoinRoomLogic(string playerName, string roomIP)
        {
            SocketClient client = new SocketClient();
            client.Connect("server_ip", 12345);  // Thay thế "server_ip" và port phù hợp với thông tin server của bạn
            return client.JoinRoom(playerName, roomIP); // Gọi phương thức JoinRoom từ SocketClient
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            string playerName = txtNameJoin.Text; // Lấy tên người chơi từ TextBox
            string roomIP = txtIP.Text; // Lấy IP phòng từ TextBox

            // Gọi hàm tham gia phòng
            bool result = JoinRoomLogic(playerName, roomIP);

            if (result)
            {
                MessageBox.Show("Joined room successfully!");
            }
            else
            {
                MessageBox.Show("Failed to join room.");
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
