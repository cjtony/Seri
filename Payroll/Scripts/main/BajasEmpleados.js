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
                        document.getElementById("id_emp").innerHTML = res[0] + " - " + res[1];
                        document.getElementById("sueldo_emp").innerHTML = res[2].substring(0, res[2].length - 2);
                        document.getElementById("aumento_emp").innerHTML = res[3];
                        document.getElementById("antiguedad_emp").innerHTML = res[4];
                        document.getElementById("ingreso_emp").innerHTML = res[5];
                        document.getElementById("nivel_emp").innerHTML = res[6];
                        document.getElementById("posicion_emp").innerHTML = res[7];
                        document.getElementById('dateAntiquityEmp').value = res[4];
                        dateSendDown.innerHTML += `<option value="0">Fecha de antiguedad - ${res[4]}</option>`;
                        dateSendDown.innerHTML += `<option value="1">Fecha de ingreso    - ${res[5]}</option>`;
                        document.getElementById('info-employee').classList.remove('d-none');
                        //document.getElementById('info-employee').classList.add('animated fadeIn delay-1s');
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
    const btnShowWindowDataDown = document.getElementById('btnShowWindowDataDonw');
    const btnGuardaBaja = document.getElementById('btnGuardaBaja');
    const keyEmployee   = document.getElementById('keyEmployee');
    const dateAntiquityEmp = document.getElementById('dateAntiquityEmp');
    const inTiposBaja   = document.getElementById('inTiposBaja');
    const inMotivosBaja = document.getElementById('inMotivosBaja');
    const dateDownEmp   = document.getElementById('dateDownEmp');
    const daysPendings  = document.getElementById('daysPendings');
    //const dateRec       = document.getElementById('dateRec');
    const dateSendDown  = document.getElementById('dateSendDown');
    const compSendEsp   = document.getElementById('compSendEsp');

    const btnCloseSettlementSelect = document.getElementById("btnCloseSettlementSelect");
    const icoCloseSettlementSelect = document.getElementById("icoCloseSettlementSelect");

    class CampoNumerico {

        constructor(selector) {
            this.nodo  = document.querySelector(selector);
            this.valor = '';
            this.eventoKeyUp();
        }

        eventoKeyUp = () => {
            this.nodo.addEventListener('keydown', function(ev) {
                const teclaPress = ev.key;
                const teclaPressNumero = Number.isInteger(parseInt(teclaPress));
                const teclaPressNoAdmitida =
                    teclaPress != "ArrowDown" && teclaPress != "ArrowUp"    &&
                    teclaPress != "ArrowLeft" && teclaPress != "ArrowRight" &&
                    teclaPress != "Backspace" && teclaPress != "Delete"     &&
                    teclaPress != "Enter"     && !teclaPressNumero;
                const comienzaCero = this.nodo.value.length === 0 && teclaPress == 0;
                if (teclaPressNoAdmitida || comienzaCero) {
                    ev.preventDefault();
                } else if (teclaPressNumero) {
                    this.valor += String(teclaPress);
                }
            }.bind(this));

            this.nodo.addEventListener('input', function (ev) {
                const cumpleFormatoEsperado = new RegExp(/^[0-9]+/).test(this.nodo.value);
                if (!cumpleFormatoEsperado) {
                    this.nodo.value = this.valor;
                } else {
                    this.valor = this.nodo.value;
                }
            }.bind(this));
        }
    }

    //new CampoNumerico("#daysPendings");

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

    // Funcion que muestra los finiquitos que un empleado tiene 
    fShowDataDown = () => {
        document.getElementById('list-data-down').innerHTML = "";
        document.getElementById('no-data-info').innerHTML = "";
        try {
            console.log("IdEmpleado: " + keyEmployee.value);
            if (keyEmployee.value != "") {
                $.ajax({
                    url: "../BajasEmpleados/ShowDataDown",
                    type: "POST",
                    data: { keyEmployee: parseInt(keyEmployee.value) },
                    beforeSend: () => {
                        console.log('Cargando');
                    }, success: (data) => {
                        console.log(data);
                        if (data.Bandera == true && data.MensajeError == "none") {
                            let sumEstatus  = 0;
                            for (let j = 0; j < data.DatosFiniquito.length; j++) {
                                if (data.DatosFiniquito[j].iEstatus > 0) {
                                    sumEstatus += 1;
                                }
                            }
                            for (let i = 0; i < data.DatosFiniquito.length; i++) {
                                let actionSavePay = `onclick="fSelectSettlementPaid(${data.DatosFiniquito[i].iIdFiniquito})"`;
                                let actionCancel  = `onclick="fCancelSettlement(${data.DatosFiniquito[i].iIdFiniquito}, 1)"`;
                                let titleCancel   = `title="Cancelar Finiquito"`;
                                let icoCancel     = `<i class="fas fa-times"></i>`;
                                let colBtnCancel  = "btn-danger";
                                let validCancel = "";
                                let infoPaid = "";
                                let checked  = "";
                                let cancel   = "";
                                let cancelPay = "";
                                const infoPeriod = `<span class="badge ml-2 badge-info"><i class="fas fa-calendar-alt mr-1"></i>
                                    ${data.DatosFiniquito[i].iAnioPeriodo} - ${data.DatosFiniquito[i].iPeriodo}
                                </span>`;
                                let enabledPay = "";
                                if (data.DatosFiniquito[i].sCancelado == "True") {
                                    validCancel  = "disabled";
                                    cancel       = `<span class="badge ml-2 badge-danger"> <i class="fas fa-times-circle mr-1"></i>Cancelado</span>`;
                                    actionCancel = `onclick="fCancelSettlement(${data.DatosFiniquito[i].iIdFiniquito}, 0)"`;
                                    titleCancel  = `title="Reactivar Finiquito"`;
                                    icoCancel    = `<i class="fas fa-undo text-white"></i>`;
                                    colBtnCancel = "btn-warning";
                                    cancelPay    = "disabled";
                                }
                                if (data.DatosFiniquito[i].iEstatus == 1) {
                                    actionSavePay = "disabled";
                                    enabledPay    = "disabled";
                                    checked       = "checked";
                                    infoPaid      = `<span class="badge ml-2 badge-warning"><i class="fas fa-clock mr-1"></i>Pendiente para pago</span>`;
                                } else if (data.DatosFiniquito[i].iEstatus == 2) {
                                    checked = "checked";
                                    infoPaid = `<span class="badge ml-2 badge-success"><i class="fas fa-clock mr-1"></i>Pagado</span>`;
                                }
                                document.getElementById('list-data-down').innerHTML += `
                                <li class="list-group-item d-flex justify-content-between align-items-center">
                                    <span>
                                        <div class="form-check mb-2" id="divSelectPay${data.DatosFiniquito[i].iIdFiniquito}">
                                            <input ${enabledPay} ${cancelPay} ${checked} class="form-check-input" type="checkbox" name="selectPay${data.DatosFiniquito[i].iIdFiniquito}"
                                                id="radioSelect${data.DatosFiniquito[i].iIdFiniquito}" 
                                                    value="${data.DatosFiniquito[i].iIdFiniquito}">
                                            <label class="form-check-label" for="radioSelect${data.DatosFiniquito[i].iIdFiniquito}">
                                            Elegir para pago ${infoPaid} ${infoPeriod} ${cancel}
                                            </label>
                                        </div>
                                        <i class="fas fa-calendar-alt mr-1 col-ico"></i>
                                            Baja: ${data.DatosFiniquito[i].sFecha_baja} 
                                        <i class="fas fa-tag ml-1 mr-1 col-ico"></i>
                                            Tipo: ${data.DatosFiniquito[i].sFiniquito_valor}
                                    </span>
                                    <span class="badge">
                                        <a href="#" class="btn btn-sm btn-primary" title="Detalle"
                                            onclick="fGenerateReceiptPDF(${data.DatosFiniquito[i].iIdFiniquito},${data.DatosFiniquito[i].iEmpleado_id})"> 
                                            <i class="fas fa-eye"></i> 
                                        </a>
                                        <button ${validCancel} class="btn btn-sm btn-success" title="Guardar" ${actionSavePay}>
                                            <i class="fas fa-check"></i>
                                        </button>
                                        <button class="btn btn-sm ${colBtnCancel}" ${titleCancel} ${actionCancel}>
                                            ${icoCancel}
                                        </button>
                                    </span>
                                </li>
                            `;
                            }
                        } else if (data.Bandera == false && data.MensajeError == "NOTLOADINFO") {
                            document.getElementById('no-data-info').innerHTML += `
                                <div class="col-md-8 offset-2 mt-3">
                                    <div class="alert alert-info" role="alert">
                                      <i class="fas fa-info-circle mr-2"></i>El empleado no cuenta con ningun finiquito generado.
                                    </div>
                                </div>
                            `;
                        } else {
                            alert('Error!');
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                alert('Error');
                location.reload();
            }
        } catch (error) {
            if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else {
                console.error('Error: ', error.message);
            }
        }
    }

    // Funcion que limpia los campos 
    fClearFields = () => {
        inTiposBaja.value   = "none";
        inMotivosBaja.value = "none";
        dateDownEmp.value = "";
        //dateRec.value = "";
        dateSendDown.value = "none";
        compSendEsp.value = "none";
    }

    // Funcion que guarda la eleccion para pago del finiquito
    fSelectSettlementPaid = (paramid) => {
        try {
            const checkBoxSel = document.getElementById("radioSelect" + String(paramid));
            if ($("#divSelectPay"+String(paramid)+" input[id='radioSelect" + String(paramid) + "']:checkbox").is(':checked')) {
                if (parseInt(paramid) > 0) {
                    $.ajax({
                        url: "../BajasEmpleados/SelectSettlementPaid",
                        type: "POST",
                        data: { keySettlement: parseInt(paramid) },
                        beforeSend: () => {
                            console.log('Guardando');
                        }, success: (data) => {
                            if (data.Bandera == true && data.MensajeError == "none") {
                                Swal.fire({
                                    title: "Correcto", text: "Opcion guardada", icon: "success",
                                    showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                    confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                }).then((acepta) => {
                                    fShowDataDown();
                                });
                            } else {
                                
                                fShowTypeAlert('Error', "Ocurrio un error al guardar la opcion para pago", "error", checkBoxSel, 0);
                            }
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    });
                } else {
                    location.reload();
                }
            } else {
                fShowTypeAlert('Atención', 'Selecciona una opcion de pago', 'info', checkBoxSel, 2);
            }
        } catch (error) {
            if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else {
                console.error('Error: ', error.message);
            }
        }
    }

    // Funcion que genera el PDF del finiquito
    fGenerateReceiptPDF = (paramid, paramide) => {
        try {
            if (parseInt(paramid) > 0) {
                $.ajax({
                    url: "../BajasEmpleados/GenerateReceiptDown",
                    type: "POST",
                    data: { keySettlement: parseInt(paramid), keyEmployee: parseInt(paramide) },
                    beforeSend: () => {
                        console.log('Generando');
                    }, success: (data) => {
                        console.log(data);
                        $("#window-data-down").modal("hide");
                        if (data.Bandera == true && data.MensajeError == "none") {
                            setTimeout(() => {
                                $("#settlement-details").modal("show");
                            }, 1000);
                            const salario_mensual = parseInt(data.InfoFiniquito[0].sSalario_mensual).toFixed(2);
                            const salario_diario  = parseInt(data.InfoFiniquito[0].sSalario_diario).toFixed(2);
                            if (data.InfoFiniquito[0].sCancelado == "True") {
                                document.getElementById('div-details').innerHTML += `
                                    <div class="col-md-12">
                                        <div class="alert alert-danger" role="alert">
                                          <h5 class="alert-heading text-center">Finiquito cancelado!</h5>
                                        </div>
                                    </div>
                                `;
                            }
                            document.getElementById("div-details").innerHTML += `
                                <div class="col-md-6 mt-4">
                                    <ul class="list-group shadow card rounded">
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-1"></i>Fecha de baja</span>
                                            <span class="badge badge-primary">${data.InfoFiniquito[0].sFecha_baja}</span>
                                        </li>
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-1"></i>Fecha de ingreso</span>
                                            <span class="badge badge-primary">${data.InfoFiniquito[0].sFecha_ingreso}</span>
                                        </li>
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-1"></i>Fecha de antiguedad</span>
                                            <span class="badge badge-primary">${data.InfoFiniquito[0].sFecha_antiguedad}</span>
                                        </li>
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-1"></i>Fecha recibo</span>
                                            <span class="badge badge-primary">${data.InfoFiniquito[0].sFecha_recibo}</span>
                                        </li>  
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-1"></i>Dias pendientes</span>
                                            <span class="badge badge-primary">${data.InfoFiniquito[0].iDias_Pendientes}</span>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-6 mt-4">
                                    <ul class="list-group shadow card rounded">
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-1"></i> Año y periodo </span>
                                            <span class="badge badge-primary">${data.InfoFiniquito[0].iAnioPeriodo} - ${data.InfoFiniquito[0].iPeriodo}</span>
                                        </li>
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-1"></i>Años trabajados</span>
                                            <span class="badge badge-primary">${data.InfoFiniquito[0].iAnios}</span>
                                        </li>
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-1"></i>Dias trabajados</span>
                                            <span class="badge badge-primary">${data.InfoFiniquito[0].sDias}</span>
                                        </li>  
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-money-check-alt mr-1 col-ico"></i>Salario mensual</span>
                                            <span class="badge badge-primary">$${salario_mensual}</span>
                                        </li>
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-money-check-alt mr-1 col-ico"></i>Salario diario</span>
                                            <span class="badge badge-primary">$${salario_diario}</span>
                                        </li> 
                                    </ul>
                                </div>
                                <div class="form-group mt-5 col-md-4 offset-4">
                                    <a class="btn btn-primary btn-block btn-sm" id="btnprint${paramid}"> <i class="fas fa-download"></i> Descargar PDF </a>
                                </div>
                            `;
                            document.getElementById("typeSettlement").textContent = data.InfoFiniquito[0].sFiniquito_valor;
                            document.getElementById("btnprint" + String(paramid)).setAttribute("download", data.NombrePDF);
                            document.getElementById("btnprint" + String(paramid)).setAttribute("href", "../../Content/" + data.NombreFolder + "/" + data.NombrePDF);
                            btnCloseSettlementSelect.setAttribute("onclick", "fDeletePdfSettlement('" + data.NombrePDF + "'," + paramid + ", '" + data.NombreFolder + "')");
                            icoCloseSettlementSelect.setAttribute("onclick", "fDeletePdfSettlement('" + data.NombrePDF + "'," + paramid + ", '"+ data.NombreFolder +"')");
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                alert('Error');
            }
        } catch (error) {
            if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else {
                console.error('Error: ', error.message);
            }
        }
    }

    // Funcion que elimina el pdf generado una vez descargado
    fDeletePdfSettlement = (paramstr, paramid, paramfolder) => {
        try {
            if (paramstr != "") {
                $.ajax({
                    url: "../BajasEmpleados/DeletePdfSettlement",
                    type: "POST",
                    data: { namePdfSettlement: paramstr, nameFolderLocation: paramfolder },
                    beforeSend: () => {
                        btnCloseSettlementSelect.disabled = true;
                        icoCloseSettlementSelect.disabled = true;
                        document.getElementById("div-details").innerHTML = `<div class='col-md-6 text-center offset-3 mt-3'>
                            <div class="alert alert-info" role="alert">
                              <b>Espere un momento por favor...</b>
                            </div>
                        </div>`;
                    }, success: (data) => {
                        setTimeout(() => {
                            if (data.BanderaValida == true && data.BanderaComprueba == true && data.BanderaElimina == true && data.MensajeError == "none") {
                                $("#settlement-details").modal("hide");
                                document.getElementById("div-details").innerHTML = "";
                                document.getElementById("typeSettlement").textContent = "";
                                btnCloseSettlementSelect.removeAttribute("onclick");
                                icoCloseSettlementSelect.removeAttribute("onclick");
                                btnCloseSettlementSelect.disabled = false;
                                icoCloseSettlementSelect.disabled = false;
                                fShowDataDown();
                                setTimeout(() => {
                                    $("#window-data-down").modal("show");
                                }, 500);
                            } else {
                                alert('Error al eliminar el pdf del almacenamiento temporal');
                            }
                        }, 1000); 
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                location.reload();
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

    // Funcion que cancela el finiquito generado
    fCancelSettlement = (paramid, typecancel) => {
        try {
            if (parseInt(paramid) > 0) {
                $.ajax({
                    url: "../BajasEmpleados/CancelSettlement",
                    type: "POST",
                    data: { keySettlement: parseInt(paramid), typeCancel: parseInt(typecancel) },
                    beforeSend: () => {
                        console.log('Cancelando...');
                    }, success: (data) => {
                        if (data.Bandera == true && data.MensajeError == "none") {
                            Swal.fire({
                                title: "Correcto",
                                text: "Finiquito " + data.TipoAccion + "!",
                                icon: "success",
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false,
                                allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                fShowDataDown();
                                setTimeout(() => { $("#window-data-down").modal("show"); }, 1000);
                            });
                        } else {
                            alert("ERROR! al cancelar");
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                alert('Error!');
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

    // Funcion que envia los datos de la baja (Finiquitos)
    fSendDataDown = () => {
        let valDaysPendings;
        if (daysPendings.value != "" || daysPendings.value > 0) {
            valDaysPendings = daysPendings.value;
        } else {
            valDaysPendings = 0;
        }
        try {
            if (inTiposBaja.value != "none") {
                if (inMotivosBaja.value != "none") {
                    if (dateDownEmp.value != "") {
                        if (dateSendDown.value == "1" || dateSendDown.value == "0") {
                            if (compSendEsp.value == "1" || compSendEsp.value == "0") {
                                const dataSend = {
                                    keyEmployee: parseInt(keyEmployee.value),
                                    dateAntiquityEmp: (dateAntiquityEmp.value),
                                    idTypeDown: parseInt(inTiposBaja.value),
                                    idReasonsDown: parseInt(inMotivosBaja.value),
                                    dateDownEmp: String(dateDownEmp.value),
                                    daysPending: parseInt(valDaysPendings),
                                    //dateReceipt: String(dateRec.value),
                                    typeDate: parseInt(dateSendDown.value),
                                    typeCompensation: parseInt(compSendEsp.value)
                                };
                                console.log(dataSend);
                                $.ajax({
                                    url: "../BajasEmpleados/SendDataDownSettlement",
                                    type: "POST",
                                    data: dataSend,
                                    success: (data) => {
                                        if (data.Bandera == true && data.MensajeError == "none") {
                                            Swal.fire({
                                                title: "Correcto",
                                                text: "Datos registrados!",
                                                icon: "success",
                                                showClass: { popup: 'animated fadeInDown faster' },
                                                hideClass: { popup: 'animated fadeOutUp faster' },
                                                confirmButtonText: "Aceptar", allowOutsideClick: false,
                                                allowEscapeKey: false, allowEnterKey: false,
                                            }).then((value) => {
                                                fClearFields();
                                                fShowDataDown();
                                                setTimeout(() => {
                                                    $("#window-data-down").modal("show");
                                                }, 1000);
                                            });
                                        } else if (data.Bandera == false && data.MensajeError == "ERRMOSTINFO") {
                                            alert("Registro correcto, error al mostrar informacion");
                                        } else if (data.Bandera == false && data.MensajeError == "ERRINSFINIQ") {
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

    btnShowWindowDataDown.addEventListener('click', fShowDataDown);
    btnGuardaBaja.addEventListener('click', fSendDataDown);

});