using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Octokit;
using GmodNetBuildBrowser.Services;
using Blazored.LocalStorage;

namespace GmodNetBuildBrowser
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<GitHubClient>(sp => new GitHubClient(new ProductHeaderValue("gmodnet-nightly-builds-browser")));
            builder.Services.AddSingleton<GmodNetRuntimeVersionsProvider>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<RuntimeBuildInfoProvider>();

            await builder.Build().RunAsync();
        }
    }
}
