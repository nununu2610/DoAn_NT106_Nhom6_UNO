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
    /// Interaction logic for Rule4.xaml
    /// </summary>
    public partial class Rule4 : Window
    {
        public Rule4()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Rule3 rule3 = new Rule3();
            rule3.Show();
            this.Close();
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            Rule5 rule5 = new Rule5();
            rule5.Show();
            this.Close();
        }
    }
}
