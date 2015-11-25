<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoutePlanner_draggable.aspx.cs" Inherits="RouteHotel.RoutePlanner_draggable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test draggable directions</title>

        <style>

      html, body, form {
        height: 100%;
        margin: 0;
        padding: 0;
      }
      #map-canvas {
        height: 100%;
        float: left;
        width: 63%;
        height: 100%;
      }
      #right-panel {
        float: right;
        width: 34%;
        height: 100%;
      }
#right-panel {
  font-family: 'Roboto','sans-serif';
  line-height: 30px;
  padding-left: 10px;
}

#right-panel select, #right-panel input {
  font-size: 15px;
}

#right-panel select {
  width: 100%;
}

#right-panel i {
  font-size: 12px;
}

      .panel {
        height: 100%;
        overflow: auto;
      }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager runat="server">
            <Services>
                <asp:ServiceReference Path="RouteAPI.asmx" />
            </Services>
        </asp:ScriptManager>
        
            <div id="map-canvas"></div>
            <div id="right-panel">
                <p>Total Distance: <span id="total"></span></p>
            </div>
        
    </form>
</body>

    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&signed_in=true&libraries=places"></script>

    <script src="/scripts/RouteTypes.js" type="text/javascript"></script>

    <script type='text/javascript'>
        function initMap() {
            var map = new google.maps.Map(document.getElementById('map-canvas'), {
                zoom: 4,
                center: { lat: -24.345, lng: 134.46 }  // Australia.
            });

            var directionsService = new google.maps.DirectionsService;
            var directionsDisplay = new google.maps.DirectionsRenderer({
                draggable: true,
                map: map,
                panel: document.getElementById('right-panel')
            });

            directionsDisplay.addListener('directions_changed', function () {
                computeTotalDistance(directionsDisplay.getDirections());
            });

            //displayRoute('Perth, WA', 'Sydney, NSW', directionsService, directionsDisplay);
            //displayRoute('Cocklebiddy, WA', 'Broken Hill, NSW', directionsService, directionsDisplay);
            displayRoute('Lviv', 'Dubrovnik', directionsService, directionsDisplay);
        }

        function displayRoute(origin, destination, service, display) {

            //      waypoints: [{ location: 'Cocklebiddy, WA' }, { location: 'Broken Hill, NSW' }],
            service.route({
                origin: origin,
                destination: destination,
                travelMode: google.maps.TravelMode.DRIVING,
                provideRouteAlternatives : true,
                avoidTolls: false
            }, function (response, status) {
                if (status === google.maps.DirectionsStatus.OK) {
                    display.setDirections(response);
                } else {
                    alert('Could not display directions due to: ' + status);
                }
            });
        }

        function computeTotalDistance(result) {
            var total = 0;
            var myroute = result.routes[0];
            for (var i = 0; i < myroute.legs.length; i++) {
                total += myroute.legs[i].distance.value;
            }
            total = total / 1000;
            document.getElementById('total').innerHTML = total + ' km';

            var resultTO = convertDirectionResultToTO(result);
            RouteHotel.RouteAPI.StartHotelSearch(resultTO, parceHotel1);
        }

        function convertDirectionResultToTO(result) {
            if (!result) return null;
            //result.routes[0].legs[0].steps[0].lat_lngs[0].lat()
            var directionResult = new DirectionResult();
            for (var i = 0; i < result.routes.length; i++) {
                directionResult.routes[i] = convertRouteToTO(result.routes[i]);
            }
            return directionResult;
        }

        function convertRouteToTO(route) {
            if (!route) return null;
            var routeTO = new Route();
            for (var i = 0; i < route.legs.length; i++) {
                routeTO.legs[i] = convertLegToTO(route.legs[i]);
            }
            return routeTO;
        }

        function convertLegToTO(leg) {
            if (!leg) return null;
            var legTO = new RouteLeg();
            for (var i = 0; i < leg.steps.length; i++) {
                legTO.steps[i] = convertStepToTO(leg.steps[i]);
            }
            return legTO;
        }

        function convertStepToTO(step) {
            if (!step) return null;
            var stepTO = new RouteStep();
            for (var i = 0; i < step.lat_lngs.length; i++) {
                stepTO.points[i] = convertPointToTO(step.lat_lngs[i]);
            }
            return stepTO;
        }

        function convertPointToTO(point) {
            if (!point) return null;
            return new LatLng(point.lat(), point.lng());
        }

        function parceHotel1(route) {
            if (null == route) return;

            processLegs(route.Legs); // iterate through legs
            zoomMap(route);

            RouteHotel.RouteAPI.GetCalculationPoints(route.RouteID, parceCalculationPoints); // parceRoute defined in RouteDisplay.js

            fetchHotels(route.RouteID);
        }

        google.maps.event.addDomListener(window, 'load', initMap);
    </script>
    
</html>
