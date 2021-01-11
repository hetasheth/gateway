var formValidator = function () {

    var hidecreate = function () {
        $(document).ready(function () {
            $('#createButton').hide();
        });
    };

    var projectID = getParameterByName('projectid');

    var loadProject = function () {
        var eid = $('#Employeeid').val();
        try {
            $.ajax({
                type: 'post',
                url: '/Task/LoadProject',
                data: { eid :eid},
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
                    }
                    if (projectID != null) {
                        $('#createButton').show();
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
            url: '/Task/loadTaskByProject',
            data: { id: value },
            datatype: 'json',
            success: function (response) {
                if (response.success == true) {
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
            $('#createButton').show();
            r = [];
            var value = $("#Project_id").val();
            
            if (value == "" || value == null) {
                value = 0;
            }
            projectChange(value);
        });

        try{
            $('#btnCreate').click(function () {
                var proid = $('#ProjectID').val();
                window.location = "/Task/Create?pid=" + proid;
            });
        }
        catch (err) {
            concole.log(err);
        }
    };

    function fillData(data) {
        try {
            var arrayReturn = [];
            for (var i = 0, len = data.length; i < len; i++) {
                var result = data[i];
                var act = '';
                act += "<div>";
                if ($('#CreateOrEdit').val() == 'T') {
                    act += "<button id='btnEdit" + i + "' class='btnEdit' data-edittype=" + result.Task_id + "><i class='fa fa-pencil'></i></button>";
                }
                act += " ";
                if ($('#Remove').val() == 'T') {
                    act += "<button id='btnDelete" + i + "' class='btnDelete' data-deletetype=" + result.Task_id + "><i class='fa fa-times'></i></button>";
                }
                act += "</div>";
                var execute = '';
                if ($('#Exe').val() == 'T') {
                    execute += "<div><button id='btnExe" + i + "' class='btn btn-success' data-exetype=" + result.Task_id + ">Execute</button></div>"
                }
                arrayReturn.push([act,result.Task_name, result.Start_date, result.End_date, result.Priority, result.Status,result.Estimate_time,execute]);
            }

            var Table =$('#myTable').dataTable({
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
                    { "sTitle": "Task", "bSortable": true },
                    { "sTitle": "Start Date", "bSortable": true },
                    { "sTitle": "End date", "bSortable": true },
                    { "sTitle": "Priority", "bSortable": false },
                    { "sTitle": "Status", "bSortable": true },
                    { "sTitle": "Estimate Time", "bSortable": true },
                    { "sTitle": "Execution", "sWidth": "10%", "bSortable": false }
                ],
                "bDestroy": true,
                "paging": false,
                "info": true
            });
            if ($('#CreateOrEdit').val() == 'F' && $('#Remove').val() == 'F') {
                Table.api().columns([0]).visible(false, false);
                Table.api().columns.adjust().draw(false);
            }
            if ($('#Exe').val() == 'F') {
                Table.api().columns([7]).visible(false, false);
                Table.api().columns.adjust().draw(false);
            }
        }
        catch (err) {
            console.log(err);
        }
    }

    function editTask() {
        try {
            $(document).on("click", "button[data-edittype]", function (e) {
                var proid = $('#ProjectID').val();
                var editID = $(this).attr("data-edittype");
                window.location = "/Task/Edit?pid=" + proid + "?id=" + editID;
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    function executeTask() {
        try {
            $(document).on("click", "button[data-exetype]", function (e) {
                var taskID = $(this).attr("data-exetype");
                window.location = "/Task/Execute?id=" + taskID;
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    function deleteTask() {
        try {
            $(document).on("click", "button[data-deletetype]", function (e) {
                var deleteID = $(this).attr("data-deletetype");

                bootbox.confirm("Are you sure you want to delete this task?", function (result) {
                    if (result == true) {
                        $.ajax({
                            type: 'post',
                            url: '/Task/Remove',
                            data: { id: deleteID },
                            dataType: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    window.location.href = "/Task/Index?projectid=" + projectID;
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
            hidecreate();
            loadProject();
            DropdownChange();
            editTask();
            executeTask();
            deleteTask();
            var value = projectID;
            if (value != null && value != undefined) {
                projectChange(value);
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