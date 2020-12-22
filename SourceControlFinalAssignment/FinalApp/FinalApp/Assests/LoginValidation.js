$(document).ready(function () {
    $(document).ready(function () {
        formValidator.init();
    });

    var formValidator = function () {

        var runValidator = function () {
            try {
                $.validator.addMethod(
                    "EMAIL",
                    function (value, element, regexp) {
                        var check = false;
                        return this.optional(element) || regexp.test(value);
                    }                    
                );    
                $.validator.addMethod(
                    "PASSWORD",
                    function (value, element, regexp) {
                        var check = false;
                        return this.optional(element) || regexp.test(value);
                    }
                );   

                var valid = $('#form').validate({
                    errorElement: "span",
                    errorClass: "help-block",
                    ignor: "",
                    rules: {
                        EmailID: {
                            required: true,
                            EMAIL: /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/
                        },
                        Password: {
                            required: true,
                            PASSWORD:/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/,
                            minlength: 8,
                            maxlength:32
                        }
                    },
                    messages: {
                        EmailID: {
                            required: 'Please Enter Email Address',
                            EMAIL: 'Please enter a valid email address.'
                        },
                        Password: {
                            required: 'Please Enter Password',
                            PASSWORD: 'Password should be atleast 8 characters long and must contain at least one uppercase letter, one lowercase letter, one number and one special character.',
                            minlength: 'Password must contain atleast 8 characters',
                            maxlength:'Password can not contain more than 32 characters'
                        }
                    },
                    submitHandler: function (form) {                        
                        form.submit();
                    }
                });
            }
            catch (err) {
                console.log(err);
            }
        };        

        return {
            init: function () {
                runValidator();                
            }
        };

    }();
});