var TeamInfoController = function (teamService, requestService) {

    var approveButton, rejectButton;

    var init = function (container) {
        $(container).on("click", ".js-team-delete", deleteTeam);
        $(container).on("click", ".js-leave", leaveTeam);
        $(container).on("click", ".js-approve", approveJoinRequest);
        $(container).on("click", ".js-reject", rejectJoinRequest);
        $(container).on("click", ".js-join", createJoinRequest);
    };

    var deleteTeam = function (e) {
        var teamId = $(e.target).attr("data-team-id");
        confirm("Bạn muốn giải thể đội bóng?",
            function () {
                teamService.deleteTeam(teamId, deleteSuccess, fail);
            });
    };

    var leaveTeam = function () {
        confirm("Bạn muốn rời khỏi đội?",
            function () {
                requestService.leave(leaveSuccess, fail);
            });
    };

    var approveJoinRequest = function (e) {
        approveButton = $(e.target);
        var userId = approveButton.attr("data-user-id"),
            teamId = approveButton.attr("data-team-id");
        requestService.approve(userId, teamId, approveSuccess, fail);
    };

    var rejectJoinRequest = function (e) {
        rejectButton = $(e.target);
        var userId = rejectButton.attr("data-user-id"),
            teamId = rejectButton.attr("data-team-id");

        requestService.reject(userId, teamId, rejectSuccess, fail);
    };

    var createJoinRequest = function (e) {
        button = $(e.target);
        var teamId = button.attr("data-team-id");

        requestService.createJoinRequest(teamId, done, fail);
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

    var redirectTo = function (url) {
        window.location.replace(url);
    };

    var deleteSuccess = function () {
        swal({
            title: "Thành công",
            text: "Dữ liệu đội bóng đã bị xóa",
            type: "success",
            showConfirmButton: false,
            timer: 2000
        },
            function () {
                redirectTo("/teams");
            });
    };

    var approveSuccess = function (username) {
        approveButton.parents("tr").fadeOut(300,
            function () {
                $(this.remove());
            });
        $(".list-group-item > ol").append("<li>" + username + "</li>");
    };

    var leaveSuccess = function () {
        swal({
            title: "Thành công",
            text: "Bạn đã rời khỏi đội",
            type: "success",
            showConfirmButton: false,
            timer: 2000
        },
            function () {
                redirectTo("/teams");
            });
    };

    var rejectSuccess = function () {
        rejectButton.parents("tr").fadeOut(300,
            function () {
                $(this.remove());
            });
    };

    var done = function () {
        swal("Thành công!", "Yêu cầu đã được gửi", "success");
    };

    var fail = function (err) {
        swal(err.responseText, null, "error");
    };

    return {
        init: init
    };

}(TeamService, RequestService);