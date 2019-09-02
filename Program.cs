using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Konzolovka.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Konzolovka
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureHostConfiguration(configBuilder =>
                {
                    configBuilder.AddJsonFile("appsettings.json", false, true);
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    
                })
                .ConfigureContainer<ContainerBuilder>(autofacContainer =>
                {
                    autofacContainer.RegisterType<TestService>().InstancePerLifetimeScope();
                })
                .ConfigureServices((hostBuilderContext, serviceCollection) =>
                {
                    serviceCollection.AddHostedService<ApplicationRoot>();
                    serviceCollection.Configure<TestOptions>(hostBuilderContext.Configuration.GetSection("Test"));
                }).Build();

            await host.StartAsync();
        }
    }
}
