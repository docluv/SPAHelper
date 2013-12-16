using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace SPAHelper
{
    public abstract class SPAModel
    {

        public string[] RouteParams { get; set; }

        public string MainRoute = "";

        public void SetRoute(string route)
        {

            if (route.Length > 0)
            {
                RouteParams = route.Split('/');

                if (RouteParams.Length > 0)
                {
                    MainRoute = RouteParams[0];
                }

            }

        }

        public string _title;
        public string _description;

        public string Title { get; set; }
        public string Description { get; set; }

    }
}