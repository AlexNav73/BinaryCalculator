using BinaryCalculator.UI.ViewModels;
using System.Windows;

namespace BinaryCalculator.UI;

public partial class MainWindow : Window, IMainWindow
{
    public MainWindow(IMainWindowViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}