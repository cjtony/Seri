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

    function someMethodIThinkMightBeSlow() {
        const startTime = performance.now();
        const duration  = performance.now() - startTime;
        console.log(`someMethodIThinkMightBeSlow took ${duration}ms`);
    }

    someMethodIThinkMightBeSlow();

    const dateLocSto = localStorage.getItem("dateedit");
    const modeLocSto = localStorage.getItem("modeedit");
    if (modeLocSto != null) {
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
        if (dateLocSto != null) {
            if (dateLocSto != fechAct) {
                setTimeout(() => {
                    fclearlocsto(0);
                }, 2000);
            }
        }
    }

    const dateact = document.getElementById('dateact');
    let d = new Date();
    let monthact = d.getMonth() + 1, dayact = d.getDay(), montlet = "", daylet = "";
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

    const navDataGenTab = document.getElementById('nav-datagen-tab'),
        navImssTab = document.getElementById('nav-imss-tab'),
        navDataNomTab = document.getElementById('nav-datanom-tab'),
        navEstructureTab = document.getElementById('nav-estructure-tab');

    const btnsaveedit = document.getElementById('btn-save-edit'),
        btnClearLocSto = document.getElementById('btn-clear-localstorage'),
        btnSaveDataGen = document.getElementById('btn-save-data-gen'),
        btnSaveDataImss = document.getElementById('btn-save-data-imss'),
        btnSaveDataNomina = document.getElementById('btn-save-data-nomina'),
        btnSaveDataEstructure = document.getElementById('btn-save-data-estructure');

    let objectDataTabDataGen = {},
        objectDataTabImss = {},
        objectDataTabNom = {},
        objectDataTabEstructure = {};

    // Variables formulario Datos Generales \\ 
    const clvemp = document.getElementById('clvemp'),
        name = document.getElementById('name'), apep = document.getElementById('apepat'),
        apem = document.getElementById('apemat'), fnaci = document.getElementById('fnaci'),
        lnaci = document.getElementById('lnaci'),
        title = document.getElementById('title'),
        sex = document.getElementById('sex'), nacion = document.getElementById('nacion'),
        estciv = document.getElementById('estciv'), codpost = document.getElementById('codpost'),
        state = document.getElementById('state'), city = document.getElementById('city'),
        colony = document.getElementById('colony'),
        street = document.getElementById('street'), numberst = document.getElementById('numberst'),
        telfij = document.getElementById('telfij'),
        telmov = document.getElementById('telmov'),
        mailus = document.getElementById('mailus'),
        tipsan = document.getElementById('tipsan'),
        fecmat = document.getElementById('fecmat');
    const btnsaveeditdatagen = document.getElementById('btn-save-edit-data-gen');


    const vardatagen = [clvemp, name, apep, apem, fnaci, lnaci, title, sex, nacion, estciv, codpost, state, city, colony, street, numberst,
        telfij, telmov, mailus, tipsan, fecmat];

    fclearfieldsvar1 = () => {
        for (let i = 0; i < vardatagen.length; i++) {
            if (vardatagen[i].getAttribute('tp-select') != null) {
                vardatagen[i].value = "0";
            } else {
                vardatagen[i].value = "";
            }
        }
    }

    // Variables formulario IMSS \\
    const clvimss = document.getElementById('clvimss'), fechefecactimss = document.getElementById('fechefecactimss'), imss = document.getElementById('regimss'),
        rfc = document.getElementById('rfc'), curp = document.getElementById('curp'),
        nivest = document.getElementById('nivest'), nivsoc = document.getElementById('nivsoc'),
        fecefe = document.getElementById('fecefe');
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
    const clvnom = document.getElementById('clvnom'),
        fecefecnom = document.getElementById('fecefecnom'),
        fechefectact = document.getElementById('fechefectact'),
        salmen = document.getElementById('salmen'), tipper = document.getElementById('tipper'),
        tipemp = document.getElementById('tipemp'), nivemp = document.getElementById('nivemp'),
        tipjor = document.getElementById('tipjor'), tipcon = document.getElementById('tipcon'),
        fecing = document.getElementById('fecing'), fecant = document.getElementById('fecant'),
        vencon = document.getElementById('vencon'),
        //estats = document.getElementById('estats'),
        tipcontra = document.getElementById('tipcontra');
        //motinc = document.getElementById('motinc')
    const btnsaveeditdatanomina = document.getElementById('btn-save-edit-data-nomina');

    const vardatanomina = [clvnom, fechefectact, fecefecnom, tipper, salmen, tipemp, nivemp, tipjor, tipcon, fecing, fecant, vencon, tipcontra];
    fclearfieldsvar3 = () => {
        for (let i = 0; i < vardatanomina.length; i++) {
            if (vardatanomina[i].getAttribute('tp-select') != null) {
                vardatanomina[i].value = "0";
            } else {
                vardatanomina[i].value = "";
            }
        }
    }

    //Variables formulario estructura \\
    const numpla = document.getElementById('numpla'), clvstr = document.getElementById('clvstr'),
        clvposasig = document.getElementById('clvposasig'), clvstract = document.getElementById('clvstract'),
        depaid = document.getElementById('depaid'), puesid = document.getElementById('puesid'),
        emprep = document.getElementById('emprep'), report = document.getElementById('report'),
        depart = document.getElementById('depart'), pueusu = document.getElementById('pueusu'),
        tippag = document.getElementById('tippag'), banuse = document.getElementById('banuse'),
        cunuse = document.getElementById('cunuse'),
        //clvbank = document.getElementById('clvbank'),
        localty = document.getElementById('localty'), fechefectpos = document.getElementById('fechefectpos');
    fechinipos = document.getElementById('fechinipos'); fechefecposact = document.getElementById('fechefecposact');
    const btnsavedataall = document.getElementById('btn-save-data-all');
    const btnsaveeditdataest = document.getElementById('btn-save-edit-dataest');
    const vardataestructure = [clvstract, clvposasig, clvstr, numpla, depaid, puesid, emprep, report, depart, pueusu, localty, fechefectpos, fechinipos, fechefecposact];
    fclearfieldsvar4 = () => {
        for (let i = 0; i < vardataestructure.length; i++) {
            if (vardataestructure[i].getAttribute('tp-select') != null) {
                vardataestructure[i].value = "0";
            } else {
                vardataestructure[i].value = "";
            }
        }
    }

    fchecklocalstotab = () => {
        if (localStorage.getItem('tabSelected') === null) {
            localStorage.setItem('tabSelected', 'none');
        }
        if (localStorage.getItem('modedit') != null) {
            document.getElementById('btn-save-data-all').disabled = false;
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
            for (i in getDataTabDataGen) {
                if (getDataTabDataGen[i].key === "general") {
                    clvemp.value = getDataTabDataGen[i].data.clvemp;
                    name.value = getDataTabDataGen[i].data.name; apep.value = getDataTabDataGen[i].data.apep;
                    apem.value = getDataTabDataGen[i].data.apem; fnaci.value = getDataTabDataGen[i].data.fnaci;
                    lnaci.value = getDataTabDataGen[i].data.lnaci;
                    title.value = getDataTabDataGen[i].data.title;
                    sex.value = getDataTabDataGen[i].data.sex; nacion.value = getDataTabDataGen[i].data.nacion;
                    estciv.value = getDataTabDataGen[i].data.estciv; codpost.value = getDataTabDataGen[i].data.codpost;
                    state.value = getDataTabDataGen[i].data.state; city.value = getDataTabDataGen[i].data.city;
                    street.value = getDataTabDataGen[i].data.street;
                    numberst.value = getDataTabDataGen[i].data.numberst;
                    telfij.value = getDataTabDataGen[i].data.telfij; telmov.value = getDataTabDataGen[i].data.telmov;
                    mailus.value = getDataTabDataGen[i].data.mailus; tipsan.value = getDataTabDataGen[i].data.tipsan;
                    fecmat.value = getDataTabDataGen[i].data.fecmat;
                }
            }
            document.getElementById('icouser').classList.remove('d-none');
            if (localStorage.getItem('modeedit') != null) {
                document.getElementById('nameuser').textContent = "Editando al Empleado: " + clvemp.value + " - " + name.value + " " + apep.value + " " + apem.value + ".";
            } else {
                document.getElementById('nameuser').textContent = "Empleado: " + name.value + " " + apep.value + " " + apem.value + ".";
            }
        }
        if (JSON.parse(localStorage.getItem('objectDataTabImss')) != null) {
            const getDataTabImss = JSON.parse(localStorage.getItem('objectDataTabImss'));
            for (i in getDataTabImss) {
                if (getDataTabImss[i].key === "imss") {
                    fecefe.value = getDataTabImss[i].data.fecefe;
                    clvimss.value = getDataTabImss[i].data.clvimss;
                    fechefecactimss.value = getDataTabImss[i].data.fechefecactimss;
                    imss.value = getDataTabImss[i].data.imss;
                    rfc.value = getDataTabImss[i].data.rfc;
                    curp.value = getDataTabImss[i].data.curp; nivest.value = getDataTabImss[i].data.nivest;
                    nivsoc.value = getDataTabImss[i].data.nivsoc;
                }
            }
        }
        if (JSON.parse(localStorage.getItem('objectDataTabNom')) != null) {
            const getDataTabNom = JSON.parse(localStorage.getItem('objectDataTabNom'));
            for (i in getDataTabNom) {
                if (getDataTabNom[i].key === "nom") {
                    clvnom.value = getDataTabNom[i].data.clvnom;
                    fecefecnom.value = getDataTabNom[i].data.fecefecnom;
                    salmen.value = getDataTabNom[i].data.salmen; fechefectact.value = getDataTabNom[i].data.fechefectact;
                    tipper.value = getDataTabNom[i].data.tipper; tipemp.value = getDataTabNom[i].data.tipemp;
                    nivemp.value = getDataTabNom[i].data.nivemp; tipjor.value = getDataTabNom[i].data.tipjor;
                    tipcon.value = getDataTabNom[i].data.tipcon; fecing.value = getDataTabNom[i].data.fecing;
                    fecant.value = getDataTabNom[i].data.fecant; vencon.value = getDataTabNom[i].data.vencon;
                    //estats.value = getDataTabNom[i].data.estats;
                    tipcontra.value = getDataTabNom[i].data.tipcontra;
                    //motinc.value = getDataTabNom[i].data.motinc;
                    tippag.value = getDataTabNom[i].data.tippag;
                    banuse.value = getDataTabNom[i].data.banuse; cunuse.value = getDataTabNom[i].data.cunuse;
                    //clvbank.textContent = getDataTabNom[i].data.clvbank;
                    if (getDataTabNom[i].data.tippag == idcuentach) {
                        cunuse.setAttribute("maxlength", 11);
                    } else if (getDataTabNom[i].data.tippag == idcuentaah) {
                        cunuse.setAttribute("maxlength", 18);
                    }
                }
            }
        }
        if (JSON.parse(localStorage.getItem('objectDataTabEstructure')) != null) {
            const getDataEstructure = JSON.parse(localStorage.getItem('objectDataTabEstructure'));
            for (i in getDataEstructure) {
                if (getDataEstructure[i].key === "estructure") {
                    clvstract.value = getDataEstructure[i].data.clvstract;
                    clvposasig.value = getDataEstructure[i].data.clvposasig;
                    clvstr.value = getDataEstructure[i].data.clvstr;
                    numpla.value = getDataEstructure[i].data.numpla;
                    emprep.value = getDataEstructure[i].data.emprep; report.value = getDataEstructure[i].data.report;
                    depaid.value = getDataEstructure[i].data.depaid; depart.value = getDataEstructure[i].data.depart;
                    puesid.value = getDataEstructure[i].data.puesid; pueusu.value = getDataEstructure[i].data.pueusu;
                    localty.value = getDataEstructure[i].data.localty; fechefectpos.value = getDataEstructure[i].data.fechefectpos;
                    fechinipos.value = getDataEstructure[i].data.fechinipos; fechefecposact.value = getDataEstructure[i].data.fechefecposact;
                }
            }
        }
    }

    floaddatatabs();

    fasignsdates = () => {
        if (localStorage.getItem('modeedit') == null) {
            const date    = new Date();
            const frmonth = ((date.getMonth() + 1) < 10) ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
            const frday   = (date.getDate() < 10) ? "0" + date.getDate() : date.getDate();
            const fechact = date.getFullYear() + '-' + frmonth + '-' + frday;
            setTimeout(() => {
                fecefe.disabled       = false;
                fecefe.value          = fechact;
                fecefecnom.disabled   = false;
                fecefecnom.value      = fechact;
                fechefectpos.disabled = false;
                fechefectpos.value    = fechact;
                fechinipos.disabled   = false;
                fechinipos.value      = fechact;
            }, 1000);
        }
    };

    fasignsdates();

    fvalidatebuttonsactionmain = () => {
        if (localStorage.getItem("modeedit") == null) {
            btnsaveeditdatagen.classList.add('d-none');
            btnSaveDataGen.classList.remove('d-none');
            btnsaveeditdataimss.classList.add('d-none');
            btnSaveDataImss.classList.remove('d-none');
            btnSaveDataNomina.classList.remove('d-none');
            btnsaveeditdatanomina.classList.add('d-none');
            btnsavedataall.classList.remove('d-none');
            btnsaveeditdataest.classList.add('d-none');
        }
    }

    fclearlocsto = (type) => {
        let timerInterval;
        localStorage.removeItem('modeedit');
        localStorage.removeItem('dateedit');
        localStorage.removeItem('tabSelected');
        localStorage.removeItem('objectTabDataGen'); localStorage.removeItem('objectDataTabImss');
        localStorage.removeItem('objectDataTabNom'); localStorage.removeItem('objectDataTabEstructure');
        localStorage.removeItem('modedit');
        fchecklocalstotab(); fselectlocalstotab(); floaddatatabs();
        fclearfieldsvar1(); fclearfieldsvar2(); fclearfieldsvar3(); fclearfieldsvar4();
        document.getElementById('icouser').classList.add('d-none');
        document.getElementById('nameuser').textContent = '';
        fvalidatebuttonsactionmain();
        if (type == 1) {
            Swal.fire({
                title: "Esta seguro", text: "de limpiar los campos?", icon: "warning",
                confirmButtonText: "Aceptar", showCancelButton: true, cancelButtonText: "Cancelar",
                allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
            }).then((result) => {
                if (result.value) {
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
        //Command: toastr["info"](String(texttoastr));
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
            showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
            confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
        }).then((acepta) => {
            $("html, body").animate({ scrollTop: $(`#${element.id}`).offset().top - 50 }, 1000);
            if (clear == 1) { setTimeout(() => { element.focus(); setTimeout(() => { element.value = ""; }, 300); }, 1200); }
            else { setTimeout(() => { element.focus(); }, 1200); }
            if (element.id == "numpla") { setTimeout(() => { $("#btn-search-table-num-posicion").click(); }, 1500); }
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
            document.getElementById('icouser').classList.remove('d-none');
            document.getElementById('nameuser').textContent = "Empleado: " + name.value + " " + apep.value + " " + apem.value + ".";
            objectDataTabDataGen.datagen = dataLocStoGen;
            localStorage.setItem('objectTabDataGen', JSON.stringify(objectDataTabDataGen));
            localStorage.setItem('tabSelected', 'imss');
        }
    });

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
        if (validate == 0) {
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
        }
    });

    btnSaveDataNomina.addEventListener('click', () => {
        const arrInput = [fecefecnom, salmen, tipper, tipemp, nivemp, tipjor, tipcon, fecing, tipcontra, tippag];
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
                        //if (arrInput[t].value > fechAct) {
                        //    fshowtypealert('Atención', 'La ' + arrInput[t].placeholder + ' seleccionada ' + arrInput[t].value + ' no puede ser mayor a la fecha actual', 'warning', arrInput[t], 1);
                        //    validate = 1;
                        //    break;
                        //}
                    } else if (arrInput[t].value != "" && attrdate == "higher") {
                        //if (arrInput[t].value < fechAct) {
                        //    fshowtypealert('Atención', 'La fecha de ' + arrInput[t].placeholder + ' seleccionada ' + arrInput[t].value + ' no puede ser menor a la fecha actual', 'warning', arrInput[t], 1);
                        //    validate = 1;
                        //    break;
                        //}
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
        if (validate == 0) {
            gotoppage(navEstructureTab, '#nav-estructure-tab', "Ahora completa los datos del apartado Estructura!");
            const dataLocSto = {
                key: 'nom', data: {
                    clvnom: clvnom.value,
                    fecefecnom: fecefecnom.value, salmen: salmen.value,
                    tipper: tipper.value, tipemp: tipemp.value,
                    nivemp: nivemp.value, tipjor: tipjor.value,
                    tipcon: tipcon.value, fecing: fecing.value,
                    fecant: fecant.value, vencon: vencon.value,
                    //estats: estats.value,
                    tipcontra: tipcontra.value,
                    //motinc: motinc.value,
                    tippag: tippag.value,
                    banuse: banuse.value, cunuse: cunuse.value,
                    //clvbank: clvbank.textContent
                }
            };
            objectDataTabNom.datanom = dataLocSto;
            localStorage.setItem('objectDataTabNom', JSON.stringify(objectDataTabNom));
            localStorage.setItem('tabSelected', 'dataestructure');
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
                    //if (arrInput[a].value < fechAct) {
                    //    fshowtypealert('Atencion', 'La fecha ' + arrInput[a].value + ' es incorrecta, debe de ser mayor a la fecha actual', 'warning', arrInput[a], 1);
                    //    validate = 1;
                    //    break;
                    //}
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
        if (validate == 0) {
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
        }
    });


});