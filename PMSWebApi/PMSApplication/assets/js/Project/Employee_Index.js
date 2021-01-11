var formValidator = function () {

    function getData() {
        try {
            $.ajax({
                type: 'post',
                url: '/Employee/loadEmployee',
                datatype: 'json',
                success: function (response) {
                    if (response.success == true) {
                        fillData(response.Data);
                    }
                    else {
                        alert("Error");
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

    function fillData(data) {
        try {
            var arrayReturn = [];
            for (var i = 0, len = data.length; i < len; i++) {
                var result = data[i];
                var x = i + 1;
                var active = '';
                if (result.IsActive == true) {
                    var msg = 'Are you sure you want to Deactive this user?';
                    active += "<div><button id='btnActive" + i + "' class='btn btn-danger' data-activetype=" + result.Emp_id + " data-msg='" + msg + "'>Deactive</button></div>";
                }
                else {
                    var msg = 'Are you sure you want to Active this user?';
                    active += "<div><button id='btnActive" + i + "' class='btn btn-primary' data-activetype=" + result.Emp_id + " data-msg='" + msg + "'>Active</button></div>";
                }
                var act = '';
                act += "<div><button id='btnRole" + i + "' class='btnRole' data-role=" + result.User_id + "><i class='fa fa-user'></i></button> </div> ";
                var action = '';
                if ($('#CreateOrEdit').val() == 'T') {
                    action += "<div><button id='btnEdit" + i + "' class='btnEdit' data-edittype=" + result.Emp_id + "><i class='fa fa-pencil'></i></button>  ";
                }
                if ($('#Remove').val() == 'T') {
                    action += "<button id='btnDelete" + i + "' class='btnDelete' data-deletetype=" + result.Emp_id + "><i class='fa fa-times'></i></button></div>";
                }
                arrayReturn.push([x, active, act, action, result.First_name, result.Middle_name, result.Last_name, result.Gender, result.Address, result.City, result.State, result.Pincode, result.PhoneNumber, result.MobileNumber, result.Email, result.DOB]);
            }

            var Table = $('#myTable').dataTable({
                "aLengthMenu": [[10, 20, 50, -1], [10, 20, 50, "All"]],
                "oLanguage": {
                    "sLengthMenu": "Show _MENU_ Rows",
                    "sSearchPlaceholder": "Search",
                    "oPaginate": {
                        "sPrevious": "",
                        "sNext": ""
                    }
                },
                "bAutoWidth": false,
                "aaData": arrayReturn,
                "bProcessing": true,
                "aoColumns": [
                    { "sTitle": "", "sWidth": "10%", "bSortable": false },
                    { "sTitle": "Active", "sWidth": "2000px", "bSortable": false },
                    { "sTitle": "Role", "sWidth": "5%", "bSortable": false },
                    { "sTitle": "Action", "sWidth": "10%", "bSortable": false },
                    { "sTitle": "First Name", "bSortable": true },
                    { "sTitle": "Middle Name", "bSortable": true },
                    { "sTitle": "Last Name", "bSortable": true },
                    { "sTitle": "Gender", "bSortable": true },
                    { "sTitle": "Address", "bSortable": true },
                    { "sTitle": "City", "bSortable": true },
                    { "sTitle": "State", "bSortable": true },
                    { "sTitle": "Pincode", "bSortable": true },
                    { "sTitle": "Phone Number", "bSortable": true },
                    { "sTitle": "Mobile Number", "bSortable": true },
                    { "sTitle": "Email ID", "bSortable": true },
                    { "sTitle": "Date Of Birth", "bSortable": true },
                ],
                "bDestroy": true,
                "paging": true,
                "info": true
            });
            if ($('#CreateOrEdit').val() == 'F' && $('#Remove').val() == 'F') {
                Table.api().columns([2]).visible(false, false);
                Table.api().columns.adjust().draw(false);
            }
        }
        catch (err) {
            console.log(err);
        }
    }

    function activeChange() {
        $(document).on("click", "button[data-activetype]", function (e) {
            var activeID = $(this).attr("data-activetype");
            var message = $(this).data("msg");

            bootbox.confirm(message, function (result) {
                if (result == true) {
                    $.ajax({
                        type: 'post',
                        url: '/Employee/loadActiveEmployee',
                        data: { id: activeID },
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
            });
        });
    };

    function roleAssign() {
        try {
            $(document).on("click", "button[data-role]", function (e) {
                var uid = $(this).attr("data-role");
                window.location = "/UserManagement/AssignUserRole?id=" + uid;
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    function editEmployee() {
        try {
            $(document).on("click", "button[data-edittype]", function (e) {
                var editID = $(this).attr("data-edittype");
                window.location = "/Employee/Edit?id=" + editID;
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    function deleteEmployee() {
        try {
            $(document).on("click", "button[data-deletetype]", function (e) {
                var deleteID = $(this).attr("data-deletetype");

                bootbox.confirm("Are you sure you want to delete this employee?", function (result) {
                    if (result == true) {
                        $.ajax({
                            type: 'post',
                            url: '/Employee/Remove',
                            data: { id: deleteID },
                            dataType: 'json',
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
                });
            });
        }
        catch (err) {
            console.log(err);
        }
    };


    return {
        init: function () {
            getData();
            activeChange();
            roleAssign();
            editEmployee();
            deleteEmployee();
        }
    };
}();