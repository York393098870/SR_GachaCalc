namespace AvaloniaGUI.ViewModels;

public partial class MainWindowViewModel
{
    public string AccuracyLevelNumber
    {
        get => _accuracyLevelNumber;
        set => SetProperty(ref _accuracyLevelNumber, value);
    }
}