using System;
using System.Threading;
using System.Threading.Tasks;
using Konzolovka.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Konzolovka
{
    public class ApplicationRoot : IHostedService
    {
        private readonly TestOptions options;
        private readonly TestService testService;

        public ApplicationRoot(IOptions<TestOptions> options, TestService testService)
        {
            this.testService = testService;
            this.options = options.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Author homepage " + options.Url);
            Console.WriteLine(testService.Test());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}