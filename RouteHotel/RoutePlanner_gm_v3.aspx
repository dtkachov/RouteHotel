<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoutePlanner_gm_v3.aspx.cs" Inherits="RouteHotel.RoutePlanner_gm_v3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Route planner</title>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no"/>
    <link href="RoutePlanner.css" rel="stylesheet" type="text/css" />
   
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&signed_in=true&libraries=places"></script>

    <script type='text/javascript'>
        var map; // goolge map global varialble

        // below goes configuration parameters
        const POINT_LINE_WEIGHT = <%= PointLineWeight %>;
        const POINT_MARKER_OPACITY = <%= PointMarkerOpacity %>;
        const POINT_COLOR = '<%= PointColor %>';
        const STEP_MARKER_SCALE = <%= StepMarkerScale %>;
        const STEP_MARKER_OPACITY = <%= StepMarkerOpacity %>;
        const START_STEP_COLOR = '<%= StartMarkerColor %>';

        // Defaults:
        const DEFAULT_PROXIMITY_RADIUS = '<%= ProximityRadius %>';
        
    </script>
    <script src="/scripts/InitializeMap.js" type="text/javascript"></script>
    <script src="/scripts/PlaceAutocomplete.js" type="text/javascript"></script>
    <script src="/scripts/SearchBuilder.js" type="text/javascript"></script>
    <script src="/scripts/RouteDisplay.js" type="text/javascript"></script>
    <script src="/scripts/CalculationPointsDisplay.js" type="text/javascript"></script>
    
</head>
<body>
    <form id="form1" runat="server">    
        <asp:ScriptManager runat="server">
            <Services>
                <asp:ServiceReference Path="RouteAPI.asmx" />
            </Services>
        </asp:ScriptManager>
        <div id ="routeSearchParams">
            <table>
                   <tr>
                       <td>From</td>
                        <td> <input id="fromPlace" class="controls" type="text" placeholder="Please enter a start location" style="autocomplete-input"/></td>
                    </tr>
                <tr>
                    <td>To</td>
                    <td><input id="toPlace" class="controls" type="text" placeholder="Please enter a finish location" style="autocomplete-input"/></td>
                    </tr>

            </table>
            
            <!-- todo add place autocomplete controls: https://developers.google.com/maps/documentation/javascript/examples/places-autocomplete-hotelsearch -->
            <br /><input type="button" onclick="performSearch();" value="Search" id="btnSearchRoute"  />
        </div>
        <div id="map-canvas" ></div> 
    </form>
</body>
</html>
