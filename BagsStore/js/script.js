$(function() {

    "use strict";
	
    var wind = $(window);

	/*		Navbar Dropdown		*/
	$('.navbar .navbar-nav .dropdown a').on('click', function(e) {
		if($(window).width() < 992){
			var submenu = $(this).parent();
			if(submenu.hasClass('active')){
				submenu.removeClass('active');
				submenu.find('ul').slideUp();
			}else{
				var not = $(e.target).hasClass('active') ? e.target : $(e.target).parents('.active');
				submenu.closest('.navbar .navbar-nav').find('.active').not(not).removeClass('active').find('ul').slideUp();
				submenu.addClass('active');
				submenu.find('ul:first').slideDown();
			}
		}
	});
	
	/*		Testimonials Carousel		*/
	var testimonials = $('.testimonials .owl-carousel');
	testimonials.owlCarousel({
		navText: ['', ''],
		margin: 50,
		nav: false,
		dots: false,
		responsive: {
			0: {
				items: 1
			},
			992: {
				items: 2
			}
		}
	});

	/*		Sponsors Carousel		*/
	var sponsors = $('.sponsors .owl-carousel');
	sponsors.owlCarousel({
		loop: true,
		navText: ['', ''],
		margin: 30,
		nav: false,
		dots: false,
		autoplay: 3000,
		responsive: {
			0:{
				items:1
			},
			448:{
				items:2
			},
			768:{
				items:3
			},
			992:{
				items:4
			},
			1200:{
				items:5
			},
			1500:{
				items:6
			}
		}
	});
});

$(window).on("load",function (){
	var wind = $(window);

	wind.scrollTop(0);

	/*		Preloader		*/
	$("#preloader").delay(500).fadeOut(400);
});

/*		Google Map		*/
function initMap(){
	var mapObject = new google.maps.Map(document.getElementById('map'), {
		zoom: 12,
        center: new google.maps.LatLng(46.0569, 14.5058)
	});

	var marker = new google.maps.Marker({
        position: new google.maps.LatLng(46.0569, 14.5058),
		map: mapObject,
		icon: {
			path: 'M11 2c-3.9 0-7 3.1-7 7 0 5.3 7 13 7 13 0 0 7-7.7 7-13 0-3.9-3.1-7-7-7Zm0 9.5c-1.4 0-2.5-1.1-2.5-2.5 0-1.4 1.1-2.5 2.5-2.5 1.4 0 2.5 1.1 2.5 2.5 0 1.4-1.1 2.5-2.5 2.5Z',
			scale: 2.5,
			anchor: new google.maps.Point(11, 22),
			fillOpacity: 1,
            fillColor: '#63A13C',
			strokeOpacity: 0
		}
	});
}

window.onscroll = function () { myFunction() };

var header = document.getElementById("navbar");
var sticky = header.offsetTop;
var img1 = document.getElementById("green");
var img2 = document.getElementById("white");
var overlay = document.getElementById("div-overlay");
var navLink = document.getElementById("nav-link1");
var navLink1 = document.getElementById("nav-link2");
var navLink2 = document.getElementById("nav-link3");
var navLink3 = document.getElementById("nav-link4");
var navLink4 = document.getElementById("nav-link5");

function myFunction() {

    if (window.pageYOffset > sticky) {
        header.classList.add("sticky");
        img1.classList.add("hide-img");
        img2.classList.add("show-img");
        overlay.classList.remove("overlay-div");
        navLink.classList.add('nav-link1');
        navLink1.classList.add('nav-link1');
        navLink2.classList.add('nav-link1');
        navLink3.classList.add('nav-link1');
        navLink4.classList.add('nav-link1');

    } else {

        header.classList.remove("sticky");
        img1.classList.remove("hide-img");
        img2.classList.remove("show-img");
        overlay.classList.add("overlay-div");
        navLink.classList.remove('nav-link1');
        navLink1.classList.remove('nav-link1');
        navLink2.classList.remove('nav-link1');
        navLink3.classList.remove('nav-link1');
        navLink4.classList.remove('nav-link1');
    }

};


