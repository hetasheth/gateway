var formValidator = function () {

    var getEmployees = function () {
        $.ajax({
            type: 'get',
            url: '/Home/GetEmpCount',
            datatype: 'json',
            success: function (response) {
                if (response.success == true) {
                    $('#employees').html(response.Data);
                }
            }
        });
    };

    var getTasks = function () {
        $.ajax({
            type: 'get',
            url: '/Home/Tasks',
            datatype: 'json',
            success: function (response) {
                if (response.success == true) {
                    $('#tasks').html(response.Data);
                }
            }
        });
    };

    var getBugs = function () {
        $.ajax({
            type: 'get',
            url: '/Home/ReportedBug',
            datatype: 'json',
            success: function (response) {
                if (response.success == true) {
                    $('#bugs').html(response.Data);
                }
            }
        });
    };

    var reports = function () {
        $('#btnTask').click(function () {
            window.location.href = "/Home/TaskReport";
        });
        $('#btnBug').click(function () {
            window.location.href = "/Home/BugReport";
        });
    };
    return {
        init: function () {
            getEmployees();
            getTasks();
            getBugs();
            reports();
        }
    };
}();
