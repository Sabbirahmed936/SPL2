$(document).ready(function () {
    // if (sessionStorage.getItem('accessToken') == null) {
    //    window.location.href = "index.html";
    // }

    $('#homeLink').on('click',
        function () {
            $('#headerDiv').removeClass('hidden');
            $('#homeLinkItem').addClass('active');
            $('#newCommitteeDiv').addClass('hidden');
            $('#changeAdminDiv').addClass('hidden');
            $('#changeAdminLinkItem').removeClass('active');
            $('#newCommitteeLinkItem').removeClass('active');
        });
    $('#newCommitteeLink').on('click',
        function () {
            $('#headerDiv').addClass('hidden');
            $('#homeLinkItem').removeClass('active');
            $('#newCommitteeDiv').removeClass('hidden');
            $('#newCommitteeLinkItem').addClass('active');
            $('#changeAdminDiv').addClass('hidden');
            $('#changeAdminLinkItem').removeClass('active');
        });

    $('#assignNewCommittee').click(function () {
        var manager = $('#newManagerName').val();
        var courseCode = $('#courseCode').val();
        var year = $('#year').val();
        $.ajax({
            url: "/api/accounts/user/" + manager,
            method: "GET",
            success: function (response) {
                $.ajax({
                    url: "api/committees/create",
                    method: "POST",
                    data: {
                        teacherId: response.id,
                        courseCode: courseCode,
                        year: year
                    },
                    success: function () {
                        alert("committee created.");
                    },
                    error: function () {
                        alert("could not create committee for course " + courseCode + "* " + response.teacherId);
                    },
                });
            },
            error: function () {
                alert("can not get user " + manager);
            },
        });
    });

    $('#changeAdminLink').on('click',
        function () {
            $('#headerDiv').addClass('hidden');
            $('#homeLinkItem').removeClass('active');
            $('#changeAdminDiv').removeClass('hidden');
            $('#changeAdminLinkItem').addClass('active');
            $('#newCommitteeDiv').addClass('hidden');
            $('#newCommitteeLinkItem').removeClass('active');
        });
    //$('#changingAdmin').click(function () {
    //    var adminName = $('#newAdminName').val();
    //    var role1 = [];
    //    role1[0] = "Teacher";
    //    var role2 = [];
    //    role2[0] = "Admin";
    //    $.ajax({
    //        url: "/api/accounts/user/roles/Admin",
    //        method: 'GET',
    //        contentType: 'application/json',
    //        accept: 'application/json',
    //        header: {
    //            authorization: "Bearer " + sessionStorage.getItem("access_token")
    //        },
    //        success: function (response) {
    //            $.ajax({
    //                url: "http://localhost:2189/api/accounts/user/" + response.userId + "/roles",
    //                method: 'PUT',
    //                header: {
    //                    authorization: "Bearer " + sessionStorage.getItem("access_token")
    //                },
    //                data: {
    //                    role1: role1
    //                },
    //                success: function () {
    //                    alert("success");
    //                },
    //                error: function (jqXHR) {
    //                    alert("error in second ajax");
    //                }
    //            });
    //        },
    //        error: function () {
    //            alert("error happened");
    //        }
    //    });
    //});


    $('#btnLogout').click(function () {
        sessionStorage.removeItem('accessToken');
        window.location.href = "index.html";
    });
});