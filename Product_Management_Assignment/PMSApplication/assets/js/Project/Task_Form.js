var formValidator = function () {

    var projectId = getParameterByName('pid');

    var pathnames = projectId;
    var path = pathnames.split("?");
    var proId = path[0];
    var dummy = path[1];
    var id = pathnames.split("=");
    var taskId = id[1];

    var runValidator = function () {
        try {
            var form1 = $('#form');
            var errorHandler1 = $('.errorHandler', form1);
            var valid = $('#form').validate({
                errorElement: "span",
                errorClass: "help-block",
                ignor: "",
                rules: {
                    Task_name:{
                        required:true
                    },
                    Start_date: {
                        required: true
                    },
                    End_date: {
                        required: true
                    },
                    Date_created: {
                        required: true
                    },
                    Last_update: {
                        required: true
                    }
                },
                messages: {
                    Task_name:{
                        required : "Enter Task"
                    },
                    Start_date: {
                        required: "Enter Start Date"
                    },
                    End_date: {
                        required: "Enter End Date"
                    },
                    Date_created: {
                        required: "Enter Created Date"
                    },
                    Last_update: {
                        required: "Enter Last Update Date"
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
                    var tid = $('#Task_id').val();
                    var planid = $('#Plan_id').val();
                    var tname = $('#Task_name').val();
                    var sdate = $('#Start_date').val();
                    var edate = $('#End_date').val();
                    var prio = $('#Priority').val();
                    var status = "Pending";
                    var dc = $('#Date_created').val();
                    var lu = $('#Last_update').val();
                    var estimate = $('#Estimate_time').val();
                    var formData = { Task_id: tid, Plan_id: planid, Task_name: tname, Start_date: sdate, End_date: edate, Priority: prio, Status: status, Date_created: dc, Last_update: lu, Estimate_time: estimate };
                    var things = JSON.stringify(formData);
                    try {
                        $.ajax({
                            type: 'post',
                            url: '/Task/Save',
                            data: formData,
                            datatype: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    window.location.href = "/Task/Index?projectid=" + proId;
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
        $("#Start_date").datepicker({
            format: 'dd-M-yyyy',
            startDate: new Date(),
            autoclose: true
        }).on('change', function () {
            var sd = Date.parse($('#Start_date').val());
            var ed = Date.parse($('#End_date').val());
            if (ed != NaN) {
                var estimate = ed - sd;
                var days = Math.floor(((estimate % 31536000000) % 2628000000) / 86400000);
                $('#Estimate_time').val(days + " days");
            }
        });
        $("#End_date").datepicker({
            format: 'dd-M-yyyy',
            startDate: new Date(),
            autoclose: true
        }).on('change', function () {
            var sd = Date.parse($('#Start_date').val());
            var ed = Date.parse($('#End_date').val());
            if (sd != NaN) {
                var estimate = ed - sd;
                var days = Math.floor(((estimate % 31536000000) % 2628000000) / 86400000);
                $('#Estimate_time').val(days+" days");
            }
        });
        $("#Date_created").datepicker({
            format: 'dd-M-yyyy',
            autoclose: true
        });
        $("#Last_update").datepicker({
            format: 'dd-M-yyyy',
            autoclose: true
        });
    }

    var loadPlan = function () {
        try {
            if (proId != null) {
                $.ajax({
                    type: 'get',
                    url: '/Task/LoadPlanId',
                    data: { proid: proId },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            $('#Plan_id').val(data);
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
                            $('#Task_id').val(data.Task_id);
                            $('#Plan_id').val(data.Plan_id);
                            $('#Task_name').val(data.Task_name);
                            $('#Start_date').val(data.Start_date);
                            $('#End_date').val(data.End_date);
                            //$('#Priority:selected').val(data.Priority);
                            $("#Priority option[value='"+ data.Priority +"']").prop('selected', true);
                            $('#Date_created').val(data.Date_created);
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


    var ClearButton = function () {
        $("#btnClear").click(function () {
            $('#Plan_id').val('').selectpicker("refresh");
            $('#Task_name').val("");
            $('#Start_date').val("");
            $('#End_date').val("");
            $('#Priority').val("");
            $('#Date_created').val("");
            $('#Last_update').val("");
            $('#Estimate_time').val("");
            $(".errorHandler").hide();
            $("div").removeClass("has-error");
            $(".help-block").hide();
        });
    };

    return {
        init: function () {
            ClearButton();
            runValidator();
            addDatePicker();
            loadPlan();
            loadTaskDataById();
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