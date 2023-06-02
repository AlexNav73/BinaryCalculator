using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BinaryCalculator.UI.ViewModels;

internal partial class MainWindowViewModel : ObservableObject, IMainWindowViewModel
{
    [ObservableProperty]
    private string _text;

    [RelayCommand]
    private void One()
    {
        Text = Text + "1";
    }
}
