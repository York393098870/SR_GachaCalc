using CommunityToolkit.Mvvm.Input;

namespace AvaloniaGUI.ViewModels.Others;

public partial class VmAbout : ViewModelBase
{
    public string AboutMessage =>
        "《崩坏·星穹铁道》全能工具箱是一款基于崩坏星穹铁道抽卡系统的抽卡模拟器，计划提供单卡池抽卡模拟，欧非计算，抽卡规划等功能。目前部分功能仍在施工当中。抽卡数据基于B站Up主'一颗平衡树'。软件采用AGPLv3开源协议，源代码托管于GitHub。";

    [RelayCommand]
    private void OpenGitHubPage()
    {
        Models.About.OpenGitHubPage();
    }
}