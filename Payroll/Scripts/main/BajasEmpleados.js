$(function () { 
    


    //Muestra en principio el modal de busqueda
    $("#modalLiveSearchEmpleado").modal("show");
    //Funcion que hace la busqueda de empleado por nombre o numero de nomina
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
                    console.log(data[0]["iFlag"]);
                    $("#resultSearchEmpleados").empty();
                    if (data[0]["iFlag"] == 0) {
                        for (var i = 0; i < data.length; i++) {
                            $("#resultSearchEmpleados").append("<div class='list-group-item list-group-item-action btnListEmpleados font-labels  font-weight-bold' onclick='MostrarDatosEmpleado(" + data[i]["IdEmpleado"] + ")' > <i class='far fa-user-circle text-primary'></i> " + data[i]["Nombre_Empleado"] + " " + data[i]["Apellido_Paterno_Empleado"] + ' ' + data[i]["Apellido_Materno_Empleado"] + "   -   <small class='text-muted'><i class='fas fa-briefcase text-warning'></i> " + data[i]["DescripcionDepartamento"] + "</small> - <small class='text-muted'>" + data[i]["DescripcionPuesto"] + "</small></div>");
                        }
                    }
                    else {
                        $("#resultSearchEmpleados").append("<button type='button' class='list-group-item list-group-item-action btnListEmpleados font-labels'  >" + data[0]["Nombre_Empleado"] + "<br><small class='text-muted'>" + data[0]["DescripcionPuesto"] + "</small> </button>");
                    }                }
            });
        } else {
            $("#resultSearchEmpleados").empty();
        }
    });
    $("#inTiposBaja").on("change", function () {

        var tipob = document.getElementById("inTiposBaja");
        var motivob = document.getElementById("inMotivosBaja");
        var t = tipob.value;
        $.ajax({
            url: "../Nomina/LoadMotivoBajaxTe",
            type: "POST",
            data: JSON.stringify(),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: (tipo) => {
                document.getElementById('inMotivosBaja').innerHTML = "<option value='none'>Selecciona</option>";
                for (var i = 0; i < tipo.length; i++) {
                    console.log(t + " y " + tipo[i]["TipoEmpleado_id"]);
                    if (t == tipo[i]["TipoEmpleado_id"]) {
                        console.log(tipob + " y " + tipo[i]["TipoEmpleado_id"]);
                        document.getElementById("inMotivosBaja").innerHTML += "<option value='" + tipo[i]["IdMotivo_Baja"] + "'>" + tipo[i]["Descripcion"] + "</option>";
                    }
                    
                }
            }
        });
    });

    

    MostrarDatosEmpleado = (idE) => {
        
        var txtIdEmpleado = { "IdEmpleado": idE };
        $.ajax({
            url: "../Empleados/SearchEmpleado",
            type: "POST",
            data: JSON.stringify(txtIdEmpleado),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: (emp) => {
                console.log('Mostrando datos de empleado');
                console.log(emp);
                document.getElementById("nameuser").innerHTML = emp[0].Nombre_Empleado + " " + emp[0].Apellido_Paterno_Empleado + " " + emp[0].Apellido_Materno_Empleado;
                //carga datos de header para baja
                $.ajax({
                    url: "../Nomina/LoadDatosBaja",
                    type: "POST",
                    data: JSON.stringify(),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: (res) => {
                        console.log(res);
                        document.getElementById('keyEmployee').value = res[0];
                        document.getElementById("id_emp").innerHTML = res[0];
                        document.getElementById("sueldo_emp").innerHTML = res[2].substring(0, res[2].length - 2);
                        document.getElementById("aumento_emp").innerHTML = res[3];
                        document.getElementById("antiguedad_emp").innerHTML = res[4];
                        document.getElementById("ingreso_emp").innerHTML = res[5];
                        document.getElementById("nivel_emp").innerHTML = res[6];
                        document.getElementById("posicion_emp").innerHTML = res[7];
                        document.getElementById('dateAntiquityEmp').value = res[4];
                        dateSendDown.innerHTML += `<option value="0">Fecha de antiguedad - ${res[4]}</option>`;
                        dateSendDown.innerHTML += `<option value="1">Fecha de ingreso - ${res[5]}</option>`;
                        document.getElementById('info-employee').classList.remove('d-none');
                        document.getElementById('info-employee').classList.add('animated fadeIn delay-1s');
                    }
                });
                //carga select tipo Baja
                $.ajax({
                    url: "../Nomina/LoadTipoBaja",
                    type: "POST",
                    data: JSON.stringify(),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: (tipo) => {
                        console.log('Tipos de baja');
                        console.log(tipo);
                        for (var i = 0; i < tipo.length; i++) {
                            document.getElementById("inTiposBaja").innerHTML += "<option value='" + tipo[i]["IdTipo_Empleado"] + "'>" + tipo[i]["Descripcion"] + "</option>";
                        }
                    }
                });

                $("#modalLiveSearchEmpleado").modal("hide");
            }
        });
    }

    /*
     * Constantes bajas
     */

    const btnGuardaBaja = document.getElementById('btnGuardaBaja');
    const keyEmployee   = document.getElementById('keyEmployee');
    const dateAntiquityEmp = document.getElementById('dateAntiquityEmp');
    const inTiposBaja   = document.getElementById('inTiposBaja');
    const inMotivosBaja = document.getElementById('inMotivosBaja');
    const dateDownEmp   = document.getElementById('dateDownEmp');
    const dateRec       = document.getElementById('dateRec');
    const dateSendDown  = document.getElementById('dateSendDown');
    const compSendEsp   = document.getElementById('compSendEsp');

    /*
     * Funciones
     */

    // Funcion que captura los errores de ajax que se puedan generar
    fcaptureaerrorsajax = (jq, exc) => {
        let msg = "";
        if (jq.status === 0) {
            msg = "No conectado. \n Verifica tu conexión de red.";
        } else if (jq.status === 404) {
            msg = 'Página solicitada no encontrada. [404]';
        } else if (jq.status == 500) {
            msg = 'Error interno del servidor [500].';
        } else if (exc === 'parsererror') {
            msg = 'El análisis JSON solicitado falló.';
        } else if (exc === 'timeout') {
            msg = 'Error de tiempo de espera.';
        } else if (exc === 'abort') {
            msg = 'Solicitud de Ajax abortada.';
        } else {
            msg = 'Error no detectado.\n' + jq.responseText;
        }
        console.log(msg);
    }

    // Funcion que muestra alertas de forma dinamica
    fShowTypeAlert = (title, text, icon, element, clear) => {
        Swal.fire({
            title: title, text: text, icon: icon,
            showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
            confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
        }).then((acepta) => {
            $("html, body").animate({ scrollTop: $(`#${element.id}`).offset().top - 50 }, 1000);
            if (clear == 1) {
                setTimeout(() => {
                    element.focus();
                    setTimeout(() => { element.value = ""; }, 300);
                }, 1200);
            } else if (clear == 2) {
                setTimeout(() => { element.focus(); }, 1200);
            }
        });
    }

    // Funcion que envia los datos de la baja (Finiquitos)
    fSendDataDown = () => {
        try {
            if (inTiposBaja.value != "none") {
                if (inMotivosBaja.value != "none") {
                    if (dateDownEmp.value != "") {
                        if (dateRec.value != "") {
                            if (dateSendDown.value == "1" || dateSendDown.value == "0") {
                                if (compSendEsp.value == "1" || compSendEsp.value == "0") {
                                    const dataSend = {
                                        keyEmployee: parseInt(keyEmployee.value),
                                        dateAntiquityEmp: (dateAntiquityEmp.value),
                                        idTypeDown: parseInt(inTiposBaja.value),
                                        idReasonsDown: parseInt(inMotivosBaja.value),
                                        dateDownEmp: String(dateDownEmp.value),
                                        dateReceipt: String(dateRec.value),
                                        typeDate: parseInt(dateSendDown.value),
                                        typeCompensation: parseInt(compSendEsp.value)
                                    };
                                    console.log(dataSend);
                                    $.ajax({
                                        url: "../BajasEmpleados/SendDataDownSettlement",
                                        type: "POST",
                                        data: dataSend,
                                        success: (data) => {
                                            if (data.Bandera == true && MensajeError == "none") {
                                                if (data.DatosFiniquito.length > 0) {
                                                    for (let i = 0; i < data.DatosFiniquito.length; i++) {
                                                        console.log(data.DatosFiniquito[i].iIdFiniquito);
                                                    }
                                                    setTimeout(() => {
                                                        $("#window-data-down").modal("show");
                                                    }, 1000);
                                                } else {
                                                    alert("No se pudo cargar la informacion");
                                                }
                                            } else if (data.Bandera == false && MensajeError == "ERRMOSTINFO") {
                                                alert("Registro correcto, error al mostrar informacion");
                                            } else if (data.Bandera == false && MensajeError == "ERRINSFINIQ") {
                                                alert("Error al registrar la informacion");
                                            } else {
                                                alert("Ocurrio un error, reporte al área de TI");
                                            }
                                        }, error: (jqXHR, exception) => {
                                            fcaptureaerrorsajax(jqXHR, exception);
                                        }
                                    });
                                } else {
                                    fShowTypeAlert('Atención', 'Seleccione una opción para el campo compensacion especial', 'info', compSendEsp, 2);
                                }
                            } else {
                                fShowTypeAlert('Atención', 'Seleccione una opción para el campo fecha a usar', 'info', dateSendDown, 2);
                            }
                        } else {
                            fShowTypeAlert('Atención', 'Selecciona una fecha de recibo', 'info', dateRec, 2);
                        }
                    } else {
                        fShowTypeAlert('Atención', 'Seleccione una fecha de baja para el empleado', 'info', dateDownEmp, 2);
                    }
                } else {
                    fShowTypeAlert('Atención', 'Selecciona una opcion para el motivo de baja', 'info', inMotivosBaja, 2);
                }
            } else {
                fShowTypeAlert('Atención', 'Seleccione una opción para el tipo de baja', 'info', inTiposBaja, 2);
            }
        } catch (error) {
            if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else {
                console.error('Error: ', error.message);
            }
        }
    }

    /*
     * Ejecucion de funciones
     */

    btnGuardaBaja.addEventListener('click', fSendDataDown);

});