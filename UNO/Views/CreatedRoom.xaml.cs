using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UNO.Server.Services;
using UNO.Views.Game;

namespace UNO.Views
{
    /// <summary>
    /// Interaction logic for CreatedRoom.xaml
    /// </summary>
    public partial class CreatedRoom : Window
    {

        public CreatedRoom(string ip, string mode)
        {
            InitializeComponent();

            // Hiển thị IP và chế độ chơi
            txtIP1.Text = ip;
            txtMode.Text = mode;
        }

        public CreatedRoom()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            GameServer gameServer = new GameServer();  // Tạo đối tượng GameServer
            gameServer.Start();  // Khởi động server, sử dụng phương thức Start() từ GameServer

            // 2. Mở giao diện GameBoard
            GameBoard boardWindow = new GameBoard();
            boardWindow.Show();

            // 3. Đóng CreateRoom nếu cần
            this.Close(); // hoặc this.Hide() nếu muốn giữ CreateRoom ở background
        }
    }
}
