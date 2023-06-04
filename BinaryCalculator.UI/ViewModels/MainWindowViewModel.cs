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
    private void ClearScreen()
    {
        Text = ToBinary(_calculator.ClearScreen());
    }

    [RelayCommand]
    private void Clear()
    {
        Text = ToBinary(_calculator.Clear());
    }

    [RelayCommand]
    private void Add()
    {
        Safe(() => Text = ToBinary(_calculator.Execute(Operation.Add)));
    }

    [RelayCommand]
    private void Subtract()
    {
        Safe(() => Text = ToBinary(_calculator.Execute(Operation.Subtract)));
    }

    [RelayCommand]
    private void Equals()
    {
        Safe(() => Text = ToBinary(_calculator.Equals()));
    }

    private void Safe(Action action)
    {
        try
        {
            action();
        }
        catch (NotSupportedException)
        {
            Text = "Invalid number";
        }
        catch (InvalidOperationException)
        {
            Text = "Something terrible happened";
        }
    }

    private string ToBinary(int value)
    {
        return Convert.ToString(value, 2).PadLeft(13, '0');
    }
}
