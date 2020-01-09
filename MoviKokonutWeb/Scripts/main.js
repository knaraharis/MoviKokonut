(function($) {
	"use strict";
	$(document).ready(function() {

		/*  [ Dropdown ]
		- - - - - - - - - - - - - - - - - - - - */
		$( '.dropdown' ).each(function() {
			var _this = $( this );
			$( this ).find('a').on( 'click', function(e) {
				e.preventDefault();
				$( _this ).toggleClass( 'open' );
				var value = $( this ).text();
				$( _this ).find( '> ul > li > a' ).text( value );
			});
		});
		$('html').on( 'click', function(e) {
            if( $( e.target ).closest('.dropdown.open').length == 0 ) {
                $( '.dropdown' ).removeClass('open');
            }
        });

		/*  [ Main Menu ]
		- - - - - - - - - - - - - - - - - - - - */
		$( '.sub-menu' ).each(function() {
			$( this ).parent().addClass( 'has-child' ).find( '> a' ).append( '<span class="arrow"></span>' );
		});
		$( '.main-menu .arrow' ).on( 'click', function(e) {
			e.preventDefault();
			$( this ).parents( 'li' ).find( '> .sub-menu' ).slideToggle( 'fast' );
		});
		$( '.mobile-menu' ).on( 'click', function() {
			$( this ).parent().toggleClass('open');
		});

		/*  [ Newsletter ]
		- - - - - - - - - - - - - - - - - - - - */
		$( '.site-newsletter form input[type=email], .newsletter-popup form input[type=email]' ).each( function() {
			var label = $( this ).parent().find( $( 'label' ) );
			$( this ).keyup( function() {
				if( $( this ).val().length > 0 ) {
					$( label ).hide();
				}
			});

			$( this ).blur( function() {
				if( $( this ).val().length > 0 ) {
					$( label ).hide();
				}else {
					$( label ).show();
				}
			});

			$( this ).on( 'change', function() {
				$( label ).hide();
			});
		});

		/*  [ Search box ]
		- - - - - - - - - - - - - - - - - - - - */
		$( '.searchbox .icon' ).on( 'click', function() {
			$( this ).parent().addClass( 'open' );
		});

		/*  [ Shop cart ]
		- - - - - - - - - - - - - - - - - - - - */
		$( '.shop-cart' ).on( 'click', function() {
			$( 'body' ).toggleClass( 'shop-cart-open' );
			$( '.shop-item' ).toggleClass( 'active' );
		});

		/*  [ Style Switch ]
		- - - - - - - - - - - - - - - - - - - - */
		$( '.style-switch a' ).on( 'click', function(e) {
			e.preventDefault();
			var style = $( this ).attr( 'data-view' );
			$( this ).parent().find('a').not( this ).removeClass('active');
			$( this ).addClass('active');
			$( this ).parents( '.site-content' ).find( '.products' ).removeClass( 'list grid' ).addClass( style );

			$('.shop-content .site-main .products').masonry({
				itemSelector: '.product',
				percentPosition: true
			})			
		});

		/*  [ prettyPhoto ]
		- - - - - - - - - - - - - - - - - - - - */
		$("a[data-gal^='prettyPhoto']").prettyPhoto({
			hook: 'data-gal',
			animation_speed:'normal',
			theme:'light_square',
			slideshow:3000,
			social_tools: false
		});

		/*  [ Add minus and plus number quantity ]
		- - - - - - - - - - - - - - - - - - - - */
		if( $( '.quantity' ).length > 0 ) {
			var form_cart = $( 'form .quantity' );
			form_cart.prepend( '<span class="minus"><i class="fa fa-minus-square-o"></i></span>' );
			form_cart.append( '<span class="plus"><i class="fa fa-plus-square-o"></i></span>' );

			var minus = form_cart.find( $( '.minus' ) );
			var plus  = form_cart.find( $( '.plus' ) );

			minus.on( 'click', function(){
				var qty = $( this ).parent().find( '.qty' );
				if ( qty.val() <= 1 ) {
					qty.val( 1 );
				} else {
					qty.val( ( parseInt( qty.val(), 10 ) - 1 ) );
				}
			});
			plus.on( 'click', function(){
				var qty = $( this ).parent().find( '.qty' );
				qty.val( ( parseInt( qty.val(), 10 ) + 1 ) );
			});
		}

		/*  [ Bxslider ]
		- - - - - - - - - - - - - - - - - - - - */
		$('.main-bxslider .slider').bxSlider({
            mode: "fade",
            controls: false
        });
		
		$('.single-product .p-thumb > ul' ).bxSlider({
			mode: 'vertical',
			slideMargin: 10,
			minSlides: 4,
			maxSlides: 4,
			pager: false,
			controls: true,
			nextText: '<i class="fa fa-caret-down"></i>',
			prevText: '<i class="fa fa-caret-up"></i>'
		});

		

	    //$( '.blog-carousel' ).owlCarousel({
		//    items: 3,
	    //    itemsDesktop: [1199, 3],
	    //    itemsDesktopSmall: [989, 2],
	    //    itemsTablet: [767, 2],
	    //    itemsMobile: [540, 1],
	    //    pagination: false,
	    //    navigation: true,
	    //    navigationText: ['<i class="fa fa-chevron-left"></i>', '<i class="fa fa-chevron-right"></i>']
	    //});

	    $( '.home-sale-off .slider' ).owlCarousel({
		    singleItem: true,
	        pagination: true,
	        navigation: false
	    });


	    /*  [ Testimonial slider ]
		- - - - - - - - - - - - - - - - - - - - */
		$( '.testimonial-slider .inner' ).owlCarousel({
			navigation: false,
			pagination: true,
			items: 4,
			lazyLoad : true
		});

		/*  [ Quickview ]
		- - - - - - - - - - - - - - - - - - - - */
		$( '.quick-view' ).on( 'click', function(e) {
			e.preventDefault();
			var _this = $( this ).parents( '.product' );
			$( _this ).find( '.popup' ).addClass( 'popup-open' );

			if( $( this ).parents( '.product-slider' ).length > 0 ) {
				$( this ).parents( '.product-slider' ).addClass('open-quickview');
			}

			setTimeout( function() {
				$( _this ).find('.p-thumb > ul' ).bxSlider({
					mode: 'vertical',
					slideMargin: 10,
					minSlides: 4,
					maxSlides: 4,
					pager: false,
					controls: true,
					nextText: '<i class="fa fa-caret-down"></i>',
					prevText: '<i class="fa fa-caret-up"></i>'
				});
			}, 100);
		});

		/*  [ Popup ]
		- - - - - - - - - - - - - - - - - - - - */
		$( '.popup-close' ).on( 'click', function(e) {
			e.preventDefault();
			$( this ).parents( '.popup' ).removeClass( 'popup-open' );

			var _this = $( this );
			if( $( _this ).parents( '.product-slider' ).length > 0 ) {
				setTimeout( function() {
					$( _this ).parents( '.product-slider' ).removeClass('open-quickview');
				}, 100);
			}
		});


		/*  [ Tabs ]
		- - - - - - - - - - - - - - - - - - - - */
		$( '.tabs-container' ).each(function() {
			$( this ).find( '.tabs li:first-child a' ).addClass('active');
			$( this ).find( '.tab-content:first' ).show();

			$( this ).find('.tabs li a').on( 'click' , function(e) {
				e.preventDefault();
				var selector = $(this).attr('href');
				$(this).parent().parent().find('a').not(this).removeClass('active');
				$(this).addClass('active');
				$(this).parents('.tabs-container').find('.tab-content').not(selector).slideUp(300);
				$(this).parents('.tabs-container').find(selector).slideDown(300);
			});
		});

		/*  [ Toogle ]
		- - - - - - - - - - - - - - - - - - - - */
		$( '.toggle' ).each(function() {
			$( this ).find( '.toggle-controls li:first-child a' ).addClass('active');
			$( this ).find( '.toggle-content:first' ).show();

			$( this ).find('.toggle-controls li a').on( 'click', function(e) {
				e.preventDefault();
				var selector = $(this).attr('href');
				$(this).parent().parent().find('a').not(this).removeClass('active');
				$(this).addClass('active');
				$(this).parents('.toggle').find('.toggle-content').not(selector).slideUp(300);
				$(this).parents('.toggle').find(selector).slideDown(300);
			});
		});

		/*  [ jQuery Countdown ]
		- - - - - - - - - - - - - - - - - - - - */
		var endDate = 'November 15, 2016';
		$( '.countdown ul' ).countdown({
			date: endDate,
			render: function(data) {
				$(this.el).html(
					'<li><span>' + this.leadingZeros(data.days, 2) + '</span> Days</li>'
					+ '<li><span>' + this.leadingZeros(data.hours, 2) + '</span>Hours</li>'
					+ '<li><span>' + this.leadingZeros(data.min, 2) + '</span>Mins</li>'
					+ '<li><span>' + this.leadingZeros(data.sec, 2) + '</span>Secs</li>'
				);
			}
		});

		/*  [ Burger Menu ]
         - - - - - - - - - - - - - - - - - - - - */
        $( '.burger-control' ).on( 'click', function(e) {
            e.preventDefault();
            $( '#popup-burger' ).addClass( 'popup-open' );
        });

        /*  [ Mouse click scroll down ]
         - - - - - - - - - - - - - - - - - - - - */
        $( '.mouse-scroll' ).on( 'click', function(e) {
        	e.preventDefault();
        	$("html, body").animate({
	            scrollTop: $( window ).height()
	        	}, 700, function() {
	        	$('.mid-header').addClass('show');
	        });
        });

        /*  [ Sticky Menu ]
         - - - - - - - - - - - - - - - - - - - - */
        $('.mid-header').sticky({ topSpacing: 0 });

		/*  [ Back to top ]
		- - - - - - - - - - - - - - - - - - - - */
		$(window).scroll(function () {
	        if ($(this).scrollTop() > 50) {
	            $('.back-to-top').addClass('show');
	        } else {
	            $('.back-to-top').removeClass('show');
	        }
	    });
	    $('.back-to-top').on( 'click', function(e) {
	    	e.preventDefault();
	        $("html, body").animate({
	            scrollTop: 0
	        }, 500);
	    });

	    /*  [ Animate Elements ]
         - - - - - - - - - - - - - - - - - - - - */
	    var $animation_elements_in = $('.animation-element.fade-in');
        var $animation_elements_left = $('.animation-element.fade-left');
        var $animation_elements_right = $('.animation-element.fade-right');
        var $window = $(window);

        function check_if_in_view() {
            var window_height = $window.height();
            var window_top_position = $window.scrollTop();
            var window_bottom_position = (window_top_position + window_height);

            $.each($animation_elements_in, function() {
                var $element = $(this);
                var element_height = $element.outerHeight();
                var element_top_position = $element.offset().top;
                var element_bottom_position = (element_top_position + element_height);

                //check to see if this current container is within viewport
                if ((element_bottom_position >= window_top_position+100) &&
                    (element_top_position <= window_bottom_position)) {
                    $element.addClass('animated fadeInUp');
                }
            });

            $.each($animation_elements_left, function() {
                var $element = $(this);
                var element_height = $element.outerHeight();
                var element_top_position = $element.offset().top;
                var element_bottom_position = (element_top_position + element_height);

                //check to see if this current container is within viewport
                if ((element_bottom_position >= window_top_position+100) &&
                    (element_top_position <= window_bottom_position)) {
                    $element.addClass('animated fadeInLeft');
                }
            });

            $.each($animation_elements_right, function() {
                var $element = $(this);
                var element_height = $element.outerHeight();
                var element_top_position = $element.offset().top;
                var element_bottom_position = (element_top_position + element_height);

                //check to see if this current container is within viewport
                if ((element_bottom_position >= window_top_position+100) &&
                    (element_top_position <= window_bottom_position)) {
                    $element.addClass('animated fadeInRight');
                }
            });
        }

        $window.on('scroll resize', check_if_in_view);
        $window.trigger('scroll');
	    
	    $( window ).load(function() {
	    	/*  [ Testimonial Height ]
			- - - - - - - - - - - - - - - - - - - - */
			var maxHeight = 0;
			$('.testimonial-desc').each(function(){
				if ($(this).height() > maxHeight) { 
					maxHeight = $(this).height();
				}
			});
			$('.testimonial-desc').height(maxHeight);

			/*  [ Page loader]
			- - - - - - - - - - - - - - - - - - - - */
			$('body').addClass('loaded');
			$('#pageloader').fadeOut();
			//setTimeout(function () {
			//	$('#pageloader').fadeOut();
			//}, 10);
	    });
	});
})(jQuery);