using Autofac;
using BinaryCalculator.UI;

namespace BinaryCalculator;

public static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var builder = new ContainerBuilder();

        builder.RegisterModule<UI.Module>();
        builder.RegisterModule<BL.Module>();

        using var container = builder.Build();

        var application = container.Resolve<IWpfApplication>();

        application.Start();
    }
}