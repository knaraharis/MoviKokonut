app.directive("mkProductDetail",
    function vgProductDetail() {
        return {
            scope: {
                vgDataProvider: "="
            },
            templateUrl: appConstants.webHostURLBase + 'Scripts/app/Directives/mk-product-detail/mk-product-detail.html',
            link: function (scope, elem) {
                /*  [ Quickview ]
		- - - - - - - - - - - - - - - - - - - - */
                $(elem).parent().find('.quick-view').on('click', function (e) {
                    e.preventDefault();
                    var _this = $(this).parents('.product');
                    $(_this).find('.popup').addClass('popup-open');

                    if ($(this).parents('.product-slider').length > 0) {
                        $(this).parents('.product-slider').addClass('open-quickview');
                    }

                    setTimeout(function () {
                        $(_this).find('.p-thumb > ul').bxSlider({
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
                $('.popup-close').on('click', function (e) {
                    e.preventDefault();
                    $(this).parents('.popup').removeClass('popup-open');

                    var _this = $(this);
                    if ($(_this).parents('.product-slider').length > 0) {
                        setTimeout(function () {
                            $(_this).parents('.product-slider').removeClass('open-quickview');
                        }, 100);
                    }
                });
            }
        };
    }
);