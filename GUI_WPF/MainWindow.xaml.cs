using System.Windows;
using System.Diagnostics;

namespace GUI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
            var result = CoreLibraries.GachaCalcApi.GachaInPoolByTimes(tryCharacterTimes, 1);
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
            var result = CoreLibraries.GachaCalcApi.GachaInPoolByTimes(tryWeaponTimes, 2);
            MessageBox.Show(result.Item3, "抽卡结果");
        }

        private void OpenWebsite_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(CoreLibraries.GlobalVariables.UrlGithub) { UseShellExecute = true });
        }
    }
}