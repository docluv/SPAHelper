﻿using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace SPAHelper
{

    public abstract class SPAWebViewPage<T> : WebViewPage<T>

    {

        public IHtmlString RenderPage(
                                long lastRead,
                                string path,
                                params Object[] data
                            )
        {

            var fileTime = File.GetLastWriteTimeUtc(path).Ticks;

            if (lastRead < fileTime)
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
