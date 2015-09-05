function HotelMarker(latlng, map, args) {
	this.latlng = latlng;	
	this.setMap(map);
	this.hotel = this.parseHotel(args);
	this.infoWindow = null;
}

HotelMarker.prototype = new google.maps.OverlayView();

HotelMarker.prototype.draw = function () {

    var self = this;

    if (!this.div) {
        this.initOverlay();
    }

    var point = this.getProjection().fromLatLngToDivPixel(this.latlng);

    if (point) {
        // consts below depends on hotel icon - use them to shift icon so that it points exactly to hotel location 
        const Y_SHIFT_TO_SHOW_ICON_ON_POINT = 15;
        const X_SHIFT_TO_SHOW_ICON_ON_POINT = 15;
        this.div.style.left = (point.x - X_SHIFT_TO_SHOW_ICON_ON_POINT) + 'px';
        this.div.style.top = (point.y - Y_SHIFT_TO_SHOW_ICON_ON_POINT) + 'px';
    }
};

HotelMarker.prototype.initOverlay = function () {
	
		this.div = document.createElement('div');
		
		this.div.className = 'marker';
		
		this.div.style.position = 'absolute';
		this.div.style.cursor = 'pointer';
		this.div.style.width = '30px';
		this.div.style.height = '25px';
		this.div.style.backgroundImage = "url('images/icons/hotel1.svg')";
		this.div.setAttribute("align", "center");
		
		var lowPriceStr = this.hotel.RateCurrencyCode + this.hotel.LowRate.toFixed(0);

		var b = document.createElement('b');
		b.innerHTML = lowPriceStr;
		b.setAttribute("align", "center");
		this.div.appendChild(b);

		var self = this;
		
		google.maps.event.addDomListener(this.div, "click", function(event) {
		    self.displayInfoWindow();
			//google.maps.event.trigger(this, "click");
		});
		
		var panes = this.getPanes();
		panes.overlayImage.appendChild(this.div);

};

HotelMarker.prototype.getPosition = function () {
	return this.latlng;	
};

HotelMarker.prototype.parseHotel = function (args) {
    if (typeof (args.hotel_obj) === 'undefined') return null;

    var hotel = args.hotel_obj;
    return hotel;
};

HotelMarker.prototype.displayInfoWindow = function () {
    if (null == this.infoWindow) {
        this.intiInfoWindow();
    }

    this.infoWindow.open(map);
};

HotelMarker.prototype.intiInfoWindow = function () {
    var contentString = '';
    if (null != this.hotel.ThumbNailUrl && this.hotel.ThumbNailUrl.trim().length > 0) {
        var src = encodeURI(this.hotel.ThumbNailUrl);
        contentString += '<img style="float: right;" src="' + src + '" onerror="this.style.display = &apos;none&apos; ;" alt=".   ." border="0" />';
    }

    contentString += '<b>' + this.hotel.Name + '</b><br>' +
        this.hotel.Address1 + ', ' + this.hotel.City;
    if (null != this.hotel.StateProvinceCode && this.hotel.StateProvinceCode.length > 0) contentString += ', ' +  this.hotel.StateProvinceCode;
    if (null != this.hotel.PostalCode && this.hotel.PostalCode.length > 0) contentString += ', ' +  this.hotel.PostalCode;
    if (null != this.hotel.CountryCode && this.hotel.CountryCode.length > 0) contentString += ', ' +  this.hotel.CountryCode;
    contentString += '<br>';

    if (this.hotel.HotelRating > 0) contentString += 'Hotel rating: <b>' + this.hotel.HotelRating.toFixed(1) + '</b><br>';

    var description = decodeHTMLEntities(this.hotel.ShortDescription); 
    contentString += description + '<br>';

    contentString += '<br>Rates from <b>' + this.hotel.LowRate.toFixed(0) + '</b> ' + this.hotel.RateCurrencyCode;
    if (this.hotel.LowRate < this.hotel.HighRate) contentString += ' to <b>' + this.hotel.HighRate.toFixed(0) + '</b> ' + this.hotel.RateCurrencyCode;

    this.infoWindow = new google.maps.InfoWindow({
        content: contentString,
        position: this.latlng,
    });
};

function decodeHTMLEntities(text) {
    var entities = [
        ['apos', '\''],
        ['amp', '&'],
        ['lt', '<'],
        ['gt', '>']
    ];

    for (var i = 0, max = entities.length; i < max; ++i)
        text = text.replace(new RegExp('&' + entities[i][0] + ';', 'g'), entities[i][1]);

    return text;
}

