using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MadDroid.DependencyInjection.Logging;
using Microsoft.Extensions.Logging;
using System.IO;

namespace MadDroid.Tests.DependencyInjection
{
    public class DependencyInjectionTests : IClassFixture<InjectionFixture>
    {
        readonly InjectionFixture fixture;

        public DependencyInjectionTests(InjectionFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void LogFileExists()
        {
            var logger = fixture.Provider.GetService<ILogger<DependencyInjectionTests>>();
            logger.LogInformation("this is a test");
            Assert.True(File.Exists(fixture.LogFile));
        }
    }
}
