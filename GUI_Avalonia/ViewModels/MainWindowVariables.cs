namespace GUI_Avalonia.ViewModels;

public partial class MainWindowViewModel
{
    private string _gachaByTimesInput;
    private string _gachaByTimesOutput = "尚未开始模拟抽卡：\r\n请选择卡池后，输入模拟抽卡次数并点击计算";

    private bool _isCharacterPoolSelected;
    private bool _isWeaponPoolSelected;

    private string _limitedCharacterCounts;
    private string _limitedWeaponCounts;
    private string _gachaTimes;

    private string _accuracyLevel="1";
}