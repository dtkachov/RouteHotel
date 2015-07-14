

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

function displayCalculationPoint(point) {
    if (null == point) return;

    var position = convertToLatLng(point.Point);

    var color = point.IsIntroduced ? "green" : "black";
    var markerImg = {
        url: 'images/icons/cross-512px.svg',
        optimized: false,
        stroke: color,
    };

    var marker = new google.maps.Marker({
        position: position,
        icon: markerImg,
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

}
