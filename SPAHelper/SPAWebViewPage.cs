using System;
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
            
            var fileTime = File.GetLastWriteTimeUtc(Server.MapPath(@"Views\Home\" + path));

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
