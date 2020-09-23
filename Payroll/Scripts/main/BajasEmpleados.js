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
                            $("#resultSearchEmpleados").append("<div style='cursor:pointer;' class='list-group-item list-group-item-action btnListEmpleados font-labels  font-weight-bold' onclick='MostrarDatosEmpleado(" + data[i]["IdEmpleado"] + ")' > <i class='far fa-user-circle text-primary'></i> " + data[i]["Nombre_Empleado"] + " " + data[i]["Apellido_Paterno_Empleado"] + ' ' + data[i]["Apellido_Materno_Empleado"] + "   -   <small class='text-muted'><i class='fas fa-briefcase text-warning'></i> " + data[i]["DescripcionDepartamento"] + "</small> - <small class='text-muted'>" + data[i]["DescripcionPuesto"] + "</small></div>");
                        }
                    } else {
                        $("#resultSearchEmpleados").append("<button type='button' class='list-group-item list-group-item-action btnListEmpleados font-labels'  >" + data[0]["Nombre_Empleado"] + "<br><small class='text-muted'>" + data[0]["DescripcionPuesto"] + "</small> </button>");
                    }                }
            });
        } else {
            $("#resultSearchEmpleados").empty();
        }
    });

    //$("#inTiposBaja").on("change", function () {

        
    //});

    fLoadMotiveDown = () => {
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
                    if (t == tipo[i]["TipoEmpleado_id"]) {
                        document.getElementById("inMotivosBaja").innerHTML += "<option value='" + tipo[i]["IdMotivo_Baja"] + "'>" + tipo[i]["Descripcion"] + "</option>";
                    }

                }
            }
        });
    }

    

    MostrarDatosEmpleado = (idE) => {
        document.getElementById('inputSearchEmpleados').value = "";
        document.getElementById('resultSearchEmpleados').innerHTML = "";
        var txtIdEmpleado = { "IdEmpleado": idE };
        dateSendDown.innerHTML = `<option value="none">Selecciona</option>`;
        document.getElementById("inTiposBaja").innerHTML = `<option value="none">Selecciona</option>`;
        $.ajax({
            url: "../Empleados/SearchEmpleado",
            type: "POST",
            data: JSON.stringify(txtIdEmpleado),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: (emp) => {
                console.log('Mostrando datos de empleado');
                console.log(emp);
                //document.getElementById("nameuser").innerHTML = emp[0].Nombre_Empleado + " " + emp[0].Apellido_Paterno_Empleado + " " + emp[0].Apellido_Materno_Empleado;
                //carga datos de header para baja
                $.ajax({
                    url: "../Nomina/LoadDatosBaja",
                    type: "POST",
                    data: JSON.stringify(),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: (res) => {
                        document.getElementById('keyEmployee').value        = res[0];
                        document.getElementById("id_emp").innerHTML         = res[0] + " - " + res[1];
                        document.getElementById("sueldo_emp").innerHTML     = "$ " + res[2];
                        document.getElementById("aumento_emp").innerHTML    = res[3];
                        document.getElementById("antiguedad_emp").innerHTML = res[4];
                        document.getElementById("ingreso_emp").innerHTML    = res[5];
                        document.getElementById("nivel_emp").innerHTML      = res[6];
                        document.getElementById("posicion_emp").innerHTML   = res[7];
                        document.getElementById('dateAntiquityEmp').value   = res[4];
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
    const downSettlement = document.getElementById('down-settlement');
    const btnShowWindowDataDown = document.getElementById('btnShowWindowDataDonw');
    const btnGuardaBaja = document.getElementById('btnGuardaBaja');
    const keyEmployee   = document.getElementById('keyEmployee');
    const dateAntiquityEmp = document.getElementById('dateAntiquityEmp');
    const inTiposBaja   = document.getElementById('inTiposBaja');
    const inMotivosBaja = document.getElementById('inMotivosBaja');
    const dateDownEmp   = document.getElementById('dateDownEmp');
    const daysPendings  = document.getElementById('daysPendings');
    const dateSendDown  = document.getElementById('dateSendDown');
    const compSendEsp   = document.getElementById('compSendEsp');
    const divContentP1  = document.getElementById('div-content-param1');
    const divContentP2  = document.getElementById('div-content-param2');
    const divContentP3  = document.getElementById('div-content-param3');

    const btnCloseSettlementSelect = document.getElementById("btnCloseSettlementSelect");
    const icoCloseSettlementSelect = document.getElementById("icoCloseSettlementSelect");

    divContentP1.classList.add('d-none');
    divContentP2.classList.add('d-none');
    divContentP3.classList.add('d-none');

    inTiposBaja.addEventListener('change', fLoadMotiveDown);

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

    // Funcion que muestra una animacion
    fShowAnimationInput = (element) => {
        setTimeout(() => {
            element.classList.add('animated', 'bounce');
        }, 1000);
        setTimeout(() => {
            element.classList.remove('animated', 'bounce');
        }, 2000);
    }

    // Funcion que muestra alertas de forma dinamica
    fShowTypeAlertDE = (title, text, icon, element, clear, animateinp) => {
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
            if (clear == 0 && animateinp == 0) {
                fClearFields();
            }
            if (animateinp == 1) {
                fShowAnimationInput(element);
            }
        });
    }

    fAddAnimatedFields = (element) => {
        element.classList.remove('d-none', 'fadeOut');
        element.classList.add('fadeIn');
    }

    fRemAnimatedFields = (element) => {
        element.classList.remove('fadeIn');
        element.classList.add('fadeOut');
        setTimeout(() => { element.classList.add('d-none'); }, 1000);
    }

    // Funcion que habilita los campos a llenar dependiendo si es baja con finiquito o no
    fSHowFieldsSettlement = () => {
        try {
            const downSetValue = downSettlement.value;
            if (downSetValue == "1") {
                fAddAnimatedFields(divContentP1);
                fAddAnimatedFields(divContentP2);
                fAddAnimatedFields(divContentP3);
            } else {
                fRemAnimatedFields(divContentP1);
                fRemAnimatedFields(divContentP2);
                fRemAnimatedFields(divContentP3);
            }
        } catch (error) {
            if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
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
                                const inf = data.DatosFiniquito[i];
                                let actionSavePay = `onclick="fSelectSettlementPaid(${data.DatosFiniquito[i].iIdFiniquito})"`;
                                let actionCancel  = `onclick="fCancelSettlement(${data.DatosFiniquito[i].iIdFiniquito}, 1)"`;
                                let titleCancel   = `title="Cancelar Finiquito"`;
                                let icoCancel     = `<i class="fas fa-times"></i>`;
                                let colBtnCancel  = "btn-danger";
                                let btnPaidSucces = "";
                                let validCancel = "";
                                let infoPaid = "";
                                let checked  = "";
                                let cancel   = "";
                                let cancelPay = "";
                                let downNotSettlement = "";
                                let btnGenerateSettlement = "";
                                let actionGeneratePDF = `onclick="fGenerateReceiptPDF(${data.DatosFiniquito[i].iIdFiniquito},${data.DatosFiniquito[i].iEmpleado_id})"`;
                                let disabledGeneratePDF = "";
                                const infoPeriod = `<span class="badge ml-2 badge-info"><i class="fas fa-calendar-alt mr-1"></i>
                                    ${data.DatosFiniquito[i].iAnioPeriodo} - ${data.DatosFiniquito[i].iPeriodo}
                                </span>`;
                                let spanDownNotSet = "";
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
                                    infoPaid = `<span class="badge ml-2 badge-warning"><i class="fas fa-clock mr-1"></i>Pendiente para pago</span>`;
                                    btnPaidSucces = `<button id="btnConfirmPaidSuc${data.DatosFiniquito[0].iIdFiniquito}" onclick="fConfirmPaidSuccess(${data.DatosFiniquito[i].iIdFiniquito})" type="button" class="btn btn-sm btn-success" title="Marcar como pagado">
                                            <i class="fas fa-check-circle"></i>
                                        </button>`;
                                } else if (data.DatosFiniquito[i].iEstatus == 2) {
                                    actionSavePay = "disabled";
                                    enabledPay    = "disabled";
                                    checked       = "checked";
                                    checked       = "checked";
                                    infoPaid = `<span class="badge ml-2 badge-success"><i class="fas fa-check-circle mr-1"></i>Pagado</span>`;
                                } else if (data.DatosFiniquito[i].iEstatus == 3) {
                                    enabledPay = "disabled";
                                    actionSavePay = "disabled";
                                    actionGeneratePDF = "";
                                    disabledGeneratePDF = "disabled";
                                    spanDownNotSet = `<span class="badge ml-2 badge-danger"><i class="fas fa-file mr-1"></i>Sin finiquito</span>`;
                                    btnGenerateSettlement = `<button onclick="fSetGenerateSettlement(${data.DatosFiniquito[i].iIdFiniquito}, '${inf.sFecha_baja}', ${inf.iTipo_finiquito_id}, ${inf.iMotivo_baja})" class="btn btn-sm btn-info" title="Asignar finiquito" id="btnGenerateSet${data.DatosFiniquito[i].iIdFiniquito}"> <i class="fas fa-money-check-alt"></i> </button>`;
                                    console.log('Imprimiendo datos del finiquito sin calculos');
                                    console.log(data.DatosFiniquito[i]);
                                }
                                document.getElementById('list-data-down').innerHTML += `
                                <li class="list-group-item d-flex justify-content-between align-items-center shadow rounded">
                                    <span>
                                        <div class="form-check mb-2" id="divSelectPay${data.DatosFiniquito[i].iIdFiniquito}">
                                            <input ${enabledPay} ${cancelPay} ${checked} class="form-check-input" type="checkbox" name="selectPay${data.DatosFiniquito[i].iIdFiniquito}"
                                                id="radioSelect${data.DatosFiniquito[i].iIdFiniquito}" 
                                                    value="${data.DatosFiniquito[i].iIdFiniquito}">
                                            <label class="form-check-label" for="radioSelect${data.DatosFiniquito[i].iIdFiniquito}">
                                            Elegir para pago ${infoPeriod} ${infoPaid} ${cancel} ${spanDownNotSet}
                                            </label>
                                        </div>
                                        <i class="fas fa-calendar-alt mr-1 col-ico"></i>
                                            <span style="font-size:14px;"><b>Baja:</b> ${data.DatosFiniquito[i].sFecha_baja}.</span>
                                        <i class="fas fa-tag ml-1 mr-1 col-ico"></i>
                                            <span style="font-size:14px;"><b>Tipo:</b> ${data.DatosFiniquito[i].sFiniquito_valor}.</span>
                                    </span>
                                    <span class="badge">
                                        ${btnGenerateSettlement}
                                        <button class="btn btn-sm btn-primary" title="Detalle" id="btnGenerateReceipt${data.DatosFiniquito[i].iIdFiniquito}"
                                            ${actionGeneratePDF} ${disabledGeneratePDF}> 
                                            <i class="fas fa-eye"></i> 
                                        </button>
                                        ${btnPaidSucces}
                                        <button ${validCancel} class="btn btn-sm btn-success" title="Guardar" ${actionSavePay} id="btnSelectPay${data.DatosFiniquito[i].iIdFiniquito}">
                                            <i class="fas fa-check"></i>
                                        </button>
                                        <button class="btn btn-sm ${colBtnCancel}" ${titleCancel} ${actionCancel} id="btnCancelSettlement${data.DatosFiniquito[i].iIdFiniquito}">
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
        downSettlement.value = "none";
        inTiposBaja.value    = "none";
        inMotivosBaja.value  = "none";
        dateDownEmp.value    = "";
        daysPendings.value   = 0;
        dateSendDown.value   = "none";
        compSendEsp.value    = "none";
        fRemAnimatedFields(divContentP1);
        fRemAnimatedFields(divContentP2);
        fRemAnimatedFields(divContentP3);
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
                            document.getElementById('btnSelectPay' + String(paramid)).disabled = true;
                        }, success: (data) => {
                            if (data.Bandera == true && data.MensajeError == "none") {
                                Swal.fire({
                                    title: "Correcto", text: "Opcion guardada", icon: "success",
                                    showClass: { popup: 'animated fadeInDown faster' },
                                    hideClass: { popup: 'animated fadeOutUp faster'  },
                                    confirmButtonText: "Aceptar",
                                    allowOutsideClick: false,
                                    allowEscapeKey:    false,
                                    allowEnterKey:     false,
                                }).then((acepta) => {
                                    fShowDataDown();
                                });
                            } else {
                                fShowTypeAlertDE('Error', "Ocurrio un error al guardar la opcion para pago", "error", checkBoxSel, 0, 0);
                            }
                            document.getElementById('btnSelectPay' + String(paramid)).disabled = false;
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    });
                } else {
                    alert('Accion invalida!');
                    location.reload();
                }
            } else {
                fShowTypeAlertDE('Atención', 'Selecciona una opcion de pago', 'info', checkBoxSel, 2, 1);
                //fShowAnimationInput(checkBoxSel);
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

    function replaceAll(string, search, replace) {
        return string.split(search).join(replace);
    }

    // Funcion que le asigna un finiquito a una baja
    fSetGenerateSettlement = (paramid, paramdate, paramtf, parammd) => {
        try {
            if (parseInt(paramid) > 0) {
                $("#window-data-down").modal('hide');
                const arrayDateDown  = String(paramdate).split("/");
                downSettlement.value = "1";
                inTiposBaja.value    = paramtf;
                dateDownEmp.value    = arrayDateDown[2] + "-" + arrayDateDown[1] + "-" + arrayDateDown[0];
                fLoadMotiveDown();
                setTimeout(() => { inMotivosBaja.value = parammd; }, 1000);
                setTimeout(() => { fSHowFieldsSettlement(); }, 1100);
            } else {
                alert('Accion invalida');
                location.reload();
            }
        } catch (error) {
            if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else {
                console.error('Error: ', error);
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
                        document.getElementById('btnGenerateReceipt' + String(paramid)).disabled = true;
                    }, success: (data) => {
                        $("#window-data-down").modal("hide");
                        if (data.Bandera == true && data.MensajeError == "none") {
                            console.log('Imprimiendo datos del finiquito');
                            console.log(data);
                            setTimeout(() => {
                                $("#settlement-details").modal("show");
                            }, 1000);
                            const salario_mensual = data.InfoFiniquito[0].sSalario_mensual;
                            const salario_diario  = data.InfoFiniquito[0].sSalario_diario;
                            if (data.InfoFiniquito[0].sCancelado == "True") {
                                document.getElementById('div-details').innerHTML += `
                                    <div class="col-md-12">
                                        <div class="alert alert-danger" role="alert">
                                          <h5 class="alert-heading text-center">Finiquito cancelado!</h5>
                                        </div>
                                    </div>
                                `;
                            }
                            let infoPaid = "";
                            if (data.InfoFiniquito[0].iEstatus == 1) {
                                infoPaid = `<span class="badge ml-2 badge-warning p-2"><i class="fas fa-clock mr-2"></i>Pendiente para pago</span>`;
                            } else if (data.InfoFiniquito[0].iEstatus == 2) {
                                infoPaid = `<span class="badge ml-2 badge-success p-2"><i class="fas fa-check-circle mr-2"></i>Pagado</span>`;
                            }
                            document.getElementById("div-details").innerHTML += `
                                <div class="col-md-6 mt-4">
                                    <ul class="list-group shadow card rounded">
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-2"></i>Fecha de baja</span>
                                            <span class="badge bg-light text-primary font-weight-bold p-2">${data.InfoFiniquito[0].sFecha_baja}</span>
                                        </li>
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-2"></i>Fecha de ingreso</span>
                                            <span class="badge bg-light text-primary font-weight-bold p-2">${data.InfoFiniquito[0].sFecha_ingreso}</span>
                                        </li>
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-2"></i>Fecha de antiguedad</span>
                                            <span class="badge bg-light text-primary font-weight-bold p-2">${data.InfoFiniquito[0].sFecha_antiguedad}</span>
                                        </li>
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-2"></i>Fecha recibo</span>
                                            <span class="badge bg-light text-primary font-weight-bold p-2">${data.InfoFiniquito[0].sFecha_recibo}</span>
                                        </li>  
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-2"></i>Dias pendientes</span>
                                            <span class="badge bg-light text-primary font-weight-bold p-2">${data.InfoFiniquito[0].iDias_Pendientes}</span>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-6 mt-4">
                                    <ul class="list-group shadow card rounded">
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-1"></i> Año y periodo </span>
                                            <span class="badge bg-light text-primary font-weight-bold p-2">${data.InfoFiniquito[0].iAnioPeriodo} - ${data.InfoFiniquito[0].iPeriodo}</span>
                                        </li>
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-1"></i>Años trabajados</span>
                                            <span class="badge bg-light text-primary font-weight-bold p-2">${data.InfoFiniquito[0].iAnios}</span>
                                        </li>
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-calendar-alt col-ico mr-1"></i>Dias trabajados</span>
                                            <span class="badge bg-light text-primary font-weight-bold p-2">${data.InfoFiniquito[0].sDias}</span>
                                        </li>  
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-money-check-alt mr-1 col-ico"></i>Salario mensual</span>
                                            <span class="badge bg-light text-primary font-weight-bold p-2">$ ${salario_mensual}</span>
                                        </li>
                                        <li class="list-group-item d-flex justify-content-between align-items-center">
                                            <span><i class="fas fa-money-check-alt mr-1 col-ico"></i>Salario diario</span>
                                            <span class="badge bg-light text-primary font-weight-bold p-2">$ ${salario_diario}</span>
                                        </li> 
                                    </ul>
                                </div>
                                <div class="form-group mt-4 col-md-4 offset-4 text-center">
                                    <a class="btn btn-primary btn-sm btn-icon-split" id="btnprint${paramid}">
                                        <span class="icon text-white-50">
                                            <i class="fas fa-download"></i>
                                        </span>
                                        <span class="text">Descargar PDF</span>
                                    </a>
                                </div>
                            `;
                            document.getElementById("typeSettlement").textContent = data.InfoFiniquito[0].sFiniquito_valor + " - ";
                            const dataInfo = data.InfoFiniquito[0];
                            document.getElementById('typeDown').textContent = dataInfo.sMotivo_baja;
                            document.getElementById("headerSettlement").innerHTML += infoPaid;
                            document.getElementById("btnprint" + String(paramid)).setAttribute("download", data.NombrePDF);
                            document.getElementById("btnprint" + String(paramid)).setAttribute("href", "../../Content/" + data.NombreFolder + "/" + data.NombrePDF);
                            btnCloseSettlementSelect.setAttribute("onclick", "fDeletePdfSettlement('" + data.NombrePDF + "'," + paramid + ", '" + data.NombreFolder + "')");
                            icoCloseSettlementSelect.setAttribute("onclick", "fDeletePdfSettlement('" + data.NombrePDF + "'," + paramid + ", '"+ data.NombreFolder +"')");
                        }
                        document.getElementById('btnGenerateReceipt' + String(paramid)).disabled = false;
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

    // Funcion que marca el finiquito como pagado
    fConfirmPaidSuccess = (paramid) => {
        try {
            if (parseInt(paramid) > 0) {
                $.ajax({
                    url: "../BajasEmpleados/ConfirmPaidSuccess",
                    type: "POST",
                    data: { keySettlement: parseInt(paramid) },
                    beforeSend: () => {
                        document.getElementById('btnConfirmPaidSuc' + String(paramid)).disabled = true;
                    }, success: (data) => {
                        if (data.Bandera === true && data.MensajeError === "none") {
                            Swal.fire({
                                title: "Correcto", text: "Opcion guardada", icon: "success",
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                fShowDataDown();
                            });
                        } else {
                            const checkBoxSel = document.getElementById("radioSelect" + String(paramid));
                            fShowTypeAlertDE('Error', "Ocurrio un error al confirmar el pago", "error", checkBoxSel, 0,0);
                        }
                        document.getElementById('btnConfirmPaidSuc' + String(paramid)).disabled = false;
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                alert('Accion invalida');
                location.reload();
            }
        } catch (error) {
            if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else {
                console.error('Error: ', error);
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
                        document.getElementById('btnCancelSettlement' + String(paramid)).disabled = true;
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
                        document.getElementById('btnCancelSettlement' + String(paramid)).disabled = false;
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
        let dataSendType = {};
        let flagTypeValt;
        try {
            if (downSettlement.value != "none") {
                flagTypeValt = (downSettlement.value == "1") ? true : false;
                if (inTiposBaja.value != "none") {
                    if (inMotivosBaja.value != "none") {
                        if (dateDownEmp.value != "") {
                            if (!flagTypeValt) {
                                dateSendDown.value = "0";
                                compSendEsp.value  = "0";
                            }
                            console.log('Valor de la bandera: ' + flagTypeValt);
                            console.log('Valor de fecha a usar: ' + dateSendDown.value);
                            console.log('Valor de compensacion: ' + compSendEsp.value);
                            if (dateSendDown.value == "1" || dateSendDown.value == "0") {
                                if (compSendEsp.value == "1" || compSendEsp.value == "0") {
                                    const dataSend = {
                                        keyEmployee: parseInt(keyEmployee.value),
                                        dateAntiquityEmp: (dateAntiquityEmp.value),
                                        idTypeDown: parseInt(inTiposBaja.value),
                                        idReasonsDown: parseInt(inMotivosBaja.value),
                                        dateDownEmp: String(dateDownEmp.value),
                                        daysPending: parseInt(valDaysPendings),
                                        typeDate: parseInt(dateSendDown.value),
                                        typeCompensation: parseInt(compSendEsp.value),
                                        flagTypeSettlement: flagTypeValt
                                    };
                                    console.log('Datos a enviar: ');
                                    console.log(dataSend);
                                    $.ajax({
                                        url: "../BajasEmpleados/SendDataDownSettlement",
                                        type: "POST",
                                        data: dataSend,
                                        beforeSend: () => {
                                            btnGuardaBaja.disabled = true;
                                        }, success: (data) => {
                                            console.log(data);
                                            if (data.Existencia === false) {
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
                                                    fShowTypeAlertDE('Error!', 'al mostrar informacion', 'error', btnShowWindowDataDown, 0, 0);
                                                } else if (data.Bandera == false && data.MensajeError == "ERRINSFINIQ") {
                                                    fShowTypeAlertDE('Error!', 'al registrar informacion', 'error', btnShowWindowDataDown, 0, 0);
                                                } else {
                                                    fShowTypeAlertDE('Error!', 'Contacte al area de TI', 'error', btnShowWindowDataDown, 0, 0);
                                                }
                                            } else {
                                                fShowTypeAlertDE('Atención!', 'No puedes generar 2 finiquitos en un mismo periodo', 'warning', btnShowWindowDataDown, 0, 0);
                                            }
                                            btnGuardaBaja.disabled = false;
                                        }, error: (jqXHR, exception) => {
                                            fcaptureaerrorsajax(jqXHR, exception);
                                        }
                                    });
                                } else {
                                    fShowTypeAlertDE('Atención', 'Seleccione una opción para el campo compensacion especial', 'info', compSendEsp, 2, 0);
                                }
                            } else {
                                fShowTypeAlertDE('Atención', 'Seleccione una opción para el campo fecha a usar', 'info', dateSendDown, 2, 0);
                            }
                        } else {
                            fShowTypeAlertDE('Atención', 'Seleccione una fecha de baja para el empleado', 'info', dateDownEmp, 2, 0);
                        }
                    } else {
                        fShowTypeAlertDE('Atención', 'Selecciona una opcion para el motivo de baja', 'info', inMotivosBaja, 2, 0);
                    }
                } else {
                    fShowTypeAlertDE('Atención', 'Seleccione una opción para el tipo de baja', 'info', inTiposBaja, 2, 0);
                }
            } else {
                fShowTypeAlertDE('Atención', 'Seleccione una opción para el baja con finiquito', 'info', downSettlement, 2, 1);
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

    downSettlement.addEventListener('change', fSHowFieldsSettlement);

});