using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GmodNET.BuildBrowser
{
    public record PackageVersion(Package PackageID, string VersionString, string DownloadPath, DateTime UploadTime);
}
