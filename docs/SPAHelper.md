# SPAHelper class

The SPAHelper class is a collection of static and static ASP.NET MVC HTML Helper extension functions. 

## SPALink

An ASP.NET MVC HTML Helper method that properly formats a URL for either a single page application hash fragment route or as a QueryString parameter for core site requests. The following is an example of using the SPALink function to create the URLs for an application's navigation.

    <nav class="main-nav" role="navigation">
        <a href="@Html.SPALink("tags")" role="menuitem" class="tags-nav"><span class="nav-icon tags-icon"></span><span class="nav-text">Tags</span></a>
        <a href="@Html.SPALink("calendar")" role="menuitem" class="calendar-nav"><span class="nav-icon calendar-icon"></span><span class="nav-text">Posts by Date</span></a>
        <a href="@Html.SPALink("events")" role="menuitem" class="events-nav"><span class="nav-icon events-icon"></span><span class="nav-text">Events</span></a>
        <a href="@Html.SPALink("presentations")" role="menuitem" class="presentations-nav"><span class="nav-icon presentations-icon"></span><span class="nav-text">Presentations</span></a>
        <a href="@Html.SPALink("libraries")" role="menuitem" class="libraries-nav"><span class="nav-icon libraries-icon"></span><span class="nav-text">Libraries</span></a>
        <a href="@Html.SPALink("about")" role="menuitem" class="about-nav"><span class="nav-icon about-icon"></span><span class="nav-text">About Me</span></a>
        <a href="@Html.SPALink("search")" role="menuitem" class="search-nav"><span class="nav-icon search-icon"></span><span class="nav-text">Search</span></a>
    </nav>

The resulting URL will look like "#!tags" for a SPA or "?_escaped_fragment_=tags" for a core site request.

## LastUpdated

Is both an HTML Helper and static function. Returns a DateTime value based on the request's "If-Modified-Since" header. This is used to decide if content should be rendered in the request or is currently cached in the browser.

## HasEscapeFragment

Is both an HTML Helper and static function. Returns true is the QueryString has the "_escaped_fragment_" parameter. If not it returns false. This is used to determine if the request is for a SPA or the core site.

## HasForceReload

This is a utility function that returns true is the QueryString has the "_force_reload_" parameter. This is a 'backdoor' to override the "If-Modified-Since" header value and force all views to be included in the response.

## TickVersionLink (deprecated)


