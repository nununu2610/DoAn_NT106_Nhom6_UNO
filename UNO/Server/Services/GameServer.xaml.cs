using System;
using System.Windows;
using UNO.Server.Services;

namespace UNO.Server.Services
{
    public partial class GameServer : Window
    {
        private SocketServer _server;

        public GameServer()
        {
            InitializeComponent();
            _server = new SocketServer();
        }

        // Khi nhấn nút Start (Bắt đầu server)
        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            int port = 12345;  // Port mặc định, bạn có thể thay đổi hoặc lấy từ TextBox
            _server.Start(port);
            MessageBox.Show($"Server started on port {port}");
        }

        // Khi nhấn nút Stop (Dừng server)
        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            _server.Stop();
            MessageBox.Show("Server stopped");
        }

        // Khi nhấn nút Play (Chế độ chơi bắt đầu)
        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Game is starting!");
            // Logic để bắt đầu game (ví dụ: chuyển sang màn hình trò chơi, bắt đầu phân phát bài, v.v.)
        }
    }
}
