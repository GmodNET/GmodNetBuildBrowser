using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Octokit;
using Semver;

namespace GmodNetBuildBrowser.Components
{
    public partial class GmodNetRuntimeVersions
    {
        [Inject]
        public GitHubClient githubClient { get; set; }

        List<Release> releases;

        protected override async Task OnInitializedAsync()
        {
            await LoadReleases();
        }

        async Task LoadReleases()
        {
            releases = (await githubClient.Repository.Release.GetAll("GmodNET", "runtime-nightly")).ToList();

            releases.Sort((x, y) =>
            {
                return -SemVersion.Compare(SemVersion.Parse(x.TagName), SemVersion.Parse(y.TagName));
            });
        }

        async Task RefreshHandler()
        {
            releases = null;

            this.StateHasChanged();

            await LoadReleases();

            this.StateHasChanged();
        }
    }
}
