function HotelMarker(latlng, map, args) {
	this.latlng = latlng;	
	this.setMap(map);
	this.hotel = this.parseHotel(args);
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
		
		google.maps.event.addDomListener(this.div, "click", function(event) {
			alert('Hotel info to be show here!');			
			google.maps.event.trigger(this, "click");
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