using System.Reactive;
using System.Windows.Input;
using AvaloniaGUI.Models;
using CommunityToolkit.Mvvm.Input;
using MsBox.Avalonia;
using static AvaloniaGUI.Models.Global.Settings;

namespace AvaloniaGUI.ViewModels.Others;

public class VmSinglePool : ViewModelBase
{
    //单卡池抽卡

    private string _singlePoolSimulateTimes = string.Empty;
    private string _singlePoolSimulateResult = "尚未开始抽卡模拟，待模拟完成后显示结果";
    private string _poolTypeIndex = "0";
    public ICommand SinglePoolSimulateCommand { get; }


    public VmSinglePool()
    {
        SinglePoolSimulateCommand = new RelayCommand(SinglePoolSimulate);
    }

    public string PoolTypeIndex
    {
        get => _poolTypeIndex;
        set => this.SetProperty(ref _poolTypeIndex, value);
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

    public string SinglePoolSimulateTimes
    {
        get => _singlePoolSimulateTimes;
        set => SetProperty(ref _singlePoolSimulateTimes, value);
    }

    public string SinglePoolSimulateResult
    {
        get => _singlePoolSimulateResult;
        set => this.SetProperty(ref _singlePoolSimulateResult, value);
    }

    private string ShareAccuracyLevel
    {
        get
        {
            return ShareAccuracyLevelSelectedIndex switch
            {
                0 => "低精度",
                1 => "中精度",
                2 => "高精度",
                _ => "未知精度"
            };
        }
    }


    private void SinglePoolSimulate()
    {
        if (int.TryParse(SinglePoolSimulateTimes, out var simulateTimes))
        {
            if (simulateTimes >= 1)
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
        else
        {
            var inputErrorMsgBox = MessageBoxManager.GetMessageBoxStandard("错误", "请输入大于1的整数。");
            inputErrorMsgBox.ShowAsync();
        }
    }
}