using System;
using System.Threading;
using System.Threading.Tasks;
using Konzolovka.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Konzolovka
{
    public class ApplicationRoot
    {
        private readonly TestOptions options;
        private readonly TestService testService;

        public ApplicationRoot(IOptions<TestOptions> options, TestService testService)
        {
            this.testService = testService;
            this.options = options.Value;
        }

        public async Task Run(string[] args, CancellationToken cancellationToken = new CancellationToken())
        {
            Console.WriteLine(testService.Test());
        }
    }
}