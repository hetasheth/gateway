var formValidator = function () {

    var loadCompleteTask = function () {
        try{
            $.ajax({
                type: 'post',
                url: '/Home/GetComTask',
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
            autoclose:true
        }).on('change', function () {
            var value = $("#Emp_id").val();

            if (value == "" || value == null) {
                value = 0;
            }
            var date = $(this).val();
            $('#date').val(date);
            empChange(value);
        });
    }

    var loadEmployee = function () {
        try {
            $.ajax({
                type: 'post',
                url: '/Home/loadEmployee',
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

    var empChange = function (value) {
        var date = $('#date').val();
        $.ajax({
            type: 'POST',
            url: '/Home/GetEmpTask',
            data: { id: value,dt:date },
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
            var date = $('#date').val();
            empChange(value);
        });
    }

    function fillData(data) {
        try {
            var arrayReturn = [];
            for (var i = 0, len = data.length; i < len; i++) {
                var result = data[i];
                arrayReturn.push([result.Project_name,result.Task_name, result.End_date, result.Status]);
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
                    { "sTitle": "Completed On", "bSortable": true },
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
            addDatePicker();
            loadCompleteTask();
            loadEmployee();
            DropdownChange();
            empChange();
        }
    };
}();