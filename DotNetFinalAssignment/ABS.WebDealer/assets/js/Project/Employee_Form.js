var formValidator = function () {

    var empId = getParameterByName('id');
    var runValidator = function () {
        try {
            var form1 = $('#form');
            var errorHandler1 = $('.errorHandler', form1);
            var valid = $('#form').validate({
                errorElement: "span",
                errorClass: "help-block",
                ignor: "",
                rules: {
                    First_name: {
                        required: true
                    },
                    Last_name: {
                        required: true
                    },
                    Gender: {
                        required: true
                    },
                    DOB: {
                        required: true
                    },
                    Address: {
                        required: true
                    },
                    City: {
                        required: true
                    },
                    State: {
                        required: true
                    },
                    Pincode: {
                        required: true,
                        minlength: 6,
                        maxlength: 6
                    },
                    PhoneNumber: {
                        required: true,
                        minlength: 10,
                        maxlength: 10
                    },
                    MobileNumber: {
                        minlength: 10,
                        maxlength: 10
                    },
                    Email: {
                        required: true,
                        email: true
                    },
                    Password: {
                        required: true
                    }
                },
                messages: {
                    First_name: {
                        required: "Enter First Name"
                    },
                    Last_name: {
                        required: "Enter Last Type"
                    },
                    Gender: {
                        required: "Choose Gender"
                    },
                    DOB: {
                        required: "Choose Date of Birth"
                    },
                    Address: {
                        required: "Enter Addess"
                    },
                    City: {
                        required: "Enter City"
                    },
                    State: {
                        required: "Enter State"
                    },
                    Pincode: {
                        required: "Enter Pincode",
                        minlength: "pincode should be of 6 digit",
                        maxlength: "pincode should be of 6 digit"
                    },
                    PhoneNumber: {
                        required: "Enter Mobile Number",
                        minlength: "mobile number should be of 10 digit",
                        maxlength: "mobile number should be of 10 digit"
                    },
                    MobileNumber: {
                        minlength: "mobile number should be of 10 digit",
                        maxlength: "mobile number should be of 10 digit"
                    },
                    Email: {
                        required: "Enter Email Id",
                        email: "Enter Valid Email Id"
                    },
                    Password: {
                        required: "Enter Password"
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
                    // set error class to the control groups
                },
                errorPlacement: function (error, element) {
                    if (element.attr("type") == "radio" || element.attr("type") == "checkbox") { // for chosen elements, need to insert the error after the chosen container
                        error.insertAfter($(element).closest('.form-line').children('div').children().last());
                    }
                },
                submitHandler: function (form1) {
                    var empid = $('#Emp_id').val();
                    var uid = $('#User_id').val();
                    var fname = $('#First_name').val();
                    var mname = $('#Middle_name').val();
                    var lname = $('#Last_name').val();
                    var gender = $('input[name="Gender"]:checked').val();
                    var dob = $('#DOB').val();
                    var addr = $('#Address').val();
                    var city = $('#City').val();
                    var state = $('#State').val();
                    var pin = $('#Pincode').val();
                    var phone = $('#PhoneNumber').val();
                    var mobile = $('#MobileNumber').val();
                    var email = $('#Email').val();
                    var password = $('#Password').val();

                    var formData = { Emp_id: empid, User_id: uid, First_name: fname, Middle_name: mname, Last_name: lname, Gender: gender, DOB: dob, Address: addr, City: city, State: state, Pincode: pin, PhoneNumber: phone, MobileNumber: mobile, Email: email, Password: password };
                    var things = JSON.stringify(formData);
                    try {
                        $.ajax({
                            type: 'post',
                            url: '/Employee/Save',
                            data: formData,
                            datatype: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    window.location.href = "/Employee/Index";
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

    var loadEmployeeDataById = function () {
        try {
            if (empId != null) {
                $.ajax({
                    type: 'get',
                    url: '/Employee/LoadEmployeeById',
                    data: { id: empId },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            $('#Emp_id').val(data.Emp_id);
                            $('#User_id').val(data.User_id);
                            $('#First_name').val(data.First_name);
                            $('#Middle_name').val(data.Middle_name);
                            $('#Last_name').val(data.Last_name);
                            //$('input[name="Gender"]:checked').val(data.Gender);
                            $('input[name="Gender"][value=' + data.Gender + ']').prop("checked", true);
                            $('#DOB').val(data.DOB);
                            $('#Address').val(data.Address);
                            $('#City').val(data.City);
                            $('#State').val(data.State);
                            $('#Pincode').val(data.Pincode);
                            $('#PhoneNumber').val(data.PhoneNumber);
                            $('#MobileNumber').val(data.MobileNumber);
                            $('#Email').val(data.Email);
                            $('#Password').val(data.Password);
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

    var addDatePicker = function () {
        $("#DOB").datepicker({
            format: 'dd-M-yyyy',
        });
    }

    var ClearButton = function () {
        $("#btnClear").click(function () {
            $('#Emp_id').val("");
            $('#User_id').val("");
            $('#First_name').val("");
            $('#Middle_name').val("");
            $('#Last_name').val("");
            $('input[name="Gender"]:checked').val("");
            $('#DOB').val("");
            $('#Address').val("");
            $('#City').val("");
            $('#State').val("");
            $('#Pincode').val("");
            $('#PhoneNumber').val("");
            $('#MobileNumber').val("");
            $('#Email').val("");
            $('#Password').val("");
            $(".errorHandler").hide();
            $("div").removeClass("has-error");
            $(".help-block").hide();
        });
    };

    return {
        init: function () {
            ClearButton();
            runValidator();
            loadEmployeeDataById();
            addDatePicker();
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