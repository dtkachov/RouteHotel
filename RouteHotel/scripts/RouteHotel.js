function initialize() {
    var mapOptions = {
        zoom: 17,
        center: { lat: -33.8666, lng: 151.1958 }
    };
    var mapContainer = document.getElementById('map-canvas');
    var map = new google.maps.Map(mapContainer, mapOptions);
}



google.maps.event.addDomListener(window, 'load', initialize);