var MemberInfoController = function(requestService) {

    var init = function (container) {
        $(container).on("click", ".js-invite", invite);
    };

    var invite = function(e) {
        var userId = $(e.target).attr("data-user-id");
        confirm("Mời vào đội?",
            function() {
                requestService.invite(userId, inviteSuccess, fail);
            });
    };

    var inviteSuccess = function() {
        swal("Thành công", "Yêu cầu đã được gửi đi", "success");
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