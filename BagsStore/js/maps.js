var 
mapObject,
marker,
markers = [],
markersData = {
	'Marker': [
	{
		name: "BAG TO STORE",
        location_latitude: 46.0562972,
        location_longitude: 14.5059909,
            location: "Slovenska cesta 56, 1000 Ljubljana",
		icon_size: 2,
		rate: "<i class=\"far fa-star\"></i><i class=\"far fa-star\"></i><i class=\"far fa-star\"></i><i class=\"far fa-star\"></i><i class=\"far fa-star\"></i>"
        }
    //,
	//{
	//	name: "Vegas Strip",
    //    location_latitude: 46.080083,
    //    location_longitude: 14.514682,
	//	location: "502 Little Lonsdale, Melbourne",
	//	icon_size: 2,
	//	rate: "<i class=\"far fa-star\"></i><i class=\"far fa-star\"></i><i class=\"far fa-star\"></i><i class=\"far fa-star\"></i><i class=\"far fa-star-half\"></i>"
    //}
    ]
};

mapObject = new google.maps.Map(document.getElementById('map'), {
	zoom: 14,
	center: new google.maps.LatLng(46.0569, 14.5058)
});

for (var key in markersData)
	markersData[key].forEach(function (item) {
		marker = new google.maps.Marker({
			position: new google.maps.LatLng(item.location_latitude, item.location_longitude),
			map: mapObject,
			icon: {
				path: 'M11 2c-3.9 0-7 3.1-7 7 0 5.3 7 13 7 13 0 0 7-7.7 7-13 0-3.9-3.1-7-7-7Zm0 9.5c-1.4 0-2.5-1.1-2.5-2.5 0-1.4 1.1-2.5 2.5-2.5 1.4 0 2.5 1.1 2.5 2.5 0 1.4-1.1 2.5-2.5 2.5Z',
				scale: item.icon_size,
				anchor: new google.maps.Point(11, 22),
				fillOpacity: 1,
                fillColor: '#63A13C',
				strokeOpacity: 0
			}
		});

		if ('undefined' === typeof markers[key])
			markers[key] = [];

		markers[key].push(marker);
		google.maps.event.addListener(marker, 'click', (function () {
			closeInfoBox();
			getInfoBox(item).open(mapObject, this);
		}));
	});

function closeInfoBox() {
	$('div.marker-info').remove();
}

function getInfoBox(item) {
	return new InfoBox({
		content:
		"<p class=\"title\">"+item.name+"</p>"+
		"<p class=\"location\">"+item.location+"</p>"+
		"<div class=\"star\">"+item.rate+"</div>",
		boxClass: 'marker-info',
		disableAutoPan: false,
		pixelOffset: new google.maps.Size(-150, -60),
		closeBoxURL: '',
		isHidden: false,
		maxWidth: 300,
		alignBottom: true,
		enableEventPropagation: true
	});
}