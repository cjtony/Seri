$(function () {
    // Comentar cuando el proyecto este en produccion \\
    //const idefectivo = 1115;
    //const idcuentach = 1116;
    //const idcajeroau = 1117;
    //const idcuentaah = 1118;

    // Descomentar cuando el proyecto este en produccion \\
    const idefectivo = 218;
    const idcuentach = 219;
    const idcajeroau = 220;
    const idcuentaah = 221;

    const dateMain    = new Date();
    const dayMain     = (dateMain.getDate() < 10) ? "0" + dateMain.getDate() : dateMain.getDate();
    const monthMain   = ((dateMain.getMonth() + 1) < 10) ? "0" + (dateMain.getMonth() + 1) : (dateMain.getMonth() + 1);
    const dateActMain = dateMain.getFullYear() + "-" + monthMain + "-" + dayMain;

    function someMethodIThinkMightBeSlow() {
        const startTime = performance.now();
        const duration  = performance.now() - startTime;
        console.log(`someMethodIThinkMightBeSlow took ${duration}ms`);
    }

    someMethodIThinkMightBeSlow();

    const dataLocStoSave = localStorage.getItem('modesave');
    const dateLocStoSave = localStorage.getItem('datesave');

    const dateLocSto = localStorage.getItem("dateedit");
    const modeLocSto = localStorage.getItem("modeedit");
    

    const dateact = document.getElementById('dateact');
    let d = new Date();
    let monthact = d.getMonth() + 1, dayact = d.getDate(), montlet = "", daylet = "";
    const months = ['', 'Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'];

    for (let i = 0; i < months.length; i++) {
        if (monthact == i) {
            montlet = months[i];
        }
    }

    const days = ['', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado', 'Domingo'];

    for (let d = 0; d < days.length; d++) {
        if (dayact == d) {
            daylet = days[d];
        }
    }

    dateact.textContent = daylet + " " + d.getDate() + " de " + montlet + " del " + d.getFullYear() + ".";

    const navDataGenTab    = document.getElementById('nav-datagen-tab');
    const navImssTab       = document.getElementById('nav-imss-tab');
    const navDataNomTab    = document.getElementById('nav-datanom-tab');
    const navEstructureTab = document.getElementById('nav-estructure-tab');

    const btnsaveedit           = document.getElementById('btn-save-edit');
    const btnClearLocSto        = document.getElementById('btn-clear-localstorage');
    const btnSaveDataGen        = document.getElementById('btn-save-data-gen');
    const btnSaveDataImss       = document.getElementById('btn-save-data-imss');
    const btnSaveDataNomina     = document.getElementById('btn-save-data-nomina');
    const btnSaveDataEstructure = document.getElementById('btn-save-data-estructure');
    const btnSaveDataAll        = document.getElementById('btn-save-data-all');

    let objectDataTabDataGen    = {};
    let objectDataTabImss       = {};
    let objectDataTabNom        = {};
    let objectDataTabEstructure = {};

    // Variables formulario Datos Generales \\ 
    const clvemp   = document.getElementById('clvemp');
    const name     = document.getElementById('name');
    const apep     = document.getElementById('apepat');
    const apem     = document.getElementById('apemat');
    const fnaci    = document.getElementById('fnaci');
    const lnaci    = document.getElementById('lnaci');
    const title    = document.getElementById('title');
    const sex      = document.getElementById('sex');
    const nacion   = document.getElementById('nacion');
    const estciv   = document.getElementById('estciv');
    const codpost  = document.getElementById('codpost');
    const state    = document.getElementById('state');
    const city     = document.getElementById('city');
    const colony   = document.getElementById('colony');
    const street   = document.getElementById('street');
    const numberst = document.getElementById('numberst');
    const telfij   = document.getElementById('telfij');
    const telmov   = document.getElementById('telmov');
    const mailus   = document.getElementById('mailus');
    const tipsan   = document.getElementById('tipsan');
    const  fecmat  = document.getElementById('fecmat');
    const btnsaveeditdatagen = document.getElementById('btn-save-edit-data-gen');


    const vardatagen = [clvemp, name, apep, apem, fnaci, lnaci, title, sex, nacion, estciv, codpost, state, city, colony, street, numberst,
        telfij, telmov, mailus, tipsan, fecmat];

    fclearfieldsvar1 = () => {
        for (let i = 0; i < vardatagen.length; i++) {
            if (vardatagen[i].getAttribute('tp-select') != null) {
                if (vardatagen[i].id == "nacion") {
                    vardatagen[i].value = 484;
                } else if (vardatagen[i].id == "estciv") {
                    vardatagen[i].value = 50;
                } else {
                    vardatagen[i].value = "0";
                }
            } else {
                vardatagen[i].value = "";
            }
        }
    }

    // Variables formulario IMSS \\
    const clvimss         = document.getElementById('clvimss');
    const fechefecactimss = document.getElementById('fechefecactimss');
    const imss            = document.getElementById('regimss');
    const rfc             = document.getElementById('rfc');
    const curp            = document.getElementById('curp');
    const nivest          = document.getElementById('nivest');
    const nivsoc          = document.getElementById('nivsoc');
    const fecefe          = document.getElementById('fecefe');
    const btnsaveeditdataimss = document.getElementById('btn-save-edit-data-imss');
    const vardataimss = [clvimss, fechefecactimss, fecefe, imss, rfc, curp, nivest, nivsoc];
    fclearfieldsvar2 = () => {
        for (let i = 0; i < vardataimss.length; i++) {
            if (vardataimss[i].getAttribute('tp-select') != null) {
                vardataimss[i].value = "0";
            } else {
                vardataimss[i].value = "";
            }
        }
    }

    // Variables formulario datos nomina \\
    const clvnom       = document.getElementById('clvnom');
    const fecefecnom   = document.getElementById('fecefecnom');
    const fechefectact = document.getElementById('fechefectact');
    const salmen       = document.getElementById('salmen');
    const salmenact    = document.getElementById('salmenact');
    const tipper       = document.getElementById('tipper');
    const tipemp       = document.getElementById('tipemp');
    const nivemp       = document.getElementById('nivemp');
    const tipjor       = document.getElementById('tipjor');
    const tipcon       = document.getElementById('tipcon');
    const fecing       = document.getElementById('fecing');
    const fecant       = document.getElementById('fecant');
    const vencon       = document.getElementById('vencon');
    const tippag       = document.getElementById('tippag');
    const banuse       = document.getElementById('banuse');
    const cunuse       = document.getElementById('cunuse');
    const tipcontra    = document.getElementById('tipcontra');
    const tiposueldo   = document.getElementById('tiposueldo');
    const politica     = document.getElementById('politica');
    const diferencia   = document.getElementById('diferencia');
    const transporte   = document.getElementById('transporte');
    const retroactivo  = document.getElementById('retroactivo');
    const conFondo     = document.getElementById('con_fondo');
    const categoriaEmp = document.getElementById('categoria_emp');
    const pagoPorEmple = document.getElementById('pago_por');
    const btnsaveeditdatanomina = document.getElementById('btn-save-edit-data-nomina');
    const ultSdi = document.getElementById('view-ultSdi');

    const vardatanomina = [
        clvnom, fechefectact, fecefecnom, tipper, salmen, salmenact, tipemp, nivemp, tipjor, tipcon, fecing, fecant, vencon, tipcontra, tippag, banuse, cunuse, tiposueldo, politica, diferencia, transporte, categoriaEmp, pagoPorEmple
    ];
    fclearfieldsvar3 = () => {
        retroactivo.checked = 0;
        conFondo.checked = 0;
        for (let i = 0; i < vardatanomina.length; i++) {
            if (vardatanomina[i].getAttribute('tp-select') != null) {
                if (vardatanomina[i].id == 'tipper') {
                    vardatanomina[i].value = "n";
                } else {
                    vardatanomina[i].value = "0";
                }
            } else {
                vardatanomina[i].value = "";
            }
        }
    }

    //Variables formulario estructura \\
    const numpla       = document.getElementById('numpla');
    const clvstr       = document.getElementById('clvstr');
    const clvposasig   = document.getElementById('clvposasig');
    const clvstract    = document.getElementById('clvstract');
    const depaid       = document.getElementById('depaid');
    const puesid       = document.getElementById('puesid');
    const emprep       = document.getElementById('emprep');
    const report       = document.getElementById('report');
    const depart       = document.getElementById('depart');
    const pueusu       = document.getElementById('pueusu');
    const localty      = document.getElementById('localty');
    const fechefectpos = document.getElementById('fechefectpos');
    const fechinipos   = document.getElementById('fechinipos');
    const fechefecposact     = document.getElementById('fechefecposact');
    const btnsaveeditdataest = document.getElementById('btn-save-edit-dataest');
    const vardataestructure  = [clvstract, clvposasig, clvstr, numpla, depaid, puesid, emprep, report, depart, pueusu, localty, fechefectpos, fechinipos, fechefecposact];
    fclearfieldsvar4 = () => {
        for (let i = 0; i < vardataestructure.length; i++) {
            if (vardataestructure[i].getAttribute('tp-select') != null) {
                vardataestructure[i].value = "0";
            } else {
                vardataestructure[i].value = "";
            }
        }
    }

    fasignsdates = () => {
        if (localStorage.getItem('modeedit') == null) {
            const date = new Date();
            const frmonth = ((date.getMonth() + 1) < 10) ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
            const frday = (date.getDate() < 10) ? "0" + date.getDate() : date.getDate();
            const fechact = date.getFullYear() + '-' + frmonth + '-' + frday;
            setTimeout(() => {
                fecefe.disabled = false;
                fecefe.value = fechact;
                fecefecnom.disabled = false;
                fecefecnom.value = fechact;
                fechefectpos.disabled = false;
                fechefectpos.value = fechact;
                fechinipos.disabled = false;
                fechinipos.value = fechact;
            }, 1000);
        }
        console.log('Fechas asignadas');
    };

    //Funcion que carga los motivos de movimiento
    fLoadMotivesMovements = (paramstreid) => {
        //console.log('IdFallo');
        //console.log(paramstreid);
        if (paramstreid.id == "motmovisal") {
            paramstreid.innerHTML = '<option value="0">Selecciona</option>';
        } else {
            paramstreid.innerHTML = '<option value="">Selecciona</option>';
        }
        paramstreid.innerHTML = ``;
        try {
            if (paramstreid != "") {
                $.ajax({
                    url: "../SearchDataCat/LoadMotivesMovements",
                    type: "POST",
                    data: {},
                    beforeSend: () => {
                        console.log('Imprimiendo tm')
                    }, success: (request) => {
                        console.log(request);
                        if (request.Bandera == true) {
                            for (let i = 0; i < request.Datos.length; i++) {
                                document.getElementById(String(paramstreid)).innerHTML += `<option value="${request.Datos[i].iId}">${request.Datos[i].sValor}</option>`;
                            }
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

    const cntIFechMovi = document.getElementById('content-new-inpt-fechmovits');

    fchecklocalstotab = () => {
        if (localStorage.getItem('tabSelected') === null) {
            localStorage.setItem('tabSelected', 'none');
        }
        if (localStorage.getItem('modeedit') != null) { 
            cntIFechMovi.innerHTML = `
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
                    <select class="form-control form-control-sm" id="motmovi" tp-select="Motivo del movimiento"> <option value="">Selecciona</option> </select> 
                </div>
            `;
            document.getElementById('content-new-inpt-movsal').innerHTML = `
                <label for="motmovisal" class="col-sm-4 col-form-label font-labels col-ico font-weight-bold" id="label-motmovi">
                    Motivo del movimiento
                </label>
                <div class="col-sm-8">
                    <select class="form-control form-control-sm" id="motmovisal" tp-select="Motivo del movimiento"> <option value="0">Selecciona</option> </select>
                </div> 
            `;
            document.getElementById('content-new-inpt-fecsal').innerHTML = `
                <label for="fechmovisal" class="col-sm-4 col-form-label font-labels col-ico font-weight-bold">
                    Fecha de movimiento
                </label>
                <div class="col-sm-8">
                    <input type="date" id="fechmovisal" class="form-control form-control-sm" placeholder="Fecha del movimiento salarial" />
                </div>
            `;
            document.getElementById('content-new-inpt-movsal').classList.remove("d-none");
            document.getElementById('content-new-inpt-fecsal').classList.remove("d-none");
            document.getElementById('content-new-inpt-ultsdi').classList.remove("d-none");
            cntIFechMovi.classList.remove('d-none');
            fLoadMotivesMovements('motmovisal');
            fLoadMotivesMovements('motmovi');
        }
    }

    fchecklocalstotab();

    fviewlaerttabcontinue = (paramid) => {
        const divAlert = `
            <div class="alert alert-info text-center" role="alert">
                <b> <i class="fas fa-edit mr-2"></i> Continua en el apartado en donde te quedaste!</b>
            </div>
        `;
        document.getElementById('div-most-alert-' + String(paramid)).classList.add('mb-4');
        document.getElementById('div-most-alert-' + String(paramid)).innerHTML = divAlert;
    }

    // Funciones para cadenas \\

    fuppercase = (string) => {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }

    String.prototype.capitalize = function () {
        return this.replace(/(^|\s)([a-z])/g, function (m, p1, p2) { return p1 + p2.toUpperCase(); });
    };

    // Funcion que valida que un correo este bien
    mailus.addEventListener('keyup', () => {
        const emailvalidate = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
        if (mailus.value.length > 0) {
            if (!emailvalidate.test(mailus.value)) {
                mailus.classList.add('is-invalid');
                btnSaveDataGen.disabled = true;
            } else {
                mailus.classList.remove('is-invalid');
                btnSaveDataGen.disabled = false;
            }
        }
    });

    // Selecciona la tab en donde el ususario se quedo editando \\

    fselectlocalstotab = () => {
        setTimeout(() => {
            const localStoTab = localStorage.getItem('tabSelected');
            if (localStoTab === "none") {
                navImssTab.classList.add('disabled');
                navDataNomTab.classList.add('disabled');
                navEstructureTab.classList.add('disabled');
                $("#nav-datagen-tab").click();
            } else if (localStoTab === "imss") {
                navDataNomTab.classList.add('disabled');
                navEstructureTab.classList.add('disabled');
                fviewlaerttabcontinue('data-imss');
                $("#nav-imss-tab").click();
            } else if (localStoTab === "datanom") {
                navEstructureTab.classList.add('disabled'); +
                    fviewlaerttabcontinue('data-nomina');
                $("#nav-datanom-tab").click();
            } else if (localStoTab === "dataestructure") {
                fviewlaerttabcontinue('data-estructure');
                $("#nav-estructure-tab").click();
            }
        }, 100);
    }

    fselectlocalstotab();

    floaddatatabs = () => {
        if (JSON.parse(localStorage.getItem('objectTabDataGen')) != null) {
            const getDataTabDataGen = JSON.parse(localStorage.getItem('objectTabDataGen'));
            let dcolony;
            for (i in getDataTabDataGen) {
                if (getDataTabDataGen[i].key === "general") {
                    clvemp.value  = getDataTabDataGen[i].data.clvemp;
                    name.value    = getDataTabDataGen[i].data.name;
                    apep.value    = getDataTabDataGen[i].data.apep;
                    apem.value    = getDataTabDataGen[i].data.apem;
                    fnaci.value   = getDataTabDataGen[i].data.fnaci;
                    lnaci.value   = getDataTabDataGen[i].data.lnaci;
                    title.value   = getDataTabDataGen[i].data.title;
                    sex.value     = getDataTabDataGen[i].data.sex;
                    nacion.value  = getDataTabDataGen[i].data.nacion;
                    estciv.value  = getDataTabDataGen[i].data.estciv;
                    codpost.value = getDataTabDataGen[i].data.codpost;
                    state.value   = getDataTabDataGen[i].data.state;
                    city.value    = getDataTabDataGen[i].data.city;
                    street.value  = getDataTabDataGen[i].data.street;
                    dcolony       = getDataTabDataGen[i].data.colony;
                    numberst.value = getDataTabDataGen[i].data.numberst;
                    telfij.value = getDataTabDataGen[i].data.telfij;
                    telmov.value = getDataTabDataGen[i].data.telmov;
                    mailus.value = getDataTabDataGen[i].data.mailus;
                    tipsan.value = getDataTabDataGen[i].data.tipsan;
                    fecmat.value = getDataTabDataGen[i].data.fecmat;
                }
            }
            document.getElementById('icouser').classList.remove('d-none');
            if (localStorage.getItem('modeedit') != null) {
                document.getElementById('nameuser').textContent = clvemp.value + " - " + name.value + " " + apep.value + " " + apem.value + ".";
            } else {
                document.getElementById('nameuser').textContent = name.value + " " + apep.value + " " + apem.value + ".";
            }
        }
        if (JSON.parse(localStorage.getItem('objectDataTabImss')) != null) {
            const getDataTabImss = JSON.parse(localStorage.getItem('objectDataTabImss'));
            for (i in getDataTabImss) {
                if (getDataTabImss[i].key === "imss") {
                    fecefe.value          = getDataTabImss[i].data.fecefe;
                    clvimss.value         = getDataTabImss[i].data.clvimss;
                    fechefecactimss.value = getDataTabImss[i].data.fechefecactimss;
                    imss.value            = getDataTabImss[i].data.imss;
                    rfc.value             = getDataTabImss[i].data.rfc;
                    curp.value            = getDataTabImss[i].data.curp;
                    nivest.value          = getDataTabImss[i].data.nivest;
                    nivsoc.value          = getDataTabImss[i].data.nivsoc;
                }
            }
        }
        if (JSON.parse(localStorage.getItem('objectDataTabNom')) != null) {
            const getDataTabNom = JSON.parse(localStorage.getItem('objectDataTabNom'));
            for (i in getDataTabNom) {
                if (getDataTabNom[i].key == "nom") {
                    clvnom.value        = getDataTabNom[i].data.clvnom;
                    fecefecnom.value    = getDataTabNom[i].data.fecefecnom;
                    salmen.value        = getDataTabNom[i].data.salmen;
                    salmenact.value     = getDataTabNom[i].data.salmenact;
                    fechefectact.value  = getDataTabNom[i].data.fechefectact;
                    tipper.value        = getDataTabNom[i].data.tipper;
                    tipemp.value        = getDataTabNom[i].data.tipemp;
                    nivemp.value        = getDataTabNom[i].data.nivemp;
                    tipjor.value        = getDataTabNom[i].data.tipjor;
                    tipcon.value        = getDataTabNom[i].data.tipcon;
                    fecing.value        = getDataTabNom[i].data.fecing;
                    fecant.value        = getDataTabNom[i].data.fecant;
                    vencon.value        = getDataTabNom[i].data.vencon;                   
                    tipcontra.value     = getDataTabNom[i].data.tipcontra;
                    tippag.value        = getDataTabNom[i].data.tippag;
                    banuse.value        = getDataTabNom[i].data.banuse;
                    politica.value      = getDataTabNom[i].data.politica;
                    diferencia.value    = getDataTabNom[i].data.diferencia;
                    transporte.value    = getDataTabNom[i].data.transporte;
                    retroactivo.checked = getDataTabNom[i].data.retroactivo;
                    conFondo.checked    = getDataTabNom[i].data.confondo;
                    if (getDataTabNom[i].data.banuse != 999) {
                        banuse.disabled = false;
                        cunuse.disabled = false;
                    } 
                    if (getDataTabNom[i].data.tippag == idcuentach) {
                        cunuse.setAttribute("maxlength", 11);
                    } else if (getDataTabNom[i].data.tippag == idcuentaah) {
                        cunuse.setAttribute("maxlength", 18);
                    }
                    const cuentaavalor = getDataTabNom[i].data.cunuse;
                    const categoriaDat = getDataTabNom[i].data.categoria;
                    const pagoporEmpld = getDataTabNom[i].data.pagopor;
                    setTimeout(() => {
                        cunuse.value       = cuentaavalor;
                        categoriaEmp.value = categoriaDat;
                        pagoPorEmple.value = pagoporEmpld;
                    }, 3000);
                    if (localStorage.getItem("modeedit") != null) {
                        ultSdi.value = getDataTabNom[i].data.ultSdi;
                    }
                }
            }
        }
        if (JSON.parse(localStorage.getItem('objectDataTabEstructure')) != null) {
            const getDataEstructure = JSON.parse(localStorage.getItem('objectDataTabEstructure'));
            for (i in getDataEstructure) {
                if (getDataEstructure[i].key === "estructure") {
                    clvstract.value      = getDataEstructure[i].data.clvstract;
                    clvposasig.value     = getDataEstructure[i].data.clvposasig;
                    clvstr.value         = getDataEstructure[i].data.clvstr;
                    numpla.value         = getDataEstructure[i].data.numpla;
                    emprep.value         = getDataEstructure[i].data.emprep;
                    report.value         = getDataEstructure[i].data.report;
                    depaid.value         = getDataEstructure[i].data.depaid;
                    depart.value         = getDataEstructure[i].data.depart;
                    puesid.value         = getDataEstructure[i].data.puesid;
                    pueusu.value         = getDataEstructure[i].data.pueusu;
                    localty.value        = getDataEstructure[i].data.localty;
                    fechefectpos.value   = getDataEstructure[i].data.fechefectpos;
                    fechinipos.value     = getDataEstructure[i].data.fechinipos;
                    fechefecposact.value = getDataEstructure[i].data.fechefecposact;
                }
            }
        }
    }

    floaddatatabs();

    

    fasignsdates();

    fvalidatebuttonsactionmain = () => {
        if (localStorage.getItem("modeedit") == null) {
            btnsaveeditdatagen.classList.add('d-none');
            btnSaveDataGen.classList.remove('d-none');
            btnsaveeditdataimss.classList.add('d-none');
            btnSaveDataImss.classList.remove('d-none');
            btnSaveDataNomina.classList.remove('d-none');
            btnsaveeditdatanomina.classList.add('d-none');
            btnsaveeditdataest.classList.add('d-none');
            btnSaveDataAll.classList.remove('d-none');
            btnSaveDataAll.disabled = false;
        }
    }

    fGenRestore = () => {
        localStorage.removeItem('modeedit');
        localStorage.removeItem('dateedit');
        localStorage.removeItem('tabSelected');
        localStorage.removeItem('objectTabDataGen'); localStorage.removeItem('objectDataTabImss');
        localStorage.removeItem('objectDataTabNom'); localStorage.removeItem('objectDataTabEstructure');
        localStorage.removeItem('modedit');
        document.getElementById('content-new-inpt-fechmovits').innerHTML = "";
        document.getElementById('content-new-inpt-motmovi').innerHTML = "";
        document.getElementById('content-new-inpt-movsal').innerHTML = "";
        document.getElementById('content-new-inpt-fecsal').innerHTML = "";
        document.getElementById('content-new-inpt-ultsdi').innerHTML = "";
        document.getElementById('content-new-inpt-fechmovits').classList.add('d-none');
        document.getElementById('content-new-inpt-motmovi').classList.add('d-none');
        document.getElementById('content-new-inpt-movsal').classList.add('d-none');
        document.getElementById('content-new-inpt-fecsal').classList.add('d-none');
        document.getElementById('content-new-inpt-ultsdi').classList.add('d-none');
        fchecklocalstotab();
        fselectlocalstotab();
        floaddatatabs();
        fclearfieldsvar1();
        fclearfieldsvar2();
        fclearfieldsvar3();
        fclearfieldsvar4();
        document.getElementById('icouser').classList.add('d-none');
        document.getElementById('nameuser').textContent = '';
        colony.innerHTML = "<option value='0'>Selecciona</option>";
        codpost.disabled = true;
        colony.disabled = true;
        street.disabled = true;
        numberst.disabled = true;
        banuse.disabled = true;
        cunuse.disabled = true;
        document.getElementById('infobankch').classList.add("d-none");
        document.getElementById('infobankct').classList.add("d-none");
        fvalidatebuttonsactionmain();
        fasignsdates();
        transporte.value = 0;
    }

    fclearlocsto = (type) => {
        let timerInterval;
        if (type == 1) {
            Swal.fire({
                title: "Esta seguro", text: "de limpiar los campos?", icon: "warning",
                confirmButtonText: "Aceptar", showCancelButton: true, cancelButtonText: "Cancelar",
                allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
            }).then((result) => {
                if (result.value) {
                    fGenRestore();
                    Swal.fire({
                        title: "Limpiando campos", html: "<b></b>",
                        timer: 2000, timerProgressBar: true,
                        allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                        onBeforeOpen: () => {
                            Swal.showLoading();
                            timerInterval = setInterval(() => { const content = Swal.getContent(); }, 100);
                        }, onClose: () => { clearInterval(timerInterval); }
                    }).then((result) => {
                        if (result.dismiss === Swal.DismissReason.timer) {
                            Swal.fire({
                                title: "Correcto", showConfirmButton: false, timer: 1500, icon: "success",
                                allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false
                            });
                        }
                    });
                } else {
                    Swal.fire({ title: "Bien", text: "Todo sigue igual", timer: 1000, showConfirmButton: false, allowEnterKey: false, allowEscapeKey: false, allowOutsideClick: false });
                }
            });
        } else {
            Swal.fire({ title: "Atención!", timer: 2200, text: "Detectamos informacion capturada que supera el tiempo de almacenamiento. Los campos se limpiaran.", icon: "info", allowEnterKey: false, allowEscapeKey: false, allowOutsideClick: false, showConfirmButton: false });
            fGenRestore();
        }
        fasignsdates();
    }

    if (dataLocStoSave != null) {
        if (dateLocStoSave != dateActMain) {
            fclearlocsto(0);
            localStorage.removeItem('modesave');
            localStorage.removeItem('datesave');
        }
    } 

    if (modeLocSto != null) {
        localStorage.removeItem('modesave');
        localStorage.removeItem('datesave');
        const date = new Date();
        let fechAct;
        const day = (date.getDate() < 10) ? "0" + date.getDate() : date.getDate();
        const month = ((date.getMonth() + 1) < 10) ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        fechAct = date.getFullYear() + "-" + month + "-" + day;
        if (dateLocSto != null) {
            console.log('Imprimiendo fechas');
            console.log(dateLocSto);
            console.log(fechAct);
            if (dateLocSto != fechAct) {
                setTimeout(() => {
                    fclearlocsto(0);
                }, 2000);
                console.log('Entro');
            }
        }
    }

    btnClearLocSto.addEventListener('click', () => { fclearlocsto(1); });

    let validate = 0;

    fshowalertcontinue = (texttoastr) => {
        Swal.fire({
            position: "top-end",
            html: "<b></b>",
            icon: "info",
            showConfirmButton: false,
            timer: 2000,
            onBeforeOpen: () => {
                const content = Swal.getContent();
                if (content) {
                    const b = content.querySelector('b');
                    if (b) { b.textContent = String(texttoastr); }
                }
            }
        });
    }

    toastr.options = {
        "closeButton": false, "debug": false,
        "newestOnTop": false, "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "1000", "hideDuration": "1000",
        "timeOut": "5000", "extendedTimeOut": "1000",
        "showEasing": "swing", "hideEasing": "linear",
        "showMethod": "fadeIn", "hideMethod": "fadeOut"
    };


    gotoppage = (element, idtab, texttoastr) => {
        fshowalertcontinue(texttoastr);
        element.classList.remove('disabled');
        $('body, html').animate({ scrollTop: '0px' }, 1000);
        setTimeout(() => { $(" " + idtab + " ").click(); }, 2000);
    }

    String.prototype.reverse = function () {
        var x = this.length;
        var cadena = "";
        while (x >= 0) {
            cadena = cadena + this.charAt(x);
            x--;
        }
        return cadena;
    };

    fshowtypealert = (title, text, icon, element, clear) => {
        Swal.fire({
            title: title, text: text, icon: icon,
            showClass: { popup: 'animated fadeInDown faster' },
            hideClass: { popup: 'animated fadeOutUp faster' },
            confirmButtonText: "Aceptar",
            allowOutsideClick: false,
            allowEscapeKey: false,
            allowEnterKey: false,
        }).then((acepta) => {
            $("html, body").animate({ scrollTop: $(`#${element.id}`).offset().top - 50 }, 1000);
            if (clear == 1) {
                setTimeout(() => {
                    element.focus(); setTimeout(() => { element.value = ""; }, 300);
                }, 1200);
            } else {
                setTimeout(() => {
                    element.focus();
                }, 1200);
            }
            if (element.id == "numpla") {
                setTimeout(() => {
                    $("#btn-search-table-num-posicion").click();
                }, 1500);
            }
        });
    };

    btnSaveDataGen.addEventListener('click', () => {
        const arrInput = [name, apep, sex, estciv, fnaci, lnaci, title, nacion, state];
        let validate = 0;
        for (let a = 0; a < arrInput.length; a++) {
            if (arrInput[a].hasAttribute('tp-select')) {
                if (arrInput[a].value == '0') {
                    const attrselect = arrInput[a].getAttribute('tp-select');
                    fshowtypealert('Atencion', 'Selecciona una opción de ' + String(attrselect), 'warning', arrInput[a], 0);
                    validate = 1;
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
                            validate = 1;
                            break;
                        }
                    }
                    else {
                        fshowtypealert('Atencion', 'Completa el campo ' + String(arrInput[a].placeholder), 'warning', arrInput[a], 0);
                        validate = 1;
                        break;
                    }
                } else {
                    if (arrInput[a].value == '') {
                        fshowtypealert('Atencion', 'Completa el campo ' + String(arrInput[a].placeholder), 'warning', arrInput[a], 0);
                        validate = 1;
                        break;
                    }
                }
                if (arrInput[a].id == 'codpost' && arrInput[a].value.length < 5 || arrInput[a].id == 'codpost' && arrInput[a].value.length > 5) {
                    fshowtypealert('Atencion', 'La longitud del codigo postal debe de ser de 5 digitos', 'warning', arrInput[a], 1);
                    validate = 1;
                    break;
                }
                if (arrInput[a].id == 'codpost' && arrInput[a].value.length == 5) {
                    arrInput.push(colony, street, telmov, mailus);
                }
                if (arrInput[a].id == 'mailus' && arrInput[a].value != "") {
                    const emailvalidate = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
                    if (!emailvalidate.test(arrInput[a].value)) {
                        fshowtypealert('Atencion', 'El correo ingresado ' + arrInput[a].value + ' no contiene un formato valido', 'warning', arrInput[a], 1);
                        validate = 1;
                        break;
                    }
                }
            }
        }
        if (validate == 0) {
            gotoppage(navImssTab, '#nav-imss-tab', "Ahora completa los datos del apartado Imss!");
            const dataLocStoGen = {
                key: 'general', data: {
                    clvemp: clvemp.value,
                    name: name.value, apep: apep.value,
                    apem: apem.value, fnaci: fnaci.value,
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
            if (localStorage.getItem("modesave") == null) {
                localStorage.setItem("modesave", 1);
                const date = new Date();
                let fechAct;
                const day   = (date.getDate() < 10) ? "0" + date.getDate() : date.getDate();
                const month = ((date.getMonth() + 1) < 10) ? "0" + (date.getMonth() + 1) : (date.getMonth() + 1);
                fechAct = date.getFullYear() + "-" + month + "-" + day;
                localStorage.setItem("datesave", fechAct);
            }
            document.getElementById('icouser').classList.remove('d-none');
            document.getElementById('nameuser').textContent = "Empleado: " + name.value + " " + apep.value + " " + apem.value + ".";
            objectDataTabDataGen.datagen = dataLocStoGen;
            localStorage.setItem('objectTabDataGen', JSON.stringify(objectDataTabDataGen));
            localStorage.setItem('tabSelected', 'imss');
        }
    });

    /* FUNCION QUE VALIDA CURP MAIN */
    function curpValidaMain(curp) {
        var re = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/,
            validado = curp.match(re);
        if (!validado)
            return false;
        function digitoVerificador(curp17) {
            var diccionario = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ",
                lngSuma = 0.0,
                lngDigito = 0.0;
            for (var i = 0; i < 17; i++)
                lngSuma = lngSuma + diccionario.indexOf(curp17.charAt(i)) * (18 - i);
            lngDigito = 10 - lngSuma % 10;
            if (lngDigito == 10) return 0;
            return lngDigito;
        }
        if (validado[2] != digitoVerificador(validado[1]))
            return false;
        return true;
    }

    function validarInputMain(input) {
        var curp = input.value.toUpperCase();
        const nameE = name.value;
        const apePE = document.getElementById('apepat').value;
        const apeME = document.getElementById('apemat').value;
        const fNaci = document.getElementById('fnaci').value;
        const arrayBirth = fNaci.split("-");
        const lyricsFirtsSurname = apePE.substring(0, 2).toUpperCase();
        const lyricsSecondSurname = apeME.substring(0, 1).toUpperCase();
        const lyricsNameEmployee = nameE.substring(0, 1).toUpperCase();
        const yearBirthEmployee = arrayBirth[0].substring(2, 4);
        const monthBirthEmployee = arrayBirth[1];
        const dayBirthEmployee = arrayBirth[2];
        let valueSexEmployee = "";
        if (parseInt(sex.value) === 55) {
            valueSexEmployee = "M";
        } else if (parseInt(sex.value) === 56) {
            valueSexEmployee = "H";
        }
        const curpFormatDataEmpl = lyricsFirtsSurname + lyricsSecondSurname + lyricsNameEmployee + yearBirthEmployee + monthBirthEmployee + dayBirthEmployee + valueSexEmployee;
        const divCurpInvalid = document.getElementById('divcurpinvalid');
        const txtCurpInvalid = document.getElementById('textcurpinvalid');
        let flagReturn;
        if (curp.length > 0) {
            const letterCurp = curp.substring(0, 11);
            if (letterCurp == curpFormatDataEmpl) {
                flagReturn = true;
                divCurpInvalid.classList.add('d-none');
                txtCurpInvalid.classList.textContent = '';
                txtCurpInvalid.classList.add('text-danger', 'font-labels');
                if (curpValidaMain(curp)) {
                    input.classList.remove('is-invalid');
                    btnSaveDataImss.disabled = false;
                } else {
                    input.classList.add('is-invalid');
                    btnSaveDataImss.disabled = true;
                }
            } else {
                flagReturn = false;
                divCurpInvalid.classList.remove('d-none');
                txtCurpInvalid.textContent = 'Los caracteres ingresados no coinciden con el formato';
                txtCurpInvalid.classList.add('text-danger', 'font-labels');
                btnSaveDataImss.disabled = true;
            }
        }
        return flagReturn;
    }

    //setTimeout(() => {
    //    name.addEventListener('change',  () => { validarInputMain(curp) });
    //    apep.addEventListener('change',  () => { validarInputMain(curp) });
    //    apem.addEventListener('change',  () => { validarInputMain(curp) });
    //    sex.addEventListener('change',   () => { validarInputMain(curp) });
    //    fnaci.addEventListener('change', () => { validarInputMain(curp) });
    //}, 2000);

    btnSaveDataImss.addEventListener('click', () => {
        const arrInput = [imss, rfc, curp, nivest, nivsoc];
        let validate = 0;
        for (let i = 0; i < arrInput.length; i++) {
            if (arrInput[i].hasAttribute("tp-select")) {
                if (arrInput[i].value == "0") {
                    const attrselect = arrInput[i].getAttribute('tp-select');
                    fshowtypealert('Atención', 'Selecciona una opción de ' + String(attrselect), 'warning', arrInput[i], 0);
                    validate = 1;
                    break;
                }
            } else {
                if (arrInput[i].value == "") {
                    fshowtypealert('Atención', 'Completa el campo ' + arrInput[i].placeholder, 'warning', arrInput[i], 0);
                    validate = 1;
                    break;
                }
            }
        }
        //let flagResultValidCurp = validarInputMain(curp);
        let flagResultValidCurp = true;

        if (flagResultValidCurp == false) {
            fshowtypealert('Atención', 'Curp invalida, compruebe', 'warning', curp, 0);
            validate = 1;
            setTimeout(() => { $("#nav-imss-tab").click(); }, 1000);
        }
        if (validate == 0) {
            $.ajax({
                url: "../SaveDataGeneral/ValidateEmployeeReg",
                type: "POST",
                data: { fieldCurp: curp.value, fieldRfc: rfc.value },
                success: (data) => {
                    if (data.Bandera === true && data.MensajeError === "none") {
                        gotoppage(navDataNomTab, '#nav-datanom-tab', "Ahora completa los datos del apartado Datos de nomina!");
                        const dataLocSto = {
                            key: 'imss', data: {
                                fecefe: fecefe.value,
                                clvimss: clvimss.value,
                                imss: imss.value,
                                rfc: rfc.value,
                                curp: curp.value,
                                nivest: nivest.value,
                                nivsoc: nivsoc.value,
                            }
                        };
                        objectDataTabImss.dataimss = dataLocSto;
                        localStorage.setItem('objectDataTabImss', JSON.stringify(objectDataTabImss));
                        localStorage.setItem('tabSelected', 'datanom');
                    } else {
                        fshowtypealert('Atención', 'Los datos del curp y rfc ya se encuentran registrados', 'warning', curp, 0);
                        fclearlocsto();
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        }
    });

    btnSaveDataNomina.addEventListener('click', () => {
        const arrInput = [fecefecnom, salmen, tipper, tipemp, nivemp, tipjor, tipcon, fecing, fecant, tipcontra, tippag, tiposueldo, politica, diferencia, transporte, categoriaEmp, pagoPorEmple];
        let validate = 0;
        for (let t = 0; t < arrInput.length; t++) {
            if (arrInput[t].hasAttribute("tp-select")) {
                let textpag;
                if (arrInput[t].id == "tipper") {
                    if (arrInput[t].value == "n") {
                        const attrselect = arrInput[t].getAttribute('tp-select');
                        fshowtypealert('Atención', 'Selecciona una opción de ' + String(attrselect), 'warning', arrInput[t], 0);
                        validate = 1;
                        break;
                    }
                } else {
                    if (arrInput[t].value == "0") {
                        const attrselect = arrInput[t].getAttribute('tp-select');
                        fshowtypealert('Atención', 'Selecciona una opción de ' + String(attrselect), 'warning', arrInput[t], 0);
                        validate = 1;
                        break;
                    }
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
                        validate = 1;
                        break;
                    }
                }
                if (arrInput[t].value == idcuentaah) {
                    if (cunuse.value.length < 18) {
                        fshowtypealert('Atención', 'El numero de cuenta de ahorro debe de contener 18 digitos y solo tiene ' + String(cunuse.value.length) + ' digitos.', 'warning', cunuse, 0);
                        validate = 1;
                        break;
                    }
                }
            } else {
                if (arrInput[t].hasAttribute("tp-date")) {
                    const attrdate = arrInput[t].getAttribute("tp-date");
                    const d = new Date();
                    let fechAct;
                    if (d.getMonth() + 1 < 10) {
                        fechAct = d.getFullYear() + "-" + "0" + (d.getMonth() + 1) + "-" + d.getDate();
                    } else {
                        fechAct = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();
                    }
                    if (arrInput[t].value != "" && attrdate == "less") {

                    } else if (arrInput[t].value != "" && attrdate == "higher") {

                    } else {
                        fshowtypealert('Atención', 'Completa el campo ' + String(arrInput[t].placeholder), 'warning', arrInput[t], 0);
                        validate = 1;
                        break;
                    }
                } else {
                    if (arrInput[t].value == "") {
                        fshowtypealert('Atención', 'Completa el campo ' + arrInput[t].placeholder, 'warning', arrInput[t], 0);
                        validate = 1;
                        break;
                    }
                }
            }
        }
        //let flagResultValidCurp = validarInputMain(curp);
        let flagResultValidCurp = true;

        if (flagResultValidCurp == false) {
            fshowtypealert('Atención', 'Curp invalida, compruebe', 'warning', curp, 0);
            validate = 1;
            setTimeout(() => { $("#nav-imss-tab").click(); }, 1000);
        }
        if (fecant.value != "" && fecing.value != "") {
            if (fecant.value > fecing.value) {
                fshowtypealert('Atención', 'La fecha de antiguedad no puede ser mayor a la fecha de ingreso', 'warning', fecant, 0);
                validate = 1;
            }
        }
        if (validate == 0) {
            $.ajax({
                url: "../SaveDataGeneral/ValidateEmployeeReg",
                type: "POST",
                data: { fieldCurp: curp.value, fieldRfc: rfc.value },
                success: (data) => {
                    if (data.Bandera === true && data.MensajeError === "none") {
                        gotoppage(navEstructureTab, '#nav-estructure-tab', "Ahora completa los datos del apartado Estructura!");
                        const dataLocSto = {
                            key: 'nom', data: {
                                clvnom: clvnom.value,
                                fecefecnom: fecefecnom.value, salmen: salmen.value, salmenact: salmenact.value,
                                tipper: tipper.value, tipemp: tipemp.value,
                                nivemp: nivemp.value, tipjor: tipjor.value,
                                tipcon: tipcon.value, fecing: fecing.value,
                                fecant: fecant.value, vencon: vencon.value,
                                tipcontra: tipcontra.value,
                                tiposueldo: tiposueldo.value,
                                politica: politica.value,
                                diferencia: diferencia.value,
                                transporte: transporte.value,
                                categoria: categoriaEmp.value,
                                pagopor: pagoPorEmple.value,
                                tippag: tippag.value,
                                banuse: banuse.value, cunuse: cunuse.value,
                            }
                        };
                        objectDataTabNom.datanom = dataLocSto;
                        localStorage.setItem('objectDataTabNom', JSON.stringify(objectDataTabNom));
                        localStorage.setItem('tabSelected', 'dataestructure');
                    } else {
                        fshowtypealert('Atención', 'Los datos del curp y rfc ya se encuentran registrados', 'warning', curp, 0);
                        fclearlocsto();
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        }
    });

    btnSaveDataEstructure.addEventListener('click', () => {
        const arrInput = [clvstr, fechefectpos, fechinipos];
        let validate = 0;
        for (let a = 0; a < arrInput.length; a++) {
            if (arrInput[a].hasAttribute('tp-date')) {
                const attrdate = arrInput[a].getAttribute('tp-date');
                if (arrInput[a].value != "" && attrdate == 'higher') {
                    const ds = new Date();
                    let fechAct;
                    let dateadd;
                    if (ds.getDate() < 10) {
                        dateadd = "0" + ds.getDate();
                    } else {
                        dateadd = ds.getDate();
                    }
                    if (ds.getMonth() + 1 < 10) {
                        fechAct = ds.getFullYear() + "-" + "0" + (ds.getMonth() + 1) + "-" + dateadd;
                    } else {
                        fechAct = ds.getFullYear() + "-" + (ds.getMonth() + 1) + "-" + dateadd;
                    }
                } else {
                    fshowtypealert('Atencion', 'Completa el campo ' + String(arrInput[a].placeholder), 'warning', arrInput[a], 0);
                    validate = 1;
                    break;
                }
            } else {
                if (arrInput[a].value == '') {
                    if (arrInput[a].id == "clvstr") {
                        fshowtypealert('Atencion', 'Completa el campo ' + String(arrInput[a].placeholder), 'warning', numpla, 0);
                        validate = 1;
                        break;
                    } else {
                        fshowtypealert('Atencion', 'Completa el campo ' + String(arrInput[a].placeholder), 'warning', arrInput[a], 0);
                        validate = 1;
                        break;
                    }
                }
            }
        }
        let flagResultValidCurp = true;
        if (flagResultValidCurp == false) {
            fshowtypealert('Atención', 'Curp invalida, compruebe', 'warning', curp, 0);
            validate = 1;
            setTimeout(() => { $("#nav-imss-tab").click(); }, 1000);
        }
        if (validate == 0) {
            $.ajax({
                url: "../SaveDataGeneral/ValidateEmployeeReg",
                type: "POST",
                data: { fieldCurp: curp.value, fieldRfc: rfc.value },
                success: (data) => {
                    if (data.Bandera === true && data.MensajeError === "none") {
                        gotoppage(navEstructureTab, '#nav-estructure-tab', "Los datos esperan para ser guardados");
                        const dataLocSto = {
                            key: 'estructure', data: {
                                numpla: numpla.value,
                                clvstr: clvstr.value,
                                depaid: depaid.value,
                                depart: depart.value,
                                puesid: puesid.value,
                                pueusu: pueusu.value,
                                emprep: emprep.value,
                                report: report.value,
                                localty: localty.value,
                                fechefectpos: fechefectpos.value,
                                fechinipos: fechinipos.value
                            }
                        };
                        objectDataTabEstructure.dataestructure = dataLocSto;
                        localStorage.setItem('objectDataTabEstructure', JSON.stringify(objectDataTabEstructure));
                        localStorage.setItem('tabSelected', 'dataestructure');
                    } else {
                        fshowtypealert('Atención', 'Los datos del curp y rfc ya se encuentran registrados', 'warning', curp, 0);
                        fclearlocsto();
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        }
    });

    const btnShowFieldsRequired = document.getElementById('btn-show-fields-required');

    localStorage.setItem("ShowFieldsRequired", 1);

    fShowFieldsRequiredDataGen = () => {
        const labelName = document.getElementById('label-name');
        const labelAPat = document.getElementById('label-patern');
        const labelAMat = document.getElementById('label-matern');
        const labelGene = document.getElementById('label-genero');
        const labelEstC = document.getElementById('label-estciv');
        const labelDBir = document.getElementById('label-datebirth');
        const labelPBir = document.getElementById('label-placebirth');
        const labelTitl = document.getElementById('label-title');
        const labelNati = document.getElementById('label-nationality');
        const labelStat = document.getElementById('label-state');
        const labelCity = document.getElementById('label-city');
        const labelCPos = document.getElementById('label-codpost');
        const labelColo = document.getElementById('label-colony');
        const labelStre = document.getElementById('label-street');
        const labelTMov = document.getElementById('label-telmovil');
        const labelEmai = document.getElementById('label-email');
        const arrInput  = [labelName, labelAPat, labelAMat, labelGene, labelEstC, labelDBir, labelPBir, labelTitl, labelNati, labelStat, labelCity, labelCPos, labelColo, labelStre, labelTMov, labelEmai];
        if (localStorage.getItem("ShowFieldsRequired") == "1") {
            for (let i = 0; i < arrInput.length; i++) {
                arrInput[i].classList.add('col-ico', 'font-weight-bold');
            }
            localStorage.setItem("ShowFieldsRequired", 2);
        } else if (localStorage.getItem("ShowFieldsRequired") == "2") {
            for (let i = 0; i < arrInput.length; i++) {
                arrInput[i].classList.remove('col-ico', 'font-weight-bold');
            }
            localStorage.setItem("ShowFieldsRequired", 1);
        }
    }

    fShowFieldsRequiredDataGen();

    fShowFieldsRequiredImss = () => {
        const labelRIms = document.getElementById('label-regimss');
        const labelEfim = document.getElementById('label-efimss');
        const labelRfc  = document.getElementById('label-rfc');
        const labelCurp = document.getElementById('label-curp');
        const labelNEst = document.getElementById('label-nivest');
        const labelNSoc = document.getElementById('label-nivsoc');
        const arrInput = [labelRIms, labelEfim, labelRfc, labelCurp, labelNEst, labelNSoc];
        for (let i = 0; i < arrInput.length; i++) {
            arrInput[i].classList.add('col-ico', 'font-weight-bold');
        }
    }

    fShowFieldsRequiredImss();

    fShowFieldsRequiredNomina = () => {
        const labelEfno = document.getElementById('label-efnom');
        const labelSMen = document.getElementById('label-salmen');
        const labelTPer = document.getElementById('label-tipper');
        const labelTEmp = document.getElementById('label-tipemp');
        const labelNEmp = document.getElementById('label-nivemp');
        const labelTJor = document.getElementById('label-tipjor');
        const labelTCon = document.getElementById('label-tipcon');
        const labelTTra = document.getElementById('label-tiptra');
        const labelFIng = document.getElementById('label-fing');
        const labelFRec = document.getElementById('label-frec');
        const labelTPag = document.getElementById('label-tippag');
        const labelPoli = document.getElementById('label-politica');
        const labelDife = document.getElementById('label-diferencia');
        const labelTran = document.getElementById('label-transporte');
        const labelRetr = document.getElementById('label-retroactivo');
        const labelCFon = document.getElementById('label-confondo');
        const labelCemp = document.getElementById('label-categoriaemp');
        const labelPagp = document.getElementById('label-pagopor');
        const arrInput = [labelEfno, labelSMen, labelTPer, labelTEmp, labelNEmp, labelTJor, labelTCon, labelTTra, labelFIng, labelFRec, labelTPag, labelPoli, labelDife, labelTran, labelRetr, labelCemp, labelPagp, labelCFon];
        for (let i = 0; i < arrInput.length; i++) {
            arrInput[i].classList.add('col-ico', 'font-weight-bold');
        }
    }

    fShowFieldsRequiredNomina();

    fShowFieldsRequiredPositions = () => {
        const labelPosi = document.getElementById('label-posit');
        const labelDepa = document.getElementById('label-depart');
        const labelPues = document.getElementById('label-puest');
        const labelLocl = document.getElementById('label-local');
        const labelRPat = document.getElementById('label-regpa');
        const labelRPos = document.getElementById('label-repos');
        const labelEPos = document.getElementById('label-efpos');
        const labelTSal = document.getElementById('label-tiposueldo');
        const arrInput = [labelPosi, labelDepa, labelPues, labelLocl, labelRPat, labelRPos, labelEPos, labelTSal];
        for (let i = 0; i < arrInput.length; i++) {
            arrInput[i].classList.add('col-ico', 'font-weight-bold');
        }
    }

    fShowFieldsRequiredPositions();

});