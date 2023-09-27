using Avalonia.Controls;
using Avalonia.Interactivity;
using CoreLibraries;
using MsBox.Avalonia;

namespace AvaloniaApplication;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void ButtonClicked(object source, RoutedEventArgs args)
    {
        const int tryRoleTimes = 1000;
        var resultMessage = GachaCalcApi.GachaInPoolByTimes(tryRoleTimes, 1).Item3;
        var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandard("抽卡结果", resultMessage);
        messageBoxStandardWindow.ShowWindowAsync();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
    }
}