var formValidator = function () {

    var projectTypeId = getParameterByName('id');

    var runValidator = function () {
        try {
            var form1 = $('#form');
            var errorHandler1 = $('.errorHandler', form1);
            var valid = $('#form').validate({
                errorElement: "span",
                errorClass: "help-block",
                ignor: "",
                rules: {
                    Type: {
                        required:true
                    },
                    Description: {
                        required:true
                    }
                },
                messages: {
                    Type: {
                        required:"Enter Project Type"
                    },
                    Description:{
                        required:"Enter Type Description"
                    }
                },
                invalidHandler: function(event,validator){
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
                submitHandler: function(form1) {
                    var ptid = $('#Type_id').val();
                    var ptype = $('#Type').val();
                    var typedescription = $('#Description').val();
                    var formData = { Type_id:ptid,Type: ptype, Description: typedescription };

                    try{
                        $.ajax({
                            type:'post',
                            url: '/ProjectType/Save',
                            data:formData,
                            datatype: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    window.location.href="/ProjectType/Index";
                                }
                            },
                            error:function(ex){
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

    var loadProjectTypeDataById = function () {
        try {
            if (projectTypeId != null) {
                $.ajax({
                    type: 'get',
                    url: '/ProjectType/LoadProjectTypeById',
                    data: { id: projectTypeId },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            $('#Type_id').val(data.Type_id);
                            $('#Type').val(data.Type);
                            $('#Description').val(data.Description);
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

    var ClearButton = function () {
        $("#btnClear").click(function () {
            $("#Type").val("");
            $("#Description").val("");
            $(".errorHandler").hide();
            $("div").removeClass("has-error");
            $(".help-block").hide();
        });
    };

    return {
        init: function () {
            ClearButton();
            runValidator();
            loadProjectTypeDataById();
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