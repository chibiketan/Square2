var through = require('through2').obj;
var fs = require('graceful-fs');
var gutil = require('gulp-util');

function prefixStream(prefixText) {
    var stream = through();
    stream.write(prefixText);
    return stream;
}


module.exports = function () {
    return through(function (file, enc, cb) {
        var prefixText = new Buffer("zutzutzutzut"); // allocate ahead of time

        if (file.isBuffer()) {
            gutil.log("file est un buffer");
            file.contents = Buffer.concat([prefixText, file.contents]);
        }

        if (file.isStream()) {
            gutil.log("file est un stream");
            // define the streamer that will transform the content
            var streamer = prefixStream("toto");
            // catch errors from the streamer and emit a gulp plugin error
            streamer.on('error', this.emit.bind(this, 'error'));
            // start the transformation
            file.contents = file.contents.pipe(streamer);
        }

        return cb(null, file);
    });
};