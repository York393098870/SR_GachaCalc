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
            //Console.WriteLine(GachaCalculate.gacha(100000));
            const int times = 10000000;
            var averageTimes = Math.Round((double)times / GachaCalculate.gacha(times), 2);
            Console.WriteLine("总共模拟抽卡:" + times + "次，获得五星角色的期望抽数为：" + averageTimes + "抽");
        }
    }
}