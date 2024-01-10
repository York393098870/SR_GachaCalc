namespace AvaloniaGUI.ViewModels.Others.GachaPlanning;

public class VmCostCalculator : ViewModelBase
{
    public string Character1Name { get; set; } = "1号角色";
    public string Character2Name { get; set; } = "2号角色";
    public string Character3Name { get; set; } = "3号角色";
    public string Character4Name { get; set; } = "4号角色";

    private string _角色1命座数 = "0";
    private string _专武1叠影数 = "0";
    private bool _角色是否为五星 = true;

    public string 角色1命座数
    {
        get => _角色1命座数;
        set => this.RaiseAndSetIfChanged(ref _角色1命座数, value);
    }

    public string 专武1叠影数
    {
        get => _专武1叠影数;
        set => this.RaiseAndSetIfChanged(ref _专武1叠影数, value);
    }

    public bool 角色是否为五星
    {
        get => _角色是否为五星;
        set => this.RaiseAndSetIfChanged(ref _角色是否为五星, value);
    }
}