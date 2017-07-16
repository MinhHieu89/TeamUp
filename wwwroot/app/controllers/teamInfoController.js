var TeamInfoController = function (teamService, requestService) {

    var init, approveButton, rejectButton,
        deleteTeam, leaveTeam, approveJoinRequest, rejectJoinRequest,
        deleteSuccess, leaveSuccess, approveSuccess, rejectSuccess, fail, confirm;

    init = function (container) {
        $(container).on("click", ".js-team-delete", deleteTeam);
        $(container).on("click", ".js-leave", leaveTeam);
        $(container).on("click", ".js-approve", approveJoinRequest);
        $(container).on("click", ".js-reject", rejectJoinRequest);
    };

    deleteTeam = function (e) {
        var teamId = $(e.target).attr("data-team-id");
        confirm("Bạn muốn giải thể đội bóng?",
            function () {
                teamService.deleteTeam(teamId, deleteSuccess, fail);
            });
    };

    leaveTeam = function () {
        confirm("Bạn muốn rời khỏi đội?",
            function () {
                requestService.leave(leaveSuccess, fail);
            });
    };

    approveJoinRequest = function (e) {
        approveButton = $(e.target);
        var userId = approveButton.attr("data-user-id"),
            teamId = approveButton.attr("data-team-id");
        requestService.approve(userId, teamId, approveSuccess, fail);
    };

    rejectJoinRequest = function (e) {
        rejectButton = $(e.target);
        var userId = rejectButton.attr("data-user-id"),
            teamId = rejectButton.attr("data-team-id");

        requestService.reject(userId, teamId, rejectSuccess, fail);
    };


    confirm = function (title, callback) {
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

    deleteSuccess = function () {
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

    approveSuccess = function (username) {
        approveButton.parents("tr").fadeOut(300,
            function () {
                $(this.remove());
            });
        $(".list-group-item > ol").append("<li>" + username + "</li>");
    };

    leaveSuccess = function () {
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

    rejectSuccess = function () {
        rejectButton.parents("tr").fadeOut(300,
            function () {
                $(this.remove());
            });
    };

    fail = function (err) {
        swal(err.responseText, null, "error");
    };

    return {
        init: init
    };

}(TeamService, RequestService);