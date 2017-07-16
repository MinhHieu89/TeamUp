var RequestService = function() {

    var createJoinRequest = function(teamId, done, fail) {
        $.post("/request/join", { TeamId: teamId })
            .done(done)
            .fail(fail);
    };

    var approve = function(userId, teamId, done, fail) {
        $.post("/request/approve", { UserId: userId, TeamId: teamId })
            .done(done)
            .fail(fail);
    };

    var reject = function (userId, teamId, done, fail) {
        $.post("/request/reject", { UserId: userId, TeamId: teamId })
            .done(done)
            .fail(fail);
    };

    var cancel = function (teamId, done, fail) {
        $.post("/request/cancel", { TeamId: teamId })
            .done(done)
            .fail(fail);
    };

    var leave = function (done, fail) {
        $.post("/request/leave")
            .done(done)
            .fail(fail);
    };

    return {
        createJoinRequest: createJoinRequest,
        approve: approve,
        reject: reject,
        cancel: cancel,
        leave: leave
    };

}();