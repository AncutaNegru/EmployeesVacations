$("#IDBusinessUnit").change(function () {
    if ($(this).val() === "") {
        $("#IDTeam").empty();
        $("#IDTeam").append('<option value=""> -- Select -- </option>');
    }
    else {
        $.getJSON("/Employee/FetchTeamsByBusinessUnitSelected", { id: $(this).val() }, function (teams) {
            $("#IDTeam").empty();
            $("#IDTeam").append('<option value=""> -- Select -- </option>');
            for (var teamKey in teams) {
                $("#IDTeam").append('<option value="' + teams[teamKey].IDTeam + '">' + teams[teamKey].Name + '</option>');
            }
        })
    }
})

