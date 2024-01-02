using ReactiveUI;

namespace AvaloniaGUI.ViewModels;

public partial class MainWindowViewModel
{
    public string AccuracyLevelNumber
    {
        get => _accuracyLevelNumber;
        set => this.RaiseAndSetIfChanged(ref _accuracyLevelNumber, value);
    }
}