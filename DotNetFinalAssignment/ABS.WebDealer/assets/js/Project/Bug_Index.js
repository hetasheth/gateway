﻿var formValidator = function () {

    function getData() {
        try {
            $.ajax({
                type: 'post',
                url: '/Bug/LoadBug',
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
                    act += "<button id='btnEdit" + i + "' class='btnEdit' data-edittype=" + result.Bug_id + "><i class='fa fa-pencil'></i></button>";
                }
                act += " ";
                if ($('#Remove').val() == 'T') {
                    act += "<button id='btnDelete" + i + "' class='btnDelete' data-deletetype=" + result.Bug_id + "><i class='fa fa-times'></i></button>";
                }
                act += "</div>";
                arrayReturn.push([act, result.Bug_description, result.Bug_date, result.Task_name, result.Project_name, result.First_name]);
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
                    { "sTitle": "Bug Description", "bSortable": false },
                    { "sTitle": "Date", "bSortable": true },
                    { "sTitle": "Task", "bSortable": true },
                    { "sTitle": "Project", "bSortable": true },
                    { "sTitle": "Employee", "bSortable": true }
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

    function editBug() {
        try {
            $(document).on("click", "button[data-edittype]", function (e) {
                var editID = $(this).attr("data-edittype");
                window.location = "/Bug/Edit?id=" + editID;
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    function deleteBug() {
        try {
            $(document).on("click", "button[data-deletetype]", function (e) {
                var deleteID = $(this).attr("data-deletetype");

                bootbox.confirm("Are you sure you want to delete this bug?", function (result) {
                    if (result == true) {
                        $.ajax({
                            type: 'post',
                            url: '/Bug/Remove',
                            data: { id: deleteID },
                            dataType: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    window.location.href = "/Bug/Index";
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
            editBug();
            getData();
            deleteBug();
        }
    };
}();