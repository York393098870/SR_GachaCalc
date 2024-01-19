using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AvaloniaGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaGUI.ViewModels.Others;

[ObservableRecipient]
public partial class VmSinglePool : ObservableValidator
{
    //单卡池

    [ObservableProperty]
    [Required(ErrorMessage = "模拟次数不允许为空！")]
    [Range(1, int.MaxValue, ErrorMessage = "请输入大于等于1的整数。")]
    [NotifyCanExecuteChangedFor(nameof(SinglePoolSimulateCommand))]
    private string _singlePoolSimulateTimes = "1";

    [ObservableProperty] private string _singlePoolSimulateResult = "尚未开始模拟，待模拟完成后显示结果";
    [ObservableProperty] private string _poolTypeIndex = "0";
    public string AverageMessages => "根据相关数据，Up角色抽取期望为：93.446抽，Up光锥抽取期望为：66.84抽。";

    private Tools.PoolType PoolType =>
        PoolTypeIndex switch
        {
            "0" => Tools.PoolType.LimitedCharacterPool,
            "1" => Tools.PoolType.LimitedWeaponPool,
            _ => Tools.PoolType.UnknownPoolType
        };


    public bool CanSimulate => int.TryParse(SinglePoolSimulateTimes, out var intResult) && intResult >= 1; //控制按钮是否可用


    [RelayCommand(CanExecute = nameof(CanSimulate))]
    private async Task SinglePoolSimulateAsync()
    {
        ValidateAllProperties();

        if (int.TryParse(SinglePoolSimulateTimes, out var simulateTimes) && simulateTimes >= 1)
        {
            var resultMessage = await Task.Run(() => SinglePoolCalculate.Calculate(PoolType, simulateTimes));
            SinglePoolSimulateResult = resultMessage;
        }
        else
        {
            MessageBox.ShowResultShouldOver1AndInt();
            SinglePoolSimulateTimes = "1";
        }
    }
}