using Autofac;using System.Linq;
using System.Reflection;

namespace BluePrismWordLadder
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.Load(nameof(BluePrismWordLadder)))
                .Where(t => t.Namespace.Contains("Logic"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            return builder.Build();
        }
    }
}
