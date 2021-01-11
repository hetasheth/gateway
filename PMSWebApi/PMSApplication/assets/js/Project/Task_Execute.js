var formValidator = function () {

    var Id = getParameterByName('id');

    var getData = function () {
        try {
            $.ajax({
                type: 'post',
                url: '/Execution/LoadTask',
                data: { id: Id },
                datatype: 'json',
                success: function (response) {
                    if (response.success == true) {
                        fillData(response.Data);
                    }
                    else {
                        alert("Error");
                    }
                },
                error: function (ex) {
                    alert(ex);
                }
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    function fillData(data) {
        try {
            var arrayReturn = [];
            for (var i = 0, len = data.length; i < len; i++) {
                var result = data[i];

                arrayReturn.push([result.Start_date, result.End_date, result.Status, result.Remark]);
            }

            var Table = $('#myTable').dataTable({
                "aLengthMenu": [[10, 20, 50, -1], [10, 20, 50, "All"]],
                "oLanguage": {
                    "sLengthMenu": "Show _MENU_ Rows",
                    "sSearchPlaceholder": "Search",
                    "oPaginate": {
                        "sPrevious": "",
                        "sNext": ""
                    }
                },
                "bAutoWidth": true,
                "aaData": arrayReturn,
                "bProcessing": true,
                "aoColumns": [
                    { "sTitle": "Start Date", "bSortable": true },
                    { "sTitle": "End date", "bSortable": true },
                    { "sTitle": "Status", "bSortable": true },
                    { "sTitle": "Remark", "bSortable": true }
                ],
                "bDestroy": true,
                "paging": false,
                "info": true
            });
        }
        catch (err) {
            console.log(err);
        }
    }

    var loadTaskDataById = function () {
        try {
            if (Id != null) {
                $.ajax({
                    type: 'get',
                    url: '/Task/LoadTaskById',
                    data: { id: Id },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            $('#Project').val(data.Project_id);
                            projectID = data.Project_id;
                            loadProject(projectID);
                            $('#Task_id').val(data.Task_id);
                            $('#Task_name').val(data.Task_name);
                            $('#Start_date').val(data.Start_date);
                            $('#End_date').val(data.End_date);
                            $('#Priority').val(data.Priority);
                            $('#Status').val(data.Status);
                            $('#Last_update').val(data.Last_update);
                            $('#Estimate_time').val(data.Estimate_time);
                        }
                    },
                    error: function (ex) {
                        alert(ex);
                    }
                });
            }
        }
        catch (err) {
            console.log(err);
        }
    };

    var loadExecutionById = function () {
        try {
            if (Id != null) {
                $.ajax({
                    type: 'get',
                    url: '/Execution/LoadExecutionById',
                    data: { id: Id },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            $('#btnStart').hide();
                            $('#divRemark').show();
                        }
                    },
                    error: function (ex) {
                        alert(ex);
                    }
                });
            }
        }
        catch (err) {
            console.log(err);
        }
    };

    var loadProject = function (projectID) {
        $.ajax({
            type: 'get',
            url: '/Project/LoadProjectById',
            data: { id: projectID },
            datatype: 'json',
            success: function (response) {
                if (response.success == true) {
                    var data = response.Data;
                    $('#Project_name').val(data.Project_name);
                }
            }
        });
    };

    var addExecution = function () {
        try {
            $('#btnStart').click(function () {
                if (Id != null) {
                    $.ajax({
                        type: 'post',
                        url: '/Execution/Start',
                        data: { id: Id },
                        datatype: 'json',
                        success: function (response) {
                            if (response.success == true) {
                                window.location.href = "/Task/Execute?id=" + Id;
                            }
                        },
                        error: function (ex) {
                            alert(ex);
                        }
                    });
                }
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    var updExecution = function () {
        try {
            $('#btnEnd').click(function () {
                var remark = $('#Remark').val();
                if (Id != null) {
                    $.ajax({
                        type: 'post',
                        url: '/Execution/End',
                        data: { id: Id, r: remark },
                        datatype: 'json',
                        success: function (response) {
                            debugger;
                            if (response.success == true) {
                                window.location.href = "/Task/Execute?id=" + Id;
                            }
                        },
                        error: function (ex) {
                            alert(ex);
                        }
                    });
                }
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    var backToTask = function () {
        try {
            $('#btnBack').click(function () {
                var Id=$('#Project').val();
                if (Id != null) {
                    window.location.href = "/Task/Index?projectid=" + Id;
                }
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    return {
        init: function () {
            getData();
            loadTaskDataById();
            loadExecutionById();
            addExecution();
            updExecution();
            backToTask();
        }
    }
}();

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}