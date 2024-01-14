using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AvaloniaGUI.ViewModels.Others;

public partial class VmSettings : ObservableRecipient
{
    [ObservableProperty]
    private string _accuracyIndex = "1";
    
    partial void OnAccuracyIndexChanged(string value)
    {
        //向全局发送精确度，默认精确度为1（中等级）
        WeakReferenceMessenger.Default.Send(new ValueChangedMessage<string>(value));
    }
}