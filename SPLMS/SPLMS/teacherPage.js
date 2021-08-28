
$(document).ready(function() {
    //if (sessionStorage.getItem('accessToken') == null) {
    //    window.location.href = "index.html";
    //}
    $('#homeLink').on('click',
        function () {
            $('#headerDiv').removeClass('hidden');
            $('#homeLinkItem').addClass('active');
            $('#uploadDocDiv').addClass('hidden');

            $('#academicCalDiv').addClass('hidden');
            $('#academicCalLinkItem').addClass('active');
            $('#groupsDiv').addClass('hidden');
            $('#createGroupDiv').addClass('hidden');
            $('#changeRulesDiv').addClass('hidden');
            $('#groupsLinkItem').removeClass('active');
            $('#feedbackDiv').addClass('hidden');
            $('#feedbackLinkItem').removeClass('active');
            $('#academicCalLinkItem').removeClass('active');
            $('#uploadDocLinkItem').removeClass('active');
            $('#createGroupLinkItem').removeClass('active');
            $('#changeRuleLinkItem').removeClass('active');
        });
    $('#academicCalLink').on('click',
        function() {
            $('#headerDiv').addClass('hidden');
            $('#homeLinkItem').removeClass('active');
            $('#uploadDocDiv').addClass('hidden');
            $('#academicCalDiv').removeClass('hidden');
            $('#academicCalLinkItem').addClass('active');
            $('#groupsDiv').addClass('hidden');
            $('#createGroupDiv').addClass('hidden');
            $('#changeRulesDiv').addClass('hidden');
            $('#feedbackDiv').addClass('hidden');
            $('#feedbackLinkItem').removeClass('active');
            $('#groupsLinkItem').removeClass('active');
            $('#uploadDocLinkItem').removeClass('active');
            $('#createGroupLinkItem').removeClass('active');
            $('#changeRuleLinkItem').removeClass('active');
            $.ajax({
                url: "api/calendars/all",
                method: "GET",
                contentType: 'application/json',
                success: function(data) {
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
            $('#academicCalDiv').addClass('hidden');
            $('#groupsDiv').addClass('hidden');
            $('#createGroupDiv').addClass('hidden');
            $('#changeRulesDiv').addClass('hidden');
            $('#feedbackDiv').addClass('hidden');
            $('#feedbackLinkItem').removeClass('active');
            $('#groupsLinkItem').removeClass('active');
            $('#academicCalLinkItem').removeClass('active');
            $('#createGroupLinkItem').removeClass('active');
            $('#changeRuleLinkItem').removeClass('active');

        });
    $('#uploadFileBtn').click(function() {
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
    $('#linkCloseFileUpload').click(function() {
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
            $('#createGroupDiv').addClass('hidden');
            $('#changeRulesDiv').addClass('hidden');
            $('#feedbackDiv').addClass('hidden');
            $('#feedbackLinkItem').removeClass('active');
            $('#uploadDocLinkItem').removeClass('active');
            $('#academicCalLinkItem').removeClass('active');
            $('#createGroupLinkItem').removeClass('active');
            $('#changeRuleLinkItem').removeClass('active');
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
    $('#createGroupLink').on('click',
        function () {
            $('#headerDiv').addClass('hidden');
            $('#homeLinkItem').removeClass('active');
            $('#uploadDocDiv').addClass('hidden');
            $('#academicCalDiv').addClass('hidden');
            $('#groupsDiv').addClass('hidden');
            $('#createGroupDiv').removeClass('hidden');
            $('#createGroupLinkItem').addClass('active');
            $('#changeRulesDiv').addClass('hidden');
            $('#feedbackDiv').addClass('hidden');
            $('#feedbackLinkItem').removeClass('active');
            $('#uploadDocLinkItem').removeClass('active');
            $('#academicCalLinkItem').removeClass('active');
            $('#groupsLinkItem').removeClass('active');
            $('#changeRuleLinkItem').removeClass('active');
        });
    $('#createGroup').on('click',
        function () {
            $.ajax({
                url: "/api/groups/create",
                method: "POST",
                data: {
                    groupName: $('#groupName').val(),
                    courseCode: $('#courseCode').val(),
                    year: $('#year').val(),
                    projectTopic: $('#projectTopic').val(),
                    teacherId: $('#supervisor').val()
                },
                success: function () {
                    alert("group created successfully!");
                },
                error: function (jqXHR) {
                    alert(jqXHR.responseText);
                }
            });
        });

    $('#feedbackLink').on('click',
        function () {
            $('#headerDiv').addClass('hidden');
            $('#homeLinkItem').removeClass('active');
            $('#uploadDocDiv').addClass('hidden');
            $('#academicCalDiv').addClass('hidden');
            $('#groupsDiv').addClass('hidden');
            $('#createGroupDiv').addClass('hidden');
            $('#createGroupLinkItem').removeClass('active');
            $('#changeRulesDiv').addClass('hidden');
            $('#feedbackDiv').removeClass('hidden');
            $('#feedbackLinkItem').addClass('active');
            $('#uploadDocLinkItem').removeClass('active');
            $('#academicCalLinkItem').removeClass('active');
            $('#groupsLinkItem').removeClass('active');
            $('#changeRuleLinkItem').removeClass('active');
            $.ajax({
                url: "/api/feedbacks/all",
                method: "GET",
                contentType: 'application/json',
                headers: {
                    'Authorization': ' Bearer ' + sessionStorage.getItem('accessToken')
                },
                success: function (data) {
                    $('#feedbackTableBody').empty();
                        $.each(data, function (index, value) {
                        var row = $('<tr><td>' + value.groupName + '</td><td>' + value.teacherId + '</td><td>' + value.feedbacks + '</td></tr>');
                        $('#feedbackTableData').append(row);
                    });
                },
                error: function () {
                    alert("error");
                }
            });
        });
    $('#feedbackBtn').click(function () {
        var groupname = $("#feedbackgroupName").val();
        $.ajax({
            url: "/api/groups/groupname/" + groupname,
            mehtod: "GET",
            contentType: 'application/json',
            accept: 'application/json',
            success: function (response) {
                $.ajax({
                    url: "/api/feedbacks/create",
                    method: "POST",
                    data: {
                        groupId: response.groupId,
                        teacherId: "a58d-ec2cd2386661",//sessionStorage.getItem("userId"),
                        feedbacks: $('#feedbacks').val()
                    },
                    success: function () {
                        alert("feedback submitted");
                    },
                    error: function () {
                        alert(jqXHR.responseText);
                    }
                });
            },
            error: function () {
                alert("error in getting group name");
            }
        });

    });
    $('#btnLogout').click(function() {
        sessionStorage.removeItem('accessToken');
        window.location.href = "index.html";
    });
});