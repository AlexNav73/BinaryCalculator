using Autofac;
using BinaryCalculator.UI.ViewModels;

namespace BinaryCalculator.UI;

public class Module : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MainWindow>().As<IMainWindow>().SingleInstance();
        builder.RegisterType<Application>().As<IWpfApplication>().SingleInstance();

        builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>().SingleInstance();
    }
}
