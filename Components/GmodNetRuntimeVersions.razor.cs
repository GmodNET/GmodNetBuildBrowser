using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Octokit;
using Semver;
using GmodNetBuildBrowser.Services;

namespace GmodNetBuildBrowser.Components
{
    public partial class GmodNetRuntimeVersions
    {
        [Inject]
        GmodNetRuntimeVersionsProvider versionsProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            versionsProvider.StateChanged += StateHasChanged;

            if(versionsProvider.Releases is null)
            {
                await versionsProvider.FetchReleasesAsync();
            }
        }
    }
}
