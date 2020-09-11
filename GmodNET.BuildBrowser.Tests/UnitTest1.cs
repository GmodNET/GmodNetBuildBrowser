using System;
using Xunit;
using GmodNET.BuildBrowser;
using System.Net.Http;

namespace GmodNET.BuildBrowser.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            HttpClient client = new HttpClient();

            PackageProvider provider = new PackageProvider(client);

            provider.LoadPackagesDataAsync().Wait();
        }
    }
}
