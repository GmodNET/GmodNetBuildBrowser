using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GmodNET.BuildBrowser
{
    public static class HelperMethods
    {
        public static NameValueCollection QueryString(this NavigationManager navigationManager)
        {
            return HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);
        }

        public static string GetWebTime(this DateTime dateTime)
        {
            var time_since = DateTime.UtcNow - dateTime;

            string ret = dateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            if(time_since.Days <= 1)
            {
                if(time_since.Days == 1)
                {
                    ret += " (Yesterday)";
                }
                else
                {
                    if(time_since.Hours > 1)
                    {
                        ret += " (" + time_since.Hours + " hours ago)";
                    }
                    else if (time_since.Hours == 1)
                    {
                        ret += " (an hour ago)";
                    }
                    else
                    {
                        if(time_since.Minutes > 1)
                        {
                            ret += " (" + time_since.Minutes + " minutes ago)";
                        }
                        else
                        {
                            ret += " (just now)";
                        }
                    }
                }
            }

            return ret;
        }
    }
}
