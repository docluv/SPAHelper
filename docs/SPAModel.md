# SPAModel Class

The SPAModel provides a base for an ASP.NET MVC single page application model. A single page application has just a single view response and must provide access to some base properties and querystring parsing. SPAModel is an abstract class and must be used as a base class for the application's custom model.

The model has properties for the application's Title and Description values. These values are normally used in the markup's HEAD TITLE and description META tags. Additional properties can be added to a derived class to manage other common header meta properties, like open graph and twitter cards.

## SetRoute

The SetRoute function parses a SPA route to isolate the primary root. This is assumed to be the key to trigger proper content or functionality page on the server. Typically this is only used to identify the appropriate view to render when a core site is needed. A core site is a more traditional web response and typically served to search engine spiders (in accordance to publish AJAX guidance) and obsolete browsers that cannot handle modern single page web applications. 

The SetRoute function sets the MainRoute property's value. This value can then be used to render the desired view. The following code example shows how to use the SetRoute function. This code is part of a controller's action method. It uses the SPAHelper's HasEscapeFragment function to determine is the request is for the core site (where the "_escape_fragment_" queryString parameter is present). If so it then parses the queryString for the first parameter, which should be the SPA route. In this case is this is not a core site request the SetRoute functionality is not needed.

    if (SpaHelper.HasEscapeFragment())
    {
        var i = 0;
    
        foreach (string key in queryString.AllKeys.Where(key => key != null))
        {
            if (i == 0)
            {
                model.SetRoute(queryString[key]);
            }
        }
    
    }
    
    return View(model);

In the view's Razor markup the model's MainRoute property can be used to determine the appropriate action. In the following example the route is used in a switch case statement to render the desired partial view in response to the core site request.

    switch (Model.MainRoute.ToLower())
    {
    
        case "":
            @RenderPage("home-view.cshtml")
            break;
    
        case "events":
        @RenderPage("events-view.cshtml")
            break;
    
        case "article":
        @RenderPage("article-view.cshtml")
            break;
    
        case "about":
        @RenderPage("about-view.cshtml")
            break;
    
        case "presentations":
        @RenderPage("presentations-view.cshtml")
            break;
    
        case "libraries":
        @RenderPage("libraries-view.cshtml")
            break;
    
        case "tags":
        @RenderPage("tags-view.cshtml")
            break;
    
        case "articles":
        @RenderPage("articles-view.cshtml")
            break;
    
        case "archive":
        @RenderPage("archive-view.cshtml")
            break;
    
        case "calendar":
        @RenderPage("calendar-view.cshtml")
            break;
    
        case "search":
        @RenderPage("search-view.cshtml")
            break;
    
        default:
        @RenderPage("notFound.cshtml")
            break;
    }


