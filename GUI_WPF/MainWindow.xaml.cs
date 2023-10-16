using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using CoreLibraries;
using Exception = System.Exception;

namespace GUI_WPF
{
    public partial class MainWindow
    {
        private static string? _accuracyLevel;
        private static int _accuracyFactor;

        public MainWindow()
        {
            InitializeComponent();
            _accuracyLevel = GetAccuracyLevel();
        }

        private void SetImageSource(Image imageControl, string imagePath)
        {
            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();

                imageControl.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载图片时出错: {ex.Message}", "错误");
            }
        }

        private string GetAccuracyLevel()
        {
            _accuracyFactor = AccuracyController.SelectedIndex + 1;
            return AccuracyController.SelectedIndex switch
            {
                0 => "低",
                1 => "中",
                2 => "高",
                _ => "不合法的精确度等级"
            };
        }

        private void GachaInCharacterPole_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CharacterInput.Text))
            {
                MessageBox.Show("你没有输入角色池抽数", "警告");
                return;
            }

            //模拟单角色池抽卡
            var tryCharacterTimes = int.Parse(CharacterInput.Text);
            var result = GachaCalcApi.GachaInPoolByTimes(tryCharacterTimes, 1);
            MessageBox.Show(result.Item3, "抽卡结果");
        }

        private void GachaInWeaponPole_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(WeaponInput.Text))
            {
                MessageBox.Show("你没有输入武器池抽数", "警告");
                return;
            }

            //模拟单光锥池抽卡
            var tryWeaponTimes = int.Parse(WeaponInput.Text);
            var result = GachaCalcApi.GachaInPoolByTimes(tryWeaponTimes, 2);
            MessageBox.Show(result.Item3, "抽卡结果");
        }

        private void OpenWebsite_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(GlobalStaticVariables.UrlGithub) { UseShellExecute = true });
        }

        private async void CalculateLuckyValue(object sender, RoutedEventArgs e)
        {
            // 禁用按钮防止重复点击
            CalculateValueOfLucky.IsEnabled = false;
            try
            {
                //判断输入是否为空
                if (string.IsNullOrEmpty(NumberOfLimitedCharacters.Text) ||
                    string.IsNullOrEmpty(NumberOfLimitedWeapons.Text) || string.IsNullOrEmpty(TotalGachaTimes.Text))
                {
                    MessageBox.Show("输入的数据不完整", "警告");
                }
                else
                {
                    var characterCount = int.Parse(NumberOfLimitedCharacters.Text);
                    var weaponCount = int.Parse(NumberOfLimitedWeapons.Text);
                    var totalGachaTimes = int.Parse(TotalGachaTimes.Text);

                    //判断输入是否合法
                    if (characterCount + weaponCount > totalGachaTimes || (characterCount + weaponCount == 0))
                    {
                        MessageBox.Show("输入的数据不合法", "警告");
                        return;
                    }


                    var result = await Task.Run(() => GachaCalcApi.GachaLuckyValueCalculate(characterCount, weaponCount,
                        totalGachaTimes, _accuracyFactor));
                    var stringResult =
                        $"你总共花费{totalGachaTimes}抽，获得{characterCount}只限定角色，{weaponCount}把限定武器。\r\n在模拟大量玩家抽卡的情况下，你的运气属于前{result.Item1:F2}%到{result.Item2:F2}%的水平，超越了{result.Item3:F3}%的玩家。\r\n（模拟精度：{_accuracyLevel}精度；计算用时：{result.Item4}秒）";
                    ResultOfLuckyValue.Text = stringResult;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                await Task.Delay(100);
                // 重新启用按钮
                CalculateValueOfLucky.IsEnabled = true;
            }
        }

        private void AllowOnlyNumbers(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && !Regex.IsMatch(textBox.Text, @"^\d*$"))
            {
                textBox.Clear();
            }
        }

        private void AccuracyController_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _accuracyLevel = GetAccuracyLevel();
        }

        private async void GenerateDistributionImages(object sender, RoutedEventArgs e)
        {
            ButtonOfDistributionCalculation.IsEnabled = false;

            if (string.IsNullOrEmpty(NumberOfCharacterOfDistribution.Text) ||
                string.IsNullOrEmpty(NumberOfWeaponOfDistribution.Text))
            {
                MessageBox.Show("输入的数据不完整", "警告");
            }
            else
            {
                try
                {
                    var characterCount = int.Parse(NumberOfCharacterOfDistribution.Text);
                    var weaponCount = int.Parse(NumberOfWeaponOfDistribution.Text);
                    double[]? x = null;
                    double[]? y = null;
                    await Task.Run(() =>
                    {
                        (x, y) =
                            ProbabilityDistributionPlotter.PlotProbabilityDistribution(characterCount, weaponCount);
                    });
                    PlottingArea.Plot.Clear();
                    PlottingArea.Plot.Title($"获取{characterCount}只限定角色和{weaponCount}只限定光锥的概率密度函数分布曲线");
                    PlottingArea.Plot.AddScatter(x, y);
                    PlottingArea.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"发生错误: {ex.Message}", "错误");
                }
                finally
                {
                    ButtonOfDistributionCalculation.IsEnabled = true;
                }
            }
        }
    }
}