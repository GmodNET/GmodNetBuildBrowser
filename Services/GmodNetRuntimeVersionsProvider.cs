using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Octokit;
using Semver;
using System.Linq;
using System.Collections.ObjectModel;

namespace GmodNetBuildBrowser.Services
{
    public class GmodNetRuntimeVersionsProvider
    {
        GitHubClient gitHubClient;
        ReadOnlyCollection<Release> releases;

        public event Action StateChanged;

        public ReadOnlyCollection<Release> Releases
        {
            get {  return releases; }
        }

        public GmodNetRuntimeVersionsProvider(GitHubClient gitHubClient)
        {
            this.gitHubClient = gitHubClient;
        }

        public async Task FetchReleasesAsync()
        {
            releases = null;
            StateChanged();

            List<Release> tmp_releases = (await gitHubClient.Repository.Release.GetAll("GmodNET", "runtime-nightly")).ToList();

            tmp_releases.Sort((x, y) =>
            {
                return -SemVersion.CompareSortOrder(SemVersion.Parse(x.TagName, SemVersionStyles.Any), 
                                                   SemVersion.Parse(y.TagName, SemVersionStyles.Any));
            });

            releases = tmp_releases.AsReadOnly();
            StateChanged();
        }
    }
}
