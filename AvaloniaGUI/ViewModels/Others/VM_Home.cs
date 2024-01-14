using AvaloniaGUI.Models;
using AvaloniaGUI.Models.Global;
using CommunityToolkit.Mvvm.ComponentModel;


namespace AvaloniaGUI.ViewModels.Others;

public class VmHome : ObservableObject
{
    public string OsVersion => Home.OsVersion;
    public string AppVersion => Information.AppVersion;
}