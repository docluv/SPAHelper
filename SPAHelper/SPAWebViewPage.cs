﻿using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace SPAHelper
{

    public abstract class SPAWebViewPage<T> : WebViewPage<T>
    {

        public IHtmlString SPARenderPage(
                                string path,
                                params Object[] data
                            )
        {
            DateTime lastRead = SpaHelper.LastUpdated();
            
            //change this so it does not assume the path of views\home
            var fileTime = File.GetLastWriteTimeUtc(Server.MapPath(@"Views\Home\" + path));

            if (lastRead < fileTime || SpaHelper.HasForceReload())
            {
                return base.RenderPage(path, data);
            }

            return new SPAHelperResult();

        }

        public new ViewDataDictionary<T> ViewData
        {
            get;
            private set;
        }
    }

}
