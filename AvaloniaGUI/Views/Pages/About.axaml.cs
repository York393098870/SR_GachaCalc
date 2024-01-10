using Avalonia.Controls;
using AvaloniaGUI.ViewModels.Others;

namespace AvaloniaGUI.Views.Pages;

public partial class About : UserControl
{
    public About()
    {
        InitializeComponent();
        DataContext = new VmAbout();
    }
}