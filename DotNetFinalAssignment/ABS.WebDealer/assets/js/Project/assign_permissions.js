var FormValidator = function () {
    var pathnames = window.location.pathname;
    var path = pathnames.split("/");
    var RoleId = path[3];
    var r = [];

    var LoadRoleData = function () {
        try {

            $.ajax({
                type: "post",
                url: "/UserManagement/GetAllPermissionAndRole",
                dataType: 'json'
            })
            .success(function (result) {

                var response = result.Data.Role;
                var permission = result.Data.GetPermissions;
                var items = '<option value="" selected="selected">Select Role </option>';
                if (response.length > 0) {
                    $.each(response, function (i, user) {

                        items += "<option value='" + user.Value + "'>" + user.Text + "</option>";

                    });

                    $('ul.select2-choices > li.select2-search-choice').remove();

                    $('#RoleId').html(items);
                    $("#RoleId").selectpicker();

                }
                else {
                    $('#RoleId').html(items);
                    $("#RoleId").selectpicker();
                    $('ul.select2-choices > li.select2-search-choice').remove();
                }
                $('#tree_2').jstree({
                    'plugins': ["wholerow", "checkbox", "types"],
                    'core': {
                        "themes": {
                            "responsive": false
                        },
                        'data': permission
                    },
                    "types": {
                        "default": {
                            "icon": "fa fa-folder text-red fa-lg"
                        },
                        "file": {
                            "icon": "fa fa-file text-red fa-lg"
                        }
                    }
                });

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
        }
        catch (err) {
            console.log(err);
        }
    };
  
    var DropdownChange = function () {
        $("#RoleId").change(function () {
            r = [];
            var value = $("#RoleId").val();
            if (value == "" || value == null) {
                value = 0;
            }

            $.ajax({
                type: "POST",
                url: "/UserManagement/GetRoleAssignPermission",
                data: { RoleId: value },
                dataType: "json",

            })
             .done(function (response) {

                 $('#tree_2').jstree("deselect_all");
                 if (response.success == true && response.data != null) {
                     var results = response.data;
                     for (var i = 0, len = results.length; i < len; i++) {
                         $("#tree_2").jstree("select_node", results[i].PermissionId);
                     }
                 }
                 else {

                 }
             });

        });
    };
    var SelectedNode = function () {
        $('#tree_2').on('changed.jstree', function (e, data) {
            var i, j;
            r = [];
            for (i = 0, j = data.selected.length; i < j; i++) {
                var check = data.instance.get_node(data.selected[i]).id;
                var str2 = "Child-";

                if (check.indexOf(str2) != -1) {
                    var getsplitid = check.split("-");
                    var Permissionid = getsplitid[1];
                    r.push(Permissionid);
                }

            }

        })
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
                },
                messages: {
                    RoleId:
                        {
                            required: "Please Specify Role Name",
                        },
                },
                invalidHandler: function (event, validator) { //display error alert on form submit
                    successHandler1.hide();
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
                success: function (label, element) {
                    label.addClass('help-block valid');
                    // mark the current input as valid and display OK icon
                    $(element).closest('.form-line').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
                },
                submitHandler: function (form) {
                    //var unique = r.filter(function (itm, i, r) {
                    //    return i == r.indexof(itm);
                    //});

                    var objdata = { RoleId: $("#RoleId").val(), PermissionId: r };

                    $.ajax({
                        type: $(form).attr('method'),
                        url: $(form).attr('action'),
                        data: objdata,
                        dataType: 'json'
                    })
                    .done(function (response) {

                        if (response.success && response.Message != null) {
                            toastr.setToast("success", response.Message);
                            //   window.location.href = "/UserManagement/RoleIndex";
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
            LoadRoleData();
            runValidatorPermission();
            SelectedNode();
            DropdownChange();
        }

    };

}();
function GetAssignRole(value) {

    if (value == "" || value == null) {
        value = 0;
    }

    $.ajax({
        type: "POST",
        url: "/UserManagement/GetRoleAssignPermission",
        data: { RoleId: value },
        dataType: "json",

    })
     .done(function (response) {

         $('#tree_2').jstree("deselect_all");
         if (response.success == true && response.data != null) {
             var results = response.data;
             for (var i = 0, len = results.length; i < len; i++) {
                 $("#tree_2").jstree("select_node", results[i].PermissionId);
             }
         }
         else {

         }
     });


}