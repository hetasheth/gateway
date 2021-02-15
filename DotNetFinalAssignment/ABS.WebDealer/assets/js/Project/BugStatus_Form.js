var formValidator = function () {

    var bsId = getParameterByName('id');

    var runValidator = function () {
        try {
            var form1 = $('#form');
            var errorHandler1 = $('.errorHandler', form1);
            var valid = $('#form').validate({
                errorElement: "span",
                errorClass: "help-block",
                ignor: "",
                rules: {
                    Bug_id: {
                        required: true
                    },
                    Status: {
                        required: true
                    },
                    Date: {
                        required: true
                    },
                    Emp_id: {
                        required: true
                    }
                },
                messages: {
                    Bug_id: {
                        required: "Choose Bug"
                    },
                    Status: {
                        required: "Enter Status"
                    },
                    Date: {
                        required: "Enter Date"
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
                    var bsid = $('#Bug_status_id').val();
                    var bid = $('#Bug_id').val();
                    var status = $('#Status').val();
                    var dt=$('#Date').val();
                    var eid = $('#Emp_id').val();
                    var formData = { Bug_status_id: bsid, Bug_id: bid,Status:status, Date: dt,  Emp_id: eid };
                    var things = JSON.stringify(formData);

                    try {
                        $.ajax({
                            type: 'post',
                            url: '/BugStatus/Save',
                            data: formData,
                            datatype: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    window.location.href = "/BugStatus/Index";
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

    var dropdownValidation = function () {
        $("#Bug_id").on('change', function (event) {
            if ($("#Bug_id").val() != "") {
                $(this).parent().closest(".has-error").removeClass("has-error").addClass("has-success").find(".help-block").hide().end().find('.symbol').addClass('ok');
            }
        });
        $("#Emp_id").on('change', function (event) {
            if ($("#Emp_id").val() != "") {
                $(this).parent().closest(".has-error").removeClass("has-error").addClass("has-success").find(".help-block").hide().end().find('.symbol').addClass('ok');
            }
        });
    }

    var addDatePicker = function () {
        $("#Date").datepicker({
            format: 'dd-M-yyyy',
            startDate: new Date()
        });
    }

    var loadBugStatusDataById = function () {
        try {
            if (bsId != null) {
                $.ajax({
                    type: 'get',
                    url: '/BugStatus/LoadBugStatusById',
                    data: { id: bsId },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            $('#Bug_status_id').val(data.Bug_status_id);
                            setTimeout(function () {
                                $('#Bug_id').val(data.Bug_id);
                                $('#Bug_id').selectpicker('refresh');
                            }, 500);
                            $('#Status').val(data.Status);
                            $('#Date').val(data.Date);
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

    var loadBug = function () {
        try {
            $.ajax({
                type: 'post',
                url: '/Bug/loadBug',
                datatype: 'json',
                success: function (response) {
                    if (response.success == true && response.Data.length > 0) {
                        var lst = response.Data;
                        var items = '<option value="" disabled selected>Select Bug </option>';

                        if (lst.length > 0) {
                            $.each(lst, function (i, user) {
                                items += "<option value='" + user.Bug_id + "'>" + user.Bug_description + "</option>";
                            });
                            $('#Bug_id').html(items);
                            $('#Bug_id').selectpicker();
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
            $('#Bug_status_id').val("");
            $('#Bug_id').val('').selectpicker("refresh");
            $('#Date').val("");
            $('#Status').val("");
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
            dropdownValidation();
            loadBugStatusDataById();
            addDatePicker();
            loadBug();
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