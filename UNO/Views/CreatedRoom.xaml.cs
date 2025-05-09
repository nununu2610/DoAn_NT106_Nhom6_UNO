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

        public class GameServer
        {
            public void Start()
            {
                // Code để khởi động server
                Console.WriteLine("Server started...");
            }
        }

        public CreatedRoom()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
           


            // Mở giao diện GameBoard sau khi khởi động server
            GameBoard boardWindow = new GameBoard();
            Application.Current.MainWindow = boardWindow;

            boardWindow.Show();  // Hiển thị GameBoard

            this.Hide();  // Đóng cửa sổ CreatedRoom
           
        }
    }
}
