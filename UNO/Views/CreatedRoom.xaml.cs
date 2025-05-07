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
    }
}
