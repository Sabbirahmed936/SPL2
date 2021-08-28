$(document).ready(function () {
    // if (sessionStorage.getItem('accessToken') == null) {
    //    window.location.href = "index.html";
    // }
    //if (sessionStorage.getItem("userRole") != "Student") {
    //    window.location.href = "index.html";
    //}
    $('#homeLink').on('click',
        function () {
            $('#headerDiv').removeClass('hidden');
            $('#homeLinkItem').addClass('active');
            $('#uploadDocDiv').addClass('hidden');
            $('#myGroupDiv').addClass('hidden');
            $('#myGroupLinkItem').removeClass('active');
            $('#academicCalDiv').addClass('hidden');
            $('#academicCalLinkItem').addClass('active');
            $('#groupsDiv').addClass('hidden');
            $('#groupsLinkItem').removeClass('active');
            $('#academicCalLinkItem').removeClass('active');
            $('#uploadDocLinkItem').removeClass('active');
        });
    $('#academicCalLink').on('click',
        function () {
            $('#headerDiv').addClass('hidden');
            $('#homeLinkItem').removeClass('active');
            $('#uploadDocDiv').addClass('hidden');
            $('#academicCalDiv').removeClass('hidden');
            $('#academicCalLinkItem').addClass('active');
            $('#myGroupDiv').addClass('hidden');
            $('#myGroupLinkItem').removeClass('active');
            $('#groupsDiv').addClass('hidden');
            $('#groupsLinkItem').removeClass('active');
            $('#uploadDocLinkItem').removeClass('active');
            $.ajax({
                url: "api/calendars/all",
                method: "GET",
                contentType: 'application/json',
                success: function (data) {
                    $('#calendarTableBody').empty();
                    $.each(data, function (index, value) {
                        var row = $('<tr><td>' + value.courseCode + '</td><td>' + value.year + '</td><td>' + value.proposalDate + '</td><td>' + value.midtermDate + '</td><td>' + value.draftReportSubmission + '</td><td>' + value.finalReportSubmission + '</td><td>' + value.finalPresentation + '</td></tr>');
                        $('#calendarTableData').append(row);
                    });
                }
            });
        });
    $('#uploadDocLink').on('click',
        function () {
            $('#headerDiv').addClass('hidden');
            $('#homeLinkItem').removeClass('active');
            $('#uploadDocDiv').removeClass('hidden');
            $('#uploadDocLinkItem').addClass('active');
            $('#myGroupDiv').addClass('hidden');
            $('#myGroupLinkItem').removeClass('active');
            $('#academicCalDiv').addClass('hidden');
            $('#groupsDiv').addClass('hidden');
            $('#groupsLinkItem').removeClass('active');
            $('#academicCalLinkItem').removeClass('active');

        });
    $('#uploadFileBtn').click(function () {
        $.ajax({
            url: "/api/file/uploadfile",
            method: "POST",
            data: {
                document: $('#uploadFile').val()
            },
            success: function () {
                $('#successModal').show('fade');
            },
            error: function (jqXHR) {
                $('#divErrorTextFileUpload').text(jqXHR.responseText);
                $('#divErrorFileUpload').show('fade');
            }
        });
    });
    $('#linkCloseFileUpload').click(function () {
        $('#divErrorFileUpload').hide('fade');
    });
    $('#groupsLink').on('click',
        function () {
            $('#headerDiv').addClass('hidden');
            $('#homeLinkItem').removeClass('active');
            $('#uploadDocDiv').addClass('hidden');
            $('#academicCalDiv').addClass('hidden');
            $('#groupsDiv').removeClass('hidden');
            $('#groupsLinkItem').addClass('active');
            $('#myGroupDiv').addClass('hidden');
            $('#myGroupLinkItem').removeClass('active');
            $('#uploadDocLinkItem').removeClass('active');
            $('#academicCalLinkItem').removeClass('active');
            $.ajax({
                url: "/api/groups/all",
                method: "GET",
                contentType: 'application/json',
                headers: {
                    'Authorization': ' Bearer ' + sessionStorage.getItem('accessToken')
                },
                success: function (data) {
                    $('#tblBody').empty();
                    $.each(data, function (index, value) {
                        var groupName;
                        $.ajax({
                            url: "/api/groups/group/" + value.groupId,
                            method: "GET",
                            headers: {
                                'Authorization': ' Bearer ' + sessionStorage.getItem('accessToken')
                            },
                            success: function (data) {
                                var row = $('<tr><td>' + data.groupName + '</td><td>' + value.courseCode + '</td><td>' + value.projectTopic + '</td><td>' + value.teacherId + '</td><td>' + value.year + '</td></tr>');
                                $('#tblData').append(row);
                            },
                            error: function () {
                                alert("error getting group name");
                            }
                        });
                    });
                },
                error: function () {
                    alert("error");
                }
            });
        });
    $('#myGroupLink').click(function () {
        $('#headerDiv').addClass('hidden');
        $('#homeLinkItem').removeClass('active');
        $('#uploadDocDiv').addClass('hidden');
        $('#academicCalDiv').addClass('hidden');
        $('#groupsDiv').addClass('hidden');
        $('#groupsLinkItem').removeClass('active');
        $('#myGroupDiv').removeClass('hidden');
        $('#myGroupLinkItem').addClass('active');
        $('#uploadDocLinkItem').removeClass('active');
        $('#academicCalLinkItem').removeClass('active');
        var userId = sessionStorage.getItem("userId");
        $.ajax({
            url: "/api/accounts/user/student/" + userId,
            method: "GET",
            contentType: 'application/json',
            success: function (response) {
                $.ajax({
                    url: "/api/feedbacks/group/" + response.groupId,
                    method: "GET",
                    contentType: 'application/json',
                    success: function (data) {
                        $('#feedbackTableBody').empty();
                        $.each(data, function (index, value) {
                            var row = $('<tr><td>' + value.teacherId + '</td><td>' + value.feedbacks + '</td></tr>');
                            $('#feedbackTableData').append(row);
                        });
                    },
                    error: function () {
                        alert("error getting feedback");
                    }
                });
                $.ajax({
                    url: "/api/groups/group/" + response.groupId,
                    method: "GET",
                    contentType: 'application/json',
                    success: function (response2) {
                        $('#myGroupTableBody').empty();
                        var row = $('<tr><td>' + value.groupName + '</td><td>' + value.projectTopic + '</td><td>' + value.teacherId + '</td></tr>');
                        $('#myGroupTableData').append(row);
                    },
                    error: function () {
                        alert("error getting group");
                    }
                });

            },
            error: function () {
                alert("error getting user id");
            }
        });
    });

    $('#btnLogout').click(function () {
        sessionStorage.removeItem('accessToken');
        window.location.href = "index.html";
    });
});