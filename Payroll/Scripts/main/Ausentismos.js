$(function () {
    var motivo = document.getElementById("inMotivoAusentismo");
    var recup = document.getElementById("inRecuperacionAusentismo");
    var fechaa = document.getElementById("inFechaAusentismo");
    var dias = document.getElementById("inDiasAusentismo");
    var causa = document.getElementById("inCausaAusentismo");
    var certif = document.getElementById("inCertificadoAusentismo");
    var coment = document.getElementById("inComentariosAusentismo");
    var btnc = document.getElementById("btnClear");
    var btnu = document.getElementById("btnUpdate");
    var btnSave = document.getElementById("btnRegistroAusentismo");

    certif.disabled = true;
    coment.disabled = true;
    //btnc.classList.add("invisible");
    //btnu.classList.add("invisible");
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
                    //console.log(data);
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

    $("#modalLiveSearchEmpleado").modal("toggle");
    var motivoA = document.getElementById("inMotivoAusentismo");

    //Funcion para guardar los Ausentismos
    $("#btnRegistroAusentismo").on("click", function (evt) {
        var data = $("#frmAusentismos").serialize();
        var cer_imss, com_imss, cau_falta;
        var form = document.getElementById("frmAusentismos");
        var ndias = 0;
        var fechafin = "0";
        var fechaini;
        var tipo = 0;

        if (form.checkValidity() === false) {
            evt.preventDefault();
            form.classList.add("was-validated");
            //console.log(data);
        } else {
            evt.preventDefault();
            //
            if ($("#option1").prop('checked')) {
                console.log('Por dias');
                ndias = document.getElementById("inDiasAusentismo").value;
                fechafin = "0";
                fechaini = document.getElementById("inFechaAusentismo").value;
                tipo = 0;
            }
            if ($("#option2").prop('checked')) {
                console.log('Por fechas');
                ndias = 0;
                fechafin = document.getElementById("inFechafAusentismo").value;
                fechaini = document.getElementById("inFechaiAusentismo").value;
                tipo = 1;
            }
            //console.log(data);
            if (certif.value.length != 0) { cer_imss = certif.value } else { cer_imss = "0" }
            if (coment.value.length != 0) { com_imss = coment.value } else { com_imss = "0" }
            if (causa.value.length != 0) { cau_falta = causa.value } else { cau_falta = "0" }

            $.ajax({
                url: "../Incidencias/SaveAusentismo",
                type: "POST",
                data: JSON.stringify({
                    Tipo_Ausentismo_id: motivo.value,
                    Recupera_Ausentismo: recup.value,
                    Fecha_Ausentismo: fechaini,
                    Dias_Ausentismo: ndias,
                    Certificado_imss: cer_imss,
                    Comentarios_imss: com_imss,
                    Causa_FaltaInjustificada: cau_falta,
                    FechaFin: fechafin,
                    Tipo: tipo
                }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: (data1) => {

                    fechaa.value = "";
                    dias.value = "";
                    cau_falta.value = "";

                    if (data1[0] == "0") {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Error!',
                            text: data1[1],
                            timer: 1000
                        });
                    } else if (data1[0] == "1") {
                        Swal.fire({
                            icon: 'success',
                            title: 'Correcto!',
                            text: data1[1],
                            timer: 1000
                        });
                    }
                    form.reset();
                    tabAusentismo();

                }
            });

        }

    });
    //Carga los tipos de ausentismos 
    $.ajax({
        url: "../Incidencias/LoadAusentismos",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: (data) => {
            motivoA.innerHTML = "<option value=''> Selecciona </option>";
            for (var i = 0; i < data.length; i++) {
                motivoA.innerHTML += `<option value='${data[i].sDescripcionAusentismo}'> ${data[i].sNombreAusentismo} </option>`;
            }
        }
    });
    //Evento de cambio de motivo accidente
    $("#inMotivoAusentismo").on("change", function () {
        //console.log("cambio de motivo");
        var causa = document.getElementById("inCausaAusentismo");
        var certif = document.getElementById("inCertificadoAusentismo");
        var coment = document.getElementById("inComentariosAusentismo");
        var motivo = $("#inMotivoAusentismo").val();
        //console.log(motivo);
        switch (motivo) {
            case "6":
                coment.disabled = false;
                certif.disabled = false;
                causa.disabled = true;
                break;
            case "7":
                coment.disabled = false;
                certif.disabled = false;
                causa.disabled = true;
                break;
            case "9":
                coment.disabled = true;
                certif.disabled = true;
                break;
            case "1":
                coment.disabled = false;
                certif.disabled = false;
                causa.disabled = false;
                break;
            case "20":
                coment.disabled = false;
                certif.disabled = false;
                causa.disabled = false;
                break;
            default:
                coment.disabled = true;
                certif.disabled = true;
                break;
        }
    });

    $("#btnUpdate").on("click", function () {
        //console.log("btn-editar");
        var data = $("#frmAusentismos").serialize();
        var cer_imss, com_imss, cau_falta;
        var ndias = 0;
        var fechafin = "0";
        var Fechaini;
        var form = document.getElementById("frmAusentismos");
        if (form.checkValidity() === false) {

            form.classList.add("was-validated");
            //console.log(data);
            //console.log("no entro");
        } else {

            if ($("#option1").prop('checked')) {
                console.log('Soy valido check 1');
                ndias = dias.value;
                fechafin = "0";
                Fechaini = fechaa.value;
                tipo = 0;
            }
            if ($("#option2").prop('checked')) {
                console.log('Soy invalido check 2');
                ndias = 0;
                fechafin = document.getElementById("inFechafAusentismo").value;
                Fechaini = document.getElementById("inFechaiAusentismo").value;
                tipo = 1;
            }

            //console.log(data);
            if (certif.value.length != 0) { cer_imss = certif.value } else { cer_imss = "0" }
            if (coment.value.length != 0) { com_imss = coment.value } else { com_imss = "0" }
            if (causa.value.length != 0) { cau_falta = causa.value } else { cau_falta = "0" }
            $.ajax({
                url: "../Incidencias/UpdateAusentismo",
                type: "POST",
                data: JSON.stringify({
                    Id: $("#btnUpdate").attr("btnupdate"),
                    Tipo_Ausentismo_id: motivo.value,
                    Recupera_Ausentismo: recup.value,
                    Fecha_Ausentismo: Fechaini,
                    Dias_Ausentismo: ndias,
                    Certificado_imss: cer_imss,
                    Comentarios_imss: com_imss,
                    Causa_FaltaInjustificada: cau_falta,
                    FechaFin: fechafin,
                    Tipo: tipo,
                    IncidenciaProgramada_id: $("#inIncidenciaProgramada_id").val()
                }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: (data1) => {
                    
                    if (data1[0] == "0") {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Error!',
                            text: data1[1],
                            timer: 1000
                        });
                    } else if (data1[0] == "1") {
                        document.getElementById('frmAusentismos').reset();
                        Swal.fire({
                            icon: 'success',
                            title: 'Correcto!',
                            text: data1[1],
                            timer: 1000
                        });
                        tabAusentismo();
                        tabIncapacidades();
                        //$("#inRecuperacionAusentismo option[value='']").attr("selected", true);
                        //$("#inMotivoAusentismo option[value='']").attr("selected", true);
                        $("#btnRegistroAusentismo").removeClass("invisible");
                        $("#btnClear").addClass("invisible");
                        $("#btnUpdate").addClass("invisible");
                    }

                }
            });

        }
    });

    $("#btn-cambiar-empleado").on("click", function () {
        $("#modalLiveSearchEmpleado").modal("toggle");
    });
    //
    MostrarDatosEmpleado = (idE) => {
        var txtIdEmpleado = { "IdEmpleado": idE };
        $.ajax({
            url: "../Empleados/SearchEmpleado",
            type: "POST",
            data: JSON.stringify(txtIdEmpleado),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                $("#tabodyIncapacidades").html("");

                var iconb = "";
                var colorb = "";
                if (data[0]["TipoEmpleado"] > 163) {
                    colorb = "badge-danger";
                    iconb = "fa-times-circle";
                } else {
                    colorb = "badge-success";
                    iconb = "fa-check-circle";
                }

                document.getElementById("EmpDes").innerHTML = "<i class='fas fa-hashtag text-primary'></i>&nbsp;&nbsp;" + data[0]["IdEmpleado"] + "&nbsp;&nbsp;<i class='fas fa-user-alt text-primary'></i>&nbsp;&nbsp;" + data[0]["Nombre_Empleado"] + "&nbsp;" + data[0]["Apellido_Paterno_Empleado"] + '&nbsp;' + data[0]["Apellido_Materno_Empleado"] + "   -   <small class='text-muted'> " + data[0]["DescripcionPuesto"] + "</small>&nbsp;&nbsp;<div class='badge " + colorb + "'><i class='fas " + iconb + "'></i>&nbsp;" + data[0]["TipoEmpleado"] + "&nbsp;-&nbsp;" + data[0]["DescTipoEmpleado"] + "</div>";
                $("#modalLiveSearchEmpleado").modal("hide");
                document.getElementById("resultSearchEmpleados").innerHTML = "";
                document.getElementById("inputSearchEmpleados").value = "";
                tabAusentismo();
                tabIncapacidades();
                //document.getElementById("nameuser").innerHTML = "<div class='text-uppercase'>" + data[0]["Nombre_Empleado"] + " " + data[0]["Apellido_Paterno_Empleado"] + ' ' + data[0]["Apellido_Materno_Empleado"] + "<small class='text-muted'>" + data[0]["DescripcionPuesto"] + "</small></div>";
            }
        });
        
    }
    //Funcion para validar solo numeros 
    $('.input-number').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
    //
    tabAusentismo = () => {
        $.ajax({
            method: "POST",
            url: "../Incidencias/LoadAusentismosTab",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (data) => {
                //console.log(data);
                document.getElementById("tabody").innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    document.getElementById("tabody").innerHTML += "" +
                        "<tr>" +
                        "<td>" + data[i]["Nombre_Ausentismo"] + "</td>" +
                        "<td>" + data[i]["Fecha_Ausentismo"].substring(0, 10) + "</td>" +
                        "<td>" + data[i]["Dias_Ausentismo"] + "</td>" +
                        "<td>" + data[i]["RecuperaAusentismo"] + "</td>" +
                        "<td>" + data[i]["Causa_FaltaInjustificada"] + "</td>" +
                        "<td>" + data[i]["Certificado_imss"] + "</td>" +
                        "<td>" + data[i]["Comentarios_imss"] + "</td>" +
                        "<td>" +
                        "<div class='btn-group' role='group' aria-label='Basic example'>" + "<div class='btn btn-subm btn-sm btn-editar-ausentismo' onclick='editarAusentismo( " + data[i]["IdAusentismo"] + " );'><i class='far fa-edit'></i></div><div class='btn btn-danger btn-sm btn-eliminar-ausentismo' onclick='eliminarAusentismo( " + data[i]["IdAusentismo"] + " );'><i class='fas fa-minus'></i></div></div>" +
                        "</td>" +
                        "</tr>";
                }
            }
        });
    }
    // Tabla de Incapacidades
    tabIncapacidades = () => {
        $.ajax({
            method: "POST",
            url: "../Incidencias/LoadIncapacidadesTab",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (data) => {

                document.getElementById("tabodyIncapacidades").innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    document.getElementById("tabodyIncapacidades").innerHTML += "" +
                        "<tr>" +
                        "<td>" + data[i]["Nombre_Ausentismo"] + "</td>" +
                        "<td>" + data[i]["Fecha_Ausentismo"].substring(0, 10) + "</td>" +
                        "<td>" + data[i]["Dias_Ausentismo"] + "</td>" +
                        "<td>" + data[i]["FechaFin"] + "</td>" +
                        "<td>" + data[i]["Causa_FaltaInjustificada"] + "</td>" +
                        "<td>" + data[i]["Certificado_imss"] + "</td>" +
                        "<td>" + data[i]["Comentarios_imss"] + "</td>" +
                        "</tr>";

                }

            }
        });
    }

    //
    eliminarAusentismo = (Ausentismo_id) => {
        //console.log("boton eliminar: " + Ausentismo_id);
        $.ajax({
            url: "../Incidencias/DeleteAusentismo",
            type: "POST",
            data: JSON.stringify({ IdAusentismo: Ausentismo_id }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                //console.log(data);
                if (data[0] == '0') {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Error!',
                        text: data[1]
                    });
                } else if (data[0] == '1') {
                    Swal.fire({
                        icon: 'success',
                        title: 'Correcto!',
                        text: data[1]
                    });
                }
                document.getElementById("tabodyIncapacidades").innerHTML = "";
                document.getElementById("tabody").innerHTML = "";
                tabAusentismo();
                tabIncapacidades();
            }
        });
    }
    //Editar ausentismo
    editarAusentismo = (Ausentismo_id) => {
        //var motivo = document.getElementById("inMotivoAusentismo");
        //var recup = document.getElementById("inRecuperacionAusentismo");
        var fechaa = document.getElementById("inFechaAusentismo");
        var dias = document.getElementById("inDiasAusentismo");
        var causa = document.getElementById("inCausaAusentismo");
        var certif = document.getElementById("inCertificadoAusentismo");
        var coment = document.getElementById("inComentariosAusentismo");

        //console.log("boton editar: " + Ausentismo_id);
        $.ajax({
            url: "../Incidencias/LoadAusentismo",
            type: "POST",
            data: JSON.stringify({ IdAusentismo: Ausentismo_id }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                if (data['Tipo'] == 'True') {
                    //checkPorDias();
                    $(".btn-option-1").click();
                    setTimeout(() => {
                        fechaa.value = data[0].Fecha_Ausentismo.substring(6, 10) + "-" + data[0].Fecha_Ausentismo.substring(3, 5) + "-" + data[0].Fecha_Ausentismo.substring(0, 2);
                        dias.value = data[0].Dias_Ausentismo;
                    }, 600);

                } else {
                    //checkPorFecha();
                    $(".btn-option-2").click();
                    setTimeout(() => {
                        document.getElementById('inFechaiAusentismo').value = data[0].Fecha_Ausentismo.substring(6, 10) + "-" + data[0].Fecha_Ausentismo.substring(3, 5) + "-" + data[0].Fecha_Ausentismo.substring(0, 2);
                        document.getElementById('inFechafAusentismo').value = data[0].FechaFin.substring(6, 10) + "-" + data[0].FechaFin.substring(3, 5) + "-" + data[0].FechaFin.substring(0, 2);
                    }, 600);

                }
                //console.log(data);
                //dias.value = data[0].Dias_Ausentismo;
                causa.value = data[0].Causa_FaltaInjustificada;
                $('#inIncidenciaProgramada_id').val(data[0].IncidenciaProgramada_id);
                //fechaa.value = data[0].Fecha_Ausentismo.substring(6, 10) + "-" + data[0].Fecha_Ausentismo.substring(3, 5) + "-" + data[0].Fecha_Ausentismo.substring(0, 2);

                $("#inRecuperacionAusentismo option[value='" + data[0].RecuperaAusentismo + "']").attr("selected", true);

                if (data[0].Certificado_imss.length != 0) {
                    certif.disabled = false;
                    coment.disabled = false;
                    certif.value = data[0].Certificado_imss;
                    coment.value = data[0].Comentarios_imss;
                }

                $("#inMotivoAusentismo option[value='" + data[0].Tipo_Ausentismo_id + "']").attr("selected", true);


                $("#btnUpdate").attr("btnUpdate", Ausentismo_id);
                //btnSave.disabled = true;
                $("#btnRegistroAusentismo").addClass("invisible");
                $("#btnUpdate").removeClass("invisible");
                $("#btnClear").removeClass("invisible");

            }
        });
    }
    // carga los tipos de recuperar ausentismos 
    LoadSelectRecuperaAusentismo = () => {
        $.ajax({
            method: "POST",
            url: "../Catalogos/LoadTipoRecuperaAusentismo",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (data) => {
                var select = document.getElementById("inRecuperacionAusentismo");
                select.innerHTML = "<option value=''> Selecciona </option>";
                for (var i = 0; i < data.length; i++) {
                    select.innerHTML += "<option value='" + data[i][0] + "'>" + data[i][1] + "</option>";
                }
            }
        });
    }
    checkPorDias = () => {
        var porDias = "<div class='form-group col-md-12'>"
            + "<label for='inFechaAusentismo'>Fecha</label>"
            + "<input type='date' name='inFechaAusentismo' id='inFechaAusentismo' class='form-control form-control-sm font-labels' required />"
            + "</div>"
            + "<div class='form-group col-md-12'>"
            + "<label for='inDiasAusentismo'>Dias</label>"
            + "<input type='text' name='inDiasAusentismo' id='inDiasAusentismo' class='form-control input-number form-control-sm' placeholder='Escriba los dias de Ausentismo' min='1' maxlength='2' required />"
            + "</div>";
        document.getElementById("frmtipo").innerHTML = "";
        setTimeout(() => {
            document.getElementById("frmtipo").innerHTML = porDias;
        }, 500);
    }
    checkPorFecha = () => {
        var porFecha = "<div class='form-group col-md-12'>"
            + "<label for='inFechaiAusentismo'> Fecha Inicio </label>"
            + "<input type='date' name='inFechaiAusentismo' id='inFechaiAusentismo' class='form-control form-control-sm font-labels' required />"
            + "</div>"
            + "<div class='form-group col-md-12'>"
            + "<label for='inFechafAusentismo'> Fecha Fin </label>"
            + "<input type='date' name='inFechafAusentismo' id='inFechafAusentismo' class='form-control form-control-sm font-labels' required />"
            + "</div>";
        document.getElementById("frmtipo").innerHTML = "";
        setTimeout(() => {
            document.getElementById("frmtipo").innerHTML = porFecha;
        }, 500);
    }
    LoadDiasRestantesPeriodo = () => {
        $.ajax({
            method: "POST",
            url: "../Catalogos/LoadDiasRestantesPeriodo",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (data) => {

            }
        });
    }
});