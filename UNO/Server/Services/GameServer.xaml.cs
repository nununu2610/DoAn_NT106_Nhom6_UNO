using System;
using System.Windows;

namespace UNO.Server.Services
{
    public partial class GameServer : Window
    {
        private SocketServer server;

        public GameServer()
        {
            InitializeComponent();
            server = new SocketServer(); // Chỉ khởi tạo, chưa Start
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                server.Start(5000); // Start với cổng 5000
                MessageBox.Show("Server started on port 5000.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to start server: " + ex.Message);
            }
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                server.Stop();
                MessageBox.Show("Server stopped.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to stop server: " + ex.Message);
            }
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            // Mở form để vào game (giả định bạn có form GameBoard)
            GameBoard gameBoard = new GameBoard();
            gameBoard.Show();
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            server?.Stop(); // Dừng server nếu form bị đóng
            base.OnClosed(e);
        }
    }
}
