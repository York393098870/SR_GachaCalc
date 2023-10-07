using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoreLibraries;

namespace GUI_WPF
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
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
            Process.Start(new ProcessStartInfo(GlobalVariables.UrlGithub) { UseShellExecute = true });
        }

        private async void CalculateLuckyValue(object sender, RoutedEventArgs e)
        {
            // 禁用按钮防止重复点击
            CalculateValueOfLucky.IsEnabled = false;
            try
            {
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
                    var result = await Task.Run(() => GachaCalcApi.GachaLuckyValueCalculate(characterCount, weaponCount,
                        totalGachaTimes));
                    var stringResult =
                        $"你总共花费{totalGachaTimes}抽，获得{characterCount}只限定角色，{weaponCount}把限定武器，在模拟大量玩家抽卡的情况下，你的运气超越了{result.Item2:F3}%的玩家。";
                    ResultOfLuckyValue.Text = stringResult;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
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
    }
}