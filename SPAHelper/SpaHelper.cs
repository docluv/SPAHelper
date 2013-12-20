using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPAHelper
{
    public static class SpaHelper
    {
        public static string SPALink(this HtmlHelper helper, string route)
        {

            if (HasEscapeFragment(helper))
            {
                return String.Format("?_escaped_fragment_={0}", route);
            }
            else
            {
                return String.Format("#!{0}", route);

            }

        }

        public static DateTime LastUpdated(this HtmlHelper helper)
        {
            return LastUpdated();

        }

        public static DateTime LastUpdated()
        {

            //borrowed from A Mads Kirstensen post
            //http://madskristensen.net/post/Use-If-Modified-Since-header-in-ASPNET

            if (HasForceReload())
            {
                return DateTime.MinValue.ToUniversalTime();
            }

            string header = HttpContext.Current.Request.Headers["If-Modified-Since"];
            DateTime lastModified = DateTime.MinValue;
            DateTime lastLoad = lastModified.ToUniversalTime();

            if (!string.IsNullOrEmpty(header))
            {

                DateTime.TryParse(header, out lastModified);

                if (lastModified != null)
                {
                    lastLoad = lastModified.ToUniversalTime();
                }

            }

            return lastLoad;
        }

        public static string TickVersionLink(this HtmlHelper helper,
                                                string fileName)
        {

            var fileTime = File.GetLastWriteTime(
                                HttpContext.Current.Server.MapPath(fileName)).Ticks;

            return fileName + "?v=" + fileTime.ToString();
        }

        public static bool HasEscapeFragment(this HtmlHelper helper)
        {
            return HasEscapeFragment();
        }

        public static bool HasEscapeFragment()
        {

            NameValueCollection queryString = HttpContext.Current.Request.QueryString;

            foreach (string key in queryString.AllKeys.Where(key => key != null))
            {
                if (key == "_escaped_fragment_")
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HasForceReload()
        {

            NameValueCollection queryString = HttpContext.Current.Request.QueryString;

            foreach (string key in queryString.AllKeys.Where(key => key != null))
            {
                if (key == "_force_reload_")
                {
                    return true;
                }
            }


            return false;
        }


    }

}

