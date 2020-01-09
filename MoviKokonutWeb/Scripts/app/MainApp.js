Array.prototype.chunk = function (pieces) {
    pieces = pieces || 2;
    var len = this.length;
    var mid = (len / pieces);
    var chunks = [];
    var start = 0;
    for (var i = 0; i < pieces; i++) {
        var last = start + mid;
        if (!len % pieces >= i) {
            last = last - 1
        }
        chunks.push(this.slice(start, last + 1) || []);
        start = last + 1;
    }
    return chunks;
}

var appConstants = {
    youtubeVideoURLBase: "https://www.youtube.com/watch?v={0}&rel=0&showinfo=0",
    youtubeThumbnailURLBase: "http://img.youtube.com/vi/{0}/{1}.jpg",
    apiHostURLBase: "http://prinhyltpdl0529/MoviKokonutWebAPI/",
    webHostURLBase: "http://prinhyltpdl0529/MoviKokonutWeb/"
}

var appUtils = {}

appUtils.getVideoURL = function (videoID, videoSrc) {
    var url = ""
    switch (videoSrc) {
        case "Youtube":
            url = this.stringFormat(appConstants.youtubeVideoURLBase, videoID);
        case "Vimeo":
            url = "";
        default:
            url = this.stringFormat(appConstants.youtubeVideoURLBase, videoID);
    }
    return url;
}

appUtils.getImageURL = function (videoID, videoSrc, imageID) {
    var url = ""
    switch (videoSrc) {
        case "Youtube":
            url = this.stringFormat(appConstants.youtubeThumbnailURLBase, videoID, imageID != null ? imageID : 0);
        case "Vimeo":
            url = "";
        default:
            url = this.stringFormat(appConstants.youtubeThumbnailURLBase, videoID, imageID != null ? imageID : 0);
    }
    return url;
}

appUtils.stringFormat = function (string) {
    var res = string
    if (string != null) {
        for (var idx = 1; idx < arguments.length; idx++) {
            res = res.replace("{" + (idx - 1) + "}", arguments[idx]);
        }
    }
    return res;
}

appUtils.reloadDOM = function () {
    $('.home-slider .slider').owlCarousel({
        singleItem: true,
        pagination: true,
        navigation: true,
        navigationText: ['<i class="fa fa-chevron-left"></i>', '<i class="fa fa-chevron-right"></i>'],
        afterAction: function () {
            this.$owlItems.removeClass('active');
            this.$owlItems.eq(this.currentItem).addClass('active');
        }
    });

    $('.customer-slider .inner').owlCarousel({
        navigation: true,
        pagination: false,
        items: 7,
        itemsDesktop: [1199, 5],
        itemsDesktopSmall: [989, 4],
        itemsTablet: [767, 3],
        itemsMobile: [480, 2],
        navigationText: ['<i class="fa fa-chevron-left"></i>', '<i class="fa fa-chevron-right"></i>']
    });
}

var app = angular.module('movCoconutApp',
        [
            "ngRoute",
            "ngSanitize",
            "com.2fdevs.videogular",
            "com.2fdevs.videogular.plugins.controls",
            "com.2fdevs.videogular.plugins.overlayplay",
            "com.2fdevs.videogular.plugins.poster",
            "com.2fdevs.videogular.plugins.analytics",
            "info.vietnamcode.nampnq.videogular.plugins.youtube"
        ]);

app.config(
    ["$routeProvider", function config($routeProvider) {
        $routeProvider
            .when('/videos/:videoId', {
                templateUrl: appConstants.webHostURLBase + 'Scripts/app/Views/VideoView.html',
                controller: 'vgVideoController',
                controllerAs: "ctrl",
                resolve: {
                    "videoData": ["$route", "videoFactory", function ($route, videoFactory) {
                        var q = videoFactory.getVideo($route.current.params.videoId)
                        q.success(function (data, status, headers, conf) {
                            return data;
                        }).error(function () {
                            console.log('Error')
                        });
                        return q;
                    }]
                }
            }).
            when('/home', {
                templateUrl: appConstants.webHostURLBase + 'Scripts/app/Views/HomeView.html',
                controller: 'popularVideoCotroller',
                controllerAs: 'ctrl',
                resolve: {
                    "popularVideoData": ["$route", "videoFactory", function ($route, videoFactory) {
                        var q = videoFactory.getTopVideos()
                        q.success(function (data, status, headers, conf) {
                            return data;
                        }).error(function () {
                            console.log('Error')
                        });
                        return q;
                    }]
                }
            }).
      otherwise({
          redirectTo: '/home'
      });

        //$locationProvider.html5Mode(false).hashPrefix('!');
    }]
);

app.filter('vgTime', function () {
    return function (ms) {
        ms = 1000 * Math.round(ms / 1000); // round to nearest second
        var d = new Date(ms);
        return d.getUTCMinutes() + ':' + d.getUTCSeconds();
    };
})

app.directive("siteLogoPlugin",
        ["VG_STATES", function (VG_STATES) {
            return {
                restrict: "E",
                require: "^videogular",
                template: "<img src='http://www.videogular.com/img/videogular.png' ng-show='API.currentState != \"play\"'>",
                link: function (scope, elem, attrs, API) {
                    scope.API = API;
                }
            }
        }
        ]);