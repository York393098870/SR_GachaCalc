using Avalonia.Controls;
using AvaloniaGUI.ViewModels.Others;

namespace AvaloniaGUI.Views.Pages;

public partial class Settings : UserControl
{
    public Settings()
    {
        InitializeComponent();
        DataContext = new VmSettings();
    }
}