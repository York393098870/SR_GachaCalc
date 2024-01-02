namespace AvaloniaGUI.ViewModels.Others;

using ReactiveUI;

public class VmSettings: ViewModelBase
{
    private int? _accuracyLevelSelectedIndex;
    
    public int? AccuracyLevelSelectedIndex
    {
        get => _accuracyLevelSelectedIndex;
        set => this.RaiseAndSetIfChanged(ref _accuracyLevelSelectedIndex, value);
    }
}