/// <binding AfterBuild='ts' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var clean = require('gulp-clean');
var ts = require('gulp-typescript');
var sass = require('gulp-sass');
var sourcemaps = require('gulp-sourcemaps');


var destPath = './wwwroot/libs/';
// Delete the dist directory
gulp.task('clean', function () {
    return gulp.src(destPath)
        .pipe(clean());
});

gulp.task("scriptsNStyles", () => {
    gulp.src([
            'core-js/client/**',
            'systemjs/dist/system.src.js',
            'reflect-metadata/**',
            'rxjs/**/*.js',
            'rxjs/**/*.js.map',
//            'rxjs/**/*.ts',
            'zone.js/dist/**',
            '@angular/**/*.js',
            '@angular/**/*.map',
            '@angular/**/*.ts',
            'jquery/dist/jquery.*js',
            'bootstrap/dist/js/bootstrap.*js',
            'bootstrap/dist/css/bootstrap.*css',
            'tether/dist/js/tether.*js',
            'moment/moment.js'
    ], {
        cwd: "node_modules/**"
    })
        .pipe(gulp.dest("./wwwroot/libs"));
});

var tsProject = ts.createProject('scripts/tsconfig.json');
var inlineTemplate = require('./gulp/gulp-inline-template.js');
gulp.task('ts', function (done) {
    //var tsResult = tsProject.src()
    var tsResult = gulp.src([
            "scripts/**/*.ts"
    ])
        .pipe(inlineTemplate("./wwwroot/"))
        .pipe(ts(tsProject), undefined, ts.reporter.fullReporter());
    return tsResult.js.pipe(gulp.dest('./wwwroot/appScripts'));
});

gulp.task('watch', ['watch.ts']);

gulp.task('watch.ts', ['ts'], function () {
    return gulp.watch('scripts/*.ts', ['ts']);
});

gulp.task('sass', function () {
    return gulp.src('./sass/*.scss')
        .pipe(sourcemaps.init())
        .pipe(sass({ outputStyle: 'compressed' }).on('error', sass.logError))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest(destPath));
});

gulp.task('default', ['scriptsNStyles', "sass", 'watch']);

