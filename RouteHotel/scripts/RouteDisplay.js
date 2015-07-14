﻿
function convertToLatLng(toObj) {
    if (null == toObj) return null;

    // consider whether check below is needed - it might be invoked hundreds/thousands ot time, hence might harm perfoprmance..
    if (!('Latitude') in toObj || !('Longitude') in toObj) return null;

    return new google.maps.LatLng(toObj.Latitude, toObj.Longitude);
}

function parceRoute(route) {
    if (null == route) return;

    processLegs(route.Legs); // iterate through legs

    RouteHotel.RouteAPI.GetCalculationPoints(route.RouteID, parceCalculationPoints); // parceRoute defined in RouteDisplay.js
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

        processStep(step, i);
    }
}

function processStep(step, index) {
    if (null == step) return;

    var startPosition = convertToLatLng(step.StartLocation);
    var startMarker = new google.maps.Marker({
        position: startPosition,
        icon: {
            path: google.maps.SymbolPath.CIRCLE,
            scale: STEP_MARKER_SCALE,
            strokeColor: START_STEP_COLOR,
        },
        draggable: true,
        opacity: STEP_MARKER_OPACITY,
        map: map
    });

    google.maps.event.addListener(startMarker, 'click', function () {
        var html =
             "Start point for step #" + index + "<br>" +
             startPosition.lat().toFixed(4) + " latitude<br/>" +
             startPosition.lng().toFixed(4) + " longitude";
        var infowindow = new google.maps.InfoWindow({
            content: html
        });
        infowindow.open(map, startMarker);
    });

    processPoints(step.Points);
}

function processPoints(points) {
    if (null == points) return;

    drawStepPolyline(points);
}



function drawStepPolyline(points)
{
    if (null == points) return;

    var coordinates = [points.length];

    for (var i = 0; i < points.length; ++i) {
        var point = points[i];

        var position = convertToLatLng(point);
        coordinates[i] = position;
    }

    var polyLine = new google.maps.Polyline({
        path: coordinates,
        strokeColor: POINT_COLOR,
        strokeOpacity: POINT_MARKER_OPACITY,
        strokeWeight: POINT_LINE_WEIGHT
    });

    polyLine.setMap(map);

}