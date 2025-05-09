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
            GameServer gameServer = new GameServer();
            gameServer.Start();

            string localIP = GetLocalIPAddress();
            string selectedMode = ((ComboBoxItem)cbbCount.SelectedItem)?.Content.ToString() ?? "Unknown";

            CreatedRoom createdRoom = new CreatedRoom(localIP, selectedMode);
            createdRoom.Show();

            GameBoard board = new GameBoard();  // Mở giao diện bàn chơi
            board.Show();

            this.Close();  // Đóng cửa sổ CreateRoom
        }

    }
}
