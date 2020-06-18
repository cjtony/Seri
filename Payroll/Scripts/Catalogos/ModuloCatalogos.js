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
    $(".badge-dark").hover(
        function () {
            //$(this).children('.btntittle').removeClass('bg-primary').addClass('bg-dark'); /*.css("background-color", "pink");*/
            //$(this).removeClass('bg-white').addClass('bg-secondary');
            alert('si');
        },
        function () {
            //$(this).children('.btntittle').removeClass('bg-dark').addClass('bg-primary'); /*.css("background-color", "pink");*/
            //$(this).removeClass('bg-secondary').addClass('bg-white');
            alert('no');
        }
    );
    $("#fechas-periodo").on("click", function () {
        $("#v-pills-FechasPeriodos-tab").click();
    });
    $("#politicas-vacaciones").on("click", function () {
        $("#v-pills-politicas-tab").click();
    });
    $("#puestos").on("click", function () {
        $("#v-pills-puestos-tab").click();
    });
    $("#localidades").on("click", function () {
        $("#v-pills-localidades-tab").click();
    });
    $("#registros-patronales").on("click", function () {
        $("#v-pills-registros-patronales-tab").click();
    });
    $("#centros-costos").on("click", function () {
        $("#v-pills-centros-costos-tab").click();
    });
    $("#sucursales").on("click", function () {
        $("#v-pills-sucursales-tab").click();
    });
    $("#regionales").on("click", function () {
        $("#v-pills-regionales-tab").click();
    });
    $("#v-pills-FechasPeriodos-tab").on("click", function () {
        LoadTabFechasPeriodos();
    });
    $("#v-pills-politicas-tab").on("click", function () {
        LoadTabPoliticasVacaciones();
    });
    $("#v-pills-editar-tab").on("click", function (evt) {
        
        if ($("#bodybotonagregar").html() == '') {
            $("#v-pills-lista-tab").click();
            console.log($("#bodybotonagregar").html());
            Swal.fire({
                icon: 'warning',
                title: 'Atención!',
                text: 'Debe seleccionar una empresa dentro de un catalogo para editar antes de entrar'
            });
            //$("#v-pills-lista-tab").click();
        } else {
            
        }
    });
    // Mostar Modal Agregar NEW EFFDT 
    $("#btn-newEffdtPoliticas").on("click", function () {
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var today = now.getFullYear() + "-" + (month) + "-" + (day);
        $("#newEffdtFecha").attr("min",today);
        LoadSelectEmpresas_sid("newEffdtEmpresa");
        $("#modalAgregarNewEffdtPoliticas").modal("show");
    });
    $('.input-number').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
    // Guardar nueva EFFDT Politicas
    $("#btnneweffdtpoliticas").on("click", function () {
        $.ajax({
            url: "../Catalogos/SaveNewEffdt",
            type: "POST",
            data: JSON.stringify({
                Empresa_id: $("#newEffdtEmpresa").val(),
                Effdt: $("#newEffdtFecha").val()
            }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                if (data[0] == "1") {
                    LoadTabPoliticasVacaciones();
                    LoadPoliticasVacacionesFuturas();
                    document.getElementById("frmnewEffdt").reset();
                    $("#modalAgregarNewEffdtPoliticas").modal("hide");
                    Swal.fire({
                        icon: 'success',
                        title: 'Completado!',
                        text: data[1]
                    });
                } else {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Error!',
                        text: data[1]
                    });
                }
            }
        });
    });

    LoadSelectEmpresas_sid = (select_id) => {
        $.ajax({
            url: "../Empresas/LoadSEmp",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(select_id);
                var select = document.getElementById(select_id);
                //select.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    select.innerHTML += "<option value='" + data[i]["IdEmpresa"] + "'>" + data[i]["IdEmpresa"] + " " + data[i]["NombreEmpresa"] + "</option>"
                }
            }
        });
    }

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
                            "<label class='col-md-1'>" + data[i]['Empresa_id'] + "</label><label class='col-md-3'>" + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]['Tipo_Periodo_Id'] + " - " + data[i]["DescripcionTipoPeriodo"] + "</label><div class='col-md-5'><div class='badge badge-success btn' onclick='LoadDetalleFechasPeriodo(\"collapse-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ");'>Ver <i class='fas fa-plus'></i></div><div class='ml-1 badge badge-primary btn' onclick='mostrarModalNuevoPeriodo(" + data[i]["Empresa_id"] + ","+ data[i]["Anio"] +");'>Nuevo <i class='fas fa-calendar-check'></i></div></div>" +
                            "<div id='collapse-"+data[i]["NombreEmpresa"]+"' class='collapse collapse-" + data[i]['NombreEmpresa'] + " col-md-12'>" +
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
                                "<label class='col-md-1'>" + data[i]['Empresa_id'] + " </label><label class='col-md-3'> " + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]['Tipo_Periodo_Id'] + " - " + data[i]["DescripcionTipoPeriodo"] + "</label><div class='col-md-5'><div class='badge badge-success btn' onclick='LoadDetalleFechasPeriodo(\"collapse-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ");'>Ver <i class='fas fa-plus'></i></div><div class='ml-1 badge badge-primary btn' onclick='mostrarModalNuevoPeriodo(" + data[i]["Empresa_id"] + "," + data[i]["Anio"] +");'>Nuevo <i class='fas fa-calendar-check'></i></div></div>" +
                                "<div id='collapse-" + data[i]["NombreEmpresa"] + "' class='collapse collapse-" + data[i]['NombreEmpresa'] + " col-md-12'>" +
                                "</div>" +
                                "</div>" +
                                "</td >" +
                                "</tr >";
                        }
                    }
                }
                //$("#v-pills-FechasPeriodos-tab").click();
            }
        });
    }

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
                            "<label class='col-md-1'>" + data[i]['Empresa_id'] + "</label><label class='col-md-3'> " + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]["Effdt"] + "</label><div class='col-md-5'><div class='badge badge-secondary btn' onclick='LoadDetallePoliticas(\"collapsetab-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ");'>Ver <i class='fas fa-plus'></i></div><div class='btn badge badge-primary ml-2' onclick='editarPoliticas(" + data[i]["Empresa_id"] + ",\"" + data[i]["Effdt"] + "\");'>Editar <i class='fas fa-edit'></i></div></div>" +
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
                                "<label class='col-md-1'>" + data[i]['Empresa_id'] + "</label><label class='col-md-3'> " + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]["Effdt"] + "</label><div class='col-md-5'><div class='badge badge-secondary btn' onclick='LoadDetallePoliticas(\"collapsetab-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ");'>Ver <i class='fas fa-plus'></i></div><div class='btn badge badge-primary ml-2' onclick='editarPoliticas(" + data[i]["Empresa_id"] + ",\"" + data[i]["Effdt"] + "\");'>Editar <i class='fas fa-edit'></i></div></div>" +
                                "<div id='collapsetab-" + data[i]["NombreEmpresa"] + "' class='collapse collapse-" + data[i]['NombreEmpresa'] + " col-md-12'>" +
                                "</div>" +
                                "</div>" +
                                "</td >" +
                                "</tr >";
                        }
                    }
                }
                //$("#v-pills-FechasPeriodos-tab").click();
                LoadPoliticasVacacionesFuturas();
            }
        });
    }

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
                            "<label class='col-md-1'>" + data[i]['Empresa_id'] + "</label><label class='col-md-3'> " + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]["Effdt"] + "</label><div class='col-md-5'><div class='badge badge-secondary btn' onclick='LoadPoliticasVacaciones_Futuras_Detalle(\"collapsetab-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ", \"" + data[i]["Effdt"] +"\");'>Ver <i class='fas fa-plus'></i></div><div class='btn badge badge-primary ml-2' onclick='editarPoliticasFuturas(" + data[i]["Empresa_id"] + ",\""+ data[i]["Effdt"] +"\");'>Editar <i class='fas fa-edit'></i></div></div>" +
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
                                "<label class='col-md-1'>" + data[i]['Empresa_id'] + "</label><label class='col-md-3'> " + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]["Effdt"] + "</label><div class='col-md-5'><div class='badge badge-secondary btn' onclick='LoadPoliticasVacaciones_Futuras_Detalle(\"collapsetab-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ", \"" + data[i]["Effdt"] + "\");'>Ver <i class='fas fa-plus'></i></div><div class='btn badge badge-primary ml-2' onclick='editarPoliticasFuturas(" + data[i]["Empresa_id"] + ",\"" + data[i]["Effdt"] +"\");'>Editar <i class='fas fa-edit'></i></div></div>" +
                                "<div id='collapsetab-" + data[i]["NombreEmpresa"] + "' class='collapse collapse-" + data[i]['NombreEmpresa'] + " col-md-12'>" +
                                "</div>" +
                                "</div>" +
                                "</td >" +
                                "</tr >";
                        }
                    }
                }
                //$("#v-pills-FechasPeriodos-tab").click();
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
                    "<th>Acciones</th>" +
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
                            "<td class=''></td>" +
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
                            "<td class=''><div class='btn badge badge-info' onclick='MostrarEditarPeriodo("+data[j]["Empresa_id"]+",\""+data[j]["id"]+"\");'>Editar <i class='fas fa-edit'></i></div></td>" +
                            "</tr>";
                    }


                    
                }
                $(".collapse").collapse("hide");
                $("#" + pilltab).collapse("toggle");
            }

        });
    }

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

    editarFechasPeriodos = (Empresa_id) => {

        $.ajax({
            url: "../Catalogos/LoadFechasPeriodosDetalle",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                $("#lblEdicion").html("Fechas - Periodos");
                $("#Empresa_id_editar").val(data[0]["Empresa_id"]);
                $("#bodybotonagregar").html("<div class='btn btn-info btn-sm col-md-6 font-label' onclick='addRegistroFechasPeriodos();'>Agregar Registro</div>");
                $("#bodyEditarModulo").html(
                    "<table class='table table-sm'>" +
                    "<thead>" +
                    "<tr>" +
                    "<th scope='col'>Año</th>" +
                    "<th scope='col'>Tipo Periodo</th>" +
                    "<th scope='col'>Periodo</th>" +
                    "<th scope='col'><i class='fas fa-key'><i/></th>" +
                    "<th scope='col'>Inicio</th>" +
                    "<th scope='col'>Final</th>" +
                    "<th scope='col'>Proceso</th>" +
                    "<th scope='col'>Pago</th>" +
                    "<th scope='col'>Dias de pago</th>" +
                    "<th scope='col'>Acciones</th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='bodytabFechasPeriodos'>" +
                    "</tbody>" +
                    "</table>");
                document.getElementById("bodytabFechasPeriodos").innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    if (data[i]["Nomina_Cerrada"] == "True") {
                        document.getElementById("bodytabFechasPeriodos").innerHTML += "" +
                            "<tr>" +
                            "<td>" + data[i]["Anio"] + "</td>" +
                            "<td>" + data[i]["Tipo_Periodo_Id"] + "</td>" +
                            "<td>" + data[i]["Periodo"] + "</td>" +
                            "<td>" + "<div class='badge badge-light'><i class='fas fa-lock text-warning'></i></div>" + "</td>" +
                            "<td>" + data[i]["Fecha_Inicio"] + "</td>" +
                            "<td>" + data[i]["Fecha_Final"] + "</td>" +
                            "<td>" + data[i]["Fecha_Proceso"] + "</td>" +
                            "<td>" + data[i]["Fecha_Pago"] + "</td>" +
                            "<td>" + data[i]["Dias_Efectivos"] + "</td>" +
                            "<td><div class='badge badge-success btn mx-1' title='Editar' onclick='cargaModalEditar(" + data[i]["id"] + ");'> <i class='fas fa-edit'></i></div><div class='badge badge-danger btn mx-1' title='Eliminar' onclick='eliminarFechaPeriodo(" + data[i]["id"] + ");'><i class='fas fa-minus'></i></div></td>" +
                            "</tr>";
                    } else {
                        document.getElementById("bodytabFechasPeriodos").innerHTML += "" +
                            "<tr>" +
                            "<td>" + data[i]["Anio"] + "</td>" +
                            "<td>" + data[i]["Tipo_Periodo_Id"] + "</td>" +
                            "<td>" + data[i]["Periodo"] + "</td>" +
                            "<td>" + "<div class='badge badge-light'><i class='fas fa-unlock-alt text-primary'></i></div>" + "</td>" +
                            "<td>" + data[i]["Fecha_Inicio"] + "</td>" +
                            "<td>" + data[i]["Fecha_Final"] + "</td>" +
                            "<td>" + data[i]["Fecha_Proceso"] + "</td>" +
                            "<td>" + data[i]["Fecha_Pago"] + "</td>" +
                            "<td>" + data[i]["Dias_Efectivos"] + "</td>" +
                            "<td><div class='badge badge-success btn mx-1' title='Editar' onclick='cargaModalEditar(" + data[i]["id"] + ");'> <i class='fas fa-edit'></i></div><div class='badge badge-danger btn mx-1' title='Eliminar' onclick='eliminarFechaPeriodo(" + data[i]["id"] + ");'><i class='fas fa-minus'></i></div></td>" +
                            "</tr>";
                    }


                    
                }

                $("#v-pills-editar-tab").click();
            }
        });

    }

    editarPoliticas = (Empresa_id, Effdt) => {

        $.ajax({
            url: "../Catalogos/LoadPoliticasVacacionesDetalle",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                
                $("#lblEdicion").html("Politicas Vacaciones " + data[0]["NombreEmpresa"]);
                $("#Empresa_id_editar").val(data[0]["Empresa_id"]);
                $("#infoEditar").addClass("invisible");
                $("#Effdt_editar").removeClass("invisible");
                $("#selectp  option[value='0']").attr("selected", true);
                $("#Effdt_editar").val(Effdt);
                $("#bodybotonagregar").html("<div class='btn btn-info btn-sm col-md-12 font-label' onclick='addRegistroFechasPeriodos();'>Agregar Registro</div>");
                $("#bodyEditarModulo").html(
                    "<table class='table table-sm font-labels table-striped text-center'>" +
                    "<thead>" +
                    "<tr>" +
                    "<th scope='col'>Años</th>" +
                    "<th scope='col'>Dias</th>" +
                    "<th scope='col'>Prima Vacacional</th>" +
                    "<th scope='col'>Dias Aguinaldo</th>" +
                    "<th scope='col'>Acciones</th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='bodytabPoliticas'>" +
                    "</tbody>" +
                    "</table>");
                document.getElementById("bodytabPoliticas").innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    
                        document.getElementById("bodytabPoliticas").innerHTML += "" +
                            "<tr>" +
                            "<td>" + data[i]["Anos"] + "</td>" +
                            "<td>" + data[i]["Dias"] + "</td>" +
                            "<td>" + data[i]["Prima_Vacacional_Porcen"] + "</td>" +
                            "<td>" + data[i]["Dias_Aguinaldo"] + "</td>" +
                            "<td><div class='badge badge-success btn mx-1' title='Editar' onclick='cargaModalEditarPolitica(" + Empresa_id + ",\"" + Effdt + "\"," + data[i]["Anos"] + ");'> <i class='fas fa-edit'></i></div><div class='badge badge-danger btn mx-1' title='Eliminar' onclick='eliminarPolitica(" + Empresa_id + ",\"" + Effdt + "\"," + data[i]["Anos"] + ");'><i class='fas fa-minus'></i></div></td>" +
                            "</tr>";
                    



                }

                $("#editar-tab").click();
            }
        });

    }

    editarPoliticasFuturas = (Empresa_id, Effdt) => {

        $.ajax({
            url: "../Catalogos/LoadPoliticasVacaciones_Futuras_Detalle",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id, Effdt: Effdt }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                $("#lblEdicion").html("Politicas Vacaciones " + data[0]["NombreEmpresa"]);
                $("#Empresa_id_editar").val(data[0]["Empresa_id"]);
                $("#infoEditar").addClass("invisible");
                $("#Effdt_editar").removeClass("invisible");
                $("#selectp  option[value='1']").attr("selected", true);
                $("#Effdt_editar").val(Effdt);
                $("#bodybotonagregar").html("<div class='btn btn-info btn-sm col-md-12 font-label' onclick='addNewPolitica(" + Empresa_id + ",\"" + Effdt + "\");'>Agregar Registro</div>");
                $("#bodyEditarModulo").html(
                    "<table class='table table-sm table-striped font-labels text-center'>" +
                    "<thead>" +
                    "<tr>" +
                    "<th scope='col'>Años</th>" +
                    "<th scope='col'>Dias</th>" +
                    "<th scope='col'>Prima Vacacional</th>" +
                    "<th scope='col'>Dias Aguinaldo</th>" +
                    "<th scope='col'>Acciones</th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='bodytabPoliticas'>" +
                    "</tbody>" +
                    "</table>");
                document.getElementById("bodytabPoliticas").innerHTML = "";
                for (var i = 0; i < data.length; i++) {

                    document.getElementById("bodytabPoliticas").innerHTML += "" +
                        "<tr>" +
                        "<td>" + data[i]["Anos"] + "</td>" +
                        "<td>" + data[i]["Dias"] + "</td>" +
                        "<td>" + data[i]["Prima_Vacacional_Porcen"] + "</td>" +
                        "<td>" + data[i]["Dias_Aguinaldo"] + "</td>" +
                        "<td><div class='badge badge-success btn mx-1' title='Editar' onclick='cargaModalEditarPolitica(" + Empresa_id + ",\"" + Effdt + "\"," + data[i]["Anos"] + ");'> <i class='fas fa-edit'></i></div><div class='badge badge-danger btn mx-1' title='Eliminar' onclick='eliminarPolitica(" + Empresa_id + ",\"" + Effdt + "\"," + data[i]["Anos"] + ");'><i class='fas fa-minus'></i></div></td>" +
                        "</tr>";




                }

                $("#editar-tab").click();
            }
        });

    }
    //mostrarModalNuevoPeriodo 
    addRegistroFechasPeriodos = () => {
        //Swal.fire({
        //    icon: 'success',
        //    title: 'Se va agregar!',
        //    timer: '1000'
        //});
        llenaMinAnios();
        $("#modalAgregarFechaPeriodo").modal("show");
    }

    addNewPolitica = () => {
        $("#modalNewPolitica").modal("show");
    }

    llenaMinAnios = () => {
        var ano = (new Date).getFullYear();
        $("#inano").attr("min", ano);
        $("#inano").val(ano);
    }
    // Reinicia el formulario de agregado de fechas periodo
    $('#modalAgregarFechaPeriodo').on('hidden.bs.modal', function (e) {
        document.getElementById("frmAddFechasPeriodos").reset();
    });

    // Guardar Fecha - Periodo
    $("#btnsavefechaperiodo").on("click", function () {

        var form = document.getElementById("frmAddFechasPeriodos");
        if (form.checkValidity() === false) {

            form.classList.add("was-validated");
            
        } else {
            var inano = document.getElementById("inano");
            var inperiodo = document.getElementById("inperiodo");
            var infinicio = document.getElementById("infinicio");
            var inffinal = document.getElementById("inffinal");
            var infproceso = document.getElementById("infproceso");
            var infpago = document.getElementById("infpago");
            var indiaspago = document.getElementById("indiaspago");
            var inEmpresa_id = document.getElementById("Empresa_id_editar");
            $.ajax({
                url: "../Catalogos/SaveNewPeriodo",
                type: "POST",
                data: JSON.stringify({
                    inEmpresa_id: inEmpresa_id.value,
                    inano: inano.value,
                    inperiodo: inperiodo.value,
                    infinicio: infinicio.value,
                    inffinal: inffinal.value,
                    infproceso: infproceso.value,
                    infpago: infpago.value,
                    indiaspago: indiaspago.value
                }),
                contentType: "application/json; charset=utf-8",
                beforeSend: () => {
                    form.classList.add("was-validated");
                },
                success: (data) => {
                    console.log(data);
                    if (data[0] == '0') {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Error!',
                            text: data[1],
                            timer: 3000
                        });
                    } else {
                        Swal.fire({
                            icon: 'success',
                            title: 'Correcto!',
                            text: data[1],
                            timer: 1500
                        });
                        editarFechasPeriodos(inEmpresa_id.value);
                        $("#modalAgregarFechaPeriodo").modal("hide");
                        document.getElementById("frmAddFechasPeriodos").reset();
                    }


                }
            });
        }
    });
    // Guardar nueva politica
    $("#btnsavenewpolitica").on("click", function () {
        var form = document.getElementById("frmnewPolitica");
        if (form.checkValidity() === false) {

            form.classList.add("was-validated");

        } else {
            var inanio = document.getElementById("newPoliticaAnio");
            var indias = document.getElementById("newPoliticaDias");
            var inprimav = document.getElementById("newPoliticaPrimav");
            var indiasa = document.getElementById("newPoliticaDiasa");

            $.ajax({
                url: "../Catalogos/SaveNewPolitica",
                type: "POST",
                data: JSON.stringify({
                    inEmpresa_id: $("#Empresa_id_editar").val(),
                    inEffdt: $("#Effdt_editar").val(),
                    inano: inanio.value,
                    indias: indias.value,
                    inprimav: inprimav.value,
                    indiasa: indiasa.value
                }),
                contentType: "application/json; charset=utf-8",
                beforeSend: () => {
                    form.classList.add("was-validated");
                },
                success: (data) => {
                    console.log(data);
                    if (data[0] == '0') {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Error!',
                            text: data[1],
                            timer: 3000
                        });
                    } else {
                        Swal.fire({
                            icon: 'success',
                            title: 'Correcto!',
                            text: data[1],
                            timer: 1500
                        });
                        if ($("#selectp").val() == 0) {
                            editarPoliticas($("#Empresa_id_editar").val(), $("#Effdt_editar").val());
                        } else {
                            editarPoliticasFuturas($("#Empresa_id_editar").val(), $("#Effdt_editar").val());
                        }
                        
                        $("#modalNewPolitica").modal("hide");
                        form.reset();
                    }


                }
            });
        }
    });

    // Eliminar una Fecha - Periodo
    eliminarFechaPeriodo = (id) => {
        Swal.fire({
            title: 'Estas seguro?',
            text: "El registro sera borrado definitivamente!",
            icon: 'warning',
            showCancelButton: true,
            CancelButtonText: 'Cancelar',
            confirmButtonColor: '#d33',
            cancelButtonColor: '#98959B',
            confirmButtonText: 'Confirmar'
        }).then((result) => {
            if (result.value) {
                var Empresa_id = document.getElementById("Empresa_id_editar");
                $.ajax({
                    url: "../Catalogos/DeletePeriodo",
                    type: "POST",
                    data: JSON.stringify({
                        Empresa_id: Empresa_id.value,
                        Id: id
                    }),
                    contentType: "application/json; charset=utf-8",
                    success: (data) => {
                        if (data[0] == 0) {
                            Swal.fire({
                                title: 'Error!',
                                icon: 'warning',
                                text: data[1],
                                timer: 3000
                            });
                        } else {
                            editarFechasPeriodos(Empresa_id.value);

                            Swal.fire({
                                title: 'Correcto!',
                                icon: 'success',
                                text: data[1],
                                timer: 1500
                            });        
                        }
                    }
                });
            }
        });



        
    }

    // Eliminar una Fecha - Periodo
    eliminarPolitica = (Empresa_id,Effdt,Anio) => {
        Swal.fire({
            title: 'Estas seguro?',
            text: "El registro sera borrado definitivamente!",
            icon: 'warning',
            showCancelButton: true,
            CancelButtonText: 'Cancelar',
            confirmButtonColor: '#d33',
            cancelButtonColor: '#98959B',
            confirmButtonText: 'Confirmar'
        }).then((result) => {
            if (result.value) {
                
                $.ajax({
                    url: "../Catalogos/DeletePolitica",
                    type: "POST",
                    data: JSON.stringify({
                        Empresa_id: Empresa_id, Effdt: Effdt, Anios: Anio
                    }),
                    contentType: "application/json; charset=utf-8",
                    success: (data) => {
                        if (data[0] == 0) {
                            Swal.fire({
                                title: 'Error!',
                                icon: 'warning',
                                text: data[1],
                                timer: 3000
                            });
                        } else {
                            editarPoliticasFuturas(Empresa_id, Effdt);
                            Swal.fire({
                                title: 'Correcto!',
                                icon: 'success',
                                text: data[1],
                                timer: 1500
                            });
                        }
                    }
                });
            }
        });




    }

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
                        + "<div class='card-header' id='heading" + data[i][0] + "'>"
                        + "<h2 class='mb-0'>"
                        + "<button class='btn btn-link btn-block d-flex justify-content-between aling-items-center' onclick='MostrarEmpresasEnGrupo(\"" + data[i][0] + "\",\"ul" + data[i][0] + "\",\"collapse" + data[i][0] + "\")' type='button'>"
                        + "" + data[i][1] + ""
                        + "</button>"
                        + "</h2>"
                        + "</div>"
                        + "<div id='collapse" + data[i][0] + "' class='collapse' aria-labelledby='heading" + data[i][0] + "' data-parent='#accordionGruposEmpresas'>"
                        + "<ul id='ul" + data[i][0] + "' class='list-group list-group-flush'>"
                        + "</ul>"
                        + "</div>"
                        + "</div >";
                }
            }
        });
    }
    //LLENA GRUPOS EMPRESAS LINE
    MostrarEmpresasEnGrupo = (Grupo_id, ul, collapse) => {
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
                    lista.innerHTML += "<li class='list-group-item list-group-item-secondary d-flex justify-content-between aling-items-center'>" + data[i][0] + " - " + data[i][1] + "<span class='btn badge badge-danger' title='Quitar empresa'><i class='fas fa-minus'></i></span></li>";
                }
                $("#" + collapse).collapse("toggle");
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
    //LLENA SELECT GRUPOS
    LoadSelectGrupos = () => {
            $.ajax({
                url: "../Catalogos/LoadGruposEmpresas",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    console.log(data);
                    var select = document.getElementById("inGrupos");
                    select.innerHTML = "";
                    for (var i = 0; i < data.length; i++) {
                        select.innerHTML += "<option value='" + data[i][0] + "'>" + data[i][1] + "</option>";
                    }
                }
            });
        }
    //CAMBIO DE SELECT GRUPOS
    $("#inGrupos").on("change", function () {
        console.log($(this).val());
        console.val('valor del grupo');
    });
    // CARGA MODAL DE EDITAR POLITICAS
    cargaModalEditarPolitica = (Empresa_id, Effdt, Anio) => {
        $.ajax({
            url: "../Catalogos/LoadPolitica",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id, Effdt: Effdt, Anio: Anio }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                console.log($("#Empresa_id_editar").val());
                $("#EditarPoliticaAnio").val(data[0]["Anos"]);
                $("#EditarPoliticaDias").val(data[0]["Dias"]);
                $("#EditarPoliticaDiasa").val(data[0]["Dias_Aguinaldo"]);
                $("#EditarPoliticaPrimav").val(data[0]["Prima_Vacacional_Porcen"]);
                $("#anioEditar").val(Anio);
                $("#modalEditarPolitica").modal("show");
            }
        });
    }
    $("#btnSaveEditPolitica").on("click", function () {
        
        $.ajax({
            url: "../Catalogos/UpdatePolitica",
            type: "POST",
            data: JSON.stringify({
                Empresa_id: $("#Empresa_id_editar").val(),
                Effdt: $("#Effdt_editar").val(),
                Anio: $("#EditarPoliticaAnio").val(),
                Dias: $("#EditarPoliticaDias").val(),
                Diasa: $("#EditarPoliticaDiasa").val(),
                Prima: $("#EditarPoliticaPrimav").val(),
                Anion: $("#anioEditar").val()
            }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                $("#modalEditarPolitica").modal("hide");
                if (data[0] == '0') {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Error!',
                        text: data[1],
                        timer: 3000
                    });
                } else {
                    if ($("#selectp").val() == 0) {
                        editarPoliticas($("#Empresa_id_editar").val(), $("#Effdt_editar").val());
                    } else {
                        editarPoliticasFuturas($("#Empresa_id_editar").val(), $("#Effdt_editar").val());
                    }
                    Swal.fire({
                        icon: 'success',
                        title: 'Correcto!',
                        text: data[1],
                        timer: 3000
                    });

                }
                

            }
        });
    });
    // MOSTRAR EDITAR PERIODO
    MostrarEditarPeriodo = (Empresa_id,Id) => {
        $.ajax({
            url: "../Catalogos/LoadPeriodo",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id, Id: Id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                $("#editano").val(data[0]["Anio"]);
                $("#editperiodo").val(data[0]["Periodo"]);
                $("#editfinicio").attr("value", data[0]["Fecha_Inicio"].substring(6) + "-" + data[0]["Fecha_Inicio"].substring(3, 5) + "-" + data[0]["Fecha_Inicio"].substring(0, 2));
                $("#editffinal").attr("value", data[0]["Fecha_Final"].substring(6) + "-" + data[0]["Fecha_Final"].substring(3, 5) + "-" + data[0]["Fecha_Final"].substring(0, 2));
                $("#editfproceso").attr("value", data[0]["Fecha_Proceso"].substring(6) + "-" + data[0]["Fecha_Proceso"].substring(3, 5) + "-" + data[0]["Fecha_Proceso"].substring(0, 2));
                $("#editfpago").attr("value", data[0]["Fecha_Pago"].substring(6) + "-" + data[0]["Fecha_Pago"].substring(3, 5) + "-" + data[0]["Fecha_Pago"].substring(0, 2));
                $("#editdiaspago").val(data[0]["Dias_Efectivos"]);
                $("#modalEditarFechaPeriodo").modal("show");
                $("#editar_empresa_id").val(Empresa_id);
                $("#editar_id").val(Id);
            }
        });
    }

    $("#btnUpdatefechaperiodo").on("click", function () {
        
        $.ajax({
            url: "../Catalogos/UpdatePeriodo",
            type: "POST",
            data: JSON.stringify({
                Empresa_id: $("#editar_empresa_id").val(),
                editid: $("#editar_id").val(),
                editano: $("#editano").val(),
                editperiodo: $("#editperiodo").val(),
                editfinicio: $("#editfinicio").val(),
                editffinal: $("#editffinal").val(),
                editfproceso: $("#editfproceso").val(),
                editfpago: $("#editfpago").val(),
                editdiaspago: $("#editdiaspago").val()}),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
            }
        });

    });
    
});