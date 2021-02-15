var formValidator = function () {

    var getTotalProject = function () {
        var eid = $('#Employeeid').val();
        if (eid != null) {
            $.ajax({
                type:'get',
                url:'/Home/GetAllProjectCount',
                data:{eid:eid},
                datatype:'json',
                success:function(response){
                    if(response.success==true)
                    {
                        $('#projects').html(response.Data);
                    }
                }
            });
        }
    };

    var getTotalTask=function(){
        var eid = $('#Employeeid').val();
        if (eid != null) {
            $.ajax({
                type: 'get',
                url: '/Home/GetCompletedTaskToday',
                data: { eid: eid },
                datatype: 'json',
                success: function (response) {
                    if (response.success == true) {
                        $('#tasks').html(response.Data);
                    }
                }
            });
        }
    };

    var getTotalPendingBug = function () {
        var eid = $('#Employeeid').val();
        if (eid != null) {
            $.ajax({
                type: 'get',
                url: '/Home/GetAllPendingBugCount',
                data: { eid: eid },
                datatype: 'json',
                success: function (response) {
                    if (response.success == true) {
                        $('#pendingBug').html(response.Data);
                    }
                }
            });
        }
    };

    var getTotalCompletedBug = function () {
        var eid = $('#Employeeid').val();
        if (eid != null) {
            $.ajax({
                type: 'get',
                url: '/Home/GetCompletedBugToday',
                data: { eid: eid },
                datatype: 'json',
                success: function (response) {
                    if (response.success == true) {
                        $('#completeBug').html(response.Data);
                    }
                }
            });
        }
    };

    return {
        init: function () {
            getTotalProject();
            getTotalTask();
            getTotalPendingBug();
            getTotalCompletedBug();
        }
    };
}();
