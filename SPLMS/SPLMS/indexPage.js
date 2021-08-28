$(document).ready(function () {
    $('#userRole').on('change',
        function () {
            if (this.value == 'Teacher') {
                $('#designationColumn').removeClass('hidden');
                $('#stdRoll').addClass('hidden');
                $('#stdBatch').addClass('hidden');
            }
            else if (this.value == 'Student') {
                $('#designationColumn').addClass('hidden');
                $('#stdRoll').removeClass('hidden');
                $('#stdBatch').removeClass('hidden');
            }
        });
    $('#linkClose').click(function () {
        $('#divError').hide('fade');
    });
    $('#linkCloseRegister').click(function () {
        $('#divErrorRegister').hide('fade');
    });
    $('#btnRegister').click(function () {
        $.ajax({
            url: "/api/accounts/create",
            method: "POST",
            data: {
                Username: $('#txtNameRegister').val(),
                Email: $('#txtEmailRegister').val(),
                RoleName: $('#userRole').val(),
                Password: $('#txtPasswordRegister').val(),
                ConfirmPassword: $('#txtConfirmPasswordRegister').val(),
                Designation: $('#teacherDesignation').val(),
                Roll: $('#rollVal').val(),
                Batch: $('#bactchVal').val()
            },
            success: function () {
                $('#successModal').modal('show');
            },
            error: function (jqXHR) {
                $('#divErrorTextRegister').text(jqXHR.responseText);
                $('#divErrorRegister').show('fade');
            }
        });
    });
    $('#btnLogin').click(function () {
        $.ajax({
            url: "/oauth/token",
            method: "POST",
            contentType: 'application/json',
            data: {
                username: $('#txtUsername').val(),
                password: $('#txtPassword').val(),
                grant_type: 'password'
            },
            success: function (response) {
                sessionStorage.setItem('accessToken', response.access_token);
                $.ajax({
                    url: "/api/accounts/user/" + $('#txtUsername').val(),
                    method: "GET",
                    contentType: 'application/json',
                    accept: 'application/json',
                    headers: {
                        'Authorization': ' Bearer ' + sessionStorage.getItem('accessToken')
                    },
                    success: function (response2) {
                        sessionStorage.setItem("userId", response2.id);
                        sessionStorage.setItem("userName", response2.userName);
                        sessionStorage.setItem("userRole", response2.roles[0]);
                        if (response2.roles[0] == "Admin") window.location.href = 'AdminDashboard.html';
                        else if (response2.roles[0] == "Student") window.location.href = 'StudentDashboard.html';
                        else if (response2.roles[0] == "Teacher") window.location.href = 'TeacherDashboard.html';
                        else if (response2.roles[0] == "Manager") window.location.href = 'ManagerDashboard.html';
                    }
                });

            },
            error: function (jqXHR) {
                $('#divErrorText').text(jqXHR.responseText);
                $('#divError').show('fade');
            }
        });
    });

});