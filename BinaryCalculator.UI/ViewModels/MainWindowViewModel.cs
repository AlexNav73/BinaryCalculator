using CommunityToolkit.Mvvm.ComponentModel;

namespace BinaryCalculator.UI.ViewModels;

internal partial class MainWindowViewModel : ObservableObject, IMainWindowViewModel
{
    [ObservableProperty]
    private string _text;
}
