var formValidator = function () {

    var tdId = getParameterByName('id');

    var runValidator = function () {
        try {
            var form1 = $('#form');
            var errorHandler1 = $('.errorHandler', form1);
            var valid = $('#form').validate({
                errorElement: "span",
                errorClass: "help-block",
                ignor: "",
                rules: {
                    Task_id: {
                        required: true
                    },
                    ParentTask_id: {
                        required: true
                    },
                    Dependency_type: {
                        required: true
                    }
                },
                messages: {
                    Task_id: {
                        required: "Choose Task"
                    },
                    ParentTask_id: {
                        required: "Choose Parent task"
                    },
                    Dependency_type: {
                        required: "Enter Dependency Type"
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
                    var tdid = $('#Td_id').val();
                    var tid = $('#Task_id').val();
                    var ptid = $('#ParentTask_id').val();
                    var dtype = $('#Dependency_type').val();
                    var formData = { Td_id: tdid, Task_id: tid, ParentTask_id: ptid, Dependency_type: dtype };
                    var things = JSON.stringify(formData);
                    try {
                        $.ajax({
                            type: 'post',
                            url: '/TaskDependancy/Save',
                            data: formData,
                            datatype: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    window.location.href = "/TaskDependancy/Index";
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
        $("#Task_id").on('change', function (event) {
            if ($("#Task_id").val() != "") {
                $(this).parent().closest(".has-error").removeClass("has-error").addClass("has-success").find(".help-block").hide().end().find('.symbol').addClass('ok');
            }
        });
        $("#ParentTask_id").on('change', function (event) {
            if ($("#ParentTask_id").val() != "") {
                $(this).parent().closest(".has-error").removeClass("has-error").addClass("has-success").find(".help-block").hide().end().find('.symbol').addClass('ok');
            }
        });
    }

    var loadTaskDepDataById = function () {
        try {
            if (tdId != null) {
                $.ajax({
                    type: 'get',
                    url: '/TaskDependancy/LoadTaskDependencyById',
                    data: { id: tdId },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            $('#Td_id').val(data.Td_id);
                            setTimeout(function () {
                                $('#Task_id').val(data.Task_id);
                                $('#Task_id').selectpicker('refresh');
                            }, 500);
                            setTimeout(function () {
                                $('#ParentTask_id').val(data.ParentTask_id);
                                $('#ParentTask_id').selectpicker('refresh');
                            }, 500);
                            $('#Dependency_type').val(data.Dependency_type);
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

    var loadPTask = function () {
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
                            $('#ParentTask_id').html(items);
                            $('#ParentTask_id').selectpicker();
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
            $('#Task_id').val('').selectpicker("refresh");
            $('#ParentTask_id').val('').selectpicker("refresh");
            $('#Dependency_type').val("");
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
            loadTaskDepDataById();
            loadTask();
            loadPTask();
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