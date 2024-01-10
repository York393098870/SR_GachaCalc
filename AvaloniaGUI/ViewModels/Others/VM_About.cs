using System.Reactive;
using System.Reflection.Metadata;
using System.Windows.Input;
using MsBox.Avalonia.ViewModels.Commands;

namespace AvaloniaGUI.ViewModels.Others;

public class VmAbout : ViewModelBase
{
    public VmAbout()
    {
        OpenGitHubPageCommand = new RelayCommand(_=> OpenGitHubPage());
    }

    public ICommand OpenGitHubPageCommand { get; }

    public string AboutMessage =>
        "《崩坏·星穹铁道》全能工具箱是一款基于崩坏星穹铁道抽卡系统的抽卡模拟器，可以模拟抽卡过程，也可以模拟抽卡结果，抽卡数据基于B站Up主'一颗平衡树'。软件采用AGPLv3开源协议，源代码托管于GitHub，欢迎各位大佬前往贡献代码或提供Star。";


    private void OpenGitHubPage()
    {
        Models.About.OpenGitHubPage();
    }
}