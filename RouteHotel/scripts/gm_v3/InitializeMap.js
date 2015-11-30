

function initialize() {
    var mapOptions = {
        zoom: 7,
        panControl: true,
        zoomControl: true,
        scaleControl: true
    };

    var mapCanvas = document.getElementById('map-canvas');
    map = new google.maps.Map(mapCanvas, mapOptions);

    centerMap(map);

    initializeAutocomplete(); // in PlaceAutocomplete.js
}

function centerMap(map) {
    // Try HTML5 geolocation
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = new google.maps.LatLng(position.coords.latitude,
                                             position.coords.longitude);

            map.setCenter(pos);
        }, function () {
            handleNoGeolocation(true);
        });
    } 
}

function handleNoGeolocation(errorFlag) {
    // TODO: consider adding error handling 
}

google.maps.event.addDomListener(window, 'load', initialize);
