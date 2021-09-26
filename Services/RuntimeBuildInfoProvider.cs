using System;
using GmodNetBuildBrowser.Records;
using Blazored.LocalStorage;
using Octokit;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace GmodNetBuildBrowser.Services
{
    public class RuntimeBuildInfoProvider
    {
        GitHubClient gitHubClient;
        ILocalStorageService localStorage;

        public RuntimeBuildInfoProvider(GitHubClient gitHubClient, ILocalStorageService localStorage)
        {
            this.gitHubClient = gitHubClient;
            this.localStorage = localStorage;
        }

        public async Task<RuntimeVersionRecord> GetBuildByIdAsync(int id)
        {
            if(await localStorage.ContainKeyAsync("runtime-build-" + id))
            {
                RuntimeVersionRecord runtimeVersion = await localStorage.GetItemAsync<RuntimeVersionRecord>("runtime-build-" + id);
                return runtimeVersion;
            }
            else
            {
                var release = await gitHubClient.Repository.Release.Get("GmodNET", "runtime-nightly", id);
                var tags = await gitHubClient.Repository.GetAllTags("GmodNET", "runtime-nightly");
                RepositoryTag tag = tags.First(t => t.Name == release.TagName);
                var releaseCommit = await gitHubClient.Repository.Commit.Get("GmodNET", "GmodDotNet", tag.Commit.Sha);

                List<RuntimeAsset> assets = new List<RuntimeAsset>();

                foreach(var asset in release.Assets)
                {
                    assets.Add(new RuntimeAsset
                    {
                        Name = asset.Name,
                        Url = asset.BrowserDownloadUrl
                    });
                }

                RuntimeVersionRecord runtimeVersion = new RuntimeVersionRecord
                {
                    Id = id,
                    Version = release.TagName,
                    UploadTime = release.CreatedAt,
                    CommitHash = releaseCommit.Sha,
                    CommitUrl = releaseCommit.HtmlUrl,
                    CommitAuthor = releaseCommit.Commit.Author.Name,
                    CommitMessage = releaseCommit.Commit.Message,
                    Assets = assets.ToArray()
                };

                await localStorage.SetItemAsync<RuntimeVersionRecord>("runtime-build-" + id, runtimeVersion);

                return runtimeVersion;
            }
        }
    }
}
