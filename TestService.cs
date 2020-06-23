using System;
using Microsoft.Extensions.Logging;

namespace Konzolovka
{
    public class TestService
    {
        private readonly ILogger<TestService> logger;

        public TestService(ILogger<TestService> logger)
        {
            this.logger = logger;
        }

        public string Test()
        {
            logger.LogInformation("Method " + nameof(Test) + " execution");

            return DateTime.Now.ToString();
        }
    }
}