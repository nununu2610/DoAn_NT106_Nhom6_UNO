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
    /// Interaction logic for Rule6.xaml
    /// </summary>
    public partial class Rule6 : Window
    {
        public Rule6()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Rule5 rule5 = new Rule5();
            rule5.Show();
            this.Close();
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            Rule7 rule7 = new Rule7();
            rule7.Show();
            this.Close();
        }
    }
}
