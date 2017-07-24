var RequestService = function() {

    var join = function(teamId, done, fail) {
        $.post("/request/join", { TeamId: teamId })
            .done(done)
            .fail(fail);
    };

    var invite = function (userId, done, fail) {
        $.post("/request/invite", { UserId: userId })
            .done(done)
            .fail(fail);
    };

    var accept = function(requestId, done, fail) {
        $.post("/request/accept", { RequestId: requestId })
            .done(done)
            .fail(fail);
    };

    var reject = function (requestId, done, fail) {
        $.post("/request/reject", { RequestId: requestId })
            .done(done)
            .fail(fail);
    };

    var cancel = function (requestId, done, fail) {
        $.post("/request/cancel", { RequestId: requestId })
            .done(done)
            .fail(fail);
    };

    var leave = function (done, fail) {
        $.post("/request/leave")
            .done(done)
            .fail(fail);
    };

    return {
        join: join,
        invite: invite,
        accept: accept,
        reject: reject,
        cancel: cancel,
        leave: leave
    };

}();