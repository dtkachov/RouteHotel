

// entry point from seatch functionality
function fetchHotels(routeID) {
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

    var proxy = new RouteHotel.RouteAPI(); // TBD - init this on server
    var path = proxy._get_path();
    path += "/GetHotels"; // TBD - init this on server

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
        this.fetchDataWithTimeout();
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
    var msg = timeStr + " Processed " + hotelResponse.ProcessedPointCount + " from " + hotelResponse.PointCount;
    console.log(msg);
    if (hotelResponse.IsFinished) console.log("!!!! finished");
}