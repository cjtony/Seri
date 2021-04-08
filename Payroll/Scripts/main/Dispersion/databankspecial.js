$(function () {

    localStorage.removeItem("bankIntConfig");
    localStorage.removeItem("nameGroup");
    localStorage.removeItem("disabledBtnSCD");
    localStorage.removeItem("typeSend");

    /*
     * CONSTANTES
     */

    // const btnViewBanks      = document.getElementById('btn-view-banks');
    const icoCloseViewBanks   = document.getElementById('ico-close-view-banks');
    const btnCloseViewBanks   = document.getElementById('btn-close-view-banks');
    const contentViewBanks    = document.getElementById('content-view-banks');
    const contentViewBanksInt = document.getElementById('content-view-banks-int');
    const contentViewBanksNom = document.getElementById('content-view-banks-nom');

    const selectBankOption = document.getElementById('select-bank-option');
    const selectBankInt    = document.getElementById('select-bank-int');
    const contentDCBank    = document.getElementById('content-details-config-banks');

    const btnCloseViewCd = document.getElementById('btn-close-view-cd');
    const icoCloseViewCd = document.getElementById('ico-close-view-cd');

    /*
     * FUNCIONES
     */

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    fShowAlertSuccessSave = () => {
        Swal.fire({
            position: "top-end",
            html: "<b></b>",
            icon: "success",
            showConfirmButton: false,
            timer: 1500,
            onBeforeOpen: () => {
                const content = Swal.getContent();
                if (content) {
                    const b = content.querySelector('b');
                    if (b) { b.textContent = String('Configuración guardada!'); }
                }
            }
        });
    }

    // Funcion que muestra alertas dinamicamente \\
    fShowTypeAlert = (title, text, icon, element, clear) => {
        Swal.fire({
            title: title, text: text, icon: icon,
            showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
            confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
        }).then((acepta) => {
            $("html, body").animate({ scrollTop: $(`#${element.id}`).offset().top - 50 }, 1000);
            
        });
    }

    // Muestra todos los bancos disponibles
    fViewBanks = (paramint, paramstr) => {
        localStorage.setItem("nameGroup", paramstr);
        contentViewBanks.innerHTML = "";
        try { 
            const badges = ["primary", "secondary", "success", "info", "light", "dark", "warning", "danger"];
            //console.log(typeof badges);
            //console.log(badges);
            $("#groupBusiness").modal("hide");
            $.ajax({
                url: "../Dispersion/ViewBanks",
                type: "POST",
                data: { key: String("banks"), keyGroup: parseInt(paramint) },
                beforeSend: () => {
                    contentViewBanks.innerHTML += `
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <div class="spinner-border text-primary" role="status">
                                    <span class="sr-only">Loading...</span>
                                </div>
                                <br />
                                <h4 class="mt-2">Un momento por favor...</h4>
                            </div>
                        </div>
                    `;
                }, success: (request) => {
                    //console.log(request);
                    contentViewBanks.innerHTML = "";
                    let htmlResult = "";
                    document.getElementById('name-group-business-bank').textContent = "Configuracion bancaria " + String(paramstr);
                    if (request.Bandera == true && request.MensajeError == "none") {
                        htmlResult = "<div class='row'> <div class='col-md-12 text-center'>";
                        for (let i = 0; i < request.Datos.length; i++) {
                            let randomNumeric = Math.floor(Math.random() * (badges.length - 0) + 0);
                            //console.log(badges[randomNumeric]);
                            if (randomNumeric == 8) {
                                randomNumeric = randomNumeric - 1;
                            }
                            htmlResult += `<span class="badge badge-${badges[randomNumeric]} mr-2 p-2 mt-2">${request.Datos[i].sNombreBanco}</span>`;
                        }
                        fShowConfigBanksInt(paramint, 'INTERBANCARIO', 'ONE');
                        fShowConfigBanksNom(paramint, 'NOMINA', 'ONE');
                        htmlResult.innerHTML += "<hr/> </div>";
                        contentViewBanks.innerHTML = htmlResult;
                        setTimeout(() => {
                            $("#viewBanks").modal("show");
                        }, 1000);
                    } else {
                        htmlResult = `
                            <div class="alert alert-danger alert-dismissible fade show shadow rounded p-3 text-center" role="alert">
                              <strong><i class="fas fa-frown mr-2"></i>Atención!</strong> No pude encontrar los bancos asociados a este grupo de empresas.
                            </div>
                        `;
                        contentViewBanks.innerHTML = htmlResult;
                        setTimeout(() => {
                            $("#viewBanks").modal("show");
                        }, 1000);
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        } catch (error) {
            if (error instanceof EvalError) {
                console.error("EvalError: ", error.message);
            } else if (error instanceof RangeError) {
                console.error("RangeError: ", error.message);
            } else if (error instanceof TypeError) {
                console.error("TypeError: ", error.message);
            } else {
                console.error("Error: ", error);
            }
        }
    }

    // Funcion que muestra los bancos interbancarios configurados
    fShowConfigBanksInt = (paramint, paramtype, paramoption) => {
        try {
            contentViewBanksInt.innerHTML = "";
            let htmlResult = "";
            if (parseInt(paramint) > 0) {
                $.ajax({
                    url: "../Dispersion/ShowConfigBanks",
                    type: "POST",
                    data: { keyGroup: parseInt(paramint), type: String(paramtype), option: String(paramoption) },
                    beforeSend: () => {
                        //console.log('Consultando...');
                    }, success: (request) => {
                        //console.log(request);
                        let checkedBanorte    = "";
                        let checkedSantander  = "";
                        let checkedScotiabank = "";
                        let disabledConfig    = "disabled";
                        for (let i = 0; i < request.Datos.length; i++) {
                            let data = request.Datos[i];
                            if (data.iBanco == 72) {
                                checkedBanorte = (data.iActivo == 1) ? "checked" : "";
                            }
                            if (data.iBanco == 14) {
                                checkedSantander = (data.iActivo == 1) ? "checked" : "";
                            }
                            if (data.iBanco == 44) {
                                checkedScotiabank = (data.iActivo == 1) ? "checked" : "";
                            }
                            if (data.iActivo == 1) {
                                disabledConfig = "";
                            }
                        }
                        htmlResult += ` 
                             <div class="row animated fadeInDown">
                                <div class="col-md-4">
                                    <div class="form-group shadow rounded p-2">
                                        <h6 class="font-weight-bold text-center">Interbancario</h6><hr/>
                                        <div class="form-group text-center">
                                            <button id="btn-save-config-bank-int" class="btn btn-success btn-sm shadow rounded" onclick="fSaveConfigBankInt(${parseInt(paramint)})"> <i class="fas fa-check-circle"></i> Guardar </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group shadow rounded p-2">
                                        <div class="form-check">
                                          <input class="form-check-input" type="checkbox" value="" id="banorteInt" ${checkedBanorte}>
                                          <label class="form-check-label" for="banorteInt">
                                            Banorte
                                          </label>
                                        </div>
                                        <div class="form-check">
                                          <input class="form-check-input" type="checkbox" value="" id="santanderInt" checkedSantander ${checkedSantander}>
                                          <label class="form-check-label" for="santanderInt">
                                            Santander
                                          </label>
                                        </div>
                                        <div class="form-check">
                                          <input class="form-check-input" type="checkbox" value="" id="scotiabankInt" ${checkedScotiabank}>
                                          <label class="form-check-label" for="scotiabankInt">
                                            Scotiabank
                                          </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div clasS="form-group mt-4">
                                        <div class="form-check text-center">
                                            <button class="btn btn-sm shadow btn-block btn-primary" ${disabledConfig} onclick="fConfigBanksSelected(${paramint}, 1, 'INTERBANCARIO')"> 
                                                <i class="fas fa-cogs mr-2"></i> Configuración </button>
                                        </div>
                                    </div>
                                </div>
                        `;
                        contentViewBanksInt.innerHTML += htmlResult;
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
                console.error("EvalError: ", error.message);
            } else if (error instanceof RangeError) {
                console.error("RangeError: ", error.message);
            } else if (error instanceof TypeError) {
                console.error("TypeError: ", error.message);
            } else {
                console.error("Error: ", error);
            }
        }
    }

    // Funcion que muestra los bancos de nomina configurados
    fShowConfigBanksNom = (paramint, paramtype, paramoption) => {
        try {
            contentViewBanksNom.innerHTML = "";
            let htmlResult = "";
            if (parseInt(paramint) > 0) {
                $.ajax({
                    url: "../Dispersion/ShowConfigBanks",
                    type: "POST",
                    data: { keyGroup: parseInt(paramint), type: String(paramtype), option: String(paramoption) },
                    beforeSend: () => {

                    }, success: (request) => {
                        //console.log(request);
                        let checkedBanorte    = "";
                        let checkedSantander  = "";
                        let checkedScotiabank = "";
                        let checkedBanamex    = "";
                        let checkedBancomer   = "";
                        let disabledConfig    = "disabled";
                        for (let i = 0; i < request.Datos.length; i++) {
                            let data = request.Datos[i];
                            if (data.iBanco == 72) {
                                checkedBanorte = (data.iActivo == 1) ? "checked" : "";
                            }
                            if (data.iBanco == 14) {
                                checkedSantander = (data.iActivo == 1) ? "checked" : "";
                            }
                            if (data.iBanco == 44) {
                                checkedScotiabank = (data.iActivo == 1) ? "checked" : "";
                            }
                            if (data.iBanco == 2) {
                                checkedBanamex = (data.iActivo == 1) ? "checked" : "";
                            }
                            if (data.iBanco == 12) {
                                checkedBancomer = (data.iActivo == 1) ? "checked" : "";
                            }
                            if (data.iActivo == 1) {
                                disabledConfig = "";
                            }
                        }
                        htmlResult += ` <div class="row animated fadeInDown">
                                <div class="col-md-4">
                                    <div class="form-group shadow rounded p-2">
                                        <h6 class="font-weight-bold text-center">Nómina</h6><hr/>
                                        <div class="form-group text-center">
                                            <button id="btn-save-config-bank-nom" class="btn btn-success btn-sm shadow rounded" onclick="fSaveConfigBankNom(${parseInt(paramint)})"> <i class="fas fa-check-circle"></i> Guardar </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group shadow rounded p-2">
                                        <div class="form-check">
                                          <input class="form-check-input" type="checkbox" value="" id="banorteNom" ${checkedBanorte}>
                                          <label class="form-check-label" for="banorteNom">
                                            Banorte
                                          </label>
                                        </div>
                                        <div class="form-check">
                                          <input class="form-check-input" type="checkbox" value="" id="banamexNom" ${checkedBanamex}>
                                          <label class="form-check-label" for="banamexNom">
                                            Banamex
                                          </label>
                                        </div>
                                        <div class="form-check">
                                          <input class="form-check-input" type="checkbox" value="" id="bancomerNom" ${checkedBancomer}>
                                          <label class="form-check-label" for="bancomerNom">
                                            Bancomer
                                          </label>
                                        </div>
                                        <div class="form-check">
                                          <input class="form-check-input" type="checkbox" value="" id="santanderNom" ${checkedSantander}>
                                          <label class="form-check-label" for="santanderNom">
                                            Santander
                                          </label>
                                        </div>
                                        <div class="form-check">
                                          <input class="form-check-input" type="checkbox" value="" id="scotiabankNom" ${checkedScotiabank}>
                                          <label class="form-check-label" for="scotiabankNom">
                                            Scotiabank
                                          </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div clasS="form-group mt-5">
                                        <div class="form-check text-center">
                                            <button class="btn btn-sm btn-block btn-primary shadow" ${disabledConfig} onclick="fConfigBanksSelected(${paramint}, 1, 'NOMINA')"> <i class="fas fa-cogs mr-2"></i> Configuración </button>
                                        </div>
                                    </div>
                                </div>
                            </div>`;
                        contentViewBanksNom.innerHTML += htmlResult;
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
                console.error("EvalError: ", error.message);
            } else if (error instanceof RangeError) {
                console.error("RangeError: ", error.message);
            } else if (error instanceof TypeError) {
                console.error("TypeError: ", error.message);
            } else {
                console.error("Error: ", error);
            }
        }
    }

    // Funcion que guarda la configuracion de la dispersion interbancaria
    fSaveConfigBankInt = (paramint) => {
        try {
            if (parseInt(paramint) > 0) {
                const btnInterb  = document.getElementById('btn-save-config-bank-int');
                const banorte    = (document.getElementById('banorteInt').checked)    ? 1 : 0;
                const santander  = (document.getElementById('santanderInt').checked)  ? 1 : 0;
                const scotiabank = (document.getElementById('scotiabankInt').checked) ? 1 : 0;
                if (banorte != 0 || santander != 0 || scotiabank != 0) {
                    const dataSend = { keyGroup: parseInt(paramint), type: "INT", banorte: banorte, santander: santander, scotiabank: scotiabank, banamex: 0, bancomer: 0 };
                    //console.log(dataSend);
                    $.ajax({
                        url: "../Dispersion/SaveBanksDSBank",
                        type: "POST",
                        data: dataSend,
                        beforeSend: () => {
                            btnInterb.disabled = true;
                        }, success: (request) => {
                            //console.log(request);
                            btnInterb.disabled = false;
                            fShowConfigBanksInt(paramint, 'INTERBANCARIO', 'ONE');
                            if (request.MensajeError == "none" && request.Bandera == true) {
                                fShowAlertSuccessSave();
                            }
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    });
                } else {
                    fShowTypeAlert("Atención", "Tienes que seleccionar al menos una opción interbancaria", "info", btnInterb, 1);
                }
            } else {
                alert('Accion invalida');
                location.reload();
            }
        } catch (error) {
            if (error instanceof EvalError) {
                console.error("EvalError: ", error.message);
            } else if (error instanceof RangeError) {
                console.error("RangeError: ", error.message);
            } else if (error instanceof TypeError) {
                console.error("TypeError: ", error.message);
            } else {
                console.error("Error: ", error);
            }
        }
    }

    // Funcion que guarda la configuracion de la dispersion de nomina
    fSaveConfigBankNom = (paramint) => {
        try {
            if (parseInt(paramint) > 0) {
                const btnNomina  = document.getElementById('btn-save-config-bank-nom');
                const banorte    = (document.getElementById('banorteNom').checked)    ? 1 : 0;
                const santander  = (document.getElementById('santanderNom').checked)  ? 1 : 0;
                const scotiabank = (document.getElementById('scotiabankNom').checked) ? 1 : 0;
                const banamex    = (document.getElementById('banamexNom').checked)    ? 1 : 0;
                const bancomer   = (document.getElementById('bancomerNom').checked)   ? 1 : 0;
                if (banorte != 0 || santander != 0 || scotiabank != 0 || banamex != 0 || bancomer != 0) {
                    const dataSend = { keyGroup: parseInt(paramint), type: "NOM", banorte: banorte, santander: santander, scotiabank: scotiabank, banamex: banamex, bancomer: bancomer };
                    console.log(dataSend);
                    $.ajax({
                        url: "../Dispersion/SaveBanksDSBank",
                        type: "POST",
                        data: dataSend,
                        beforeSend: () => {
                            btnNomina.disabled = true;
                        }, success: (request) => {
                            //console.log(request);
                            btnNomina.disabled = false;
                            fShowConfigBanksNom(paramint, 'NOMINA', 'ONE');
                            if (request.MensajeError == "none" && request.Bandera == true) {
                                fShowAlertSuccessSave();
                            }
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    });
                } else {
                    fShowTypeAlert("Atención", "Tienes que seleccionar al menos una opción interbancaria", "info", btnNomina, 1);
                }
            } else {
                alert('Accion invalida');
                location.reload();
            }
        } catch (error) {
            if (error instanceof EvalError) {
                console.error("EvalError: ", error.message);
            } else if (error instanceof RangeError) {
                console.error("RangeError: ", error.message);
            } else if (error instanceof TypeError) {
                console.error("TypeError: ", error.message);
            } else {
                console.error("Error: ", error);
            }
        }
    }

    // Funcion que muestra la ventana de seleccion de bancos de acuerdo a los seleccionados previamente
    fConfigBanksSelected = (paramkeygroup, paramtype, paramselect) => {
        localStorage.removeItem("typeSend");
        let typeSend = "";
        if (paramselect == "INTERBANCARIO") {
            typeSend = "INT";
        } else {
            typeSend = "NOM";
        }
        try {
            if (parseInt(paramkeygroup) > 0) {
                if (paramtype == 1) {
                    $("#viewBanks").modal("hide");
                    setTimeout(() => {
                        $("#configBanksSelected").modal("show");
                    }, 1000);
                }                
                const contentGeneralCInt     = document.getElementById('content-general-config-int');
                const optionsBanks     = document.getElementById('options-banks');
                const banksDefined     = document.getElementById('banks-defined');
                const banksAvailable   = document.getElementById('banks-available');
                banksAvailable.innerHTML = "";
                const btnSConfigDetail = document.getElementById('btn-sconfig-detail');
                btnSConfigDetail.innerHTML = "";
                document.getElementById('nameGInt').textContent = localStorage.getItem("nameGroup");
                localStorage.setItem("typeSend", typeSend);
                $.ajax({
                    url: "../Dispersion/ShowConfigBanks",
                    type: "POST",
                    data: { keyGroup: parseInt(paramkeygroup), type: String(paramselect), option: String('ALL') },
                    beforeSend: () => {

                    }, success: (request) => {
                        //console.log(request);
                        let options     = "<option value='none'>Selecciona</option>";
                        let optionsPaid = "<option value='none'>Selecciona</option>";
                        let disabledBOr = 0;
                        let disabledBDi = 0;
                        let disabledSDi = "";
                        let titleSDi = "";
                        let titleSBd = "";
                        for (let i = 0; i < request.Datos.length; i++) {
                            let data = request.Datos[i];
                            options += `<option value="${data.iIdConfiguracion}">${data.sNombre}</option>`;
                        }
                        for (let j = 0; j < request.Bancos.length; j++) {
                            let data = request.Bancos[j];
                            optionsPaid += `<option value="${data.iIdBanco}">${data.sNombreBanco}</option>`;
                            disabledBOr += 1;
                            disabledBDi += 1;
                        }
                        if (disabledBOr == 0) {
                            localStorage.setItem("disabledBtnSCD", "disabled");
                            disabledSDi = "disabled";
                            titleSDi = "Todos los bancos han sido asignados.";
                            titleSBd = "Inhabilitado por falta de bancos disponibles.";
                        } else {
                            localStorage.removeItem("disabledBtnSCD");
                        }
                        if (paramtype == 2) {
                            selectBankOption.value = "1";
                        } else {
                            selectBankOption.value = "2";
                        }
                        selectBankInt.innerHTML = options;
                        banksAvailable.innerHTML += `
                            <div class="form-group p-2 animated fadeInDown">
                                <label class="col-form-label" for="select-bank-paid">Bancos disponibles:</label>
                                <select title="${titleSDi}" id="select-bank-paid" class="form-control form-control-sm" ${disabledSDi}>
                                    ${optionsPaid}
                                </select>
                            </div>
                        `;
                        btnSConfigDetail.innerHTML += `
                            <div class="form-group p-2">
                                <label class="col-form-label"> Acción: </label>
                                <button title="${titleSBd}" class="btn btn-primary btn-sm btn-block" id="btn-save-cd" onclick="fSaveConfigDetails(${paramkeygroup}, '${paramselect}');"> <i class="fas fa-check-circle mr-2"></i> Guardar </button>
                            </div>
                        `;
                        if (disabledBOr == 0) {
                            document.getElementById("btn-save-cd").disabled = true;
                        } else {
                            document.getElementById("btn-save-cd").disabled = false;
                        }
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
                console.error("EvalError: ", error.message);
            } else if (error instanceof RangeError) {
                console.error("RangeError: ", error.message);
            } else if (error instanceof TypeError) {
                console.error("TypeError: ", error.message);
            } else {
                console.error("Error: ", error);
            }
        }
    }

    // Funcion que guarda el detalla de la configuracion por banco
    fSaveConfigDetails = (paramint, paramstr) => {
        localStorage.removeItem("bankIntConfig");
        let typeSend = "";
        if (paramstr == "INTERBANCARIO") {
            typeSend = "INT";
        } else {
            typeSend = "NOM";
        }
        try {
            const selectBankPaid = document.getElementById('select-bank-paid');
            if (parseInt(paramint) > 0) {
                if (selectBankInt.value != "none") {
                    if (selectBankPaid.value != "none") {
                        localStorage.setItem("bankIntConfig", selectBankInt.value);
                        const dataSend = { keyGroup: parseInt(paramint), type: String(typeSend), configurationId: parseInt(selectBankInt.value), bank: parseInt(selectBankPaid.value) };
                        $.ajax({
                            url: "../Dispersion/SaveConfigDetails",
                            type: "POST",
                            data: dataSend,
                            beforeSend: () => {
                                document.getElementById("btn-save-cd").disabled = true;
                            }, success: (request) => {
                                if (request.Bandera == true && request.MensajeError == "none") {
                                    fConfigBanksSelected(paramint, 0, paramstr);
                                    fShowAlertSuccessSave();
                                    setTimeout(() => {
                                        selectBankInt.value = localStorage.getItem("bankIntConfig");
                                    }, 2500);
                                } else {
                                    fShowTypeAlert("Error!", "Ocurrio un error interno en la aplicación", "error", selectBankPaid, 1);
                                }
                            }, error: (jqXHR, exception) => {
                                fcaptureaerrorsajax(jqXHR, exception);
                            }
                        });
                    } else {
                        fShowTypeAlert("Atención!", "Selecciona un banco disponible", "warning", selectBankPaid, 1);
                    }
                } else {
                    fShowTypeAlert("Atención!", "Selecciona un banco definido", "warning", selectBankInt, 1);
                }
            } else {
                alert('Accion invalida');
                location.reload();
            }
        } catch (error) {
            if (error instanceof EvalError) {
                console.error("EvalError: ", error.message);
            } else if (error instanceof RangeError) {
                console.error("RangeError: ", error.message);
            } else if (error instanceof TypeError) {
                console.error("TypeError: ", error.message);
            } else {
                console.error("Error: ", error);
            }
        }
    }

    fDisabledBtnCloseConfig = (flag) => {
        icoCloseViewCd.disabled = flag;
        btnCloseViewCd.disabled = flag;
    }

    // Funcion que muestra la configuracion de los datos bancarios por banco
    fConfigDataBank = (paramconfig) => {
        contentDCBank.innerHTML = "";
        try {
            if (parseInt(paramconfig) > 0 && paramconfig != "") {
                $.ajax({
                    url: "../Dispersion/ConfigDataBank",
                    type: "POST",
                    data: { keyConfig: parseInt(paramconfig) },
                    beforeSend: () => {
                        fDisabledBtnCloseConfig(true);
                    }, success: (request) => {
                        console.log(request);
                        if (request.Bandera == true && request.MensajeError == "none") {
                            let data = request.Datos;
                            contentDCBank.innerHTML += ` <div class="col-md-12"><hr/></div>
                                <div class="col-md-4 offset-2 animated fadeIn">
                                    <div class="form-group rounded">
                                        <label class="col-form-label" for="nCliente">Numero cliente:</label>
                                        <input class="form-control form-control-sm" type="text" id="nCliente" value="${data.sNumeroCliente}" />
                                    </div>
                                </div>
                                <div class="col-md-4 animated fadeIn">
                                    <div class="form-group rounded">
                                        <label class="col-form-label" for="nCuenta">Numero cuenta:</label>
                                        <input class="form-control form-control-sm" type="text" id="nCuenta" value="${data.sNumeroCuenta}" />
                                    </div>
                                </div>
                                <div class="col-md-4 offset-2 animated fadeIn">
                                    <div class="form-group rounded">
                                        <label class="col-form-label" for="clabe">Clabe:</label>
                                        <input class="form-control form-control-sm" type="text" id="nClabe" value="${data.sClabe}" />
                                    </div>
                                </div>
                                 <div class="col-md-4 animated fadeIn">
                                    <div class="form-group rounded">
                                        <label class="col-form-label" for="plaza">Plaza:</label>
                                        <input class="form-control form-control-sm" type="number" id="nPlaza" value="${data.iPlaza}" />
                                    </div>
                                </div>
                                <div class="col-md-6 offset-3 mt-3 animated fadeInDown">
                                    <div class="form-group rounded">
                                        <button class="btn btn-primary btn-sm btn-block" onclick="fSaveConfigDataBank(${parseInt(paramconfig)});"> <i class="fas fa-check-circle mr-2"></i> Guardar configuracion</button>
                                    </div>
                                </div>
                            `;
                        } else if (request.Bandera == false && request.MensajeError == "NOTFOUND") {
                            fShowTypeAlert("Atencion!", "No se encontro informacion coincidente", "warning", selectBankInt, 1);
                        } else {
                            fShowTypeAlert("Error!", "Ha ocurrido un error interno en la aplicacion", "error", selectBankInt, 1);
                        }
                        fDisabledBtnCloseConfig(false);
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                alert("Acción invalida");
                location.reload();
            }
        } catch (error) {
            if (error instanceof EvalError) {
                console.error("EvalError: ", error.message);
            } else if (error instanceof RangeError) {
                console.error("RangeError: ", error.message);
            } else if (error instanceof TypeError) {
                console.error("TypeError: ", error.message);
            } else {
                console.error("Error: ", error);
            }
        }
    }

    // Funcion que guarda la configuracion bancaria de cada banco 
    fSaveConfigDataBank = (paramconfig) => {
        try {
            if (parseInt(paramconfig) > 0 && paramconfig != "") {
                const nCliente = document.getElementById('nCliente');
                const nCuenta  = document.getElementById('nCuenta');
                const nClabe   = document.getElementById('nClabe');
                const nPlaza = document.getElementById('nPlaza');
                if (nCliente.value != "") {
                    if (nCuenta.value != "") {
                        if (nClabe.value != "") {
                            if (nPlaza.value != "") {
                                const dataSend = {
                                    nClient:   String(nCliente.value),
                                    nAccount:  String(nCuenta.value),
                                    nClabe:    String(nClabe.value),
                                    nSquare:   parseInt(nPlaza.value),
                                    keyConfig: parseInt(paramconfig)
                                };
                                $.ajax({
                                    url: "../Dispersion/SaveConfigDataBank",
                                    type: "POST",
                                    data: dataSend,
                                    beforeSend: () => {

                                    }, success: (request) => {
                                        console.log(request);
                                        if (request.Bandera == true && request.MensajeError == "none") {
                                            fShowAlertSuccessSave()
                                        } else {
                                            fShowTypeAlert("Error!", "Ocurrio un error interno en la aplicación", "error", nCuenta, 1);
                                        }
                                    }, error: (jqXHR, exception) => {
                                        fcaptureaerrorsajax(jqXHR, exception);
                                    }
                                });
                            } else {
                                fShowTypeAlert("Atención!", "Ingresa un nuermo de plaza", "warning", nCuenta, 1);
                            }
                        } else {
                            fShowTypeAlert("Atención!", "Ingresa una clabe", "warning", nClabe, 1);
                        }
                    } else {
                        fShowTypeAlert("Atención!", "Ingresa un nuermo de cuenta", "warning", nCuenta, 1);
                    }
                } else {
                    fShowTypeAlert("Atención!", "Ingresa un numero de cliente", "warning", nCliente, 1);
                }
            } else {
                alert("Acción invalida");
                location.reload();
            }
        } catch (error) {
            if (error instanceof EvalError) {
                console.error("EvalError: ", error.message);
            } else if (error instanceof RangeError) {
                console.error("RangeError: ", error.message);
            } else if (error instanceof TypeError) {
                console.error("TypeError: ", error.message);
            } else {
                console.error("Error: ", error);
            }
        }
    }

    // Funcion que muestra la accion dependiendo la seleccion
    selectBankOption.addEventListener('change', () => {
        const option = selectBankOption.value;
        if (option == 2) {
            if (localStorage.getItem("disabledBtnSCD") != null) {
                document.getElementById('btn-save-cd').disabled = true;
                document.getElementById('select-bank-paid').disabled = true;
            } else {
                document.getElementById('btn-save-cd').disabled = false;
                document.getElementById('select-bank-paid').disabled = false;
            }
        } else if (option == 1) {
            document.getElementById('select-bank-int').focus();
            document.getElementById('select-bank-int').value = "none";
            document.getElementById('btn-save-cd').disabled = true;
            //document.getElementById('select-bank-paid').disabled = true;
            document.getElementById('select-bank-paid').value = "none";
        } else if (option == 3) {
            //if (selectBankInt.value != "none") {
            //    fConfigDataBank(selectBankInt.value);
            //}
            document.getElementById('select-bank-int').focus();
            document.getElementById('select-bank-int').value = "none";
            document.getElementById('btn-save-cd').disabled = true;
            //document.getElementById('select-bank-paid').disabled = true;
            document.getElementById('select-bank-paid').value = "none";
        } else {
            document.getElementById('btn-save-cd').disabled      = true;
            //document.getElementById('select-bank-paid').disabled = true;
            document.getElementById('select-bank-paid').value    = "none";
            document.getElementById('select-bank-int').value     = "none";
        }
        contentDCBank.innerHTML = "";
    });

    // Funcion que elimina el banco del detalle
    fRemoveBankDetail = (paramint, paramconfig, paramgroup) => {
        console.log(paramgroup);
        try {
            if (parseInt(paramint) > 0 && parseInt(paramconfig) > 0) {
                const dataSend = { keyConfigBank: parseInt(paramint), keyDetailConfig: parseInt(paramconfig) };
                $.ajax({
                    url: "../Dispersion/RemoveBankDetail",
                    type: "POST",
                    data: dataSend,
                    beforeSend: () => {
                        console.log('Removiendo');
                    }, success: (request) => {
                        console.log(request);
                        if (request.Bandera == true && request.MensajeError == "none") {
                            let typeSend = localStorage.getItem("typeSend");
                            fShowBankOrigin(paramint, typeSend);
                            fConfigBanksSelected(paramgroup, 2, 'INTERBANCARIO');
                        }
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
                console.error("EvalError: ", error.message);
            } else if (error instanceof RangeError) {
                console.error("RangeError: ", error.message);
            } else if (error instanceof TypeError) {
                console.error("TypeError: ", error.message);
            } else {
                console.error("Error: ", error);
            }
        }
    }

    // Funcion que muestra los bancos configurados a un banco origen
    fShowBankOrigin = (paramint, paramstr) => {
        contentDCBank.innerHTML = "";
        try {
            if (paramint > 0) {
                const badges = ["primary", "secondary", "success", "info", "light", "dark", "warning", "danger"];
                const dataSend = { keyGroup: parseInt(0), type: String(paramstr), option: String('ONE'), configuration: parseInt(paramint) };
                console.log(dataSend);
                $.ajax({
                    url: "../Dispersion/ShowBanksConfigDetails",
                    type: "POST",
                    data: dataSend,
                    beforeSend: () => {
                        console.log('Consultando...');
                    }, success: (request) => {
                        console.log(request);
                        let htmlResult = "";
                        if (request.Bandera == true && request.MensajeError == "none") {
                            htmlResult += "<div class='col-md-10 offset-1 mb-4 animated fadeIn'> <hr/>"
                            for (let i = 0; i < request.Datos.length; i++) {
                                let randomNumeric = Math.floor(Math.random() * (badges.length - 0) + 0);
                                console.log(badges[randomNumeric]);
                                if (randomNumeric == 8) {
                                    randomNumeric = randomNumeric - 1;
                                }
                                htmlResult += `<span class="badge badge-${badges[randomNumeric]} mr-2 p-3 mt-2">${request.Datos[i].sNombreBanco} <i title="Remover" style="cursor:pointer;" class="fas fa-times-circle ml-2 fa-lg" onclick="fRemoveBankDetail(${paramint}, ${request.Datos[i].iConfiguracion}, ${request.Datos[i].iGrupoId})"></i> </span>`;
                            }
                            htmlResult += "</div>";
                            setTimeout(() => {
                                document.getElementById("btn-save-cd").disabled = true;
                            }, 1500);
                        } else {
                            htmlResult += `
                                <div class="alert alert-info alert-dismissible fade show col-md-10 offset-1 text-center mt-3 animated fadeInDown" role="alert">
                                  <strong>No encontramos bancos asignados al banco seleccionado.</strong>
                                </div>
                            `;
                        }
                        contentDCBank.innerHTML = htmlResult;
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
                console.error("EvalError: ", error.message);
            } else if (error instanceof RangeError) {
                console.error("RangeError: ", error.message);
            } else if (error instanceof TypeError) {
                console.error("TypeError: ", error.message);
            } else {
                console.error("Error: ", error);
            }
        }
    }

    selectBankInt.addEventListener('change', () => {
        const option = selectBankOption.value;
        if (option == 1) {
            if (selectBankInt.value != "none") {
                let typeSend = localStorage.getItem("typeSend");
                fShowBankOrigin(selectBankInt.value, typeSend);
            } else {
                contentDCBank.innerHTML = "";
            }
        } else if (option == 3) {
            if (selectBankInt.value != "none") {
                fConfigDataBank(selectBankInt.value);
            }
        } else {
            contentDCBank.innerHTML = "";
        }
    });

    // Funcion que cierra la ventana modal de configuracion
    fCloseViewConfig = () => {
        contentDCBank.innerHTML = "";
        setTimeout(() => {
            $("#viewBanks").modal("show");
        }, 600);
    }

    /*
     * EJECUCION DE FUNCIONES
     */

    btnCloseViewCd.addEventListener('click', fCloseViewConfig);
    icoCloseViewCd.addEventListener('click', fCloseViewConfig);

    // NUEVA CONFIGURACION

    const btnSaveNewBank = document.getElementById('btn-save-new-bank');
    const bankSel = document.getElementById('bankSel');
    const numberCli = document.getElementById('numberCli');
    const plaza = document.getElementById('plaza');
    const numberAccount = document.getElementById('numberAccount');
    const clabe = document.getElementById('clabe');
    const typeDisp = document.getElementById('typeDisp');

    // Funcion que llenna el listado de los bancos
    fLoadBanks = () => {
        try {
            $.ajax({
                url: "../SearchDataCat/LoadBanks",
                type: "POST",
                data: {},
                beforeSend: () => {

                }, success: (data) => {
                    console.log(data);
                    if (data.Bandera == true && data.MensajeError == "none") {
                        for (let i = 0; i < data.Bancos.length; i++) {
                            bankSel.innerHTML += `<option value="${data.Bancos[i].iIdBanco}">${data.Bancos[i].sNombreBanco}</option>`;
                        }
                    } else {
                        alert('No se cargo el listado de bancos');
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
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

    fLoadBanks();

    // Funcion que carga los tipos de dispersion
    fLoadTypeDispersionNew = async () => {
        try {
            await $.ajax({
                url: "../ConfigDataBank/LoadTypeDispersion",
                type: "POST",
                data: {},
                success: (data) => {
                    if (data.Bandera === true && data.MensajeError === "none") {
                        for (let i = 0; i < data.DatosDispersion.length; i++) {
                            var tDis = data.DatosDispersion[i];
                            typeDisp.innerHTML += `<option value="${tDis.iId}">${tDis.sValor}</option>`;
                        }
                    } else {
                        alert('Error al cargar los tipos de dispersion');
                        location.reload();
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        } catch (error) {
            if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }

    fLoadTypeDispersionNew();

    // Funcion que realiza el guardado de la configuracion
    fSaveNewConfigurationBank = () => {
        try {
            if (bankSel.value != "none") {
                if (numberCli.value != "") {
                    if (plaza.value != "") {
                        if (numberAccount.value != "") {
                            if (clabe.value != "") {
                                if (typeDisp.value != "none") {
                                    const dataSend = {
                                        bank: bankSel.value,
                                        numberCli: numberCli.value,
                                        plaza: plaza.value, numberAccount: numberAccount.value, clabe: clabe.value,
                                        typeDisp: typeDisp.value
                                    };
                                    $.ajax({
                                        url: "../SearchDataCat/SaveNewConfigurationBank",
                                        type: "POST",
                                        data: dataSend,
                                        beforeSend: () => {

                                        }, success: (data) => {
                                            console.log(data);
                                            if (data.Bandera == true) {
                                                alert('Registro Correcto!');
                                                location.reload();
                                            } else {
                                                alert('El banco que intenta guardar ya se encuentra registrado, o ocurrio un problema interno');
                                            }
                                        }, error: (jqXHR, exception) => {
                                            fcaptureaerrorsajax(jqXHR, exception);
                                        }
                                    });
                                } else {
                                    alert('Seleccione un tipo de dispersion');
                                }
                            } else {
                                alert('Ingrese la clabe');
                            }
                        } else {
                            alert('Ingrese el numero de cuenta');
                        }
                    } else {
                        alert('Ingrese el numero de plaza');
                    }
                } else {
                    alert('Ingrese el numero de cliente');
                }
            } else {
                alert('Seleccione un banco');
            }
        } catch (e) {
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

    btnSaveNewBank.addEventListener('click', fSaveNewConfigurationBank);

});