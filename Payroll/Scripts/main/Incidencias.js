﻿$(function () {
    var ren_incidencia = document.getElementById("inRenglon");
    var concepto_incidencia = document.getElementById("inConcepto_incidencia");
    var cantidad_incidencia = document.getElementById("inCantidad");
    var plazos_incidencia = document.getElementById("inPlazos");
    var leyenda_incidencia = document.getElementById("inLeyenda");
    var referencia_incidencia = document.getElementById("inReferencia");
    var fecha_incidencia = document.getElementById("inFechaA");
    //var infinicio = document.getElementById("infinicio");
    //var inffinal = document.getElementById("inffinal");
    //document.getElementById("lbloptions").style.visibility = "none";

    // SE LANZA LA INSTRUCCION DE MOSTRAR EL MODAL DE BUSQUEDA DE EMPLEADOS
    $("#modalLiveSearchEmpleado").modal("show");
    // Al hacer click en el boton cambiar usuario muestra el modal de busqueda 
    $("#btn-cambiar-empleado").on("click", function () {
        $("#modalLiveSearchEmpleado").modal("toggle");
    });
    //Busqueda de empleado
    $("#inputSearchEmpleados").on("keyup", function () {
        $("#inputSearchEmpleados").empty();
        var txt = $("#inputSearchEmpleados").val();
        if ($("#inputSearchEmpleados").val() != "") {
            var txtSearch = { "txtSearch": txt };
            $.ajax({
                url: "../Empleados/SearchEmpleados",
                type: "POST",
                cache: false,
                data: JSON.stringify(txtSearch),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    console.log(data);
                    $("#resultSearchEmpleados").empty();
                    if (data[0]["iFlag"] == 0) {
                        for (var i = 0; i < data.length; i++) {
                            $("#resultSearchEmpleados").append("<div class='list-group-item list-group-item-action btnListEmpleados font-labels  font-weight-bold' onclick='MostrarDatosEmpleado(" + data[i]["IdEmpleado"] + ")'> <i class='far fa-user-circle text-primary'></i> " + data[i]["Nombre_Empleado"] + " " + data[i]["Apellido_Paterno_Empleado"] + ' ' + data[i]["Apellido_Materno_Empleado"] + "   -   <small class='text-muted'><i class='fas fa-briefcase text-warning'></i> " + data[i]["DescripcionDepartamento"] + "</small> - <small class='text-muted'>" + data[i]["DescripcionPuesto"] + "</small></div>");
                        }
                    }
                    else {
                        $("#resultSearchEmpleados").append("<button type='button' class='list-group-item list-group-item-action btnListEmpleados font-labels'  >" + data[0]["Nombre_Empleado"] + "<br><small class='text-muted'>" + data[0]["DescripcionPuesto"] + "</small> </button>");
                    }
                }
            });
        } else {
            $("#resultSearchEmpleados").empty();
        }
    });
    //VALIDACION DE CAMPOS NUMERICOS
    $('.input-number').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
    ////CARGA CONCEPTO 
    ////////$.ajax({
    ////////    url: "../Incidencias/LoadTipoIncidencia",
    ////////    type: "POST",
    ////////    dataType: "json",
    ////////    contentType: "application/json; charset=utf-8",
    ////////    success: (data) => {
    ////////        document.getElementById("inConcepto_incidencia").innerHTML = "<option class='' value=''> Selecciona </option>"
    ////////        for (var i = 0; i < data.length; i++) {
    ////////            document.getElementById("inConcepto_incidencia").innerHTML += "<option class='' value='" + data[i]["Ren_incid_id"] + "'> " + data[i]["Ren_incid_id"] + " : " + data[i]["Descripcion"] + "</option>";
    ////////        }
    ////////    }
    ////////});
    //CAMBIOS EN EL SELECT Y EL RENGLON
    //////////////////$("#inRenglon").on("change", function () {
    checkrenglon = () => {
        //ren_incidencia.value = concepto_incidencia.value;
        if (ren_incidencia.value == '71') {
            //console.log("si");
            $("#lblCantidad").html("Dias");
            //$("#inflbl").html("<small class='px-2'> *Si selecciona por días se insertaran en el periodo actual, si selecciona por fechas solo aplicara dentro del rango agregado.<small>").attr("class", "alert-warning text-center col-md-12 mx-3 mb-2");
            $("#inCantidad").attr("placeholder", "#");
            document.getElementById("inPlazos").disabled = true;
            //$("#collapsefechas").collapse("show");
            //$("#lbloptions").html(""
            //    + "<div class='btn-group btn-group-toggle' data-toggle='buttons' >"
            //    + "<label class='btn btn-sm btn-outline-info font-labels active btn-option-1' onclick='checkPorDias();'>"
            //    + "<input type='radio' name='options' id='opt1' checked> Por Días"
            //    + "</label>"
            //    + "<label class='btn btn-sm btn-outline-info font-labels  btn-option-2' onclick='checkPorFecha();'>"
            //    + "<input type='radio' name='options' id='opt2'> Por Fechas"
            //    + "</label>"
            //    + "</div>"
            //    //+ "<div class='btn-group' role='group' aria-label='Basic example'>"
            //    //+ "<button type='button' id='opt1' class='btn btn-outline-info btn-sm'><small> <i class='fas fa-calendar-day'></i> Por Dias </small> </button>"
            //    //+ "<button type='button' id='opt2' class='btn btn-outline-info btn-sm'><small> <i class='fas fa-calendar-alt'></i> Por Fechas </small> </button>"
            //    //+ "</div >"
            //);

            //setTimeout(() => {
            //    document.getElementById("opt1").click();
            //}, 500);

            //$("#opt1").click();
        } else {
            //console.log("no");
            $("#lblCantidad").html("Cantidad");
            //$("#inflbl").html("").attr("class", "");
            $("#inCantidad").attr("placeholder", "$ 0000.00");
            //$("#collapsefechas").collapse("hide");
            //$("#lbloptions").html("");
            document.getElementById("inPlazos").disabled = false;

        }
    }
    ///////////////});
    //checkPorDias = () => {
    //    document.getElementById("inCantidad").disabled = false;
    //    document.getElementById("infinicio").disabled = true;
    //    document.getElementById("inffinal").disabled = true;
    //}
    //checkPorFecha = () => {
    //    document.getElementById("inCantidad").disabled = true;
    //    document.getElementById("infinicio").disabled = false;
    //    document.getElementById("inffinal").disabled = false;
    //}
    // ABRE EL COLLAPSE PARA INSERTAR NUEVAS INCIDENCIAS 
    mostrarFormNewIncidencias = () => {
        $("#incidenciasCollapse").collapse("show");
        $("#txtsearchtipoincidencia").click();
        $("#txtsearchtipoincidencia").focus();
    }

    //GUARDAR INCIDENCIA
    $("#btnSaveIncidencias").on("click", function () {

        var tipo = 0;
        var fi = "";
        var ff = "";
        var cant = 0;

        var form = document.getElementById("frmIncidencias");
        if (form.checkValidity() == false) {
            setTimeout(() => {
                form.classList.add("was-validated");
            }, 5000);
        } else {
            //if (concepto_incidencia.value == "71") {
            //    if ($("#opt1").prop('checked')) {
            //        console.log("Por dias");
            //        fi = "0";
            //        ff = "0";
            //        tipo = 0;
            //        cant = document.getElementById("inCantidad").value;

            //    } else if ($("#opt2").prop('checked')) {
            //        console.log("Por dias");
            //        fi = document.getElementById("infinicio").value;
            //        ff = document.getElementById("inffinal").value;
            //        tipo = 1;
            //        cant = 0;

            //    }
            //} else {
            cant = document.getElementById("inCantidad").value;
            //}

            var Vform = $("#frmIncidencias").serialize();
            $.ajax({
                url: "../Incidencias/SaveRegistroIncidencia",
                type: "POST",
                data: JSON.stringify({
                    inRenglon: concepto_incidencia.value,
                    inCantidad: cant,
                    inPlazos: plazos_incidencia.value,
                    inLeyenda: leyenda_incidencia.value,
                    inReferencia: referencia_incidencia.value,
                    inFechaA: fecha_incidencia.value,
                    //infinicio: fi,
                    //inffinal: ff,
                    //intipo: tipo
                }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    if (data[0] == '0') {
                        Swal.fire({
                            icon: 'danger',
                            title: 'Error!',
                            text: data[1],
                            timer: 1000
                        });
                    } else if (data[0] == '1') {
                        $("#incidenciasCollapse").collapse("hide");
                        concepto_incidencia.value = '';
                        cantidad_incidencia.value = '';
                        plazos_incidencia.value = '';
                        leyenda_incidencia.value = '';
                        referencia_incidencia.value = '';
                        fecha_incidencia.value = '';
                        createTab();
                        Swal.fire({
                            icon: 'success',
                            title: 'Completado!',
                            text: data[1],
                            timer: 1000
                        });
                    }
                }
            });
        }
    });

    //Funciones
    MostrarDatosEmpleado = (idE) => {
        var txtIdEmpleado = { "IdEmpleado": idE, Empresa_id: 0 };
        $.ajax({
            url: "../Empleados/SearchDataEmpleado",
            type: "POST",
            data: JSON.stringify({ "Empleado_id": idE, Empresa_id: "0" }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                $("#tabIncidenciasBody").html("");
                createTab();
                console.log(data);
                var iconb = "";
                var colorb = "";
                if (data["54"] > 163) {
                    colorb = "badge-danger";
                    iconb = "fa-times-circle";
                } else {
                    colorb = "badge-success";
                    iconb = "fa-check-circle";
                }

                document.getElementById("EmpDes").innerHTML = "<i class='fas fa-hashtag text-primary'></i> " + data[0] + "&nbsp;&nbsp;&nbsp;&nbsp;<i class='fas fa-user-alt text-primary'></i> " + data[1] + " " + data[2] + ' ' + data[3] + "&nbsp;&nbsp;-&nbsp;&nbsp;<small class='text-muted'>" + data[51] + "</small>&nbsp;&nbsp;<div class='badge "+ colorb +"'><i class='fas "+ iconb +"'></i>&nbsp;" + data["54"] + "&nbsp;-&nbsp;" + data["33"] + "</div>";
                $("#modalLiveSearchEmpleado").modal("hide");
                document.getElementById("resultSearchEmpleados").innerHTML = "";
                document.getElementById("inputSearchEmpleados").value = "";
                //console.log(data);

            }
        });
    }

    createTab = () => {
        $.ajax({
            method: "POST",
            url: "../Incidencias/LoadIncidenciasEmpleado",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (data) => {
                console.log(data);
                document.getElementById("tabIncidenciasBody").innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    var period = $("#lblPeriodoId").html();
                    var incidenciaProg_id;
                    var cantidad;
                    console.log(data[i]["IncidenciaP_id"]);
                    if (data[i]["IncidenciaP_id"] == "" || data[i]["IncidenciaP_id"] == 0) {
                        incidenciaProg_id = 0;
                    } else {
                        incidenciaProg_id = data[i]["IncidenciaP_id"];
                    }

                    if (data[i]["VW_TipoIncidencia_id"] == 71 || data[i]["VW_TipoIncidencia_id"] == "71") {
                        cantidad = parseFloat(data[i]["Numero_dias"]);
                    } else {
                        cantidad = parseFloat(data[i]["Cantidad"]);
                    }

                    document.getElementById("tabIncidenciasBody").innerHTML += "" +
                        "<tr>" +
                        "<td scope='row'>" + data[i]["Nombre_Renglon"] + "</td>" +
                        "<td class='text-center'>" + data[i]["VW_TipoIncidencia_id"] + "</td>" +
                        "<td class='text-center'>" + cantidad + "</td>" +
                        "<td class='text-center'>" + data[i]["Plazos"] + "</td>" +
                        "<td class='text-center'>" + data[i]["Descripcion"] + "</td>" +
                        "<td class='text-center'>" + data[i]["Fecha_Aplicacion"] + "</td>" +
                        "<td class='text-center'>" + data[i]["NPeriodo"] + "</td>" +
                        "<td class='text-center'>" +
                        "<div class='badge badge-success btn mx-1' onclick='editarIncidencia(" + data[i]["Incidencia_id"] + ");' title='Editar'><i class='fas fa-pencil-alt'></i></div>" +
                        //"<div class='badge badge-success btn' onclick='adelantarPagoIncidencia(" + data[i]["Incidencia_id"] + "," + data[i]["IncidenciaP_id"] + ");' title='Adelantar Pago'><i class='fas fa-donate'></i></div>" +
                        "<div class='badge badge-danger btn mx-1' onclick='deleteIncidencia(" + data[i]["Incidencia_id"] + "," + incidenciaProg_id + ");' title='Eliminar'><i class='fas fa-minus'></i></div>" +
                        //"<div class='badge badge-success btn' title='Adelantar Pago'><i class='fas fa-donate'></i></div>" +
                        //"<div class='badge badge-danger btn' title='Eliminar'><i class='fas fa-minus'></i></div>" +
                        "</td>" +
                        "</tr>";
                }
            }
        });

    }

    deleteIncidencia = (Incidencia_id, IncidenciaP_id) => {
        Swal.fire({
            title: 'Quieres eliminar la incidencia?',
            text: "No podras recuperarla!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#A52A0F',
            cancelButtonColor: 'secondary',
            confirmButtonText: 'Eliminar!',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    method: "POST",
                    data: JSON.stringify({ Incidencia_id: Incidencia_id, IncidenciaP_id: IncidenciaP_id }),
                    url: "../Incidencias/DeleteIncidencia",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: (data) => {
                        document.getElementById("tabIncidenciasBody").innerHTML = "";
                        createTab();
                        if (data[0] == '0') {
                            Swal.fire({
                                title: 'Error!',
                                text: data[1],
                                icon: 'warning',
                                timer: 1000
                            });
                        } else {

                            Swal.fire({
                                icon: 'success',
                                title: 'Borrado!',
                                text: data[1],
                                timer: 1000
                            });

                        }
                    }
                });
            }
        });
    }

    editarIncidencia = (incidencia_id) => {

        $.ajax({
            method: "POST",
            data: JSON.stringify({ Incidencia_id: incidencia_id }),
            url: "../Incidencias/LoadIncidencia",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (data) => {
                //console.log(data);
                if (data[0]["Renglon"] == "71" || data[0]["Renglon"] == 71) {
                    $("#edConcepto").html("<option value='" + data[0]["IdTRegistro_Incidencia"] + "'>" + data[0]["Concepto"] + "</option>");
                    document.getElementById("edRenglon").disabled = false;
                    $("#edRenglon").val(data[0]["Renglon"]);
                    document.getElementById("edRenglon").disabled = true;
                    $("#edCantidad").val(data[0]["Numero_dias"].substring(0, data[0]["Numero_dias"].length - 2));
                    document.getElementById("edSaldo").disabled = false;
                    $("#edSaldo").val(data[0]["Saldo"].substring(0, data[0]["Saldo"].length - 2));
                    document.getElementById("edSaldo").disabled = true;
                    document.getElementById("edPlazos").disabled = false;
                    $("#edPlazos").val(data[0]["Plazos"]);
                    document.getElementById("edPlazos").disabled = true;
                    document.getElementById("edPagosRestantes").disabled = false;
                    $("#edPagosRestantes").val(data[0]["Pagos_restantes"]);
                    document.getElementById("edPagosRestantes").disabled = true;
                    $("#edLeyenda").val(data[0]["Descripcion"]);
                    $("#edReferencia").val(data[0]["Referencia"]);
                    $("#edFechaAplicacion").val(data[0]["Fecha_Aplicacion"]);
                    $("#editIncidencia").modal("show");
                } else {
                    $("#edConcepto").html("<option value='" + data[0]["IdTRegistro_Incidencia"] + "'>" + data[0]["Concepto"] + "</option>");
                    document.getElementById("edRenglon").disabled = false;
                    $("#edRenglon").val(data[0]["Renglon"]);
                    document.getElementById("edRenglon").disabled = true;
                    $("#edCantidad").val(data[0]["Cantidad"].substring(0, data[0]["Cantidad"].length - 2));
                    $("#actualCantidad").val(data[0]["Cantidad"].substring(0, data[0]["Cantidad"].length - 2));
                    document.getElementById("edSaldo").disabled = false;
                    $("#edSaldo").val(data[0]["Saldo"].substring(0, data[0]["Saldo"].length - 2));
                    $("#actualSaldo").val(data[0]["Saldo"].substring(0, data[0]["Saldo"].length - 2));
                    document.getElementById("edPlazos").disabled = false;
                    $("#edPlazos").val(data[0]["Plazos"]);
                    $("#actualPlazos").val(data[0]["Plazos"]);
                    document.getElementById("edPagosRestantes").disabled = false;
                    $("#edPagosRestantes").val(data[0]["Pagos_restantes"]);
                    $("#actualPagosrestantes").val(data[0]["Pagos_restantes"]);
                    $("#edLeyenda").val(data[0]["Descripcion"]);
                    $("#edReferencia").val(data[0]["Referencia"]);
                    $("#edFechaAplicacion").val(data[0]["Fecha_Aplicacion"]);
                    $("#editIncidencia").modal("show");
                }
            }
        });
    }

    validaCantidad = () => {
        var cant = parseFloat($("#edCantidad").val());
        var sal = parseFloat($("#edSaldo").val());
        var actCantidad = parseFloat($("#actualCantidad").val());
        var actSaldo = parseFloat($("#actualSaldo").val());

        if ($("#edRenglon").val() == "71" || $("#edRenglon").val() == 71) {

        } else {

            if (cant === actCantidad) {
                $("#validactc").html("");
                $("#edSaldo").val(actSaldo);
            } else {
                $("#edSaldo").val(cant);
                sal = cant;
                $("#validactc").html("<div class='alert alert-primary alert-dismissable'>" +
                    "<button type='button' class='close' data-dismiss='alert'>&times;</button>" +
                    "<strong>¡Aviso!</strong> Si la cantidad es modificada el saldo se registrará con el valor de la nueva cantidad." +
                    "</div>");
            }

            if (cant < sal) {
                //console.log($("#edCantidad").val());
                //console.log($("#edSaldo").val());
                $("#edCantidad").removeClass("is-valid");
                $("#edSaldo").removeClass("is-valid");
                $("#edCantidad").addClass("is-invalid");
                $("#edSaldo").addClass("is-invalid");

                $("#validc").html("<div class='alert alert-warning alert-dismissable'>" +
                    "<button type='button' class='close' data-dismiss='alert'>&times;</button>" +
                    "<strong>¡Aviso!</strong> La Cantidad no puede ser menor al Saldo." +
                    "</div>");
            } else {
                $("#validc").html("");
                $("#edCantidad").removeClass("is-invalid");
                $("#edSaldo").removeClass("is-invalid");
                $("#edCantidad").addClass("is-valid");
                $("#edSaldo").addClass("is-valid");
            }
        }
    }

    validaPlazos = () => {
        var plaz = parseInt($("#edPlazos").val());
        var pagr = parseInt($("#edPagosRestantes").val());

        var actPlazos = parseInt($("#actualPlazos").val());
        var actPagosrestantes = parseInt($("#actualPagosrestantes").val());
        if ($("#edRenglon").val() == "71" || $("#edRenglon").val() == 71) {

        } else {

            if (plaz === actPlazos) {
                $("#validactp").html("");
                $("#edPagosRestantes").val(actPagosrestantes);
            } else {
                $("#edPagosRestantes").val(plaz);
                pagr = plaz;
                $("#validactp").html("<div class='alert alert-primary alert-dismissable'>" +
                    "<button type='button' class='close' data-dismiss='alert'>&times;</button>" +
                    "<strong>¡Aviso!</strong> Si los Plazos son modificacdos los pagos restantes serán los mismos a los plazos." +
                    "</div>");
            }

            if (plaz < pagr) {
                console.log($("#edPlazos").val());
                console.log($("#edPagosRestantes").val());
                $("#edPlazos").removeClass("is-valid");
                $("#edPagosRestantes").removeClass("is-valid");
                $("#edPlazos").addClass("is-invalid");
                $("#edPagosRestantes").addClass("is-invalid");

                $("#validp").html("<div class='alert alert-warning alert-dismissable'>" +
                    "<button type='button' class='close' data-dismiss='alert'>&times;</button>" +
                    "<strong>¡Aviso!</strong> Los Plazos no pueden ser menores a los Pagos Restantes." +
                    "</div>");
            } else {
                $("#validp").html("");
                console.log($("#edPlazos").val());
                console.log($("#edPagosRestantes").val());
                $("#edPlazos").removeClass("is-invalid");
                $("#edPagosRestantes").removeClass("is-invalid");
                $("#edPlazos").addClass("is-valid");
                $("#edPagosRestantes").addClass("is-valid");
            }
        }
    }

    // FUNCION PARA LA ACTUALIZACION DE LAS INCIDENCIAS
    updateIncidencia = () => {
        var dias;
        var cantidad;

        if ($("#edRenglon").val() == "71") {
            cantidad = 0;
            dias = $("#edCantidad").val();
        } else {
            cantidad = $("#edCantidad").val();
            dias = 0;
        }

        var form = document.getElementById("frmEditIncidencia");
        if (form.checkValidity() == false) {
            setTimeout(() => {
                form.classList.add("was-validated");
            }, 5000);
        } else {

            $.ajax({
                method: "POST",
                data: JSON.stringify({
                    Incidencia_id: document.getElementById("edConcepto").value,
                    Renglon_id: $("#edRenglon").val(),
                    Cantidad: cantidad,
                    Saldo: $("#edSaldo").val(),
                    Plazos: $("#edPlazos").val(),
                    Pagos_restantes: $("#edPagosRestantes").val(),
                    Leyenda: $("#edLeyenda").val(),
                    Referencia: $("#edReferencia").val(),
                    Fecha_Aplicacion: $("#edFechaAplicacion").val(),
                    Numero_dias: dias
                }),
                url: "../Incidencias/UpdateIncidencia",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: (data) => {
                    if (data[0] == '0') {
                        Swal.fire({
                            title: 'Error!',
                            text: data[1],
                            icon: 'warning',
                            timer: 1000
                        });
                        //crearToast("erorr", "Error", data[1]);
                    } else {
                        document.getElementById("tabIncidenciasBody").innerHTML = "";
                        createTab();
                        $("#editIncidencia").modal("hide");
                        Swal.fire({
                            icon: 'success',
                            title: 'Incidencia Actualizada',
                            text: data[1],
                            timer: 1000
                        });
                        //crearToast("success", "Incidencia actualizada", data[1]);
                    }
                }
            });
        }

    }
    // VALIDA QUE AL CERRRAR EL MODAL SE LIMPIE CUALQUIER VALIDACION HECHA EN EL FORMULARIO
    $('#editIncidencia').on('hidden.bs.modal', function (e) {
        $("#validp").html("");
        $("#validc").html("");
        $("#edPlazos").removeClass("is-invalid");
        $("#edPagosRestantes").removeClass("is-invalid");
        $("#edPlazos").removeClass("is-valid");
        $("#edPagosRestantes").removeClass("is-valid");
    });


    $("#txtsearchtipoincidencia").keyup(function () {
        var txt = $("#txtsearchtipoincidencia").val();

        document.getElementById("resulttipoincidencia").innerHTML = "";
        $("#inRenglon").val("");
        $.ajax({
            url: "../Incidencias/LoadTipoIncidencia",
            type: "POST",
            cache: false,
            data: JSON.stringify({
                txtSearch: txt
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                //$("#resulttipoincidencia").html("");
                document.getElementById("resulttipoincidencia").innerHTML = "";
                if (data.length < 0 || data == "") {
                    //console.log(data);

                } else {
                    //console.log(data);
                    for (var i = 0; i < data.length; i++) {
                        document.getElementById("resulttipoincidencia").innerHTML += "<div class='list-group-item list-group-item-light list-group-item-action py-0' onclick='selectIncidencia(" + data[i]["Ren_incid_id"] + ",\"" + data[i]["Descripcion"] + "\");'> " + data[i]["Ren_incid_id"] + " : " + data[i]["Descripcion"] + "</div>";
                    }
                }
            }
        });
    });

    selectIncidencia = (Renglon_id, Descripcion) => {
        $("#txtsearchtipoincidencia").val(Descripcion);
        document.getElementById("resulttipoincidencia").innerHTML = "";
        $("#inRenglon").val(Renglon_id);
        checkrenglon();
    }
});