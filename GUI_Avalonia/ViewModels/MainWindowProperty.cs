using System;
using ReactiveUI;

namespace GUI_Avalonia.ViewModels;

public partial class MainWindowViewModel
{
    public string GachaByTimesInput
    {
        get => _gachaByTimesInput;
        set => this.RaiseAndSetIfChanged(ref _gachaByTimesInput, value);
    }

    public string GachaByTimesOutput
    {
        get => _gachaByTimesOutput;
        set => this.RaiseAndSetIfChanged(ref _gachaByTimesOutput, value);
    }

    public bool IsCharacterPoolSelected
    {
        get => _isCharacterPoolSelected;
        set => this.RaiseAndSetIfChanged(ref _isCharacterPoolSelected, value);
    }

    public bool IsWeaponPoolSelected
    {
        get => _isWeaponPoolSelected;
        set => this.RaiseAndSetIfChanged(ref _isWeaponPoolSelected, value);
    }

    public string LimitedCharacterCounts
    {
        get => _limitedCharacterCounts;
        set
            => this.RaiseAndSetIfChanged(ref _limitedCharacterCounts, value);
    }

    public string LimitedWeaponCounts
    {
        get => _limitedWeaponCounts;
        set => this.RaiseAndSetIfChanged(ref _limitedWeaponCounts, value);
    }

    public string GachaTimes
    {
        get => _gachaTimes;
        set => this.RaiseAndSetIfChanged(ref _gachaTimes, value);
    }

    public string AccuracyLevel
    {
        get => _accuracyLevel;
        set => this.RaiseAndSetIfChanged(ref _accuracyLevel, value);
    }
}