var formValidator = function () {

    function getData() {
        try {
            $.ajax({
                type: 'post',
                url: '/ProjectType/LoadProjectType',
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
                    act += "<button id='btnEdit" + i + "' class='btnEdit' data-edittype=" + result.Type_id + "><i class='fa fa-pencil'></i></button>";
                }
                if ($('#Remove').val() == 'T') {
                    act += "<button id='btnDlt" + i + "' class='btnDlt' data-deletetype=" + result.Type_id + "><i class='fa fa-times'></i></button>";
                }
                act += "</div>";
                arrayReturn.push([act, result.Type, result.Description]);
            }

            var Table =  $('#myTable').dataTable({
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
                    { "sTitle": "Type", "bSortable": true },
                    { "sTitle": "Description", "bSortable": false }
                ],
                "bDestroy": true,
                "paging": true,
                "info": true
            });
            if ($('#CreateOrEdit').val() == 'F' && $('#Remove').val() == 'F')
            {
                Table.api().columns([0]).visible(false, false);
                Table.api().columns.adjust().draw(false);
            }
        }
        catch (err) {
            console.log(err);
        }
    }

    function editProjectType() {
        try {
            $(document).on("click", "button[data-edittype]", function (e) {
                var editID = $(this).attr("data-edittype");
                window.location = "/ProjectType/Edit?id=" + editID;
            });
        }
        catch (err) {
            console.log(err);
        }
    };

    function deleteProjectType() {
        try {
            $(document).on("click", "button[data-deletetype]", function (e) {
                var deleteID = $(this).attr("data-deletetype");

                bootbox.confirm("Are you sure you want to delete this project type?", function (result) {
                    if (result == true) {
                        $.ajax({
                            type: 'post',
                            url: '/ProjectType/Remove',
                            data: { id: deleteID },
                            dataType: 'json',
                            success: function (response) {
                                if (response.success == true) {
                                    window.location.href = "/ProjectType/Index";
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
            editProjectType();
            getData();
            deleteProjectType();
        }
    };
}();