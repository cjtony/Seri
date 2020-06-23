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

    // ** Configuracion toastrjs ** \\

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-left",
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

    /*
     *  VARIABLES
     */

    let name       = document.getElementById('name');
    const state    = document.getElementById('state');
    const codpost  = document.getElementById('codpost');
    const city     = document.getElementById('city');
    const colony   = document.getElementById('colony');
    const street   = document.getElementById('street');
    const numberst = document.getElementById('numberst');
    const banuse   = document.getElementById('banuse');
    const cunuse   = document.getElementById('cunuse');
    const tippag   = document.getElementById('tippag');
    const nivtab   = document.getElementById('nivtab');
    //const clvbank  = document.getElementById('clvbank');
    const curp     = document.getElementById('curp');

    const btnVerifCodPost = document.getElementById('btn-verif-codpost');
    const btnSaveDataImss = document.getElementById('btn-save-data-imss');
    // ** Funcion que deshabilita los inputs de domicilio  ** \\
    fdisabledfields = (flag) => {
        colony.disabled = flag;
        street.disabled = flag;
        numberst.disabled = flag;
    }

    city.disabled = true;
    codpost.disabled = true;
    btnVerifCodPost.disabled = true;

    /*
     * FUNCION BLOQUEA CAMPOS DEPENDIENDO LA OPCIÓN
     */

    fdatabank = (flag) => {
        banuse.disabled = flag;
        cunuse.disabled = flag;
    }

    // ** EJECUCIÓN DE FUNCION BLOQUEA CAMPOS BANCARIOS ** \\

    //fdatabank(true);

    /*
     * VARIABLES DE LOCALSTORAGE 
     */

    const getDataTabDataGen = JSON.parse(localStorage.getItem('objectTabDataGen'));
    const getDataTabImss = JSON.parse(localStorage.getItem('objectDataTabImss'));
    const getDataEstructure = JSON.parse(localStorage.getItem('objectDataTabEstructure'));
    const getDataTabNom = JSON.parse(localStorage.getItem('objectDataTabNom'));

    /*
     * VARIABLES DE CATALOGOS
     */

    /* Datos Generales */
    const title = document.getElementById('title');
    const sex = document.getElementById('sex');
    const estciv = document.getElementById('estciv');
    const nacion = document.getElementById('nacion');

    /* Datos IMSS */
    const nivsoc = document.getElementById('nivsoc');

    /* Datos Nomina */
    const tipper = document.getElementById('tipper');
    const tipemp = document.getElementById('tipemp');
    const nivemp = document.getElementById('nivemp');
    const tipjor = document.getElementById('tipjor');
    const tipcon = document.getElementById('tipcon');
    const tipcontra = document.getElementById('tipcontra');
    //const motinc = document.getElementById('motinc');


    /* 
     * FUNCION GENERAL PARA CARGAR DATOS DEL CATALOGO GENERAL
     */

    floaddatagentype = (element, state, type, keycat, keycam, typeloc) => {
        try {
            $.ajax({
                url: "../Empleados/LoadDataCatGen",
                type: "POST",
                data: { state: state, type: String(type), keycat: parseInt(keycat), keycam: parseInt(keycam) },
                success: (data) => {
                    const quantity = data.length;
                    var fieldval;
                    if (typeloc === 'nom') {
                        if (JSON.parse(localStorage.getItem('objectDataTabNom')) != null) {
                            for (i in getDataTabNom) {
                                if (getDataTabNom[i].key === "nom") {
                                    val = String(element.id);
                                    fieldval = getDataTabNom[i].data[val];
                                }
                            }
                        }
                    } else if (typeloc === 'general') {
                        for (t in getDataTabDataGen) {
                            if (getDataTabDataGen[t].key === "general") {
                                val = String(element.id);
                                fieldval = getDataTabDataGen[t].data[val];
                            }
                        }
                    } else if (typeloc === 'imss') {
                        if (JSON.parse(localStorage.getItem('objectDataTabImss')) != null) {
                            for (i in getDataTabImss) {
                                if (getDataTabImss[i].key === "imss") {
                                    val = String(element.id);
                                    fieldval = getDataTabImss[i].data[val];
                                }
                            }
                        }
                    }
                    if (quantity > 0) {
                        for (i = 0; i < data.length; i++) {
                            if (fieldval == data[i].iId) {
                                element.innerHTML += `<option selected value="${data[i].iId}">${data[i].sValor}</option>`;
                            } else {
                                if (data[i].sValor == 'SOLTERO') {
                                    element.innerHTML += `<option selected value="${data[i].iId}">${data[i].sValor}</option>`;
                                } else {
                                    element.innerHTML += `<option value="${data[i].iId}">${data[i].sValor}</option>`;
                                }
                            }
                        }
                    } else {
                        console.error('Ocurrio un problema al cargar');
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        } catch (error) {
            if (error instanceof TypeError) {
                console.log("TypeError ", error);
            } else if (error instanceof RangeError) {
                console.log("RangeError ", error);
            } else if (error instanceof EvalError) {
                console.log("EvalError", error);
            }
        }
    }

    // ** Ejecución de la carga de los datos del titulo del empleado M -> DATOS GENERALES ** \\
    floaddatagentype(title, 0, 'Active/Desactive', 0, 8, 'general');

    // ** Ejecución de la carga de los datos del estado civil del empleado M -> DATOS GENERALES ** \\
    floaddatagentype(estciv, 0, 'Active/Desactive', 0, 6, 'general');

    // ** Ejecución de la carga de los datos del genero del empleado M -> DATOS GENERALES ** \\
    floaddatagentype(sex, 0, 'Active/Desactive', 0, 7, 'general');

    // ** Ejecución de la carga de los datos del nivel socioeconomico del empleado M -> DATOS GENERALES ** \\
    floaddatagentype(nivsoc, 0, 'Active/Desactive', 0, 10, 'imss');

    // ** Ejecución de la carga de los datos del nivel del empleado M -> DATOS NOMINA ** \\
    floaddatagentype(nivemp, 0, 'Active/Desactive', 0, 11, 'nom');

    // ** Ejecución de la carga de los datos del tipo de empleado M -> DATOS NOMINA ** \\
    floaddatagentype(tipemp, 0, 'Active/Desactive', 0, 12, 'nom');

    // ** Ejecución de la carga de los datos del tipo de jornada del empleado M -> DATOS NOMINA ** \\
    floaddatagentype(tipjor, 0, 'Active/Desactive', 0, 13, 'nom');

    // ** Ejecución de la carga de los datos del tipo de contrato del empleado M -> DATOS NOMINA ** \\
    floaddatagentype(tipcon, 0, 'Active/Desactive', 0, 14, 'nom');

    // ** Ejecución de la carga de los datos del tipo de contratacion del empleado M -> DATOS NOMINA ** \\
    floaddatagentype(tipcontra, 0, 'Active/Desactive', 0, 19, 'nom');

    // ** Ejecución de la carga de los datos deL motivo de incremento del empleado M -> DATOS NOMINA ** \\
    //floaddatagentype(motinc, 0, 'Active/Desactive', 0, 21, 'nom');

    // ** Ejecución de la carga de los datos del tipo de pago M ->  DATOS NOMINA ** \\
    floaddatagentype(tippag, 0, 'Active/Desactive', 0, 22, 'nom');

    floaddatanacionalidades = () => {
        try {
            $.ajax({
                url: "../Empleados/LoadNacions",
                type: "POST",
                data: {},
                success: (data) => {
                    const quantity = data.length;
                    let naciond;
                    let flag = false;
                    for (t in getDataTabDataGen) {
                        if (getDataTabDataGen[t].key === "general") {
                            naciond = getDataTabDataGen[t].data.nacion;
                        }
                    }
                    if (quantity > 0) {
                        for (let i = 0; i < data.length; i++) {
                            if (naciond == data[i].iIdNacionalidad) {
                                nacion.innerHTML += `<option selected value="${data[i].iIdNacionalidad}">${data[i].sDescripcion}</option>`;
                                flag = true;
                            } else {
                                if (data[i].iIdNacionalidad == 484) {
                                    if (flag == false) {
                                        nacion.innerHTML += `<option selected value="${data[i].iIdNacionalidad}">${data[i].sDescripcion}</option>`;
                                    } else {
                                        nacion.innerHTML += `<option value="${data[i].iIdNacionalidad}">${data[i].sDescripcion}</option>`;
                                    }
                                } else {
                                    nacion.innerHTML += `<option value="${data[i].iIdNacionalidad}">${data[i].sDescripcion}</option>`;
                                }
                            }
                        }
                    } else {
                        console.log('No hay registros');
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
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

    // ** Ejecución de la carga de las nacionalidades M -> DATOS GENERALES ** \\
    floaddatanacionalidades();

    floaddatatypeper = () => {
        try {
            $.ajax({
                url: "../Empleados/LoadTypePer",
                type: "POST",
                data: {},
                success: (data) => {
                    const quantity = data.length;
                    let typerd;
                    if (JSON.parse(localStorage.getItem('objectDataTabNom')) != null) {
                        for (i in getDataTabNom) {
                            if (getDataTabNom[i].key === "nom") {
                                typerd = getDataTabNom[i].data.tipper;
                            }
                        }
                    }
                    if (quantity > 0) {
                        for (let i = 0; i < data.length; i++) {
                            if (data[i].iId == 0 || data[i].iId == 2 ||data[i].iId == 3) {
                                if (typerd == data[i].iId) {
                                    tipper.innerHTML += `<option selected value="${data[i].iId}">${data[i].sValor}</option>`;
                                } else {
                                    tipper.innerHTML += `<option value="${data[i].iId}">${data[i].sValor}</option>`;
                                }
                            }
                        }
                    } else {
                        console.log('No hay registros');
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
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
    // ** Ejecución de la carga de los datos del tipo de periodo del empleado --> DATOS NOMINA ** \\
    floaddatatypeper();

    // INICIO FUNCIONALIDADES ESTADOS \\

    // ** Funcion que carga los estados ** \\
    floadstates = () => {
        try {
            $.ajax({
                url: "../Empleados/LoadStates",
                type: "POST",
                data: {},
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    const quantity = data.length;
                    let stated;
                    for (t in getDataTabDataGen) {
                        if (getDataTabDataGen[t].key === "general") {
                            stated = getDataTabDataGen[t].data.state;
                        }
                    }
                    if (quantity > 0) {
                        for (i = 0; i < data.length; i++) {
                            if (stated == data[i].iId) {
                                state.innerHTML += `
                                    <option selected value="${data[i].iId}">${data[i].sValor}</option>
                                `;
                            } else {
                                state.innerHTML += `
                                    <option value="${data[i].iId}">${data[i].sValor}</option>
                                `;
                            }
                        }
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        } catch (error) {
            if (error instanceof TypeError) {
                console.log("TypeError ", error);
            } else if (error instanceof RangeError) {
                console.log("RangeError ", error);
            } else if (error instanceof EvalError) {
                console.log("EvalError", error);
            }
        }
    }

    floadstates();

    fvalidatestate = () => {
        colony.innerHTML = '<option value="0">Selecciona</option>'
        fdisabledfields(true);
        city.value     = "";
        street.value   = "";
        numberst.value = "";
        codpost.value  = "";
        if (state.value != "0") {
            codpost.disabled = false;
            setTimeout(() => { codpost.focus() }, 500);
        } else {
            codpost.disabled = true;
        }
    }

    state.addEventListener('change', fvalidatestate);

    fvalidatecodpost = () => {
        if (codpost.value.length == 5) {
            btnVerifCodPost.disabled = false;
            btnVerifCodPost.classList.add('btn-info');
        } else {
            btnVerifCodPost.disabled = true;
            btnVerifCodPost.classList.remove('btn-info');
            colony.value      = "0";
            colony.disabled   = true;
            city.value        = "";
            city.disabled     = true;
            street.value      = "";
            street.disabled   = true;
            numberst.value    = "";
            numberst.disabled = true;
        }
    }

    codpost.addEventListener('keyup', fvalidatecodpost);

    fvalidatestatecodpost = (foc, paramstate) => {
        colony.innerHTML = "<option value='0'>Selecciona</option>";
        let statesend = state.value;
        if (paramstate != 0) {
            statesend = paramstate;
        }
        if (codpost.value.length === 5) {
            document.getElementById('load-spinner').classList.remove('d-none');
            btnVerifCodPost.classList.add('d-none');
            setTimeout(() => {
                $.ajax({
                    url: "../Empleados/LoadInformationHome",
                    type: "POST",
                    data: { codepost: codpost.value, state: statesend },
                    success: (data) => {
                        if (data.length > 0) {
                            fdisabledfields(false);
                            let colonyd;
                            for (t in getDataTabDataGen) {
                                if (getDataTabDataGen[t].key === "general") {
                                    colonyd = getDataTabDataGen[t].data.colony;
                                }
                            }
                            for (i = 0; i < data.length; i++) {
                                city.value = data[i].sCiudad;
                                if (data[i].sColonia != "") {
                                    if (data[i].sColonia.toUpperCase() == colonyd) {
                                        colony.innerHTML += `<option selected value="${data[i].sColonia.toUpperCase()}">${data[i].sColonia}</option>`;
                                    } else {
                                        colony.innerHTML += `<option value="${data[i].sColonia.toUpperCase()}">${data[i].sColonia}</option>`;
                                    }
                                }
                            }
                            if (foc == 1) {
                                setTimeout(() => {
                                    colony.focus();
                                }, 500);
                            }
                        } else {
                            Swal.fire({
                                title: "Atención",
                                text: "El código postal ingresado es incorrecto no pertenece al estado seleccionado",
                                icon: "warning",
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                setTimeout(() => { codpost.focus(); }, 800)
                            });
                        }
                        btnVerifCodPost.classList.remove('d-none');
                        document.getElementById('load-spinner').classList.add('d-none')
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            }, 1000);
        } else {
            Swal.fire({
                title: "Atención", text: "El código postal debe de contener 5 caracteres", icon: "warning",
                showClass: { popup: 'animated fadeInDown faster' },
                hideClass: { popup: 'animated fadeOutUp faster' },
                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
            }).then((acepta) => {
                setTimeout(() => { codpost.focus(); }, 800);
            });
        }
    }

    btnVerifCodPost.addEventListener('click', () => {
        fvalidatestatecodpost(1,0);
    });

    fvalidatelocalstorageinfdom = () => {
        let stateloc = 0;
        let flag = false;
        if (JSON.parse(localStorage.getItem("objectTabDataGen")) != null) {
            for (dt in getDataTabDataGen) {
                if (getDataTabDataGen[dt].data.colony != "" && getDataTabDataGen[dt].data.street != ""
                    && getDataTabDataGen[dt].data.codpost != "") {
                    stateloc = getDataTabDataGen[dt].data.state;
                    flag = true;
                    break;
                }
            }
        }
        if (flag) {
            fdisabledfields(false);
            codpost.disabled = false;
            fvalidatestatecodpost(0, stateloc);
        } else {
            fdisabledfields(true);
        }
    }

    fvalidatelocalstorageinfdom();

    // FIN FUNCIONALIDADES ESTADOS \\

    // CARGA DE CATALOGOS VISTA IMMS \\



    floadnivelstudy = () => {
        try {
            $.ajax({
                url: "../Empleados/LoadNivelStudy",
                type: "POST",
                data: { state: 1, type: 'Active/Desactive', keynivel: 0 },
                success: (data) => {
                    const quantity = data.length;
                    let nivestd = 0;
                    if (JSON.parse(localStorage.getItem('objectDataTabImss')) != null) {
                        for (i in getDataTabImss) {
                            if (getDataTabImss[i].key === "imss") {
                                nivestd = getDataTabImss[i].data.nivest;
                            }
                        }
                    }
                    if (quantity > 0) {
                        for (var i = 0; i < data.length; i++) {
                            if (nivestd == data[i].iIdNivelEstudio) {
                                nivest.innerHTML += `<option selected value="${data[i].iIdNivelEstudio}">${data[i].sNombreNivelEstudio}</option>`;
                            } else {
                                nivest.innerHTML += `<option value="${data[i].iIdNivelEstudio}">${data[i].sNombreNivelEstudio}</option>`;
                            }
                        }
                    }
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

    floadnivelstudy();

    // CARGA DE CATALOGOS VISTA ESTRUCTURA \\

    floadniveltab = () => {
        try {
            $.ajax({
                url: "../Empleados/LoadTabs",
                type: "POST",
                data: { state: 1, type: 'Active/Desactive', keytab: 0 },
                success: (data) => {
                    const quantity = data.length;
                    let nivtabd = 0;
                    if (JSON.parse(localStorage.getItem('objectDataTabEstructure')) != null) {
                        for (i in getDataEstructure) {
                            if (getDataEstructure[i].key === "estructure") {
                                nivtabd = getDataEstructure[i].data.nivtab;
                            }
                        }
                    }
                    if (quantity > 0) {
                        for (i = 0; i < data.length; i++) {
                            if (nivtabd == data[i].iIdTabulador) {
                                nivtab.innerHTML += `<option selected value="${data[i].iIdTabulador}">${data[i].sTabulador}</option>`;
                            } else {
                                nivtab.innerHTML += `<option value="${data[i].iIdTabulador}">${data[i].sTabulador}</option>`;
                            }
                        }
                    } else {
                        console.error('Ocurrio un problema al cargar');
                    }
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

    //floadniveltab();

    let arrbank = [];

    floadbanks = () => {
        try {
            $.ajax({
                url: "../Empleados/LoadBanks",
                type: "POST",
                data: { keyban: 0 },
                success: (data) => {
                    const quantity = data.length;
                    let banused = 0;
                    if (JSON.parse(localStorage.getItem('objectDataTabNom')) != null) {
                        for (i in getDataTabNom) {
                            if (getDataTabNom[i].key === "nom") {
                                banused = getDataTabNom[i].data.banuse;
                            }
                        }
                    }
                    if (quantity > 0) {
                        for (i = 0; i < data.length; i++) {
                            arrbank.push(data[i]);
                            if (banused == data[i].iIdBanco) {
                                banuse.innerHTML += `<option selected value="${data[i].iIdBanco}">${data[i].sNombreBanco}</option>`;
                            } else {
                                banuse.innerHTML += `<option value="${data[i].iIdBanco}">${data[i].sNombreBanco}</option>`;
                            }
                        }
                    } else {
                        console.error('No se encontro ningun registro');
                    }
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

    floadbanks();

    // ** VALIDACION DE DIGITOS EN CUENTA ** \\

    const infobankct = document.getElementById('infobankct');
    const infobankch = document.getElementById('infobankch');

    fchangetippag = () => {
        if (tippag.value == idcuentach || tippag.value == idcuentaah) {
            fdatabank(false);
            if (tippag.value == idcuentach) {
                cunuse.setAttribute("maxlength", 11);
                infobankch.classList.remove('d-none');
                infobankct.classList.add('d-none');
            } else if (tippag.value == idcuentaah) {
                cunuse.setAttribute("maxlength", 18);
                infobankct.classList.remove('d-none');
                infobankch.classList.add('d-none');
            }
            //clvbank.textContent = '';
            //cunuse.value = "12313123";
            //banuse.value = "0";
        } else {
            banuse.value = "0";
            cunuse.value = "";
            //clvbank.textContent = '';
            infobankch.classList.add('d-none');
            infobankct.classList.add('d-none');
            fdatabank(true);
        }
    }

    if (JSON.parse(localStorage.getItem('objectDataTabNom')) != null) {
        for (i in getDataTabNom) {
            if (getDataTabNom[i].key === "nom") {
                if (getDataTabNom[i].data.banuse != "") {
                    fdatabank(false);
                } else {
                    fdatabank(true);
                }
            }
        }
        //fchangetippag();
        setTimeout(() => { fchangetippag(); }, 2000);
    }


    tippag.addEventListener('change', () => {
        cunuse.value = "";
        banuse.value = "0";
        fchangetippag();
    });

    $("#cunuse").keyup(function () {
        this.value = (this.value + '').replace(/[^0-9]/g, '');
    });

    $("#telfij").keyup(function () {
        this.value = (this.value + '').replace(/[^0-9]/g, '');
    });

    $("#telmov").keyup(function () {
        this.value = (this.value + '').replace(/[^0-9]/g, '');
    });

    $("#regimss").keyup(function () {
        this.value = (this.value + '').replace(/[^0-9]/g, '');
    });

    //banuse.addEventListener('change', () => {
    //    if (tippag.value == idcuentaah) {
    //        for (i = 0; i < arrbank.length; i++) {
    //            if (arrbank[i].iIdBanco == banuse.value) {
    //                if (String(arrbank[i].iClave).length == 2) {
    //                    clvbank.textContent = String(0) + String(arrbank[i].iClave);
    //                } else if (String(arrbank[i].iClave).length == 1) {
    //                    clvbank.textContent = String(0) + String(0) + String(arrbank[i].iClave);
    //                } else {
    //                    clvbank.textContent = arrbank[i].iClave;
    //                }
    //            }
    //        }
    //    }
    //});

    String.prototype.reverse = function () {
        var x = this.length;
        var cadena = "";
        while (x >= 0) {
            cadena = cadena + this.charAt(x);
            x--;
        }
        return cadena;
    };

    /*
     * Funcion que cuenta palabras en el nombre
     */

    function contar_palabras() {
        //Obtenemos el texto del campo
        var texto = document.getElementById("name").value;
        var apep = document.getElementById('apepat').value;
        var apem = document.getElementById('apemat').value;
        //Reemplazamos los saltos de linea por espacios
        texto = texto.replace(/\r?\n/g, " ");
        //Reemplazamos los espacios seguidos por uno solo
        texto = texto.replace(/[ ]+/g, " ");
        //Quitarmos los espacios del principio y del final
        texto = texto.replace(/^ /, "");
        texto = texto.replace(/ $/, "");
        //Troceamos el texto por los espacios
        var textoTroceado = texto.split(" ");
        //Contamos todos los trozos de cadenas que existen
        var numeroPalabras = textoTroceado.length;
        //Mostramos el número de palabras
        console.log(numeroPalabras);
        len = texto.split(' ');
        ape = apep.split('');
        lon = ''
        for (i = 0; i < len.length; i++) {
            lon += len[i].substring(0, 1);
            console.log(len[i].substring(0, 1));
        }
        console.log(apep.substring(0, 1));
        console.log(apem.substring(0, 1)); 
        console.log(lon.reverse());
        console.log(apep.substring(0, 1) + apem.substring(0, 1) + lon.reverse());
    }


    /*
    * Funcion que valida el formato de la curp
    */

    function curpValida(curp) {
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

    function validarInput(input) {
        var curp = input.value.toUpperCase();
        const nameE = name.value;
        const apePE = document.getElementById('apepat').value;
        const apeME = document.getElementById('apemat').value;
        const fNaci = document.getElementById('fnaci').value;
        const arrayBirth = fNaci.split("-");
        const lyricsFirtsSurname  = apePE.substring(0, 2).toUpperCase();
        const lyricsSecondSurname = apeME.substring(0, 1).toUpperCase();
        const lyricsNameEmployee  = nameE.substring(0, 1).toUpperCase();
        const yearBirthEmployee   = arrayBirth[0].substring(2, 4);
        const monthBirthEmployee  = arrayBirth[1];
        const dayBirthEmployee    = arrayBirth[2];
        let   valueSexEmployee    = "";
        if (parseInt(sex.value) === 55) {
            valueSexEmployee = "M";
        } else if (parseInt(sex.value) === 56) {
            valueSexEmployee = "H";
        }
        const curpFormatDataEmpl = lyricsFirtsSurname + lyricsSecondSurname + lyricsNameEmployee + yearBirthEmployee + monthBirthEmployee + dayBirthEmployee + valueSexEmployee;
        console.log('Prototipo curp: ' + curpFormatDataEmpl);
        const divCurpInvalid = document.getElementById('divcurpinvalid');
        const txtCurpInvalid = document.getElementById('textcurpinvalid');
        if (curp.length > 0) {
            const letterCurp = curp.substring(0, 11);
            console.log("Extracion: " + letterCurp);
            if (letterCurp == curpFormatDataEmpl) {
                divCurpInvalid.classList.add('d-none');
                txtCurpInvalid.classList.textContent = '';
                txtCurpInvalid.classList.add('text-danger', 'font-labels');
                if (curpValida(curp)) {
                    input.classList.remove('is-invalid');
                    btnSaveDataImss.disabled = false;
                } else {
                    input.classList.add('is-invalid');
                    btnSaveDataImss.disabled = true;
                }
            } else {
                divCurpInvalid.classList.remove('d-none');
                txtCurpInvalid.textContent = 'Los caracteres ingresados no coinciden con el formato';
                txtCurpInvalid.classList.add('text-danger', 'font-labels');
                btnSaveDataImss.disabled = true;
            }
        }
    }

    const rfc = document.getElementById('rfc');

    curp.addEventListener('change', () => {
        validarInput(curp);
    });

    curp.addEventListener('keyup', () => {
        if (rfc.value.length > 10) {
            if (curp.value.length > 10) {
                let formatrfc = String(rfc.value).substring(0, 10);
                let formatcurp = String(curp.value).substring(0, 10);
                if (formatrfc == formatcurp) {
                    curp.classList.remove('is-invalid');
                    //rfc.classList.remove('is-invalid');
                    btnSaveDataImss.disabled = false;
                } else {
                    curp.classList.add('is-invalid');
                    //rfc.classList.add('is-invalid');
                    btnSaveDataImss.disabled = true;
                }
            }
        }
    });

    rfc.addEventListener('keyup', () => {
        if (curp.value.length > 10) {
            if (rfc.value.length > 10) {
                let formatrfc = String(rfc.value).substring(0, 10);
                let formatcurp = String(curp.value).substring(0, 10);
                if (formatrfc == formatcurp) {
                    //curp.classList.remove('is-invalid');
                    rfc.classList.remove('is-invalid');
                    btnSaveDataImss.disabled = false;
                } else {
                    //curp.classList.add('is-invalid');
                    rfc.classList.add('is-invalid');
                    btnSaveDataImss.disabled = true;
                }
            }
        }
    });

});