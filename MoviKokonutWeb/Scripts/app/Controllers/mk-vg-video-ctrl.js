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