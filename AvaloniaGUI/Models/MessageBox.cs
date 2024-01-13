using MsBox.Avalonia;

namespace AvaloniaGUI.Models;


public class MessageBox
{

    public static void ShowResultShouldOver1AndInt()
    {
        var ErrorMsgBox = MessageBoxManager.GetMessageBoxStandard("错误", "请输入大于等于1的整数。");
        ErrorMsgBox.ShowAsync();
    }
}