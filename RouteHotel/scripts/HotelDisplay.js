

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

function __parceHotels(hotels) {
    __hotelDisplay.parceHotels(hotels);
}

HotelDisplay.prototype.parceHotels = function(hotels) {
    if (null == hotels) return;

    for (var i = 0; i < hotels.length; ++i) {
        var hotel = hotels[i];

        this.displayHotel(hotel);
    }

    this.fetchDataWithTimeout();
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