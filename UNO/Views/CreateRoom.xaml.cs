using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using UNO.Views.Game;
using static UNO.Views.CreatedRoom;

namespace UNO.Views
{
    /// <summary>
    /// Interaction logic for CreateRoom.xaml
    /// </summary>
    public partial class CreateRoom : Window
    {
        // Thuộc tính để lưu trạng thái tạo phòng
        public bool IsCreatingRoom { get; set; }

        // Constructor nhận tham số để xác định trạng thái tạo phòng
        public CreateRoom(bool isCreatingRoom)
        {
            InitializeComponent();
            IsCreatingRoom = isCreatingRoom;
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            // Quay lại menu
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }

        private string GetLocalIPAddress()
        {
            string localIP = "Not found";
            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString(); // Lấy địa chỉ IP máy tính
                    break;
                }
            }
            return localIP;
        }

        private void btnCreateRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string localIP = GetLocalIPAddress();  // Lấy IP máy
                string selectedMode = ((ComboBoxItem)cbbCount.SelectedItem)?.Content.ToString() ?? "Unknown"; // Lấy chế độ số lượng người chơi (2, 3, hoặc 4)

                // Kiểm tra nếu chế độ không được chọn, hiển thị thông báo lỗi
                if (selectedMode == "Unknown")
                {
                    MessageBox.Show("Vui lòng chọn số lượng người chơi", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Tạo phòng và chuyển đến cửa sổ CreatedRoom
                CreatedRoom createdRoom = new CreatedRoom(localIP, selectedMode);
                createdRoom.Show();

                // Đóng cửa sổ tạo phòng
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tạo phòng:\n" + ex.ToString(), "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
