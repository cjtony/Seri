$(function () {

    /**
     * Constantes formulario
     */

    //const btnContinueReport  = document.getElementById('btnContinueReport');
    const typeReportselect   = document.getElementById('typeReportselect');
    const contentParameters  = document.getElementById('contentParameters');
    const contentBtnGenerate = document.getElementById('contentBtnGenerate');

    const parameterYear   = `<input type="number" class="form-control form-control-sm" id="paramYear"/>`;
    const parameterNPer   = `<input type="number" class="form-control form-control-sm" id="paramNper"/>`;
    const parameterTPer   = '<input type="number" class="form-control form-control-sm" id="paramTper"/>';
    const parameterDate   = '<input type="date" class="form-control form-control-sm" id="paramDate"/>';
    const parameterYears  = `<input type="number" class="form-control form-control-sm" id="paramYearS"/>`;
    const parameterYearE  = `<input type="number" class="form-control form-control-sm" id="paramYearE"/>`;
    const parameterNReng  = `<input type="number" class="form-control form-control-sm" id="paramNReng"/>`;
    const parameterTEmpl  = `<input type="text" class="form-control form-control-sm" id="paramTEmpl"/>`;
    const parameterDateS  = '<input type="date" class="form-control form-control-sm" id="paramDateS"/>';
    const parameterDateE  = '<input type="date" class="form-control form-control-sm" id="paramDateE"/>';
    const parameterPStart = `<input type="number" class="form-control form-control-sm" id="paramPStart"/>`;
    const parameterPEnd   = `<input type="number" class="form-control form-control-sm" id="paramPEnd"/>`;
    const parameterPeriod = `<input type="number" class="form-control form-control-sm" id="paramPeriod"/>`;
    const parameterNPeriods = `<input type="number" class="form-control form-control-sm" id="paramNPeriods"/>`;
    const parameterNEmploye = `<input type="number" class="form-control form-control-sm" id="paramNPeriods"/>`;

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

    // Funcion que se encarga de mostrar el periodo actual \\
    fLoadInfoPeriodPayroll = () => {
        try {
            const d = new Date(), yearact = d.getFullYear();
            $.ajax({
                url: "../Dispersion/LoadInfoPeriodPayroll",
                type: "POST",
                data: { yearAct: yearact },
                success: (data) => {
                    if (data.Bandera == true && data.MensajeError == "none") {
                        document.getElementById('typeperactnomemp').textContent = data.InfoPeriodo.iTipoPeriodo;
                        document.getElementById('peractnomemp').textContent     = data.InfoPeriodo.iPeriodo;
                        document.getElementById('fechaspernomemp').textContent  = data.InfoPeriodo.sFechaInicio + " - " + data.InfoPeriodo.sFechaFinal;
                        //periodis.value = data.InfoPeriodo.iPeriodo;
                    } else {
                        fShowTypeAlert('Atención!', 'No se ha cargado la informacion del periodo actual, contacte al área de TI indicando el siguiente código: #CODERRfLoadInfoPeriodPayrollMAINREP#', 'error', navDispersion, 0);
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        } catch (error) {
            if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }


    // Funcion que muestra los parametros a insertar dependiendo el tipo de reporte
    fShowParametersRequired = () => {
        contentParameters.innerHTML  = "";
        contentBtnGenerate.innerHTML = "";
        try {
            let btnDisabled = "";
            if (typeReportselect.value != 0) {
                $("html, body").animate({ scrollTop: $(`#${contentParameters.id}`).offset().top - 50 }, 1000);
                if (typeReportselect.value == "ABONO" || typeReportselect.value == "ABOTOTAL" || typeReportselect.value == "TOTACUMS") {
                    contentParameters.innerHTML += `
                    <div class="row mt-3 animated fadeInDown">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="col-form-label font-labels"> Año </label> ${parameterYear} 
                            </div> 
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="col-form-label font-labels"> Numero periodo </label> ${parameterNPer} 
                            </div> 
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="col-form-label font-labels"> Tipo periodo </label> ${parameterTPer} 
                            </div> 
                        </div>
                    </div>
                    `;
                } else if (typeReportselect.value == "ESTRUCTURA" || typeReportselect.value == "CAT_EMP_AC" || typeReportselect.value == "CATEMPACSS") {
                    contentParameters.innerHTML += `
                        <div class="row mt-3 animated fadeInDown">
                            <div class="col-md-6 offset-3">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">DIGITE FECHA HASTA DEL PERSONAL ACTIVO. </label> ${parameterDate}
                                </div>
                            </div>
                        </div>
                    `;
                } else if (typeReportselect.value == "RECIRENG" || typeReportselect.value == "RECIRENG_") {
                    contentParameters.innerHTML += `
                        <div class="row mt-3 animated fadeInDown">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Año inicio</label> ${parameterYears}
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Año final</label> ${parameterYearE}
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Tipo periodo</label> ${parameterTPer}
                                </div>
                            </div>
                            <div class="col-md-4 offset-2">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Numero renglones</label> ${parameterNReng}
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Tipo de empleados</label> ${parameterTEmpl}
                                </div>
                            </div>
                        </div>
                    `;
                } else if (typeReportselect.value == "BAJA_FEC") {
                    contentParameters.innerHTML += `
                        <div class="row mt-3 animated fadeInDown">
                            <div class="col-md-4 offset-2">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Fecha inicio</label> ${parameterDateS}
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Fecha final</label> ${parameterDateE}
                                </div>
                            </div>
                        </div>
                    `;
                } else if (typeReportselect.value == "ACUMATRIX" || typeReportselect.value == "RECMATRIX" || typeReportselect.value == "RECINOMI") {
                    contentParameters.innerHTML += `
                        <div class="row mt-3 animated fadeInDown">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Año</label> ${parameterYear}
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Periodo inicio</label> ${parameterPStart}
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Periodo final</label> ${parameterPEnd}
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Tipo periodo</label> ${parameterTPer}
                                </div>
                            </div>
                        </div>
                    `;
                } else if (typeReportselect.value == "ALTAEMP") {
                    contentParameters.innerHTML += `
                        <div class="row mt-3 animated fadeInDown">
                            <div class="col-md-4 offset-2">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Año</label> ${parameterYear}
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Periodo</label> ${parameterPeriod}
                                </div>
                            </div>
                        </div>
                    `;
                } else if (typeReportselect.value == "ACUM_NOM") {
                    contentParameters.innerHTML += `
                        <div class="row mt-3 animated fadeInDown">
                            <div class="col-md-4 offset-2">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Año</label> ${parameterYear}
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Numero periodos</label> ${parameterNPeriods}
                                </div>
                            </div>
                            <div class="col-md-4 offset-2">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Numero de empleado</label> ${parameterNEmploye}
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="col-form-label font-labels">Tipo periodo</label> ${parameterTPer}
                                </div>
                            </div>
                        </div>
                    `;
                }
                contentBtnGenerate.innerHTML += `<div class="text-center animated fadeIn delay-1s"><button class="btn btn-outline-primary btn-sm" onclick="fGenerateReport('${typeReportselect.value}')">
                    <i class="fas fa-play mr-2"></i> Generar Reporte
                </button></div>`;
            } else {
                alert('Selecciona un tipo de reporte');
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

    // Funcion que genera el reporte
    fGenerateReport = (report) => {
        try {
            console.log(report);
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

    fLoadInfoPeriodPayroll();
    
    typeReportselect.addEventListener('change', fShowParametersRequired);
    //btnContinueReport.addEventListener('click', fShowParametersRequired);

});