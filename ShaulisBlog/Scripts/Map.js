function initMap() {
    google.maps.event.addDomListener(window, "load", initialize);
}
 
// Where all the fun happens 
function initialize() {
    $.getJSON("/Locations/GetLocations", null, function (data) {
        google.maps.visualRefresh = true;

        if (data.length > 0) {
            var main = new google.maps.LatLng(data[0].Latitude, data[0].Longitude);

            // These are options that set initial zoom level, where the map is centered globally to start, and the type of map to show 
            var mapOptions = {
                zoom: 18,
                center: main,
                mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
            };

            // This makes the div with id "map_canvas" a google map 
            var map = new google.maps.Map($("#map_canvas")[0], mapOptions);
        }
        else {
            // This makes the div with id "map_canvas" a google map 
            var map = new google.maps.Map($("#map_canvas")[0]);
        }

        // Using the JQuery "each" selector to iterate through the JSON list and drop marker pins 
        $.each(data, function (i, item) {
            var marker = new google.maps.Marker({
                'position': new google.maps.LatLng(item.Latitude, item.Longitude),
                'map': map,
                'title': item.Name
            });

            // Make the marker-pin blue! 
            marker.setIcon('http://maps.google.com/mapfiles/ms/icons/blue-dot.png')

            // put in some information about each json object - in this case, the opening hours. 
            var infowindow = new google.maps.InfoWindow({
                content: "<div class='infoDiv'><h2>" + item.Name + "</div></div>"
            });

            // finally hook up an "OnClick" listener to the map so it pops up out info-window when the marker-pin is clicked! 
            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
            });
        })
    });
} 