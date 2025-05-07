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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UNO.Views.HowToPlay;

namespace UNO.Views
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

  
        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            JoinRoom enterInfo = new JoinRoom(isJoinRoom: true);
            enterInfo.Show();
            this.Close();
        }



        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateRoom enterInfo = new CreateRoom(isCreatingRoom: true);  // Tạo một instance của CreateRoom
            enterInfo.Show(); // Hiển thị cửa sổ CreateRoom
            this.Close(); // Đóng cửa sổ hiện tại (Menu)
        }

        private void btnHowtoPlay_Click(object sender, RoutedEventArgs e)
        {
            var rule1 = new Rule1();
            rule1.Show();
            this.Close();
        }
    }
}
