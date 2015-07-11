

function initialize() {
    var mapOptions = {
        zoom: 6
    };

    var mapCanvas = document.getElementById('map-canvas');
    var map = new google.maps.Map(mapCanvas, mapOptions);

    centerMap(map)
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