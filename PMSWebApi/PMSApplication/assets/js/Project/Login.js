var formValidator = function () {

    var runValidator = function () {
        try {
            var form1 = $('#form');
            var errorHandler1 = $('.errorHandler', form1);
            var valid = $('#form').validate({
                errorElement: "span",
                errorClass: "help-block",
                ignor: "",
                rules: {
                    Email: {
                        required: true
                    },
                    Password: {
                        required: true
                    }
                },
                messages: {
                    Email: {
                        required: "Enter Valid Email"
                    },
                    Password: {
                        required: "Enter Valid Password"
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
                    var emailid = $('#Email').val();
                    var password = $('#Password').val();
                    try {
                        $.ajax({
                            type: $(form1).attr("method"),
                            url: $(form1).attr("action"),
                            data: $(form1).serialize(),
                            datatype: 'json',
                            success: function (response) {
                                if (response.success == true && response.Data=="A") {
                                    window.location.href = "/Home/AdminDashBoard";
                                }
                                else if (response.success == true && response.Data == "D") {
                                    window.location.href = "/Home/DeveloperDashBoard";
                                }
                                else if (response.success == true && response.Data == "T") {
                                    window.location.href = "/Home/TesterDashBoard";
                                }
                                else {
                                    toastr.setToast("error", response.responseText);
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


    return {
        init: function () {
            runValidator();
        }
    }
}();

