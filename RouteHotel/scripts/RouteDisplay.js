
function convertToLatLng(toObj) {
    if (null == toObj) return null;

    // consider whether check below is needed - it might be invoked hundreds/thousands ot time, hence might harm perfoprmance..
    if (!('Latitude') in toObj || !('Longitude') in toObj) return null;

    return new google.maps.LatLng(toObj.Latitude, toObj.Longitude);
}

function parceRoute(route) {
    if (null != route) {
        processLegs(route.Legs); // iterate through legs
    }
}

// processes legs - display them on map
function processLegs(legs) {
    if (null == legs) return;

    for (var i = 0; i < legs.length; ++i) {
        var leg = legs[i];
        processLeg(leg);
    }
}

function processLeg(leg) {
    if (null == leg) return;

    var markerImg = {
        url: 'images/icons/iconmonstr-map-5-icon.svg',
        optimized: false,
    };

    var startPosition = convertToLatLng(leg.StartLocation);
    var markerStart = new google.maps.Marker({
        position: startPosition,
        icon: markerImg,
        draggable: false,
        map: map
    });

    var endPosition = convertToLatLng(leg.EndLocation);
    var markerFinish = new google.maps.Marker({
        position: endPosition,
        icon: markerImg,
        draggable: false,
        map: map
    });

    processSteps(leg.Steps);
}

function processSteps(steps) {
    if (null == steps) return;

    for (var i = 0; i < steps.length; ++i) {
        var step = steps[i];

        processStep(step);
    }
}

function processStep(step) {

}

/*
var marker = new google.maps.Marker({
    position: map.getCenter(),
    icon: {
        path: google.maps.SymbolPath.CIRCLE,
        scale: 10
    },
    draggable: true,
    map: map
});

*/