var formValidator = function () {

    var projectID = getParameterByName('projectid');
    var status;
    var value;

    var hidecreate = function () {
        $(document).ready(function () {
            $('#createButton').hide();
        });
    };

    var getData = function (status, value) {
        try {
            $.ajax({
                type: 'post',
                url: '/BugStatus/LoadBugByStatus',
                data: { str: status, proid: value},
                datatype: 'json',
                success: function (response) {
                    if (response.success == true) {
                        fillData(response.Data, status);
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
    };

    function fillData(data, status) {
        try {

            var arrayReturn = [];
            for (var i = 0, len = data.length; i < len; i++) {
                var result = data[i];
                var act = '';
                act += "<div>";
                if ($('#CreateOrEdit').val() == 'T') {
                    act += "<button id='btnEdit" + i + "' class='btnEdit' data-edittype=" + result.Bug_id + "><i class='fa fa-pencil'></i></button>";
                }
                var detail = '';
                detail += "<div><button id='btnDetail" + i + "' class='btn btn-success' data-detail=" + result.Bug_status_id + ">Details</button></div>"
                arrayReturn.push([act,result.Task_name, result.Bug_description, result.Bug_date,result.Remark, result.First_name,detail]);
            }

            var s = status;

            if (s == "Pending") {
                var Table = $('#myTable1').dataTable({
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
                        { "sTitle": "Bug", "bSortable": true },
                        { "sTitle": "Date", "bSortable": true },
                        { "sTitle": "Remark", "bSortable": true },
                        { "sTitle": "Employee", "bSortable": true },
                        { "sTitle": "Detail", "sWidth": "10%", "bSortable": false }
                    ],
                    "bDestroy": true,
                    "paging": false,
                    "info": true
                });
                if ($('#CreateOrEdit').val() == 'F') {
                    Table.api().columns([0]).visible(false, false);
                    Table.api().columns.adjust().draw(false);
                }
            }
            else if (s == "Complete") {
                var Table = $('#myTable2').dataTable({
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
                        { "sTitle": "Bug", "bSortable": true },
                        { "sTitle": "Date", "bSortable": true },
                        { "sTitle": "Remark", "bSortable": true },
                        { "sTitle": "Employee", "bSortable": true },
                        { "sTitle": "Detail", "sWidth": "10%", "bSortable": false }
                    ],
                    "bDestroy": true,
                    "paging": false,
                    "info": true
                });
                if ($('#CreateOrEdit').val() == 'F' || s=="Complete") {
                    Table.api().columns([0]).visible(false, false);
                    Table.api().columns.adjust().draw(false);
                }
            }
            else if (s == "Close") {
                var Table = $('#myTable3').dataTable({
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
                        { "sTitle": "Bug", "bSortable": true },
                        { "sTitle": "Date", "bSortable": true },
                        { "sTitle": "Remark", "bSortable": true },
                        { "sTitle": "Employee", "bSortable": true },
                        { "sTitle": "Details", "bSortable": true }
                    ],
                    "bDestroy": true,
                    "paging": false,
                    "info": true
                });
                if ($('#CreateOrEdit').val() == 'F' || s == "Close") {
                    Table.api().columns([0]).visible(false, false);
                    Table.api().columns.adjust().draw(false);
                }
            }
        }
        catch (err) {
            console.log(err);
        }
    }

    var onChangeStatus = function () {
        $("#btnPending").click(function () {
            var status = "Pending";
            value = $("#Project_id").val();
            getData(status, value);
        });
        $("#btnComplete").click(function () {
            var status = "Complete";
            value = $("#Project_id").val();
            getData(status, value);
        });
        $("#btnClose").click(function () {
            var status = "Close";
            value = $("#Project_id").val();
            getData(status, value);
        });
    }

    var loadProject = function () {
        var eid = $('#Employeeid').val();
        try {
            $.ajax({
                type: 'post',
                url: '/Task/loadProject',
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
                    }
                    if (projectID != null) {
                        $('#divTab').show();
                        $("#Project_id").val(projectID);
                        $("#Project_id").selectpicker('refresh');
                        BugStatusData();
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

    };

    var BugStatusData = function () {
        r = [];
        value = $("#Project_id").val();
        $('#createButton').show();
        $('#divTab').show();
        status = $("#divTab").find('.active').find('a').html();
        getData(status, value);
        if (value == "" || value == null) {
            value = 0;
        }
        projectChange(value);
    };

    var DropdownChange = function () {
        $("#Project_id").change(function () {
            BugStatusData();
        });

        try {
            $('#btnCreate').click(function () {
                var proid = $('#ProjectID').val();
                window.location = "/Bug/Create?projectid=" + proid;
            });
        }
        catch (err) {
            concole.log(err);
        }
    };

    var BugDetails=function () {
        try {
            $(document).on("click", "button[data-detail]", function (e) {
                var bugid = $(this).attr("data-detail");
                window.location = "/BugStatus/Detail?id=" + bugid;
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    var editBug=function() {
        try {
            $(document).on("click", "button[data-edittype]", function (e) {
                var bugid = $(this).attr("data-edittype");
                window.location = "/Bug/AddBug?bugid=" + bugid;
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
            BugDetails();
            onChangeStatus();
            hidecreate();
            editBug();
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