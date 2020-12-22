$(document).ready(function () {
    $(document).ready(function () {
        formValidator.init();
    });

    var formValidator = function () {

        var addDatePicker = function () {

            $("#DOB").datepicker({
                format: 'dd-M-yyyy',
                minDate: '-80Y',
                maxDate: '-18Y',
                autoclose: true
            }).on('change', function () {
                var db = $('#DOB').val();
                if (isNaN(db)) {
                    var dt = db.split("/");
                    var y = parseInt(dt[2]);
                    var lastDate = new Date();
                    if (y > lastDate.getFullYear() - 18) {
                        //alert("You must be adult");
                        $("#errDOB").removeClass('no-error').addClass('show-error');
                        $('#DOB').val("");
                    }
                    else {
                        $("#errDOB").addClass('no-error').removeClass('show-error');
                    }
                }
            });

        };

        var validatePhoto = function () {
            $("#Photo").on('change', function () {
                var fileExtension = ['jpeg', 'jpg', 'png','gif'];
                var pic = $("#Photo").val();
                var ext = pic.substring(pic.lastIndexOf('.') + 1).toLowerCase();
                if ($.inArray(ext, fileExtension) == -1) {
                    $("#errPhoto").removeClass('no-error').addClass('show-error');
                    $("#Photo").val("");
                }
                else {
                    $("#errPhoto").addClass('no-error').removeClass('show-error');
                }
            });
            
        };
        
        return {
            init: function () {
                addDatePicker();
                validatePhoto();
            }
        };

    }();
});

