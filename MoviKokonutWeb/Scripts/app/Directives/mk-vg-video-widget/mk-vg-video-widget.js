app.directive("vgVideoWidget",
        function vgVideoWidget() {
            return {
                scope: {
                    vgConfig: "="
                },
                //templateUrl: appConstants.webHostURLBase + 'wwwroot/dist/app-html/mk-vg-video-widget/mk-vg-video-widget.html',
                templateUrl: appConstants.webHostURLBase + 'Scripts/app/Directives/mk-vg-video-widget/mk-vg-video-widget.html',
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