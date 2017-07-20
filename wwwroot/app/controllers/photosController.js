var PhotosController = function(photoService) {

    var $uploadCrop,
        $inputFile,
        $acceptedFileTypes = [".png", ".jpg", ".jpeg"];

    var init = function(container, cropOption) {
        $uploadCrop = $(container + " .image-viewport").croppie(cropOption);
        $(container).on("change", ".input-file", readFile);
        $(container).on("click", ".start-button", startUpload);
    };

    var readFile = function(e) {
        $inputFile = e.target;
        if ($inputFile.files && $inputFile.files[0]) {
            var fileName = $inputFile.files[0].name;
            var fileType = fileName.substring(fileName.indexOf("."));
            if ($acceptedFileTypes.indexOf(fileType) < 0) {
                swal("Lỗi", "Bạn chỉ được phép tải ảnh .png, .jpg hoặc .jpeg", "error");
                return;
            }
            var reader = new FileReader();
            reader.onload = function(e) {
                $uploadCrop.croppie("bind", { url: e.target.result });
            };
            reader.readAsDataURL($inputFile.files[0]);
        }
    };

    var startUpload = function (e) {
        e.preventDefault();
        if ($inputFile === undefined) {
            swal("Lỗi", "Bạn hãy chọn ảnh để upload", "error");
            return;
        }
        $uploadCrop.croppie("result", {
            type: "canvas",
            size: "viewport"
        }).then(function (resp) {
            var blob = dataURLtoBlob(resp);
            var formData = new FormData();
            formData.append("file", blob);
            photoService.upload(formData, done, fail);
        });
    };

    var done = function() {
        swal("Thành công", "Hình đại diện của bạn đã được cập nhật", "success");
    };

    var fail = function(err) {
        swal(err.responseText, null, "error");
    };

    var dataURLtoBlob = function(dataURL) {
        var BASE64_MARKER = ";base64,",
            parts,
            contentType,
            raw;

        if (dataURL.indexOf(BASE64_MARKER) === -1) {
            parts = dataURL.split(",");
            contentType = parts[0].split(":")[1];
            raw = decodeURIComponent(parts[1]);
            return new Blob([raw], { type: contentType });
        }
        parts = dataURL.split(BASE64_MARKER);
        contentType = parts[0].split(":")[1];
        raw = window.atob(parts[1]);
        var rawLength = raw.length;
        var uInt8Array = new Uint8Array(rawLength);
        for (var i = 0; i < rawLength; ++i) {
            uInt8Array[i] = raw.charCodeAt(i);
        }
        return new Blob([uInt8Array], { type: contentType });
    };

    return {
        init: init
    };

}(PhotoService);