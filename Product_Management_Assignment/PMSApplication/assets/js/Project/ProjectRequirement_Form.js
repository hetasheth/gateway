var formValidator = function () {

    var projectReqId = getParameterByName('id');

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
                    Module: {
                        required: true
                    }
                },
                messages: {
                    Project_id: {
                        required: "Choose Project"
                    },
                    Module: {
                        required: "Enter Module"
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
                    var proreqid = $('#Proj_require_id').val();
                    var proid = $('#Project_id').val();
                    var module = $('#Module').val();
                    var sub = $('#SubModule').val();
                    var parent = $('#ParentModule').val();
                    var formData = { Proj_require_id: proreqid, Project_id: proid, Module: module, SubModule: sub, ParentModule: parent };

                    try {
                        $.ajax({
                            type: 'post',
                            url: '/ProjectRequirement/Save',
                            data: formData,
                            datatype: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    window.location.href = "/ProjectRequirement/Index";
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
    }

    var loadProjectReqDataById = function () {
        try {
            if (projectReqId != null) {
                $.ajax({
                    type: 'get',
                    url: '/ProjectRequirement/LoadProjectRequirementById',
                    data: { id: projectReqId },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            $('#Proj_require_id').val(data.Proj_require_id);
                            setTimeout(function () {
                                $('#Project_id').val(data.Project_id);
                                $('#Project_id').selectpicker('refresh');
                            },500);
                            $('#Module').val(data.Module);
                            $('#SubModule').val(data.SubModule);
                            $('#ParentModule').val(data.ParentModule);
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

    var ClearButton = function () {
        $("#btnClear").click(function () {
            $('#Proj_require_id').val("");
            $('#Project_id').val('').selectpicker("refresh");
            $('#Module').val("");
            $('#SubModule').val("");
            $('#ParentModule').val("");
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
            loadProjectReqDataById();
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