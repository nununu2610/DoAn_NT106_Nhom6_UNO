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
    /// Interaction logic for Rule1.xaml
    /// </summary>
    public partial class Rule1 : Window
    {
        public Rule1()
        {
            InitializeComponent();
        }

      
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var rule2 = new Rule2();
            rule2.Show();
            this.Close();
        }
    }
}
