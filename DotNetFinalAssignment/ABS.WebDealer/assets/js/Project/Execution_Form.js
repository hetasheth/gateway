var formValidator = function () {

    var exeId = getParameterByName('id');

    var runValidator = function () {
        try {
            var form1 = $('#form');
            var errorHandler1 = $('.errorHandler', form1);
            var valid = $('#form').validate({
                errorElement: "span",
                errorClass: "help-block",
                ignor: "",
                rules: {
                    Task_id:{
                        required:true
                    },
                    Plan_id: {
                        required: true
                    },
                    Start_date: {
                        required: true
                    },
                    End_date: {
                        required: true
                    },
                    Emp_id: {
                        required:true
                    }
                },
                messages: {
                    Task_id: {
                        required: "Choose Task"
                    },
                    Plan_id: {
                        required: "Choose Plan"
                    },
                    Start_date: {
                        required: "Enter start date"
                    },
                    End_date: {
                        required: "Enter end date"
                    },
                    Emp_id: {
                        required: "Choose Employee"
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
                    var exeid = $('#Execution_id').val();
                    var tid = $('#Task_id').val();
                    var pid = $('#Plan_id').val();
                    var sdate = $('#Start_date').val();
                    var edate = $('#End_date').val();
                    var empid = $('#Emp_id').val();
                    var formData = { Execution_id: exeid, Task_id: tid, Plan_id: pid, Start_date: sdate, End_date: edate, Emp_id:empid };
                    var things = JSON.stringify(formData);
                    try {
                        $.ajax({
                            type: 'post',
                            url: '/Execution/Save',
                            data: formData,
                            datatype: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    window.location.href = "/Execution/Index";
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
            startDate: new Date()
        });
        $("#End_date").datepicker({
            format: 'dd-M-yyyy',
            startDate: new Date()
        });
    }

    var dropdownValidation = function () {
        $("#Task_id").on('change', function (event) {
            if ($("#Task_id").val() != "") {
                $(this).parent().closest(".has-error").removeClass("has-error").addClass("has-success").find(".help-block").hide().end().find('.symbol').addClass('ok');
            }
        });
        $("#Plan_id").on('change', function (event) {
            if ($("#Plan_id").val() != "") {
                $(this).parent().closest(".has-error").removeClass("has-error").addClass("has-success").find(".help-block").hide().end().find('.symbol').addClass('ok');
            }
        });
        $("#Emp_id").on('change', function (event) {
            if ($("#Emp_id").val() != "") {
                $(this).parent().closest(".has-error").removeClass("has-error").addClass("has-success").find(".help-block").hide().end().find('.symbol').addClass('ok');
            }
        });
    }

    var loadExecutionDataById = function () {
        try {
            if (exeId != null) {
                $.ajax({
                    type: 'get',
                    url: '/Execution/LoadExecutionById',
                    data: { id: exeId },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            $('#Execution_id').val(data.Execution_id);
                            setTimeout(function () {
                                $('#Task_id').val(data.Task_id);
                                $('#Task_id').selectpicker('refresh');
                            }, 500);
                            setTimeout(function () {
                                $('#Plan_id').val(data.Plan_id);
                                $('#Plan_id').selectpicker('refresh');
                            }, 500);
                            $('#Start_date').val(data.Start_date);
                            $('#End_date').val(data.End_date);
                            setTimeout(function () {
                                $('#Emp_id').val(data.Emp_id);
                                $('#Emp_id').selectpicker('refresh');
                            }, 500);
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
    }

    var loadTask = function () {
        try {
            $.ajax({
                type: 'post',
                url: '/Task/loadTask',
                datatype: 'json',
                success: function (response) {
                    if (response.success == true && response.Data.length > 0) {
                        var lst = response.Data;
                        var items = '<option value="" disabled selected>Select Task </option>';

                        if (lst.length > 0) {
                            $.each(lst, function (i, user) {
                                items += "<option value='" + user.Task_id + "'>" + user.Task_name + "</option>";
                            });
                            $('#Task_id').html(items);
                            $('#Task_id').selectpicker();
                        }
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

    var loadPlan = function () {
        try {
            $.ajax({
                type: 'post',
                url: '/Plan/loadPlan',
                datatype: 'json',
                success: function (response) {
                    if (response.success == true && response.Data.length > 0) {
                        var lst = response.Data;
                        var items = '<option value="" disabled selected>Select Plan </option>';

                        if (lst.length > 0) {
                            $.each(lst, function (i, user) {
                                items += "<option value='" + user.Plan_id + "'>" + user.Plan_id + "</option>";
                            });
                            $('#Plan_id').html(items);
                            $('#Plan_id').selectpicker();
                        }
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

    var loadEmp = function () {
        try {
            $.ajax({
                type: 'post',
                url: '/Employee/loadEmployee',
                datatype: 'json',
                success: function (response) {
                    if (response.success == true && response.Data.length > 0) {
                        var lst = response.Data;
                        var items = '<option value="" disabled selected>Select Employee </option>';

                        if (lst.length > 0) {
                            $.each(lst, function (i, user) {
                                items += "<option value='" + user.Emp_id + "'>" + user.First_name + "</option>";
                            });
                            $('#Emp_id').html(items);
                            $('#Emp_id').selectpicker();
                        }
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

    var ClearButton = function () {
        $("#btnClear").click(function () {
            $('#Execution_id').val("");
            $('#Task_id').val('').selectpicker("refresh");
            $('#Plan_id').val('').selectpicker("refresh");
            $('#Start_date').val("");
            $('#End_date').val("");
            $('#Emp_id').val('').selectpicker("refresh");
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
            dropdownValidation();
            loadExecutionDataById();
            loadPlan();
            loadTask();
            loadEmp();
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