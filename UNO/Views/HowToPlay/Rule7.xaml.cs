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

namespace UNO.Views.HowToPlay
{
    /// <summary>
    /// Interaction logic for Rule7.xaml
    /// </summary>
    public partial class Rule7 : Window
    {
        public Rule7()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Rule6 rule6 = new Rule6();
            rule6.Show();
            this.Close();
        }

        private void buttonRule1_Click(object sender, RoutedEventArgs e)
        {
            Rule1 rule1 = new Rule1();
            rule1.Show();
            this.Close();
        }

        private void buttonSkip_Click(object sender, RoutedEventArgs e)
        {
           
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
