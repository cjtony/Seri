$(function () {
    $("#pills-catalogos.nav-link.active").addClass('bg-secondary');
    $("#pills-catalogos.nav-link").on("click", function () {
        $("#pills-catalogos.nav-link").removeClass('bg-secondary');
        $(this).addClass('bg-secondary');
    });
    $(".btntab").hover(
        function () {
            $(this).children('.btntittle').removeClass('bg-primary').addClass('bg-dark'); /*.css("background-color", "pink");*/
            //$(this).removeClass('bg-white').addClass('bg-secondary');
            //alert('si');
        },
        function () {
            $(this).children('.btntittle').removeClass('bg-dark').addClass('bg-primary'); /*.css("background-color", "pink");*/
            //$(this).removeClass('bg-secondary').addClass('bg-white');
            //alert('no');
        }
    );
    $("#fechas-periodo").on("click", function () {
        
        //LoadTabFechasPeriodos();
        //console.log(LoadDetalleTab(2));
        $("#v-pills-profile-tab").click();
    });


    
    LoadDetalleFechasPeriodo = (pilltab,Empresa_id) => {
        $.ajax({
            url: "../Catalogos/LoadFechasPeriodosDetalle",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                console.log(pilltab);
                document.getElementById(pilltab).innerHTML = "";
                document.getElementById(pilltab).innerHTML += "<table class='table table-sm table-in-fechas-periodos col-md-12'>" +
                    "<thead class='col-md-12'>" +
                    "<tr>" +
                    "<th class='col'>No. Periodo</th>" +
                    "<th class='col'>Fecha Inicio</th>" +
                    "<th class='col'>Fecha Final</th>" +
                    "<th class='col'>Fecha Proceso</th>" +
                    "<th class='col'>Fecha Pago</th>" +
                    "<th class='col'>Días Efectivos</th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='tab" + pilltab + "'></tbody>" + "</table>";
                for (var j = 0; j < data.length; j++) {

                    document.getElementById("tab" + pilltab).innerHTML += "<tr>" +
                            "<td>" + data[j]["Periodo"] + "<td>" +
                            "<td>" + data[j]["Fecha_Inicio"] + "<td>" +
                            "<td>" + data[j]["Fecha_Final"] + "<td>" +
                            "<td>" + data[j]["Fecha_Proceso"] + "<td>" +
                            "<td>" + data[j]["Fecha_Pago"] + "<td>" +
                            "<td>" + data[j]["Dias_Efectivos"] + "<td>" +
                        "</tr>";

                }
                
                $("#" + pilltab).collapse("toggle");
            }

        });
    }

});