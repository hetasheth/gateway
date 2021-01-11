var formValidator = function () {

    var projectID = getParameterByName('projectid');

    var loadProject = function () {
        var eid = $('#Employeeid').val();
        try {
            $.ajax({
                type: 'post',
                url: '/Task/LoadProject',
                data: { eid: eid },
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
                        $('#btnCreate').hide();
                        $('#btnAdd').hide();
                        $('#manage').hide();
                    }
                    if (projectID != null) {
                        projectChange(projectID);
                        $("#Project_id").val(projectID);
                        $("#Project_id").selectpicker('refresh');
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
    };

    var projectChange = function (value) {
        $('#ProjectID').val(value);
        $.ajax({
            type: 'POST',
            url: '/Team/LoadTeamByProject',
            data: { id: value },
            datatype: 'json',
            success: function (response) {
                if (response.success == true) {
                    $('#btnCreate').show();
                    fillData(response.Data);
                }
                else {
                    alert("Error");
                }
            }
        });
    };

    var DropdownChange = function () {

        $("#Project_id").change(function () {
            r = [];
            var value = $("#Project_id").val();

            if (value == "" || value == null) {
                value = 0;
            }
            projectChange(value);
        });

        $('#Emp_id').change(function () {
            $('#btnAdd').show();
        });

        $('#btnAdd').click(function () {
            var eid = $('#Emp_id').val();
            var pid = $('#Project_id').val();
            $.ajax({
                type: 'POST',
                url: '/Team/Save',
                data: { eid: eid,pid:pid },
                datatype: 'json',
                success: function (response) {
                    if (response.success == true) {
                        var pid = $('#Project_id').val();
                        window.location.href = "/Team/Index?projectid=" + pid;
                    }
                    else {
                        alert("Error");
                    }
                }
            });
        });

        $('#btnCreate').click(function () {
            $('#manage').show();
            var value = $("#Project_id").val();
            loadEmp(value);
        });
    };

    function fillData(data) {
        try {
            var arrayReturn = [];
            for (var i = 0, len = data.length; i < len; i++) {
                var result = data[i];
                var act = '';
                act += "<div>";
                //if ($('#CreateOrEdit').val() == 'T') {
                //    act += "<button id='btnEdit" + i + "' class='btnEdit' data-edittype=" + result.Task_id + "><i class='fa fa-pencil'></i></button>";
                //}
                //act += " ";
                if ($('#Remove').val() == 'T') {
                    act += "<button id='btnDelete" + i + "' class='btnDelete' data-deletetype=" + result.Team_id + "><i class='fa fa-times'></i></button>";
                }
                act += "</div>";
                //var execute = '';
                //if ($('#Exe').val() == 'T') {
                //    execute += "<div><button id='btnExe" + i + "' class='btn btn-success' data-exetype=" + result.Task_id + ">Execute</button></div>"
                //}
                arrayReturn.push([act, result.First_name + " " + result.Last_name]);
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
                "bAutoWidth": true,
                "aaData": arrayReturn,
                "bProcessing": true,
                "aoColumns": [
                    { "sTitle": "Action", "sWidth": "10%", "bSortable": false },
                    { "sTitle": "Employee", "bSortable": true }
                ],
                "bDestroy": true,
                "paging": false,
                "info": true
            });

        }
        catch (err) {
            console.log(err);
        }
    };

    var loadEmp= function (value) {
        try {
            $.ajax({
                type: 'post',
                url: '/Team/loadEmployee',
                data: { id: value },
                datatype: 'json',
                success: function (response) {
                    if (response.success == true && response.Data.length > 0) {
                        var lst = response.Data;
                        var items = '';
                        if (lst.length > 0) {
                            $.each(lst, function (i, user) {
                                items += "<option value='" + user.Emp_id + "'>" + user.First_name + " " + user.Last_name + "</option>";
                            });
                            $('#Emp_id').html(items);
                            $('#Emp_id').selectpicker();
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
    };

    function deleteTeam() {
        try {
            $(document).on("click", "button[data-deletetype]", function (e) {
                var deleteID = $(this).attr("data-deletetype");

                bootbox.confirm("Are you sure you want to delete this employee?", function (result) {
                    if (result == true) {
                        $.ajax({
                            type: 'post',
                            url: '/Team/Remove',
                            data: { id: deleteID },
                            dataType: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    var pid = $('#Project_id').val();
                                    window.location.href = "/Team/Index?projectid=" + pid;
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
            loadProject();
            DropdownChange();
            deleteTeam();
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