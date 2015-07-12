function LatLng(latitude, longitude) {
    this.latitude = latitude;
    this.longitude = longitude;
}

function Location() {
    this.latLng = new LatLng(0, 0);
    this.locationName = "";
}

function RouteParams() {
    var optimizeRoute = true;
    var locations = [];
    var proximityRadius;
}

// the following method builds parameters for the search
function buildParams() {
    var result = new RouteParams();
    result.OptimizeRoute = true;
    result.proximityRadius = DEFAULT_PROXIMITY_RADIUS;

    var locations = [];
    {
        var location1 = new Location();
        location1.LocationName = "Lviv";
        //location1.LatLng = new LatLng(49.83549134162667, 24.024996757507324);
        var location2 = new Location();
        location2.LocationName = "Kyiv";

        locations[0] = location1;
        locations[1] = location2;
    }
    result.Locations = locations;

    return result;
}

function performSearch() {
    var params = buildParams();
    RouteHotel.RouteAPI.GetRouteHotels(params, parceRoute); // parceRoute defined in RouteDisplay.js
}