app.factory('videoHelperFactory', [function () {
    var helper = {};
    var youtubeVideoURLBase = "https://www.youtube.com/watch?v={0}"
    var youtubeThumbnailURLBase = "http://img.youtube.com/vi/{0}/{1}.jpg"

    helper.getVideoURL = function (videoID, videoSrc) {
        var url = ""
        switch (videoSrc) {
            case "Youtube":
                url = this.stringFormat(youtubeVideoURLBase, videoID);
            case "Vimeo":
                url = "";
            default:
                url = this.stringFormat(youtubeVideoURLBase, videoID);
        }
        return url;
    }

    helper.getImageURL = function (videoID, videoSrc, imageID) {
        var url = ""
        switch (videoSrc) {
            case "Youtube":
                url = this.stringFormat(youtubeThumbnailURLBase, videoID, imageID != null ? imageID : 0);
            case "Vimeo":
                url = "";
            default:
                url = this.stringFormat(youtubeThumbnailURLBase, videoID, imageID != null ? imageID : 0);
        }
        return url;
    }

    helper.stringFormat = function (string) {
        var res = ""
        if (string != null) {
            for (var idx = 0; idx < arguments.length; idx++) {
                res = string.replace("{" + idx + "}", arguments[idx]);
            }
        }
        return res;
    }

    return helper;
}]
);