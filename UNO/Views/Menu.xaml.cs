using System.Windows;
using UNO.Views;
using UNO.Views.HowToPlay;

namespace UNO.Views
{
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            JoinRoom enterInfo = new JoinRoom(); // Truyền tham số vào constructor nếu cần
            enterInfo.Show();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateRoom enterInfo = new CreateRoom(); // Truyền tham số vào constructor nếu cần
            enterInfo.Show();
        }

        private void btnHowtoPlay_Click(object sender, RoutedEventArgs e)
        {
            var rule1 = new Rule1();
            rule1.Show();
            this.Close();
        }
    }
}
