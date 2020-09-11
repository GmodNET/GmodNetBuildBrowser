using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GmodNET.BuildBrowser
{
    public class PackageVersion
    {
        Package package;
        string version;
        DateTime creation_time;
        string downloadUrl;

        public Package Package
        {
            get { return package; }
        }

        public string Version
        {
            get { return version; }
        }

        public DateTime CreationTime
        {
            get { return creation_time; }
        }

        public string DownloadUrl
        {
            get { return downloadUrl; }
        }

        public PackageVersion(Package _package, string _version, DateTime _creationTime, string _downloadUrl)
        {
            package = _package;
            version = _version;
            creation_time = _creationTime;
            downloadUrl = _downloadUrl;
        }
    }
}
