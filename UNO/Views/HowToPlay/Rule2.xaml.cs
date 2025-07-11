﻿using System;
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
    /// Interaction logic for Rule2.xaml
    /// </summary>
    public partial class Rule2 : Window
    {
        public Rule2()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Rule1 rule1 = new Rule1();
            rule1.Show();
            this.Close();
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            Rule3 rule3 = new Rule3();
            rule3.Show();
            this.Close();
        }
    }
}
