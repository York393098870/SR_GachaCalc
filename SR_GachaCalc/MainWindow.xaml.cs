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
            const int times = 844;
            var gachaResult = GachaCalculate.Gacha(times);
            var averagelimitedcount = Math.Round((double)times / gachaResult.Item1, 2);
            var averagetotalcount = Math.Round((double)times / (gachaResult.Item1 + gachaResult.Item2), 2);
            Console.WriteLine(
                $"你总共模拟抽卡{times}次，获得五星数量{gachaResult.Item1 + gachaResult.Item2}只，五星平均抽数为{averagetotalcount}。其中，获得限定五星数量{gachaResult.Item1}只，限定五星平均抽数为{averagelimitedcount}；获得常驻五星数量{gachaResult.Item2}只。");
        }
    }
}