using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GmodNET.BuildBrowser
{
    public class Package
    {
        readonly List<PackageVersion> versions;
        readonly string name;

        public List<PackageVersion> Versions
        {
            get { return versions; }
        }

        public string Name
        {
            get { return name; }
        }

        public Package(string name)
        {
            this.name = name;
            versions = new List<PackageVersion>();
        }

        public void AddVersion(string version, string download_path, string upload_date)
        {
            if(!versions.Any(package => package.VersionString == version))
            {
                DateTime uploadDate = DateTime.ParseExact(upload_date, @"yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);

                versions.Add(new PackageVersion(this, version, download_path, uploadDate));
            }
        }
    }
}
