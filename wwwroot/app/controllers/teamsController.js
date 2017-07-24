var TeamsController = function(requestService) {

    var button,
        init, done, fail, createJoinRequest;

    init = function (container) {
        $(container).on("click", ".js-join", createJoinRequest);
    };

    createJoinRequest = function (e) {
        button = $(e.target);
        var teamId = button.attr("data-team-id");

        requestService.join(teamId, done, fail);
    };

    done = function () {
        swal("Thành công!", "Yêu cầu đã được gửi", "success");
    };

    fail = function (err) {
        swal(err.responseText, null, "error");

    };
    return {
        init: init
    };
}(RequestService);