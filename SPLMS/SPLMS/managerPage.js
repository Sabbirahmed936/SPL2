$(document).ready(function() {
    // if (sessionStorage.getItem('accessToken') == null) {
    //    window.location.href = "index.html";
    // }
    $('#homeLink').on('click',
        function() {
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
        });
    $('#createAC').on('click',
        function() {
            $.ajax({
                url: "/api/calendars/create",
                method: 'POST',
                data: {
                    committeeId: "1234",
                    proposalDate: $('#proposalDate').val(),
                    midtermDate: $('#midtermDate').val(),
                    draftReportSubmission: $('#draftDate').val(),
                    finalReportSubmission: $('#finalReportDate').val(),
                    finalPresentation: $('#finalDate').val(),
                    courseCode: $('#courseCodeRule').val(),
                    year: $('#ruleYear').val()
                },
                success: function() {
                    $.ajax({
                        url: "/api/grouprules/create",
                        method: 'POST',
                        data: {
                            committeeId: "1234",
                            maxNumOfStudentOneCanSupervise: $('#maxStd').val(),
                            numberOfStudentInEachGroup: $('#numOfStd').val(),
                            courseCode: $('#courseCodeRule').val()
                        },
                        success: function() {
                            alert("calendar and rules cretad");
                        },
                        error: function() {
                            alert("error in rules");
                        }
                    });
                },
                error: function (jqXHR) {
                    alert(jqXHR.responseText);
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
            var student1 = $('#std1').val();
            var student2 = $('#std2').val();
            var student1Info, student2Info;
            $.ajax({
                url: "/api/accounts/user/" + student1,
                method: "GET",
                success: function (response) {
                    student1Info = response;
                },
                error: function () {
                    alert("could not find student " + student1);
                }
            });
            $.ajax({
                url: "/api/accounts/user/" + student2,
                method: "GET",
                success: function (response) {
                    student2Info = response;
                },
                error: function () {
                    alert("could not find student " + student2);
                }
            });
            $.ajax({
                url: "/api/groups/create",
                method: "POST",
                data: {
                    groupName: $('#groupName').val(),
                    courseCode: $('#courseCode').val(),
                    year: $('#year').val(),
                    projectTopic: $('#projectTopic').val(),
                    teacherId: $('#supervisor').val(),
                    //studentId1: student1Info.id,
                    //studentId2: student2Info.id
                },
                success: function (response) {
                    $.ajax({
                        url: "/api/student/" + student1Info.id,
                        method: "PUT",
                        data: {
                            groupId: response.id,
                            roll: student1Info.roll,
                            batch: student1Info.batch
                        },
                        success: function () {
                            alert("student one added to group")
                        },
                        error: function () {
                            alert("could not add student one");
                        }
                    });
                    $.ajax({
                        url: "/api/student/" + student2Info.id,
                        method: "PUT",
                        data: {
                            groupId: response.id,
                            roll: student2Info.roll,
                            batch: student2Info.batch
                        },
                        success: function () {
                            alert("student two added to group")
                        },
                        error: function () {
                            alert("could not add student two");
                        }
                    });
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
                        var row = $('<tr><td>' + value.groupId + '</td><td>' + value.teacherId + '</td><td>' + value.feedbacks + '</td></tr>');
                        $('#feedbackTableData').append(row);
                    });
                },
                error: function () {
                    alert("error");
                }
            });
        });
    $('#feedbackBtn').click(function() {
        var groupname = $("#feedbackgroupName").val();
        $.ajax({
            url: "/api/groups/groupname/" + groupname,
            mehtod: "GET",
            contentType: 'application/json',
            accept: 'application/json',
            success: function(response) {
                $.ajax({
                    url: "/api/feedbacks/create",
                    method: "POST",
                    data: {
                        groupId: response.groupId,
                        teacherId: "a58d-ec2cd2386661",
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
            error: function() {
                alert("error in getting group name");
            }
        });
            
    });
    $('#btnLogout').click(function() {
        sessionStorage.removeItem('accessToken');
        window.location.href = "index.html";
    });
});