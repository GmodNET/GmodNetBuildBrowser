using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Octokit;

namespace GmodNetBuildBrowser.Pages
{
    public partial class GmodDotNetRuntimeBuild
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public GitHubClient gitHubClient { get; set; }

        Release release;
        Exception initException;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                release = await gitHubClient.Repository.Release.Get("GmodNET", "runtime-nightly", Id);
            }
            catch (Exception ex)
            {
                initException = ex;
                Console.WriteLine("Exception was thrown while requesting GitHub release");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
