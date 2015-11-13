<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoutePlanner_gm_v3.aspx.cs" Inherits="RouteHotel.RoutePlanner_gm_v3" %>

<%@ Register Assembly="ResourcesToJavascript" Namespace="ResourcesToJavaScript1" TagPrefix="JSR" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Route planner</title>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <link href="RoutePlanner.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
       
         <JSR:GlobalResourcesToJavaScript ID="GlobalResourceToJavaScript1" runat="server" GlobalResXFileName="RoutePlanner_JS" JavaScriptObjectName="Resources"></JSR:GlobalResourcesToJavaScript>

        <asp:ScriptManager runat="server">
            <Services>
                <asp:ServiceReference Path="RouteAPI.asmx" />
            </Services>
        </asp:ScriptManager>
        <div id="routeSearchParams">
            <table width="100%">
                <tr>
                    <td><%= GetLocalResourceObject("routeSearchParams_from")%></td>
                    <td>
                        <input id="fromPlace" class="controls" type="text" placeholder='<%= GetLocalResourceObject("routeSearchParams_fromPlace_hint")%>' style="autocomplete-input" /></td>
                </tr>
                <tr>
                    <td><%= GetLocalResourceObject("routeSearchParams_to")%></td>
                    <td>
                        <input id="toPlace" class="controls" type="text" placeholder='<%= GetLocalResourceObject("routeSearchParams_toPlace_hint")%>' style="autocomplete-input" /></td>
                </tr>

            </table>

            <!-- todo add place autocomplete controls: https://developers.google.com/maps/documentation/javascript/examples/places-autocomplete-hotelsearch -->
            <br />
            <input type="button" onclick="performSearch();" value="<%= GetLocalResourceObject("routeSearchParams_btnSearch_text")%>" id="btnSearchRoute" />
        </div>
        <div id="map-canvas"></div>

        <div id="openModal" class="modalDialog">

            <div>
                <div class="internalHotelPopupContainer">
                    <div class="hotelPopupTitle">
                        <a href="#close" title="Close" class="close"
                             onclick="document.getElementById('hoteInfoFrameContainer').getElementsByTagName('iframe')[0].setAttribute('src', '') ;" 
                            >X</a>
                        <h2>Hotel info</h2>
                    </div>
                    <div id="hoteInfoFrameContainer" class="hotelPopupContentContainer">
                        <iframe class="hotelFrameContainer"></iframe>
                    </div>
                </div>
            </div>

        </div>


    </form>
</body>

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
    const FETCH_DATA_TIMEOUT = '<%= FetchDataTimeout %>';
    const GET_HOTELS_METHOD_NAME = '<%= GetHotelsMethodName %>';
        
</script>
    <script>
        window.location.hash = "top"; // prevent scroling to anchor when saved in URL or on pagre reload  
    </script>
<script src="/scripts/InitializeMap.js" type="text/javascript"></script>
<script src="/scripts/PlaceAutocomplete.js" type="text/javascript"></script>
<script src="/scripts/SearchBuilder.js" type="text/javascript"></script>
<script src="/scripts/RouteDisplay.js" type="text/javascript"></script>
<script src="/scripts/CalculationPointsDisplay.js" type="text/javascript"></script>
<script src="/scripts/HotelGoogleMapMarker.js" type="text/javascript"></script>
<script src="/scripts/HotelDisplay.js" type="text/javascript"></script>

</html>
