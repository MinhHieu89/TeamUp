var MemberInfoController = function(requestService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-cancel", cancelJoinRequest);
    };

    var cancelJoinRequest = function(e) {
        button = $(e.target);
        var teamId = button.attr("data-team-id");

        confirm("Bạn chắc chắn muốn hủy yêu cầu chứ?",
            function() {
                requestService.cancel(teamId, done, fail);
            });
    };

    var done = function() {
        swal({
                title: "Thành công",
                text: "Yêu cầu đã được hủy",
                type: "success",
                showConfirmButton: true
            },
            function () {
                button.parent().prev()[0].innerText = "Canceled";
                button.remove();
            });
    };

    fail = function (err) {
        swal(err.responseText, null, "error");
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

    return {
        init: init
    };

}(RequestService);