<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoutePlanner.aspx.cs" Inherits="RouteHotel.RoutePlanner" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Route planner</title>
    <script type="text/javascript" src="http://www.google.com/jsapi?key=ABQIAAAAXw0Gn2f0e-KVCjvRQu5UwxTgIssoOZSoEMkKklFmzu8AXooxOBS7VXcSWa2ThfSRTjfnxpJJOfbr8g"></script>
    <script type="text/javascript">
    //<![CDATA[
    google.load("maps", "2");
    var map, questionMarkIcon;
    var lastClick = { lat: 0, lng: 0 };
    var prevClick = { lat: 0, lng: 0 };
    
    function initialize() 
    {
        map = new google.maps.Map2(document.getElementById("map"));
        map.setCenter(new google.maps.LatLng(49.8331374, 24.0115938), 15);
        map.addControl(new GLargeMapControl());
        map.enableScrollWheelZoom();
        
        questionMarkIcon = new GIcon();
        questionMarkIcon.image = "question-mark.png";
        questionMarkIcon.iconSize = new GSize(23, 36);
        questionMarkIcon.shadowSize = new GSize(48, 36);
        questionMarkIcon.iconAnchor = new GPoint(15, 36);
        questionMarkIcon.infoWindowAnchor = new GPoint(15, 0);
        questionMarkIcon.shadow = "question-mark-shadow.png";
        questionMarkIcon.infoShadowAnchor = new GPoint(20, 35);
        
        document.body.onunload = GUnload;
        
        GEvent.addListener(map, "click", function(overlay, latLng) {
            if (!overlay) //if overlay is not null, the user has clicked on an object on the map
            {
                prevClick.lat = lastClick.lat;
                prevClick.lng = lastClick.lng;

                lastClick.lat = latLng.lat();
                lastClick.lng = latLng.lng();

                var icon = new GIcon(questionMarkIcon);
                var markerOptions = {icon: icon};
                //Place marker on click location
                var marker = new GMarker(latLng, markerOptions);
                GEvent.addListener(marker, "click", function() {
                    var html = 
                        "Original click:<br/>" +
                        latLng.lat().toFixed(4) + " latitude<br/>" +
                        latLng.lng().toFixed(4) + " longitude";
                    marker.openInfoWindowHtml(html);
                });
                map.addOverlay(marker);
                RouteHotel.RouteAPI.GetTO(parceTO);
            }
        });
    }
    google.setOnLoadCallback(initialize);

    function parceTO(routeParams)
    {
        if (routeParams != null) {
            var latLng = lastClick
            var marker = new GMarker(latLng);
            GEvent.addListener(marker, "click", function () {
                var html = routeParams.OptimizeRoute + "<br/>";
                var locations = routeParams.Locations;
                for (i = 0; i < locations.length; ++i) {
                    var location = locations[i];
                    if (location.LocationName) html += location.LocationName + "<br/>";
                    if (location.LatLng) html += location.LatLng.Latitude + " : " + location.LatLng.Longitude + "<br/>";
                }
                marker.openInfoWindowHtml(html);

                RouteHotel.RouteAPI.GetRoute(routeParams, parceRoute);
            });
            //Place marker on reverse-geocode result location
            map.addOverlay(marker);
        }
    }

    function parceRoute(route)
    {
        if (null != route)
        {
            var latLng = lastClick
            var marker = new GMarker(latLng);
            GEvent.addListener(marker, "click", function () {
                var html = route.Summary + "<br/>";
                /*
                var locations = routeParams.Locations;
                for (i = 0; i < locations.length; ++i) {
                    var location = locations[i];
                    if (location.LocationName) html += location.LocationName + "<br/>";
                    if (location.LatLng) html += location.LatLng.Latitude + " : " + location.LatLng.Longitude + "<br/>";
                }
                */
                marker.openInfoWindowHtml(html);
            });
        }
    }

    function plotPoint(geocodeResult)
    {
        if (geocodeResult.Address != null && geocodeResult.Point != null)
        {
            var latLng = new GLatLng(parseFloat(geocodeResult.Point.Latitude), parseFloat(geocodeResult.Point.Longitude));
            var marker = new GMarker(latLng);
            GEvent.addListener(marker, "click", function() {
                var html = 
                    geocodeResult.Address.Street1 + "<br/>" +
                    geocodeResult.Address.City + ", " + geocodeResult.Address.StateOrProvince + " " + geocodeResult.Address.PostalCode + "<br/>" +
                    geocodeResult.Address.Country;
                marker.openInfoWindowHtml(html);
            });
            //Place marker on reverse-geocode result location
            map.addOverlay(marker);
            
            //Zoom the map to the correct level based on the distance between the two points
            var minLat, minLng, maxLat, maxLng;

            clickToCompare = prevClick;
            if (clickToCompare.lng > parseFloat(geocodeResult.Point.Longitude))
            {
                minLng = parseFloat(geocodeResult.Point.Longitude);
                maxLng = clickToCompare.lng;
            }
            else
            {
                minLng = prevClick.lng;
                maxLng = parseFloat(geocodeResult.Point.Longitude);
            }
            if (clickToCompare.lat > parseFloat(geocodeResult.Point.Latitude))
            {
                minLat = parseFloat(geocodeResult.Point.Latitude);
                maxLat = clickToCompare.lat;
            }
            else
            {
                minLat = clickToCompare.lat;
                maxLat = parseFloat(geocodeResult.Point.Latitude);
            }
            
            var pointSW = new GLatLng(minLat, minLng);
            var pointNE = new GLatLng(maxLat, maxLng);

            var markerSW = new GMarker(pointSW);
            var markerNE = new GMarker(pointNE);
           // map.addOverlay(markerSW);
          //  map.addOverlay(markerNE);
            
            var bounds = new GLatLngBounds(pointSW, pointNE);
            if (clickToCompare.lat) { // if not first client - and there is somethiong to compare with
                map.setZoom(map.getBoundsZoomLevel(bounds));

                //Convert data to float type
                minLat = parseFloat(minLat);
                maxLat = parseFloat(maxLat);
                minLng = parseFloat(minLng);
                maxLng = parseFloat(maxLng);

                //Zoom to the center of the two points
                var centerLatLng = new GLatLng((minLat + maxLat) / 2, (minLng + maxLng) / 2);
                map.panTo(centerLatLng);
            }
        }
    }

    function zoomMap()
    {
        var swLat = parseFloat(document.getElementById("swLatitude").value);
        var swLon = parseFloat(document.getElementById("swLongitude").value);
        var neLat = parseFloat(document.getElementById("neLatitude").value);
        var neLon = parseFloat(document.getElementById("neLongitude").value);
        
        //alert(swLat + ',' + swLon + ' - ' + neLat + ',' + neLon);

        var pointSW = new GLatLng(swLat, swLon);
        var pointNE = new GLatLng(neLat, neLon);

        //alert(pointSW + ' - ' + pointNE);

        var bounds = new GLatLngBounds(pointSW, pointNE);
        //alert(bounds);
        map.setZoom(map.getBoundsZoomLevel(bounds));

        //Convert data to float type
        minLat = swLat;
        maxLat = neLat;
        minLng = swLon;
        maxLng = neLon;

        //Zoom to the center of the two points
        var centerLatLng = new GLatLng((minLat + maxLat) / 2, (minLng + maxLng) / 2);
        //alert(centerLatLng);
        map.panTo(centerLatLng);

    }
    //]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Services>
                <asp:ServiceReference Path="RouteAPI.asmx" />
            </Services>
        </asp:ScriptManager>
        <div id="map" style="height: 600px; width: 800px">
        
        </div>    
    
    </form>
</body>
</html>
