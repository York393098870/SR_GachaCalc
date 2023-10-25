using System;
using System.Reactive;
using GUI_Avalonia.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;


namespace GUI_Avalonia.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _gachaByTimesInput;
    private string _gachaByTimesOutput = "尚未开始模拟抽卡：\r\n请选择卡池后，输入模拟抽卡次数并点击计算";

    private bool _isCharacterPoolSelected;
    private bool _isWeaponPoolSelected;

    private string _limitedCharacterCounts;
    private string _limitedWeaponCounts;
    private string _gachaTimes;


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


    public ReactiveCommand<Unit, Unit> ProcessCommand { get; }

    public MainWindowViewModel()
    {
        var canExecute = this.WhenAny(x => x.IsCharacterPoolSelected, x => x.IsWeaponPoolSelected,
                (characterSelected, weaponSelected) => characterSelected.GetValue() || weaponSelected.GetValue())
            ;
        ProcessCommand = ReactiveCommand.Create(CalculateSinglePool, canExecute);
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
        }
    }
}