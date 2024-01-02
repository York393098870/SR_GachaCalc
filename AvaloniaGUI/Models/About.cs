using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using MsBox.Avalonia;


namespace AvaloniaGUI.Models;

public class About
{
    public static void OpenGitHubPage()
    {
        const string targetUrl = "https://github.com/York393098870/SR_GachaCalc";
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                //调用ShellExecute打开浏览器

                Process.Start(new ProcessStartInfo(targetUrl) { UseShellExecute = true });
            }
            else
            {
                //Todo:在其他平台打开浏览器
                var errorMsgBox = MessageBoxManager.GetMessageBoxStandard("错误", "该功能在当前操作系统不可用。");
                errorMsgBox.ShowAsync();
            }
        }
        catch (Exception ex)
        {
            var errorMsgBox = MessageBoxManager.GetMessageBoxStandard("错误", $"{ex.Message}");
            errorMsgBox.ShowAsync();
        }
    }
}