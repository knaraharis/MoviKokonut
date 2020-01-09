app.directive("vgItem",
    function vgTweet() {
        return {
            scope: {
                vgDataProvider: "="
            },
            templateUrl: appConstants.webHostURLBase + 'Scripts/app/Directives/mk-vg-item/mk-vg-item.html'
        };
    }
);