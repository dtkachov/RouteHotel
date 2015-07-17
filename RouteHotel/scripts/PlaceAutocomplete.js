

function RouteSearchControls() {
    this.autocompletes = [];
    
    this.setStartAutocompleteControl = function (autocompleteControl) {
        this.autocompletes[0] = autocompleteControl;
    };

    this.setFinishAutocompleteControl = function (autocompleteControl) {
        var index = this.autocompletes.length > 1 ? this.autocompletes.length - 1 : 1;
        this.autocompletes[index] = autocompleteControl;
    };

    this.isRouteCanBeSearch = function () {
        var isFirstControlEmpty = null == this.autocompletes[0].getPlace();
        if (isFirstControlEmpty) return false;

        var isLastControlEmpty = null == this.autocompletes[this.autocompletes.length - 1].getPlace();
        if (isLastControlEmpty) return false;

        return true; // since first and last control are not empty we would proceed with search
    };

    this.getLocations = function () {
        var locations = [];

        // routeSearchControls - defined in Search builder
        for (var i = 0; i < this.autocompletes.length; ++i) {
            var autoCompleteControl = this.autocompletes[i];
            var place = autoCompleteControl.getPlace();
            
            if (null != place) {
                var latLng = new LatLng(place.geometry.location.lat(), place.geometry.location.lng());

                var location = new Location(latLng);
                location.latLng = latLng;
                locations[locations.length] = location;
            }
        }

        return locations;
    }
}

var routeSearchControls = new RouteSearchControls();

function initializeAutocomplete() {
    var fromInputControl = document.getElementById('fromPlace');
    var toInputControl = document.getElementById('toPlace');

    var startAutocomleteControl = initializeAutocompleteControl(fromInputControl);
    var finishAutocomleteControl = initializeAutocompleteControl(toInputControl);

    routeSearchControls.setStartAutocompleteControl(startAutocomleteControl);
    routeSearchControls.setFinishAutocompleteControl(finishAutocomleteControl);

    defineRouteSearchEnability();
}

function getSearchButton() {
    return document.getElementById('btnSearch');
}


function disableSearch() {
    var searchBtn = getSearchButton();
    searchBtn.disabled = true;
}

function enableSearch() {
    var searchBtn = getSearchButton();
    searchBtn.disabled = false;
}

// sets search controls enable or disable depending on search parameters
function defineRouteSearchEnability() {
    if (routeSearchControls.isRouteCanBeSearch()) enableSearch();
    else disableSearch();
}

function initializeAutocompleteControl(input) {
    var autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo('bounds', map);

    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        defineRouteSearchEnability();
    });

    return autocomplete;
}
