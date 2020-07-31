﻿$(function () {

	/*
	 * Constantes dispersion
	 */ 

    const navDispersion           = document.getElementById('nav-dispersion');
    const containerDataDeploy     = document.getElementById('container-data-deploy');
    const tableDataDeposits       = document.getElementById('table-data-deposits');
    const alertDataDeposits       = document.getElementById('alert-data-deposits');
    const containerBtnsProDepBank = document.getElementById('container-btns-process-deposits-bank');
    const btndesplegartab         = document.getElementById('btn-desplegar-tab');
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
    const datedis  = document.getElementById('datedis');

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
                        periodis.value = data.InfoPeriodo.iPeriodo;
                        typeperiod.value = data.InfoPeriodo.iTipoPeriodo;
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

    /* Funcion que carga informacion en los inputs de dispersion */
    fLoadInfoDataDispersion = () => {
        const d = new Date();
        yeardis.value = d.getFullYear();
        yeardis.setAttribute('readonly', 'true');
        periodis.setAttribute('readonly', 'true');
        const day = (d.getDate() < 10) ? "0" + d.getDate() : d.getDate();
        const mth = ((d.getMonth() + 1) < 10) ? "0" + (d.getMonth() + 1) : d.getMonth() + 1;
        const yer = d.getFullYear();
        datedis.value = yer + '-' + mth + '-' + day;
        console.log("Día: " + day + " Mes: " + mth + " Año: " + yer);
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
            if (validate === 0) {
                $.ajax({
                    url: "../Dispersion/ToDeployDispersion",
                    type: "POST",
                    data: {
                        yearDispersion: parseInt(yeardis.value),
                        typePeriodDisp: parseInt(typeperiod.value),
                        periodDispersion: parseInt(periodis.value),
                        dateDispersion: datedis.value,
                        type: "test"
                    },
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
                                            <div class="row animated fadeInDown delay-1s mt-4">
                                                <div class="col-md-6 text-center">
                                                    <div class="form-group">
                                                        <button type="button" class="btn btn-primary btn-sm btn-icon-split shadow"
                                                            onclick="fValidateBankInterbank('INT');" id="btn-process-deposits-interbank">
                                                            <span class="icon text-white">
                                                                <i class="fas fa-play mr-1"></i>
                                                                <i class="fas fa-money-check-alt"></i>
                                                            </span>
                                                            <span class="text">Procesar Depósitos Interbancarios</span>
                                                        </button>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 text-center">
                                                    <div class="form-group">
                                                        <button type="button" class="btn btn-primary btn-sm btn-icon-split shadow"                                      onclick="fValidateBankInterbank('NOM');" id="btn-process-deposits-payroll">
                                                            <span class="icon text-white">
                                                                <i class="fas fa-play mr-1"></i>
                                                                <i class="fas fa-money-bill-wave"></i>
                                                            </span>
                                                            <span class="text">Procesar Depósitos de Nomina</span>
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
                            fShowTypeAlert('Atención!', 'Ocurrio un problema', 'error', btndesplegartab, 0);
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

    // Funcion valida banco interbancario existe
    fValidateBankInterbank = (paramcode) => {
        try {
            $.ajax({
                url: "../Dispersion/ValidateBankInterbank",
                type: "POST",
                data: {},
                beforeSend: () => {
                    console.log('validando');
                }, success: (data) => {
                    console.log(data);
                    if (data.Bandera === true && data.MensajeError == "none") {
                        if (String(paramcode) == "INT") {
                            fProcessDepositsInterbank();
                        } else if (String(paramcode) == "NOM") {
                            fProcessDepositsPayroll();
                        } else {
                            alert('Accion invalida');
                        }
                    } else {
                        fShowTypeAlert('Atención!', 'No hay bancos interbancarios definidos, defina uno para continuar', 'warning', btndesplegartab, 0);
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

    // Funcion payroll
    fProcessDepositsPayroll = () => {
        try {
            document.getElementById('divbtndownzip').innerHTML = "";
            document.getElementById('div-controls').innerHTML  = "";
            const btnProcessDepositsPayroll   = document.getElementById('btn-process-deposits-payroll');
            const btnProcessDepositsInterbank = document.getElementById('btn-process-deposits-interbank');
            const dataSendProcessDepPayroll = {
                yearPeriod: parseInt(yeardis.value),
                numberPeriod: parseInt(periodis.value),
                typePeriod: parseInt(typeperiod.value),
                dateDeposits: datedis.value
            };
            $.ajax({
                url: "../Dispersion/ProcessDepositsPayroll",
                type: "POST",
                data: dataSendProcessDepPayroll,
                beforeSend: () => {
                    btnProcessDepositsPayroll.disabled   = true;
                    btnProcessDepositsInterbank.disabled = true;
                    document.getElementById('div-show-alert-loading').innerHTML = `
                        <div class="text-center">
                            <div class="spinner-grow text-info" role="status">
                                <span class="sr-only">Loading...</span>
                            </div>
                            <h6 class="font-weight-bold text-info">Generando...</h6>
                        </div>
                    `;
                }, success: (data) => {
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
                            document.getElementById('divbtndownzip').innerHTML = "";
                            document.getElementById('div-controls').innerHTML = "";
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
            const dataSend = {
                yearPeriod: parseInt(yeardis.value),
                numberPeriod: parseInt(periodis.value),
                typePeriod: parseInt(typeperiod.value),
                dateDeposits: datedis.value
            };
            $.ajax({
                url: "../Dispersion/ProcessDepositsInterbank",
                type: "POST",
                data: dataSend,
                beforeSend: () => {
                    btnProcessDepositsPayroll.disabled = true;
                    btnProcessDepositsInterbank.disabled = true;
                    document.getElementById('div-show-alert-loading').innerHTML = `
                        <div class="text-center">
                            <div class="spinner-grow text-info" role="status">
                                <span class="sr-only">Loading...</span>
                            </div>
                            <h6 class="font-weight-bold text-info">Generando...</h6>
                        </div>
                    `;
                }, success: (data) => {
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
});