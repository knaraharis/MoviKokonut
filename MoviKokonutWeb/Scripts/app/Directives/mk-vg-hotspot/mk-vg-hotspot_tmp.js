app.directive(
    'vgHotspot',
    [function () {
        return {
            restrict: 'A',
            require: '^videogular',
            template: '<div class="hotspot" ng-show="showHotspot" style="left: {{posLeft}}px;top:{{posTop}}px;"></div>',
            scope: {
                hotspot: '=',
            },
            link: function ($scope, elem, attr, API) {
                console.log("init called from hotspot child controller")
                $scope.showHotspot = false
                $scope.onUpdateTime = function onUpdateTime(newCurrentTime) {
                    currentTime = newCurrentTime / 1000;
                    var cp = $scope.hotspot;
                    var currentSecond = parseInt(currentTime, 10);
                    var start = parseInt(cp.timeLapse.start, 10);

                    // If timeLapse.end is not defined we set it as 1 second length
                    if (!cp.timeLapse.end) cp.timeLapse.end = cp.timeLapse.start + 1;

                    if (currentTime < cp.timeLapse.end) cp.$$$isCompleted = false;

                    // Fire the onEnter event once reach to the cue point
                    if (!cp.$$$isDirty && currentSecond === start) {
                        $scope.showHotspot = true;
                        cp.$$$isDirty = true;
                    }

                    // Check if we've been reached to the cue point
                    if (currentTime > cp.timeLapse.start) {
                        // We're in the timelapse
                        if (currentTime < cp.timeLapse.end) {
                            // Trigger onUpdate each time we enter here
                            $scope.showHotspot = true;

                            // Trigger onEnter if we enter on the cue point by manually seeking
                            if (!cp.$$$isDirty) {
                                $scope.showHotspot = true;
                            }

                            cp.$$$isDirty = true;
                        }

                        // We've been passed the cue point
                        if (currentTime >= cp.timeLapse.end) {
                            if (!cp.$$$isCompleted) {
                                cp.$$$isCompleted = true;
                                $scope.showHotspot = false;
                            }

                            cp.$$$isDirty = false;
                        }
                    }
                    else {
                        if (cp.$$$isDirty) {
                            $scope.showHotspot = false;
                        }

                        cp.$$$isDirty = false;
                    }
                };

                $scope.UpdatePosition = function (newWidth, newHeight) {
                    $scope.posLeft = (newWidth * ($scope.hotspot.locationPoint.posX / 100)).toString()
                    $scope.posTop = (newHeight * ($scope.hotspot.locationPoint.posY / 100)).toString();
                }

                $scope.$watch(
                    function () {
                        return {
                            width: elem.parent().width(),
                            height: elem.parent().height()
                        }
                    },
                    function (newValues) {
                        $scope.UpdatePosition(newValues.width, newValues.height);
                    },
                    true
                );

                $scope.$watch(
                    function () {
                        return API.currentTime;
                    },
                    function (newVal, oldVal) {
                        $scope.onUpdateTime(newVal);
                    }
                );
            }
        }
    }]);

app.controller("vgHotspotController",
    ["$scope", "$element", function vgVideoHotspotController($scope, elem) {

        this.init = function init() {
            $scope.showHotspot = false
            //this.UpdatePosition();
            
        };

        
        this.init();
    }]);


app.directive(
		'vgHotspots',
		[function () {
		    return {
		        restrict: 'E',
		        require: '^videogular',
		        templateUrl: '/VideoKartWeb/Scripts/app/Directives/mk-vg-hotspot/mk-vg-hotspot.html',
		        scope: {
		            hostpots: '=vgHotspotsConfig',
		            theme: '=vgHotspotsTheme',
		        },
		        controller: "vgHotspotsController",
		        controllerAs: "ctrl",
		        link: function ($scope, elem, attr, API) {
		            // shamelessly stolen from part of videogular's updateTheme function
		            function updateTheme(value) {
		                if (value) {
		                    var headElem = angular.element(document).find("head");
		                    headElem.append("<link rel='stylesheet' href='" + value + "'>");
		                }
		            }

		            $scope.calcLeft = function (hotspot) {
		                var parentWidth = elem.parent()[0].offsetWidth
		                var spotVal = hotspot.locationPoint.posX

		                return (parentWidth * (spotVal / 100)).toString();
		            };

		            $scope.calcTop = function (hotspot) {
		                var parentHeight = elem.parent()[0].offsetHeight
		                var spotVal = hotspot.locationPoint.posY
		                return (parentHeight * (spotVal / 100)).toString();
		            };

		            $scope.onUpdateTime = function onUpdateTime(newCurrentTime) {
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
		                    return {
		                        width: elem.parent().width(),
		                        height: elem.parent().height()
		                    }
		                },
                        function (newValues) {
                            elem.css({
                                width: newValues.width + 'px',
                                height: newValues.height + 'px',
                            })
                        },
                        true
		            );


		            //$scope.$watch(
		            //    function () {
		            //        return {
		            //            width: elem.parent().width(),
		            //            height: elem.parent().height(),
		            //        }
		            //    },
		            //   function () {
		            //       elem.css({
		            //           width: elem.parent()[0].offsetWidth + 'px',
		            //           height: elem.parent()[0].offsetHeight + 'px',
		            //       });
		            //   }, //listener 
		            //   true //deep watch
		            //);
		            updateTheme($scope.theme);
		        },
		    };
		}]);

app.controller("vgHotspotsController",
    ["$scope", "$element", function vgVideoHotspotController($scope, elem) {



        this.init = function init() {

            for (var i = 0, l = $scope.hostpots.items.length; i < l; i++) {
                $scope.hostpots.items[i].showHotspot = false;
            }
            console.log("init called from controller")
        };

        this.init();
    }]
);