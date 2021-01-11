var formValidator = function () {

    function getData() {
        try {
            $.ajax({
                type: 'post',
                url: '/Plan/LoadPlan',
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
                var act = '';
                act += "<div>";
                if ($('#CreateOrEdit').val() == 'T') {
                    act += "<button id='btnEdit" + i + "' class='btnEdit' data-edittype=" + result.Plan_id + "><i class='fa fa-pencil'></i></button>";
                }
                act += " ";
                if ($('#Remove').val() == 'T') {
                    act += "<button id='btnDelete" + i + "' class='btnDelete' data-deletetype=" + result.Plan_id + "><i class='fa fa-times'></i></button>";
                }
                act += "</div>";
                arrayReturn.push([act, result.Start_date, result.End_date,  result.Project_name, result.Total_estimate_time]);
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
                    { "sTitle": "Start Date", "bSortable": true },
                    { "sTitle": "End Date", "bSortable": true },
                    { "sTitle": "Project", "bSortable": true },
                    { "sTitle": "Total Estimate Time", "bSortable": true }
                ],
                "bDestroy": true,
                "paging": true,
                "info": true
            });
            if ($('#CreateOrEdit').val() == 'F' && $('#Remove').val() == 'F') {
                Table.api().columns([0]).visible(false, false);
                Table.api().columns.adjust().draw(false);
            }
        }
        catch (err) {
            console.log(err);
        }
    }

    function editPlan() {
        try {
            $(document).on("click", "button[data-edittype]", function (e) {
                var editID = $(this).attr("data-edittype");
                window.location = "/Plan/Edit?id=" + editID;
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    function deletePlan() {
        try {
            $(document).on("click", "button[data-deletetype]", function (e) {
                var deleteID = $(this).attr("data-deletetype");

                bootbox.confirm("Are you sure you want to delete this plan?", function (result) {
                    if (result == true) {
                        $.ajax({
                            type: 'post',
                            url: '/Plan/Remove',
                            data: { id: deleteID },
                            dataType: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    window.location.href = "/Plan/Index";
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
            editPlan();
            getData();
            deletePlan();
        }
    };
}();