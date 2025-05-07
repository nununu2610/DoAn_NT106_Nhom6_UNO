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
    /// Interaction logic for Rule3.xaml
    /// </summary>
    public partial class Rule3 : Window
    {
        public Rule3()
        {
            InitializeComponent();
        }

        private void butttonBack_Click(object sender, RoutedEventArgs e)
        {
            Rule2 rule2 = new Rule2();
            rule2.Show();
            this.Close();
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            Rule4 rule4 = new Rule4();
            rule4.Show();
            this.Close();
        }
    }
}
