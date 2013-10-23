using System;
using System.Collections.Specialized;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SPAHelper
{
    public static class SpaHelper
    {

        public static string SPALink(this HtmlHelper helper, string route)
        {

            var type = HttpContext.Current.Request.Browser.MajorVersion;

            //adding this for those freaking outdated versions of IE
            if (helper.HasEscapeFragment() || type == 8)
            {
                return String.Format("?_escaped_fragment_={0}", route);
            }
            else
            {
                return String.Format("#!{0}", route);

            }

        }

        public static long SetUpdateCookie(this HtmlHelper helper,
                                    string dt, int debugTTL, int productionTTL)
        {

            DateTime dtNow = DateTime.Now;
            var cookieTTLMin = 0;
            var cookieTTLDay = 0;

            if (HttpContext.Current.IsDebuggingEnabled)
            {
                cookieTTLMin = debugTTL;
            }
            else
            {
                cookieTTLDay = productionTTL;
            }

            TimeSpan tsMinute = new TimeSpan(cookieTTLDay, 0, cookieTTLMin, 0);
            long lastLoad = DateTime.MinValue.ToUniversalTime().Ticks;
            var cookie = HttpContext.Current.Request.Cookies[dt];

            if (cookie == null)
            {

                cookie = new HttpCookie(dt);

                //Set the cookies value
                cookie.Value = DateTime.Now.ToUniversalTime().Ticks.ToString();

                cookie.Expires = dtNow + tsMinute;

                //Add the cookie
                HttpContext.Current.Response.Cookies.Add(cookie);

            }
            else
            {
                lastLoad = long.Parse(cookie.Value);
            }

            //Set the cookies value
            cookie.Value = DateTime.Now.ToUniversalTime().Ticks.ToString();

            //Set the cookie to expire in 1 minute

            cookie.Expires = dtNow + tsMinute;

            return lastLoad;
        }

        public static long LastUpdated(this HtmlHelper helper) {

            string header = HttpContext.Current.Request.Headers["If-Modified-Since"];
            DateTime lastModified = DateTime.MinValue;
            long lastLoad = lastModified.ToUniversalTime().Ticks;

            if (!string.IsNullOrEmpty(header))
            {

                DateTime.TryParse(header, out lastModified);

                if (lastModified != null)
                {
                    lastLoad = lastModified.ToUniversalTime().Ticks;
                }

            }

            return lastLoad;
        }

        public static string TickVersionLink(this HtmlHelper helper,
                                                string fileName)
        {

            var appcacheFileTime = File.GetLastWriteTime(
                                HttpContext.Current.Server.MapPath(fileName)).Ticks;

            return fileName + "?v=" + appcacheFileTime.ToString();
        }

        public static bool HasEscapeFragment(this HtmlHelper helper)
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


    }

}

