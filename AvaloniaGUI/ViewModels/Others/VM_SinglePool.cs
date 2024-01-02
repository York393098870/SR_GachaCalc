using ReactiveUI;

namespace AvaloniaGUI.ViewModels.Others;

public class VmSinglePool : ViewModelBase
{
    //单卡池抽卡

    private string _singlePoolSimulateTimes = string.Empty;
    private string _singlePoolSimulateResult = "尚未开始抽卡模拟，待模拟完成后显示结果";

    public string SinglePoolSimulateTimes
    {
        get => _singlePoolSimulateTimes;
        set => this.RaiseAndSetIfChanged(ref _singlePoolSimulateTimes, value);
    }
    
    public string SinglePoolSimulateResult
    {
        get => _singlePoolSimulateResult;
        set => this.RaiseAndSetIfChanged(ref _singlePoolSimulateResult, value);
    }
}