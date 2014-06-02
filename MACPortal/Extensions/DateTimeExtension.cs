using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellaMates.Extensions
{
    public static class DateTimeExtension
    {
        public static String GetTimestamp(this DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}