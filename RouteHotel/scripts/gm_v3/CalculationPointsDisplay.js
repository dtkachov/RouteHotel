

function parceCalculationPoints(calculationRouteLegs) {
    if (null == calculationRouteLegs) return;

    for (var i = 0; i < calculationRouteLegs.length; ++i) {
        var leg = calculationRouteLegs[i];

        displayLeg(leg);
    }
}

function displayLeg(leg) {
    if (null == leg) return;
    if (null == leg.Points) return;

    for (var i = 0; i < leg.Points.length; ++i)
    {
        displayCalculationPoint(leg.Points[i]);
    }
}

// stroke works for lines while fil works for other shapes inside svg
function crossSymbol(color) {
  return {
    path: "M-3,-3 L3,3 M-3,3 L3,-3",
    fillColor: color, 
    strokeColor: color, 
    fillOpacity: 0.5,
    scale: 1
  };
}

function displayCalculationPoint(point) {
    if (null == point) return;

    var position = convertToLatLng(point.Point);

    var color = point.IsIntroduced ? "green" : "yellow";

    var marker = new google.maps.Marker({
        position: position,
        icon: crossSymbol(color),
        draggable: false,
        map: map
        
    });

    google.maps.event.addListener(marker, 'click', function () {
        var html =
             "Calculation point at position " +
             position.lat().toFixed(4) + " : " +
             position.lng().toFixed(4) + " <br/>" +
             (point.IsIntroduced ? "introduced" : "original") + "<br/>" +
            "distance: " + point.Distance.toFixed(4) + "OriginalDistance: " + point.OriginalDistance.toFixed(4) + "<br/>";
        var infowindow = new google.maps.InfoWindow({
            content: html
        });
        infowindow.open(map, marker);

        //marker.style.fill = "white";
    });

    // display proximity range
    var radius = parseInt(DEFAULT_PROXIMITY_RADIUS); // TBD - read from configuraiton

    var circle = new google.maps.Circle({
        center: position,
        clickable: true,
        draggable: false,
        editable: false,
        fillColor: '#ffa500',
        fillOpacity: 0.22,
        map: map,
        radius: radius,
        strokeColor: 'yellow',
        strokeOpacity: 0.32,
        strokeWeight: 1,
    });
}
