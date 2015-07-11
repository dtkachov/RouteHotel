<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoutePlanner_gm_v3.aspx.cs" Inherits="RouteHotel.RoutePlanner_gm_v3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Route planner</title>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no">
    <link href="RoutePlanner.css" rel="stylesheet" type="text/css" />
    
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&signed_in=true"></script>

    <script type='text/javascript'>
            var map; // goolge map global varialble
    </script>
    <script src="/scripts/InitializeMap.js" type="text/javascript"></script>
    <script src="/scripts/SearchBuilder.js" type="text/javascript"></script>
    <script src="/scripts/RouteDisplay.js" type="text/javascript"></script>
    
</head>
<body>
    <form id="form1" runat="server">    
        <asp:ScriptManager runat="server">
            <Services>
                <asp:ServiceReference Path="RouteAPI.asmx" />
            </Services>
        </asp:ScriptManager>
        <div id ="routeSearchParams">
            TODO: add search params here<br/><br/>
            <!-- todo add place autocomplete controls: https://developers.google.com/maps/documentation/javascript/examples/places-autocomplete-hotelsearch -->
            <input type="button" onclick="performSearch();" value="Search" />
        </div>
        <div id="map-canvas" ></div> 
    </form>
</body>
</html>
