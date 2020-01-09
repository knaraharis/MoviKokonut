app.directive("mkVideoItem",
    function vgVideoItem() {
        return {
            scope: {
                vgDataProvider: "="
            },
            templateUrl: appConstants.webHostURLBase + 'Scripts/app/Directives/mk-video-item/mk-video-item.html',
            link: function (scope) {
                scope.util = appUtils;
            }
        };
    }
);