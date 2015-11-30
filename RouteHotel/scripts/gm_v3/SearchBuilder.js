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

// Represents room parameter
function RoomParameter() {
    var adultsCount; // Count of adults in room
    var childrens = [];  /// Array represening children. If empry or null - no childrens. If not empty - each element represent a children's age
}

// User preference for hotel (price, stars, review, etc)
function HotelPreference() {
    var locale; // Locale for Response.
    var arrivalDate; // Date of arrival for hotel search    
    var departureDate; /// Date of departure for hotel searc
    var currencyCode; // Code of currency to present data in
    var rooms = [];  // Represents parameters rooms array
}

// the following method builds parameters for the search
function buildParams() {
    var result = new RouteParams();
    result.OptimizeRoute = true; // TODO - fetch it from UI
    result.proximityRadius = DEFAULT_PROXIMITY_RADIUS; // TODO - fetch it from UI

    var locations = buildLocationParams();
    result.Locations = locations;

    var hotelParameters = buildHotelParams();
    result.HotelParameters = hotelParameters;

    return result;
}

function buildLocationParams() {
    return routeSearchControls.getLocations();
}

function buildHotelParams() {
    var result = new HotelPreference();
    result.locale = "ua_UK"; // TODO - fetch it from UI

    {
        // TODO - fetch it from UI
        var today = new Date();
        var from = new Date(); 
        var to = new Date();
        from.setDate(today.getDate() + 5);
        to.setDate(today.getDate() + 6);

        result.arrivalDate = from;
        result.departureDate = to;
    }

    result.currencyCode = "EUR"; // TODO - fetch it from UI

    var rooms = buildRooms();
    result.rooms = rooms;

    return result;
}

function buildRooms() {
    var result = [];
    var room1 = new RoomParameter();
    room1.adultsCount = 2; // TODO - fetch it from UI
    // result.childrens = [3,7]; // TODO - fetch it from UI
    result[result.length] = room1;
    return result;
}

function performSearch() {
    if (!routeSearchControls.isRouteCanBeSearch()) return;

    var params = buildParams();
    RouteHotel.RouteAPI.GetRouteHotels(params, parceRoute); // parceRoute defined in RouteDisplay.js
}