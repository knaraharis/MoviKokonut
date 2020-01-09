var gulp = require('gulp');
var bower = require('gulp-bower');
var concat = require('gulp-concat');

gulp.task('bower', function () {
    return bower('./bower_components');
});

gulp.task('copyLibs', ['bower'], function () {
    gulp.src(['bower_components/angular/*.*', 'bower_components/angular-mocks/*.*', 'bower_components/angular-route/*.*', 'bower_components/angular-sanitize/*.*', 'bower_components/angular-scenario/*.*', 'bower_components/angulartics/src/*.*'])
        .pipe(gulp.dest('wwwroot/lib/angular'));

    gulp.src(['bower_components/videogular/*.*',
        'bower_components/videogular-angulartics/*.*',
        'bower_components/videogular-controls/*.*',
        'bower_components/videogular-overlay-play/*.*',
        'bower_components/videogular-poster/*.*',
        'bower_components/videogular-themes-default/*.*',
        'bower_components/videogular-themes-default/**/*.*',
        'bower_components/videogular-youtube/*.*'])
        .pipe(gulp.dest('wwwroot/lib/videogular'));

    gulp.src(['bower_components/jquery/dist/*.*'])
        .pipe(gulp.dest('wwwroot/lib/jquery'));

    gulp.src(['bower_components/bootstrap/dist/css/*.*'])
        .pipe(gulp.dest('wwwroot/lib/bootstrap'));
});

gulp.task('concatAppSrc', function () {
    return gulp.src(['Scripts/app/MainApp.js',
                    'Scripts/app/Models/ProductMappingModels.js',
                    'Scripts/app/Factories/VideoFactory.js',
                    'Scripts/app/Controllers/mk-vg-video-ctrl.js',
                    'Scripts/app/Controllers/VideosController.js',
                    'Scripts/app/Directives/mk-vg-video-widget/mk-vg-video-widget.js',
                    'Scripts/app/Directives/mk-vg-item/mk-vg-item.js',
                    'Scripts/app/Directives/mk-vg-cuepoint-marker/mk-vg-cuepoint-marker.js',
                    'Scripts/app/Directives/mk-vg-hotspot/mk-vg-hotspot.js'])
    .pipe(concat('wwwroot/dist/moviKokonutCombined.js'))
    .pipe(gulp.dest('.'));
});

gulp.task('copyHtml', function () {
    return gulp.src(['Scripts/app/Directives/**/*.html'])
        .pipe(gulp.dest('wwwroot/dist/app-html'));
});

gulp.task('copyCss', function () {
    return gulp.src(['Content/*.css'])
        .pipe(gulp.dest('wwwroot/dist'));
});

gulp.task('default', ['copyLibs', 'copyCss', 'concatAppSrc', 'copyHtml']);

