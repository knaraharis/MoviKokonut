app.controller("popularVideoCotroller",
    ['$scope', "popularVideoData", function VideoCtrl($scope, vDetails) {
        $scope.videos = [];
        for (var idx = 0; idx < vDetails.data.length; idx++) {
            var videoConfig = {
                sources: [{ src: appUtils.getVideoURL(vDetails.data[idx].videoId, vDetails.data[idx].videoSrc) }],
                items: vDetails.data[idx].items,
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
            $scope.videos.push(videoConfig);
        }
        appUtils.reloadDOM();
    }]
);