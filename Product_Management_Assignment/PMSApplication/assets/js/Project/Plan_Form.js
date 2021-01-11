var formValidator = function () {

    var planId = getParameterByName('id');
    var pid;

    var runValidator = function () {
        try {
            var form1 = $('#form');
            var errorHandler1 = $('.errorHandler', form1);
            var valid = $('#form').validate({
                errorElement: "span",
                errorClass: "help-block",
                ignor: "",
                rules: {
                    Start_date: {
                        required: true
                    },
                    End_date: {
                        required: true
                    },
                    Status: {
                        required: true
                    },
                    Project_id: {
                        required: true
                    },
                    Total_estimate_time: {
                        required: true
                    }
                },
                messages: {
                    Start_date: {
                        required: "Enter Start Date"
                    },
                    End_date: {
                        required: "Enter End Date"
                    },
                    Status: {
                        required: "Enter Plan Status"
                    },
                    Project_id: {
                        required: "Choose Project"
                    },
                    Total_estimate_time: {
                        required: "Enter Total Estimate Time"
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
                    var planid = $('#Plan_id').val();
                    var sdate = $('#Start_date').val();
                    var edate = $('#End_date').val();
                    var status = $('#Status').val();
                    var projectid = $('#Project_id').val();
                    if (projectid == "" || projectid==null)
                    {
                        projectid = pid;
                    }
                    var desc = $('#Description').val();
                    var estimate = $('#Total_estimate_time').val();
                    var formData = { Plan_id: planid, Start_date: sdate, End_date: edate, Status: status, Project_id: projectid, Description: desc, Total_estimate_time: estimate };

                    try {
                        $.ajax({
                            type: 'post',
                            url: '/Plan/Save',
                            data: formData,
                            datatype: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    window.location.href = "/Plan/Index";
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
        });
        $("#End_date").datepicker({
            format: 'dd-M-yyyy',
            startDate: new Date(),
            autoclose: true
        });
    }

    var dropdownValidation = function () {
        $("#Project_id").on('change', function (event) {
            if ($("#Project_id").val() != "") {
                $(this).parent().closest(".has-error").removeClass("has-error").addClass("has-success").find(".help-block").hide().end().find('.symbol').addClass('ok');
            }
        });
    }

    var loadPlanDataById = function () {
        try {
            if (planId != null) {
                $.ajax({
                    type: 'get',
                    url: '/Plan/LoadPlanById',
                    data: { id: planId },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            $('#Plan_id').val(data.Plan_id);
                            $('#Start_date').val(data.Start_date);
                            $('#End_date').val(data.End_date);
                            $('#Status').val(data.Status);
                            $('#Project_id').val(data.Project_id);
                            pid = data.Project_id;
                            $('#divProject').hide();
                            $('#Description').val(data.Description);
                            $('#Total_estimate_time').val(data.Total_estimate_time);
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

    var loadProject = function () {
        try {
            $.ajax({
                type: 'post',
                url: '/Plan/LoadProject',
                datatype: 'json',
                success: function (response) {
                    if (response.success == true && response.Data.length > 0) {
                        var lst = response.Data;
                        var items = '<option value="" disabled selected>Select Project </option>';

                        if (lst.length > 0) {
                            $.each(lst, function (i, user) {
                                items += "<option value='" + user.Project_id + "'>" + user.Project_name + "</option>";
                            });
                            $('#Project_id').html(items);
                            $('#Project_id').selectpicker();
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
            $('#Plan_id').val("");
            $('#Start_date').val("");
            $('#End_date').val("");
            $('#Status').val("");
            $('#Project_id').val('').selectpicker("refresh");
            $('#Description').val("");
            $('#Total_estimate_time').val("");
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
            loadPlanDataById();
            loadProject();
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