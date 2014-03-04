# SPAWebViewPage

SPAWebViewPage is an abstract class that inherits from WebViewPage. The class adds the SPARenderPage method to an ASP.NET MVC view. The view assumes the view is in the Views\Home path. The SPARenderPage method is used to render individual single page application views from cshtml files.

A single page application functions differently than a classic web site where URL routing is handled on the server with at least a single dedicated view file mapped to a route. The view-route mapping is handled in the browser in a single page application. The SPARenderPage method manages if a view needs to be sent to the browser or the currently cached version is current. The function compares the last modified header to the view's file last updated time. If the file has been modified since the last request the view is included in the overal response.

View markup is cached in the browser using a mechanism like backpackjs (https://github.com/docluv/Backpack). Backpack stores views in localStorage and works in concert with SPAjs (https://github.com/docluv/spa) to manage views in a single page application. The SPAHelper provides extensions for ASP.NET MVC to manage the server-side responsibilities of a single page application.

You must register SPAWebViewPage with the ASP.NET MVC application in the web.config. The pageBaseType must be set to the SPAWebViewPage class. This is an example of how this is done:

    <system.web.webPages.razor>
      <host factoryType="System.Web.Mvc.MvcWebRazorHostFactory, System.Web.Mvc, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" />
      <pages pageBaseType="SPAHelper.SPAWebViewPage">
        <namespaces>
          <add namespace="System.Web.Mvc" />
          <add namespace="System.Web.Mvc.Ajax" />
          <add namespace="System.Web.Mvc.Html" />
          <add namespace="System.Web.Routing" />
        </namespaces>
      </pages>
    </system.web.webPages.razor>

Once the SPAWebViewPage is registered all views can use the SPARenderPage to render partial views. The following is an example:

    @SPARenderPage("views/home-view.cshtml")

