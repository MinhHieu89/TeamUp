var RequestsController = function (requestService) {

    var acceptButton, rejectButton, cancelButton;

    var init = function (container) {
        $(container).on("click", ".js-approve", accept);
        $(container).on("click", ".js-reject", reject);
        $(container).on("click", ".js-cancel", cancel);
    };

    var accept = function (e) {
        acceptButton = $(e.target);
        confirm("Chấp nhận yêu cầu?",
            function() {
                var requestId = acceptButton.attr("data-request-id");
                requestService.accept(requestId, acceptSuccess, fail);
            });
    };

    var acceptSuccess = function () {
        swal({
                title: "Thành công",
                text: "Yêu cầu được chấp nhận",
                type: "success",
                showConfirmButton: true
            },
            function () {
                acceptButton.parent().prev()[0].innerText = "Approved";
                acceptButton.next().remove();
                acceptButton.remove();
            });
    };

    var reject = function (e) {
        rejectButton = $(e.target);

        confirm("Bạn muốn từ chối yêu cầu này?",
            function() {
                var requestId = rejectButton.attr("data-request-id");
                requestService.reject(requestId, rejectSuccess, fail);
            });
    };

    var rejectSuccess = function () {
        swal({
                title: "Thành công",
                text: "Yêu cầu đã bị từ chối",
                type: "success",
                showConfirmButton: true
            },
            function () {
                rejectButton.parent().prev()[0].innerText = "Rejected";
                rejectButton.prev().remove();
                rejectButton.remove();
            });
    };

    var cancel = function (e) {
        cancelButton = $(e.target);
        var requestId = cancelButton.attr("data-request-id");

        confirm("Bạn chắc chắn muốn hủy yêu cầu chứ?",
            function () {
                requestService.cancel(requestId, cancelSuccess, fail);
            });
    };

    var cancelSuccess = function () {
        swal({
                title: "Thành công",
                text: "Yêu cầu đã được hủy",
                type: "success",
                showConfirmButton: true
            },
            function () {
                cancelButton.parent().prev()[0].innerText = "Canceled";
                cancelButton.remove();
            });
    };

    var confirm = function (title, callback) {
        swal({
                title: title,
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Chắc chắn",
                closeOnConfirm: false
            },
            callback);
    };

    var fail = function (err) {
        swal(err.responseText, null, "error");
    };

    return {
        init: init
    };

}(RequestService);