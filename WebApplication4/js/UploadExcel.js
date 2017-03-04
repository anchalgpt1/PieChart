var UploadExcel = {
    excelData: "",

    //function to draw pie chart
    drawPieChart : function() {
        var dtable = JSON.parse(UploadExcel.excelData);
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Department');
        data.addColumn('number', 'Percentage');
        for (var item in dtable) {
            data.addRow(dtable[item]);
        }
        var options = {
            //'legend': { position: 'labeled', alignment: "center", labeledValueText: 'value' },
           // 'legend': { 'position': 'top', 'alignment': 'center' },
            'title': 'Department Wise Sales',
            'is3D': true,
            //s pieSliceText : 'label'
           // 'width': 450,
           // 'height': 400
        };
        var chart = new google.visualization.PieChart(document.getElementById('chartContainer'));

        chart.draw(data, options);
    },

    // Function to register all the click events
    registerEvents : function(){  
        $("#uploadExcel").click(function () {
            if ($('#inpExcel').val() == "") {
                $(".errorFile").removeClass("hide");
                return false;
            }
            else {
                $(".errorFile").addClass("hide");
                return true;
            }
        });
    },

    // Function to be called on page load
    pageLoad: function () {
        UploadExcel.registerEvents();      
    }
};