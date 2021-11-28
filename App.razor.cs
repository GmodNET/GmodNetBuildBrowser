using System;
using System.Threading.Tasks;
using GmodNET.VersionTool.Info;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace GmodNetBuildBrowser
{
    public partial class App
    {
        [Inject]
        ILocalStorageService localStorage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine("GmodNET Nightly Builds Browser\n" +
                              $"Version {BuildInfo.FullVersion}");

            if (!(await localStorage.ContainKeyAsync("AppVersion")) || 
                (await localStorage.GetItemAsync<string>("AppVersion")) != BuildInfo.FullVersion)
            {
                await localStorage.ClearAsync();
                await localStorage.SetItemAsync("AppVersion", BuildInfo.FullVersion);
            }
        }
    }
}
