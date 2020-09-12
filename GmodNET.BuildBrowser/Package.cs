using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Semver;

namespace GmodNET.BuildBrowser
{
    public class Package
    {
        readonly SortedSet<PackageVersion> versions;
        readonly string name;

        public IEnumerable<PackageVersion> Versions
        {
            get { return versions; }
        }

        public string Name
        {
            get { return name; }
        }

        public DateTime LastUpdate
        {
            get
            {
                DateTime time = versions.FirstOrDefault().UploadTime;

                foreach(PackageVersion v in versions)
                {
                    if(v.UploadTime > time)
                    {
                        time = v.UploadTime;
                    }
                }

                return time;
            }
        }

        public Package(string name)
        {
            this.name = name;
            versions = new SortedSet<PackageVersion>(new VersionComparer());
        }

        public void AddVersion(string version, string download_path, string upload_date)
        {
            if(!versions.Any(package => package.VersionString == version))
            {
                DateTime uploadDate = DateTime.ParseExact(upload_date, @"yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);

                versions.Add(new PackageVersion(this, version, download_path, uploadDate));
            }
        }

        class VersionComparer : IComparer<PackageVersion>
        {
            public int Compare(PackageVersion x, PackageVersion y)
            {
                SemVersion x_ver = SemVersion.Parse(x.VersionString);
                SemVersion y_ver = SemVersion.Parse(y.VersionString);

                return -x_ver.CompareByPrecedence(y_ver);
            }
        }
    }
}
