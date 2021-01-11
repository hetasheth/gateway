var FormValidator = function () {
    var pathnames = window.location.pathname;
    var path = pathnames.split("/");
    var RoleId = path[3];
    var r = [];

    var UID = getParameterByName('id');

    var LoadRoleData = function (arrRole, UserId) {
        try {

            $.ajax({
                type: "post",
                url: "/UserManagement/GetAllUser",
                dataType: 'json'
            })
            .success(function (result) {

                var response = result.Data.Role;
                var items = '';
                if (response.length > 0) {
                    $.each(response, function (i, user) {
                        if ($.inArray(parseInt(user.Value), arrRole) != -1) {
                            items += "<option value='" + user.Value + "' selected>" + user.Text + "</option>";
                        }
                        else {
                            items += "<option value='" + user.Value + "'>" + user.Text + "</option>";
                        }
                    });

                    $('ul.select2-choices > li.select2-search-choice').remove();
                    $('#RoleId').html(items);
                }
                else {
                    $('#RoleId').html(items);
                    $('ul.select2-choices > li.select2-search-choice').remove();
                }

                $('#RoleId').multiselect({
                    buttonWidth: '400px',
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true
                });
                $("#RoleId").multiselect('rebuild');

                var response1 = result.Data.Userdata;
                var items1 = '<option value="">Select Employee </option>';
                if (response1.length > 0) {
                    $.each(response1, function (i, user) {

                        items1 += "<option value='" + user.Value + "'>" + user.Text + "</option>";

                    });

                    $('ul.select2-choices > li.select2-search-choice').remove();
                    $('#UserId').html(items1);
                    $("#UserId").selectpicker();

                }
                else {
                    $('#UserId').html(items1);
                    $("#UserId").selectpicker();
                    $('ul.select2-choices > li.select2-search-choice').remove();
                }
                if (UserId != "") {
                    $("#UserId").val(UserId);
                    $("#UserId").selectpicker('refresh');
                }
                

            })
            .fail(function () {
                toastr.setToast("error", 'Error!! Operation Failed');
            });


        }
        catch (err) {
            console.log(err);
        }
    };
    var validateDropdown = function (val) {
        try {
            $("#RoleId").on('change', function (event) {
                if ($("#RoleId").val() != "") {

                    $(this).parent().closest(".has-error").removeClass("has-error").addClass("has-success").find(".help-block").hide().end().find('.symbol').addClass('ok');
                }

            });
            $("#UserId").on('change', function (event) {
                if ($("#UserId").val() != "") {

                    $(this).parent().closest(".has-error").removeClass("has-error").addClass("has-success").find(".help-block").hide().end().find('.symbol').addClass('ok');
                }

            });
        }
        catch (err) {
            console.log(err);
        }
    };

    var getUserById = function () {
        try{
            if (UID != null) {
                $.ajax({
                    type: 'get',
                    url: '/UserManagement/GetUserById',
                    data: { id: UID },
                    datatype: 'json',
                    success: function (response) {
                        if (response.success == true) {
                            var data = response.Data;
                            setTimeout(function () {
                                $('#UserId').val(data.Id);
                                $('#UserId').selectpicker('refresh');
                            }, 500);
                        }
                    },
                    error: function (ex) {
                        alert(ex);
                    }
                });
            }
        }
        catch(err){
            console.log(err);
        }
    };

    var editRole = function(value)
    {
        $.ajax({
            type: "POST",
            url: "/UserManagement/GetUserAssignRole",
            data: { UserId: value },
            dataType: "json",
        })
             .done(function (response) {
                 var results = response.data;
                 var r = [];
                 for (var i = 0, len = results.length; i < len; i++) {
                     r.push(results[i].Role_Id);
                 }
                 LoadRoleData(r, value);
             });

    }

    var DropdownChange = function () {
        $("#UserId").change(function () {
            r = [];
            var value = $("#UserId").val();
            if (value == "" || value == null) {
                value = 0;
                alert(value);
            }
            editRole(value);
            
        });
    };


    var runValidatorPermission = function () {
        try {
            var form1 = $('#form');
            var errorHandler1 = $('.errorHandler', form1);
            var successHandler1 = $('.successHandler', form1);

            var valid = $('#form').validate({
                //focusCleanup: true,
                errorElement: "span",
                errorClass: 'help-block',
                ignore: "",
                rules: {
                    RoleId: {
                        required: true,
                    },
                    UserId: {
                        required: true,
                    },
                },
                messages: {
                    RoleId:
                       {
                           required: "Please Specify Role",
                       },
                    UserId:
                        {
                            required: "Please Specify Employee",
                        },
                },
                invalidHandler: function (event, validator) { //display error alert on form submit
                    successHandler1.hide();
                    errorHandler1.show();
                },
                errorPlacement: function (error, element) {
                    if (element.attr("type") == "radio" || element.attr("type") == "checkbox") { // for chosen elements, need to insert the error after the chosen container
                        error.insertAfter($(element).closest('.form-group').children('div').children().last());
                    }
                    else if (element.parent('.input-group').length) {
                        error.insertAfter(element.parent());
                    }
                    else if (element.attr("id") == "RoleId") {
                        error.insertAfter($(element).closest('.form-line').children('div').children().last());
                    } else {
                        error.insertAfter(element);
                        // for other inputs, just perform default behavior
                    }

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
                success: function (label, element) {
                    label.addClass('help-block valid');
                    // mark the current input as valid and display OK icon
                    $(element).closest('.form-line').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
                },
                submitHandler: function (form) {
                    //var unique = r.filter(function (itm, i, r) {
                    //    return i == r.indexof(itm);
                    //});

                    var objdata = { UserId: $("#UserId").val(), RoleId: $("#RoleId").val() };
                    
                    $.ajax({
                        type: $(form).attr('method'),
                        url: $(form).attr('action'),
                        data: objdata,
                        dataType: 'json'
                    })
                    .done(function (response) {

                        if (response.success && response.Message != null) {
                            toastr.setToast("success", response.Message);

                        }
                        else if (response.success == false && response.Message != null) {

                            toastr.setToast("error", response.Message);

                        }

                        else {
                            toastr.setToast("error", 'Error!! Operation Failed');
                        }
                    })
                    .fail(function () {
                        toastr.setToast("error", 'Error!! Operation Failed');
                    });
                    return false;


                }
            });


            $("#form").removeAttr("novalidate");
        } catch (err) {
            console.log(err);
        }
    };


    return {
        //main function to initiate template pages
        init: function () {
            validateDropdown();
            getUserById();
            LoadRoleData();
            DropdownChange();
            runValidatorPermission();
            var value = UID;
            if(value != null && value != undefined)
            {
                editRole(value);
            }
                
            
        }
    };

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