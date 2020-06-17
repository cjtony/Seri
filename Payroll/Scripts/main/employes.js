$(function () {

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
    const tipper = document.getElementById('tipper');
    const tipemp = document.getElementById('tipemp');
    const nivemp = document.getElementById('nivemp');
    const tipjor = document.getElementById('tipjor');
    const tipcon = document.getElementById('tipcon');
    const fecing = document.getElementById('fecing');
    const fecant = document.getElementById('fecant');
    const vencon = document.getElementById('vencon');
    const tipcontra = document.getElementById('tipcontra');
    //const estats = document.getElementById('estats');
    //const motinc = document.getElementById('motinc');
    const tippag = document.getElementById('tippag');
    const banuse = document.getElementById('banuse');
    const cunuse = document.getElementById('cunuse');
    const clvstr = document.getElementById('clvstr');
    const btnsaveeditdatanomina = document.getElementById('btn-save-edit-data-nomina');
    const btnsavedatanomina = document.getElementById('btn-save-data-nomina');
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
        const dataLocSto = {
            key: 'nom', data: {
                clvnom: clvnom.value, fechefectact: fechefectact.value,
                fecefecnom: fecefecnom.value,
                salmen: salmen.value, tipper: tipper.value,
                tipemp: tipemp.value, nivemp: nivemp.value,
                tipjor: tipjor.value, tipcon: tipcon.value,
                tipcontra: tipcontra.value,
                //motinc: motinc.value,
                fecing: fecing.value,
                fecant: fecant.value, vencon: vencon.value,
                //estats: estats.value,
                tippag: tippag.value,
                banuse: banuse.value, cunuse: cunuse.value
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
        document.getElementById('nameuser').textContent = "Editando al empleado: " + name.value + " " + apepat.value + " " + apemat.value + ".";
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
        console.log("Estructura");
        try {
            $.ajax({
                url: "../Empleados/DataTabStructureEmploye",
                type: "POST",
                data: { keyemploye: paramid },
                success: (data) => {
                    console.log("Imprimiendo los datos de la estructura");
                    console.log(data);
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
                        if (localStorage.getItem('modeedit') != null) {
                            btnsavedataall.classList.add('d-none');
                            btnsaveeditdataest.classList.remove('d-none');
                        }
                    } else {
                        document.getElementById('div-most-alert-data-estructure').innerHTML += `
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
        console.log("Nomina");
        try {
            $.ajax({
                url: "../Empleados/DataTabNominaEmploye",
                type: "POST",
                data: { keyemploye: paramid },
                success: (data) => {
                    console.log("Imprimiendo los datos de nomina");
                    console.log(data);
                    if (data.Bandera === true && data.MensajeError === "none") {
                        clvnom.value       = data.Datos.iIdNomina;
                        fechefectact.value = data.Datos.sFechaEfectiva;
                        fecefecnom.value   = data.Datos.sFechaEfectiva;
                        salmen.value       = data.Datos.dSalarioMensual;
                        tipper.value       = data.Datos.iTipoPeriodo;
                        tipemp.value       = data.Datos.iTipoEmpleado_id;
                        nivemp.value       = data.Datos.iNivelEmpleado_id;
                        tipjor.value       = data.Datos.iTipoJornada_id;
                        tipcon.value       = data.Datos.iTipoContrato_id;
                        tipcontra.value    = data.Datos.iTipoContratacion_id;
                        //motinc.value = data.iMotivoIncremento_id;
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
                        flocalstodatatabnomina();
                        if (localStorage.getItem('modeedit') != null) {
                            btnsavedatanomina.classList.add('d-none');
                            btnsaveeditdatanomina.classList.remove('d-none');
                        }
                    } else {
                        document.getElementById('div-most-alert-data-imss').innerHTML += `
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
        console.log("IMSS");
        try {
            $.ajax({
                url: "../Empleados/DataTabImssEmploye",
                type: "POST",
                data: { keyemploye: paramid },
                success: (data) => {
                    console.log('Imprimiendo los datos del IMSS')
                    console.log(data);
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
                    } else {
                        document.getElementById('div-most-alert-data-imss').innerHTML += `
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
                    console.log("Imprimiendo los datos generales");
                    console.log(data);
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
                            colony.value = data.Datos.sColonia;
                            floaddatatabimss(paramid);
                            flocalstodatatabimss();
                            flocalstodatatabgen();
                        }, 2000);
                    } else {
                        document.getElementById('div-most-alert-data-gen').innerHTML += `
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
    const date = new Date();
    let fechAct;
    let day = date.getDay();
    if (date.getDay() < 10) {
        day = "0" + date.getDay();
    }
    if (date.getMonth() + 1 < 10) {
        fechAct = date.getFullYear() + "-" + "0" + (date.getMonth() + 1) + "-" + day;
    } else {
        fechAct = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + day;
    }
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
                        console.log('Generando localstorage')
                        const date = new Date();
                        let fechAct;
                        let day = date.getDay();
                        if (date.getDay() < 10) {
                            day = "0" + date.getDay();
                        }
                        if (date.getMonth() + 1 < 10) {
                            fechAct = date.getFullYear() + "-" + "0" + (date.getMonth() + 1) + "-" + day;
                        } else {
                            fechAct = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + day;
                        }
                        console.log(fechAct);
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
            searchemployekey.placeholder = "NUMERO DEL EMPLEADO";
            labsearchemp.textContent     = "Numero";
        } else if (filtered == "nombre") {
            searchemployekey.placeholder = "NOMBRE DEL EMPLEADO";
            labsearchemp.textContent     = "Nombre";
        }
        searchemployekey.value     = "";
        resultemployekey.innerHTML = "";
        setTimeout(() => { searchemployekey.focus() }, 500);
    }
    /* EJECUCION DE FUNCION QUE APLICA FILTRO A LA BUSQUEDA DE LOS EMPLEADOS */
    filtroname.addEventListener('click', fselectfilterdsearchemploye);
    filtronumber.addEventListener('click', fselectfilterdsearchemploye);
    /* FUNCION QUE EJECUTA LA BUSUQEDA REAL DE LOS EMPLEADOS */
    fsearchemployes = () => {
        const filtered = $("input:radio[name=filtroemp]:checked").val();
        try {
            resultemployekey.innerHTML = '';
            if (searchemployekey.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchEmploye",
                    type: "POST",
                    data: { wordsearch: searchemployekey.value, filtered: filtered.trim() },
                    success: (data) => {
                        resultemployekey.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                resultemployekey.innerHTML += `
                                    <button class="list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back">
                                        ${number}. ${data[i].iIdEmpleado} - ${data[i].sNombreEmpleado}
                                       <span>
                                             <i title="Editar" class="fas fa-edit ml-2 text-warning fa-lg shadow" onclick="fselectemploye(${data[i].iIdEmpleado}, '${data[i].sNombreEmpleado}')"></i> 
                                             <i title="Reporte" class="fas fa-eye ml-2 col-ico fa-lg shadow" onclick="fviewdetailsemploye(${data[i].iIdEmpleado})"></i>
                                       </span>
                                    </button>`;
                            }
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
                            const ds = new Date();
                            const fechAct = ds.getFullYear() + "-" + (ds.getMonth() + 1) + "-" + ds.getDate();
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
                        console.log('Editando datos generales');
                        console.log(data);
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
            console.log(dataSendImssEdit);
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
                        console.log('Editando datos del imss');
                        console.log(data);
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
                //estats: estats.value,
                empleado: name.value, apepat: apepat.value, apemat: apemat.value, fechanaci: fnaci.value, tipper: tipper.value, tipcontra: tipcontra.value,
                //motinc: motinc.value,
                tippag: tippag.value, banuse: banco, cunuse: cunuse.value, position: clvstr.value, clvemp: clvemp.value
            };
        } else {
            url = "../EditDataGeneral/EditDataNomina";
            datasend = {
                fecefecnom: fecefecnom.value, fechefectact: fechefectact.value, salmen: salmen.value, tipper: tipper.value, tipemp: tipemp.value,
                nivemp: nivemp.value, tipjor: tipjor.value, tipcon: tipcon.value, tipcontra: tipcontra.value,
                //motinc: motinc.value,
                fecing: fecing.value, fecant: fecant.value, vencon: vencon.value, tippag: tippag.value, banuse: banco,
                cunuse: cunuse.value, clvnom: clvnom.value, position: clvstr.value
            };
        }
        try {
            let validatedatanom = 0;
            const arrInput = [salmen, tipper, tipemp, nivemp, tipjor, tipcon, fecing, tipcontra, tippag];
            if (fecefecnom.value != fechefectact.value) {
                arrInput.push(fecefecnom);
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
                            fshowtypealert('Atencion', 'El numero de cuenta de cheques debe contener 10 digitos y solo tiene ' + String(cunuse.value.length) + ' digitos.', 'warning', cunuse, 0);
                            validatedatanom = 1;
                            break;
                        }
                    }
                    if (arrInput[t].value == idcuentaah) {
                        if (cunuse.value.length < 15) {
                            fshowtypealert('Atención', 'El numero de cuenta de ahorro debe de contener 18 digitos y solo tiene ' + String(cunuse.value.length) + ' digitos.', 'warning', cunuse, 0);
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
                        //if (arrInput[t].value != "" && attrdate == "less") {
                        //    if (arrInput[t].value > fechAct) {
                        //        fshowtypealert('Atención', 'La ' + arrInput[t].placeholder + ' seleccionada ' + arrInput[t].value + ' no puede ser mayor a la fecha actual', 'warning', arrInput[t], 1);
                        //        validatedatanom = 1;
                        //        break;
                        //    }
                        //} else if (arrInput[t].value != "" && attrdate == "higher") {
                        //    if (arrInput[t].value < fechAct) {
                        //        fshowtypealert('Atención', 'La fecha de ' + arrInput[t].placeholder + ' seleccionada ' + arrInput[t].value + ' no puede ser menor a la fecha actual', 'warning', arrInput[t], 1);
                        //        validatedatanom = 1;
                        //        console.log();
                        //        break;
                        //    }
                        //} else {
                        //    fshowtypealert('Atención', 'Completa el campo ' + String(arrInput[t].placeholder), 'warning', arrInput[t], 0);
                        //    validatedatanom = 1;
                        //    break;
                        //}
                    } else {
                        if (arrInput[t].value == "") {
                            fshowtypealert('Atención', 'Completa el campo ' + arrInput[t].placeholder, 'warning', arrInput[t], 0);
                            validatedatanom = 1;
                            break;
                        }
                    }
                }
            }
            if (validatedatanom == 0) {
                $.ajax({
                    url: url,
                    type: "POST",
                    data: datasend,
                    success: (data) => {
                        console.log(data);
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
                const arrInput = [clvstr, fechefectpos, fechinipos];
                let validateSend = 0;
                for (let a = 0; a < arrInput.length; a++) {
                    if (arrInput[a].hasAttribute('tp-date')) {
                        const attrdate = arrInput[a].getAttribute('tp-date');
                        if (arrInput[a].value != "" && attrdate == 'higher') {
                            const ds = new Date();
                            let fechAct, datetod;
                            if (ds.getDate() < 10) {
                                datetod = "0" + ds.getDate();
                            } else { datetod = ds.getDate(); }
                            if (ds.getMonth() + 1 < 10) {
                                fechAct = ds.getFullYear() + "-" + "0" + (ds.getMonth() + 1) + "-" + datetod;
                            } else {
                                fechAct = ds.getFullYear() + "-" + (ds.getMonth() + 1) + "-" + datetod;
                            }
                            //if (arrInput[a].value < fechAct) {
                            //    console.log(arrInput[a].value);
                            //    console.log(fechAct);
                            //    fshowtypealert('Atencion', 'La fecha ' + arrInput[a].value + ' es incorrecta, debe de ser mayor a la fecha actual', 'warning', arrInput[a], 1);
                            //    validateSend = 1;
                            //    break;
                            //}
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
                    clvstr: clvstr.value, fechefectpos: fechefectpos.value, fechinipos: fechinipos.value, clvemp: clvemp.value,
                    //clvposasig: clvposasig.value,
                    clvnom: clvnom.value,
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