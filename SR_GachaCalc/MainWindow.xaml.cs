using System;
using System.Windows;

namespace SR_GachaCalc
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(GachaCalculate.get_succeed_probability(0));
        }
    }
}