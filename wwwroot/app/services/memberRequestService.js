var MemberRequestService = function() {

    var join = function(teamId, done, fail) {
        $.post("/memberRequest/join", { TeamId: teamId })
            .done(done)
            .fail(fail);
    };

    var accept = function(requestId, done, fail) {
        $.post("/memberRequest/accept", { RequestId: requestId })
            .done(done)
            .fail(fail);
    };

    var reject = function (requestId, done, fail) {
        $.post("/memberRequest/reject", { RequestId: requestId })
            .done(done)
            .fail(fail);
    };

    var cancel = function (requestId, done, fail) {
        $.post("/memberRequest/cancel", { RequestId: requestId })
            .done(done)
            .fail(fail);
    };

    var leave = function (done, fail) {
        $.post("/memberRequest/leave")
            .done(done)
            .fail(fail);
    };

    return {
        join: join,
        accept: accept,
        reject: reject,
        cancel: cancel,
        leave: leave
    };

}();