using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaGUI.ViewModels.Others;

public partial class VmSettings : ViewModelBase
{
    [ObservableProperty]
    private string _accuracyLevelSelectedIndex = "1";
    
}