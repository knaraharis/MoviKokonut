app.directive("mkProductItem",
    function vgProductItem() {
        return {
            scope: {
                vgDataProvider: "="
            },
            templateUrl: appConstants.webHostURLBase + 'Scripts/app/Directives/mk-product-item/mk-product-item.html',
            link: function (scope) {
                
            }
        };
    }
);