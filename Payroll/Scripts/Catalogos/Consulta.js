﻿$(function () {
    //CARGA TABLA DE EMPRESAS CON PERIODOS
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
                            "<div class='col-md-12 row'>" +
                            "<label class='col-md-1'>" + data[i]['Empresa_id'] + "</label><label class='col-md-3'>" + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]['Tipo_Periodo_Id'] + " - " + data[i]["DescripcionTipoPeriodo"] + "</label><div class='col-md-5'><div class='badge badge-success btn' onclick='LoadDetalleFechasPeriodo(\"collapse-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ");'>Ver <i class='fas fa-plus'></i></div></div>" +
                            "<div id='collapse-" + data[i]["NombreEmpresa"] + "' class='collapse collapse-" + data[i]['NombreEmpresa'] + " col-md-12'>" +
                            "</div>" +
                            "</div>" +
                            "</td >" +
                            "</tr >";

                    } else {
                        empresa = data[i]["Empresa_id"];
                        if (data[i]["Empresa_id"] == empresa && data[i]["Periodo"] == 1) {
                            //console.log(data[i]["Empresa_id"] + " - " + data[i]["Periodo"]);
                            tab.innerHTML += "" +
                                "<tr>" +
                                "<td colspan='3' >" +
                                "<div class='col-md-12 row'>" +
                                "<label class='col-md-1'>" + data[i]['Empresa_id'] + " </label><label class='col-md-3'> " + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]['Tipo_Periodo_Id'] + " - " + data[i]["DescripcionTipoPeriodo"] + "</label><div class='col-md-5'><div class='badge badge-success btn' onclick='LoadDetalleFechasPeriodo(\"collapse-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ");'>Ver <i class='fas fa-plus'></i></div></div>" +
                                "<div id='collapse-" + data[i]["NombreEmpresa"] + "' class='collapse collapse-" + data[i]['NombreEmpresa'] + " col-md-12'>" +
                                "</div>" +
                                "</div>" +
                                "</td >" +
                                "</tr >";
                        }
                    }
                }
            }
        });
    }
    //CARGA TABLAS DE POLITICAS DE VACACIONES
    LoadTabPoliticasVacaciones = () => {
        $.ajax({
            url: "../Catalogos/LoadPoliticasVacaciones",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var tab = document.getElementById("bodytab-politicas-vacaciones");
                tab.innerHTML = "";
                var empresa;
                for (var i = 0; i < data.length; i++) {
                    if (i == 0) {
                        empresa = data[i]["Empresa_id"];
                    }
                    if (data[i]["Empresa_id"] == empresa) {
                        //console.log(data[i]["Empresa_id"] + " - " + data[i]["Periodo"]);
                        //console.log(data[i]);
                        tab.innerHTML += "" +
                            "<tr>" +
                            "<td colspan='3' >" +
                            "<div class='col-md-12 row'>" +
                            "<label class='col-md-1'>" + data[i]['Empresa_id'] + "</label><label class='col-md-3'> " + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]["Effdt"] + "</label><div class='col-md-5'><div class='badge badge-secondary btn' onclick='LoadDetallePoliticas(\"collapsetab-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ");'>Ver <i class='fas fa-plus'></i></div></div>" +
                            "<div id='collapsetab-" + data[i]["NombreEmpresa"] + "' class='collapse collapse-" + data[i]['NombreEmpresa'] + " col-md-12'>" +
                            "</div>" +
                            "</div>" +
                            "</td >" +
                            "</tr >";
                    } else {
                        empresa = data[i]["Empresa_id"];
                        if (data[i]["Empresa_id"] == empresa) {
                            //console.log(data[i]["Empresa_id"] + " - " + data[i]["Periodo"]);
                            tab.innerHTML += "" +
                                "<tr>" +
                                "<td colspan='3' >" +
                                "<div class='col-md-12 row'>" +
                                "<label class='col-md-1'>" + data[i]['Empresa_id'] + "</label><label class='col-md-3'> " + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]["Effdt"] + "</label><div class='col-md-5'><div class='badge badge-secondary btn' onclick='LoadDetallePoliticas(\"collapsetab-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ");'>Ver <i class='fas fa-plus'></i></div></div>" +
                                "<div id='collapsetab-" + data[i]["NombreEmpresa"] + "' class='collapse collapse-" + data[i]['NombreEmpresa'] + " col-md-12'>" +
                                "</div>" +
                                "</div>" +
                                "</td >" +
                                "</tr >";
                        }
                    }
                }
                LoadPoliticasVacacionesFuturas();
            }
        });
    }
    //SUB-LLENADO DE POLITICAS DE VACACIONES EN TABLA DE POLITICAS FUTURAS
    LoadPoliticasVacacionesFuturas = () => {
        $.ajax({
            url: "../Catalogos/LoadPoliticasVacacionesFuturas",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var tab = document.getElementById("bodytab-politicas-vacaciones-futuras");
                tab.innerHTML = "";
                var empresa;
                for (var i = 0; i < data.length; i++) {
                    if (i == 0) {
                        empresa = data[i]["Empresa_id"];
                    }
                    if (data[i]["Empresa_id"] == empresa) {
                        //console.log(data[i]["Empresa_id"] + " - " + data[i]["Periodo"]);
                        //console.log(data[i]);
                        tab.innerHTML += "" +
                            "<tr>" +
                            "<td colspan='3' >" +
                            "<div class='col-md-12 row'>" +
                            "<label class='col-md-1'>" + data[i]['Empresa_id'] + "</label><label class='col-md-3'> " + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]["Effdt"] + "</label><div class='col-md-5'><div class='badge badge-secondary btn' onclick='LoadPoliticasVacaciones_Futuras_Detalle(\"collapsetab-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ", \"" + data[i]["Effdt"] + "\");'>Ver <i class='fas fa-plus'></i></div></div>" +
                            "<div id='collapsetab-" + data[i]["NombreEmpresa"] + "' class='collapse collapse-" + data[i]['NombreEmpresa'] + " col-md-12'>" +
                            "</div>" +
                            "</div>" +
                            "</td >" +
                            "</tr >";

                    } else {
                        empresa = data[i]["Empresa_id"];
                        if (data[i]["Empresa_id"] == empresa) {
                            //console.log(data[i]["Empresa_id"] + " - " + data[i]["Periodo"]);
                            tab.innerHTML += "" +
                                "<tr>" +
                                "<td colspan='3' >" +
                                "<div class='col-md-12 row'>" +
                                "<label class='col-md-1'>" + data[i]['Empresa_id'] + "</label><label class='col-md-3'> " + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]["Effdt"] + "</label><div class='col-md-5'><div class='badge badge-secondary btn' onclick='LoadPoliticasVacaciones_Futuras_Detalle(\"collapsetab-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ", \"" + data[i]["Effdt"] + "\");'>Ver <i class='fas fa-plus'></i></div></div>" +
                                "<div id='collapsetab-" + data[i]["NombreEmpresa"] + "' class='collapse collapse-" + data[i]['NombreEmpresa'] + " col-md-12'>" +
                                "</div>" +
                                "</div>" +
                                "</td >" +
                                "</tr >";
                        }
                    }
                }
            }
        });
    }
    //MUESTRA LOS PERIODOS POR EMPRESA
    LoadDetalleFechasPeriodo = (pilltab, Empresa_id) => {
        $.ajax({
            url: "../Catalogos/LoadFechasPeriodosDetalle",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                console.log(pilltab);
                document.getElementById(pilltab).innerHTML = "";
                document.getElementById(pilltab).innerHTML += "<table class='table table-sm table-in-fechas-periodos text-center col-md-12'>" +
                    "<thead class='col-md-12'>" +
                    "<tr>" +
                    "<th>No. Periodo</th>" +
                    "<th scope='col'><i class='fas fa-key'><i/></th>" +
                    "<th>Fecha Inicio</th>" +
                    "<th>Fecha Final</th>" +
                    "<th>Fecha Proceso</th>" +
                    "<th>Fecha Pago</th>" +
                    "<th>Días Efectivos</th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='tab" + pilltab + "' class=''></tbody>" + "</table>";
                for (var j = 0; j < data.length; j++) {
                    if (data[j]["Nomina_Cerrada"] == "True") {
                        document.getElementById("tab" + pilltab).innerHTML += "<tr>" +
                            "<td class=''>" + data[j]["Periodo"] + "</td>" +
                            "<td>" + "<div class='badge badge-light'><i class='fas fa-lock text-warning'></i></div>" + "</td>" +
                            "<td class=''>" + data[j]["Fecha_Inicio"] + "</td>" +
                            "<td class=''>" + data[j]["Fecha_Final"] + "</td>" +
                            "<td class=''>" + data[j]["Fecha_Proceso"] + "</td>" +
                            "<td class=''>" + data[j]["Fecha_Pago"] + "</td>" +
                            "<td class=''>" + data[j]["Dias_Efectivos"] + "</td>" +
                            "</tr>";
                    } else {
                        document.getElementById("tab" + pilltab).innerHTML += "<tr>" +
                            "<td class=''>" + data[j]["Periodo"] + "</td>" +
                            "<td>" + "<div class='badge badge-light'><i class='fas fa-unlock-alt text-primary'></i></div>" + "</td>" +
                            "<td class=''>" + data[j]["Fecha_Inicio"] + "</td>" +
                            "<td class=''>" + data[j]["Fecha_Final"] + "</td>" +
                            "<td class=''>" + data[j]["Fecha_Proceso"] + "</td>" +
                            "<td class=''>" + data[j]["Fecha_Pago"] + "</td>" +
                            "<td class=''>" + data[j]["Dias_Efectivos"] + "</td>" +
                            "</tr>";
                    }



                }
                $(".collapse").collapse("hide");
                $("#" + pilltab).collapse("toggle");
            }

        });
    }
    //MUESTRA LAS POLITICAS POR EMPRESA
    LoadDetallePoliticas = (pilltab, Empresa_id) => {
        $.ajax({
            url: "../Catalogos/LoadPoliticasVacacionesDetalle",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                //console.log(data);
                //console.log(pilltab);
                document.getElementById(pilltab).innerHTML = "";
                document.getElementById(pilltab).innerHTML += "<table class='table table-sm table-in-politicas-vacaciones col-md-8'>" +
                    "<thead class='col-md-12'>" +
                    "<tr>" +
                    "<th class=''> Años </th>" +
                    "<th class=''> Dias </th>" +
                    "<th class=''> Prima vacacional </th>" +
                    "<th class=''> Dias Aguinaldo </th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='tabp" + pilltab + "'></tbody>" + "</table>";
                for (var j = 0; j < data.length; j++) {
                    if (j == 0) {
                        console.log("NOMBRE EMPRESA " + data[j]["NombreEmpresa"]);
                        document.getElementById("politicas-modal-title").innerHTML = data[j]["NombreEmpresa"];
                    }
                    document.getElementById("tabp" + pilltab).innerHTML += "<tr>" +
                        "<td class=''>" + data[j]["Anos"] + "</td>" +
                        "<td class=''>" + data[j]["Dias"] + "</td>" +
                        "<td class=''>" + data[j]["Prima_Vacacional_Porcen"] + "</td>" +
                        "<td class=''>" + data[j]["Dias_Aguinaldo"] + "</td>" +
                        "</tr>";
                }
                //console.log($("#tabp" + pilltab).html());
                document.getElementById("modal-body-politicas").innerHTML = $("#" + pilltab).html();
                //$("#" + pilltab).collapse("toggle");

                $("#modalMostrarPoliticas").modal("show");
            }

        });
    }
    //LLENADO DE POLITICAS POR EMPRESA Y EFFDT 
    LoadPoliticasVacaciones_Futuras_Detalle = (pilltab, Empresa_id, Effdt) => {
        $.ajax({
            url: "../Catalogos/LoadPoliticasVacaciones_Futuras_Detalle",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id, Effdt: Effdt }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                //console.log(data);
                //console.log(pilltab);
                document.getElementById(pilltab).innerHTML = "";
                document.getElementById(pilltab).innerHTML += "<table class='table table-sm table-in-politicas-vacaciones col-md-8'>" +
                    "<thead class='col-md-12'>" +
                    "<tr>" +
                    "<th class=''> Años </th>" +
                    "<th class=''> Dias </th>" +
                    "<th class=''> Prima vacacional </th>" +
                    "<th class=''> Dias Aguinaldo </th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='tabp" + pilltab + "'></tbody>" + "</table>";
                for (var j = 0; j < data.length; j++) {
                    if (j == 0) {
                        console.log("NOMBRE EMPRESA " + data[j]["NombreEmpresa"]);
                        document.getElementById("politicas-modal-title").innerHTML = data[j]["NombreEmpresa"];
                    }
                    document.getElementById("tabp" + pilltab).innerHTML += "<tr>" +
                        "<td class=''>" + data[j]["Anos"] + "</td>" +
                        "<td class=''>" + data[j]["Dias"] + "</td>" +
                        "<td class=''>" + data[j]["Prima_Vacacional_Porcen"] + "</td>" +
                        "<td class=''>" + data[j]["Dias_Aguinaldo"] + "</td>" +
                        "</tr>";
                }
                //console.log($("#tabp" + pilltab).html());
                document.getElementById("modal-body-politicas").innerHTML = $("#" + pilltab).html();
                //$("#" + pilltab).collapse("toggle");

                $("#modalMostrarPoliticas").modal("show");
            }

        });
    }
    //LLENADO DE EMPRESAS EN VISTA EMPLEADO
    LoadBodyCards = () => {
        $.ajax({
            url: "../Catalogos/LoadEmpresasNEmpleados",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                var tab = document.getElementById("body-cards-empresas");
                tab.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    tab.innerHTML += "<div class='col-md-12 row'>"
                        //+ "<div class=' p-0 row'>"
                        + "<small class='col-md-8'>"+ data[i]["Empresa_id"] + " " + data[i]["NombreEmpresa"] +"</small>"
                        + "<small class=''><span class='badge badge-primary'><i class='fas fa-users'></i>&nbsp;&nbsp;&nbsp;" + data[i]["No"] + "</span></small>"
                        //+ "</div>"
                        + "</div>";
                }
            }
        }); 
    }
    //LLENA SELECT EMPRESAS
    LoadSelectEmpresas = () => {
        $.ajax({
            url: "../Empresas/LoadSEmp",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                var select = document.getElementById("inEmpresa");
                select.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    select.innerHTML += "<option value='" + data[i]["IdEmpresa"] + "'>" + data[i]["IdEmpresa"] + "&nbsp;&nbsp;" + data[i]["NombreEmpresa"] + "</option>";
                }
            }
        });
    }
    //BUSQUEDA DE EMPLEADOS
    $("#inSearch").on("keyup", function () {
        $("#result").empty();
        var txt = $("#inSearch").val();
        var Empresa_id = $("#inEmpresa").val();
        if ($("#inSearch").val() != "") {
            var txtSearch = { "txtSearch": txt, "Empresa_id": Empresa_id };
            $.ajax({
                url: "../Empleados/SearchEmpleadosM",
                type: "POST",
                cache: false,
                data: JSON.stringify(txtSearch),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    console.log(data);
                    $("#result").empty();
                    if (data[0]["iFlag"] == 0) {
                        for (var i = 0; i < data.length; i++) {
                            $("#result").append("<button class='text-left list-group-item list-group-item-action font-labels' onclick='MostrarDataEmpleado("+ Empresa_id +","+ data[i]["IdEmpleado"] + ")'><i class='far fa-user-circle text-primary'></i> " + data[i]["Nombre_Empleado"] + " " + data[i]["Apellido_Paterno_Empleado"] + ' ' + data[i]["Apellido_Materno_Empleado"] + "<br><small><i class='fas fa-briefcase text-warning'></i> " + data[i]["DescripcionDepartamento"] + " - " + data[i]["DescripcionPuesto"] + "</small></button>");
                        }
                    }
                    else {
                        $("#result").append("<button type='button' class='text-left list-group-item list-group-item-action font-labels'  >" + data[0]["Nombre_Empleado"] + "<br><small class='text-muted'>" + data[0]["DescripcionPuesto"] + "</small> </button>");
                    }
                }
            });
        } else {
            $("#result").empty();
        }
    });
    //MOSTRAR DATOS DEL EMPLEADO DE LA BUSQUEDA
    MostrarDataEmpleado = (Empresa_id,Empleado_id) => {
        $.ajax({
            url: "../Empleados/SearchDataEmpleado",
            type: "POST",
            cache: "false",
            data: JSON.stringify({Empresa_id:Empresa_id,Empleado_id:Empleado_id}),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data.length);
                //$("#collapseEmp").html(data);
                for (var i = 0; i < data.length; i++) {
                    var j = i + 1;
                    $(".lbl-" + j).val(data[i]);
                }
                $(".collapseDataEmp").collapse("show");
                $("#modalBusquedaEmpleado").modal("hide");
            }
        });
    }
    //CARGA TABLA DE EMPRESAS Y NUMERO DE PUESTOS
    LoadTabPuestos = () => {
        $.ajax({
            url: "../Catalogos/LoadPuestosEmpresas",
            type: "POST",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                var tab = document.getElementById("bodytab-puestos");
                tab.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    tab.innerHTML += "" +
                        "<tr>" +
                        "<td colspan='3' >" +
                        "<div class='col-md-12 row m-0 p-0'>" +
                        "<label class='col-md-1'>" + data[i][0] + "</label><label class='col-md-3'>" + data[i][1] + " </label><label class='col-md-3'><span class='badge badge-primary'><i class='fas fa-briefcase text-warning'></i> " + data[i][2] + "</></label><div class='col-md-5'><div class='badge badge-success btn' onclick='irabuscar("+data[i][0]+");'>Ver <i class='fas fa-plus'></i></div></div>" +
                        "</div>" +
                        "</td >" +
                        "</tr >";
                }
            }
        });
    }
    //CARGA
    //BUSQUEDA DE PUESTOS
    $("#inSearchPuesto").on("keyup", function () {
        $("#resultPuesto").empty();
        var txt = $("#inSearchPuesto").val();
        var Empresa_id = $("#inEmpresa").val();
        if ($("#inSearchPuesto").val() != "") {
            var txtSearch = { "Empresa_id": Empresa_id, "Search": txt };
            $.ajax({
                url: "../Catalogos/SearchPuesto",
                type: "POST",
                cache: false,
                data: JSON.stringify(txtSearch),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    console.log(data);
                    $("#resultPuesto").empty();
                    if (data[0]["iFlag"] == 0) {
                        for (var i = 0; i < data.length; i++) {
                            $("#resultPuesto").append("<button class='text-left list-group-item list-group-item-action font-labels' onclick='MostrarDataPuesto(" + Empresa_id + "," + data[i]["idPuesto"] + ")'><i class='fas fa-briefcase text-warning'></i> " + data[i]["PuestoCodigo"] + " " + data[i]["NombrePuesto"] + "</button>");
                        }
                    }
                    else {
                        $("#resultPuesto").append("<button type='button' class='text-left list-group-item list-group-item-action font-labels'  >" + data[0]["NombrePuesto"] + "<br><small class='text-muted'>" + data[0]["DescripcionPuesto"] + "</small> </button>");
                    }
                }
            });
        } else {
            $("#resultPuesto").empty();
        }
        //MUESTRA EL PUESTO SELECCIONADO
        MostrarDataPuesto = (Empresa_id,Puesto_id) => {
            $.ajax({
                url: "../Catalogos/LoadPuesto",
                type: "POST",
                cache: "false",
                data: JSON.stringify({ Empresa_id: Empresa_id, Puesto_id: Puesto_id }),
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    
                    $("#inp-0").val(data[0]["PuestoCodigo"]);
                    $("#inp-1").val(data[0]["NombrePuesto"]);
                    $("#inp-2").val(data[0]["DescripcionPuesto"]);
                    $("#inp-3").val(data[0]["NombreProfesion"]);
                    $("#inp-4").val(data[0]["ClasificacionPuesto"]);
                    $("#inp-5").val(data[0]["Colectivo"]);
                    $("#inp-6").val(data[0]["NivelJerarquico"]);
                    $("#inp-7").val(data[0]["fecha_alta"].substring(0,10));
                    $("#inp-8").val(data[0]["PerformanceManager"]);
                    
                    $("#resultPuesto").empty();
                    $("#inSearchPuesto").val("");

                }
            });
        }
        //SELECCIONAR LA EMPRESA CON LA QUE SE QUIERE BUSCAR
        irabuscar = (Empresa_id) => {
            $("#inEmpresa option[value='" + Empresa_id + "']").attr("selected", true);
            $("#inSearchPuesto").focus();
        }
    });
    // BOTON QUE EXPORTA LOS PUESTOS A UN ARCHIVO CSV 
    $("#btnExportToCSV").on("click", function () {

        const headers = {
            id: 'Id Puesto',
            empresa_id: 'Empresa',
            codigo: 'Codigo Puesto',
            nombre: 'Nombre del puesto',
            descripcion: 'Descripcion',
            profecion: 'Profesion',
            clasificacion: 'Clasificacion',
            colectivo: 'Colectivo',
            nivel_jerarquico: 'Nivel Jerarquico',
            performance: 'Performance Manager',
            tabulador: 'Tabulador',
            fecha_alta: 'Fecha Alta'
        };
        //
        $.ajax({
            url: "../Catalogos/LoadAllPuestos",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                
            }
        });

        //
        const data = [
            {
                id: '1', empresa_id: '2', codigo: 'Emp23', nombre: 'Puesto1',
                descripcion: 'Descripcion', profecion: 'Profesion', clasificacion: 'Clasificacion', colectivo: 'Colectivo',
                nivel_jerarquico: 'Nivel Jerarquico', performance: 'Performance Manager', tabulador: 'Tabulador', fecha_alta: 'Fecha Alta'
            }

        ];

        exportCSVFile(headers, data, 'Lista de Puestos');

    });

    //LLENA GRUPOS EMPRESAS HEADER
    LoadAcordeonGrupos = () => {
        $.ajax({
            url: "../Catalogos/LoadGruposEmpresas",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                var acordeon = document.getElementById("accordionGruposEmpresas");
                acordeon.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    acordeon.innerHTML += ""
                        + "<div class='card'>"
                        + "<div class='card-header' id='heading" + data[i][0] +"'>"
                        + "<h2 class='mb-0'>"
                        + "<button class='btn btn-link btn-block d-flex justify-content-between aling-items-center' onclick='MostrarEmpresasEnGrupo(\"" + data[i][0] +"\",\"ul"+data[i][0]+"\",\"collapse"+data[i][0]+"\")' type='button'>"
                        + "" + data[i][1] + ""
                        + "</button>"
                        + "</h2>"
                        + "</div>"
                        + "<div id='collapse" + data[i][0] + "' class='collapse' aria-labelledby='heading" + data[i][0] +"' data-parent='#accordionGruposEmpresas'>"
                        + "<ul id='ul"+data[i][0]+"' class='list-group list-group-flush'>"
                        + "</ul>"
                        + "</div>"
                        + "</div >";
                }
            }
        });
    }
    //LLENA GRUPOS EMPRESAS LINE
    MostrarEmpresasEnGrupo = (Grupo_id,ul,collapse) => {
        $.ajax({
            url: "../Catalogos/LoadEmpresasGrupo",
            type: "POST",
            data: JSON.stringify({ Grupo_id: Grupo_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                var lista = document.getElementById(ul)
                lista.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    lista.innerHTML += "<li class='list-group-item list-group-item-secondary'>" + data[i][0] + " - " + data[i][1] + "</li>";
                }
                $("#" + collapse).collapse("toggle");
            }
        });
    }
});