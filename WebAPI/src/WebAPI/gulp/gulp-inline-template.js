var through = require('through2').obj;
var fs = require('graceful-fs');
var gutil = require('gulp-util');
var stripBomFromBuffer = require('strip-bom');

function prefixStream(prefixText) {
    var stream = through();
    stream.write(prefixText);
    return stream;
}


module.exports = function (templateBasePath) {
    return through(function (file, enc, cb) {
        if (file.isBuffer()) {
            var fullContent = file.contents.toString();

            var regex = /templateurl:\s*["']([^"]+)["']/gim;

            var match = regex.exec(fullContent);
            if (null != match) {
                // il y a bien une url
                gutil.log("lecture du fichier : " + match[1]);

                fs.readFile(templateBasePath + match[1], "utf-8",
                    function (err, data) {
                        if (err) {
                            return cb("Erreur lors de la lecture du fichier : " + err);
                        }
                        // on considère que tous les fichiers .ts sont avec un BOM
                        file.contents = new Buffer(fullContent.replace(regex, "template: `" + stripBomFromBuffer(data).toString() + "`"));
                        cb(null, file);
                    });
            } else {
                cb(null, file);
            }
        }

        if (file.isStream()) {
            this.emit("ERROR", "Not implemented");
        }
    });
};