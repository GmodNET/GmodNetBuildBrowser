using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GmodNetBuildBrowser
{
    public static class TimeHelper
    {
        public static string GetUploadTimeString(DateTimeOffset uploadTimeOffset)
        {
            DateTime uploadTime = uploadTimeOffset.ToLocalTime().DateTime;

            DateTime currentTime = DateTime.Now;

            TimeSpan timeDelta = currentTime - uploadTime;

            string result = uploadTime.ToString("yyyy-MM-dd HH:mm:ss");

            if (timeDelta.TotalMinutes < 2)
            {
                result += " (just now)";
            }
            else if (timeDelta.TotalHours < 2)
            {
                result += $" ({(int)timeDelta.TotalMinutes} minutes ago)";
            }
            else if (timeDelta.TotalDays < 2)
            {
                result += $" ({(int)timeDelta.TotalHours} hours ago)";
            }

            return result;
        }
    }
}
