var formValidator = function () {

    var bugstatusId = getParameterByName('id');
    var projectID;
    var bugId;

    var btnhide = function () {
        $(document).ready(function () {
            $('#btnComplete').hide();
            $('#btnResolve').hide();
            $('#btnClose').hide();
            if (bugstatusId != null) {
                $.ajax({
                    type: 'get',
                    url: '/BugStatus/LoadBugStatusById',
                    data: { id: bugstatusId },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            bugId = data.Bug_id;
                            loadBugDataById(bugId);
                            $('#Status').val(data.Status);
                            $('#Remark').val(data.Remark);
                            var pa = $('#Admin').val();
                            var pt = $('#Tester').val();
                            var st = $('#Status').val();
                            bugStatus(st,pa,pt);
                        }
                    }
                });
            }
            
        });
    };

    var loadBugDataById = function (bugId) {
        try {
            if (bugId != null) {
                $.ajax({
                    type: 'get',
                    url: '/Bug/LoadBugById',
                    data: { id: bugId },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            $('#Project_id').val(data.Project_id);
                            projectID = data.Project_id;
                            loadProject(projectID);
                            $('#Task_id').val(data.Task_id);
                            var taskID = data.Task_id;
                            loadTask(taskID);
                            $('#Bug_description').val(data.Bug_description);
                            $('#Bug_date').val(data.Bug_date);
                            var empID=data.Emp_id;
                            loadEmp(empID);
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

    var loadTask = function (taskID) {
        $.ajax({
            type: 'get',
            url: '/Task/LoadTaskById',
            data: { id: taskID },
            datatype: 'json',
            success: function (response) {
                if (response.success == true) {
                    var data = response.Data;
                    $('#Task_name').val(data.Task_name);
                }
            }
        });
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

    var loadEmp = function (empID) {
        $.ajax({
            type: 'get',
            url: '/Employee/LoadEmployeeById',
            data: { id: empID },
            datatype: 'json',
            success: function (response) {
                if (response.success == true) {
                    var data = response.Data;
                    var name = data.First_name + " " + data.Last_name;
                    $('#Employee_name').val(name);
                }
            }
        });
    };

    var bugStatus = function (st, pa,pt) {
        if ((st == "Complete" && pt == 'T') || (st == "Complete" && pa == 'T')) {
            $('#btnResolve').show();
            $('#btnClose').show();
            $('#Remark').val("");
        }
        else
        {
            $("#Remark").attr("disabled", "disabled");
        }
        if ((st == "Pending" && pt == 'F') || (st == "Pending" && pa == 'T')) {
            $('#btnComplete').show();
            $('#Remark').val("");
        }
        if(st=="Close")
        {
            $("#Remark").attr("disabled", "disabled");
        }
        
    };

    var resolveClick = function () {
        try {
            $('#btnResolve').click(function () {
                var remark = $('#Remark').val();
                if (bugId != null) {
                    $.ajax({
                        type: 'post',
                        url: '/BugStatus/Resolve',
                        data: { id: bugId ,r:remark},
                        datatype: 'json',
                        success: function (response) {
                            if (response.success == true) {
                                window.location.href = "/BugStatus/Index?projectid=" + projectID;
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
    }

    var completeClick = function () {
        try {
            $('#btnComplete').click(function () {
                if (bugId != null) {
                    var remark = $('#Remark').val();
                    var status = "Complete";
                    $.ajax({
                        type: 'post',
                        url: '/BugStatus/CompleteOrClose',
                        data: { id: bugId, sts:status, r:remark},
                        datatype: 'json',
                        success: function (response) {
                            if (response.success == true) {
                                window.location.href = "/BugStatus/Index?projectid=" + projectID;
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
    }

    var closeClick = function () {
        try {
            $('#btnClose').click(function () {
                if (bugId != null) {
                    var remark = $('#Remark').val();
                    var status = "Close";
                    $.ajax({
                        type: 'post',
                        url: '/BugStatus/CompleteOrClose',
                        data: { id: bugId, sts: status ,r:remark},
                        datatype: 'json',
                        success: function (response) {
                            if (response.success == true) {
                                window.location.href = "/BugStatus/Index?projectid=" + projectID;
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
    }

    var backToTask = function () {
        try {
            $('#btnBack').click(function () {
                var Id = projectID ;
                if (Id != null) {
                    window.location.href = "/BugStatus/Index?projectid=" + Id;
                }
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    return {
        init: function () {
            btnhide();
            loadBugDataById();
            loadTask();
            loadEmp();
            loadProject();
            resolveClick();
            completeClick();
            closeClick();
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