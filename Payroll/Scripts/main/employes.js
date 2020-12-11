$(function () {

    fRandHours = (min, max) => {
        return Math.random() * (max - min) + min;
    }

    fReturnTime = (records) => {
        i = 0;
        while (i < records) {
            let hours    = parseInt(fRandHours(8, 10));
            let minutes  = parseInt(fRandHours(1, 30));
            let seconds  = parseInt(fRandHours(1, 60));
            let secondsF = (seconds >= 10) ? seconds : "0" + String(seconds);
            let minutesF = (minutes >= 10) ? minutes : "0" + String(minutes);
            console.log("0" + hours + ":" + minutesF + ":" + secondsF);
            i += 1;
        }
    }

    // fReturnTime(100);

    const divHistoryImss     = document.getElementById('div-history-imss');
    const divContentTabsImss = document.getElementById('div-content-tabs-imss');
    const divContentInfoImss = document.getElementById('div-content-info-imss');

    const divHistoryNomina     = document.getElementById('div-history-nomina');
    const divContentTabsNomina = document.getElementById('div-content-tabs-nomina');
    const divContentInfoNomina = document.getElementById('div-content-info-nomina');

    const divHistoryPosicion     = document.getElementById('div-history-posicion');
    const divContentTabsPosicion = document.getElementById('div-content-tabs-posicion');
    const divContentInfoPosicion = document.getElementById('div-content-info-posicion');

    // Funcion que muestra los botones del historial de cada apartado
    fShowBtnsHistoryApart = (param) => {
        if (param == "IMSS") {
            divHistoryImss.innerHTML = `<button onclick="fShowHistoryImss();" class="btn btn-sm btn-primary shadow rounded"> <i class="fas fa-book mr-2"></i>  Ver Historial</button>`;
        }
        if (param == "NOMINA") {
            divHistoryNomina.innerHTML = `<button onclick="fShowHistoryNomina();" class="btn btn-sm btn-primary shadow rounded"> <i class="fas fa-book mr-2"></i>  Ver Historial</button>`;
        }
        if (param == "POSICION") {
            divHistoryPosicion.innerHTML = `<button onclick="fShowHistoryPosicion();" class="btn btn-sm btn-primary shadow rounded"> <i class="fas fa-book mr-2"></i>  Ver Historial</button>`;
        }
    }

    if (localStorage.getItem("modeedit") != null) {
        fShowBtnsHistoryApart("IMSS");
        fShowBtnsHistoryApart("NOMINA");
        fShowBtnsHistoryApart("POSICION");
    }

    // Funcion que muestra el historial del Imss
    fShowHistoryImss = () => {
        divContentTabsImss.innerHTML = "";
        divContentInfoImss.innerHTML = "";
        try {
            let keyEmp = 0;
            if (JSON.parse(localStorage.getItem('objectTabDataGen')) != null) {
                const getDataTabDataGen = JSON.parse(localStorage.getItem('objectTabDataGen'));
                for (i in getDataTabDataGen) {
                    if (getDataTabDataGen[i].key === "general") {
                        keyEmp = getDataTabDataGen[i].data.clvemp;
                    }
                }
            }
            const key = "IMSS";
            if (keyEmp != 0) {
                $.ajax({
                    url: "../SearchDataCat/LoadHistoryImss",
                    type: "POST",
                    data: { key: String(key), keyEmployee: parseInt(keyEmp) },
                    beforeSend: () => {
                        console.log('Cargando historial....');
                    }, success: (data) => {
                        console.log(data);
                        if (data.Bandera === true && data.MensajeError == "none") {
                            $("#modalHistoryImss").modal("show");
                            setTimeout(() => {
                                let active1 = "";
                                let contac1 = 0;
                                for (let i = 0; i < data.Datos.length; i++) {
                                    if (contac1 == 0) {
                                        active1 = "active";
                                    } else {
                                        active1 = "";
                                    }
                                    divContentTabsImss.innerHTML += `
                                        <a class="nav-link ${active1} text-center" id="v-pills-home-tab" data-toggle="pill"
                                            href="#tab-imss${data.Datos[i].iIdImss}" role="tab" aria-controls="v-pills-home" aria-selected="true">
                                            <i class="fas fa-calendar-alt mr-2"></i> ${data.Datos[i].sFechaEfectiva}
                                        </a>
                                    `;
                                    contac1 += 1;
                                }
                                let active2 = "";
                                let contac2 = 0;
                                for (let i = 0; i < data.Datos.length; i++) {
                                    if (contac2 == 0) {
                                        active2 = "active";
                                    } else {
                                        active2 = "";
                                    }
                                    divContentInfoImss.innerHTML += `
                                        <div class="tab-pane fade ${active2} show border-left-primary p-1 shadow" id="tab-imss${data.Datos[i].iIdImss}" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                            <div class="row p-2 animated fadeInLeft">
                                                <div class="col-md-6">
                                                    <small class=""><b>Curp:</b>
                                                        <span class="text-primary text-uppercase">${data.Datos[i].sCurp}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6">
                                                    <small class="text-center"><b>Rfc:</b>
                                                        <span class="text-primary text-uppercase">${data.Datos[i].sRfc}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class="text-center"><b>Imss:</b>
                                                        <span class="text-primary">${data.Datos[i].sRegistroImss}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small classs="text-center"><b>Nivel Estudio:</b>
                                                        <span class="text-primary">${data.Datos[i].sNivelEstudio}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small classs="text-center"><b>Nivel Socioeconomico:</b>
                                                        <span class="text-primary">${data.Datos[i].sNivelSocieconomico}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small classs="text-center"><b>Alta:</b>
                                                        <span class="text-primary">${data.Datos[i].sFechaAlta}</span>
                                                    </small>
                                                </div>
                                            </div>
                                        </div>
                                    `; 
                                    contac2 += 1;
                                }
                            }, 500);
                        } else {
                            alert('Ocurrio un problema al cargar el historial');
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                alert('Ocurrio un error al cargar el historial Imss del Empleado');
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


    // Funcion que muestra el historial de la nomina
    fShowHistoryNomina = () => {
        divContentTabsNomina.innerHTML = "";
        divContentInfoNomina.innerHTML = "";
        try {
            let keyEmp = 0;
            if (JSON.parse(localStorage.getItem("objectTabDataGen")) != null) {
                const getDataTabDataGen = JSON.parse(localStorage.getItem("objectTabDataGen"));
                for (i in getDataTabDataGen) {
                    if (getDataTabDataGen[i].key === "general") {
                        keyEmp = getDataTabDataGen[i].data.clvemp;
                    }
                }
            }
            const key = "NOMINA";
            if (keyEmp != 0) {
                $.ajax({
                    url: "../SearchDataCat/LoadHistoryNomina",
                    type: "POST",
                    data: { key: String(key), keyEmployee: parseInt(keyEmp) },
                    beforeSend: () => {
                        console.log('Cargando historial....');
                    }, success: (data) => {
                        console.log(data);
                        if (data.Bandera === true && data.MensajeError == "none") {
                            $("#modalHistoryNomina").modal("show");
                            setTimeout(() => {
                                let active1 = "";
                                let contac1 = 0;
                                for (let i = 0; i < data.Datos.length; i++) {
                                    if (contac1 == 0) {
                                        active1 = "active";
                                    } else {
                                        active1 = "";
                                    }
                                    divContentTabsNomina.innerHTML += `
                                        <a class="nav-link ${active1} text-center" id="v-pills-home-tab" data-toggle="pill"
                                            href="#tab-nomina${data.Datos[i].iIdNomina}" role="tab" aria-controls="v-pills-home" aria-selected="true">
                                            <i class="fas fa-calendar-alt mr-2"></i> ${data.Datos[i].sFechaEfectiva}
                                        </a>
                                    `;
                                    contac1 += 1;
                                }
                                let active2 = "";
                                let contac2 = 0;
                                for (let i = 0; i < data.Datos.length; i++) {
                                    if (contac2 == 0) {
                                        active2 = "active";
                                    } else {
                                        active2 = "";
                                    }
                                    divContentInfoNomina.innerHTML += `
                                        <div class="tab-pane fade ${active2} show border-left-primary p-1 shadow" id="tab-nomina${data.Datos[i].iIdNomina}" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                            <div class="row p-2 animated fadeInLeft">
                                                <div class="col-md-6">
                                                    <small class=""><b>Periodo:</b>
                                                        <span class="text-primary">${data.Datos[i].sPeriodo}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6">
                                                    <small class=""><b>Salario M:</b>
                                                        <span class="text-primary">$ ${data.Datos[i].sSalarioMensual}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Tipo Empleado:</b>
                                                        <span class="text-primary">${data.Datos[i].sTipoEmpleado}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Nivel Empleado:</b>
                                                        <span class="text-primary">${data.Datos[i].sNivelEmpleado}</span>
                                                    </small>
                                                </div>
                                                 <div class="col-md-6 mt-2">
                                                    <small class=""><b>Tipo Jornada:</b>
                                                        <span class="text-primary">${data.Datos[i].sTipoJornada}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Ingreso:</b>
                                                        <span class="text-primary">${data.Datos[i].sFechaIngreso}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Antiguedad:</b>
                                                        <span class="text-primary">${data.Datos[i].sFechaAntiguedad}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Vencimiento contrato:</b>
                                                        <span class="text-primary">${data.Datos[i].sVencimientoContrato}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Tipo Pago:</b>
                                                        <span class="text-primary">${data.Datos[i].sTipoPago}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Banco:</b>
                                                        <span class="text-primary">${data.Datos[i].sBanco}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Cuenta Cheques:</b>
                                                        <span class="text-primary">${data.Datos[i].sCuentaCheques}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Alta:</b>
                                                        <span class="text-primary">${data.Datos[i].sFechaAlta}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-12 mt-2">
                                                    <small class=""><b>Tipo Contratacion:</b>
                                                        <span class="text-primary">${data.Datos[i].sTipoContratacion}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-12 mt-2">
                                                    <small class=""><b>Tipo Contrato:</b>
                                                        <span class="text-primary">${data.Datos[i].sTipoContrato}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Tipo Sueldo:</b>
                                                        <span class="text-primary">${data.Datos[i].sTipoSueldo}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Politica:</b>
                                                        <span class="text-primary">${data.Datos[i].iPolitica}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Diferencia P:</b>
                                                        <span class="text-primary">${data.Datos[i].dDiferencia}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Transporte:</b>
                                                        <span class="text-primary">${data.Datos[i].dTransporte}</span>
                                                    </small>
                                                </div>
                                            </div>
                                        </div>
                                    `;
                                    contac2 += 1;
                                }
                            }, 500);
                        } else {
                            alert('Ocurrio un problema al cargar el historial');
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                alert('Ocurrio un error al cargar el Historial Nomina del empleado');
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

    // Funcion que muestra el historial de la posicion
    fShowHistoryPosicion = () => {
        divContentTabsPosicion.innerHTML = "";
        divContentInfoPosicion.innerHTML = "";
        try {
            let keyEmp = 0;
            if (JSON.parse(localStorage.getItem('objectTabDataGen')) != null) {
                const getDataTabDataGen = JSON.parse(localStorage.getItem('objectTabDataGen'));
                for (i in getDataTabDataGen) {
                    if (getDataTabDataGen[i].key === "general") {
                        keyEmp = getDataTabDataGen[i].data.clvemp;
                    }
                }
            }
            const key = "POSICION";
            if (keyEmp != 0) {
                $.ajax({
                    url: "../SearchDataCat/LoadHistoryPosicion",
                    type: "POST",
                    data: { key: String(key), keyEmployee: parseInt(keyEmp) },
                    beforeSend: () => {
                        console.log('Cargando historial....');
                    }, success: (data) => {
                        console.log(data);
                        if (data.Bandera === true && data.MensajeError == "none") {
                            $("#modalHistoryPosicion").modal("show");
                            setTimeout(() => {
                                let active1 = "";
                                let contac1 = 0;
                                for (let i = 0; i < data.Datos.length; i++) {
                                    if (contac1 == 0) {
                                        active1 = "active";
                                    } else {
                                        active1 = "";
                                    }
                                    divContentTabsPosicion.innerHTML += `
                                        <a class="nav-link ${active1} text-center" id="v-pills-home-tab" data-toggle="pill"
                                            href="#tab-posicion${data.Datos[i].iIdPosicion}" role="tab" aria-controls="v-pills-home" aria-selected="true">
                                            <i class="fas fa-calendar mr-2"></i> ${data.Datos[i].sFechaEffectiva}
                                        </a>
                                    `;
                                    contac1 += 1;
                                }
                                let active2 = "";
                                let contac2 = 0;
                                for (let i = 0; i < data.Datos.length; i++) {
                                    if (contac2 == 0) {
                                        active2 = "active";
                                    } else {
                                        active2 = "";
                                    }
                                    divContentInfoPosicion.innerHTML += `
                                        <div class="tab-pane fade ${active2} show border-left-primary p-1 shadow" id="tab-posicion${data.Datos[i].iIdPosicion}" role="tabpanel" aria-labelledby="v-pills-home-tab">
                                            <div class="row p-2 animated fadeInLeft">
                                                <div class="col-md-6">
                                                    <small class=""><b>Posicion código:</b>
                                                        <span class="text-primary">${data.Datos[i].sPosicionCodigo}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-12 mt-2">
                                                    <small class=""><b>Departamento:</b>
                                                        <span class="text-primary">${data.Datos[i].sNombreDepartamento}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-12 mt-2">
                                                    <small class=""><b>Puesto:</b>
                                                        <span class="text-primary">${data.Datos[i].sNombrePuesto}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Localidad:</b>
                                                        <span class="text-primary">${data.Datos[i].sLocalidad}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Afiliacion IMSS:</b>
                                                        <span class="text-primary">${data.Datos[i].sRegistroPat}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Reporta A:</b>
                                                        <span class="text-primary">${data.Datos[i].sNombreE}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <small class=""><b>Reporta posicion:</b>
                                                        <span class="text-primary">${data.Datos[i].iIdReportaAPosicion}</span>
                                                    </small>
                                                </div>
                                                <div class="col-md-12 mt-2">
                                                    <small class=""><b>Fecha Alta:</b>
                                                        <span class="text-primary">${data.Datos[i].sFechaInicio}</span>
                                                    </small>
                                                </div>
                                            </div>
                                        </div>
                                    `;
                                    contac2 += 1;
                                }
                            }, 500);
                        } else {
                            alert('Ocurrio un problema al cargar el historial');
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                alert('Ocurrio un error al cargar el historial de Posiciones del Empleado');
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

    const idefectivo = 218;
    const idcuentach = 219;
    const idcajeroau = 220;
    const idcuentaah = 221;
    /*
     * Variables apartado datos generales
     */
    const clvemp = document.getElementById('clvemp');
    const name = document.getElementById('name');
    const apepat = document.getElementById('apepat');
    const apemat = document.getElementById('apemat');
    const sex = document.getElementById('sex');
    const estciv = document.getElementById('estciv');
    const fnaci = document.getElementById('fnaci');
    const lnaci = document.getElementById('lnaci');
    const title = document.getElementById('title');
    const nacion = document.getElementById('nacion');
    const state = document.getElementById('state');
    const codpost = document.getElementById('codpost');
    const city = document.getElementById('city');
    const colony = document.getElementById('colony');
    const street = document.getElementById('street');
    const numberst = document.getElementById('numberst');
    const telfij = document.getElementById('telfij');
    const telmov = document.getElementById('telmov');
    const mailus = document.getElementById('mailus');
    const tipsan = document.getElementById('tipsan');
    const fecmat = document.getElementById('fecmat');
    const btnsaveeditdatagen = document.getElementById('btn-save-edit-data-gen');
    const btnsavedatagen = document.getElementById('btn-save-data-gen');
    /*
     * VARIABLES IMSS 
     */
    const clvimss = document.getElementById('clvimss');
    const fechefecactimss = document.getElementById('fechefecactimss');
    const fecefe = document.getElementById('fecefe');
    const regimss = document.getElementById('regimss');
    const rfc = document.getElementById('rfc');
    const curp = document.getElementById('curp');
    const nivest = document.getElementById('nivest');
    const nivsoc = document.getElementById('nivsoc');
    const btnsaveeditdataimss = document.getElementById('btn-save-edit-data-imss');
    const btnsavedataimss = document.getElementById('btn-save-data-imss');
    /*
     * Variables del apartado datos de nomina
     */
    const clvnom = document.getElementById('clvnom');
    const fechefectact = document.getElementById('fechefectact');
    const fecefecnom = document.getElementById('fecefecnom');
    const salmen = document.getElementById('salmen');
    const salmenact = document.getElementById('salmenact');
    const tipper = document.getElementById('tipper');
    const tipemp = document.getElementById('tipemp');
    const nivemp = document.getElementById('nivemp');
    const tipjor = document.getElementById('tipjor');
    const tipcon = document.getElementById('tipcon');
    const fecing = document.getElementById('fecing');
    const fecant = document.getElementById('fecant');
    const vencon = document.getElementById('vencon');
    const tipcontra  = document.getElementById('tipcontra');
    const tiposueldo = document.getElementById('tiposueldo');
    const politica   = document.getElementById('politica');
    const diferencia = document.getElementById('diferencia');
    const transporte = document.getElementById('transporte');
    const retroactivo = document.getElementById('retroactivo');
    const tippag = document.getElementById('tippag');
    const banuse = document.getElementById('banuse');
    const cunuse = document.getElementById('cunuse');
    const clvstr = document.getElementById('clvstr');
    const btnsaveeditdatanomina = document.getElementById('btn-save-edit-data-nomina');
    const btnsavedatanomina     = document.getElementById('btn-save-data-nomina');
    /*
     * Variables apartado estructura
     */
    const clvstract = document.getElementById('clvstract');
    const clvposasig = document.getElementById('clvposasig');
    const fechefecposact = document.getElementById('fechefecposact');
    const fechefectpos = document.getElementById('fechefectpos');
    const fechinipos = document.getElementById('fechinipos');
    const numpla = document.getElementById('numpla');
    const depaid = document.getElementById('depaid');
    const depart = document.getElementById('depart');
    const puesid = document.getElementById('puesid');
    const pueusu = document.getElementById('pueusu');
    const localty = document.getElementById('localty');
    const emprep = document.getElementById('emprep');
    const report = document.getElementById('report');
    const btnsavedataall = document.getElementById('btn-save-data-all');
    const btnsaveeditdataest = document.getElementById('btn-save-edit-dataest');
    /* CONSTANTES BUSQUEDA EN TIEMPO REAL DE LOS EMPLEADOS */
    const searchemployekey = document.getElementById('searchemployekey');
    const resultemployekey = document.getElementById('resultemployekey');
    const labsearchemp     = document.getElementById('labsearchemp');
    const filtronumber     = document.getElementById('filtronumber');
    const filtroname       = document.getElementById('filtroname');
    /* CONSTANTES BOTONES DE LA VENTANA MODAL DE BUSQUEDA DE EMPLEADOS */
    const btnmodalsearchemploye = document.getElementById('btn-modal-search-employe');
    const icoclosesearchemployesbtn = document.getElementById('ico-close-searchemployes-btn');
    const btnclosesearchemployesbtn = document.getElementById('btn-close-searchemployes-btn');
    /* EJECUCION DE EVENTO QUE ACTIVA EL CAMPOS DE BUSQUEDA DE EMPLEADOS */
    btnmodalsearchemploye.addEventListener('click', () => {
        setTimeout(() => { searchemployekey.focus(); }, 1000);
    });
    /* FUNCION QUE LIMPIA LA CAJA DE BUSQUEDA DE EMPLEADOS Y LA LISTA DE LOS RESULTADOS */
    fclearsearchresults = () => {
        searchemployekey.value = '';
        resultemployekey.innerHTML = '';
        document.getElementById('noresultssearchemployees').innerHTML = "";
    }
    /* EJECUCION DE FUNCION QUE LIMPIA LA CAJA DE BUSQUEDA Y LA LISTA DE RESULTADOS */
    icoclosesearchemployesbtn.addEventListener('click', fclearsearchresults);
    btnclosesearchemployesbtn.addEventListener('click', fclearsearchresults);
    /* CONSTANTES ALMACENA EL TAB DE LAS PESTAÑAS */
    const navDataGenTab = document.getElementById('nav-datagen-tab'),
        navImssTab = document.getElementById('nav-imss-tab'),
        navDataNomTab = document.getElementById('nav-datanom-tab'),
        navEstructureTab = document.getElementById('nav-estructure-tab');
    /* FUNCION QUE EJECUTA UN SP PARA ACTUALIZAR LA POSICION DEL EMPLEADO EN TB -> EMPLEADO_NOMINA */
    fupdateposnew = () => {
        try {
            if (clvemp.value != "" && clvemp.value > 0) {
                $.ajax({
                    url: "../Empleados/UpdatePosicionAct",
                    type: "POST",
                    data: { clvemp: clvemp.value },
                    success: (data) => {
                        if (data.result == "success") {
                            floaddatatabgeneral(data.empleado);
                        }
                    }, complete: (comp) => {
                        //console.log(comp);
                        //console.log('Finalizado');
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            }
        } catch (error) {
            if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }
    /* EJECUCION DE FUNCTION QUE ACTUALIZA LA POSICION */
    fupdateposnew();
    /* VARIABLES ALMACENA LOCAL STORAGE */
    let objectDataTabDataGen = {},
        objectDataTabImss = {},
        objectDataTabNom = {},
        objectDataTabEstructure = {};
    /* FUNCION QUE CREA UN LOCAL STORAGE DEL EMPLEADO A EDITAR DATOS DE ESTRUCTURA */
    flocalstodatatabstructure = () => {
        const dataLocSto = {
            key: 'estructure', data: {
                clvstract: clvstract.value,
                //clvposasig: clvposasig.value,
                clvstr: clvstr.value,
                numpla: numpla.value,
                depaid: depaid.value,
                depart: depart.value,
                puesid: puesid.value,
                pueusu: pueusu.value,
                localty: localty.value,
                emprep: emprep.value,
                report: report.value,
                fechefectpos: fechefectpos.value,
                //fechinipos: fechinipos.value,
                fechefecposact: fechefecposact.value
            }
        };
        objectDataTabEstructure.dataestructure = dataLocSto;
        localStorage.setItem('objectDataTabEstructure', JSON.stringify(objectDataTabEstructure));
        localStorage.setItem('tabSelected', 'dataestructure');
    }
    /* FUNCION QUE CREA UN LOCAL STORAGE DEL EMPLEADO A EDITAR DATOS DE NOMINA */
    flocalstodatatabnomina = () => {
        let retroactivoSave = 0;
        if (retroactivo.checked) {
            retroactivoSave = 1;
        }
        const dataLocSto = {
            key: 'nom', data: {
                clvnom: clvnom.value, fechefectact: fechefectact.value,
                fecefecnom: fecefecnom.value,
                salmen: salmen.value, salmenact: salmen.value, tipper: tipper.value,
                tipemp: tipemp.value, nivemp: nivemp.value,
                tipjor: tipjor.value, tipcon: tipcon.value,
                tipcontra: tipcontra.value,
                tiposueldo: tiposueldo.value,
                politica: politica.value,
                diferencia: diferencia.value,
                transporte: transporte.value,
                retroactivo: retroactivoSave,
                fecing: fecing.value,
                fecant: fecant.value,
                vencon: vencon.value,
                tippag: tippag.value,
                banuse: banuse.value,
                cunuse: cunuse.value,
                ultSdi: document.getElementById('view-ultSdi').value
            }
        };
        objectDataTabNom.datanom = dataLocSto;
        localStorage.setItem('objectDataTabNom', JSON.stringify(objectDataTabNom));
        localStorage.setItem('tabSelected', 'dataestructure');
        navEstructureTab.classList.remove('disabled');
    }
    /* FUNCION QUE CREA UN LOCAL STORAGE DEL EMPLEADO A EDITAR DATOS GENERALES */
    flocalstodatatabgen = () => {
        const dataLocStoGen = {
            key: 'general', data: {
                clvemp: clvemp.value,
                name: name.value, apep: apepat.value,
                apem: apemat.value, fnaci: fnaci.value,
                lnaci: lnaci.value, title: title.value,
                sex: sex.value, nacion: nacion.value,
                estciv: estciv.value, codpost: codpost.value,
                state: state.value, city: city.value,
                colony: colony.value, street: street.value,
                numberst: numberst.value, telfij: telfij.value,
                telmov: telmov.value, mailus: mailus.value,
                tipsan: tipsan.value, fecmat: fecmat.value
            }
        };
        document.getElementById('icouser').classList.remove('d-none');
        document.getElementById('nameuser').textContent = clvemp.value + " - " + name.value + " " + apepat.value + " " + apemat.value + ".";
        objectDataTabDataGen.datagen = dataLocStoGen;
        localStorage.setItem('objectTabDataGen', JSON.stringify(objectDataTabDataGen));
        localStorage.setItem('tabSelected', 'imss');
        navImssTab.classList.remove('disabled');
        flocalstodatatabimss();
    }
    /* FUNCION QUE CREA UN LOCAL STORAGE DEL EMPLEADO A EDITAR e */
    flocalstodatatabimss = () => {
        const dataLocSto = {
            key: 'imss', data: {
                clvimss: clvimss.value,
                fechefecactimss: fechefecactimss.value,
                fecefe: fecefe.value,
                imss: regimss.value,
                rfc: rfc.value,
                curp: curp.value,
                nivest: nivest.value,
                nivsoc: nivsoc.value,
            }
        };
        objectDataTabImss.dataimss = dataLocSto;
        localStorage.setItem('objectDataTabImss', JSON.stringify(objectDataTabImss));
        localStorage.setItem('tabSelected', 'datanom');
        navDataNomTab.classList.remove('disabled');
    }
    /* FUNCION QUE CARGA LOS DATOS DE LA POSICION ASIGNADA A LA ESTRUCTURA */
    floaddatatabstructure = (paramid) => {
        try {
            $.ajax({
                url: "../Empleados/DataTabStructureEmploye",
                type: "POST",
                data: { keyemploye: paramid },
                success: (data) => {
                    if (data.Bandera === true && data.MensajeError === "none") {
                        clvstract.value = data.Datos.iIdPosicion;
                        numpla.value    = data.Datos.sPosicionCodigo;
                        puesid.value    = data.Datos.iPuesto_id;
                        pueusu.value    = data.Datos.sNombrePuesto;
                        depaid.value    = data.Datos.iDepartamento_id;
                        depart.value    = data.Datos.sDeptoCodigo;
                        localty.value   = data.Datos.sLocalidad;
                        emprep.value    = data.Datos.sRegistroPat;
                        report.value    = data.Datos.iIdReportaAPosicion;
                        fechefectpos.value   = data.Datos.sFechaEffectiva;
                        fechefecposact.value = data.Datos.sFechaEffectiva;
                        document.getElementById('btn-save-data-all').disabled = true;
                        flocalstodatatabstructure();
                        fchecklocalstotab();
                        fShowBtnsHistoryApart("POSICION");
                        if (localStorage.getItem('modeedit') != null) {
                            btnsavedataall.classList.add('d-none');
                            btnsaveeditdataest.classList.remove('d-none');
                            document.getElementById('content-new-inpt-fechmovi').classList.remove('d-none');
                            document.getElementById('content-new-inpt-motmovi').classList.remove('d-none');
                            document.getElementById('content-new-inpt-fechmovi').innerHTML = `
                                <label for="fechmovi" class="col-sm-4 col-form-label font-labels col-ico font-weight-bold">
                                    Fecha de movimiento
                                </label>
                                <div class="col-sm-8">
                                    <input type="date" id="fechmovi" class="form-control form-control-sm" placeholder="Fecha del movimiento" />
                                </div>
                            `;
                            document.getElementById('content-new-inpt-motmovi').innerHTML = `
                                <label for="motmovi" class="col-sm-4 col-form-label font-labels col-ico font-weight-bold" id="label-motmovi">
                                    Motivo del movimiento
                                </label>
                                <div class="col-sm-8">
                                    <input type="text" id="motmovi" class="form-control form-control-sm" placeholder="Motivo del movimiento"/>
                                </div>
                            `;
                        }
                    } else {
                        document.getElementById('div-most-alert-data-estructure').innerHTML = `
                            <div class="alert alert-danger text-center" role="alert">
                                <b>
                                    <i class="fas fa-times-circle mr-2"></i> No se cargaron los datos de la estructura, informe al área de TI.
                                    <br/>
                                    <i class="fas fa-code mr-2"></i> Error: ${data.MensajeError}
                                </b>
                            </div>
                        `;
                    }
                }, error: (jqXHR, exception) => { fcaptureaerrorsajax(jqXHR, exception); }
            });
        } catch (error) {
            if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }
    /* FUNCION QUE CARGA LOS DATOS DE NOMINA DEL EMPLEADO SELECCIONADO A EDICION */
    floaddatatabnomina = (paramid) => {
        try {
            $.ajax({
                url: "../Empleados/DataTabNominaEmploye",
                type: "POST",
                data: { keyemploye: paramid },
                success: (data) => {
                    console.log(data);
                    let retroactivoShow = 0;
                    if (data.Bandera === true && data.MensajeError === "none") {
                        clvnom.value       = data.Datos.iIdNomina;
                        fechefectact.value = data.Datos.sFechaEfectiva;
                        fecefecnom.value   = data.Datos.sFechaEfectiva;
                        salmen.value       = data.Datos.dSalarioMensual;
                        salmenact.value    = data.Datos.dSalarioMensual;
                        tipper.value       = data.Datos.iTipoPeriodo;
                        tipemp.value       = data.Datos.iTipoEmpleado_id;
                        nivemp.value       = data.Datos.iNivelEmpleado_id;
                        tipjor.value       = data.Datos.iTipoJornada_id;
                        tipcon.value       = data.Datos.iTipoContrato_id;
                        tipcontra.value    = data.Datos.iTipoContratacion_id;
                        tiposueldo.value   = data.Datos.iTipoSueldo_id;
                        politica.value     = data.Datos.iPolitica;
                        diferencia.value   = data.Datos.dDiferencia;
                        transporte.value   = data.Datos.dTransporte;
                        if (data.Datos.iRetroactivo == 1) {
                            retroactivo.checked = 1;
                        } else {
                            retroactivo.checked = 0;
                        }
                        fecing.value       = data.Datos.sFechaIngreso;
                        fecant.value       = data.Datos.sFechaAntiguedad;
                        vencon.value       = data.Datos.sVencimientoContrato;
                        tippag.value       = data.Datos.iTipoPago_id;
                        banuse.value       = data.Datos.iBanco_id;
                        cunuse.value       = data.Datos.sCuentaCheques;
                        clvstr.value       = data.Datos.iPosicion_id;
                        if (data.Datos.iBanco_id != 999) {
                            fdatabank(false);
                        }
                        floaddatatabstructure(paramid);
                        fShowBtnsHistoryApart("NOMINA");
                        if (localStorage.getItem('modeedit') != null) {
                            btnsavedatanomina.classList.add('d-none');
                            btnsaveeditdatanomina.classList.remove('d-none');
                            document.getElementById('content-new-inpt-movsal').classList.remove('d-none');
                            document.getElementById('content-new-inpt-fecsal').classList.remove('d-none');
                            document.getElementById('content-new-inpt-ultsdi').classList.remove('d-none');
                            document.getElementById('content-new-inpt-movsal').innerHTML = `
                                <label for="motmovisal" class="col-sm-4 col-form-label font-labels col-ico font-weight-bold" id="label-motmovi">
                                    Motivo del movimiento
                                </label>
                                <div class="col-sm-8">
                                    <input type="text" id="motmovisal" class="form-control form-control-sm" placeholder="Motivo del movimiento"/>
                                </div>
                            `;
                            document.getElementById('content-new-inpt-fecsal').innerHTML = `
                                <label for="fechmovisal" class="col-sm-4 col-form-label font-labels col-ico font-weight-bold">
                                    Fecha de movimiento
                                </label>
                                <div class="col-sm-8">
                                    <input type="date" id="fechmovisal" class="form-control form-control-sm" placeholder="Fecha del movimiento" />
                                </div>
                            `;
                            document.getElementById('content-new-inpt-ultsdi').innerHTML = `
                                <label class="col-sm-4 col-form-label font-labels col-ico font-weight-bold">
                                    Ultimo sdi
                                </label>
                                <div class="col-sm-8">
                                    <input id="view-ultSdi" type="number" value="${data.Datos.sUlt_sdi}"  class="form-control form-control-sm" disabled />
                                </div>
                            `;
                        }
                        flocalstodatatabnomina();
                    } else {
                        document.getElementById('div-most-alert-data-imss').innerHTML = `
                            <div class="alert alert-danger text-center" role="alert">
                                <b>
                                    <i class="fas fa-times-circle mr-2"></i> No se cargaron los datos de la nomina, informe al área de TI.
                                    <br/>
                                    <i class="fas fa-code mr-2"></i> Error: ${data.MensajeError}
                                </b>
                            </div>
                        `;
                    }
                }, error: (jqXHR, exception) => { fcaptureaerrorsajax(jqXHR, exception); }
            });
        } catch (error) {
            if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }
    /* FUNCION QUE CARGA LOS DATOS DEL IMSS DEL EMPLEADO SELECCIONADO A EDICION */
    floaddatatabimss = (paramid) => {
        try {
            $.ajax({
                url: "../Empleados/DataTabImssEmploye",
                type: "POST",
                data: { keyemploye: paramid },
                success: (data) => {
                    if (localStorage.getItem('modeedit') != null) {
                        btnsaveeditdataimss.classList.remove('d-none');
                        btnsavedataimss.classList.add('d-none');
                    }
                    if (data.Bandera === true && data.MensajeError === "none") {
                        clvimss.value         = data.Datos.iIdImss;
                        fechefecactimss.value = data.Datos.sFechaEfectiva;
                        fecefe.value          = data.Datos.sFechaEfectiva;
                        regimss.value         = data.Datos.sRegistroImss;
                        rfc.value             = data.Datos.sRfc;
                        curp.value            = data.Datos.sCurp;
                        nivest.value          = data.Datos.iNivelEstudio_id;
                        nivsoc.value          = data.Datos.iNivelSocioeconomico_id;
                        flocalstodatatabimss();
                        floaddatatabnomina(paramid);
                        fShowBtnsHistoryApart('IMSS');
                    } else {
                        document.getElementById('div-most-alert-data-imss').innerHTML = `
                            <div class="alert alert-danger text-center" role="alert">
                                <b>
                                    <i class="fas fa-times-circle mr-2"></i> No se cargaron los datos del imss, informe al área de TI.
                                    <br/>
                                    <i class="fas fa-code mr-2"></i> Error: ${data.MensajeError}
                                </b>
                            </div>
                        `;
                    }
                }, error: (jqXHR, exception) => { fcaptureaerrorsajax(jqXHR, exception); }
            });
        } catch (error) {
            if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }
    fvalidatebuttonsaction = () => {
        if (localStorage.getItem("modeedit") != null) {
            btnsaveeditdatagen.classList.remove('d-none');
            btnsavedatagen.classList.add('d-none');
            btnsaveeditdataimss.classList.remove('d-none');
            btnsavedataimss.classList.add('d-none');
            btnsavedatanomina.classList.add('d-none');
            btnsaveeditdatanomina.classList.remove('d-none');
            btnsavedataall.classList.add('d-none');
            btnsaveeditdataest.classList.remove('d-none');
        }
    }
    fvalidatebuttonsaction();
    /* FUNCION QUE CARGA LOS DATOS GENERALES DEL EMPLEADO SELECCIONADO A EDICION */
    floaddatatabgeneral = (paramid) => {
        document.getElementById('div-most-alert-data-gen').innerHTML = "";
        try {
            $.ajax({
                url: "../Empleados/DataTabGenEmploye",
                type: "POST",
                data: { keyemploye: paramid },
                success: (data) => {
                    if (localStorage.getItem("modeedit") != null) {
                        btnsaveeditdatagen.classList.remove('d-none');
                        btnsavedatagen.classList.add('d-none');
                    }
                    if (data.MensajeError === "none" && data.Bandera === true) {
                        clvemp.value   = data.Datos.iIdEmpleado;
                        name.value     = data.Datos.sNombreEmpleado;
                        apepat.value   = data.Datos.sApellidoPaterno;
                        apemat.value   = data.Datos.sApellidoMaterno;
                        fnaci.value    = data.Datos.sFechaNacimiento;
                        lnaci.value    = data.Datos.sLugarNacimiento;
                        title.value    = data.Datos.iTitulo_id;
                        sex.value      = data.Datos.iGeneroEmpleado;
                        nacion.value   = data.Datos.iNacionalidad;
                        estciv.value   = data.Datos.iEstadoCivil;
                        codpost.value  = data.Datos.sCodigoPostal;
                        state.value    = data.Datos.iEstado_id;
                        city.value     = data.Datos.sCiudad;
                        street.value   = data.Datos.sCalle;
                        numberst.value = data.Datos.sNumeroCalle;
                        telfij.value   = data.Datos.sTelefonoFijo;
                        telmov.value   = data.Datos.sTelefonoMovil;
                        mailus.value   = data.Datos.sCorreoElectronico;
                        tipsan.value   = data.Datos.sTipoSangre;
                        fecmat.value   = data.Datos.sFechaMatrimonio;
                        fvalidatestatecodpost(0, data.Datos.iEstado_id);
                        setTimeout(() => {
                            colony.value = data.Datos.sColonia.toUpperCase();
                            floaddatatabimss(paramid);
                            flocalstodatatabimss();
                            flocalstodatatabgen();
                        }, 2000);
                    } else {
                        document.getElementById('div-most-alert-data-gen').innerHTML = `
                            <div class="alert alert-danger text-center" role="alert">
                                <b>
                                    <i class="fas fa-times-circle mr-2"></i> No se cargaron los datos generales, informe al área de TI.
                                    <br/>
                                    <i class="fas fa-code mr-2"></i> Error: ${data.MensajeError}
                                </b>
                            </div>
                        `;
                    }
                    setTimeout(() => {
                        document.getElementById('body-init').style.paddingRight = '0px';
                        //document.getElementById('body-init').removeAttribute("style");
                    }, 2000);
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        } catch (error) {
            if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }
    const date    = new Date();
    const dayG    = (date.getDate() < 10) ? "0" + date.getDate() : date.getDate();
    const monthG  = ((date.getMonth() + 1) < 10) ? "0" + (date.getMonth() + 1) : date.getMonth();
    const fechAct = date.getFullYear() + "-" + monthG + "-" + dayG;
    fechinipos.value = fechAct;
    /* FUNCION QUE CARGA LOS DATOS DEL EMPLEADO SELECCIONADO */
    fselectemploye = (paramid, paramstr) => {
        Swal.fire({
            title: 'Esta seguro?', text: 'De editar a: ' + paramstr + '?', icon: 'question',
            showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
            confirmButtonText: "Aceptar", showCancelButton: true, cancelButtonText: "Cancelar",
            allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
        }).then((acepta) => {
            if (acepta.value) {
                searchemployekey.value = '';
                resultemployekey.innerHTML = '';
                $("#searchemploye").modal('hide');
                localStorage.removeItem('tabSelected');
                localStorage.removeItem('objectTabDataGen');
                localStorage.removeItem('objectDataTabImss');
                localStorage.removeItem('objectDataTabNom');
                localStorage.removeItem('objectDataTabEstructure');
                let timerInterval;
                //fvalidatebuttonsaction();
                Swal.fire({
                    title: 'Cargando información',
                    html: 'Terminando en <b></b> milisegundos.',
                    timer: 5000, timerProgressBar: true,
                    allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                    onBeforeOpen: () => {
                        Swal.showLoading();
                        timerInterval = setInterval(() => {
                            Swal.getContent().querySelector('b').textContent = Swal.getTimerLeft();
                        }, 100)
                    },
                    onClose: () => { clearInterval(timerInterval); }
                }).then((result) => {
                    if (result.dismiss === Swal.DismissReason.timer) {
                        floaddatatabgeneral(paramid);
                        localStorage.setItem('modeedit', 1);
                        const date = new Date();
                        let fechAct;
                        let day = date.getDate();
                        if (date.getDate() < 10) {
                            day = "0" + date.getDate();
                        }
                        if (date.getMonth() + 1 < 10) {
                            fechAct = date.getFullYear() + "-" + "0" + (date.getMonth() + 1) + "-" + day;
                        } else {
                            fechAct = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + day;
                        }
                        localStorage.setItem('dateedit', fechAct);
                        setTimeout(() => {
                            $("#nav-datagen-tab").click();
                        }, 1000);
                    }
                })
            }
        });
    }
    /* FUNCION QUE CARGA LOS DATOS DEL EMPLEADO SELECCIONADO EN UN REPORTE */
    fviewdetailsemploye = (paramid) => {
        alert('Listo para generar reporte de ' + String(paramid));
        console.log(paramid)
    }
    /* FUNCION QUE COMPRUEBA QUE TIPO DE FILTRO SE APLICARA A LA BUSQUEDA DE EMPLEADOS */
    fselectfilterdsearchemploye = () => {
        const filtered = $("input:radio[name=filtroemp]:checked").val();
        if (filtered == "numero") {
            searchemployekey.placeholder = "NOMINA DEL EMPLEADO";
            labsearchemp.textContent     = "Nomina";
        } else if (filtered == "nombre") {
            searchemployekey.placeholder = "NOMBRE DEL EMPLEADO";
            labsearchemp.textContent     = "Nombre";
        }
        searchemployekey.value     = "";
        resultemployekey.innerHTML = "";
        setTimeout(() => { searchemployekey.focus() }, 500);
    }

    searchemployekey.style.transition = "1s";
    searchemployekey.style.cursor     = "pointer";
    filtroname.style.cursor           = "pointer";
    filtronumber.style.cursor         = "pointer";
    document.getElementById('labelfiltronumber').style.cursor   = "pointer";
    document.getElementById('labelfiltroname').style.cursor     = "pointer";
    document.getElementById('labelsearchemployee').style.cursor = "pointer";
    searchemployekey.addEventListener('mouseover', () => { searchemployekey.classList.add('shadow'); });
    searchemployekey.addEventListener('mouseleave', () => { searchemployekey.classList.remove('shadow'); });

    /* EJECUCION DE FUNCION QUE APLICA FILTRO A LA BUSQUEDA DE LOS EMPLEADOS */
    filtroname.addEventListener('click', fselectfilterdsearchemploye);
    filtronumber.addEventListener('click', fselectfilterdsearchemploye);
    /* FUNCION QUE EJECUTA LA BUSUQEDA REAL DE LOS EMPLEADOS */
    fsearchemployes = () => {
        const filtered = $("input:radio[name=filtroemp]:checked").val();
        try {
            resultemployekey.innerHTML = '';
            document.getElementById('noresultssearchemployees').innerHTML = "";
            if (searchemployekey.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchEmploye",
                    type: "POST",
                    data: { wordsearch: searchemployekey.value, filtered: filtered.trim() },
                    success: (data) => {
                        resultemployekey.innerHTML = '';
                        document.getElementById('noresultssearchemployees').innerHTML = "";
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                resultemployekey.innerHTML += `
                                    <button onclick="fselectemploye(${data[i].iIdEmpleado}, '${data[i].sNombreEmpleado}')" class="animated fadeIn list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back border-left-primary">
                                        ${number}. ${data[i].iIdEmpleado} - ${data[i].sNombreEmpleado}
                                       <span>
                                             <i title="Editar" class="fas fa-edit ml-2 text-warning fa-lg shadow"></i> 
                                       </span>
                                    </button>`;
                            }
                        } else {
                            document.getElementById('noresultssearchemployees').innerHTML += `
                                <div class="alert alert-danger" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron Empleados activos con el termino: <b class="text-uppercase">${searchemployekey.value}</b>
                                </div>
                            `;
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                resultemployekey.innerHTML = '';
            }
        } catch (error) {
            if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }
    /* EJECUCION DE LA FUNCION QUE HACE LA BUSQUEDA DE EMPLEADOS EN TIEMPO REAL */
    searchemployekey.addEventListener('keyup', fsearchemployes);
    /* FUNCION QUE GUARDA LA EDICION DE LOS DATOS GENERALES DEL EMPLEADO */
    fsaveeditdatagen = () => {
        try {
            const dataSendGenEdit = {
                name: name.value, apepat: apepat.value, apemat: apemat.value, sex: sex.value, estciv: estciv.value, fnaci: fnaci.value, lnaci: lnaci.value,
                title: title.value, nacion: nacion.value, state: state.value, codpost: codpost.value, city: city.value, colony: colony.value, street: street.value, numberst: numberst.value, telfij: telfij.value, telmov: telmov.value, email: mailus.value,
                fecmat: fecmat.value, tipsan: tipsan.value, clvemp: clvemp.value
            };
            let validatedatagen = 0;
            const arrInput = [name, apepat, sex, estciv, fnaci, lnaci, title, nacion, state];
            for (let a = 0; a < arrInput.length; a++) {
                if (arrInput[a].hasAttribute('tp-select')) {
                    if (arrInput[a].value == '0') {
                        const attrselect = arrInput[a].getAttribute('tp-select');
                        fshowtypealert('Atencion', 'Selecciona una opción de ' + String(attrselect), 'warning', arrInput[a], 0);
                        validatedatagen = 1;
                        break;
                    }
                    if (arrInput[a].id == 'state' && arrInput[a].value != '0') {
                        arrInput.push(codpost);
                    }
                } else {
                    if (arrInput[a].hasAttribute('tp-date')) {
                        const attrdate = arrInput[a].getAttribute('tp-date');
                        if (arrInput[a].value != "" && attrdate == 'less') {
                            const ds      = new Date();
                            const dayI    = (ds.getDate() < 10) ? "0" + ds.getDate() : ds.getDate();
                            const monthI  = ((ds.getMonth() + 1) < 10) ? "0" + (ds.getMonth() + 1) : ds.getMonth();
                            const fechAct = ds.getFullYear() + "-" + monthI + "-" + dayI;
                            if (arrInput[a].value > fechAct) {
                                fshowtypealert('Atencion', 'La fecha de nacimiento ' + arrInput[a].value + ' es incorrecta, no debe de ser mayor a la fecha actual', 'warning', arrInput[a], 1);
                                validatedatagen = 1;
                                break;
                            }
                        }
                        else {
                            fshowtypealert('Atencion', 'Completa el campo ' + String(arrInput[a].placeholder), 'warning', arrInput[a], 0);
                            validatedatagen = 1;
                            break;
                        }
                    } else {
                        if (arrInput[a].value == '') {
                            fshowtypealert('Atencion', 'Completa el campo ' + String(arrInput[a].placeholder), 'warning', arrInput[a], 0);
                            validatedatagen = 1;
                            break;
                        }
                    }
                    if (arrInput[a].id == 'codpost' && arrInput[a].value.length < 5 || arrInput[a].id == 'codpost' && arrInput[a].value.length > 5) {
                        fshowtypealert('Atencion', 'La longitud del codigo postal debe de ser de 5 digitos', 'warning', arrInput[a], 1);
                        validatedatagen = 1;
                        break;
                    }
                    if (arrInput[a].id == 'codpost' && arrInput[a].value.length == 5) {
                        arrInput.push(colony, street, telmov, mailus);
                    }
                    if (arrInput[a].id == 'mailus' && arrInput[a].value != "") {
                        const emailvalidate = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
                        if (!emailvalidate.test(arrInput[a].value)) {
                            fshowtypealert('Atencion', 'El correo ingresado ' + arrInput[a].value + ' no contiene un formato valido', 'warning', arrInput[a], 1);
                            validatedatagen = 1;
                            break;
                        }
                    }
                }
            }
            if (validatedatagen == 0) {
                $.ajax({
                    url: "../EditDataGeneral/EditDataGeneral",
                    type: "POST",
                    data: dataSendGenEdit,
                    success: (data) => {
                        if (data.Bandera === true && data.MensajeError === "none") {
                            Swal.fire({
                                title: 'Correcto!', text: "Datos generales actualizados", icon: 'success',
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                floaddatatabgeneral(clvemp.value);
                                $("html, body").animate({
                                    scrollTop: $('#nav-datagen-tab').offset().top - 50
                                }, 1000);
                            });
                        } else {
                            Swal.fire({
                                title: 'Error!', text: "Contacte a sistemas", icon: 'error',
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                location.reload();
                            });
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            }
        } catch (error) {
            if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }
    /* EJECUCION DE FUNCION QUE EDITA LOS DATOS GENERALES DEL EMPLEADO */
    btnsaveeditdatagen.addEventListener('click', fsaveeditdatagen);
    /* FUNCION QUE GUARDA LA EDICION DE LOS DATOS DEL IMSS DEL EMPLEADO */
    fsaveeditdataimss = () => {
        try {
            const dataSendImssEdit = {
                regimss: regimss.value, fecefe: fecefe.value, rfc: rfc.value, curp: curp.value, nivest: nivest.value, nivsoc: nivsoc.value, clvimss: clvimss.value, fecefeact: fechefecactimss.value, keyemployee: clvemp.value
            };
            let validatedataimss = 0;
            const arrInput = [regimss, fecefe, rfc, curp, nivest, nivsoc];
            for (let i = 0; i < arrInput.length; i++) {
                if (arrInput[i].hasAttribute("tp-date")) {
                    const attrdate = arrInput[i].getAttribute("tp-date");
                    const d = new Date();
                    let fechAct;
                    if (d.getMonth() + 1 < 10) {
                        fechAct = d.getFullYear() + "-" + "0" + (d.getMonth() + 1) + "-" + d.getDate();
                    } else {
                        fechAct = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();
                    }
                    //if (arrInput[i].value != "" && attrdate == "higher") {
                    //    if (localStorage.getItem("modeedit") == null) {
                    //        if (arrInput[i].value < fechAct) {
                    //            fshowtypealert('Atención', 'La fecha de ' + arrInput[i].placeholder + ' seleccionada ' + arrInput[i].value + ' no puede ser menor a la fecha actual', 'warning', arrInput[i], 1);
                    //            validatedatanom = 1;
                    //            break;
                    //        }
                    //    }
                    //} else {
                    //    fshowtypealert('Atención', 'Completa el campo ' + String(arrInput[i].placeholder), 'warning', arrInput[i], 0);
                    //    validatedatanom = 1;
                    //    break;
                    //}
                } else {
                    if (arrInput[i].hasAttribute("tp-select")) {
                        if (arrInput[i].value == "0") {
                            const attrselect = arrInput[i].getAttribute('tp-select');
                            fshowtypealert('Atención', 'Selecciona una opción de ' + String(attrselect), 'warning', arrInput[i], 0);
                            validatedataimss = 1;
                            break;
                        }
                    } else {
                        if (arrInput[i].value == "") {
                            fshowtypealert('Atención', 'Completa el campo ' + arrInput[i].placeholder, 'warning', arrInput[i], 0);
                            validatedataimss = 1;
                            break;
                        }
                    }
                }
            }
            if (validatedataimss == 0) {
                $.ajax({
                    url: "../EditDataGeneral/EditDataImss",
                    type: "POST",
                    data: dataSendImssEdit,
                    success: (data) => {
                        if (data.Bandera === true && data.MensajeError === "none") {
                            Swal.fire({
                                title: 'Correcto!', text: "Datos del imss actualizados", icon: 'success',
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                floaddatatabgeneral(clvemp.value);
                                $("html, body").animate({
                                    scrollTop: $('#nav-imss-tab').offset().top - 50
                                }, 1000);
                            });
                        } else {
                            Swal.fire({
                                title: 'Error!', text: "Contacte a sistemas", icon: 'error',
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                location.reload();
                            });
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            }
        } catch (error) {
            if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }
    /* EJECUCION DE LA FUNCION QUE GUARDA LA EDICION DE LOS DATOS DEL EMPLEADO */
    btnsaveeditdataimss.addEventListener('click', fsaveeditdataimss);
    /* FUNCION QUE GUARDA LA EDICION DE LOS DATOS DE NOMINA DEL EMPLEADO */
    fsaveeditdatanomina = () => {
        let url = "", datasend, banco;
        const flagSal          = (salmen.value != salmenact.value) ? true : false;
        const retroactivoSendE = (retroactivo.checked) ? 1 : 0;
        const motMoviSal       = document.getElementById('motmovisal');
        const fechMoviSal      = document.getElementById('fechmovisal');
        if (tippag.value == "218" || tippag.value == "220") {
            banco = 999;
        } else {
            banco = banuse.value;
        }
        if (banuse.value == "0") { banco = 999; } else { banco = banuse.value; }
        if (fechefectact.value != fecefecnom.value) {
            url = "../SaveDataGeneral/DataNomina";
            datasend = {
                fecefecnom: fecefecnom.value, salmen: salmen.value, tipemp: tipemp.value, nivemp: nivemp.value,
                tipjor: tipjor.value, tipcon: tipcon.value, fecing: fecing.value, fecant: fecant.value, vencon: vencon.value,
                empleado: name.value, apepat: apepat.value, apemat: apemat.value, fechanaci: fnaci.value, tipper: tipper.value, tipcontra: tipcontra.value,
                tippag: tippag.value, banuse: banco, cunuse: cunuse.value, position: clvstr.value, clvemp: clvemp.value, tiposueldo: tiposueldo.value, politica: politica.value,
                diferencia: diferencia.value, transporte: transporte.value, retroactivo: retroactivoSendE, flagSal: flagSal, motMoviSal: "none", fechMoviSal: "none", salmenact: salmenact.value
            };
        } else {
            url = "../EditDataGeneral/EditDataNomina";
            datasend = {
                fecefecnom: fecefecnom.value, fechefectact: fechefectact.value, salmen: salmen.value, tipper: tipper.value, tipemp: tipemp.value,
                nivemp: nivemp.value, tipjor: tipjor.value, tipcon: tipcon.value, tipcontra: tipcontra.value,
                fecing: fecing.value, fecant: fecant.value, vencon: vencon.value, tippag: tippag.value, banuse: banco,
                cunuse: cunuse.value, clvnom: clvnom.value, position: clvstr.value, tiposueldo: tiposueldo.value, politica: politica.value, diferencia: diferencia.value,
                transporte: transporte.value, retroactivo: retroactivoSendE, motMoviSal: "none", fechMoviSal: "none", flagSal: flagSal, salmenact: salmenact.value, clvemp: clvemp.value
            };
        }
        try {
            let validatedatanom = 0;
            const arrInput = [salmen, tipper, tipemp, nivemp, tipjor, tipcon, fecing, fecant, tipcontra, tiposueldo, politica, diferencia, transporte, tippag];
            if (fecefecnom.value != fechefectact.value) {
                arrInput.push(fecefecnom);
            }
            if (flagSal) {
                arrInput.push(motMoviSal);
                arrInput.push(fechMoviSal);
                datasend.motMoviSal  = motMoviSal.value;
                datasend.fechMoviSal = fechMoviSal.value;
            }
            for (let t = 0; t < arrInput.length; t++) {
                if (arrInput[t].hasAttribute("tp-select")) {
                    let textpag;
                    if (arrInput[t].value == "0" && arrInput[t].id != "tipper") {
                        const attrselect = arrInput[t].getAttribute('tp-select');
                        fshowtypealert('Atención', 'Selecciona una opción de ' + String(attrselect), 'warning', arrInput[t], 0);
                        validatedatanom = 1;
                        break;
                    }
                    if (arrInput[t].value == "n" && arrInput[t].id == "tipper") {
                        const attrselect = arrInput[t].getAttribute('tp-select');
                        fshowtypealert('Atención', 'Selecciona una opción de ' + String(attrselect), 'warning', arrInput[t], 0);
                        validatedatanom = 1;
                        break;
                    }
                    if (arrInput[t].id == "tippag") {
                        textpag = $('select[id="tippag"] option:selected').text();
                    }
                    if (arrInput[t].value == idcuentach || arrInput[t].value == idcuentaah) {
                        arrInput.push(banuse, cunuse);
                    }
                    if (arrInput[t].id == "tipemp" && arrInput[t].value == 76) {
                        arrInput.push(vencon);
                    }
                    if (arrInput[t].value == idcuentach) {
                        if (cunuse.value.length < 10) {
                            fshowtypealert('Atencion', 'El numero de cuenta de cheques debe contener 10 o 11 digitos y solo tiene ' + String(cunuse.value.length) + ' digitos.', 'warning', cunuse, 0);
                            validatedatanom = 1;
                            break;
                        }
                    }
                    if (arrInput[t].value == idcuentaah) {
                        if (cunuse.value.length < 18) {
                            fshowtypealert('Atención', 'El numero de cuenta de cheques clabe debe de contener 18 digitos y solo tiene ' + String(cunuse.value.length) + ' digitos.', 'warning', cunuse, 0);
                            validatedatanom = 1;
                            break;
                        }
                    }
                } else {
                    if (arrInput[t].hasAttribute("tp-date")) {
                        const attrdate = arrInput[t].getAttribute("tp-date");
                        const d = new Date();
                        let fechAct, dateadd;
                        if (d.getDate() < 10) {
                            dateadd = "0" + d.getDate();
                        } else { dateadd = d.getDate(); }
                        if (d.getMonth() + 1 < 10) {
                            fechAct = d.getFullYear() + "-" + "0" + (d.getMonth() + 1) + "-" + dateadd;
                        } else {
                            fechAct = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + dateadd;
                        }
                    } else {
                        if (arrInput[t].value == "") {
                            fshowtypealert('Atención', 'Completa el campo ' + arrInput[t].placeholder, 'warning', arrInput[t], 0);
                            validatedatanom = 1;
                            break;
                        }
                    }
                }
            }
            if (fecant.value != "" && fecing.value != "") {
                if (fecant.value > fecing.value) {
                    fshowtypealert('Atención', 'La fecha de antiguedad no puede ser mayor a la fecha de ingreso', 'warning', fecant, 0);
                    validatedatanom = 1;
                }
            }
            if (validatedatanom == 0) {
                console.log(datasend);
                $.ajax({
                    url: url,
                    type: "POST",
                    data: datasend,
                    success: (data) => {
                        if (data.Bandera === true && data.MensajeError === "none") {
                            Swal.fire({
                                title: 'Correcto!', text: "Datos de nomina actualizados", icon: 'success',
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                floaddatatabgeneral(clvemp.value);
                                $("html, body").animate({
                                    scrollTop: $('#nav-datanom-tab').offset().top - 50
                                }, 1000);
                            });
                        } else {
                            Swal.fire({
                                title: 'Error!', text: "Contacte a sistemas", icon: 'error',
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                location.reload();
                            });
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            }
        } catch (error) {
            if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }
    /* EJECUCION DE LA FUNCION QUE GUARDA LA EDICION DE LOS DATOS DE NOMINA DEL EMPLEADO */
    btnsaveeditdatanomina.addEventListener('click', fsaveeditdatanomina);
    /* FUNCION QUE GUARDA LA EDICION DE LOS DATOS DE LA ESTRUCTURA DEL EMPLEADO */
    fsaveeditdataest = () => {
        try {
            if (clvstract.value != clvstr.value) {
                const fechmovi = document.getElementById('fechmovi');
                const motmovi  = document.getElementById('motmovi');
                let fechActE;
                const arrInput = [clvstr, fechefectpos, fechinipos, motmovi, fechmovi];
                let validateSend = 0;
                for (let a = 0; a < arrInput.length; a++) {
                    if (arrInput[a].hasAttribute('tp-date')) {
                        const attrdate = arrInput[a].getAttribute('tp-date');
                        if (arrInput[a].value != "" && attrdate == 'higher') {
                            const dsE     = new Date();
                            const dayE    = (dsE.getDate() < 10) ? "0" + dsE.getDate() : dsE.getDate();
                            const monthE  = ((dsE.getMonth() + 1) < 10) ? "0" + dsE.getMonth() + 1 : dsE.getMonth();
                            fechActE      = dsE.getFullYear() + "-" + monthE + "-" + dayE;
                        }
                        else {
                            fshowtypealert('Atencion', 'Completa el campo ' + String(arrInput[a].placeholder), 'warning', arrInput[a], 0);
                            validateSend = 1;
                            break;
                        }
                    } else {
                        if (arrInput[a].value == '') {
                            fshowtypealert('Atencion', 'Completa el campo ' + String(arrInput[a].placeholder), 'warning', arrInput[a], 0);
                            validateSend = 1;
                            break;
                        }
                    }
                }
                const dataSend = {
                    clvstr: clvstr.value, clvact: clvstract.value , fechefectpos: fechefectpos.value, 
                    fechinipos: fechinipos.value, clvemp: clvemp.value,
                    clvnom: clvnom.value, fechmovi: fechmovi.value, motmovi: motmovi.value
                };
                if (validateSend == 0) {
                    $.ajax({
                        url: "../SaveDataGeneral/DataEstructuraEdit",
                        type: "POST",
                        data: dataSend,
                        success: (data) => {
                            if (data.Bandera === true && data.MensajeError === "none") {
                                Swal.fire({
                                    title: 'Correcto!', text: "Datos de estructura actualizados", icon: 'success',
                                    showClass: { popup: 'animated fadeInDown faster' },
                                    hideClass: { popup: 'animated fadeOutUp faster' },
                                    confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                }).then((acepta) => {
                                    floaddatatabgeneral(clvemp.value);
                                    $("html, body").animate({
                                        scrollTop: $('#nav-datanom-tab').offset().top - 50
                                    }, 1000);
                                });
                            } else {
                                Swal.fire({
                                    title: 'Error!', text: "Contacte a sistemas", icon: 'error',
                                    showClass: { popup: 'animated fadeInDown faster' },
                                    hideClass: { popup: 'animated fadeOutUp faster' },
                                    confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                }).then((acepta) => {
                                    location.reload();
                                });
                            }
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    })
                }
            } else {
                Swal.fire({
                    title: "No hay nada que editar", icon: "info",
                    showClass: { popup: 'animated fadeInDown faster' },
                    hideClass: { popup: 'animated fadeOutUp faster' },
                    confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                }).then((acepta) => {
                    $("html, body").animate({
                        scrollTop: $('#nav-estructure-tab').offset().top - 50
                    }, 1000);
                });
            }
        } catch (error) {
            if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }
    /* EJEUCION DE LA FUNCION QUE GUARDA LA EDICION DE LOS DATOS DE LA ESTRUCTURA DEL EMPLEADO */
    btnsaveeditdataest.addEventListener('click', fsaveeditdataest);
});