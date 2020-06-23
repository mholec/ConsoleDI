using System.Threading;
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
            var host = Host
                .CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration(configBuilder =>
                {
                    configBuilder.AddJsonFile("appsettings.json", false, true);
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    
                })
                .ConfigureServices((hostBuilderContext, serviceCollection) =>
                {
                    serviceCollection.AddTransient<ApplicationRoot>();
                    serviceCollection.Configure<TestOptions>(hostBuilderContext.Configuration.GetSection("Test"));
                })
                .ConfigureContainer<ContainerBuilder>(autofacContainer =>
                {
                    autofacContainer.RegisterType<TestService>().InstancePerLifetimeScope();
                })
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                
                var appRoot = scope.ServiceProvider.GetRequiredService<ApplicationRoot>();
                await appRoot.Run(args, cts.Token);
            }
        }
    }
}
