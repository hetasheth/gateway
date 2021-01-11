var formValidator = function () {

    var projectID = getParameterByName('projectid');

    var loadProject = function () {
        $.ajax({
            type: 'get',
            url: '/Project/LoadProjectById',
            data: { id: projectID },
            datatype: 'json',
            success: function (response) {
                if (response.success == true) {
                    var data = response.Data;
                    $('#Project').val(data.Project_name);
                }
            }
        });
    };

    var addBugToProject= function () {
        try {
            $(document).on("click", "button[data-bug]", function (e) {
                var taskID = $(this).attr("data-bug");
                window.location = "/Bug/AddBug?id=" + taskID;
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    var getTaskData = function () {
        $('#ProjectID').val(projectID);
        $.ajax({
            type: 'POST',
            url: '/Task/loadExecutionByProject',
            data: { id: projectID },
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

    function fillData(data) {
        try {
            var arrayReturn = [];
            for (var i = 0, len = data.length; i < len; i++) {
                var result = data[i];
                var add = '';
                if ($('#CreateOrEdit').val() == 'T') {
                    add += "<div><button id='btnAddBug" + i + "' class='btn btn-success' data-bug=" + result.Task_id + ">Add Bug</button></div>"
                }
                arrayReturn.push([result.Project_name,result.Task_name, result.Start_date, result.End_date, result.Priority, result.Status, add]);
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
                    { "sTitle": "Project", "bSortable": true },
                    { "sTitle": "Task", "bSortable": true },
                    { "sTitle": "Start Date", "bSortable": true },
                    { "sTitle": "End date", "bSortable": true },
                    { "sTitle": "Priority", "bSortable": false },
                    { "sTitle": "Status", "bSortable": true },
                    { "sTitle": "Add Bug", "sWidth": "10%", "bSortable": false }
                ],
                "bDestroy": true,
                "paging": false,
                "info": true
            });
            
            if ($('#CreateOrEdit').val() == 'F') {
                Table.api().columns([6]).visible(false, false);
                Table.api().columns.adjust().draw(false);
            }
        }
        catch (err) {
            console.log(err);
        }
    }

    return {
        init: function () {
            getTaskData();
            loadProject();
            addBugToProject();
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