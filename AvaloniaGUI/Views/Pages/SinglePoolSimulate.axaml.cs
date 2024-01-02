using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaGUI.ViewModels.Others;

namespace AvaloniaGUI.Views.Pages;

public partial class SinglePoolSimulate : UserControl
{
    public SinglePoolSimulate()
    {
        InitializeComponent();
        DataContext = new VmSinglePool();
    }
}