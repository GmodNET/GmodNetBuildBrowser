using System;

namespace GmodNetBuildBrowser.Records
{
    public record RuntimeVersionRecord
    {
        public int Id { get; init; }

        public string Version { get; init; }

        public DateTimeOffset UploadTime { get; init; }

        public string CommitHash { get; init; }

        public string CommitUrl { get; init; }

        public string CommitAuthor { get; init; }

        public string CommitMessage { get; init; }

        public RuntimeAsset[] Assets { get; init; }
    }

    public record RuntimeAsset
    {
        public string Name { get; init; }

        public string Url { get; init; }
    }
}
