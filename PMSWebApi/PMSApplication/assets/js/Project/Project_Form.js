var formValidator = function () {

    var projectId = getParameterByName('id');

    var runValidator = function () {
        try {
            var form1 = $('#form');
            var errorHandler1 = $('.errorHandler', form1);
            var valid = $('#form').validate({
                errorElement: "span",
                errorClass: "help-block",
                ignor: "",
                rules: {
                    Project_name: {
                        required: true
                    },
                    Type_id: {
                        required: true
                    },
                    Tech_id: {
                        required: true
                    }
                },
                messages: {
                    Project_name: {
                        required: "Enter Project Name"
                    },
                    Type_id: {
                        required: "Choose Project Type"
                    },
                    Tech_id: {
                        required: "Choose Technology"
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
                    var proid = $('#Project_id').val();
                    var project = $('#Project_name').val();
                    var typeid = $('#Type_id').val();
                    var techid = $('#Tech_id').val();
                    var desc = $('#Description').val;
                    var priority = $('#Priority').val();
                    var formData = { Project_id: proid, Project_name: project, Type_id: typeid, Tech_id: techid, Description: desc, Priority: priority };

                    try {
                        $.ajax({
                            type: 'post',
                            url: '/Project/Save',
                            data: formData,
                            datatype: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    window.location.href = "/Project/Index";
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
        $("#Type_id").on('change', function (event) {
            if ($("#Type_id").val() != "") {
                $(this).parent().closest(".has-error").removeClass("has-error").addClass("has-success").find(".help-block").hide().end().find('.symbol').addClass('ok');
            }
        });
        $("#Tech_id").on('change', function (event) {
            if ($("#Tech_id").val() != "") {
                $(this).parent().closest(".has-error").removeClass("has-error").addClass("has-success").find(".help-block").hide().end().find('.symbol').addClass('ok');
            }
        });
    }

    var loadProjectDataById = function () {
        try {
            if (projectId != null) {
                $.ajax({
                    type: 'get',
                    url: '/Project/LoadProjectById',
                    data: { id: projectId },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            $('#Project_id').val(data.Project_id);
                            $('#Project_name').val(data.Project_name);
                            setTimeout(function () {
                                $('#Type_id').val(data.Type_id);
                                $('#Type_id').selectpicker('refresh');
                            }, 100);
                            setTimeout(function () {
                                $('#Tech_id').val(data.Tech_id);
                                $('#Tech_id').selectpicker('refresh');
                            }, 100);
                            $('#Description').val(data.Description);
                            $('#Priority').val(data.Priority).change();
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

    var loadProjectType = function () {
        try {
            $.ajax({
                type: 'post',
                url: '/ProjectType/loadProjectType',
                datatype: 'json',
                success: function (response) {
                    if (response.success == true && response.Data.length > 0) {
                        var lst = response.Data;
                        var items = '<option value="" disabled selected>Select Project Type </option>';

                        if (lst.length > 0) {
                            $.each(lst, function (i, user) {
                                items += "<option value='" + user.Type_id + "'>" + user.Type + "</option>";
                            });
                            $('#Type_id').html(items);
                            $('#Type_id').selectpicker();
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

    var loadTechnology = function () {
        try {
            $.ajax({
                type: 'post',
                url: '/Technology/loadTechnology',
                datatype: 'json',
                success: function (response) {
                    if (response.success == true && response.Data.length > 0) {
                        var lst = response.Data;
                        var items = '<option value="" disabled selected>Select Technology </option>';

                        if (lst.length > 0) {
                            $.each(lst, function (i, user) {
                                items += "<option value='" + user.tech_id + "'>" + user.technology1 + " " + user.version + "</option>";
                            });
                            $('#Tech_id').html(items);
                            $('#Tech_id').selectpicker();
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
            $('#Project_id').val("");
            $('#Project_name').val("");
            $('#Type_id').val('').selectpicker("refresh");
            $('#Tech_id').val('').selectpicker("refresh");
            $('#Description').val("");
            $('#Priority').val("");
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
            loadProjectDataById();
            loadProjectType();
            loadTechnology();
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