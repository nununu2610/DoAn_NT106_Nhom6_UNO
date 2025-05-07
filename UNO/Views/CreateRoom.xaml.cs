using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;

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
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        private void btnCreateRoom_Click(object sender, RoutedEventArgs e)
        {
            string localIP = GetLocalIPAddress();

            // Lấy chế độ chơi từ ComboBox
            string selectedMode = ((ComboBoxItem)cbbCount.SelectedItem)?.Content.ToString() ?? "Unknown";

            // Mở form CreatedRoom và truyền thông tin
            CreatedRoom createdRoom = new CreatedRoom(localIP, selectedMode);
            createdRoom.Show();

            // Đóng form CreateRoom
            this.Close();
        }
    }
}
