app.factory('videoFactory', ['$http', function ($http) {

    var urlBase = appConstants.apiHostURLBase + 'api/video';
    var videoFactory = {};

    videoFactory.getVideos = function () {
        return $http.get(urlBase);
    };

    videoFactory.getTopVideos = function () {
        return $http.get(urlBase + '/' + 'popular');
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