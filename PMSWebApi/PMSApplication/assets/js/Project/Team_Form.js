var formValidator = function () {

    var teamId = getParameterByName('id');

    var runValidator = function () {
        try {
            var form1 = $('#form');
            var errorHandler1 = $('.errorHandler', form1);
            var valid = $('#form').validate({
                errorElement: "span",
                errorClass: "help-block",
                ignor: "",
                rules: {
                    Project_id: {
                        required: true
                    },
                    Emp_id: {
                        required: true
                    }
                },
                messages: {
                    Project_id: {
                        required: "Choose Project"
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
                    var tid = $('#Team_id').val();
                    var proid = $('#Project_id').val();
                    var eid = $('#Emp_id').val();
                    var formData = { Team_id: tid, Project_id: proid, Emp_id: eid };

                    try {
                        $.ajax({
                            type: 'post',
                            url: '/Team/Save',
                            data: formData,
                            datatype: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    window.location.href = "/Team/Index";
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
        $("#Project_id").on('change', function (event) {
            if ($("#Project_id").val() != "") {
                $(this).parent().closest(".has-error").removeClass("has-error").addClass("has-success").find(".help-block").hide().end().find('.symbol').addClass('ok');
            }
        });
        $("#Emp_id").on('change', function (event) {
            if ($("#Emp_id").val() != "") {
                $(this).parent().closest(".has-error").removeClass("has-error").addClass("has-success").find(".help-block").hide().end().find('.symbol').addClass('ok');
            }
        });
    }

    var loadTeamDataById = function () {
        try {
            if (teamId != null) {
                $.ajax({
                    type: 'get',
                    url: '/Team/LoadTeamById',
                    data: { id: teamId },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            $('#Team_id').val(data.Team_id);
                            setTimeout(function () {
                                $('#Project_id').val(data.Project_id);
                                $('#Project_id').selectpicker('refresh');
                            }, 500);
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

    var loadProject = function () {
        try {
            $.ajax({
                type: 'post',
                url: '/Project/loadProject',
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
            $('#Team_id').val("");
            $('#Project_id').val('').selectpicker("refresh");
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
            loadTeamDataById();
            loadProject();
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