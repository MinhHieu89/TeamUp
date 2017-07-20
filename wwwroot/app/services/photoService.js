var PhotoService = function() {
    var upload = function (data, done, fail) {
        $.ajax({
            url: "/photos/upload",
            type: "POST",
            data: data,
            contentType: false,
            cache: false,
            processData: false,
            success: done,
            error: fail
        });
    };

    return {
        upload: upload
    };
}();