using System;
using System.Reactive;
using GUI_Avalonia.Models;
using MsBox.Avalonia;
using ReactiveUI;


namespace GUI_Avalonia.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, Unit> SinglePoolCalculate { get; }
    public ReactiveCommand<Unit, Unit> LuckyValueCalculate { get; }

    public MainWindowViewModel()
    {
        var singlePoolCalculateCanExecute = this.WhenAny(x => x.IsCharacterPoolSelected, x => x.IsWeaponPoolSelected,
                (characterSelected, weaponSelected) => characterSelected.GetValue() || weaponSelected.GetValue())
            ;
        SinglePoolCalculate = ReactiveCommand.Create(CalculateSinglePool, singlePoolCalculateCanExecute);

        var luckyValueCalculateCanExecute =
            this.WhenAnyValue(x => x.LimitedCharacterCounts, x => x.LimitedWeaponCounts, x => x.GachaTimes,
                (limitedCharacterCounts, limitedWeaponCounts, gachaTimes) =>
                    !string.IsNullOrEmpty(limitedCharacterCounts) && !string.IsNullOrEmpty(limitedWeaponCounts) &&
                    !string.IsNullOrEmpty(gachaTimes));
        LuckyValueCalculate = ReactiveCommand.Create(CalculateLuckyValue, luckyValueCalculateCanExecute);
    }


    private void CalculateSinglePool()
    {
        int poolType;
        if (IsCharacterPoolSelected)
        {
            poolType = 1;
        }
        else if (IsWeaponPoolSelected)
        {
            poolType = 2;
        }
        else
        {
            throw new InvalidOperationException("No pool type selected");
        }


        if (Tools.InputValidationCheck.OnlyAllowPositiveNumbers(GachaByTimesInput))
        {
            var result = GachaByTimesModel.Process(int.Parse(GachaByTimesInput), poolType);
            GachaByTimesOutput = result;
        }
        else
        {
            GachaByTimesInput = "0";
            var alertBox = MessageBoxManager.GetMessageBoxStandard("警告", "抽卡次数应为正整数，请重新输入");
            alertBox.ShowAsync();
        }
    }

    private void CalculateLuckyValue()
    {
    }
}