$(function () {

    /**
     * Constantes formulario
     */

    const btnClearParamsReports    = document.getElementById('btn-clear-params-reports');
    btnClearParamsReports.disabled = true;

    //const btnContinueReport  = document.getElementById('btnContinueReport');
    const typeReportselect   = document.getElementById('typeReportselect');
    const contentParameters  = document.getElementById('contentParameters');
    const contentBtnGenerate = document.getElementById('contentBtnGenerate');
    const contentGenerateRep = document.getElementById('contentGenerateRep');

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

    const oneRadioBusiness    = document.getElementById('oneRadioBusiness');
    const groupRadioBusiness  = document.getElementById('groupRadioBusiness');
    const selectOneBusiness   = document.getElementById('selectOneBusiness');
    const selectGroupBusiness = document.getElementById('selectGroupBusiness');

    selectOneBusiness.disabled   = true;
    selectGroupBusiness.disabled = true;
    typeReportselect.disabled    = true;

    const nameBusinessGroup     = document.getElementById('nameBusinessGroup');
    const containerBusiness     = document.getElementById('containerBusinessGroup');
    const btnCloseBusinessGroup = document.getElementById('btnCloseBusinessGroup');
    const icoCloseBusinessGroup = document.getElementById('icoCloseBusinessGroup');

    const spanish = {
        "decimal": "",
        "emptyTable": "No hay información",
        "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
        "infoEmpty": "Mostrando 0 t 0 of 0 Entradas",
        "infoFiltered": "(Filtrado de _MAX_ total entradas)",
        "infoPostFix": "",
        "thousands": ",",
        "lengthMenu": "Mostrar _MENU_ Entradas",
        "loadingRecords": "Cargando...",
        "processing": "Procesando...",
        "search": "Buscar:",
        "zeroRecords": "Sin resultados encontrados",
        "paginate": {
            "first": "Primero",
            "last": "Ultimo",
            "next": "Siguiente",
            "previous": "Anterior"
        }
    };

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

    // Funcion que carga los grupos de la empresa en la ventana modal y el select
    floadgroupsbusiness = (type) => {
        try {
            $.ajax({
                url: "../Reportes/LoadGroupBusiness",
                type: "POST",
                data: { stateGrpBusiness: parseInt(0) },
                success: (data) => {
                    if (data.Bandera === true && data.MensajeError === "none") {
                        let lengthData = data.Datos.length;
                        let dataLength = 0;
                        for (let i = 0; i < data.Datos.length; i++) {
                            console.log(data.Datos[i]);
                            if (type == "table") {
                                document.getElementById('data-groupbusiness').innerHTML += `
                                <tr>
                                    <td>${data.Datos[i].sNombreGrupo}</td>
                                    <td>
                                        <button type="button" class="btn btn-success btn-sm btn-icon-split shadow" onclick="fShowBusinessGroup(${data.Datos[i].iIdGrupoEmpresa})">
                                            <span class="icon text-white-50">
                                                <i class="fas fa-eye"></i>
                                            </span>
                                            <span class="text">Ver empresas</span>
                                        </button>
                                    </td>
                                </tr>
                            `;
                                dataLength += 1;
                            } else if (type == "select") {
                                selectGroupBusiness.innerHTML += `<option value="${data.Datos[i].iIdGrupoEmpresa}">${data.Datos[i].sNombreGrupo}</option>`;
                            }
                        }
                        if (type == "table") {
                            if (dataLength == lengthData) {
                                $("#dataTable").DataTable({
                                    language: spanish
                                });
                            }
                        }
                    } else {
                        alert('ERROR AL CARGAR LOS GRUPOS DE EMPRESAS');
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                } 
            });
        } catch (error) {
            if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }

    // Funcion que muestra las empresas en el grupo seleccionado
    fShowBusinessGroup = (paramid) => {
        containerBusiness.innerHTML = "";
        try {
            if (paramid > 0 || paramid != "") {
                $.ajax({
                    url: "../Reportes/BusinessGroup",
                    type: "POST",
                    data: { keyBusinessGroup: parseInt(paramid) },
                    beforeSend: () => {
                        console.log('Cargando...');
                    }, success: (data) => {
                        if (data.Bandera === true && data.MensajeError === "none") {
                            const lengthData = data.Datos.length;
                            let   dataLength = 0;
                            for (let i = 0; i < data.Datos.length; i++) {
                                nameBusinessGroup.textContent = data.Datos[i].sNombreGrupo;
                                containerBusiness.innerHTML += ` <div class="col-md-4 mb-4">
                                    <div class="card border-left-primary shadow h-100 py-2">
                                        <div class="card-body">
                                            <div class="row no-gutters align-items-center">
                                                <div class="col mr-2">
                                                    <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">${data.Datos[i].sRfc}</div>
                                                    <div class="h5 mb-0 font-weight-bold text-gray-800">${data.Datos[i].sNombre_empresa}</div>
                                                </div>
                                                <div class="col-auto">
                                                    <i class="fas fa-building fa-2x text-gray-300"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                `;
                                dataLength += 1;
                            }
                            if (dataLength == lengthData) {
                                $("#searchGroupBusiness").modal("hide");
                                setTimeout(() => { $("#viewBusinessGroup").modal("show"); }, 500);
                            }
                        } else {
                            alert('No se cargaron las empresas del grupo');
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                alert('Accion invalida.');
                location.reload();
            }
        } catch (error) {
            if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }

    // Funcion que limpia los resultados de las empresas en el grupo seleccionado
    fClearResultsGrpBusiness = () => {
        nameBusinessGroup.textContent = '';
        containerBusiness.innerHTML = '';
        $("#viewBusinessGroup").modal('hide');
        setTimeout(() => {
            $("#searchGroupBusiness").modal('show');
        }, 500);
    }

    // Funcion que carga el listado de las empresas
    floadbusiness = (state, type, keyemp, elementid) => {
        try {
            $.ajax({
                url: "../CatalogsTables/Business",
                type: "POST",
                data: { state: state, type: type, keyemp: keyemp },
                success: (data) => {
                    const quantity = data.length;
                    if (quantity > 0) {
                        for (let i = 0; i < data.length; i++) {
                            elementid.innerHTML += `<option value="${data[i].iIdEmpresa}">${data[i].sNombreEmpresa}</option>`;
                        }
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

    // Funcion que se encarga de mostrar el periodo actual \\
    fLoadInfoPeriodPayroll = () => {
        localStorage.removeItem("period");
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
                        localStorage.setItem("period", data.InfoPeriodo.iPeriodo);
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

    // Funcion que habilita el campo de tipo de reporte
    fEnableTypeReport = (typeSelect) => {
        if (typeSelect.value != "none") {
            typeReportselect.disabled = false;
        } else {
            typeReportselect.disabled = true;
            typeReportselect.value    = "0";
            contentBtnGenerate.innerHTML = "";
            contentParameters.innerHTML  = "";
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
                } else if (typeReportselect.value == "BAJA_FEC" || typeReportselect.value == "ALTAEMP") {
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
                //} else if (typeReportselect.value == "ALTAEMP") {
                //    contentParameters.innerHTML += `
                //        <div class="row mt-3 animated fadeInDown">
                //            <div class="col-md-4 offset-2">
                //                <div class="form-group">
                //                    <label class="col-form-label font-labels">Año</label> ${parameterYear}
                //                </div>
                //            </div>
                //            <div class="col-md-4">
                //                <div class="form-group">
                //                    <label class="col-form-label font-labels">Periodo</label> ${parameterPeriod}
                //                </div>
                //            </div>
                //        </div>
                //    `;
                //    const d = new Date();
                //    document.getElementById('paramYear').value = d.getFullYear();
                //    document.getElementById('paramPeriod').value = localStorage.getItem("period");
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
                contentBtnGenerate.innerHTML += `
                    <div class="text-center animated fadeIn delay-1s mt-2 mb-4">
                        <button id="btnGenerateReport" type="button" class="btn btn-primary btn-sm btn-icon-split" onclick="fGenerateReport('${typeReportselect.value}')">
                            <span class="icon text-white-50">
                                <i class="fas fa-play"></i>
                            </span>
                            <span class="text" id="txtBtnGR">Generar Reporte</span>
                        </button>
                    </div>
                `;
                //contentBtnGenerate.innerHTML += `<div class="text-center animated fadeIn delay-1s"><button class="btn btn-primary-new text-white btn-sm" onclick="fGenerateReport('${typeReportselect.value}')">
                //    <i class="fas fa-play mr-2"></i> Generar Reporte
                //</button></div>`;
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

    // Funcion que valida los parametros del reporte
    fGenerateReport = (report) => {
        try {
            if (report != "") {
                if ($('input:radio[name=optionReportGenerate]:checked')) {
                    const valueOptionReport = $("input:radio[name=optionReportGenerate]:checked").val();
                    const reportGenerateOpt = (parseInt(valueOptionReport) === 0) ? "BUSINESS" : "GRPBUSINESS";
                    if (reportGenerateOpt === "BUSINESS" && selectOneBusiness.value == "none") {
                        alert('Selecciona una opcion de Empresa');
                    } else if (reportGenerateOpt === "GRPBUSINESS" && selectGroupBusiness.value == "none") {
                        alert('Selecciona una opcion de Grupos de Empresas');
                    } else {
                        fValidateParametersReports(reportGenerateOpt, report);
                    }
                } else {
                    alert('Seleccione una opcion de generar reporte');
                }
            } else {
                alert('Accion invalida');
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

    // Funcion que comprueba el llenado de los campos del reporte seleccionado
    fValidateParametersReports = async (optionBusiness, typeReport) => {
        try {
            if (optionBusiness != "" && typeReport != "") {
                let keyBusinessOpt;
                if (optionBusiness === "BUSINESS") {
                    keyBusinessOpt = selectOneBusiness.value;
                } else if (optionBusiness === "GRPBUSINESS") {
                    keyBusinessOpt = selectGroupBusiness.value;
                }
                // Validamos que tipo de reporte vamos a realizar
                if (typeReport === "SABANA") {
                    await fGenerateReportPayroll(optionBusiness, keyBusinessOpt);
                } else if (typeReport === "ALTAEMP") {
                    await fGenerateReportEmployeesUp(optionBusiness, keyBusinessOpt);
                } else if (typeReport === "BAJA_FEC") {
                    await fGenerateReportEmployeesDown(optionBusiness, keyBusinessOpt);
                } else if (typeReport === "CAT_EMP_AC") {
                    await fGenerateReportEmployeesActiveWithSalary(optionBusiness, keyBusinessOpt);
                } else if (typeReport === "CATEMPACSS") {
                    await fGenerateReportEmployeesActiveWithoutSalary(optionBusiness, keyBusinessOpt);
                } else if (typeReport === "ABONO" || typeReport === "ABOTOTAL") {
                    await fGenerateReportBillsChecksDetailsTotals(optionBusiness, keyBusinessOpt, typeReport);
                } else {
                    alert('Estamos trabajando en ello...');
                }
            } else {
                alert('Accion invalida');
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
                console.error('Error: ', error);
            }
        }
    }

    // Funcion que desabilita los botones
    fDisabledButtonsRep = () => {
        document.getElementById('txtBtnGR').textContent       = "Generando...";
        document.getElementById('btnGenerateReport').disabled = true;
        document.getElementById('btnGenerateReport').classList.remove('btn-primary');
        document.getElementById('btnGenerateReport').classList.add('btn-info');
        selectOneBusiness.disabled   = true;
        selectGroupBusiness.disabled = true;
        typeReportselect.disabled    = true;
        oneRadioBusiness.disabled    = true;
        groupRadioBusiness.disabled  = true;
    }

    // Funcion que habilita los botones
    fEnabledButtonsRep = () => {
        btnClearParamsReports.disabled = false;
        document.getElementById('txtBtnGR').textContent = "Generar Reporte";
        document.getElementById('btnGenerateReport').disabled = true;
        document.getElementById('btnGenerateReport').classList.remove('btn-info');
        document.getElementById('btnGenerateReport').classList.add('btn-primary');
    }

    // Funcion que muestra que no hay registros encontrados
    fShowContentNoDataReport = (element) => {
        element.innerHTML += `
            <div class="card border-left-warning shadow h-100 py-2 animated fadeIn">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="text-xs font-weight-bold text-warning text-uppercase mb-1">Atención!</div>
                                <div class="row no-gutters align-items-center">
                                    <div class="col">
                                        <p>No se encontraron registros</p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-exclamation-triangle fa-2x text-warning"></i>
                            </div>
                        </div>
                    </div>
                </div>`;
    }

    // Funcion que muestra el apartado de descarga del archivo
    fShowContentDownloadFile = (element, folder, file) => {
        element.innerHTML += `
                <div class="card border-left-success shadow h-100 py-2 animated fadeIn">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="text-xs font-weight-bold text-success text-uppercase mb-1">Completado</div>
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
                                <a href="/Content/Reportes/${folder}/${file}" download="${file}"><i class="fas fa-download fa-2x text-gray-300"></i></a>
                            </div>
                        </div>
                    </div>
                </div>`;
    }

    // Funcion que genera el reporte de la hoja de calculo
    fGenerateReportPayroll = (option, keyOption) => {
        try {
            if (option != "" && parseInt(keyOption) > 0) {
                const period = localStorage.getItem("period");
                $.ajax({
                    url: "../Reportes/ReportPayroll",
                    type: "POST",
                    data: { typeOption: option, keyOptionSel: parseInt(keyOption), periodActually: parseInt(period) },
                    beforeSend: (evt) => {
                        fDisabledButtonsRep();
                    }, success: (data) => {
                        setTimeout(() => {
                            if (data.Bandera === true && data.MensajeError === "none") {
                                if (data.Rows > 0) {
                                    fShowContentDownloadFile(contentGenerateRep, data.Folder, data.Archivo);
                                } else {
                                    fShowContentNoDataReport(contentGenerateRep);
                                }
                            } else {
                                alert('Algo fallo al realizar el reporte');
                                location.reload();
                            }
                            fEnabledButtonsRep();
                        }, 2000);
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                alert('Accion invalida');
                location.reload();
            }
        } catch (error) {
            if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }

    // Funcion que genera el reporte de las altas de empleados en un periodo
    fGenerateReportEmployeesUp = (option, keyOption) => {
        try {
            if (option != "" && parseInt(keyOption) > 0) {
                const paramDateS = document.getElementById('paramDateS');
                const paramDateE = document.getElementById('paramDateE');
                if (paramDateS.value != "") {
                    if (paramDateE.value != "") {
                        $.ajax({
                            url: "../Reportes/ReportEmployeesUp",
                            type: "POST",
                            data: { typeOption: option, keyOptionSel: parseInt(keyOption), dateS: paramDateS.value, dateE: paramDateE.value },
                            beforeSend: () => {
                                fDisabledButtonsRep();
                            }, success: (data) => {
                                setTimeout(() => {
                                    if (data.Bandera === true && data.MensajeError === "none") {
                                        if (data.Rows > 0) {
                                            fShowContentDownloadFile(contentGenerateRep, data.Folder, data.Archivo);
                                        } else {
                                            fShowContentNoDataReport(contentGenerateRep);
                                        }
                                    } else {
                                        alert('Algo fallo al realizar el reporte');
                                        location.reload();
                                    }
                                    fEnabledButtonsRep();
                                }, 2000);
                            }, error: (jqXHR, exception) => {
                                fcaptureaerrorsajax(jqXHR, exception);
                            }
                        });
                    } else {
                        fShowTypeAlert('Atención', 'Complete el campo Fecha final', 'warning', paramDateE, 2);
                    }
                } else {
                    fShowTypeAlert('Atención', 'Complete el campo Fecha inicio', 'warning', paramDateS, 2);
                }
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

    // Funcion que genera el reporte de bajas de los empleados por rango de fechas
    fGenerateReportEmployeesDown = (option, keyOption) => {
        try {
            if (option != "" && parseInt(keyOption) > 0) {
                const paramDateS = document.getElementById('paramDateS');
                const paramDateE = document.getElementById('paramDateE');
                if (paramDateS.value != "") {
                    if (paramDateE.value != "") {
                        $.ajax({
                            url: "../Reportes/ReportEmployeesDown",
                            type: "POST",
                            data: { typeOption: option, keyOptionSel: parseInt(keyOption), dateS: paramDateS.value, dateE: paramDateE.value },
                            beforeSend: () => {
                                fDisabledButtonsRep();
                            }, success: (data) => {
                                setTimeout(() => {
                                    if (data.Bandera === true && data.MensajeError === "none") {
                                        if (data.Rows > 0) {
                                            fShowContentDownloadFile(contentGenerateRep, data.Folder, data.Archivo);
                                        } else {
                                            fShowContentNoDataReport(contentGenerateRep);
                                        }
                                    } else {
                                        alert('Algo fallo al realizar el reporte');
                                        location.reload();
                                    }
                                    fEnabledButtonsRep();
                                }, 2000);
                            }, error: (jqXHR, exception) => {
                                fcaptureaerrorsajax(jqXHR, exception);
                            }
                        });
                    } else {
                        fShowTypeAlert('Atención', 'Complete el campo Fecha final', 'warning', paramDateE, 2);
                    }
                } else {
                    fShowTypeAlert('Atención', 'Complete el campo Fecha inicio', 'warning', paramDateS, 2);
                }
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

    // Funcion que genera el reporte de empleados activos con sueldo
    fGenerateReportEmployeesActiveWithSalary = (option, keyOption) => {
        try {
            if (option != "" && parseInt(keyOption) > 0) {
                const paramDate = document.getElementById('paramDate');
                if (paramDate.value != "") {
                    $.ajax({
                        url: "../Reportes/ReportEmployeesActiveWithSalary",
                        type: "POST",
                        data: { typeOption: option, keyOptionSel: parseInt(keyOption), dateActive: paramDate.value },
                        beforeSend: () => {
                            fDisabledButtonsRep();
                        }, success: (data) => {
                            if (data.Bandera === true && data.MensajeError === "none") {
                                if (data.Rows > 0) {
                                    fShowContentDownloadFile(contentGenerateRep, data.Folder, data.Archivo);
                                } else {
                                    fShowContentNoDataReport(contentGenerateRep);
                                }
                            } else {
                                alert('Algo fallo al realizar el reporte');
                                location.reload();
                            }
                            fEnabledButtonsRep();
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    });
                } else {
                    fShowTypeAlert('Atención', 'Complete el campo Fecha del personal activo', 'warning', paramDate, 2);
                }
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

    // Funcion que genera el reporte de empleados activos sin sueldo
    fGenerateReportEmployeesActiveWithoutSalary = (option, keyOption) => {
        try {
            if (option != "" && parseInt(keyOption) > 0) {
                const paramDate = document.getElementById('paramDate');
                if (paramDate.value != "") {
                    $.ajax({
                        url: "../Reportes/ReportEmployeesActiveWithoutSalary",
                        type: "POST",
                        data: { typeOption: option, keyOptionSel: parseInt(keyOption), dateActive: paramDate.value },
                        beforeSend: () => {
                            fDisabledButtonsRep();
                        }, success: (data) => {
                            if (data.Bandera === true && data.MensajeError === "none") {
                                if (data.Rows > 0) {
                                    fShowContentDownloadFile(contentGenerateRep, data.Folder, data.Archivo);
                                } else {
                                    fShowContentNoDataReport(contentGenerateRep);
                                }
                            } else {
                                alert('Algo fallo al realizar el reporte');
                                location.reload();
                            }
                            fEnabledButtonsRep();
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    });
                } else {
                    fShowTypeAlert('Atención!', 'Complete el campo Fecha del personal activo', 'warning', paramDate, 2);
                }
            } else {
                alert('Accion invalida');
                location.reload();
            }
        } catch (error) {
            if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }

    // Funcion que genera el reporte de abonos a cuenta de cheques detalle
    fGenerateReportBillsChecksDetailsTotals = (option, keyOption, typeRep) => {
        try {
            if (option != "" && parseInt(keyOption) > 0) {
                const urlTReport = (typeRep === "ABONO") ? "ReportBillsChecksDetails" : "ReportBillsChecksTotals";
                const paramYear  = document.getElementById('paramYear');
                const paramNper  = document.getElementById('paramNper');
                const paramTper = document.getElementById('paramTper');
                if (paramYear.value != "" && paramYear.value > 0 && paramYear.value.length === 4) {
                    if (paramNper.value != "" && paramNper.value > 0) {
                        if (paramTper.value != "" && paramTper.value > 0) {
                            $.ajax({
                                url: "../Reportes/ReportBillsChecksDetailsTotals",
                                type: "POST",
                                data: {
                                    typeOption: option, keyOptionSel: parseInt(keyOption), typeReport: typeRep,
                                    yearSelect: paramYear.value, periodSelect: paramNper.value, typePSelect: paramTper.value
                                },
                                beforeSend: () => {
                                    fDisabledButtonsRep();
                                }, success: (data) => {
                                    if (data.Bandera === true && data.MensajeError === "none") {
                                        if (data.Rows > 0) {
                                            fShowContentDownloadFile(contentGenerateRep, data.Folder, data.Archivo);
                                        } else {
                                            fShowContentNoDataReport(contentGenerateRep);
                                        }
                                    } else {
                                        alert('Algo fallo al realizar el reporte');
                                        location.reload();
                                    }
                                    console.log(data);
                                    fEnabledButtonsRep();
                                }, error: (jqXHR, exception) => {
                                    fcaptureaerrorsajax(jqXHR, exception);
                                }
                            });
                        } else {
                            fShowTypeAlert('Atención!', 'Complete el campo tipo periodo correctamente', 'warning', paramTper, 2);
                        }
                    } else {
                        fShowTypeAlert('Atención!', 'Complete el campo periodo correctamente', 'warning', paramNper, 2);
                    }
                } else {
                    fShowTypeAlert('Atención!', 'Complete el campo año correctamente', 'warning', paramYear, 2);
                }
            } else {
                alert('Accion invalida');
                location.reload();
            }
        } catch (error) {
            if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }

    // Funcion que limpia la parametrizacion del formulario de reportes
    fClearParamsReports = () => {
        contentGenerateRep.innerHTML = "";
        contentBtnGenerate.innerHTML = "";
        selectOneBusiness.disabled   = true;
        selectOneBusiness.value      = "none";
        selectGroupBusiness.disabled = true;
        selectGroupBusiness.value    = "none";
        typeReportselect.disabled    = true;
        typeReportselect.value       = "0";
        document.querySelectorAll('[name=optionReportGenerate]').forEach((x) => x.checked = false);
        btnClearParamsReports.disabled = true;
        oneRadioBusiness.disabled      = false;
        groupRadioBusiness.disabled    = false;
        contentParameters.innerHTML = "";
        $("html, body").animate({ scrollTop: $("#nav-reporte").offset().top - 50 }, 1000);
    }

    /*
     * Ejecucion de funciones
     */

    fLoadInfoPeriodPayroll();

    oneRadioBusiness.addEventListener('click', () => {
        selectOneBusiness.disabled   = false;
        selectGroupBusiness.disabled = true;
        selectGroupBusiness.value    = "none";
        typeReportselect.disabled    = true;
        typeReportselect.value       = "0";
        contentBtnGenerate.innerHTML = "";
        contentParameters.innerHTML  = "";
    });

    groupRadioBusiness.addEventListener('click', () => {
        selectGroupBusiness.disabled = false;
        selectOneBusiness.disabled   = true;
        selectOneBusiness.value      = "none";
        typeReportselect.disabled    = true;
        typeReportselect.value       = "0";
        contentBtnGenerate.innerHTML = "";
        contentParameters.innerHTML  = "";
    });

    selectOneBusiness.addEventListener('change', () => {
        fEnableTypeReport(selectOneBusiness);
    });

    selectGroupBusiness.addEventListener('change', () => {
        fEnableTypeReport(selectGroupBusiness)
    });

    floadbusiness(0, 'Active/Desactive', 0, selectOneBusiness);
    floadgroupsbusiness("table");
    floadgroupsbusiness("select");

    btnCloseBusinessGroup.addEventListener('click', fClearResultsGrpBusiness);
    icoCloseBusinessGroup.addEventListener('click', fClearResultsGrpBusiness);
    
    typeReportselect.addEventListener('change', fShowParametersRequired);

    btnClearParamsReports.addEventListener('click', fClearParamsReports);
    //btnContinueReport.addEventListener('click', fShowParametersRequired);

});