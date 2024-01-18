using System.ComponentModel.DataAnnotations;
using AvaloniaGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaGUI.ViewModels.Others;

[ObservableRecipient]
public partial class VmLuckyCalculator : ObservableValidator
{
    [ObservableProperty] [Required] private string _timesInLimitedCharacterPool;
    [ObservableProperty] [Required] private string _timesInLimitedWeaponPool;

    [RelayCommand]
    private void Calculate()
    {
        ValidateAllProperties();
        if (HasErrors == false && int.TryParse(TimesInLimitedCharacterPool, out var characterTimes) &&
            int.TryParse(TimesInLimitedWeaponPool, out var weaponTimes) && characterTimes >= 1 && weaponTimes >= 1)
        {
            //Todo: Calculate
        }
        else
        {
            MessageBox.ShowResultShouldOver1AndInt();
            TimesInLimitedCharacterPool = "1";
            TimesInLimitedWeaponPool = "1";
        }
    }
}