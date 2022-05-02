"use strict";

var IAProcess = function () {


    var initTableSearch = function () {
        var table = $('#kt_table_1');
        var tbody = $('#kt_table_1 > tbody');

        $.ajax({
            url: '/home/loadDataIA',
            method: "GET",
            success: function (data) {
                console.log(data);

                tbody.empty();
                $.each(data, function (a, b) {
                    var icon = "<span class='glyphicon glyphicon-remove'></span>";
                    if ((parseFloat(b.Confidence) >= 0.7 && parseFloat(b.Confidence) <= 0.9) && (parseFloat(b.Lift) > 1)) {
                        icon = "<span class='glyphicon glyphicon-ok'></span>";
                    }

                    tbody.append("<tr><td>" + b.Combination + "</td>" +
                        "<td>" + b.Remaining + "</td>" +
                        "<td>" + b.Confidence + "</td>" +
                        "<td>" + b.Lift + "</td>"+
                        "<td>" + icon + "</td></tr > ");
                });

                table.DataTable({
                    language: {
                        url: "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
                    },
                    responsive: true,
                });
            }
        }); 
    }

    var buttonForm = function () {
        $('#btnSubmit').click(function(){
            addLoading();
        });
    }


    return {
        // public functions
        init: function () {
            initTableSearch();
            buttonForm();
        }
    };
}();


jQuery(document).ready(function () {
    IAProcess.init();
    
});
