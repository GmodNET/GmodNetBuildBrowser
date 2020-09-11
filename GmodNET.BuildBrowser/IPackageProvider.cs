using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace GmodNET.BuildBrowser
{
    public interface IPackageProvider
    {
        public bool WasDataLoaded { get; }

        public bool WasOperationSuccessful { get; }

        public Exception LoadException { get; }

        public Task LoadPackagesDataAsync();

        public List<Package> GetPackages();
    }
}
