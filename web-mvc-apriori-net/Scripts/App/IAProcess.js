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
                    tbody.append("<tr><td>" + b.Combination + "</td>" +
                        "<td>" + b.Remaining + "</td>" +
                        "<td>" + b.Confidence + "</td>" +
                        "<td>" + b.Lift + "</td></tr>");
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
