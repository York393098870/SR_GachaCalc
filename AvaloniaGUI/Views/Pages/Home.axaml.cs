using Avalonia.Controls;

namespace AvaloniaGUI.Views.Pages;

public partial class Home : UserControl
{
    public Home()
    {
        InitializeComponent();
        DataContext = new ViewModels.Others.VmHome();
    }
}