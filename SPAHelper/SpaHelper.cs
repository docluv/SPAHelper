﻿using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPAHelper
{
    public static class SpaHelper
    {


        //adding this for those freaking outdated versions of IE
        //deal with obsolete Internet Explorer Versions
        //I am working a good way to make this test very extensible and not so hard coded.
        public static bool IsObsoleteBrowser(this HtmlHelper helper) {

            var Request = HttpContext.Current.Request;

            //return ((Request.Browser.Browser == "InternetExplorer" || Request.Browser.Browser == "IE")
            //        && Request.Browser.MajorVersion != 10
            //        && Request.Browser.MajorVersion != 11);

            var ret = IsOldIE();

            if (!ret && !(IsIE10() || IsIE11()))
            {
                ret = false;
            }

            return ret;
        }

        public static bool IsOldIE() {

            return HttpContext.Current.Request.Browser.Browser == "IE";
        
        }

        public static bool IsIE10() { 

            var Request = HttpContext.Current.Request;

            return (Request.Browser.Browser == "InternetExplorer"
                    && Request.Browser.MajorVersion != 10);
        
        }

        /* Right now this only matters on my local dev machine. My Windows 2012 server correctly identifies IE 11 as not Internet Explorer, but the version # is incorectly reports as 7. So this is mostly for my local development purposes*/
        public static bool IsIE11() {

            var Request = HttpContext.Current.Request;

            return (Request.Browser.Browser == "InternetExplorer"
                    && Request.Browser.MajorVersion != 11);

        }

        /* Coming soon */
        public static bool IsOldAndroidWebKit() {

            return false;
        
        }

        public static string SPALink(this HtmlHelper helper, string route)
        {

            if (IsObsoleteBrowser(helper))
            {
                return String.Format("?_escaped_fragment_={0}", route);
            }
            else
            {
                return String.Format("#!{0}", route);

            }

        }

        [Obsolete("You should adjust code to use Last Modified Header Instead, LastUpdated() Method")]
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

            if (HasForceReload())
            {
                return DateTime.MinValue.ToUniversalTime().Ticks;
            }

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

