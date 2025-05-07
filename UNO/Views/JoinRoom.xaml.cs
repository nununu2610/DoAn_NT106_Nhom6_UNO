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

namespace UNO.Views
{
    /// <summary>
    /// Interaction logic for JoinRoom.xaml
    /// </summary>
    public partial class JoinRoom : Window
    {
        public bool IsJoinRoom { get; set; }

        public JoinRoom(bool isJoinRoom)
        {
            InitializeComponent();
            IsJoinRoom = isJoinRoom;
        }
        public JoinRoom()
        {
            InitializeComponent();
          
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
          
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
