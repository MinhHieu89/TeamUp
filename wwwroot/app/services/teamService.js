var TeamService = function() {

    var deleteTeam = function(teamId, done, fail) {
        $.post("/teams/delete/" + teamId)
            .done(done)
            .fail(fail);
    };

    return {
        deleteTeam: deleteTeam
    };

}();