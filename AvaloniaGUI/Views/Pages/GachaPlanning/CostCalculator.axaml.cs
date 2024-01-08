using Avalonia.Controls;
using AvaloniaGUI.ViewModels.Others.GachaPlanning;

namespace AvaloniaGUI.Views.Pages.GachaPlanning;

public partial class CostCalculator : UserControl
{
    public CostCalculator()
    {
        InitializeComponent();
        DataContext = new VmCostCalculator();
    }
}