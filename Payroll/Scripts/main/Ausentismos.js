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

    $("#modalLiveSearchEmpleado").modal("toggle");
    var motivoA = document.getElementById("inMotivoAusentismo");
    //var tabAusentismo = $("#tabAusentismos").DataTable();
    $("#inFechaAusentismo").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd/mm/yy'
    });
    //Funcion para guardar los Ausentismos
    $("#btnRegistroAusentismo").on("click", function (evt) {
        var data = $("#frmAusentismos").serialize();
        var cer_imss, com_imss,cau_falta;
        var form = document.getElementById("frmAusentismos");
        if (form.checkValidity() === false) {
            evt.preventDefault();
            form.classList.add("was-validated");
            console.log(data);
        } else {
            evt.preventDefault();
            console.log(data);
            if (certif.value.length != 0) { cer_imss = certif.value } else { cer_imss = "0" }
            if (coment.value.length != 0) { com_imss = coment.value } else { com_imss = "0" }
            if (causa.value.length != 0) { cau_falta = causa.value } else { cau_falta = "0" }
            $.ajax({
                url: "../Incidencias/SaveAusentismo",
                type: "POST",
                data: JSON.stringify({
                    Tipo_Ausentismo_id: motivo.value,
                    Recupera_Ausentismo: recup.value,
                    Fecha_Ausentismo: fechaa.value,
                    Dias_Ausentismo: dias.value,
                    Certificado_imss: cer_imss,
                    Comentarios_imss: com_imss,
                    Causa_FaltaInjustificada: cau_falta
                }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    
                    $.ajax({
                        method: "POST",
                        url: "../Incidencias/LoadAusentismosTab",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: (data) => {
                            console.log(data);
                            document.getElementById("tabody").innerHTML = "";
                            for (var i = 0; i < data.length; i++) {
                                //document.getElementById("tabody").innerHTML += "<tr><td>" + data[i]["Tipo_Ausentismo_id"] + "</td><td>" + data[i]["Fecha_Ausentismo"] + "</td><td>" + data[i]["Dias_Ausentismo"] + "</td><td><div class='btn btn-secondary btn-sm btn-editar-ausentismo' onclick='eliminarAusentismo( " + data[i]["IdAusentismo"] + " );'><i class='far fa-edit'></i></div></td></tr>";
                                document.getElementById("tabody").innerHTML += "<tr><td>" + data[i]["Nombre_Ausentismo"] + "</td><td>" + data[i]["Fecha_Ausentismo"].substring(0,10) + "</td><td>" + data[i]["Dias_Ausentismo"] + "</td><td><div class='btn-group' role='group' aria-label='Basic example'><div class='btn btn-subm btn-sm btn-editar-ausentismo' onclick='editarAusentismo( " + data[i]["IdAusentismo"] + " );'><i class='far fa-edit'></i></div><div class='btn btn-secondary btn-sm btn-eliminar-ausentismo' onclick='eliminarAusentismo( " + data[i]["IdAusentismo"] + " );'><i class='far fa-trash-alt'></i></div></div></td></tr>";
                                console.log(data[i]["Tipo_Ausentismo_id"]);
                            }
                        }
                    });
                    fechaa.value = "";
                    dias.value = "";
                    cau_falta.value = "";
                    Swal.fire({
                        icon: 'success',
                        title: 'Correcto!',
                        text: "Ausentismo agregado con éxito"

                    });

                   
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
                if (data[i].iIdTipoAusentismo >= 22) {

                } else {
                    motivoA.innerHTML += `<option value='${data[i].iIdTipoAusentismo}'> ${data[i].sDescripcionAusentismo} </option>`
                }
            }
        }
    });
    //Evento de cambio de motivo accidente
    $("#inMotivoAusentismo").on("change", function () {
        console.log("cambio de motivo");
        var causa = document.getElementById("inCausaAusentismo");
        var certif = document.getElementById("inCertificadoAusentismo");
        var coment = document.getElementById("inComentariosAusentismo");
        var motivo = $("#inMotivoAusentismo").val();
        console.log(motivo);
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
        console.log("btn-editar");
        var data = $("#frmAusentismos").serialize();
        var cer_imss, com_imss, cau_falta;
        var form = document.getElementById("frmAusentismos");
        if (form.checkValidity() === false) {
            
            form.classList.add("was-validated");
            console.log(data);
        } else {
            
            console.log(data);
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
                    Fecha_Ausentismo: fechaa.value,
                    Dias_Ausentismo: dias.value,
                    Certificado_imss: cer_imss,
                    Comentarios_imss: com_imss,
                    Causa_FaltaInjustificada: cau_falta
                }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: (data) => {

                    $.ajax({
                        method: "POST",
                        url: "../Incidencias/LoadAusentismosTab",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: (data) => {
                            console.log(data);
                            document.getElementById("tabody").innerHTML = "";
                            for (var i = 0; i < data.length; i++) {
                                //document.getElementById("tabody").innerHTML += "<tr><td>" + data[i]["Tipo_Ausentismo_id"] + "</td><td>" + data[i]["Fecha_Ausentismo"] + "</td><td>" + data[i]["Dias_Ausentismo"] + "</td><td><div class='btn btn-secondary btn-sm btn-editar-ausentismo' onclick='eliminarAusentismo( " + data[i]["IdAusentismo"] + " );'><i class='far fa-edit'></i></div></td></tr>";
                                document.getElementById("tabody").innerHTML += "<tr><td>" + data[i]["Nombre_Ausentismo"] + "</td><td>" + data[i]["Fecha_Ausentismo"].substring(0, 10) + "</td><td>" + data[i]["Dias_Ausentismo"] + "</td><td><div class='btn-group' role='group' aria-label='Basic example'><div class='btn btn-subm btn-sm btn-editar-ausentismo' onclick='editarAusentismo( " + data[i]["IdAusentismo"] + " );'><i class='far fa-edit'></i></div><div class='btn btn-secondary btn-sm btn-eliminar-ausentismo' onclick='eliminarAusentismo( " + data[i]["IdAusentismo"] + " );'><i class='far fa-trash-alt'></i></div></div></td></tr>";
                                console.log(data[i]["Tipo_Ausentismo_id"]);
                            }
                        }
                    });
                    fechaa.value = "";
                    dias.value = "";
                    cau_falta.value = "";
                    Swal.fire({
                        icon: 'success',
                        title: 'Correcto!',
                        text: "Ausentismo agregado con éxito"

                    });


                }
            });

        }
    });
    $("#btn-cambiar-empleado").on("click", function () {
        $("#modalLiveSearchEmpleado").modal("toggle");
    });

});

function MostrarDatosEmpleado(idE) {
    var txtIdEmpleado = { "IdEmpleado": idE };
    $.ajax({
        url: "../Empleados/SearchEmpleado",
        type: "POST",
        data: JSON.stringify(txtIdEmpleado),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: (data) => {
            console.log(data[0]["Nombre_Empleado"]);
            document.getElementById("EmpDes").innerHTML = "<i class='far fa-user-circle text-primary'></i> " + data[0]["Nombre_Empleado"] + " " + data[0]["Apellido_Paterno_Empleado"] + ' ' + data[0]["Apellido_Materno_Empleado"] + "   -   <small class='text-muted'> " + data[0]["DescripcionDepartamento"] + "</small> - <small class='text-muted'>" + data[0]["DescripcionPuesto"] + "</small>";
            $("#modalLiveSearchEmpleado").modal("hide");
            document.getElementById("resultSearchEmpleados").innerHTML = "";
            document.getElementById("inputSearchEmpleados").value = "";
            tabAusentismo();
            //document.getElementById("nameuser").innerHTML = "<div class='text-uppercase'>" + data[0]["Nombre_Empleado"] + " " + data[0]["Apellido_Paterno_Empleado"] + ' ' + data[0]["Apellido_Materno_Empleado"] + "<small class='text-muted'>" + data[0]["DescripcionPuesto"] + "</small></div>";
        }
    });
    //Funcion para validar solo numeros 
    $('.input-number').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
}
function tabAusentismo() {
    $.ajax({
        method: "POST",
        url: "../Incidencias/LoadAusentismosTab",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: (data) => {
            console.log(data);
            document.getElementById("tabody").innerHTML = "";
            for (var i = 0; i < data.length; i++) {
                document.getElementById("tabody").innerHTML += "<tr><td>" + data[i]["Nombre_Ausentismo"] + "</td><td>" + data[i]["Fecha_Ausentismo"].substring(0,10) + "</td><td>" + data[i]["Dias_Ausentismo"] + "</td><td><div class='btn-group' role='group' aria-label='Basic example'><div class='btn btn-subm btn-sm btn-editar-ausentismo' onclick='editarAusentismo( " + data[i]["IdAusentismo"] + " );'><i class='far fa-edit'></i></div><div class='btn btn-secondary btn-sm btn-eliminar-ausentismo' onclick='eliminarAusentismo( " + data[i]["IdAusentismo"] + " );'><i class='far fa-trash-alt'></i></div></div></td></tr>";
                console.log(data[i]["Tipo_Ausentismo_id"]);
            }
        }
    });
}
function eliminarAusentismo(Ausentismo_id) {
    console.log("boton eliminar: " + Ausentismo_id);
    $.ajax({
        url: "../Incidencias/DeleteAusentismo",
        type: "POST",
        data: JSON.stringify({ IdAusentismo : Ausentismo_id }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: (data) => {
            console.log(data);
            Swal.fire({
                icon: 'success',
                title: 'Correcto!',
                text: data[1]

            });
        }
    });
}
//Editar ausentismo
function editarAusentismo(Ausentismo_id) {
    var motivo = document.getElementById("inMotivoAusentismo");
    var recup = document.getElementById("inRecuperacionAusentismo");
    var fechaa = document.getElementById("inFechaAusentismo");
    var dias = document.getElementById("inDiasAusentismo");
    var causa = document.getElementById("inCausaAusentismo");
    var certif = document.getElementById("inCertificadoAusentismo");
    var coment = document.getElementById("inComentariosAusentismo");

    console.log("boton eliminar: " + Ausentismo_id);
    $.ajax({
        url: "../Incidencias/LoadAusentismo",
        type: "POST",
        data: JSON.stringify({ IdAusentismo: Ausentismo_id }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: (data) => {
            console.log(data);
            dias.value = data[0].Dias_Ausentismo;
            causa.value = data[0].Causa_FaltaInjustificada;
            console.log(data[0].Certificado_imss.length);
            if (data[0].Certificado_imss.length != 0) {
                certif.disabled = false;
                coment.disabled = false;
                certif.value = data[0].Certificado_imss;
                coment.value = data[0].Comentarios_imss;
            }
            //Carga los tipos de ausentismos 
            $.ajax({
                url: "../Incidencias/LoadAusentismos",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: (data1) => {
                    
                    motivo.innerHTML = "";
                    for (var j = 0; j < data1.length; j++) {
                        if (data1[j].iIdTipoAusentismo >= 21) {

                        } else {

                            if (data[0].Tipo_Ausentismo_id == data1[j].iIdTipoAusentismo) {
                                motivo.innerHTML += `<option selected value='${data1[j].iIdTipoAusentismo}'> ${data1[j].sDescripcionAusentismo} </option>`
                            } else {
                                motivo.innerHTML += `<option value='${data1[j].iIdTipoAusentismo}'> ${data1[j].sDescripcionAusentismo} </option>`
                            }
                        }
                    }
                    $("#btnUpdate").attr("btnUpdate", Ausentismo_id);
                }
            });

        }
    });
}
function updateAusentismo() {

}