using System;
using System.Reactive;
using GUI_Avalonia.Models;
using ReactiveUI;

namespace GUI_Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _gachaByTimesInput;
    private string _gachaByTimesOutput = "尚未开始模拟抽卡：\r\n请选择卡池后，输入模拟抽卡次数并点击计算";

    private bool _isCharacterPoolSelected;
    private bool _isWeaponPoolSelected;

    private bool IsOnePoolSelected => IsCharacterPoolSelected || IsWeaponPoolSelected;

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


    public ReactiveCommand<Unit, Unit> ProcessCommand { get; }

    public MainWindowViewModel()
    {
        var canExecute = this.WhenAny(x => x.IsCharacterPoolSelected, x => x.IsWeaponPoolSelected,
                (characterSelected, weaponSelected) => characterSelected.GetValue() || weaponSelected.GetValue())
            ;
        ProcessCommand = ReactiveCommand.Create(Process, canExecute);
    }

    private void Process()
    {
        var model = new GachaByTimesModel();
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

        GachaByTimesOutput = GachaByTimesModel.Process(int.Parse(GachaByTimesInput), poolType);
    }
}