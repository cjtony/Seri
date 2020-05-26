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
    $("#v-pills-profile-tab").on("click", function () {
        LoadTabFechasPeriodos();
    });
    LoadTabFechasPeriodos = () => {
        $.ajax({
            url: "../Catalogos/LoadFechasPeriodos",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                
                var tab = document.getElementById("bodytab-fechas-periodos");
                tab.innerHTML = "";
                var empresa;
                for (var i = 0; i < data.length; i++) {
                    if (i == 0) {
                        empresa = data[i]["Empresa_id"];
                    }
                    if (data[i]["Empresa_id"] == empresa && data[i]["Periodo"] == 1) {
                        console.log(data[i]["Empresa_id"] + " - " + data[i]["Periodo"]);
                        console.log(data[i]);
                        tab.innerHTML += "" +
                            "<tr>" +
                            "<td colspan='3' >" +
                            "<div class='col-md-12'>" +
                            "<label>" + data[i]['Empresa_id'] + " " + data[i]['NombreEmpresa'] + " - " + data[i]['Tipo_Periodo_Id'] + "" + data[i]["DescripcionTipoPeriodo"] + " - Quincenal</label>&nbsp;&nbsp;&nbsp;<div class='badge badge-success btn' onclick='LoadDetalleFechasPeriodo(\"collapse-"+data[i]["NombreEmpresa"]+"\", "+data[i]["Empresa_id"]+");'><i class='fas fa-plus'></i></div>" +
                            "<div id='collapse-"+data[i]["NombreEmpresa"]+"' class='collapse collapse-" + data[i]['NombreEmpresa'] + " col-md-12'>" +
                            "</div>" +
                            "</div>" +
                            "</td >" +
                            "</tr >";
                        
                    } else {
                        empresa = data[i]["Empresa_id"]
                        if (data[i]["Empresa_id"] == empresa && data[i]["Periodo"] == 1) {
                            //console.log(data[i]["Empresa_id"] + " - " + data[i]["Periodo"]);
                            tab.innerHTML += "" +
                                "<tr>" +
                                "<td colspan='3' >" +
                                "<div class='col-md-12'>" +
                                "<label>" + data[i]['Empresa_id'] + " " + data[i]['NombreEmpresa'] + " - " + data[i]['Tipo_Periodo_Id'] + "" + data[i]["DescripcionTipoPeriodo"] + " - Quincenal</label>&nbsp;&nbsp;&nbsp;<div class='badge badge-success btn' onclick='LoadDetalleFechasPeriodo(\"collapse-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ");'><i class='fas fa-plus'></i></div>" +
                                "<div id='collapse-" + data[i]["NombreEmpresa"] + "' class='collapse collapse-" + data[i]['NombreEmpresa'] + " col-md-12'>" +
                                "</div>" +
                                "</div>" +
                                "</td >" +
                                "</tr >";
                        }
                    }
                }
                //$("#v-pills-profile-tab").click();
            }
        });
    }
    
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
                    "<th class='col-md-2'>No. Periodo</th>" +
                    "<th class='col-md-2'>Fecha Inicio</th>" +
                    "<th class='col-md-2'>Fecha Final</th>" +
                    "<th class='col-md-2'>Fecha Proceso</th>" +
                    "<th class='col-md-2'>Fecha Pago</th>" +
                    "<th class='col-md-2'>Días Efectivos</th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='tab" + pilltab + "'></tbody>" + "</table>";
                for (var j = 0; j < data.length; j++) {

                    document.getElementById("tab" + pilltab).innerHTML += "<tr>" +
                            "<td class='col-md-2'>" + data[j]["Periodo"] + "</td>" +
                            "<td class='col-md-2'>" + data[j]["Fecha_Inicio"] + "</td>" +
                            "<td class='col-md-2'>" + data[j]["Fecha_Final"] + "</td>" +
                            "<td class='col-md-2'>" + data[j]["Fecha_Proceso"] + "</td>" +
                            "<td class='col-md-2'>" + data[j]["Fecha_Pago"] + "</td>" +
                            "<td class='col-md-2'>" + data[j]["Dias_Efectivos"] + "</td>" +
                        "</tr>";
                }
                
                $("#" + pilltab).collapse("toggle");
            }

        });
    }

});