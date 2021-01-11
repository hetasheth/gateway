var formValidator = function () {

    var loadAllBugs = function () {
        var value = $("#Emp_id").val();
        var pid = $("#Project_id").val();
        var date = $('#date').val();
        var status = $("#Status").val();
        try {
            $.ajax({
                type: 'post',
                url: '/Home/GetReportBugByParam',
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
        }
        catch (err) {
            console.log(err);
        }
    };

    var addDatePicker = function () {
        $("#date").datepicker({
            format: 'dd-M-yyyy',
            autoclose: true
        }).on('change', function () {
            var value = $("#Emp_id").val();

            if (value == "" || value == null) {
                value = 0;
            }
            var date = $(this).val();
            $('#date').val(date);
            empChange();
        });
    }

    var loadEmployee = function () {
        try {
            $.ajax({
                type: 'post',
                url: '/Home/loadEmp',
                datatype: 'json',
                success: function (response) {
                    if (response.success == true && response.Data.length > 0) {
                        var lst = response.Data;
                        var items = '<option value="" disabled selected>Select Employee </option>';

                        if (lst.length > 0) {
                            $.each(lst, function (i, user) {
                                items += "<option value='" + user.Emp_id + "'>" + user.First_name + " " + user.Last_name + "</option>";
                            });
                            $('#Emp_id').html(items);
                            $('#Emp_id').selectpicker('refresh');
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

    var loadProject = function (value) {
        $.ajax({
            type: 'POST',
            url: '/Task/LoadProject',
            data: { eid: value },
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
                        $('#Project_id').selectpicker('refresh');
                    }
                }
            }
        });
    };

    var empChange = function () {
        var value = $("#Emp_id").val();
        var pid = $("#Project_id").val();
        var date = $('#date').val();
        var status = $("#Status").val();
        $.ajax({
            type: 'POST',
            url: '/Home/GetReportBug',
            data: { eid: value,pid:pid, date: date,status:status },
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

        $("#Emp_id").change(function () {
            r = [];
            var value = $("#Emp_id").val();
            if (value == "" || value == null) {
                value = 0;
            }
            $('#Project_id').val('').selectpicker("refresh");
            loadProject(value);
        });
        $("#Project_id").change(function () {
            r = [];
            var pid = $("#Project_id").val();
            if (pid == "" || pid == null) {
                pid = 0;
            }
            empChange();
        });
        $("#Status").change(function () {
            r = [];
            var value = $("#Emp_id").val();
            var status = $("#Status").val();
            if (status == "" || status == null) {
                status = "";
            }
            empChange();
        });
    }

    function fillData(data) {
        try {
            var arrayReturn = [];
            for (var i = 0, len = data.length; i < len; i++) {
                var result = data[i];
                arrayReturn.push([result.Project_name, result.Task_name, result.Bug_description, result.Date, result.Status]);
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
                    { "sTitle": "Bug", "bSortable": true },
                    { "sTitle": "Date", "bSortable": true },
                    { "sTitle": "Status", "bSortable": true },
                ],
                "bDestroy": true,
                "paging": false,
                "info": true
            });

        }
        catch (err) {
            console.log(err);
        }
    }

    return {
        init: function () {
            loadAllBugs();
            loadEmployee();
            addDatePicker();
            DropdownChange();
        }
    };
}();