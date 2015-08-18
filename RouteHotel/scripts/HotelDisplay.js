

// entry point from seatch functionality
function fetchHotels(routeID) {
    var hotelDisplay = new HotelDisplay(routeID);
    hotelDisplay.fetchDataWithTimeout();

    __hotelDisplay = hotelDisplay;// to be removed and made pure object method
}

var __hotelDisplay;

function HotelDisplay(routeID) {
    this.routeID = routeID;
    this.hotelMarkers = [];
}

HotelDisplay.prototype.fetchDataWithTimeout = function () {
    var _this = this;
    setTimeout(function () { _this.queryNextDataBunch(); }, FETCH_DATA_TIMEOUT);
}

HotelDisplay.prototype.queryNextDataBunch = function() {

    /*
    var xhr = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject('Microsoft.XMLHTTP');
    xhr.open("GET", "http://mysite.com/myscript.asp", true);
    xhr.onreadystatechange = function ()
    {
        if (xhr.status == 200 && xhr.readystate == 4)
        {
            if (JSON) // provided by json2.js or browsers with native JSON
                var result = JSON.parse(xhr.responseText);
            else
                var result = eval ('(' + xhr.responseText + ')');

            // Do something with the result here
        }
    }
    xhr.send();
    */
    var processedHotelsCount = this.hotelMarkers.length;
    RouteHotel.RouteAPI.GetHotels(this.routeID, processedHotelsCount, __parceHotels);
}

function __parceHotels(hotelResponse) {
    __hotelDisplay.parceHotels(hotelResponse);
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