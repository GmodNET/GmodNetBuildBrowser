using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace GmodNET.BuildBrowser
{
    public class PackageProvider : IPackageProvider
    {
        readonly HttpClient httpClient;

        bool was_loaded;
        bool was_successful;
        Exception load_exception;

        List<Package> packages;

        readonly Regex package_matcher = new Regex(@"storage\/[^\/]+\.\d+\.\d+\.\d+(\-[a-zA-Z0-9\.\-]+)?\.([a-zA-Z0-9]+|tar\.gz)$", 
            RegexOptions.ECMAScript | RegexOptions.Compiled);

        public bool WasDataLoaded => was_loaded;

        public bool WasOperationSuccessful => was_successful;

        public Exception LoadException => load_exception;

        public PackageProvider(HttpClient _httpClient)
        {
            httpClient = _httpClient;

            was_loaded = false;
            was_successful = false;
            load_exception = null;

            packages = new List<Package>();
        }

        public List<Package> GetPackages()
        {
            return packages;
        }

        public async Task LoadPackagesDataAsync()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("https://gleb-krasilich.fra1.digitaloceanspaces.com/");

                if(!response.IsSuccessStatusCode)
                {
                    throw new Exception("Unable to fetch the package storage (http status code " + response.StatusCode.ToString() + ").");
                }

                if(response.Content.Headers.ContentType.MediaType != "application/xml")
                {
                    throw new Exception("The package storage server returned invalid response ('Content-Type' header is not set to 'application/xml').");
                }

                XmlDocument xmlFileList = new XmlDocument();
                xmlFileList.LoadXml(await response.Content.ReadAsStringAsync());

                XmlNodeList contents = xmlFileList.GetElementsByTagName("Contents");

                foreach(XmlNode n in contents)
                {
                    if(package_matcher.IsMatch(n["Key"].InnerText))
                    {
                        string file_name_no_path = n["Key"].InnerText.Split("/").Last();
                    }
                }

                was_loaded = true;
                was_successful = true;
            }
            catch(Exception e)
            {
                was_successful = false;
                was_loaded = true;

                load_exception = e;
            }
        }
    }
}
