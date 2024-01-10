using AvaloniaGUI.Models.Global;

namespace AvaloniaGUI.ViewModels.Others;

public class VmSettings : ViewModelBase
{
    public int? AccuracyLevelSelectedIndex
    {
        get => Settings.ShareAccuracyLevelSelectedIndex;
        set => this.RaiseAndSetIfChanged(ref Settings.ShareAccuracyLevelSelectedIndex, value);
    }
}