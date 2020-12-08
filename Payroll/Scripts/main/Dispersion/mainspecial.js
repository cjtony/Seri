$(function () {

    /*
     * Constantes
    */

    const yeardisS    = document.getElementById('yeardis1');
    const typeperiodS = document.getElementById('typeperiod1');
    const periodisS   = document.getElementById('periodis1');
    const datedisS    = document.getElementById('datedis1');

    /*
     * Funciones
    */

    const fCatchError = (error) => {
        let msj = "";
        if (error instanceof EvalError) {
            msj = `EvalError: ${error.message}`;
        } else if (error instanceof TypeError) {
            msj = `TypeError: ${error.message}`;
        } else if (error instanceof RangeError) {
            msj = `RangeError: ${error.message}`;
        } else {
            msj = `Error: ${error}`;
        }
        return msj;
    }

    fnSendDataDispersionSpecial = () => {
        try {
            const optionGroup    = document.getElementById('option-group');
            const typeDispersion = document.getElementById('type-dispersion');
            if (optionGroup.value != "none") {
                if (typeDispersion.value != "none") {
                    const dataSend = {
                        group: parseInt(optionGroup.value),
                        type: String(typeDispersion.value),
                        yearPeriod: parseInt(yeardisS.value),
                        numberPeriod: parseInt(periodisS.value),
                        typePeriod: parseInt(typeperiodS.value),
                        dateDeposits: datedisS.value,
                        mirror: 0
                    };
                    console.log(dataSend);
                    $.ajax({
                        url: "../DispersionSpecial/DispersionSpecialInit",
                        type: "POST",
                        data: dataSend,
                        beforeSend: () => {
                            document.getElementById('btn-send-dispersion-special').disabled = true;
                            document.getElementById('btn-send-report-ds').disabled          = true;
                            document.getElementById('option-group').disabled    = true;
                            document.getElementById('type-dispersion').disabled = true;
                            document.getElementById('btn-send-dispersion-special').disabled = true;
                            document.getElementById('ismirrorspecial').disabled = true;
                            document.getElementById('div-show-alert-loading1').innerHTML = `
                                <div class="text-center">
                                    <div class="spinner-grow text-info" role="status">
                                        <span class="sr-only">Loading...</span>
                                    </div>
                                    <h6 class="font-weight-bold text-info">Generando...</h6>
                                </div>
                            `;
                        }, success: (request) => {
                            document.getElementById('div-show-alert-loading1').innerHTML = '';
                            console.log(request);
                            if (request.Bandera == true) {
                                document.getElementById('divbtndownzip1').innerHTML += `
                                    <div class="card border-left-success shadow h-100 py-2 animated fadeInRight delay-2s">
                                        <div class="card-body">
                                            <div class="row no-gutters align-items-center">
                                                <div class="col mr-2">
                                                    <div class="text-xs font-weight-bold text-success text-uppercase mb-1">${request.Zip}.zip</div>
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col-auto">
                                                            <div class="h5 mb-0 mr-3 font-weight-bold text-gray-800">100%</div>
                                                        </div>
                                                        <div class="col">
                                                            <div class="progress progress-sm mr-2">
                                                                <div class="progress-bar bg-success" role="progressbar" style="width: 100%" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-auto">
                                                    <a title="Descargar archivo ${request.Zip}.zip" id="btn-down-txt" download="${request.Zip}.zip" href="/DispersionZIP/${request.Anio}/${request.Carpeta}/${request.Zip}.zip" ><i class="fas fa-download fa-2x text-gray-300"></i></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>`;
                                document.getElementById('div-controls1').innerHTML += `
                                    <div class="animated fadeInDown delay-1s">
                                        <h6 class="text-primary font-weight-bold"> <i class="fas fa-check-circle mr-2"></i> Depositos ${request.Label} generados!</h6>
                                        <hr />
                                        <button id="btn-restart-to-deploy-special" class="btn btn-sm btn-primary" type="button" onclick="fRestartToDeploySpecial('${request.Zip}',${request.Anio}, 'NOM');"> <i class="fas fa-undo mr-2"></i> Activar botones </button>
                                    </div>
                                `;
                            } else {
                                fShowTypeAlert('Atención!', 'No se encontraron depositos', 'warning', document.getElementById('btn-send-dispersion-special'), 0);
                                document.getElementById('btn-send-dispersion-special').disabled = false;
                            }
                        }, error: (jqXHR, exception) => {
                            
                        }
                    });
                } else {
                    fShowTypeAlert('Atención!', 'Selecciona un tipo de dispersion', 'warning', typeDispersion, 0);
                }
            } else {
                fShowTypeAlert('Atención!', 'Selecciona un grupo', 'warning', optionGroup, 0);
            }
        } catch (error) {
            console.log(fCatchError(error));
        }
    }

    fRestartToDeploySpecial = (paramzip, paramyear, paramtype) => {
        try {
            if (String(paramzip) != "" && String(paramtype) != "" && String(paramzip).length > 0 && String(paramtype).length > 0) {
                if (parseInt(paramyear) != 0 && String(paramyear).length == 4) {
                    const btnRestartToDeploy = document.getElementById('btn-restart-to-deploy-special');
                    const dataSend = {
                        paramNameFile: String(paramzip),
                        paramYear: parseInt(paramyear),
                        paramCode: String(paramtype)
                    };
                    $.ajax({
                        url: "../DispersionSpecial/RestartToDeploySpecial",
                        type: "POST",
                        data: dataSend,
                        beforeSend: () => {
                            btnRestartToDeploy.disabled = true;
                        }, success: (data) => {
                            document.getElementById('ismirrorspecial').disabled             = false;
                            document.getElementById('btn-send-dispersion-special').disabled = false;
                            document.getElementById('option-group').value          = "none";
                            document.getElementById('option-group').disabled       = false;
                            document.getElementById('type-dispersion').value       = "none";
                            document.getElementById('type-dispersion').disabled    = false;
                            document.getElementById('divbtndownzip1').innerHTML    = "";
                            document.getElementById('div-controls1').innerHTML     = "";
                            document.getElementById('divbtndownzipint1').innerHTML = "";
                            document.getElementById('div-controls-int1').innerHTML = "";
                            document.getElementById('btn-send-report-ds').disabled = false;
                            //$("html, body").animate({ scrollTop: $('#btn-desplegar-especial-tab').offset().top - 50 }, 1000);
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    })
                } else {
                    alert('Accion invalida');
                    location.reload();
                }
            } else {
                alert('Accion invalida');
                location.reload();
            }
        } catch (error) {
            console.log(fCatchError(error));
        }
    }

    // Funcion que genera un reporte
    fnSendReportDs = () => {
        try {
            document.getElementById('divbtndownzip1').innerHTML = "";
            document.getElementById('div-controls1').innerHTML  = "";
            const optionGroup     = document.getElementById('option-group');
            const typeDispersion  = document.getElementById('type-dispersion');
            const ismirrorspecial = document.getElementById('ismirrorspecial');
            let isMirrorSend = 0;
            if (ismirrorspecial.checked) {
                isMirrorSend = 1;
            }
            if (optionGroup.value != "none") {
                if (typeDispersion.value != "none") {
                    const dataSend = {
                        group: parseInt(optionGroup.value),
                        type: String(typeDispersion.value),
                        yearPeriod: parseInt(yeardisS.value),
                        numberPeriod: parseInt(periodisS.value),
                        typePeriod: parseInt(typeperiodS.value),
                        dateDeposits: datedisS.value,
                        mirror: isMirrorSend
                    };
                    console.log(dataSend);
                    $.ajax({
                        url: "../DispersionSpecial/ReporteDs",
                        type: "POST",
                        data: dataSend,
                        beforeSend: () => {
                            document.getElementById('btn-send-dispersion-special').disabled = true;
                            document.getElementById('btn-send-report-ds').disabled = true;
                            document.getElementById('option-group').disabled       = true;
                            document.getElementById('type-dispersion').disabled    = true;
                            ismirrorspecial.disabled = true;
                        }, success: (request) => {
                            console.log(request);
                            //document.getElementById('btn-send-dispersion-special').disabled = false;
                            //document.getElementById('btn-send-report-ds').disabled = false;
                            //document.getElementById('option-group').disabled       = false;
                            //document.getElementById('type-dispersion').disabled    = false;
                            if (request.Bandera == true && request.MensajeError == "none") {
                                document.getElementById('divbtndownzip1').innerHTML += `
                                    <div class="card border-left-success shadow h-100 py-2 animated fadeInRight delay-2s">
                                        <div class="card-body">
                                            <div class="row no-gutters align-items-center">
                                                <div class="col mr-2">
                                                    <div class="text-xs font-weight-bold text-success text-uppercase mb-1">${request.Archivo}.zip</div>
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col-auto">
                                                            <div class="h5 mb-0 mr-3 font-weight-bold text-gray-800">100%</div>
                                                        </div>
                                                        <div class="col">
                                                            <div class="progress progress-sm mr-2">
                                                                <div class="progress-bar bg-success" role="progressbar" style="width: 100%" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-auto">
                                                    <a title="Descargar archivo ${request.Archivo}.xlsx" id="btn-down-txt" download="${request.Archivo}" href="/Content/DISPERSION/${request.Folder}/${request.Archivo}" ><i class="fas fa-download fa-2x text-gray-300"></i></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>`;
                                document.getElementById('div-controls1').innerHTML += `
                                    <div class="animated fadeInDown delay-1s">
                                        <h6 class="text-primary font-weight-bold"> <i class="fas fa-check-circle mr-2"></i> Archivo generado!</h6>
                                        <hr />
                                        <button id="btn-restart-to-deploy-special" class="btn btn-sm btn-primary" type="button" onclick="fRestartReportFile('${request.Folder}', '${request.Archivo}');"> <i class="fas fa-undo mr-2"></i> Activar botones </button>
                                    </div>
                                `;
                            } else {
                                fShowTypeAlert('Error!', 'Ocurrio un error interno en la aplicacion', 'error', document.getElementById('btn-send-dispersion-special'), 0);
                            }
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    });
                } else {
                    fShowTypeAlert('Atención!', 'Selecciona un tipo de dispersion', 'warning', typeDispersion, 0);
                }
            } else {
                fShowTypeAlert('Atención!', 'Selecciona un grupo', 'warning', optionGroup, 0);
            }
        } catch (error) {
            console.log(fCatchError(error));
        }
    }

    fRestartReportFile = (paramFolder, paramFile) => {
        try {
            if (paramFolder != "" && paramFile != "") {
                $.ajax({
                    url: "../DispersionSpecial/RestartReportFile",
                    type: "POST",
                    data: { folder: String(paramFolder), file: String(paramFile) },
                    beforeSend: () => {
                        document.getElementById('btn-send-dispersion-special').disabled = true;
                        document.getElementById('btn-send-report-ds').disabled = true;
                        document.getElementById('option-group').disabled       = true;
                        document.getElementById('type-dispersion').disabled    = true;
                    }, success: (request) => {
                        console.log(request);
                        document.getElementById('btn-send-dispersion-special').disabled = false;
                        document.getElementById('btn-send-report-ds').disabled          = false;
                        document.getElementById('option-group').disabled    = false;
                        document.getElementById('type-dispersion').disabled = false;
                        document.getElementById('ismirrorspecial').checked  = 0;
                        document.getElementById('option-group').value       = "none";
                        document.getElementById('type-dispersion').value    = "none";
                        document.getElementById('divbtndownzip1').innerHTML = "";
                        document.getElementById('div-controls1').innerHTML  = "";
                        document.getElementById('divbtndownzipint1').innerHTML = "";
                        document.getElementById('div-controls-int1').innerHTML = "";
                        document.getElementById('ismirrorspecial').disabled    = false;
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                alert('Accion invalida');
                location.reload();
            }
        } catch (error) {
            console.log(fCatchError(error));
        }
    }

    /*
     * Ejecucion fn
    */
    
});