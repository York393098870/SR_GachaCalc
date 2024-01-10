using AvaloniaGUI.Models.Global;

namespace AvaloniaGUI.ViewModels.Others;

public class VmSettings : ViewModelBase
{
    public int? AccuracyLevelSelectedIndex
    {
        get => Settings.ShareAccuracyLevelSelectedIndex;
        set => SetProperty(ref Settings.ShareAccuracyLevelSelectedIndex, value);
    }
}