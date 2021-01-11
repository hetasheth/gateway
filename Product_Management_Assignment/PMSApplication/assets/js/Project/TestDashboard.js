var formValidator = function () {

    var getTasks = function () {
        $.ajax({
            type: 'get',
            url: '/Home/Tasks',
            datatype: 'json',
            success: function (response) {
                if (response.success == true) {
                    $('#completeTask').html(response.Data);
                }
            }
        });
    };

    var getBugReport = function () {
        $.ajax({
            type: 'get',
            url: '/Home/ReportedBug',
            datatype: 'json',
            success: function (response) {
                if (response.success == true) {
                    $('#totalBug').html(response.Data);
                }
            }
        });
    };

    var getBugClose = function () {
        $.ajax({
            type: 'get',
            url: '/Home/ClosedBug',
            datatype: 'json',
            success: function (response) {
                if (response.success == true) {
                    $('#closeBug').html(response.Data);
                }
            }
        });
    };

    return {
        init: function () {
            getTasks();
            getBugReport();
            getBugClose();
        }
    };
}();
