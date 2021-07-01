$(function () {

	/*
	 * Constantes dispersion
	 */

    const navDispersion            = document.getElementById('nav-dispersion');
    const containerDataDeploy      = document.getElementById('container-data-deploy');
    const tableDataDeposits        = document.getElementById('table-data-deposits');
    const tableDataDepositsSpecial = document.getElementById('table-data-deposits-especial');
    const alertDataDeposits        = document.getElementById('alert-data-deposits');
    const alertDataDepositsSpecial = document.getElementById('alert-data-deposits-special');
    const containerBtnsProDepBank  = document.getElementById('container-btns-process-deposits-bank');
    const containerBtnsProDepBankSpecial = document.getElementById('container-btns-process-deposits-bank-special');
    const btndesplegartab         = document.getElementById('btn-desplegar-tab');
    const btndesplegarespecialtab = document.getElementById('btn-desplegar-especial-tab');
    const btnretnominaemp         = document.getElementById('btn-ret-nomina-employe');
    const searchemployekeynom     = document.getElementById('searchemployekeynom');
    const resultemployekeynom     = document.getElementById('resultemployekeynom');
    const icoclosesearchempnomret = document.getElementById('ico-close-searchemployesnom-btn');
    const btnclosesearchempnomret = document.getElementById('btn-close-searchemployesnom-btn');
    const btnregisterretnomina    = document.getElementById('btn-regiser-retnomina');
    const filtronamenom           = document.getElementById('filtronamenom');
    const filtronumbernom         = document.getElementById('filtronumbernom');
    const labsearchempnom         = document.getElementById('labsearchempnom');

    const yeardis  = document.getElementById('yeardis');
    const typeperiod = document.getElementById('typeperiod');
    const periodis = document.getElementById('periodis');
    const datedis = document.getElementById('datedis');


    const yeardis1    = document.getElementById('yeardis1');
    const typeperiod1 = document.getElementById('typeperiod1');
    const periodis1   = document.getElementById('periodis1');
    const datedis1 = document.getElementById('datedis1');

    const optionGroup = document.getElementById('option-group');

    //const isMirror = document.getElementById('ismirror');

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

	// Funcion que muestra alertas dinamicamente \\
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

    // Funcion que muestra los periodos donde hay nominas retenidas en la empresa
    //fLoadPeriodsRetainetPayroll = () => {
    //    try {
    //        $.ajax({
    //            url: "../Dispersion/LoadPeriodsRetainedPayroll",
    //            type: "POST",
    //            data: { Anio: 0 },
    //            success: (data) => {
    //                console.log('Periodos');
    //                console.log(data);
    //                for (let i = 0; i < data.Periodos.length; i++) {
    //                    if (data.Periodos[i] != data.InfoPeriodo.iPeriodo) {
    //                        document.getElementById('periodosRetenidos').innerHTML += `<option value="${data.Periodos[i]}">${data.Periodos[i]}</option>`;
    //                    }
    //                }
    //                document.getElementById('periodosRetenidos').innerHTML += `<option value="${data.InfoPeriodo.iPeriodo}">${data.InfoPeriodo.iPeriodo}</option>`;
    //                document.getElementById('anioRetenido').value = data.InfoPeriodo.iAnio;
    //                setTimeout(() => {
    //                    document.getElementById('periodosRetenidos').value = data.InfoPeriodo.iPeriodo;
    //                }, 1000);
    //            }, error: (jqXHR, exception) => {
    //                fcaptureaerrorsajax(jqXHR, exception);
    //            }
    //        });
    //    } catch (error) {
    //        if (error instanceof EvalError) {
    //            console.log('EvalError ', error);
    //        } else if (error instanceof RangeError) {
    //            console.log('RangeError ', error);
    //        } else if (error instanceof TypeError) {
    //            console.log('TypeError ', error);
    //        } else {
    //            console.log('Error ', error);
    //        }
    //    }
    //}

    //fLoadPeriodsRetainetPayroll();

	// Funcion que se encarga de mostrar el periodo actual \\
    fLoadInfoPeriodPayroll = () => {
        try {
            const d = new Date(), yearact = d.getFullYear();
            $.ajax({
                url: "../Dispersion/LoadInfoPeriodPayroll",
                type: "POST",
                data: { yearAct: yearact },
                success: (data) => {
                    console.log('Datos periodo');
                    console.log(data);
                    if (data.Bandera == true && data.MensajeError == "none") {
                        document.getElementById('typeperactnomemp').textContent = data.InfoPeriodo.iTipoPeriodo;
                        document.getElementById('peractnomemp').textContent = data.InfoPeriodo.iPeriodo;
                        document.getElementById('fechaspernomemp').textContent = data.InfoPeriodo.sFechaInicio + " - " + data.InfoPeriodo.sFechaFinal;
                        document.getElementById('typeperactnomemp1').textContent = data.InfoPeriodo.iTipoPeriodo;
                        document.getElementById('peractnomemp1').textContent = data.InfoPeriodo.iPeriodo;
                        document.getElementById('fechaspernomemp1').textContent = data.InfoPeriodo.sFechaInicio + " - " + data.InfoPeriodo.sFechaFinal;
                        periodis.value    = data.InfoPeriodo.iPeriodo;
                        typeperiod.value  = data.InfoPeriodo.iTipoPeriodo;
                        periodis1.value   = data.InfoPeriodo.iPeriodo;
                        typeperiod1.value = data.InfoPeriodo.iTipoPeriodo;
                    } else {
                        fShowTypeAlert('Atención!', 'No se ha cargado la informacion del periodo actual, contacte al área de TI indicando el siguiente código: #CODERRfLoadInfoPeriodPayrollMAINDIS#', 'error', navDispersion, 0);
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

	// Carga de las nominas retenidas \\
    const tableNomRetenidas = $("#table-nom-retenidas").DataTable({
        ajax: {
            method: "POST",
            url: "../Dispersion/PayrollRetainedEmployees",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            dataSrc: "data"
        },
        columns: [
            { "data": "sNombreEmpleado" },
            { "data": "sDescripcion" },
            { "data": "iPeriodo" },
            { "defaultContent": "<button title='Restaurar nomina retenida' class='btn text-center btn-outline-primary shadow rounded ml-2'><i class='fas fa-undo'></i></button>" }
        ],
        language: spanish
    });

	// Remueve la nomina retenida al empleado \\
    $("#table-nom-retenidas tbody").on('click', 'button', function () {
        var data = tableNomRetenidas.row($(this).parents('tr')).data();
        const clvnomret = data.iIdNominaRetenida;
        try {
            $.ajax({
                url: "../Dispersion/RemovePayrollRetainedEmployee",
                type: "POST",
                data: { keyPayrollRetained: clvnomret },
                success: (data) => {
                    console.log(data);
                    if (data.Bandera === true && data.MensajeError === "none") {
                        Swal.fire({
                            title: "Correcto", text: "Se quito al nomina retenida al empleado", icon: "success",
                            showClass: { popup: 'animated fadeInDown faster' },
                            hideClass: { popup: 'animated fadeOutUp faster' },
                            confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false
                        }).then((acepta) => {
                            tableNomRetenidas.ajax.reload();
                        });
                    } else {
                        Swal.fire({
                            title: "Ocurrio un problema", text: "Reporte el problema al area de TI indicando el siguiente código: #CODERRRemovePayrollRetainedEmployeeMAINDIS#", icon: "error",
                            showClass: { popup: 'animated fadeInDown faster' },
                            hideClass: { popup: 'animated fadeOutUp faster' },
                            confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false
                        }).then((acepta) => {

                        });
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
    });


	// Funcion que limpia la busqueda de los empleados a retener nomina \\
    fClearSearchPayrollRetained = () => {
        searchemployekeynom.value = '';
        resultemployekeynom.innerHTML = '';
    }

	// Funcion que comprueba que tipo de filtro de aplicara a la busqueda de empleados \\
    fSelectFilteredSearchEmployee = () => {
        const filtered = $("input:radio[name=filtroempretnom]:checked").val();
        if (filtered == "numero") {
            searchemployekeynom.placeholder = "NUMERO DEL EMPLEADO";
            searchemployekeynom.type = "number";
            labsearchempnom.textContent = "Numero";
        } else if (filtered == "nombre") {
            searchemployekeynom.placeholder = "NOMBRE DEL EMPLEADO";
            searchemployekeynom.type = "text";
            labsearchempnom.textContent = "Nombre";
        }
        searchemployekeynom.value = "";
        resultemployekeynom.innerHTML = "";
        setTimeout(() => { searchemployekeynom.focus() }, 500);
    }

	// Funcion que ejecuta la busqueda de los empleados a retener nomina \\
    fSearchEmployeesRetainedPayroll = () => {
        resultemployekeynom.innerHTML = '';
        const filtered = $("input:radio[name=filtroempretnom]:checked").val();
        try {
            if (searchemployekeynom.value != "") {
                $.ajax({
                    url: "../Dispersion/SearchEmployeesRetainedPayroll",
                    type: "POST",
                    data: { searchEmployee: searchemployekeynom.value, filter: filtered },
                    success: (data) => {
                        console.log('Busqueda de empleado');
                        console.log(data);
                        const quantity = data.length;
                        if (quantity > 0) {
                            let number = 0;
                            for (let i = 0; i < quantity; i++) {
                                number += 1;
                                resultemployekeynom.innerHTML += `
                                    <button onclick="fRetainedPayrollEmployee(${data[i].iIdEmpleado}, '${data[i].sNombreEmpleado}', ${data[i].iTipoPeriodo})" class="animated fadeIn list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back">
                                        ${number}. ${data[i].iIdEmpleado} - ${data[i].sNombreEmpleado}
                                       <span>
                                             <i title="Retener nomina" class="fas fa-user-times ml-2 text-danger fa-lg shadow"></i>
                                       </span>
                                    </button>
                                `;
                            }
                        }
                    }, complete: (suc) => {
                        console.log(suc);
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                resultemployekeynom.innerHTML = '';
            }
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

	// Funcion que inserta un empleado en la tabla de retencion de nomina \\
    fRetainedPayrollEmployee = (paramid, paramstr, paramper) => {
        try {
            const d = new Date();
            $.ajax({
                url: "../Dispersion/LoadTypePeriod",
                type: "POST",
                data: { year: d.getFullYear(), typePeriod: paramper },
                success: (data) => {
                    if (data.Bandera == true && data.MensajeError == "none") {
                        if (data.Datos.iPeriodo != 0) {
                            perretnom.value = data.Datos.iPeriodo;
                            document.getElementById('yearretnom').value = data.Datos.iAnio;
                        }
                        $("#retnominaemploye").modal('hide');
                        setTimeout(() => { $("#retnominaemployeconfig").modal('show'); }, 500);
                        document.getElementById('nameempret').value = paramstr;
                        document.getElementById('clvempretn').value = paramid;
                        document.getElementById('tipperretn').value = paramper;
                    } else {
                        fShowTypeAlert('Atención!', 'No se ha cargado la informacion del periodo actual, contacte al área de TI indicando el siguiente código: #CODERRfRetainedPayrollEmployeeMAINDIS#', 'error', navDispersion, 0);
                    }
                    searchemployekeynom.value     = '';
                    resultemployekeynom.innerHTML = '';
                }, error: (jqXHR, exception) => { fcaptureaerrorsajax(jqXHR, exception); }
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

	// Funcion que guarda los datos del empleado a su nomina retenida \\
    fRegisterPayrollRetainedEmployee = () => {
        try {
            const clvempretn = document.getElementById('clvempretn');
            const tipperretn = document.getElementById('tipperretn');
            const perretnom  = document.getElementById('perretnom');
            const yearretnom = document.getElementById('yearretnom');
            const descretnom = document.getElementById('descretnom');
            if (clvempretn != 0) {
                Swal.fire({
                    title: "Esta seguro de retener la nomina a", text: document.getElementById('nameempret').value + "?", icon: "warning",
                    showClass: { popup: 'animated fadeInDown faster' },
                    hideClass: { popup: 'animated fadeOutUp faster' },
                    confirmButtonText: "Aceptar", showCancelButton: true, cancelButtonText: "Cancelar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false
                }).then((acepta) => {
                    if (acepta.value) {
                        $.ajax({
                            url: "../Dispersion/RetainedPayrollEmployee",
                            type: "POST",
                            data: {
                                keyEmployee: clvempretn.value,
                                typePeriod: tipperretn.value,
                                periodPayroll: perretnom.value,
                                yearRetained: yearretnom.value,
                                descriptionRetained: descretnom.value
                            },
                            success: (data) => {
                                if (data.Bandera == true && data.MensajeError == "none") {
                                    tableNomRetenidas.ajax.reload();
                                    Swal.fire({
                                        title: "Correcto!", text: "Usuario registrado con nomina retenida", icon: "success",
                                        showClass: { popup: 'animated fadeInDown faster' },
                                        hideClass: { popup: 'animated fadeOutUp faster' },
                                        confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false
                                    }).then((acepta) => {
                                        $("#retnominaemployeconfig").modal('hide');
                                        tipperretn.value = "0";
                                        descretnom.value = "";
                                        setTimeout(() => {
                                            document.getElementById('body-init').removeAttribute("style");
                                        }, 1000);
                                    });
                                } else {
                                    Swal.fire({
                                        title: "Ocurrio un error", text: "Reporte el problema al area de TI indicando el siguiente código: #CODERRfRegisterPayrollRetainedEmployeeMAINDIS# ", icon: "error",
                                        showClass: { popup: 'animated fadeInDown faster' },
                                        hideClass: { popup: 'animated fadeOutUp faster' },
                                        confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false
                                    }).then((acepta) => {
                                        location.reload();
                                    });
                                }
                            }, error: (jqXHR, exception) => {
                                fcaptureaerrorsajax(jqXHR, exception);
                            }
                        });
                    } else {
                        Swal.fire('Atención', 'Acción cancelada', 'warning');
                    }
                });
            } else {
                location.reload();
            }
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

    // Destruimos la tabla
    fDestroyTable = (type) => {
        if (type == "table") {
            const dataTable = $("#dataTableSpecial").DataTable();
            dataTable.destroy();
            document.getElementById('data-groupbusiness').innerHTML = "";
        }
    }

    fLoadGroupBusiness = (type, selectid) => {
        fDestroyTable("table");
        try {
            if (type == "table") {
                $("#groupBusiness").modal("show");
            }
            $.ajax({
                url: "../Dispersion/LoadGroupBusiness",
                type: "POST",
                data: {},
                beforeSend: () => {

                }, success: (request) => {
                    console.log(request);
                    if (request.Bandera == true && request.MensajeError == "none") {
                        if (type == "table") {
                            if (request.Datos.length > 0) {
                                document.getElementById('data-groupbusiness').innerHTML = request.Html;
                            }
                            setTimeout(() => {
                                $("#dataTableSpecial").DataTable({
                                    language: spanish
                                });
                            }, 500);
                        } else {
                            for (let i = 0; i < request.Datos.length; i++) {
                                document.getElementById(selectid).innerHTML += `<option value="${request.Datos[i].iIdGrupoEmpresa}">
                                    ${request.Datos[i].sNombreGrupo}
                                </option>`;
                            }
                        }
                    } else {
                        if (type == "table") {
                            setTimeout(() => {
                                $("#dataTableSpecial").DataTable({
                                    language: spanish
                                });
                            }, 500);
                        }
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
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

    /* Funcion que carga informacion en los inputs de dispersion */
    fLoadInfoDataDispersion = () => {
        const d = new Date();
        yeardis.value  = d.getFullYear();
        yeardis1.value = d.getFullYear();
        //yeardis.setAttribute('readonly', 'true');
        //yeardis1.setAttribute('readonly', 'true');
        //periodis.setAttribute('readonly', 'true');
        //periodis1.setAttribute('readonly', 'true');
        const day = (d.getDate() < 10) ? "0" + d.getDate() : d.getDate();
        const mth = ((d.getMonth() + 1) < 10) ? "0" + (d.getMonth() + 1) : d.getMonth() + 1;
        const yer = d.getFullYear();
        datedis.value  = yer + '-' + mth + '-' + day;
        datedis1.value = yer + '-' + mth + '-' + day;
    }

    fLoadGroupBusiness("select", "option-group");


    // Funcion que añade los campos nuevos
    fLoadNewFieldsOptionDisEsp = () => {
        //floadgroupbusiness("select", "option-group");
        containerBtnsProDepBankSpecial.innerHTML += `
            <div class="row animated fadeInDown delay-1s mt-4 border-left-primary border-right-primary shadow rounded p-2">
                <div class="col-md-3 text-center">
                    <div class="form-group">
                        <label class="col-form-label font-labels" for="type-dispersion">Tipo dispersion</label>
                        <select class="form-control form-control-sm" id="type-dispersion">
                            <option value="none">Selecciona</option>
                            <option value="NOM">Nomina</option>
                            <option value="INT">Interbancarios</option>
                        </select>
                    </div>
                </div>
                <div class="col-md-3 text-center">
                    <div class="form-group form-check mt-1 rounded text-primary font-weight-bold mt-4" style="">
                        <input type="checkbox" class="form-check-input" id="ismirrorspecial" checked>
                        <label class="form-check-label" for="ismirrorspecial">Solo Espejo</label>
                    </div>
                </div>
                <div class="col-md-3 text-center">
                    <div class="form-group mt-4">
                        <button class="btn btn-primary btn-sm" type="button" id="btn-send-dispersion-special" onclick="fnSendDataDispersionSpecial();"> <i class="fas fa-play mr-2"></i>Procesar depositos</button>
                    </div>
                </div>
                <div class="col-md-3 text-center">
                    <div class="form-group mt-4">
                        <button class="btn btn-primary btn-sm" type="button" id="btn-send-report-ds" onclick="fnSendReportDs();"> <i class="fas fa-file mr-2"></i> Generar reporte </button>
                    </div>
                </div>
            </div>
        `;
    }

    fToDeployInfoDispersionEspecial = () => {
        btndesplegarespecialtab.innerHTML = `<i class="fas fa-play-circle mr-2"></i> Desplegar `;
        btndesplegarespecialtab.classList.remove('active'); 
        tableDataDepositsSpecial.classList.remove('animated', 'fadeIn');
        tableDataDepositsSpecial.innerHTML = '';
        alertDataDepositsSpecial.innerHTML = "";
        containerBtnsProDepBankSpecial.innerHTML = "";
        document.getElementById('divbtndownzip1').innerHTML    = "";
        document.getElementById('div-controls1').innerHTML     = "";
        document.getElementById('divbtndownzipint1').innerHTML = "";
        document.getElementById('div-controls-int1').innerHTML = "";
        try {
            const arrInput = [yeardis1, periodis1, datedis1, optionGroup];
            let validate = 0;
            for (let i = 0; i < arrInput.length; i++) {
                if (arrInput[i].value === "") {
                    fShowTypeAlert('Atencion', 'Completa el campo ' + String(arrInput[i].placeholder), 'warning', arrInput[i], 2);
                    validate = 1;
                    break;
                }
                if (arrInput[i].id == "option-group") {
                    if (arrInput[i].value == "none") {
                        fShowTypeAlert('Atención', 'Selecciona un grupo de empresas', 'warning', arrInput[i], 2);
                        validate = 1;
                        break;
                    }
                }
            }
            if (validate === 0) {
                $.ajax({
                    url: "../Dispersion/ToDeployDispersionSpecial",
                    type: "POST",
                    data: {
                        keyGroup: parseInt(optionGroup.value),
                        year: parseInt(yeardis1.value),
                        typePeriod: parseInt(typeperiod1.value),
                        period: parseInt(periodis1.value),
                        date: datedis1.value,
                        type: "test"
                    },
                    beforeSend: () => {
                        btndesplegarespecialtab.innerHTML = `
                            <span class="spinner-grow spinner-grow-sm mr-1" role="status" aria-hidden="true"></span>
                            <span class="sr-only">Loading...</span>
                            Desplegando
                        `;
                        btndesplegarespecialtab.disabled = true;
                    },
                    success: (data) => {
                        console.log('Mostrando los datos de dis especial')
                        console.log(data);
                        btndesplegarespecialtab.classList.add('active');
                        btndesplegarespecialtab.innerHTML = `<i class="fas fa-play mr-2"></i> Desplegar`;
                        btndesplegarespecialtab.disabled  = false;
                        if (data.BanderaDispersion == true) {
                            if (data.BanderaBancos == true) {
                                if (data.MensajeError == "none") {
                                    if (data.DatosDepositos.length > 0) {
                                        alertDataDepositsSpecial.innerHTML += `
                                            <div class="alert alert-info alert-dismissible fade show" role="alert">
                                              <strong> 
                                                <i class="fas fa-info-circle mr-1"></i> Correcto!
                                              </strong> La información bancaria ha sido desplegada (ESPEJO) .
                                              <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                              </button>
                                            </div>
                                        `;
                                        tableDataDepositsSpecial.classList.add('animated', 'fadeIn');
                                        tableDataDepositsSpecial.innerHTML += `
                                        <thead>
                                            <tr>
                                                <th scope="col">Banco</th>
                                                <th scope="col">Concepto</th>
                                                <th scope="col">Depositos</th>
                                                <th scope="col">Importe</th>
                                            </tr>
                                        </thead>
                                        <tbody id="table-body-data-especial"></tbody>
                                    `;
                                        for (let i = 0; i < data.DatosDepositos.length; i++) {
                                            let nomBanco = "";
                                            for (let j = 0; j < data.DatosBancos.length; j++) {
                                                if (data.DatosBancos[j].iIdBanco === data.DatosDepositos[i].iIdBanco) {
                                                    nomBanco = data.DatosBancos[j].sNombreBanco;
                                                }
                                            }
                                            document.getElementById("table-body-data-especial").innerHTML += `
                                                <tr>
                                                    <th scope="row">
                                                        <i class="fas fa-university mr-2 text-primary"></i>
                                                        ${data.DatosDepositos[i].iIdBanco}
                                                    </th>
                                                    <td>
                                                        <i class="fas fa-file-alt mr-2 text-primary"></i>
                                                        ${nomBanco}
                                                    </td>
                                                    <td>
                                                        <i class="fas fa-calculator mr-2 text-primary"></i>
                                                        ${data.DatosDepositos[i].iDepositos}
                                                    </td>
                                                    <td>
                                                        <i class="fas fa-money-bill mr-2 text-success"></i>
                                                        $ ${data.DatosDepositos[i].sImporte}
                                                    </td>
                                                </tr>
                                            `;
                                        }
                                        fLoadNewFieldsOptionDisEsp();
                                        //fLoadGroupBusiness("select", "option-group");
                                        //containerBtnsProDepBankSpecial.innerHTML += `
                                        //    <div class="row animated fadeInDown delay-1s mt-4 border-left-primary border-right-primary shadow rounded p-2">
                                        //        <div class="col-md-4">
                                        //            <div class="form-group">
                                        //                <label class="col-form-label font-labels" for="option-group">Grupo de empresas</label>
                                        //                <select class="form-control form-control-sm" id="option-group">
                                        //                    <option value="none">Selecciona</option>
                                        //                </select>
                                        //            </div>
                                        //        </div>
                                        //        <div class="col-md-4">
                                        //            <div class="form-group">
                                        //                <label class="col-form-label font-labels" for="type-dispersion">Tipo dispersion</label>
                                        //                <select class="form-control form-control-sm" id="type-dispersion">
                                        //                    <option value="none">Selecciona</option>
                                        //                    <option value="NOM">Nomina</option>
                                        //                    <option value="INT">Interbancarios</option>
                                        //                </select>
                                        //            </div>
                                        //        </div>
                                        //        <div class="col-md-4">
                                        //            <div class="form-group form-check mt-1 rounded text-primary font-weight-bold mt-4" style="">
                                        //                <input type="checkbox" class="form-check-input" id="ismirrorspecial">
                                        //                <label class="form-check-label" for="ismirrorspecial">Espejo</label>
                                        //            </div>
                                        //        </div>
                                        //        <div class="col-md-8 offset-2"><hr/></div>
                                        //        <div class="col-md-4 offset-2 text-center">
                                        //            <div class="form-group">
                                        //                <button class="btn btn-primary btn-sm" type="button" id="btn-send-dispersion-special" onclick="fnSendDataDispersionSpecial();"> <i class="fas fa-play mr-2"></i>Procesar depositos</button>
                                        //            </div>
                                        //        </div>
                                        //        <div class="col-md-4 text-center">
                                        //            <div class="form-group">
                                        //                <button class="btn btn-primary btn-sm" type="button" id="btn-send-report-ds" onclick="fnSendReportDs();"> <i class="fas fa-file mr-2"></i> Generar reporte </button>
                                        //            </div>
                                        //        </div>
                                        //    </div>
                                        //`;
                                    } else {
                                        fShowTypeAlert('Atención!', 'No se encontraron depositos', 'warning', btndesplegarespecialtab, 0);
                                    }
                                } else {
                                    fShowTypeAlert('Error!', 'Ocurrio un problema con el despliege de información, contacte al area de TI', 'error', btndesplegarespecialtab, 0);
                                }
                            } else {
                                fShowTypeAlert('Atención!', 'No hay bancos definidos', 'warning', btndesplegarespecialtab, 0);
                            }
                        } else {
                            fShowTypeAlert('Atención!', 'No se encontro informacion bancaria, verifique que la nomina haya sido ejecutada.', 'warning', btndesplegarespecialtab, 0);
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            }
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

    fToDeployInfoDispersion = () => {
        btndesplegartab.innerHTML = `<i class="fas fa-play-circle mr-2"></i> Desplegar `;
        btndesplegartab.classList.remove('active');
        tableDataDeposits.classList.remove('animated', 'fadeIn');
        tableDataDeposits.innerHTML = '';
        alertDataDeposits.innerHTML = '';
        containerBtnsProDepBank.innerHTML = "";
        document.getElementById('divbtndownzip').innerHTML    = "";
        document.getElementById('div-controls').innerHTML     = "";
        document.getElementById('divbtndownzipint').innerHTML = "";
        document.getElementById('div-controls-int').innerHTML = "";
        try {
            const arrInput = [yeardis, periodis, datedis];
            let validate = 0;
            for (let i = 0; i < arrInput.length; i++) {
                if (arrInput[i].value === "") {
                    fShowTypeAlert('Atencion', 'Completa el campo ' + String(arrInput[i].placeholder), 'warning', arrInput[i], 2);
                    validate = 1;
                    break;
                }
            }
            const dataSend = {
                yearDispersion: parseInt(yeardis.value), typePeriodDisp: parseInt(typeperiod.value),
                periodDispersion: parseInt(periodis.value), dateDispersion: datedis.value, type: "test"
            };
            console.log(dataSend);
            if (validate === 0) {
                $.ajax({
                    url: "../Dispersion/ToDeployDispersion",
                    type: "POST",
                    data: dataSend,
                    beforeSend: () => {
                        btndesplegartab.innerHTML = `
                            <span class="spinner-grow spinner-grow-sm mr-1" role="status" aria-hidden="true"></span>
                            <span class="sr-only">Loading...</span>
                            Desplegando
                        `;
                        btndesplegartab.disabled = true;
                    },
                    success: (data) => {
                        btndesplegartab.classList.add('active');
                        btndesplegartab.innerHTML = `<i class="fas fa-play mr-2"></i> Desplegar`;
                        btndesplegartab.disabled = false;
                        if (data.BanderaDispersion == true) {
                            if (data.BanderaBancos == true) {
                                if (data.MensajeError == "none") {
                                    if (data.DatosDepositos.length > 0) {
                                        alertDataDeposits.innerHTML += `
                                            <div class="alert alert-info alert-dismissible fade show" role="alert">
                                              <strong> 
                                                <i class="fas fa-info-circle mr-1"></i> Correcto!
                                              </strong> La información bancaria ha sido desplegada.
                                              <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                              </button>
                                            </div>
                                        `;
                                        tableDataDeposits.classList.add('animated', 'fadeIn');
                                        tableDataDeposits.innerHTML += `
                                        <thead>
                                            <tr>
                                                <th scope="col">Banco</th>
                                                <th scope="col">Concepto</th>
                                                <th scope="col">Depositos</th>
                                                <th scope="col">Importe</th>
                                            </tr>
                                        </thead>
                                        <tbody id="table-body-data"></tbody>
                                    `;
                                        for (let i = 0; i < data.DatosDepositos.length; i++) {
                                            let nomBanco = "";
                                            for (let j = 0; j < data.DatosBancos.length; j++) {
                                                if (data.DatosBancos[j].iIdBanco === data.DatosDepositos[i].iIdBanco) {
                                                    nomBanco = "[" + data.DatosBancos[j].sSufijo + "] " + data.DatosBancos[j].sNombreBanco;
                                                }
                                            }
                                            document.getElementById("table-body-data").innerHTML += `
                                                <tr>
                                                    <th scope="row">
                                                        <i class="fas fa-university mr-2 text-primary"></i>
                                                        ${data.DatosDepositos[i].iIdBanco}
                                                    </th>
                                                    <td>
                                                        <i class="fas fa-file-alt mr-2 text-primary"></i>
                                                        ${nomBanco}
                                                    </td>
                                                    <td>
                                                        <i class="fas fa-calculator mr-2 text-primary"></i>
                                                        ${data.DatosDepositos[i].iDepositos}
                                                    </td>
                                                    <td>
                                                        <i class="fas fa-money-bill mr-2 text-success"></i>
                                                        $ ${data.DatosDepositos[i].sImporte}
                                                    </td>
                                                </tr>
                                            `;
                                        }
                                        containerBtnsProDepBank.innerHTML += `
                                            <div class="row animated fadeInDown mt-4">
                                                <div class="col-md-6 offset-3">
                                                    <div class="form-group">
                                                        <label class="col-form-label">Fecha dispersión:</label>
                                                        <input type="date" class="form-control-sm form-control" id="dateDisC"/>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 offset-3">
                                                    <div class="form-group">
                                                        <label class="col-form-label">Tipo pago:</label>
                                                        <select class="form-control form-control-sm" id="tipPago">
                                                            <option value="1">PAGO NOMINA</option>
                                                            <option value="2">HONORARIOS</option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row animated fadeInDown delay-1s mt-4">
                                                <div class="col-md-3 text-center">
                                                    <div class="form-group form-check mt-1 rounded text-primary font-weight-bold" style="">
                                                        <input type="checkbox" class="form-check-input" id="ismirror">
                                                        <label class="form-check-label" for="ismirror">Con Archivos Espejo</label>
                                                    </div>
                                                </div>
                                                <div class="col-md-3 text-center">
                                                    <div class="form-group">
                                                        <button type="button" class="btn btn-primary btn-sm btn-block shadow"
                                                            onclick="fValidateBankInterbank('INT');" id="btn-process-deposits-interbank">
                                                            <i class="fas fa-play mr-1"></i>
                                                            <i class="fas fa-money-check-alt mr-2"></i>
                                                            <span class="text">Interbancarios</span>
                                                        </button>
                                                    </div>
                                                </div>
                                                <div class="col-md-3 text-center">
                                                    <div class="form-group">
                                                        <button type="button" class="btn btn-primary btn-sm btn-block shadow"                                      onclick="fValidateBankInterbank('NOM');" id="btn-process-deposits-payroll">
                                                           
                                                            <i class="fas fa-play mr-1"></i>
                                                            <i class="fas fa-money-bill-wave mr-1"></i>
                                                            <span class="text">Nómina</span>
                                                        </button>
                                                    </div>
                                                </div>
                                                <div class="col-md-3 text-center">
                                                    <div class="form-group">
                                                        <button type="button" class="btn btn-success btn-sm btn-block shadow" 
                                                            id="btn-process-report" onclick="fGenerateReportDispersion();">
                                                            <i class="fas fa-play mr-1"></i>
                                                            <i class="fas fa-file mr-1"></i>
                                                            <span class="text">Reporte</span>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        `;
                                    } else {
                                        fShowTypeAlert('Atención!', 'No se encontraron depositos', 'warning', btndesplegartab, 0);
                                    }
                                } else {
                                    fShowTypeAlert('Error!', 'Ocurrio un problema con el despliege de información, contacte al area de TI', 'error', btndesplegartab, 0);
                                }
                            } else {
                                fShowTypeAlert('Atención!', 'No hay bancos definidos', 'warning', btndesplegartab, 0);
                            }
                        } else {
                            fShowTypeAlert('Atención!', 'No se encontro informacion bancaria, verifique que la nomina haya sido ejecutada.', 'warning', btndesplegartab, 0);
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            }
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

    // Funcion genera el reporte en formato EXCEL
    //fGenerateReportDis = () => {
    //    try {
    //        $.ajax({
    //            url: "",
    //            type: "POST",
    //            data: {},
    //            beforeSend: () => {

    //            }, success: (request) => {
    //                console.log(request);
    //            }, error: (jqXHR, exception) => {
    //                fcaptureaerrorsajax(jqXHR, exception);
    //            }
    //        });
    //    } catch (error) {
    //        if (error instanceof EvalError) {
    //            console.log('EvalError ', error);
    //        } else if (error instanceof RangeError) {
    //            console.log('RangeError ', error);
    //        } else if (error instanceof TypeError) {
    //            console.log('TypeError ', error);
    //        } else {
    //            console.log('Error ', error);
    //        }
    //    }
    //}

    // Funcion valida banco interbancario existe
    fValidateBankInterbank = (paramcode) => {
        const valSend = (paramcode == "NOM") ? 285 : 286;
        let labelSend = (paramcode == "NOM") ? "de nomina" : "interbancarios";
        try {
            $.ajax({
                url: "../Dispersion/ValidateBankInterbank",
                type: "POST",
                data: { type: parseInt(valSend) },
                beforeSend: () => {
                    //console.log('validando');
                }, success: (data) => {
                    //console.log(data);
                    if (data.Bandera === true && data.MensajeError == "none") {
                        if (String(paramcode) == "INT") {
                            fProcessDepositsInterbank();
                        } else if (String(paramcode) == "NOM") {
                            fProcessDepositsPayroll();
                        } else {
                            alert('Accion invalida');
                        }
                    } else {
                        fShowTypeAlert('Atención!', 'No hay bancos ' + labelSend + ' definidos, defina uno para continuar', 'warning', btndesplegartab, 0);
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
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


    fRestartReportFileDN = (paramFolder, paramFile, type) => {
        try {
            if (type == 1) {
                if (paramFolder != "" && paramFile != "") {
                    $.ajax({
                        url: "../DispersionSpecial/RestartReportFile",
                        type: "POST",
                        data: { folder: String(paramFolder), file: String(paramFile) },
                        beforeSend: () => {

                        }, success: (request) => {
                            console.log(request);
                            document.getElementById('btn-process-deposits-payroll').disabled   = false;
                            document.getElementById('btn-process-deposits-interbank').disabled = false;
                            document.getElementById('ismirror').disabled           = false;
                            document.getElementById('btn-process-report').disabled = false;
                            document.getElementById('divbtndownzip').innerHTML     = "";
                            document.getElementById('div-controls').innerHTML      = "";
                            document.getElementById('divbtndownzipint').innerHTML  = "";
                            document.getElementById('div-controls-int').innerHTML  = "";
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    });
                } else {
                    alert('Accion invalida');
                    location.reload();
                }
            } else {
                document.getElementById('btn-process-deposits-payroll').disabled   = false;
                document.getElementById('btn-process-deposits-interbank').disabled = false;
                document.getElementById('ismirror').disabled           = false;
                document.getElementById('btn-process-report').disabled = false;
                document.getElementById('divbtndownzip').innerHTML     = "";
                document.getElementById('div-controls').innerHTML      = "";
                document.getElementById('divbtndownzipint').innerHTML  = "";
                document.getElementById('div-controls-int').innerHTML  = "";
            }
        } catch (error) {
            console.log(fCatchError(error));
        }
    }

    // Funcion que genera el reporte de la dispersion
    fGenerateReportDispersion = () => {
        try {
            const dataSendProcessDepPayroll = {
                yearPeriod: parseInt(yeardis.value),
                numberPeriod: parseInt(periodis.value),
                typePeriod: parseInt(typeperiod.value),
            };
            $.ajax({
                url: "../Dispersion/GenerateReportDispersion",
                type: "POST",
                data: dataSendProcessDepPayroll,
                beforeSend: () => {
                    document.getElementById('btn-process-deposits-payroll').disabled = true;
                    document.getElementById('btn-process-deposits-interbank').disabled = true;
                    document.getElementById('ismirror').disabled = true;
                    document.getElementById('btn-process-report').disabled = true;
                }, success: (request) => {
                    console.log(request);
                    if (request.Bandera == true && request.MensajeError == "none") {
                        document.getElementById('divbtndownzip').innerHTML += `
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
                        document.getElementById('div-controls').innerHTML += `
                                    <div class="animated fadeInDown delay-1s">
                                        <h6 class="text-primary font-weight-bold"> <i class="fas fa-check-circle mr-2"></i> Archivo generado!</h6>
                                        <hr />
                                        <button id="btn-restart-to-deploy-special" class="btn btn-sm btn-primary" type="button" onclick="fRestartReportFileDN('${request.Folder}', '${request.Archivo}', 1);"> <i class="fas fa-undo mr-2"></i> Activar botones </button>
                                    </div>
                                `;
                    } else if (request.Bandera == false && request.Rows == 0) {
                        fShowTypeAlert('Atención!', 'No se encontraron registros para generar el reporte', 'info', document.getElementById('btn-send-dispersion-special'), 0);
                        fRestartReportFileDN('none', 'none', 2);
                    } else {
                        fShowTypeAlert('Error!', 'Ocurrio un error interno en la aplicacion', 'error', document.getElementById('btn-send-dispersion-special'), 0);
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

    // Funcion payroll
    fProcessDepositsPayroll = () => {
        try {
            document.getElementById('divbtndownzip').innerHTML = "";
            document.getElementById('div-controls').innerHTML  = "";
            const btnProcessDepositsPayroll   = document.getElementById('btn-process-deposits-payroll');
            const btnProcessDepositsInterbank = document.getElementById('btn-process-deposits-interbank');
            const mirrorSend = (document.getElementById('ismirror').checked) ? 1 : 0;
            const dataSendProcessDepPayroll = {
                yearPeriod: parseInt(yeardis.value),
                numberPeriod: parseInt(periodis.value),
                typePeriod: parseInt(typeperiod.value),
                dateDeposits: datedis.value,
                mirror: mirrorSend,
                type: 285,
                dateDisC: document.getElementById('dateDisC').value
            };
            $.ajax({
                url: "../Dispersion/ProcessDepositsPayroll",
                type: "POST",
                data: dataSendProcessDepPayroll,
                beforeSend: () => {
                    btnProcessDepositsPayroll.disabled   = true;
                    btnProcessDepositsInterbank.disabled = true;
                    document.getElementById('ismirror').disabled = true;
                    document.getElementById('div-show-alert-loading').innerHTML = `
                        <div class="text-center">
                            <div class="spinner-grow text-info" role="status">
                                <span class="sr-only">Loading...</span>
                            </div>
                            <h6 class="font-weight-bold text-info">Generando...</h6>
                        </div>
                    `;
                }, success: (data) => {
                    console.log('Imprimiendo valor de data Nomina');
                    console.log(data);
                    document.getElementById('div-show-alert-loading').innerHTML = '';
                    if (data.Bandera == true) {
                        document.getElementById('divbtndownzip').innerHTML += `
                            <div class="card border-left-success shadow h-100 py-2 animated fadeInRight delay-2s">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-success text-uppercase mb-1">${data.Zip}.zip</div>
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
                                            <a title="Descargar archivo ${data.Zip}.zip" id="btn-down-txt" download="${data.Zip}.zip" href="/DispersionZIP/${data.Anio}/NOMINAS/${data.Zip}.zip" ><i class="fas fa-download fa-2x text-gray-300"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>`;
                        document.getElementById('div-controls').innerHTML += `
                            <div class="animated fadeInDown delay-1s">
                                <h6 class="text-primary font-weight-bold"> <i class="fas fa-check-circle mr-2"></i> Depositos de nomina generados!</h6>
                                <hr />
                                <button id="btn-restart-to-deploy" class="btn btn-sm btn-primary" type="button" onclick="fRestartToDeploy('${data.Zip}',${data.Anio}, 'NOM');"> <i class="fas fa-undo mr-2"></i> Activar botones </button>
                            </div>
                        `;
                    } else {
                        fShowTypeAlert('Atención!', 'No se encontraron depositos', 'warning', btnProcessDepositsPayroll, 0);
                        btnProcessDepositsPayroll.disabled = false;
                        btnProcessDepositsInterbank.disabled = false;
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
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

    // Funcion Restart ToDeploy
    fRestartToDeploy = (paramname, paramyear, paramcode) => {
        try {
            if (String(paramname) != "" && String(paramcode) != "" && String(paramname).length > 0 && String(paramcode).length > 0) {
                if (parseInt(paramyear) != 0 && String(paramyear).length == 4) {
                    const btnProcessDepositsPayroll   = document.getElementById('btn-process-deposits-payroll');
                    const btnProcessDepositsInterbank = document.getElementById('btn-process-deposits-interbank');
                    const btnRestartToDeploy          = document.getElementById('btn-restart-to-deploy');
                    const dataSend = {
                        paramNameFile: String(paramname),
                        paramYear: parseInt(paramyear),
                        paramCode: String(paramcode)
                    };
                    $.ajax({
                        url: "../Dispersion/RestartToDeploy",
                        type: "POST",
                        data: dataSend,
                        beforeSend: () => {
                            btnRestartToDeploy.disabled = true;
                        }, success: (data) => {
                            btnProcessDepositsPayroll.disabled   = false;
                            btnProcessDepositsInterbank.disabled = false;
                            document.getElementById('ismirror').disabled = false;
                            document.getElementById('ismirror').checked  = 0;
                            document.getElementById('divbtndownzip').innerHTML = "";
                            document.getElementById('div-controls').innerHTML  = "";
                            document.getElementById('divbtndownzipint').innerHTML = "";
                            document.getElementById('div-controls-int').innerHTML = "";
                            $("html, body").animate({ scrollTop: $('#btn-desplegar-tab').offset().top - 50 }, 1000);
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

    // Funcion Interbancarios
    fProcessDepositsInterbank = () => {
        try {
            const btnProcessDepositsPayroll   = document.getElementById('btn-process-deposits-payroll');
            const btnProcessDepositsInterbank = document.getElementById('btn-process-deposits-interbank');
            const mirrorSend = (document.getElementById('ismirror').checked) ? 1 : 0;
            const dataSend   = {
                yearPeriod: parseInt(yeardis.value),
                numberPeriod: parseInt(periodis.value),
                typePeriod: parseInt(typeperiod.value),
                dateDeposits: datedis.value,
                mirror: mirrorSend,
                type: 286,
                dateDisC: document.getElementById('dateDisC').value,
                tipPago: document.getElementById('tipPago').value
            };
            $.ajax({
                url: "../Dispersion/ProcessDepositsInterbank",
                type: "POST",
                data: dataSend,
                beforeSend: () => {
                    btnProcessDepositsPayroll.disabled   = true;
                    btnProcessDepositsInterbank.disabled = true;
                    document.getElementById('ismirror').disabled = true;
                    document.getElementById('div-show-alert-loading').innerHTML = `
                        <div class="text-center">
                            <div class="spinner-grow text-info" role="status">
                                <span class="sr-only">Loading...</span>
                            </div>
                            <h6 class="font-weight-bold text-info">Generando...</h6>
                        </div>
                    `;
                }, success: (data) => {
                    console.log('Imprimiendo valor de data Interbancario');
                    console.log(data);
                    document.getElementById('div-show-alert-loading').innerHTML = '';
                    if (data.Bandera == true) {
                        document.getElementById('divbtndownzipint').innerHTML += `
                            <div class="card border-left-success shadow h-100 py-2 animated fadeInLeft delay-2s">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-success text-uppercase mb-1">${data.Zip}.zip</div>
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
                                            <a title="Descargar archivo ${data.Zip}.zip" id="btn-down-txt" download="${data.Zip}.zip" href="/DispersionZIP/${data.Anio}/INTERBANCARIOS/${data.Zip}.zip" ><i class="fas fa-download fa-2x text-gray-300"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>`;
                        document.getElementById('div-controls-int').innerHTML += `
                            <div class="animated fadeInDown delay-1s">
                                <h6 class="text-primary font-weight-bold"> <i class="fas fa-check-circle mr-2"></i> Depositos interbancarios generados!</h6>
                                <hr />
                                <button id="btn-restart-to-deploy" class="btn btn-sm btn-primary" type="button" onclick="fRestartToDeploy('${data.Zip}',${data.Anio}, 'INT');"> <i class="fas fa-undo mr-2"></i> Activar botones </button>
                            </div>
                        `;
                    } else {
                        fShowTypeAlert('Atención!', 'No se encontraron depositos', 'warning', btnProcessDepositsPayroll, 0);
                        btnProcessDepositsPayroll.disabled   = false;
                        btnProcessDepositsInterbank.disabled = false;
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
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

    /* * Inicio dispersion especial */

    const btnShowGroupBusiness = document.getElementById('btn-show-group-business');
    const btnNewGroupBusiness  = document.getElementById('btn-new-group-business');

    const btnRegisterGroupBusiness = document.getElementById('btn-new-group-business');
    const btnSaveNewGroupBusiness  = document.getElementById('btn-save-new-group-business');

    const icoCloseGroupBusiness = document.getElementById('ico-close-group-business');
    const btnCloseGroupBusiness = document.getElementById('btn-close-group-business');

    const icoCloseNewGroupBusiness = document.getElementById('ico-close-new-group-business');
    const btnCloseNewGroupBusiness = document.getElementById('btn-close-new-group-business');

    const btnSaveAsignGroupBusiness = document.getElementById('btn-save-asign-group-business');

    const icoCloseAsignGroupBusiness = document.getElementById('ico-close-asign-group-business');
    const btnCloseAsignGroupBusiness = document.getElementById('btn-close-asign-group-business');

    const icoCloseViewGroupBusiness = document.getElementById('ico-close-view-group-business');
    const btnCloseViewGroupBusiness = document.getElementById('btn-close-view-group-business');

    // Guardamos un nuevo grupo de empresas
    fSaveNewGroupBusiness = () => {
        try {
            const nameNewGroup = document.getElementById('name-new-group');
            if (nameNewGroup.value != "") {
                const dataSend = { name: nameNewGroup.value };
                $.ajax({
                    url: "../Dispersion/SaveNewGroupBusiness",
                    type: "POST",
                    data: dataSend,
                    beforeSend: () => {
                        btnSaveNewGroupBusiness.disabled = true;
                    }, success: (request) => {
                        console.log(request);
                        btnSaveNewGroupBusiness.disabled = false;
                        if (request.Bandera == true && request.MensajeError == "none" && request.Mensaje == "SUCCESS") {
                            fDestroyTable("table");
                            Swal.fire({
                                title: 'Correcto!', text: 'Grupo añadido', icon: 'success',
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                nameNewGroup.value = "";
                                $("#modalNewGroup").modal("hide");
                                setTimeout(() => {
                                    fLoadGroupBusiness("table");
                                }, 1500);
                            });
                        } else if (request.Bandera == false && request.Mensaje == "EXISTS") {
                            fShowTypeAlert("Anteción!", "El grupo que intenta registrar ya existe!", "warning", nameNewGroup, 2);
                        } else {
                            fShowTypeAlert("Error!", "Ha ocurrido un error interno en la aplicación", "error", nameNewGroup, 2);
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                fShowTypeAlert('Atención!', 'Ingrese el nombre del grupo', 'warning', nameNewGroup, 0);
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

    btnSaveNewGroupBusiness.addEventListener('click', fSaveNewGroupBusiness);

    icoCloseGroupBusiness.addEventListener('click', () => { fDestroyTable("table"); });
    btnCloseGroupBusiness.addEventListener('click', () => { fDestroyTable("table"); });

    btnShowGroupBusiness.addEventListener('click', () => { fLoadGroupBusiness("table"); });
    btnRegisterGroupBusiness.addEventListener('click', () => {
        fDestroyTable("table");
        setTimeout(() => {
            document.getElementById('name-new-group').focus();
        }, 1000);
    });

    // Carga las empresas que no estan en un grupo
    fLoadBusinessNotInGroup = () => {
        document.getElementById('select-business').innerHTML = `<option value="none">Selecciona</option>`;
        $.ajax({
            url: "../Dispersion/LoadBusinessNotGroup",
            type: "POST",
            data: {},
            beforeSend: () => {

            }, success: (request) => {
                console.log(request);
                if (request.Bandera == true && request.MensajeError == "none") {
                    for (let i = 0; i < request.Datos.length; i++) {
                        document.getElementById('select-business').innerHTML += `<option value="${request.Datos[i].iIdEmpresa}">
                           [${request.Datos[i].iIdEmpresa}] ${request.Datos[i].sNombreEmpresa}
                        </option>`;
                    }
                } else if (request.Bandera == false && request.MensajeError == "none") {
                    fShowTypeAlert("Atención!", "No hay empresas disponibles para asignar a aun grupo.", "info", btnNewGroupBusiness, 0);
                } else {
                    fShowTypeAlert("Error!", "Ocurrio un error interno de la aplicación", "error", btnNewGroupBusiness, 0);
                    document.getElementById('select-groups').value = "none";
                    document.getElementById('select-business').value = "none";
                }
            }, error: (jqXHR, exception) => {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });
    }

    btnNewGroupBusiness.addEventListener('click', () => {
        $("#modalAsignGroup").modal('show');
        fLoadGroupBusiness("select", "select-groups");
        fLoadBusinessNotInGroup();
    });

    // Funcion limpia campo de nuevo grupo
    fClearFieldNewGroup = () => {
        document.getElementById('name-new-group').value = "";
        setTimeout(() => {
            fLoadGroupBusiness("table");
        }, 500);
    }

    icoCloseNewGroupBusiness.addEventListener('click', fClearFieldNewGroup);
    btnCloseNewGroupBusiness.addEventListener('click', fClearFieldNewGroup);

    // Funcion que guarda la asignacion de una empresa y un grupo
    fSaveAsignGroupBusiness = () => {
        try {
            const selectGroups = document.getElementById('select-groups');
            const selectBusiness = document.getElementById('select-business');
            if (selectGroups.value != "none") {
                if (selectBusiness.value != "none") {
                    const dataSend = { group: selectGroups.value, business: selectBusiness.value };
                    $.ajax({
                        url: "../Dispersion/SaveAsignGroupBusiness",
                        type: "POST",
                        data: dataSend,
                        beforeSend: () => {
                            btnSaveAsignGroupBusiness.disabled = true;
                        }, success: (request) => {
                            btnSaveAsignGroupBusiness.disabled = false;
                            console.log(request);
                            if (request.Bandera == true && request.MensajeError == "none") {
                                Swal.fire({
                                    title: 'Correcto!', text: 'Empresa añadida al grupo!', icon: 'success',
                                    showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                    confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                }).then((acepta) => {
                                    selectGroups.value = "none";
                                    fLoadBusinessNotInGroup();
                                });
                            } else if (request.Bandera == false && request.Mensaje == "NOTINSERT") {
                                fShowTypeAlert("Error", "Información no guardada", "error", btnSaveAsignGroupBusiness, 0);
                            } else {
                                fShowTypeAlert("Error", "Ocurrio un error interno en la aplicación", "error", btnSaveAsignGroupBusiness, 0);
                            }
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    });
                } else {
                    fShowTypeAlert("Atención!", "Selecciona una empresa", "warning", selectBusiness, 2);
                }
            } else {
                fShowTypeAlert("Atención!", "Selecciona un grupo de empresas", "warning", selectGroups, 2);
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

    btnSaveAsignGroupBusiness.addEventListener('click', fSaveAsignGroupBusiness);

    // Limpiamos los campos de la asignacion de empresas a grupos
    fClearFieldsAsignGroups = () => {
        document.getElementById('select-groups').innerHTML   = `<option value="none">Selecciona</option>`;
        document.getElementById('select-business').innerHTML = `<option value="none">Selecciona</option>`;
    }

    icoCloseAsignGroupBusiness.addEventListener('click', fClearFieldsAsignGroups);
    btnCloseAsignGroupBusiness.addEventListener('click', fClearFieldsAsignGroups);

    // Funcion para ver las empresas de un grupo
    fViewBusinessGroup = (paramint, paramstr) => {
        document.getElementById('content-groups').innerHTML = "";
        try {
            if (parseInt(paramint) > 0) {
                $.ajax({
                    url: "../Dispersion/ViewBusinessGroup",
                    type: "POST",
                    data: { keyGroup: paramint },
                    beforeSend: () => {

                    }, success: (request) => {
                        console.log(request);
                        if (request.Bandera == true && request.MensajeError == "none") {
                            document.getElementById('namegroup').textContent = paramstr;
                            for (let i = 0; i < request.Datos.length; i++) {
                                document.getElementById('content-groups').innerHTML += `<div class="col-md-4 mb-4">
                                    <div class="card border-left-primary shadow h-100 py-2">
                                        <span class="text-right" style="cursor:pointer;" onclick="fRemoveBusinessGroup(${request.Datos[i].iIdDetalleGrupo}, ${paramint}, '${paramstr}')"> <i class="fas fa-times text-danger mr-2"></i> </span> 
                                        <div class="card-body">
                                            <div class="row no-gutters align-items-center">
                                                <div class="col mr-2">
                                                    <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">${request.Datos[i].fRfc}</div>
                                                    <div class="h5 mb-0 font-weight-bold text-gray-800"> <small class="text-primary"># ${request.Datos[i].iIdEmpresa}</small> ${request.Datos[i].sNombreEmpresa}</div>
                                                </div>
                                                <div class="col-auto">
                                                    <i class="fas fa-building fa-2x text-gray-300"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                `;
                            }
                            $("#groupBusiness").modal("hide");
                            setTimeout(() => {
                                $("#modalViewGroupBusiness").modal('show');
                            }, 500);
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

    // Limpiamos contenedor de empresas por grupo
    fClearContainerGroupBusiness = () => {
        document.getElementById('content-groups').innerHTML = "";
        setTimeout(() => {
            $("#groupBusiness").modal('show');
        }, 500);
    }

    icoCloseViewGroupBusiness.addEventListener('click', fClearContainerGroupBusiness);
    btnCloseViewGroupBusiness.addEventListener('click', fClearContainerGroupBusiness);

    // Funcion que remueve una empresa del un grupo
    fRemoveBusinessGroup = (paramint, paramkey, paramstr) => {
        try {
            if (paramint > 0) {
                $.ajax({
                    url: "../Dispersion/RemoveBusinessGroup",
                    type: "POST",
                    data: { keyBusinessGroup: parseInt(paramint) },
                    beforeSend: () => {

                    }, success: (request) => {
                        console.log(request);
                        if (request.Bandera == true && request.MensajeError == "none") {
                            Swal.fire({
                                title: 'Correcto!', text: 'Empresa removida del grupo!', icon: 'success',
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                fViewBusinessGroup(paramkey, `'${paramstr}'`);
                            });
                        } else if (request.Bandera == false && request.MensajeError == "NOTDELETE") {
                            fShowTypeAlert("Atención!", "Empresa no eliminada del grupo", "warning", null, 0);
                        } else {
                            fShowTypeAlert("Error", "Ocurrio un error interno en la aplicación", "error", null, 0);
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

    /*
     * Fin dispersion especial
     */

	/*
	 * Ejecucion de funciones
	 */

    fLoadInfoPeriodPayroll();

    icoclosesearchempnomret.addEventListener('click', fClearSearchPayrollRetained);
    btnclosesearchempnomret.addEventListener('click', fClearSearchPayrollRetained);

    filtronamenom.addEventListener('click', fSelectFilteredSearchEmployee);
    filtronumbernom.addEventListener('click', fSelectFilteredSearchEmployee);

    searchemployekeynom.addEventListener('keyup', fSearchEmployeesRetainedPayroll);

    btnretnominaemp.addEventListener('click', () => { setTimeout(() => { searchemployekeynom.focus(); }, 1200); });

    btnregisterretnomina.addEventListener('click', fRegisterPayrollRetainedEmployee);

    fLoadInfoDataDispersion();

    btndesplegartab.addEventListener('click', fToDeployInfoDispersion);

    btndesplegarespecialtab.addEventListener('click', fToDeployInfoDispersionEspecial);
});