app.directive(
		'vgCuepoints',
		[function () {
		    return {
		        restrict: 'E',
		        require: '^videogular',
		        templateUrl: appConstants.webHostURLBase + 'Scripts/app/Directives/mk-vg-cuepoint-marker/mk-vg-cuepoint-marker.html',
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