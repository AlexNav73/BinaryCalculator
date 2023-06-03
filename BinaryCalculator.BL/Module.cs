using Autofac;

namespace BinaryCalculator.BL;

public sealed class Module : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Calculator>().As<ICalculator>();
    }
}
