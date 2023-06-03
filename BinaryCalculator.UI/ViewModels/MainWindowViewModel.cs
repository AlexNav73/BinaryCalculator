using BinaryCalculator.BL;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BinaryCalculator.UI.ViewModels;

internal partial class MainWindowViewModel : ObservableObject, IMainWindowViewModel
{
    private readonly ICalculator _calculator;

    [ObservableProperty]
    private string _text = new string('0', 13);

    public MainWindowViewModel(ICalculator calculator)
    {
        _calculator = calculator;
    }

    [RelayCommand]
    private void One()
    {
        Text = ToBinary(_calculator.Push(true));
    }

    [RelayCommand]
    private void Zero()
    {
        Text = ToBinary(_calculator.Push(false));
    }

    [RelayCommand]
    private void Clear()
    {
        Text = ToBinary(_calculator.Clear());
    }

    [RelayCommand]
    private void Add()
    {
        Text = ToBinary(_calculator.Execute(Operation.Add));
    }

    [RelayCommand]
    private void Subtract()
    {
        Text = ToBinary(_calculator.Execute(Operation.Subtract));
    }

    [RelayCommand]
    private void Equals()
    {
        Text = ToBinary(_calculator.Equals());
    }

    private string ToBinary(int value)
    {
        return Convert.ToString(value, 2).PadLeft(13, '0');
    }
}
