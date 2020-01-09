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
                        $scope.videos = vids; //vids.chunk(4);
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