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
            if (!(await localStorage.ContainKeyAsync("AppVersion")) 
                || 
                (await localStorage.GetItemAsync<string>("AppVersion")) != BuildInfo.FullVersion)
                {
                    Console.WriteLine("New version of GmodNET Build Browser detected, initiating new local cache");
                    await localStorage.ClearAsync();
                    await localStorage.SetItemAsync("AppVersion", BuildInfo.FullVersion);
                }
        }
    }
}
