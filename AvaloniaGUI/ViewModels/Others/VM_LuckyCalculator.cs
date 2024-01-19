using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AvaloniaGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NewCoreLibrary.LuckyValue;

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
                                   int.TryParse(NumbersOfTotalGachaTimes, out var totalTimes) && characterTimes >= 0 &&
                                   weaponTimes >= 0 && (characterTimes + weaponTimes >= 1) &&
                                   totalTimes >= characterTimes + weaponTimes;

    [RelayCommand(CanExecute = nameof(CanCalculate))]
    private async Task CalculateAsync()
    {
        ValidateAllProperties();
        if (HasErrors) return;
        var calculator = new LuckyCalculate();
        var result = await Task.Run(() => calculator.Calculate(
            targetAmountOfLimitedCharacters: int.Parse(NumbersOfCharacters),
            lastCharacterNormal: IsLastLimitedCharacterFailed,
            targetAmountOfLimitedWeapons: int.Parse(NumbersOfWeapons),
            lastWeaponNormal: IsLastLimitedWeaponFailed,
            gachaTimes: int.Parse(NumbersOfTotalGachaTimes)));
        Result =
            $"模拟抽取{NumbersOfCharacters}只Up角色和{NumbersOfWeapons}把Up光锥的情况：\n你花费了总计{NumbersOfTotalGachaTimes}抽，根据模拟计算结果，\n你的欧非百分比为（百分比越低运气越好）为：{result * 100:F2}%，超越了{(1 - result) * 100:F2}%的玩家。";
    }
}