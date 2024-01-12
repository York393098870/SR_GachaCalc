using System.Windows.Input;
using AvaloniaGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MsBox.Avalonia;

namespace AvaloniaGUI.ViewModels.Others;

public partial class VmSinglePool : ViewModelBase
{
    //单卡池

    [ObservableProperty]
    private string _singlePoolSimulateTimes = string.Empty;
    [ObservableProperty]
    private string _singlePoolSimulateResult = "尚未开始模拟，待模拟完成后显示结果";
    [ObservableProperty] private string _poolTypeIndex = "0";
    public ICommand SinglePoolSimulateCommand { get; }


    public VmSinglePool()
    {
        SinglePoolSimulateCommand = new RelayCommand(SinglePoolSimulate);
    }


    private Tools.PoolType PoolType
    {
        get
        {
            return PoolTypeIndex switch
            {
                "0" => Tools.PoolType.LimitedCharacterPool,
                "1" => Tools.PoolType.LimitedWeaponPool,
                _ => Tools.PoolType.UnknownPoolType
            };
        }
    }


    private void SinglePoolSimulate()
    {
        if (int.TryParse(SinglePoolSimulateTimes, out var simulateTimes) && simulateTimes >= 1)
        {
            var resultMessage = SinglePoolCalculate.Calculate(PoolType, simulateTimes);
            SinglePoolSimulateResult = resultMessage;
        }
        else
        {
            var inputErrorMsgBox = MessageBoxManager.GetMessageBoxStandard("错误", "请输入大于1的整数。");
            inputErrorMsgBox.ShowAsync();
        }
    }
}