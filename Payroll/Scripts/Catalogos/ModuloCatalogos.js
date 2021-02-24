
$(function () {
    $("#pills-catalogos.nav-link.active").addClass('bg-secondary');
    $("#pills-catalogos.nav-link").on("click", function () {
        $("#pills-catalogos.nav-link").removeClass('bg-secondary');
        $(this).addClass('bg-secondary');
    });
    $(".btntab").hover(
        function () {
            $(this).children('.btntittle').removeClass('bg-primary').addClass('bg-dark');
        },
        function () {
            $(this).children('.btntittle').removeClass('bg-dark').addClass('bg-primary');
        }
    );
    $(".badge-dark").hover(
        function () {
            alert('si');
        },
        function () {
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
            Swal.fire({
                icon: 'warning',
                title: 'Atención!',
                text: 'Debe seleccionar una empresa dentro de un catalogo para editar antes de entrar'
            });
        } else {

        }
    });
    // Mostar Modal Agregar NEW EFFDT 
    $("#btn-newEffdtPoliticas").on("click", function () {
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var today = now.getFullYear() + "-" + (month) + "-" + (day);
        $("#newEffdtFecha").attr("min", today);
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
                var select = document.getElementById(select_id);
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
                tab.innerHTML = "";
                var empresa;
                for (var i = 0; i < data.length; i++) {
                    if (i == 0) {
                        empresa = data[i]["Empresa_id"];
                    }

                    tab.innerHTML += "" +
                        "<tr>" +
                        "<td colspan='3' >" +
                        "<div class='col-md-12 row'>" +
                        "<label class='col-md-1'>" + data[i]['Empresa_id'] + "</label><label class='col-md-3'>" + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]['Tipo_Periodo_Id'] + " - " + data[i]["DescripcionTipoPeriodo"] + "</label><div class='col-md-5'><div class='badge badge-success btn' onclick='LoadDetalleFechasPeriodo(\"collapse-" + data[i]["NombreEmpresa"].replace(/ /g, "") + data[i]['Empresa_id'] + "\", " + data[i]["Empresa_id"] + ");'>Ver <i class='fas fa-plus'></i></div><div class='ml-1 badge badge-primary btn' onclick='mostrarModalNuevoPeriodo(" + data[i]["Empresa_id"] + "," + data[i]["Anio"] + "," + data[i]["Tipo_Periodo_Id"] + ");'>Nuevo <i class='fas fa-calendar-check'></i></div></div>" +
                        "<div id='collapse-" + data[i]["NombreEmpresa"].replace(/ /g, "") + data[i]['Empresa_id'] + "' class='collapse collapse-" + data[i]['NombreEmpresa'].replace(/ /g, "") + data[i]['Empresa_id'] + " col-md-12'>" +
                        "</div>" +
                        "</div>" +
                        "</td >" +
                        "</tr >";
                }

            }
        });
    }
    // FUNCION QUE REINICIA EL MODAL DE CARGA MASIVA DE PERIODOS
    $('#modalCargaMasiva').on('hidden.bs.modal', function () {
        $(".custom-file-input").val("").next('.custom-file-label').removeClass('selected').html("Seleccionar Archivo");
        document.getElementById("modalCargaMasiva").reset();
        //console.log("Close modal");
    });
    // Cambio en el input file
    $('.custom-file-input').on('change', function () {
        var fileName = "";
        if ($(this).val().length != 0) {
            fileName = $(this).val().split('\\').pop();
            $(this).next('.custom-file-label').addClass("selected").html(fileName);
        } else {
            fileName = "Seleccionar Archivo";
            $(this).next('.custom-file-label').removeClass('selected').html(fileName);
        }

    });


    beforeValidarFile = () => {

        $("#btnCargaMasiva").html("<span class='spinner-grow spinner-grow-sm' role='status' aria-hidden='true'></span> Cargando...");
        document.getElementById("btnCargaMasiva").disabled = true;
        
        setTimeout(function () {
            validateUploadFile();
        }, 500);
    }

    validateUploadFile = () => {
        var selectedFile = $("#file-toup").prop("files")[0];
        var selectedF = ($("#file-toup"))[0].files[0];

        if (!selectedF) {
            Swal.fire({
                icon: 'warning',
                title: 'Aviso!',
                text: 'Aun no selecciona un archivo'
            });
            $("#btnCargaMasiva").html("<i class='fas fa-check-circle mr-2'></i> Cargar archivo");
            document.getElementById("btnCargaMasiva").disabled = false;
        } else {

            Swal.fire({
                title: 'Mensaje de confirmación',
                html: "<h5>Se cargará Layout de <strong class='text-danger h4 text-uppercase'>" + "Periodos" + "</strong>, <br/>¿Es correcto?</h5>",
                icon: 'warning',
                showCancelButton: true,
                CancelButtonText: 'Cancelar',
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#ff3f3f',//'#d33',
                confirmButtonText: 'Confirmar'
            }).then((result) => {
                if (result.value) {
                    var datos = new FormData();
                    datos.append("fileUpload", selectedF);
                    
                    $.ajax({
                        url: "../Catalogos/LoadFilePeriodos",
                        type: "POST",
                        data: datos,
                        processData: false,
                        contentType: false,
                        async: false,
                        success: function (data) {
                            if (data[0] == "0") {
                                document.getElementById("btnCargaMasiva").disabled = false;
                                $("#btnCargaMasiva").html("<i class='fas fa-check-circle mr-2'></i> Cargar archivo");
                                var btn = "<div class='col-md-12 d-flex justify-content-center'><a class=' btn btn-success btn-sm btn-icon-split' href='" + data[1] + "' download><span class='icon'> <i class='fas fa-download text-white'></i> </span><span class='text'> Descargar archivo log .txt </span></a></div>";
                                var txt = "<div class='alert alert-warning col-md-12 my-3' role='alert' id='alert-validation'>" +
                                    "<button type='button' class='close' data-dismiss='alert' aria-label='Close'> <span aria-hidden='true'>&times;</span></button>" +
                                    "<strong> Atención </strong> Hubo errores en el archivo Layout de carga.\n Descargue en archivo log con los errores " + btn + "</div >";
                                $("#collapse-validation-cm").html(txt);
                                $("#collapse-validation-cm").collapse("show");
                                //$("a.btn-success").focus();
                                $("#file-toup").val('');
                            }
                            if (data[0] == "1") {
                                document.getElementById("btnCargaMasiva").disabled = false;
                                $("#btnCargaMasiva").html("<i class='fas fa-check-circle mr-2'></i> Cargar archivo");
                                var txt = "<div class='alert alert-success col-md-12 my-3' role='alert' id='alert-validation'>" +
                                    "<button type='button' class='close' data-dismiss='alert' aria-label='Close'> <span aria-hidden='true'>&times;</span></button>" +
                                    "<strong> Listo </strong> " + data[1] + " </div >";
                                $("#collapse-validation-cm").html(txt);
                                $("#collapse-validation-cm").collapse("show");
                                $("#file-toup").val('');

                                //$("#tabCargasMasivas").fadeOut();
                                //$('.table').DataTable.destroy();
                                //loadCargasMasivas();
                            }
                        }
                    });
                }
            });
        }
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
                        tab.innerHTML += "" +
                            "<tr>" +
                            "<td colspan='3' >" +
                            "<div class='col-md-12 row'>" +
                            "<label class='col-md-1'>" + data[i]['Empresa_id'] + "</label><label class='col-md-3'> " + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]["Effdt"] + "</label><div class='col-md-5'><div class='badge badge-secondary btn' onclick='LoadDetallePoliticas(\"collapsetab-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ");'>Ver <i class='fas fa-plus'></i></div><div class='btn badge badge-primary ml-2' onclick='editarPoliticas(" + data[i]["Empresa_id"] + ",\"" + data[i]["Effdt"] + "\");'>Editar <i class='fas fa-edit'></i></div></div>" +
                            "<div id='collapsetab-" + data[i]["NombreEmpresa"] + "' class='collapse collapse-" + data[i]['NombreEmpresa'] + data[i]['Empresa_id'] + " col-md-12'>" +
                            "</div>" +
                            "</div>" +
                            "</td >" +
                            "</tr >";
                    } else {
                        empresa = data[i]["Empresa_id"];
                        if (data[i]["Empresa_id"] == empresa) {
                            tab.innerHTML += "" +
                                "<tr>" +
                                "<td colspan='3' >" +
                                "<div class='col-md-12 row'>" +
                                "<label class='col-md-1'>" + data[i]['Empresa_id'] + "</label><label class='col-md-3'> " + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]["Effdt"] + "</label><div class='col-md-5'><div class='badge badge-secondary btn' onclick='LoadDetallePoliticas(\"collapsetab-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ");'>Ver <i class='fas fa-plus'></i></div><div class='btn badge badge-primary ml-2' onclick='editarPoliticas(" + data[i]["Empresa_id"] + ",\"" + data[i]["Effdt"] + "\");'>Editar <i class='fas fa-edit'></i></div></div>" +
                                "<div id='collapsetab-" + data[i]["NombreEmpresa"] + "' class='collapse collapse-" + data[i]['NombreEmpresa'] + data[i]['Empresa_id'] + " col-md-12'>" +
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
                        tab.innerHTML += "" +
                            "<tr>" +
                            "<td colspan='3' >" +
                            "<div class='col-md-12 row'>" +
                            "<label class='col-md-1'>" + data[i]['Empresa_id'] + "</label><label class='col-md-3'> " + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]["Effdt"] + "</label><div class='col-md-5'><div class='badge badge-secondary btn' onclick='LoadPoliticasVacaciones_Futuras_Detalle(\"collapsetab-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ", \"" + data[i]["Effdt"] + "\");'>Ver <i class='fas fa-plus'></i></div><div class='btn badge badge-primary ml-2' onclick='editarPoliticasFuturas(" + data[i]["Empresa_id"] + ",\"" + data[i]["Effdt"] + "\");'>Editar <i class='fas fa-edit'></i></div></div>" +
                            "<div id='collapsetab-" + data[i]["NombreEmpresa"] + "' class='collapse collapse-" + data[i]['NombreEmpresa'] + " col-md-12'>" +
                            "</div>" +
                            "</div>" +
                            "</td >" +
                            "</tr >";
                    } else {
                        empresa = data[i]["Empresa_id"];
                        if (data[i]["Empresa_id"] == empresa) {
                            tab.innerHTML += "" +
                                "<tr>" +
                                "<td colspan='3' >" +
                                "<div class='col-md-12 row'>" +
                                "<label class='col-md-1'>" + data[i]['Empresa_id'] + "</label><label class='col-md-3'> " + data[i]['NombreEmpresa'] + " </label><label class='col-md-3'> " + data[i]["Effdt"] + "</label><div class='col-md-5'><div class='badge badge-secondary btn' onclick='LoadPoliticasVacaciones_Futuras_Detalle(\"collapsetab-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ", \"" + data[i]["Effdt"] + "\");'>Ver <i class='fas fa-plus'></i></div><div class='btn badge badge-primary ml-2' onclick='editarPoliticasFuturas(" + data[i]["Empresa_id"] + ",\"" + data[i]["Effdt"] + "\");'>Editar <i class='fas fa-edit'></i></div></div>" +
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

    LoadDetalleFechasPeriodo = (pilltab, Empresa_id) => {
        $.ajax({
            url: "../Catalogos/LoadFechasPeriodosDetalle",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
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
                            "<td class=''><div class='btn badge badge-info' onclick='MostrarEditarPeriodo(" + data[j]["Empresa_id"] + ",\"" + data[j]["id"] + "\");'>Editar <i class='fas fa-edit'></i></div></td>" +
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
                        document.getElementById("politicas-modal-title").innerHTML = data[j]["NombreEmpresa"];
                    }
                    document.getElementById("tabp" + pilltab).innerHTML += "<tr>" +
                        "<td class=''>" + data[j]["Anos"] + "</td>" +
                        "<td class=''>" + data[j]["Dias"] + "</td>" +
                        "<td class=''>" + data[j]["Prima_Vacacional_Porcen"] + "</td>" +
                        "<td class=''>" + data[j]["Dias_Aguinaldo"] + "</td>" +
                        "</tr>";
                }
                document.getElementById("modal-body-politicas").innerHTML = $("#" + pilltab).html();

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
                        document.getElementById("politicas-modal-title").innerHTML = data[j]["NombreEmpresa"];
                    }
                    document.getElementById("tabp" + pilltab).innerHTML += "<tr>" +
                        "<td class=''>" + data[j]["Anos"] + "</td>" +
                        "<td class=''>" + data[j]["Dias"] + "</td>" +
                        "<td class=''>" + data[j]["Prima_Vacacional_Porcen"] + "</td>" +
                        "<td class=''>" + data[j]["Dias_Aguinaldo"] + "</td>" +
                        "</tr>";
                }
                document.getElementById("modal-body-politicas").innerHTML = $("#" + pilltab).html();

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
    mostrarModalNuevoPeriodo = (Empresa_id, Anio, Tipo_Periodo_id) => {
        llenaMinAnios();
        $("#modalAgregarFechaPeriodo").modal("show");
        $("#inanio").val(Anio);
        $("#intipoperiodoid").val(Tipo_Periodo_id);
        $("#inEmpresa_id").val(Empresa_id);
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
        document.getElementById("frmNewFechasPeriodos").reset();
    });

    // Guardar Fecha - Periodo
    $("#btnsavefechaperiodo").on("click", function () {
        var form = document.getElementById("frmNewFechasPeriodos");
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
            var inEmpresa_id = document.getElementById("inEmpresa_id");
            var intipoperiodoid = document.getElementById("intipoperiodoid");
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
                    indiaspago: indiaspago.value,
                    intipoperiodoid: intipoperiodoid.value
                }),
                contentType: "application/json; charset=utf-8",
                beforeSend: () => {
                    form.classList.add("was-validated");
                },
                success: (data) => {
                    if (data[0] == '1') {
                        Swal.fire({
                            icon: 'success',
                            title: 'Correcto!',
                            text: data[1],
                            timer: 1500
                        });
                        editarFechasPeriodos(inEmpresa_id.value);
                        $("#modalAgregarFechaPeriodo").modal("hide");
                        document.getElementById("frmNewFechasPeriodos").reset();
                    } else {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Error!',
                            text: data[1],
                            timer: 3000
                        });
                    }


                }
            });
        }
        //}
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
    //
    // Eliminar una Fecha - Periodo
    eliminarPolitica = (Empresa_id, Effdt, Anio) => {
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
    //LLENA SELECT EMPRESAS
    LoadSelectEmpresas = () => {
        $.ajax({
            url: "../Empresas/LoadSEmp",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var select = document.getElementById("inEmpresa");
                select.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    select.innerHTML += "<option value='" + data[i]["IdEmpresa"] + "'>" + data[i]["IdEmpresa"] + "&nbsp;&nbsp;" + data[i]["NombreEmpresa"] + "</option>";
                }
            }
        });
    }
    //
    //CAMBIO DE SELECT GRUPOS
    $("#inGrupos").on("change", function () {
        //
    });
    // CARGA MODAL DE EDITAR POLITICAS
    cargaModalEditarPolitica = (Empresa_id, Effdt, Anio) => {
        $.ajax({
            url: "../Catalogos/LoadPolitica",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id, Effdt: Effdt, Anio: Anio }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
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
    MostrarEditarPeriodo = (Empresa_id, Id) => {
        $.ajax({
            url: "../Catalogos/LoadPeriodo",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id, Id: Id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
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
                editdiaspago: $("#editdiaspago").val()
            }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                $(".collapse").collapse("hide");
                $("#modalEditarFechaPeriodo").modal("hide");
            }
        });

    });

    /////////////////////////////////////////////////////
    ////////////////    GRUPOS EMPRESAS    //////////////
    ////////////////////////////////////////////////////
    // LLENA SELECT GRUPOS
    LoadSelectGrupos = () => {
        $.ajax({
            url: "../Catalogos/LoadGruposEmpresas",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var select = document.getElementById("inGrupos");
                select.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    select.innerHTML += "<option value='" + data[i][0] + "'>" + data[i][1] + "</option>";
                }
            }
        });
    }
    //
    // ACTUALIZA GRUPO EMPRESA
    updateGroup = () => {
        var grupo = document.getElementById("inGrupos");
        var empresa = document.getElementById("inEmpresa");
        $.ajax({
            url: '../Empresas/UpdateGrupo',
            type: 'POST',
            data: JSON.stringify({
                Empresa_id: $("#inEmpresa").val(),
                Grupo_id: $("#inGrupos").val()
            }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                if (data[0] == 0) {
                    Swal.fire({
                        timer: 1500,
                        showConfirmButton: false,
                        position: 'top-end',
                        title: 'Error!',
                        icon: 'error',
                        text: data[1],
                        width: 400,
                        height: 200
                    });
                } else if (data[0] == 1) {
                    Swal.fire({
                        timer: 1500,
                        showConfirmButton: false,
                        position: 'top-end',
                        title: 'Correcto!',
                        icon: 'success',
                        text: data[1],
                        width: 400,
                        iconHeight: 200
                    });
                } else if (data[0] == 2) {
                    Swal.fire({
                        timer: 1500,
                        showConfirmButton: false,
                        position: 'top-end',
                        title: 'Aviso!',
                        icon: 'warning',
                        text: data[1],
                        width: 400,
                        height: 200
                    });
                }
            }
        });
    }
    //
    //LLENA GRUPOS EMPRESAS HEADER
    LoadAcordeonGrupos = () => {
        $.ajax({
            url: "../Catalogos/LoadGruposEmpresas",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var acordeon = document.getElementById("accordeonGruposEmpresas");
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
                        + "<div id='collapse" + data[i][0] + "' class='collapse' aria-labelledby='heading" + data[i][0] + "' data-parent='#accordeonGruposEmpresas'>"
                        + "<ul id='ul" + data[i][0] + "' class='list-group list-group-flush'>"
                        + "</ul>"
                        + "</div>"
                        + "</div >";
                }
            }
        });
    }
    //
    //LLENA GRUPOS EMPRESAS LINE
    MostrarEmpresasEnGrupo = (Grupo_id, ul, collapse) => {
        $.ajax({
            url: "../Catalogos/LoadEmpresasGrupo",
            type: "POST",
            data: JSON.stringify({ Grupo_id: Grupo_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var lista = document.getElementById(ul)
                lista.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    lista.innerHTML += "<li class='list-group-item list-group-item-secondary d-flex justify-content-between aling-items-center'>" + data[i][0] + " - " + data[i][1] + "<span class='btn badge badge-danger' title='Quitar empresa'><i class='fas fa-minus'></i></span></li>";
                }
                $("#" + collapse).collapse("toggle");
            }
        });
    }
    //
    //QUITAR GRUPO DE EMPRESA
    updateSinEmpresa = (notempresa) => {
        $.ajax({
            url: "../Empresas/UpdateGrupo",
            type: "POST",
            data: JSON.stringify({
                cero: notempresa
            }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {

            }
        });
    }
    //
    //AGREGAR GRUPOS DE EMPRESAS
    SaveNewGrupo = () => {
        var inNewNombreGrupo = document.getElementById("innewgrupoempresa");
        var form = document.getElementById("formnewgrupo");
        if (form.checkValidity() == false) {
            form.classList.add("was-validated");
            setTimeout(() => {
                form.classList.remove("was-validated");
            }, 5000);
        } else {
            $.ajax({
                url: "../Empresas/SaveGrupo",
                type: "POST",
                data: JSON.stringify({
                    NombreGrupo: document.getElementById("innewgrupoempresa").value
                }),
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    if (data[0] == 0) {
                        Swal.fire({
                            timer: 1500,
                            showConfirmButton: false,
                            position: 'top-end',
                            title: 'Error!',
                            icon: 'error',
                            text: data[1],
                            width: 400,
                            height: 200
                        });
                    } else if (data[0] == 1) {
                        Swal.fire({
                            timer: 1500,
                            showConfirmButton: false,
                            position: 'top-end',
                            title: 'Correcto!',
                            icon: 'success',
                            text: data[1],
                            width: 400,
                            iconHeight: 200
                        });
                        inNewNombreGrupo.value = "";
                        LoadSelectEmpresas();
                        LoadSelectGrupos();
                        LoadAcordeonGrupos();
                    }
                }
            });
        }


    }
    //
    //MOSTRAR MODAL DE AGREGADO DE NUEVO GRUPO
    mostrarmodalnewgrupo = () => {
        $("#modalnewgrupo").modal("show");
    }
    /////////////////////////////////////////////////////
    ///////////////        BANCOS         ///////////////
    /////////////////////////////////////////////////////
    // LLENA TABLA BANCOS
    LoadSelectBancos = (Banco_id) => {
        $.ajax({
            url: "../Empleados/LoadBanks",
            type: "POST",
            data: JSON.stringify({ keyban: Banco_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var tab = document.getElementById("tbody-bancos");
                tab.innerHTML = "";
                var select = document.getElementById("newbanco");
                select.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    tab.innerHTML += ""
                        + "<tr>"
                        + "<td>" + data[i]["iIdBanco"] + "</td>"
                        + "<td>" + data[i]["sNombreBanco"] + "</td>"
                        + "<td></td>"
                        + "<td>" + data[i]["iClave"] + "</td>"
                        + "</tr>";
                    select.innerHTML += "<option value='" + data[i]["iIdBanco"] + "'>" + data[i]["sNombreBanco"] + "</option>"
                }
                $('#tabbancos').DataTable({
                    "language": {
                        "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                    },

                });
            }
        });
    }
    // LLENA TABLA EMPRESAS TABLA EMPRESAS - BANCOS
    LoadEmpresasBancos = () => {
        $.ajax({
            url: "../Empresas/LoadSEmp",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {

                var tab = document.getElementById("tbody-bancos-empresas");
                tab.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    tab.innerHTML += ""
                        + "<td colspan='3' class='col-md-12 row'>"
                        + "<div class='col-md-4'>" + data[i]["IdEmpresa"] + "</div>"
                        + "<div class='col-md-4'>" + data[i]["NombreEmpresa"] + "</div>"
                        + "<div class='col-md-4'>"
                        + "<div class='ml-1 btn badge badge-pill badge-success' onclick='mostrarbancosempresa(" + "\"collapse-" + data[i]["NombreEmpresa"].replace(/ /g, "") + "\"," + data[i]["IdEmpresa"] + ");'> Ver <i class='fas fa-search-dollar' ></i></div>"
                        + "<div class='ml-1 btn badge badge-pill badge-primary' onclick='mostrarmodalnuevo(" + data[i]["IdEmpresa"] + ");'>Agregar <i class='fas fa-plus'></i></div>"
                        + "</div>"
                        + "<div id='collapse-" + data[i]["NombreEmpresa"].replace(/ /g, "") + "' class='col-md-12 collapse-" + data[i]["NombreEmpresa"].replace(/ /g, "") + "'></div>"
                        + "</td> ";
                }
                $('#tabbancosempresa').DataTable({
                    "language": {
                        "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                    },
                    ordering: false,
                    searching: false
                });
            }
        });
    }
    // LLENA LOS BANCOS QUE TIENE PARA NOMINA CADA UNA DE LAS EMPRESAS
    mostrarbancosempresa = (collapse, Empresa_id) => {
        $.ajax({
            url: "../Catalogos/LoadBancosEmpresa",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                document.getElementById(collapse).innerHTML = "";
                document.getElementById(collapse).innerHTML += "<table class='table table-sm table-in-fechas-periodos col-md-12 m-3'>" +
                    "<thead class='col-md-12'>" +
                    "<tr>" +
                    "<th>Banco Id</th>" +
                    "<th>Nombre Banco</th>" +
                    "<th># Cliente</th>" +
                    "<th>Plaza</th>" +
                    "<th># Cuenta Empresa</th>" +
                    "<th>Clabe</th>" +
                    "<th>Tipo Banco</th>" +
                    "<th class='text-center'>Activo</th>" +
                    "<th class=''>Acciones</th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='tab" + collapse + "' class=''></tbody>" + "</table>";
                for (var j = 0; j < data.length; j++) {
                    if (data[j]["Cancelado"] == "True") {
                        document.getElementById("tab" + collapse).innerHTML += "<tr>" +
                            "<td class=''>" + data[j]["Banco_id"] + "</td>" +
                            "<td>" + data[j]["Descripcion"] + "</td>" +
                            "<td>" + data[j]["Num_cliente"] + "</td>" +
                            "<td>" + data[j]["Plaza"] + "</td>" +
                            "<td>" + data[j]["Num_Cta_Empresa"] + "</td>" +
                            "<td>" + data[j]["Clabe"] + "</td>" +
                            "<td class=''>" + data[j]["tipo_banco"] + "</td>" +
                            "<td class='text-center'><div><i class='fas fa-eye-slash text-danger'></i> </div></td>" +
                            "<td class='row'>" +
                            "<div title='Activar' class='ml-1 badge badge-pill badge-dark btn btn-sm' onclick='editarbanco(" + 3 + "," + data[j]["idBanco_Emp"] + ",\"" + collapse + "\"," + data[j]["Empresa_id"] + ");'><i class='far fa-check-circle fa-lg'></i></div>" +
                            //"<div title='Eliminar' class='ml-1 badge badge-pill badge-danger btn' onclick='editarbanco(" + 2 + "," + data[j]["idBanco_Emp"] + ",\"" + collapse + "\");'><i class='far fa-trash-alt fa-lg'></i></div>" +
                            "<div class='ml-1 badge badge-pill badge-info btn' onclick='mostrarmodaleditarbanco(" + data[j]["Empresa_id"] + "," + data[j]["Banco_id"] + "," + data[j]["tipo_banco_id"] + "," + data[j]["idBanco_Emp"] + ",\"" + collapse + "\");'><i class='fas fa-edit fa-lg'></i> Editar</div>" +
                            "</td>" +
                            "</tr>";
                    } else {
                        document.getElementById("tab" + collapse).innerHTML += "<tr>" +
                            "<td class=''>" + data[j]["Banco_id"] + "</td>" +
                            "<td>" + data[j]["Descripcion"] + "</td>" +
                            "<td>" + data[j]["Num_cliente"] + "</td>" +
                            "<td>" + data[j]["Plaza"] + "</td>" +
                            "<td>" + data[j]["Num_Cta_Empresa"] + "</td>" +
                            "<td>" + data[j]["Clabe"] + "</td>" +
                            "<td class=''>" + data[j]["tipo_banco"] + "</td>" +
                            "<td class='text-center'><div><i class='fas fa-eye text-primary fa-lg'></i></div></td>" +
                            "<td class='row'>" +
                            "<div title='Desactivar' class='ml-1 badge badge-pill badge-dark btn btn-sm' onclick='editarbanco(" + 1 + "," + data[j]["idBanco_Emp"] + ",\"" + collapse + "\"," + data[j]["Empresa_id"] + ");'><i class='far fa-times-circle fa-lg'></i></div>" +
                            //"<div title='Eliminar' class='ml-1 badge badge-pill badge-danger btn' onclick='editarbanco(" + 2 + "," + data[j]["idBanco_Emp"] + ",\"" + collapse + "\");'><i class='far fa-trash-alt fa-lg'></i></div>" +
                            "<div class='ml-1 badge badge-pill badge-info btn' onclick='mostrarmodaleditarbanco(" + data[j]["Empresa_id"] + "," + data[j]["Banco_id"] + "," + data[j]["tipo_banco_id"] + "," + data[j]["idBanco_Emp"] + ",\"" + collapse + "\");'><i class='fas fa-edit fa-lg'></i> Editar</div>" +
                            "</td>" +
                            "</tr>";
                    }

                }
                $(".collapse").collapse("hide");
                $("#" + collapse).collapse("toggle");
            }
        });
    }
    // MOSTRAR MODAL NUEVO BANCO EN EMPRESA
    mostrarmodalnuevo = (Empresa_id) => {
        $.ajax({
            url: "../Catalogos/LoadTipoBanco",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var select = document.getElementById("newtipobanco");
                select.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    //if ( data[i]["iId"] == 285 || data[i]["iId"] == 286 ) {
                    select.innerHTML += "<option value='" + data[i]["iId"] + "'>" + data[i]["sValor"] + "</option>";
                    //}
                }
                $("#newempresa").val(Empresa_id);
                $("#modal-nuevo-bancoempresa").modal("show");

            }
        });


    }
    // FUNCION QUE REINICIA EL MODAL NUEVO
    $('#modal-nuevo-bancoempresa').on('hidden.bs.modal', function () {
        document.getElementById("formnewbanco").classList.remove("was-validated").reset();
    });
    // GUARDAR NUEVO BANCO EN EMPRESA
    $("#btnnewbanco").on("click", function () {

        var form = document.getElementById("formnewbanco");
        if (form.checkValidity() === false) {
            form.classList.add("was-validated");
        } else {

            var cliente, plaza;

            if (document.getElementById("newcliente").value == "") {
                cliente = "0";
            } else {
                cliente = document.getElementById("newcliente").value;
            }

            if (document.getElementById("newplaza").value == "") {
                plaza = "0";
            } else {
                plaza = document.getElementById("newplaza").value;
            }

            var tb = document.getElementById("newtipobanco");
            $.ajax({
                url: "../Catalogos/SaveNewBanco",
                type: "POST",
                data: JSON.stringify({
                    Empresa_id: document.getElementById("newempresa").value
                    , Banco_id: document.getElementById("newbanco").value
                    , TipoBanco: tb.value
                    , Cliente: cliente
                    , Plaza: plaza
                    , CuentaEmp: document.getElementById("newcuentaempresa").value
                    , Clabe: document.getElementById("newclabe").value
                }),
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    if (data[0] == '0') {
                        Swal.fire({
                            title: 'Error!',
                            text: data[1],
                            icon: 'warning',
                            timer: 1000
                        });
                    } else {
                        $("#modal-nuevo-bancoempresa").modal("hide");
                        $("div.collapse.show").collapse("hide");
                        Swal.fire({
                            title: 'Completo!',
                            text: data[1],
                            icon: 'success',
                            timer: 1000
                        });
                    }


                }
            });
        }
    });
    // MOSTRAR MODAL EDITAR BANCO
    mostrarmodaleditarbanco = (Empresa_id, Banco_id, TipoBanco, BancoEmp, collapse) => {
        $.ajax({
            url: "../Empleados/LoadBanks",
            type: "POST",
            data: JSON.stringify({ keyban: 0 }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                $("#editempresa").val(Empresa_id)
                var select = document.getElementById("editbanco");
                select.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    if (Banco_id == data[i]["iIdBanco"]) {
                        select.innerHTML += "<option value='" + data[i]["iIdBanco"] + "' selected>" + data[i]["sNombreBanco"] + "</option>"
                    } else {
                        //select.innerHTML += "<option value='" + data[i]["iIdBanco"] + "'>" + data[i]["sNombreBanco"] + "</option>"
                    }
                }
                $.ajax({
                    url: "../Catalogos/LoadTipoBanco",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: (data) => {
                        var select = document.getElementById("edittipobanco");
                        select.innerHTML = "";
                        for (var j = 0; j < data.length; j++) {
                            if (TipoBanco == data[j]["iId"]) {
                                select.innerHTML += "<option value='" + data[j]["iId"] + "' selected>" + data[j]["sValor"] + "</option>";
                            } else {
                                select.innerHTML += "<option value='" + data[j]["iId"] + "'>" + data[j]["sValor"] + "</option>";
                            }
                        }

                        $.ajax({
                            url: "../Catalogos/LoadBancosEmpresa",
                            type: "POST",
                            data: JSON.stringify({ Empresa_id: Empresa_id }),
                            contentType: "application/json; charset=utf-8",
                            success: (data) => {
                                for (var i = 0; i < data.length; i++) {
                                    if (data[i]["Empresa_id"] == Empresa_id && data[i]["Banco_id"] == Banco_id) {
                                        $("#editcliente").val(data[i]["Num_cliente"]);
                                        $("#editplaza").val(data[i]["Plaza"]);
                                        $("#editcuentaempresa").val(data[i]["Num_Cta_Empresa"]);
                                        $("#editclabe").val(data[i]["Clabe"]);

                                    }
                                }
                                $("#editarid").val(BancoEmp);
                                $("#editarcollapse").val(collapse);
                                $("#modal-editar-bancoempresa").modal("show");

                            }
                        });

                    }
                });
            }
        });
    }
    // GUARDAR ACTUALIZACION DE BANCO EN LA EMPRESA
    $("#btneditarbanco").on("click", function () {
        var form = document.getElementById("formeditbanco");
        if (form.checkValidity() === false) {
            form.classList.add("was-validated");
        } else {

            var cliente = "", plaza = "";
            if ($("#editcliente").val() == "") {
                cliente = "0";
            } else {
                cliente = $("#editcliente").val();
            }
            if ($("#editplaza").val() == "") {
                plaza = "0";
            } else {
                plaza = $("#editplaza").val();
            }

            var tb = document.getElementById("edittipobanco");
            $.ajax({
                url: "../Catalogos/UpdateBancoEmpresa",
                type: "POST",
                data: JSON.stringify({
                    Banco_id: $("#editbanco").val()
                    , TipoBanco: tb.value
                    , Id: $("#editarid").val()
                    , Cliente: cliente
                    , Plaza: plaza
                    , CuentaEmp: $("#editcuentaempresa").val()
                    , Clabe: $("#editclabe").val()
                }),
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    if (data[0] == '0') {
                        Swal.fire({
                            title: 'Error!',
                            text: data[1],
                            icon: 'danger'
                        });
                    } else if (data[0] == '1') {
                        $("#" + $("#editarcollapse").val()).collapse("hide");
                        $("#modal-editar-bancoempresa").modal("hide");
                        Swal.fire({
                            title: 'Correcto!',
                            text: data[1],
                            icon: 'success',
                            timer: 1000
                        });
                    } else if (data[0] == '2') {
                        Swal.fire({
                            title: 'Advertencia!',
                            text: data[1],
                            icon: 'warning'
                        });
                    }


                }
            });
        }
    });
    //
    // FUNCION QUE REINICIA EL MODAL NUEVO
    $('#modal-editar-bancoempresa').on('hidden.bs.modal', function () {
        document.getElementById("formeditbanco").classList.remove("was-validated");
    });
    //
    // EDIT BANK TWO OPTIONS 
    editarbanco = (key, Id, collapse) => {
        var texto = "";
        if (key == 1) {
            texto = "El banco será desactivado de la empresa"
        } else if (key == 2) {
            texto = "El banco será eliminado de la empresa"
        } else if (key == 3) {
            texto = "El banco será activado en la empresa"
        }
        Swal.fire({
            title: 'Estas seguro?',
            text: texto,
            icon: 'warning',
            showCancelButton: true,
            CancelButtonText: 'Cancelar',
            confirmButtonColor: '#d33',
            cancelButtonColor: '#98959B',
            confirmButtonText: 'Confirmar'
        }).then((result) => {
            if (result.value) {

                $.ajax({
                    url: "../Catalogos/UpdateBanco",
                    type: "POST",
                    data: JSON.stringify({ key: key, Id: Id }),
                    contentType: "application/json; charset=utf-8",
                    success: (data) => {
                        $("#" + collapse).collapse("toggle");
                        if (data[0] == 0) {
                            Swal.fire({
                                icon: 'warning',
                                title: 'Error!',
                                text: data[1],
                                timer: 3000
                            });
                        } else if (data[0] == 1) {
                            Swal.fire({
                                icon: 'success',
                                title: 'Hecho!',
                                text: data[1],
                                timer: 3000
                            });
                        } else if (data[0] == 2) {
                            Swal.fire({
                                icon: 'success',
                                title: 'Hecho!',
                                text: data[1],
                                timer: 3000
                            });
                        } else if (data[0] == 3) {
                            Swal.fire({
                                icon: 'success',
                                title: 'Hecho!',
                                text: data[1],
                                timer: 3000
                            });
                        }
                    }
                });
            }
        });
    }
    //
    ///////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////
    //////////////////////     CATALOGO USUARIOS     //////////////////////
    ///////////////////////////////////////////////////////////////////////
    //
    // Carga tabla de Usuarios disponibles
    loadtabusers = () => {
        $.ajax({
            url: "../Catalogos/LoadUsers",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var tab = document.getElementById("tabusersbody");
                tab.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    if (data[i]["Cancelado"] == "True") {
                        text = "<i class='fas fa-eye-slash fa-lg text-danger'></i>";
                        btns = "<a class='badge badge-light btn btns border'><i class='fas fa-eye fa-lg text-primary'></i> Activar</a>";
                    } else {
                        text = "<i class='fas fa-eye fa-lg text-primary'></i>";
                        btns = "<a class='badge badge-light btn btns border'><i class='fas fa-eye-slash fa-lg text-danger'></i> Desactivar</a>";
                    }
                    tab.innerHTML += "" +
                        "<tr>" +
                        "<td>" + data[i]["Usuario"] + "</td>" +
                        "<td>" + data[i]["Perfil_id"] + "</td>" +
                        "<td>" + data[i]["Ps"] + "</td>" +
                        "<td>" + text + "</td>" +
                        "<td>" +
                        "<div class='badge badge-success btn'><i class='fas fa-edit fa-lg'></i> Editar</div>" +
                        "<div class='badge badge-info btn mx-1' onclick='mostarmodalnewperfil();'><i class='fas fa-plus fa-lg'></i> Nuevo</div>" +
                        btns +
                        "</td>" +
                        "</tr>";
                }
                $('#tabUsers').DataTable({
                    "language": {
                        "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                    },
                });
            }
        });
    }
    //
    // Carga tabla de Perfiles de usuarios
    loadtabprofiles = () => {

        $.ajax({
            url: "../Catalogos/LoadProfiles",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var tab = document.getElementById("tabprofilesbody");
                tab.innerHTML = "";
                var text = "";
                var btns = "";
                for (var i = 0; i < data.length; i++) {
                    if (data[i]["Cancelado"] == "True") {
                        text = "<i class='fas fa-eye-slash fa-lg text-danger'></i>";
                        btns = "<div class='badge badge-light btn btns border'><i class='fas fa-eye fa-lg text-primary'></i> Activar</div>";
                    } else {
                        text = "<i class='fas fa-eye fa-lg text-primary'></i>";
                        btns = "<div class='badge badge-light btn btns border'><i class='fas fa-eye-slash fa-lg text-danger'></i> Desactivar</div>";
                    }
                    tab.innerHTML += "" +
                        "<tr ondblclick='loadtwo(" + data[i]["IdPerfil"] + ");' onclick=''>" +
                        "<td>" + data[i]["IdPerfil"] + "</td>" +
                        "<td>" + data[i]["Perfil"] + "</td>" +
                        "<td>" + data[i]["Fecha_Alta"] + "</td>" +
                        "<td>" + text + "</td>" +
                        "<td> " + btns + "<div class='badge badge-info btn '><i class='fas fa-edit fa-lg'></i> Editar</div></td>" +
                        "</tr>";
                }
                $('#tabProfiles').DataTable({
                    "language": {
                        "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                    },
                    searching: false
                });
            }
        });
    }
    //
    // FUNCION PARA HACER SECUENCIA PARA DAR MOVIEMIENTO A LOS BADGES
    $(".btns").hover(function () {
        $('.btns').removeClass('badge-light').addClass('badge-dark');
    }, function () {
        $('.btns').removeClass('badge-dark').addClass('badge-light');
    });

    // FUNCION DE un CLICK EN EL ROW 
    $("#collapseProfiles").show(function () {

        $(this).html("<div class='container row col-md-12'>" +
            "<div class='col-sm'>Empresas</div>" +
            "<div class='col-sm'>Empleado</div>" +
            "<div class='col-sm'>Incidencias</div>" +
            "<div class='col-sm'>Nomina</div>" +
            "<div class='col-sm'>Reportes</div>" +
            "<div class='col-sm'>Catalogos</div>" +
            "<div class='col-sm'><label class='col-md-12'>Consulta <button class='btn btn-light btn-sm py-0' data-toggle='collapse' data-target='.collapse-consulta' type='button' onclick='loadcheck(35, \"collapse-cons\");'><i class='fas fa-caret-down fa-lg'></i></button></label>" +
            "<div class='collapse collapse-cons col-md-12'></div>" +
            "</div>" +
            "<div class='col-sm'>Kiosko</div>" +
            "</div>"
        );
    });
    //
    // carga los menus principales
    //


    loadmainchecks = () => {

        var btns = "";
        var item;
        var nom;

        $.ajax({
            url: "../Catalogos/Loadmainmenus",
            type: "POST",
            data: JSON.stringify({}),
            contentType: "application/json; charset=utf-8",
            success: (data) => {

                for (var i = 0; i < data.length; i++) {
                    item = data[i]['iIdItem'];
                    nom = data[i]['sNombre'];

                    var collapse =
                        "<div class='accordion col-md-6 my-0 py-0' id='accordion" + item + "'>" +
                        "<div class='card' >" +
                        "<div class='my-0 py-0 btn btn-light' id='heading" + item + "'>" +
                        "<div class='row d-flex justify-content-between align-content-end flex-wrap'>" +
                        "<div class='custom-control custom-checkbox ml-3  h6 py-1'>" +
                        "<input type='checkbox' class='custom-control-input' id='check" + item + "' name='maincheck' value='" + item + "' status='0' onclick='loadsubchecks(\"check" + item + "\",\"collapse" + item + "\", " + item + ");'>" +
                        "<label class='custom-control-label pt-1' for='check" + item + "'>" + nom + "</label>" +
                        "</div>" +
                        "<div class='btn btn-link' onclick='changeicon(\"collapse" + item + "\",\"icon" + item + "\");' data-toggle='collapse' data-target='#collapse" + item + "' aria-expanded='true' aria-controls='collapse" + item + "'>" +
                        "<span id='icon" + item + "' class='fas fa-bars text-success fa-lg' status='0'></span>" +
                        "</div>" +
                        "</div>" +
                        "</div>" +
                        "<div id='collapse" + item + "' class='collapse my-0 py-0' aria-labelledby='heading" + item + "' data-parent='#accordion" + item + "'>" +
                        "<div class='card-body my-0 py-0' id='card-body-" + item + "'>" +
                        "</div>" +
                        "</div>" +
                        "</div>" +
                        "</div>";
                    btns += collapse
                }

                $("#formbodyprofiles").html(btns);

            }
        });

    }

    loadsubchecks = (check, collapse, item) => {
        var status = $("#" + check).attr("status");
        var subcheck = "";
        var subchecks = "";
        if (status == "0" || status == 0) {

            $.ajax({
                url: "../Catalogos/Loadonemenu",
                type: "POST",
                data: JSON.stringify({ Id: item }),
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    subchecks = "<ul class='list-group list-group-flush'>"
                    if (data.length == 0 || data == null || data == "") {

                    } else {
                        for (var i = 0; i < data.length; i++) {
                            subcheck =
                                "<li class='list-group-item my-0 py-0 d-flex justify-content-start font-labels align-items-center'>" +
                                "<div class='custom-control custom-checkbox ml-3'>" +
                                "<input type='checkbox' class='custom-control-input' id='check" + data[i]["iIdItem"] + "' name='subcheck' value='"+data[i]["iIdItem"]+"' parent='" + item + "'>" +
                                "<label class='custom-control-label' for='check" + data[i]["iIdItem"] + "'>" + data[i]["sNombre"] + "</label>" +
                                "</div>" +
                                "</li>";
                            subchecks += subcheck;
                        }
                        console.log(subchecks);
                    }
                    subchecks += "</ul>";
                    $("#" + "card-body-" + item).html(subchecks);
                }
            });

            $("#" + check).attr("status", "1");
            changeicon(collapse, "icon" + item);
        } else if (status == "1" || status == 1) {
            changeicon(collapse, "icon" + item);
        }
    }

    changeicon = (collapse, icon) => {

        var status = $("#" + icon).attr("status");

        if (status == "0" || status == 0) {
            $("#" + collapse).collapse("show");

            $("#" + icon).attr("class", "fas fa-times text-success fa-lg");
            $("#" + icon).attr("status", "1");
        } else if (status == "1" || status == 1) {
            $("#" + collapse).collapse("hide");

            $("#" + icon).attr("class", "fas fa-bars text-success fa-lg");
            $("#" + icon).attr("status", "0");
        }
    }

    // carga los checks que pertenecen a cada menu principal
    loadcheck = (id, collapse) => {
        if ($('.' + collapse).prop('show')) {

        } else {
            $.ajax({
                url: "../Catalogos/Loadonemenu",
                type: "POST",
                data: JSON.stringify({ Id: id }),
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    $("." + collapse).html("");
                    var body = "";
                    for (var i = 0; i < data.length; i++) {
                        body +=
                            "<div class='custom-control custom-checkbox'>" +
                            "<input class='custom-control-input' type='checkbox' id='" + data[i]["iParent"] + "-" + data[i]["iIdItem"] + "' name='menuitem' value='" + data[i]["iIdItem"] + "'>" +
                            "<label class='custom-control-label' for='" + data[i]["iParent"] + "-" + data[i]["iIdItem"] + "'>" + data[i]["sNombre"] + "</label>" +
                            "</div><div class='dropdown-divider'></div>";
                    }
                    $("." + collapse).html(body);
                    $("." + collapse).collapse("show");

                }
            });
        }
    }

    carganewperfil = () => {

        var form = $("#frmProfiles").serialize();
        console.log(form);

        $.ajax({
            url: "../Catalogos/SaveNewPerfil",
            type: "POST",
            data: JSON.stringify({ form }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                
            }
        });
    }

    // CARGA MODAL DEL NUEVO USUARIO
    mostarmodalnewperfil = () => {
        $("#modalnewusuario").modal("show");
    }

    $("#btnnewperfil").on("click", function () {
        var data = $("#frmProfiles").serialize();
    });
    ////////////////////////////////////////////
    //////////  CENTROS DE COSTO  //////////////
    ////////////////////////////////////////////
    // CARGA TABLA DE EMPRESAS EN CENTROS DE COSTO
    LoadCentrosCostos = () => {
        $.ajax({
            url: "../Empresas/LoadSEmp",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var tab = document.getElementById("bodytab-centroscostos");
                tab.innerHTML = "";
                var empresa;
                for (var i = 0; i < data.length; i++) {
                    if (i == 0) {
                        empresa = data[i]["IdEmpresa"];
                    }
                    tab.innerHTML += "" +
                        "<tr>" +
                        "<td colspan='3'>" +
                        "<div class='col-md-12 row'>" +
                        "<label class='col-md-2'>" + data[i]['IdEmpresa'] + "</label>" +
                        "<label class='col-md-7'>" + data[i]['NombreEmpresa'] + " </label>" +
                        "<div class='col-md-3'>" +
                        "<div class='badge badge-success btn' onclick='LoadDetalleCentrosCostos(\"collapse-" + data[i]["NombreEmpresa"].replace(/ /g, "") + "\", " + data[i]["IdEmpresa"] + ");'> Ver <i class='fas fa-eye'></i></div>" +
                        "<div class='badge badge-info btn mx-1' onclick='LoadModalNuevoCentrosCostos(" + data[i]["IdEmpresa"] + ");'> Nuevo <i class='fas fa-plus'></i></div>" +
                        "</div>" +
                        "<div id='collapse-" + data[i]["NombreEmpresa"].replace(/ /g, "") + "' class='collapse collapse-" + data[i]['NombreEmpresa'].replace(/ /g, "") + " col-md-12'>" +
                        "</div>" +
                        "</div>" +
                        "</td >" +
                        "</tr >";
                }
                //setTimeout(function () {
                //    $("#table-centros-costos").DataTable({
                //        "language": {
                //            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                //        }
                //    });
                //}, 1000);
            }
        });
    }
    // CARGA TABLA DETALLE DE CADA EMPRESA CON SUS CENTROS DE COSTO
    LoadDetalleCentrosCostos = (pilltab, Empresa_id) => {
        $.ajax({
            url: "../Catalogos/LoadCentrosCostoDetalle",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                document.getElementById(pilltab).innerHTML = "";
                document.getElementById(pilltab).innerHTML += "<table class='table table-sm table" + Empresa_id + " table-in-centros-costos col-md-12 pb-4'>" +
                    "<thead class='col-md-12'>" +
                    "<tr>" +
                    "<th class=''> Id </th>" +
                    "<th class=''> Centro Costo </th>" +
                    "<th class=''> Descripción </th>" +
                    "<th class=''> Estatus </th>" +
                    "<th class=''> Fecha Alta </th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='tab" + pilltab + "' class=''></tbody>" + "</table>";
                for (var j = 0; j < data.length; j++) {
                    if (data[j]["Estado"] == "0") {
                        document.getElementById("tab" + pilltab).innerHTML += "<tr>" +
                            "<td class=''>" + data[j]["IdCentroCosto"] + "</td>" +
                            "<td class=''>" + data[j]["CentroCosto"] + "</td>" +
                            "<td class=''>" + data[j]["Descripcion"] + "</td>" +
                            "<td class=''>" + "<i class='fas fa-eye text-primary'></i>" + "</td>" +
                            "<td class=''>" + data[j]["Fecha_Alta"].substr(0, 10) + "</td>" +
                            "</tr>";
                    } else {
                        document.getElementById("tab" + pilltab).innerHTML += "<tr>" +
                            "<td class=''>" + data[j]["IdCentroCosto"] + "</td>" +
                            "<td class=''>" + data[j]["CentroCosto"] + "</td>" +
                            "<td class=''>" + data[j]["Descripcion"] + "</td>" +
                            "<td class=''>" + "<i class='fas fa-eye-slash text-danger'></i>" + "</td>" +
                            "<td class=''>" + data[j]["Fecha_Alta"].substr(0, 10) + "</td>" +
                            "</tr>";
                    }
                }
                $(".collapse").collapse("hide").addClass("bg-light p-4 rounded");
                $("#" + pilltab).collapse("toggle");
                setTimeout(function () {
                    $(".table" + Empresa_id + "").DataTable({
                        "language": {
                            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                        }
                    });
                }, 100);
            }
        });
    }
    // carga modal nuevo centro costo
    LoadModalNuevoCentrosCostos = (Empresa_id) => {
        $("#newempresa").val(Empresa_id);
        $("#modal-nuevo-centrocostos").modal("show");
    }
    // GUARDA NUEVO CENTRO COSTO
    $("#btnsavecentrocosto").on("click", function () {
        var form = document.getElementById("frmnuevocentrocostos");
        if (form.checkValidity() === false) {
            form.classList.add("was-validated");
        } else {

            $.ajax({
                url: "../Catalogos/SaveNewCentrosCostos",
                type: "POST",
                data: JSON.stringify({
                    Empresa_id: document.getElementById("newempresa").value
                    , Nombre: document.getElementById("newnombre").value
                    , Descripcion: document.getElementById("newdescripcion").value
                }),
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    if (data[0] == '0') {
                        Swal.fire({
                            title: 'Error!',
                            text: data[1],
                            icon: 'warning'
                        });
                    } else {
                        $("#modal-nuevo-centrocostos").modal("hide");
                        $("div.collapse.show").collapse("hide");
                        Swal.fire({
                            title: 'Completo!',
                            text: data[1],
                            icon: 'success',
                            timer: 1000
                        });
                    }


                }
            });
        }
    });
    ////////////////////////////////////////////
    ////////  REGISTROS PATRONALES  ////////////
    ////////////////////////////////////////////
    // CARGA TABLA DE EMPRESAS EN REGISTROS PATRONALES
    LoadRegistrosPatronales = () => {
        $.ajax({
            url: "../Empresas/LoadSEmp",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var tab = document.getElementById("bodytab-registropatronal");
                tab.innerHTML = "";
                var empresa;
                for (var i = 0; i < data.length; i++) {
                    if (i == 0) {
                        empresa = data[i]["IdEmpresa"];
                    }
                    tab.innerHTML += "" +
                        "<tr>" +
                        "<td colspan='3' >" +
                        "<div class='col-md-12 row'>" +
                        "<label class='col-md-2'>" + data[i]['IdEmpresa'] + "</label><label class='col-md-7'>" + data[i]['NombreEmpresa'] + " </label><div class='col-md-3'>" +
                        "<div class='badge badge-success btn' onclick='LoadDetalleRegistrosPatronales(\"collapse-" + data[i]["NombreEmpresa"].replace(/ /g, "") + "\", " + data[i]["IdEmpresa"] + ");'>Ver <i class='fas fa-eye'></i></div>" +
                        "<div class='badge badge-info btn mx-1' onclick='LoadModalNuevoRegistrosPatronales(" + data[i]["IdEmpresa"] + ");'> Nuevo <i class='fas fa-plus'></i></div>" +
                        "</div>" +
                        "<div id='collapse-" + data[i]["NombreEmpresa"].replace(/ /g, "") + "' class='collapse table-responsive collapse-" + data[i]['NombreEmpresa'].replace(/ /g, "") + " col-md-12'>" +
                        "</div>" +
                        "</div>" +
                        "</td >" +
                        "</tr >";
                }

            }
        });
    }
    // CARGA TABLA DETALLE DE CADA EMPRESA CON SUS REGISTROS PATRONALES
    LoadDetalleRegistrosPatronales = (pilltab, Empresa_id) => {
        $.ajax({
            url: "../Empresas/LoadRegistrosPatronalesDetalle",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                document.getElementById(pilltab).innerHTML = "";
                document.getElementById(pilltab).innerHTML += "<table class='table table-sm table" + Empresa_id + " table-in-registros-patronales col-md-12 pb-4'>" +
                    "<thead class='col-md-12'>" +
                    "<tr>" +
                    "<th class=''> Id </th>" +
                    "<th class=''> Afiliacion IMSS </th>" +
                    "<th class=''> Nombre Afiliación </th>" +
                    "<th class=''> Riesgo de trabajo </th>" +
                    "<th class=''> Clase </th>" +
                    "<th class=''> Estatus </th>" +
                    "<th class=''> Acciones </th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='tab" + pilltab + "' class=''></tbody>" + "</table>";
                for (var j = 0; j < data.length; j++) {
                    if (data[j]["Cancelado"] == "True") {
                        document.getElementById("tab" + pilltab).innerHTML += "<tr>" +
                            "<td class=''>" + data[j]["IdRegPat"] + "</td>" +
                            "<td class=''>" + data[j]["Afiliacion_IMSS"] + "</td>" +
                            "<td class=''>" + data[j]["Nombre_Afiliacion"] + "</td>" +
                            "<td class=''>" + data[j]["Riesgo_Trabajo"] + "</td>" +
                            "<td class=''>" + data[j]["ClasesRegPat_id"] + "</td>" +
                            "<td class=''>" + "<i class='fas fa-eye-slash text-danger' title='Inactivo'></i>" + "</td>" +
                            "<th class=''>" +
                            "<div class='badge badge-success btn mx-1' title='Editar' onclick='editarregistropatronal(" + Empresa_id + "," + data[j]["IdRegPat"] + ");'> Editar <i class='fas fa-edit'></i></div>" +
                            "<div class='badge badge-success btn' title='Activar' onclick='editarestatusregistropatronal(" + Empresa_id + "," + data[j]["IdRegPat"] + ",0,\"" + pilltab + "\");'><i class='fas fa-check'></i></div>" +
                            "</th>" +
                            "</tr>";
                    } else {
                        document.getElementById("tab" + pilltab).innerHTML += "<tr>" +
                            "<td class=''>" + data[j]["IdRegPat"] + "</td>" +
                            "<td class=''>" + data[j]["Afiliacion_IMSS"] + "</td>" +
                            "<td class=''>" + data[j]["Nombre_Afiliacion"] + "</td>" +
                            "<td class=''>" + data[j]["Riesgo_Trabajo"] + "</td>" +
                            "<td class=''>" + data[j]["ClasesRegPat_id"] + "</td>" +
                            "<td class=''>" + "<i class='fas fa-eye text-primary' title='Activo'></i>" + "</td>" +
                            "<th class=''>" +
                            "<div class='badge badge-success btn mx-1' title='Editar' onclick='editarregistropatronal(" + Empresa_id + "," + data[j]["IdRegPat"] + ");'> Editar <i class='fas fa-edit'></i></div>" +
                            "<div class='badge badge-warning btn' title='Desactivar' onclick='editarestatusregistropatronal(" + Empresa_id + "," + data[j]["IdRegPat"] + ",1,\"" + pilltab + "\");'><i class='fas fa-times fa-lg'></i></div>" +
                            "</th>" +
                            "</tr>";
                    }
                }
                $(".collapse").collapse("hide").addClass("bg-light p-4 rounded");
                $("#" + pilltab).collapse("toggle");
                setTimeout(function () {
                    $(".table" + Empresa_id + "").DataTable({
                        "language": {
                            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                        }
                    });
                }, 100);
            }
        });
    }

    LoadModalNuevoRegistrosPatronales = (Empresa_id) => {
        loadClases();
        $("#modal-nuevo-registropatronal").modal("show");

        $("#newempresa").val(Empresa_id);

    }

    editarestatusregistropatronal = (Empresa_id, RegPat_id, Status, collapse) => {
        //alert("entra");
        //Swal.fire({
        //    title: 'Estas seguro?',
        //    text: "El registro sera borrado definitivamente!",
        //    icon: 'warning',
        //    showCancelButton: true,
        //    CancelButtonText: 'Cancelar',
        //    confirmButtonColor: '#d33',
        //    cancelButtonColor: '#98959B',
        //    confirmButtonText: 'Confirmar'
        //}).then((result) => {
        //    if (result.value) {
        $.ajax({
            url: "../Catalogos/ChangestatusRegistroPatronal",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                Empresa_id: Empresa_id
                , RegPat_id: RegPat_id
                , Status: Status
            }),
            success: (data) => {
                if (data[0] == '0') {
                    Swal.fire({
                        title: 'Aviso!',
                        icon: 'warning',
                        text: data[1]
                    });
                } else if (data[0] == '1') {
                    $("#" + collapse).collapse("toggle");
                    Swal.fire({
                        title: 'Correcto',
                        icon: 'success',
                        text: data[1]
                    });
                }
            }
        });
        //    }
        //});


    }

    editarregistropatronal = (Empresa_id, RegPat_id) => {
        alert("si");
    }
    //Carga catalogo Clases 
    loadClases = () => {
        $.ajax({
            url: "../Empresas/LoadClasesRP",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                document.getElementById("newclase").innerHTML = "<option vlaue=''>Selecciona</option>";
                for (i = 0; i < data.length; i++) {
                    document.getElementById("newclase").innerHTML += `<option value='${data[i].IdClase}'>${data[i].Nombre_Clase}</option>`;

                    //document.getElementById("btnUpdateRP").classList.add("invisible");
                }
            }
        });
    }
    // Guardar nuevo registro patronal
    $("#btnsaveregistropatronal").on("click", function () {
        var form = document.getElementById("frmnuevoregistropatronal");
        if (form.checkValidity() === false) {
            form.classList.add("was-validated");
        } else {

            $.ajax({
                url: "../Catalogos/SaveNewRegistroPatronal",
                type: "POST",
                data: JSON.stringify({
                    Empresa_id: $("#newempresa").val()
                    , Afiliacion_IMSS: $("#newafiliacion").val()
                    , NombreAfiliacion: $("#newnombre").val()
                    , RiesgoTrabajo: $("#newriesgotrabajo").val()
                    , Clase: document.getElementById("newclase").value
                }),
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    if (data[0] == '0') {
                        Swal.fire({
                            title: 'Aviso!',
                            icon: 'warning',
                            text: data[1]
                        });
                    } else if (data[0] == '1') {
                        $("div.collapse.show").collapse("hide");
                        Swal.fire({
                            title: 'Correcto',
                            icon: 'success',
                            text: data[1]
                        });
                    }
                }
            });
        }
    });
    ////////////////////////////////////////////
    /////////////  REGIONALES  /////////////////
    ////////////////////////////////////////////
    // CARGA TABLA DE EMPRESAS EN REGIONALES
    LoadRegionales = () => {
        $.ajax({
            url: "../Empresas/LoadSEmp",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var tab = document.getElementById("bodytab-regionales");
                tab.innerHTML = "";
                var empresa;
                for (var i = 0; i < data.length; i++) {
                    if (i == 0) {
                        empresa = data[i]["IdEmpresa"];
                    }
                    tab.innerHTML += "" +
                        "<tr>" +
                        "<td colspan='3' >" +
                        "<div class='col-md-12 row'>" +
                        "<label class='col-md-2'>" + data[i]['IdEmpresa'] + "</label><label class='col-md-7'>" + data[i]['NombreEmpresa'] + " </label><div class='col-md-3'><div class='badge badge-success btn' onclick='LoadDetalleRegionales(\"collapse-" + data[i]["NombreEmpresa"].replace(/ /g, "") + "\", " + data[i]["IdEmpresa"] + ");'>Ver <i class='fas fa-plus'></i></div><div class='badge badge-info btn mx-1' onclick='mostrarmodalnuevaregional(" + data[i]["IdEmpresa"] + ");'> Nuevo <i class='fas fa-plus'></i></div></div>" +
                        "<div id='collapse-" + data[i]["NombreEmpresa"].replace(/ /g, "") + "' class='collapse table-responsive collapse-" + data[i]['NombreEmpresa'].replace(/ /g, "") + " col-md-12'>" +
                        "</div>" +
                        "</div>" +
                        "</td >" +
                        "</tr >";
                }
            }
        });
    }
    // CARGA TABLA DETALLE DE CADA EMPRESA CON SUS REGIONALES
    LoadDetalleRegionales = (pilltab, Empresa_id) => {
        $.ajax({
            url: "../Empresas/LoadRegionalesDetalle",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                document.getElementById(pilltab).innerHTML = "";
                document.getElementById(pilltab).innerHTML += "<table class='table table-sm table" + Empresa_id + " table-in-registros-patronales col-md-12 pb-4'>" +
                    "<thead class='col-md-12'>" +
                    "<tr>" +
                    "<th class=''> Id </th>" +
                    "<th class=''> Clave </th>" +
                    "<th class=''> Descripción </th>" +
                    "<th class=''> Fecha Alta </th>" +
                    "<th class=''> Acciones </th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='tab" + pilltab + "' class=''></tbody>" + "</table>";
                for (var j = 0; j < data.length; j++) {
                    document.getElementById("tab" + pilltab).innerHTML += "<tr>" +
                        "<td class=''>" + data[j]["iIdRegional"] + "</td>" +
                        "<td class=''>" + data[j]["sClaveRegional"] + "</td>" +
                        "<td class=''>" + data[j]["sDescripcionRegional"] + "</td>" +
                        "<td class=''>" + data[j]["sFechaAlta"] + "</td>" +
                        "<td class=''><span class='badge badge-pill badge-danger'><i class='fas fa-trash'></i></span></td></tr>";
                }
                $(".collapse").collapse("hide").addClass("bg-light p-4 rounded");
                $("#" + pilltab).collapse("toggle");
                setTimeout(function () {
                    $(".table" + Empresa_id + "").DataTable({
                        "language": {
                            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                        }
                    });
                }, 100);
            }
        });
    }
    // MUESTA EL MODAL DE NUEVA REGIONAL 
    mostrarmodalnuevaregional = (Empresa_id) => {
        $("#modalnuevaregional").modal("show");
        $("#newinputempresa").val(Empresa_id);
    }
    // GUARDA LA NUEVA REGIONAL 
    $("#btnnewregional").on("click", function () {
        var form = document.getElementById("frmnewregional");
        if (form.checkValidity() === false) {
            form.classList.add("was-validated");
        } else {

            $.ajax({
                url: "../Catalogos/SaveNewRegionalesCatalogo",
                type: "POST",
                cache: false,
                data: JSON.stringify({
                    Empresa_id: $("#newinputempresa").val()
                    , ClaveRegion: $("#newinputclave").val()
                    , Descripcion: $("#newinputdescripcion").val()
                }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    if (data[0] == '0') {
                        Swal.fire({
                            title: 'Error!',
                            text: data[1],
                            icon: 'warning',
                            timer: 1000
                        });
                    } else {
                        $("div.collapse.show").collapse("hide");
                        $("#modalnuevaregional").modal("hide");
                        Swal.fire({
                            title: 'Completo!',
                            text: data[1],
                            icon: 'success',
                            timer: 1000
                        });
                    }
                }
            });
        }
    });
    ////////////////////////////////////////////
    /////////////  SUCURSALES  /////////////////
    ////////////////////////////////////////////
    // CARGA SUCURSALES EN VISTA SUCURSALES
    LoadTabSucursales = () => {
        $.ajax({
            url: "../Empresas/LoadSucursales",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var tab = document.getElementById("bodytab-sucursales");
                tab.innerHTML = "";
                var empresa;
                for (var i = 0; i < data.length; i++) {
                    tab.innerHTML += "" +
                        "<tr>" +
                        "<td>" + data[i]["iIdSucursal"] + "</td>" +
                        "<td>" + data[i]["sClaveSucursal"] + "</td>" +
                        "<td>" + data[i]["sDescripcionSucursal"] + "</td>" +
                        "<td>" + data[i]["sFechaAlta"] + "</td>" +
                        "</tr >";
                }
                setTimeout(function () {
                    $("#tab-sucursales").DataTable({
                        "language": {
                            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                        }
                    });
                }, 1000);
            }
        });
    }

    ////////////////////////////////////////////
    /////////////  LOCALIDADES  ////////////////
    ////////////////////////////////////////////
    // CARGA EMPRESAS EN VISTA SUCURSALES
    LoadLocalidades = () => {
        $.ajax({
            url: "../Empresas/LoadSEmp",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var tab = document.getElementById("bodytab-localidades");
                tab.innerHTML = "";
                var empresa;
                for (var i = 0; i < data.length; i++) {
                    if (i == 0) {
                        empresa = data[i]["IdEmpresa"];
                    }
                    tab.innerHTML += "" +
                        "<tr>" +
                        "<td colspan='3' >" +
                        "<div class='col-md-12 row'>" +
                        "<label class='col-md-2'>" + data[i]['IdEmpresa'] + "</label><label class='col-md-7'>" + data[i]['NombreEmpresa'] + " </label><div class='col-md-3'><div class='badge badge-success btn' onclick='LoadDetalleLocalidades(\"collapse-" + data[i]["NombreEmpresa"].replace(/ /g, "") + "\", " + data[i]["IdEmpresa"] + ");'>Ver <i class='fas fa-plus'></i></div><div class='badge badge-info btn mx-1' onclick='mostrarmodalnuevalocalidad(" + data[i]["IdEmpresa"] + ");'> Nuevo <i class='fas fa-plus'></i></div></div>" +
                        "<div id='collapse-" + data[i]["NombreEmpresa"].replace(/ /g, "") + "' class='collapse table-responsive collapse-" + data[i]['NombreEmpresa'].replace(/ /g, "") + " col-md-12'>" +
                        "</div>" +
                        "</div>" +
                        "</td >" +
                        "</tr >";
                }
            }
        });
    }
    // CARGA TABLA DE LOCALIDADES POR EMPRESA
    LoadDetalleLocalidades = (pilltab, Empresa_id) => {
        $.ajax({
            url: "../Empresas/LoadLocalidadesCatalogos",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                document.getElementById(pilltab).innerHTML = "";
                document.getElementById(pilltab).innerHTML += "<table class='table table-sm table" + Empresa_id + " table-in-localidades col-md-12 pb-4'>" +
                    "<thead class='col-md-12'>" +
                    "<tr>" +
                    "<th class=''> Id </th>" +
                    "<th class=''> Codigo </th>" +
                    "<th class=''> Descripción </th>" +
                    "<th class=''> Tasa IVA </th>" +
                    "<th class=''> Registro Patronal </th>" +
                    "<th class=''> Regional </th>" +
                    "<th class=''> Zona Economica </th>" +
                    "<th class=''> Sucursal </th>" +
                    "<th class=''> Estado </th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='tab" + pilltab + "' class=''></tbody>" + "</table>";
                for (var i = 0; i < data.length; i++) {

                    document.getElementById("tab" + pilltab).innerHTML += "<tr>" +
                        "<td>" + data[i]["IdLocalidad"] + "</td>" +
                        "<td>" + data[i]["Codigo_Localidad"] + "</td>" +
                        "<td>" + data[i]["Descripcion"] + "</td>" +
                        "<td>" + data[i]["TasaIva"] + "</td>" +
                        "<td>" + data[i]["NombreRegistroPatronal"] + "</td>" +
                        "<td>" + data[i]["Regional_id"] + "</td>" +
                        "<td>" + data[i]["ZonaEconomica_id"] + "</td>" +
                        "<td>" + data[i]["Sucursal_id"] + "</td>" +
                        "<td>" + data[i]["Estado_id"] + "</td>" +
                        //"<td class=''>" + data[i]["Fecha_Alta"].substr(0, 10) + "</td>" +
                        "</tr>";
                }
                $(".collapse").collapse("hide").addClass("bg-light p-4 rounded");
                $("#" + pilltab).collapse("toggle");
                setTimeout(function () {
                    $(".table" + Empresa_id + "").DataTable({
                        "language": {
                            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
                        }
                    });
                }, 100);
            }
        });



    }
    // MOSTRAR MODAL NUEVA LOCALIDAD 
    mostrarmodalnuevalocalidad = () => {
        $("#modalnuevalocalidad").modal("show");
    }
    // funcion al cerrar el modal de buscar sucursal
    $('#modalbuscarsucursal').on('hidden.bs.modal', function (e) {
        mostrarmodalnuevalocalidad();
    });
    // funcion al cerrar el modal de crear regional
    $('#modalnuevaregional').on('hidden.bs.modal', function (e) {
        mostrarmodalnuevalocalidad();
    });
    // funcion al abrir el modal de crear regional
    $('#modalnuevaregional').on('show.bs.modal', function (e) {
        $("#modalnuevalocalidad").modal("hide");
    });
    // funcion al abrir el modal de buscar sucursal
    $('#modalbuscarsucursal').on('show.bs.modal', function (e) {
        $("#modalnuevalocalidad").modal("hide");
    });

});