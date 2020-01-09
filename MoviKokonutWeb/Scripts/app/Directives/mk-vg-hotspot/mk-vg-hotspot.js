app.directive(
		'vgHotspots',
		['$compile', function ($compile) {
		    return {
		        restrict: 'E',
		        require: '^videogular',
		        templateUrl: appConstants.webHostURLBase + 'Scripts/app/Directives/mk-vg-hotspot/mk-vg-hotspot.html',
		        //templateUrl: appConstants.webHostURLBase + 'wwwroot/dist/app-html/mk-vg-hotspot/mk-vg-hotspot.html',
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

