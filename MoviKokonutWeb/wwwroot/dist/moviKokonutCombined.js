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
    youtubeVideoURLBase: "https://www.youtube.com/watch?v={0}",
    youtubeThumbnailURLBase: "http://img.youtube.com/vi/{0}/{1}.jpg",
    apiHostURLBase: "http://localhost/MoviKokonutWebAPI/",
    webHostURLBase: "http://localhost/MoviKokonutWeb/"
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
      otherwise({
          redirectTo: '/videos'
      });

        //$locationProvider.html5Mode(false).hashPrefix('!');
    }]
);



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
function VideoDetails() {
    this.videoId = 0;
    this.videoName = "";
    this.videoSrc = "";
    this.items = [];
}

function ItemDetails(name, id, desc) {
    this.itemName = name;
    this.itemId = id;
    this.itemDescription = desc;
    this.itemLocations = [];

    this.itemDefaultLocation = function () {
        if (this.itemLocations.length > 0)
            return this.itemLocations[0];
        else{
            if(id == 1){
                return new ItemLocation(25, 45, 56, 85);
            }
            else {
                return new ItemLocation(78, 135, 176, 400);
            }
        }
    }
}

function ItemLocation(sTm, eTm, pX, pY) {
    this.startTime = sTm;
    this.endTime = eTm;
    this.posX = pX;
    this.posY = pY;
}

function GetVideoDetailsMock() {
    var video = new VideoDetails();
    video.videoId = 1;
    video.videoName = "Test Video";
    video.videoSrc = "https://www.youtube.com/watch?v=I7ZUkd44-Co";
    var itm1 = new ItemDetails("Item1", 1, "Item 1 description")
    itm1.itemLocations.push(new ItemLocation(25, 45, 56, 85))
    itm1.itemLocations.push(new ItemLocation(56, 90, 16, 85))
    itm1.itemLocations.push(new ItemLocation(345, 456, 35, 55))
    itm1.itemLocations.push(new ItemLocation(489, 525, 56, 25))
    itm1.itemLocations.push(new ItemLocation(600, 735, 67, 35))

    var itm2 = new ItemDetails("Item2", 2, "Item 2 description")
    itm2.itemLocations.push(new ItemLocation(78, 135, 76, 40))
    itm2.itemLocations.push(new ItemLocation(100, 126, 56, 85))
    itm2.itemLocations.push(new ItemLocation(157, 225, 35, 85))
    itm2.itemLocations.push(new ItemLocation(400, 554, 16, 22))
    itm2.itemLocations.push(new ItemLocation(600, 735, 67, 32))

    video.items = [itm1, itm2];

    return video;
}
app.factory('videoFactory', ['$http', function ($http) {

    var urlBase = appConstants.apiHostURLBase + 'api/video';
    var videoFactory = {};

    videoFactory.getVideos = function () {
        return $http.get(urlBase);
    };

    videoFactory.getVideo = function (id) {
        return $http.get(urlBase + '/' + id)
    };

    videoFactory.insertVideo = function (video) {
        return $http.post(urlBase, video);
    };

    videoFactory.updateVideo = function (video) {
        return $http.put(urlBase + '/' + video.ID, cust)
    };

    videoFactory.deleteVideo = function (id) {
        return $http.delete(urlBase + '/' + id);
    };

    videoFactory.getVideoItems = function (id) {
        return $http.get(urlBase + '/' + id + '/items');
    };

    return videoFactory;
}]);
app.controller("vgVideoController",
    ['$scope', "videoData", function VideoCtrl($scope, vDetails) {
        $scope.videoDetails = vDetails.data;
        $scope.config = {
            sources: [{ src: appUtils.getVideoURL($scope.videoDetails.videoId, $scope.videoDetails.videoSrc) }],
            items: $scope.videoDetails.items,
            plugins: {
                analytics: {
                    category: "Videogular",
                    label: "MovKoKnut Video Widget",
                    events: {
                        ready: true,
                        play: true,
                        pause: true,
                        stop: true,
                        complete: true,
                        progress: 10
                    }
                }
            }
        };
    }]
);
app.controller('videosController', ['$scope', 'videoFactory',
        function ($scope, videoFactory) {

            $scope.status;
            $scope.videos;
            $scope.items;
            $scope.util = appUtils;
            getVideos();

            function getVideos() {
                videoFactory.getVideos()
                    .success(function (vids) {
                        $scope.videos = vids.chunk(4);
                    })
                    .error(function (error) {
                        $scope.status = 'Unable to load videos data: ' + error.message;
                    });
            }

            $scope.updateVideo = function (id) {
                var vid;
                for (var i = 0; i < $scope.videos.length; i++) {
                    var currVid = $scope.videos[i];
                    if (currVid.ID === id) {
                        vid = currVid;
                        break;
                    }
                }

                videoFactory.updateVideo(vid)
                  .success(function () {
                      $scope.status = 'Updated Video! Refreshing customer list.';
                  })
                  .error(function (error) {
                      $scope.status = 'Unable to update video: ' + error.message;
                  });
            };

            $scope.insertVideo = function () {
                //Fake customer data
                var vid = {
                    videoId: '',
                    videoName: 'JoJo',
                    videoSrc: 'Pikidily'
                };
                videoFactory.insertVideo(vid)
                    .success(function () {
                        $scope.status = 'Inserted Video! Refreshing video list.';
                        $scope.customers.push(vid);
                    }).
                    error(function (error) {
                        $scope.status = 'Unable to insert video: ' + error.message;
                    });
            };

            $scope.deleteVideo = function (id) {
                videoFactory.deleteVideo(id)
                .success(function () {
                    $scope.status = 'Deleted Video! Refreshing videos list.';
                    for (var i = 0; i < $scope.videos.length; i++) {
                        var vid = $scope.videos[i];
                        if (vid.ID === id) {
                            $scope.videos.splice(i, 1);
                            break;
                        }
                    }
                    $scope.items = null;
                })
                .error(function (error) {
                    $scope.status = 'Unable to delete video: ' + error.message;
                });
            };

            $scope.getVideoItems = function (id) {
                videoFactory.getVideoItems(id)
                .success(function (items) {
                    $scope.status = 'Retrieved items!';
                    $scope.items = items;
                })
                .error(function (error) {
                    $scope.status = 'Error retrieving items! ' + error.message;
                });
            };
        }]);
app.directive("vgVideoWidget",
        function vgVideoWidget() {
            return {
                scope: {
                    vgConfig: "="
                },
                templateUrl: appConstants.webHostURLBase + 'wwwroot/dist/app-html/mk-vg-video-widget/mk-vg-video-widget.html',
                controller: "vgVideoWidgetController",
                controllerAs: "ctrl",
                link: function link(scope, elem, attrs, ctrl) {
                    ctrl.itemElements = elem[0].getElementsByTagName("vg-item");
                }
            };
        }
    );

app.controller("vgVideoWidgetController",
    ["$scope", function vgVideoWidgetController($scope) {
        this.API = null;

        this.onPlayerReady = function onPlayerReady(API) {
            this.API = API;
        };

        this.init = function init() {
            var items = [];
            var uniqueItems = [];
            if ($scope.vgConfig != null && $scope.vgConfig.items != null) {
                for (var i = 0, l = $scope.vgConfig.items.length; i < l; i++) {
                    var uniqItm = {};
                    uniqItm.ItemDetails = $scope.vgConfig.items[i];
                    var ul = {};

                    if ($scope.vgConfig.items[i].itemDefaultLocation)
                        uniqItm.DefaultLocation = {
                            start: $scope.vgConfig.items[i].itemDefaultLocation().startTime,
                            end: $scope.vgConfig.items[i].itemDefaultLocation().endTime
                        };
                    else if ($scope.vgConfig.items[i].itemLocations && $scope.vgConfig.items[i].itemLocations.length > 0)
                        uniqItm.DefaultLocation = {
                            start: $scope.vgConfig.items[i].itemLocations[0].startTime,
                            end: $scope.vgConfig.items[i].itemLocations[0].endTime
                        };
                    else
                        uniqItm.DefaultLocation = {
                            start: 0,
                            end: 0
                        };

                    for (var locIdx = 0, m = $scope.vgConfig.items[i].itemLocations.length; locIdx < m; locIdx++) {
                        var item = {};
                        var loc = $scope.vgConfig.items[i].itemLocations[locIdx];

                        item.timeLapse = {
                            start: loc.startTime,
                            end: loc.endTime
                        };

                        item.locationPoint = {
                            posX: loc.posX,
                            posY: loc.posY
                        };

                        item.onLeave = this.onLeave.bind(this);
                        item.onUpdate = this.onUpdate.bind(this);
                        item.onComplete = this.onComplete.bind(this);
                        item.onChoose = this.onChoose;

                        item.params = item;
                        item.params.default = uniqItm;

                        item.params.index = i;

                        items.push(item);
                    }
                    uniqueItems.push(uniqItm);
                }
            }
            this.config = {
                sources: $scope.vgConfig.sources,
                cuePoints: {
                    items: items
                },
                uniqItems: uniqueItems,
                plugins: $scope.vgConfig.plugins
            };
        };

        this.onLeave = function onLeave(currentTime, timeLapse, params) {
            //params.itemDetails.completed = false;
            params.default.selected = false;
        };

        this.onComplete = function onComplete(currentTime, timeLapse, params) {
            //params.itemDetails.completed = true;
            params.default.selected = false;
        };

        this.onUpdate = function onUpdate(currentTime, timeLapse, params) {
            if (!params.selected) {
                //this.itemElements[params.index].scrollIntoView();
                //params.itemDetails.completed = false;
                params.default.selected = true;
            }
        };

        this.onChoose = function onChoose(params, element) {
            var tmpl = '<div><header><p style="display:none">' + params.default.ItemDetails.itemId + '</p><div class="user"><h4>' + params.default.ItemDetails.itemName + '</h4><span>@' + params.default.ItemDetails.itemDescription + '</span></div></header><p>' + params.default.ItemDetails.itemDescription + '</p></div>'
            element.html('');
            element.html(tmpl);
        }

        this.init();
    }]
);
app.directive("vgItem",
    function vgTweet() {
        return {
            scope: {
                vgDataProvider: "="
            },
            templateUrl: appConstants.webHostURLBase + 'wwwroot/dist/app-html/mk-vg-item/mk-vg-item.html'
        };
    }
);
app.directive(
		'vgCuepoints',
		[function () {
		    return {
		        restrict: 'E',
		        require: '^videogular',
		        templateUrl: appConstants.webHostURLBase + 'wwwroot/dist/app-html/mk-vg-cuepoint-marker/mk-vg-cuepoint-marker.html',
		        scope: {
		            cuepoints: '=vgCuepointsConfig',
		            theme: '=vgCuepointsTheme',
		        },
		        link: function ($scope, elem, attr, API) {
		            // shamelessly stolen from part of videogular's updateTheme function
		            function updateTheme(value) {
		                if (value) {
		                    var headElem = angular.element(document).find("head");
		                    headElem.append("<link rel='stylesheet' href='" + value + "'>");
		                }
		            }

		            $scope.calcLeft = function (cuepoint) {
		                if (API.totalTime === 0) return '-1000';

		                var videoLength = API.totalTime / 1000;
		                return (cuepoint.timeLapse.start * 100 / videoLength).toString();
		            };

		            updateTheme($scope.theme);
		        },
		    };
		}]);
app.directive(
		'vgHotspots',
		['$compile', function ($compile) {
		    return {
		        restrict: 'E',
		        require: '^videogular',
		        //templateUrl: appConstants.webHostURLBase + 'Scripts/app/Directives/mk-vg-hotspot/mk-vg-hotspot.html',
		        templateUrl: appConstants.webHostURLBase + 'wwwroot/dist/app-html/mk-vg-hotspot/mk-vg-hotspot.html',
		        scope: {
		            hostpots: '=vgHotspotsConfig',
		            theme: '=vgHotspotsTheme',
		        },
		        link: function ($scope, elem, attr, API) {
		            // shamelessly stolen from part of videogular's updateTheme function
		            function updateTheme(value) {
		                if (value) {
		                    var headElem = angular.element(document).find("head");
		                    headElem.append("<link rel='stylesheet' href='" + value + "'>");
		                }
		            }

		            function init() {
		                $scope.API = API;
		                $scope.showDetails = false
                        $scope.selectedElement = null
		                for (var i = 0, l = $scope.hostpots.items.length; i < l; i++) {
		                    $scope.hostpots.items[i].showHotspot = false;
		                }
		            }

		            $scope.ShowHotspotDetails = function (hotspot) {
		                API.pause();
		                var detailsElmt = elem.children('vg-hotspot-details')
		                var lft = parseInt(hotspot.posLeft)
		                lft = lft + 10;
		                detailsElmt.css({
		                    left: lft.toString() + 'px',
		                    top: (hotspot.posTop) + 'px'
		                });
		                $scope.showDetails = true;
		                if (hotspot.onChoose)
		                    hotspot.onChoose(hotspot, detailsElmt);
		                //detailsElmt.html('')
		                //$scope.selectedElement = hotspot
		                //var el = $compile('<vg-item vg-data-provider="$scope.selectedElement.default.ItemDetails"></vg-item>')($scope);
		                //angular.element(detailsElmt[0]).append();
		            }

		            $scope.UpdatePosition = function (newWidth, newHeight) {
		                for (var i = 0, l = $scope.hostpots.items.length; i < l; i++) {
		                    $scope.hostpots.items[i].posLeft = (newWidth * ($scope.hostpots.items[i].locationPoint.posX / 100)).toString()
		                    $scope.hostpots.items[i].posTop = (newHeight * ($scope.hostpots.items[i].locationPoint.posY / 100)).toString();
		                }
		            }

		            $scope.onUpdateTime = function onUpdateTime(newCurrentTime) {
		                //$scope.showDetails = false;
		                currentTime = newCurrentTime / 1000;
		                for (var i = 0, l = $scope.hostpots.items.length; i < l; i++) {
		                    var cp = $scope.hostpots.items[i];
		                    var currentSecond = parseInt(currentTime, 10);
		                    var start = parseInt(cp.timeLapse.start, 10);

		                    // If timeLapse.end is not defined we set it as 1 second length
		                    if (!cp.timeLapse.end) cp.timeLapse.end = cp.timeLapse.start + 1;

		                    if (currentTime < cp.timeLapse.end) cp.$$$isCompleted = false;

		                    // Fire the onEnter event once reach to the cue point
		                    if (!cp.$$$isDirty && currentSecond === start) {
		                        cp.showHotspot = true;
		                        cp.$$$isDirty = true;
		                    }

		                    // Check if we've been reached to the cue point
		                    if (currentTime > cp.timeLapse.start) {
		                        // We're in the timelapse
		                        if (currentTime < cp.timeLapse.end) {
		                            // Trigger onUpdate each time we enter here
		                            cp.showHotspot = true;

		                            // Trigger onEnter if we enter on the cue point by manually seeking
		                            if (!cp.$$$isDirty) {
		                                cp.showHotspot = true;
		                            }

		                            cp.$$$isDirty = true;
		                        }

		                        // We've been passed the cue point
		                        if (currentTime >= cp.timeLapse.end) {
		                            if (!cp.$$$isCompleted) {
		                                cp.$$$isCompleted = true;
		                                cp.showHotspot = false;
		                            }

		                            cp.$$$isDirty = false;
		                        }
		                    }
		                    else {
		                        if (cp.$$$isDirty) {
		                            cp.showHotspot = false;
		                        }
		                        cp.$$$isDirty = false;
		                    }
		                }
		            };

		            $scope.$watch(
                        function () {
                            return API.currentTime;
                        },
                        function (newVal, oldVal) {
                            $scope.onUpdateTime(newVal);
                        }
                    );

		            $scope.$watch(
                        function () {
                            return {
                                width: elem.parent().width(),
                                height: elem.parent().height(),
                            }
                        },
                       function (newValues) {
                           elem.css({
                               width: newValues.width + 'px',
                               height: newValues.height + 'px',
                           })
                           $scope.UpdatePosition(newValues.width, newValues.height);
                       }, //listener 
                       true //deep watch
                    );
		            init();
		            updateTheme($scope.theme);
		        },
		    };
		}]);

