using System;
using System.Windows;

namespace UNO.Views
{
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            var menu = new Menu();  
            menu.Show();         
            this.Close();
        }

    }
}
