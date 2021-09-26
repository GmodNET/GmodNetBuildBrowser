using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Octokit;
using GmodNetBuildBrowser.Services;
using GmodNetBuildBrowser.Records;

namespace GmodNetBuildBrowser.Pages
{
    public partial class GmodDotNetRuntimeBuild
    {
        Exception initException;
        RuntimeVersionRecord versionRecord;

        [Parameter]
        public int Id { get; set; }

        [Inject]
        RuntimeBuildInfoProvider buildInfoProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                versionRecord = await buildInfoProvider.GetBuildByIdAsync(Id);
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
