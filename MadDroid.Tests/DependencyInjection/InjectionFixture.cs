using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using MadDroid.DependencyInjection.Logging;
using System.IO;
using Microsoft.Extensions.Logging;

namespace MadDroid.Tests.DependencyInjection
{
    public class InjectionFixture : IDisposable
    {
        public string LogFile { get; } = $"{DateTime.Now.ToString("dd-MM-yyyy")}.txt";

        public readonly IServiceProvider Provider;

        public InjectionFixture()
        {
            Provider = new ServiceCollection()
                .AddLogging(options =>
                {
                    options.AddConsole();
                    options.AddDebug();
                    options.AddFile(LogFile);
                })
                .BuildServiceProvider();
        }

        public void Dispose()
        {
            File.Delete(LogFile);
        }
    }
}
