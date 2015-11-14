function LatLng(latitude, longitude) {
    this.latitude = latitude;
    this.longitude = longitude;
}

function RouteStep() {
    this.points = []; // array of LatLng
}

function RouteLeg() {
    this.steps = []; // array of RouteStep
}

function Route() {
    this.legs = []; // array of RouteLeg
}

// represent result of direction (route search). Might contain several routes 
function DirectionResult() {
    this.routes = []; // array of routes
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