using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Ketan.Square2.Service.Authentication.Data
{
    static class DateTimeHelper
    {
        public static DateTime StripTick(this DateTime datetime)
        {
            var t = datetime.Ticks - ((datetime.Ticks / 10000L) * 10000L);
            return datetime.AddTicks(-t);
        }
        public static DateTime? StripTick(this DateTime? datetime)
        {
            return datetime?.StripTick();
        }
    }
}
