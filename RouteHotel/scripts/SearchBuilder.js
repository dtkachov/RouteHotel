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

    var locations = buildLocationParams();
    result.Locations = locations;

    return result;
}

function buildLocationParams() {
    return routeSearchControls.getLocations();
}

function performSearch() {
    if (!routeSearchControls.isRouteCanBeSearch()) return;

    var params = buildParams();
    RouteHotel.RouteAPI.GetRouteHotels(params, parceRoute); // parceRoute defined in RouteDisplay.js
}