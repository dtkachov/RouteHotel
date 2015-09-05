

// entry point from seatch functionality
function fetchHotels(routeID) {
    displayHotelSearchAreas(routeID);

    var hotelDisplay = new HotelDisplay(routeID);
    hotelDisplay.fetchDataWithTimeout();
}

function HotelDisplay(routeID) {
    this.routeID = routeID;
    this.hotelMarkers = [];
}

HotelDisplay.prototype.fetchDataWithTimeout = function () {
    var _this = this;
    setTimeout(function () { _this.queryNextDataBunch(); }, FETCH_DATA_TIMEOUT);
}

HotelDisplay.prototype.queryNextDataBunch = function () {
    var xhr = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject('Microsoft.XMLHTTP');

    var proxy = new RouteHotel.RouteAPI(); 
    var path = proxy._get_path();
    path += "/" + GET_HOTELS_METHOD_NAME;

    xhr.open("POST", path, true);
    xhr.setRequestHeader('Content-Type', 'application/json');

    var self = this;

    xhr.onreadystatechange = function ()
    {
        if (xhr.status == 200 && xhr.readyState == 4)
        {
            var hotelResponse = JSON.parse(xhr.responseText);
            self.parceHotels(hotelResponse.d); // why "d" - I do not know - maybe MS framework add some wrapper
        }
    }

    var params = this.buildParamStr();
    xhr.send(params);
}

HotelDisplay.prototype.buildParamStr = function () {
    var result = "{\"routeID\":\"" + this.routeID + "\"";
    var processedHotelsCount = this.hotelMarkers.length;
    result += ",\"alreadyFetchedHotelsCount\":" + processedHotelsCount + "}";
    return result;   
}

HotelDisplay.prototype.parceHotels = function (hotelResponse) {
    if (null == hotelResponse) return; 

    for (var i = 0; i < hotelResponse.Hotels.length; ++i) {
        this.displayHotel(hotelResponse.Hotels[i]);
    }

    this.displayProgress(hotelResponse);

    if (!hotelResponse.IsFinished) {
        this.fetchDataWithTimeout(); // fetch ne wdata
    }


}

HotelDisplay.prototype.displayHotel = function(hotel) {
    if (null == hotel) return;
    if (null == hotel.Latitude || null == hotel.Longitude) return;

    var location = new google.maps.LatLng(hotel.Latitude, hotel.Longitude);

    var newHotelMarker = new HotelMarker(
		location,
		map,
		{
		    hotel_obj: hotel
		}
	);

    this.hotelMarkers[this.hotelMarkers.length] = newHotelMarker;
}

HotelDisplay.prototype.displayProgress = function (hotelResponse) {
    // TBD - change to real progress display
    var time = new Date();
    var timeStr = time.toLocaleTimeString();
    var msg = timeStr + " Processed " + hotelResponse.ProcessedPointCount + " from " + hotelResponse.CalculationPointCount;
    console.log(msg);
    if (hotelResponse.IsFinished) console.log("!!!! finished");
}

function displayHotelSearchAreas(routeID) {
    RouteHotel.RouteAPI.GetHotelSearchPoints(routeID, parceHotelSearchPoints);
}

function parceHotelSearchPoints(hotelCalculationPoints) {
    if (null == hotelCalculationPoints) return;
    if (null == hotelCalculationPoints.Points) return;
    //sample taken here http://obeattie.github.io/gmaps-radius/
    
    radius = hotelCalculationPoints.CalculationRadius;

    for (var i = 0; i < hotelCalculationPoints.Points.length; ++i) {
        var pointSrv = hotelCalculationPoints.Points[i];
        var point = convertToLatLng(pointSrv);

        var circle = new google.maps.Circle({
            center: point,
            clickable: true,
            draggable: false,
            editable: false,
            fillColor: '#004de8',
            fillOpacity: 0.09,
            map: map,
            radius: radius,
            strokeColor: '#004de8',
            strokeOpacity: 0.42,
            strokeWeight: 1,
        });

    }
    
}
