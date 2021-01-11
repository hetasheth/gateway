var formValidator = function () {

    var taskId = getParameterByName('id');
    var bugId = getParameterByName('bugid');

    if (bugId == null || bugId == undefined)
    {
        bugId = 0;
    }


    var runValidator = function () {
        try {
            var form1 = $('#form');
            var errorHandler1 = $('.errorHandler', form1);
            var valid = $('#form').validate({
                errorElement: "span",
                errorClass: "help-block",
                ignor: "",
                rules: {
                    Bug_description: {
                        required: true
                    }                    
                },
                messages: {
                    Bug_description: {
                        required: "Enter Bug"
                    }
                },
                invalidHandler: function (event, validator) {
                    errorHandler1.show();
                },
                highlight: function (element) {
                    $(element).closest('.help-block').removeClass('valid');
                    // display OK icon
                    $(element).closest('.form-line').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
                    // add the Bootstrap error class to the control group
                },
                unhighlight: function (element) { // revert the change done by hightlight
                    $(element).closest('.form-line').removeClass('has-error');
                    // set error class to the control group
                },
                submitHandler: function (form1) {
                    var bid = bugId;
                    var bdes = $('#Bug_description').val();
                    var tid = $('#Task_id').val();
                    var proid = $('#Project_id').val();
                    var eid=$('#Emp_id').val();
                    var formData = { Bug_id: bid, Bug_description: bdes,Task_id: tid, Project_id: proid, Emp_id: eid };
                    var things = JSON.stringify(formData);
                    try {
                        $.ajax({
                            type: 'post',
                            url: '/Bug/Save',
                            data: formData,
                            datatype: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    window.location.href = "/BugStatus/Index?projectid=" + proid;
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
                }
            });
        }
        catch (err) {
            console.log(err);
        }
    }

    var addDatePicker = function () {
        $("#Bug_date").datepicker({
            format: 'dd-M-yyyy',
            startDate: new Date()
        });
        
    }


    var loadBug = function () {
        try {
            if(bugId!=null)
            {
                $.ajax({
                    type: 'get',
                    url: '/Bug/LoadBugById',
                    data: { id: bugId },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            $('#Task_id').val(data.Task_id);
                            taskId = data.Task_id;
                            loadTaskDataById(taskId);
                            $('#Bug_description').val(data.Bug_description);
                            $('#Bug_date').val(data.Bug_date);
                        }
                    },
                });
            }
        }
        catch (err)
        {
            console.log(err);
        }
    };

    var loadTaskDataById = function () {
        try {
            if (taskId != null) {
                $.ajax({
                    type: 'get',
                    url: '/Task/LoadTaskById',
                    data: { id: taskId },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            $('#Project_id').val(data.Project_id);
                            var projectID = data.Project_id;
                            loadProject(projectID);
                            $('#Task_id').val(data.Task_id);
                            $('#Task_name').val(data.Task_name);
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

    var loadEmpId = function () {
        try {
            if (taskId != null) {
                $.ajax({
                    type: 'get',
                    url: '/Task/LoadExe',
                    data: { id: taskId },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            $('#Emp_id').val(data);
                            loadEmp(data);
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

    var loadEmp = function (empid) {
        $.ajax({
            type: 'get',
            url: '/Employee/LoadEmployeeById',
            data: { id: empid },
            datatype: 'json',
            success: function (response) {
                if (response.success == true) {
                    var data = response.Data;
                    $('#Emp_name').val(data.First_name+" "+data.Last_name);
                }
            }
        });
    };

    var backToTask = function () {
        try {
            $('#btnBack').click(function () {
                var Id = $('#Project_id').val();
                if (Id != null) {
                    window.location.href = "/BugStatus/Index?projectid=" + Id;
                }
            });
            $('#btnCancel').click(function () {
                var Id = $('#Project_id').val();
                if (Id != null) {
                    window.location.href = "/BugStatus/Index?projectid=" + Id;
                }
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    var ClearButton = function () {
        $("#btnClear").click(function () {
            $('#Bug_description').val("");
            $('#Bug_date').val("");
            $(".errorHandler").hide();
            $("div").removeClass("has-error");
            $(".help-block").hide();
        });
    };

    return {
        init: function () {
            runValidator();
            addDatePicker();
            loadBug();
            loadEmpId();
            loadEmp();
            loadTaskDataById();
            backToTask();
            ClearButton();
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