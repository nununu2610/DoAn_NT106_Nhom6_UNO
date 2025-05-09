using System;
using System.Windows;
using UNO.Views.Game;

namespace UNO.Views
{
    /// <summary>
    /// Interaction logic for CreatedRoom.xaml
    /// </summary>
    public partial class CreatedRoom : Window
    {
        private string roomIP;
        private string selectedMode;

        // Constructor nhận thông tin IP và chế độ số lượng người chơi
        public CreatedRoom(string roomIP, string selectedMode)
        {
            InitializeComponent();
            this.roomIP = roomIP;
            this.selectedMode = selectedMode;

            // Hiển thị thông tin phòng
            txtIP1.Text = $"Room IP: {roomIP}";
            txtMode.Text = $"Mode: {selectedMode}";
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Mở cửa sổ WaitingRoom
                WaitingRoom waitingRoom = new WaitingRoom(roomIP, selectedMode);
                waitingRoom.Show();

                // Đóng cửa sổ CreatedRoom
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi chuyển sang WaitingRoom:\n" + ex.ToString(), "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
