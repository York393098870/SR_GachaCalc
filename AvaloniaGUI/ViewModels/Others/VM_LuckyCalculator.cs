using System.ComponentModel.DataAnnotations;
using AvaloniaGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaGUI.ViewModels.Others;

[ObservableRecipient]
public partial class VmLuckyCalculator : ObservableValidator
{
    [ObservableProperty]
    [Required(ErrorMessage = Messages.CanNotBeEmpty)]
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    private string _numbersOfCharacters = "1";

    [ObservableProperty]
    [Required(ErrorMessage = Messages.CanNotBeEmpty)]
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    private string _numbersOfWeapons = "1";

    [ObservableProperty]
    [Required(ErrorMessage = Messages.CanNotBeEmpty)]
    [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
    private string _numbersOfTotalGachaTimes = "0";

    [ObservableProperty] private bool _isLastLimitedCharacterFailed;
    [ObservableProperty] private bool _isLastLimitedWeaponFailed;
    [ObservableProperty] private string _result = "当前尚未开始模拟计算，请输入数据后点击计算按钮。";

    private bool CanCalculate() => int.TryParse(NumbersOfCharacters, out var characterTimes) &&
                                   int.TryParse(NumbersOfWeapons, out var weaponTimes) &&
                                   int.TryParse(NumbersOfTotalGachaTimes, out var totalTimes) && characterTimes >= 1 &&
                                   weaponTimes >= 1 &&
                                   totalTimes >= characterTimes + weaponTimes;

    [RelayCommand(CanExecute = nameof(CanCalculate))]
    private void Calculate()
    {
        ValidateAllProperties();
        if (!HasErrors)
        {
            //Todo: Calculate
        }
    }
}