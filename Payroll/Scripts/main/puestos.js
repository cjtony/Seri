$(function () {

    /* FUNCION QUE OBTIENE DATOS DEL CATALOGO GENERAL -> CLASIFICACION DEL PUESTO */
    floadcataloggeneral = (element, state, type, keycol, catalog) => {
        try {
            $.ajax({
                url: "../CatalogsTables/ClasifPuest",
                type: "POST",
                data: { state: state, type: type, keycla: keycol, catalog: catalog },
                success: (data) => {
                    const quantity = data.length;
                    if (quantity > 0) {
                        for (let i = 0; i < data.length; i++) {
                            element.innerHTML += `<option value="${data[i].iId}">${data[i].sValor}</option>`;
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
    /* FUNCION QUE CARGA LOS DATOS DEL SELECT TIPOS DE PUESTOS */
    floadtypesjobs = async (paramstr) => {
        try {
            if (paramstr != "") {
                await $.ajax({
                    url: "../SearchDataCat/TypesJobs",
                    type: "POST",
                    data: { typeJob: paramstr },
                    success: (data) => {
                        if (data.Bandera === true && data.MensajeError === "none") {
                            for (let i = 0; i < data.Datos.length; i++) {
                                typeregpuesto.innerHTML += `<option value="${data.Datos[i].iId},${data.Datos[i].sCodigo}">${data.Datos[i].sDescripcion}</option>`;
                            }
                        } else {

                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                alert('Accion no valida, no se cargaron los tipos de puestos');
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
    /* Funcion que carga los datos del select profesion familia  */
    floadproffamily = (state, type, keyprof, elementid) => {
        try {
            $.ajax({
                url: "../CatalogsTables/JobFamily",
                type: "POST",
                data: { state: state, type: type, keyprof: keyprof },
                success: (data) => {
                    const quantity = data.length;
                    if (quantity > 0) {
                        for (let i = 0; i < data.length; i++) {
                            elementid.innerHTML += `<option value="${data[i].iIdProfesionFamilia}">${data[i].sNombreProfesion}</option>`;
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
    /* CONSTANTES DE REGISTRAR UN NUEVO PUESTO */
    const typeregpuesto  = document.getElementById('typeregpuesto');
    const regcodpuesto   = document.getElementById('regcodpuesto');
    const regpuesto      = document.getElementById('regpuesto');
    const regdescpuesto  = document.getElementById('regdescpuesto');
    const proffamily     = document.getElementById('proffamily');
    const clasifpuesto   = document.getElementById('clasifpuesto');
    const regcolect      = document.getElementById('regcolect');
    const nivjerarpuesto = document.getElementById('nivjerarpuesto');
    const perfmanager    = document.getElementById('perfmanager');
    const tabpuesto      = document.getElementById('tabpuesto');
    /* CONSTANTES DE LA BUSQUEDA EN TIEMPO REAL DE PUESTOS */
    const searchpuestokey   = document.getElementById('searchpuestokey');
    const resultpuestos     = document.getElementById('resultpuestos');
    /* CONSTANTES DE LA BUSQUEDA EN TIEMPO REAL DE PUESTOS AL MOMENTO DE REGISTRAR UNA NUEVA POSICION */
    const searchpuestokeyadd = document.getElementById('searchpuestokeyadd');
    const resultpuestosadd   = document.getElementById('resultpuestosadd');
    const icoclosesearchpuestosadd = document.getElementById('ico-close-search-puestosadd');
    const btnclosesearchpuestosadd = document.getElementById('btn-close-search-puestosadd');
    /* FUNCION QUE LIMPIA EL CAMPO DE BUSQUEDA Y LA LISTA DE RESULTADOS */
    fclearsearchresultsadd = () => {
        searchpuestokeyadd.value   = '';
        resultpuestosadd.innerHTML = '';
        document.getElementById('noresultsjobs2').innerHTML = '';
        $("#searchpuesto").modal('hide');
        $("#registerposition").modal('show');
        setTimeout(() => { pueusureg.focus(); }, 1000);
    }
    /* EJECUCION DE FUNCION QUE LIMPIA EL CAMPO DE BUSQUEDA Y LISTA DE RESULTADOS */
    btnclosesearchpuestosadd.addEventListener('click', fclearsearchresultsadd);
    icoclosesearchpuestosadd.addEventListener('click', fclearsearchresultsadd);
    /* CONSTANTES DEL FORMULARIO DE REGISTRAR UNA NUEVA POSICION Y SELECCIONAR UN PUESTO */
    const btnsearchpuesto = document.getElementById('btn-search-puesto');
    const pueusureg       = document.getElementById('pueusureg');
    const puesid          = document.getElementById('puesid');
    const puesidedit      = document.getElementById('puesidedit');
    const pueusuedit      = document.getElementById('pueusuedit');
    /* CONSTANTES DE LA EDICION DEL PUESTO */
    const clvpuesto         = document.getElementById('clvpuesto');
    const edicodpuesto      = document.getElementById('edicodpuesto');
    edicodpuesto.readOnly   = true;
    const edipuesto         = document.getElementById('edipuesto');
    const edidescpuesto     = document.getElementById('edidescpuesto');
    const ediproffamily     = document.getElementById('ediproffamily');
    const edicolect         = document.getElementById('edicolect');
    const ediclasifpuesto   = document.getElementById('ediclasifpuesto');
    const edinivjerarpuesto = document.getElementById('edinivjerarpuesto');
    const ediperfmanager    = document.getElementById('ediperfmanager');
    const editabpuesto      = document.getElementById('editabpuesto');
    const btnEdiPuesto = document.getElementById('btnedipuesto');
    /* CONSTANTES DE LA BUSQUEDA DE PUESTOS AL CREAR UNA NUEVA POSICION */
    const btnregisterpuesto = document.getElementById('btnregisterpuesto');
    /* CONSTANTES DE BOTONES DE VENTANA MODAL BUSQUEDA DE PUESTOS*/
    const btnModalSearchPuesto = document.getElementById('btn-modal-search-puesto');
    const btnSavePuesto        = document.getElementById('btnsavepuesto');
    const btnCloseRegisterPbtn = document.getElementById('btn-close-registerpuesto-btn');
    const icoCloseRegisterPbtn = document.getElementById('ico-close-registerpuesto-btn');
    const btnRegisterPuestobtn = document.getElementById('btnregisterpuestobtn');
    /* CONSTANTES DE BOTONES DE LA VENTANA MODAL DE REGISTRO PUESTO */
    const btnClearFieldsJob = document.getElementById('btn-clear-fields-job');
    const icoClearFieldsJob = document.getElementById('ico-clear-fields-job');
    const btncloseeditjob   = document.getElementById('btn-close-edit-job');
    const icocloseeditjob   = document.getElementById('ico-close-edit-job');
    /* FUNCION QUE LIMPIA LOS CAMPOS DE REGISTRAR UN NUEVO PUESTO */
    fclearfieldsnewjob = () => {
        //localStorage.removeItem('modalbtnpuesto');
        regcodpuesto.value = "";   regpuesto.value = "";
        regdescpuesto.value = "";  proffamily.value = "0";
        clasifpuesto.value = "0";  regcolect.value = "0";
        nivjerarpuesto.value = "0"; perfmanager.value = "0";
        tabpuesto.value = "0";
        setTimeout(() => { searchpuestokey.focus(); }, 1000);
        if (localStorage.getItem('modalbtnpuesto') != null) {
            $("#searchpuestobtn").modal('show');
            setTimeout(() => { searchpuestokey.focus(); }, 1000);
        } else {
            $("#searchpuesto").modal('show');
            setTimeout(() => { searchpuestokeyadd.focus(); }, 1000);
        }
    }
    /* EJECUCION DE LIMPIEZA DE LOS CAMPOS DE REGISTRAR UN NUEVO PUESTO */
    btnClearFieldsJob.addEventListener('click', fclearfieldsnewjob);
    icoClearFieldsJob.addEventListener('click', fclearfieldsnewjob);
    btncloseeditjob.addEventListener('click', fclearfieldsnewjob);
    icocloseeditjob.addEventListener('click', fclearfieldsnewjob);
    /* EJECUCION DE EVENTO QUE CIERRA LA VENTANA MODAL DE REGISTRAR UNA NUEVA POSICION */
    btnsearchpuesto.addEventListener('click', () => { $("#registerposition").modal('hide'); setTimeout(() => { searchpuestokeyadd.focus(); }, 1000); });
    /* EJECUCION DE CARGA LOS DATOS DEL SELECT CLASIFICACION PUESTO */
    floadcataloggeneral(ediclasifpuesto, 0, 'Active/Desactive', 0, 23);
    /* EJECUCION DE CARGA LOS DATOS DEL SELECT TIPO DE PUESTO */
    floadtypesjobs('puesto');
    /* EJECUCION DE CARGA DE LOS DATOS DEL SELECT PROFESION FAMILIA */
    floadproffamily(1, 'Active/Desactive', 0, ediproffamily);
    /* EJECUCION DE CARGA DE LOS DATOS DEL SELECT COLECTIVO */
    floadcataloggeneral(edicolect, 0, 'Active/Desactive', 0, 24);
    /* EJECUCION DE CARGA DE LOS DATOS DEL SELECT NIVEL JERARQUICO */
    floadcataloggeneral(edinivjerarpuesto, 0, 'Active/Desactive', 0, 25);
    /* EJECUCION DE CARGA DE LOS DATOS DEL SELECT PERFOMANCE MANAGER */
    floadcataloggeneral(ediperfmanager, 0, 'Active/Desactive', 0, 26);
    /* EJECUCION DE CARGA DE LOS DATOS DEL SELECT TABULADOR */
    floadcataloggeneral(editabpuesto, 0, 'Active/Desactive', 0, 27);
    /* EJECUCION DE CREACION DE LOCAL STORAGE */
    localStorage.removeItem('modalbtnpuesto');
    btnModalSearchPuesto.addEventListener('click', () => { localStorage.setItem('modalbtnpuesto', 1); setTimeout(() => { searchpuestokey.focus(); }, 1000); });
    btnCloseRegisterPbtn.addEventListener('click', () => {
        localStorage.removeItem('modalbtnpuesto'); searchpuestokey.value = '';
        resultpuestos.innerHTML = '';
        document.getElementById('noresultsjobs1').innerHTML = '';
    });
    icoCloseRegisterPbtn.addEventListener('click', () => {
        localStorage.removeItem('modalbtnpuesto');
        searchpuestokey.value = ''; resultpuestos.innerHTML = '';
        document.getElementById('noresultsjobs1').innerHTML = '';
        
    });
    /* EJECUCION DE FUNCION QUE OCULTA LA VENTANA DE BUSQUEDA DE PUESTOS */
    btnRegisterPuestobtn.addEventListener('click', () => {
        $("#searchpuestobtn").modal('hide');
        searchpuestokey.value = ''; resultpuestos.innerHTML = '';
        document.getElementById('noresultsjobs1').innerHTML = '';
        regcodpuesto.disabled = true;
        setTimeout(() => {
            typeregpuesto.focus();
            //regcodpuesto.focus();
        }, 1000);
    });
    /* EJECUCION DE EVENTO QUE OCULTA LA VENTANA DE BUSQUEDA DE PUESTOS AL REGISTRAR UNA NUEVA POSICION */
    btnregisterpuesto.addEventListener('click', () => {
        $("#searchpuesto").modal('hide');
        searchpuestokeyadd.value = '';
        document.getElementById('noresultsjobs2').innerHTML = '';
        resultpuestosadd.innerHTML = '';
        setTimeout(() => { regcodpuesto.focus(); }, 1000);
    });
    /* EJECUCION DE FUNCION QUE CARGA LAS PROFESIONES FAMILIA AL REGISTRAR UN NUEVO PUESTO */
    floadproffamily(1, 'Active/Desactive', 0, proffamily);
    /* EJECUCION DE FUNCION QUE CARGA LAS CLASIFICACIONES DEL PUESTO AL REGISTRAR UN NUEVO PUESTO */
    floadcataloggeneral(clasifpuesto, 0, 'Active/Desactive', 0, 23);
    /* EJECUCION DE FUNCION QUE CARGA EL SELECT COLECTIVO AL REGISTRAR UN NUEVO PUESTO */
    floadcataloggeneral(regcolect, 0, 'Active/Desactive', 0, 24);
    /* EJECUCION DE FUNCION QUE CARGA EL SELECT NIVEL JERARQUICO AL REGISTRAR UN NUEVO PUESTO */
    floadcataloggeneral(nivjerarpuesto, 0, 'Active/Desactive', 0, 25);
    /* EJECUCION DE FUNCION QUE CARGA EL SELECT PERFOMANCE MANAGER AL REGISTRAR UN NUEVO PUESTO */
    floadcataloggeneral(perfmanager, 0, 'Active/Desactive', 0, 26); 
    /* EJECUCION DE FUNCION QUE CARGA EL SELECT DE TABULADOR AL REGISTRAR UN NUEVO PUESTO */
    floadcataloggeneral(tabpuesto, 0, 'Active/Desactive', 0, 27);
    /* FUNCION QUE CARGA LOS DATOS DE LA REGIONAL SELECCIONADA POR EL USUARIO */
    feditdatapuesto = (param) => {
        searchpuestokey.value = '';
        resultpuestos.innerHTML = '';
        try {
            $.ajax({
                url: "../SearchDataCat/DataSelectPuesto",
                type: "POST",
                data: { clvpuesto: param },
                success: (data) => {
                    console.log(data);
                    $("#searchpuestobtn").modal('hide');
                    $("#editpuesto").modal('show');
                    setTimeout(() => { edicodpuesto.focus(); }, 1000);
                    document.getElementById('namepuestoedi').textContent = data.sNombrePuesto;
                    clvpuesto.value         = data.iIdPuesto;
                    edicodpuesto.value      = data.sCodigoPuesto;
                    edipuesto.value         = data.sNombrePuesto;
                    edidescpuesto.value     = data.sDescripcionPuesto;
                    ediproffamily.value     = data.iIdProfesionFamilia;
                    edicolect.value         = data.iIdColectivo;
                    ediclasifpuesto.value   = data.iIdClasificacionPuesto;
                    edinivjerarpuesto.value = data.iIdNivelJerarquico;
                    ediperfmanager.value    = data.iIdPerfomanceManager;
                    editabpuesto.value      = data.iIdTabulador;
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
    /* FUNCION QUE HACE LA BUSQUEDA EN TIEMPO REAL */
    fsearchkeyuppuesto = () => {
        noresultsjobs1.innerHTML = '';
        resultpuestos.innerHTML = '';
        try {
            if (searchpuestokey.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchPuesto",
                    type: "POST",
                    data: { wordsearch: searchpuestokey.value },
                    success: (data) => {
                        resultpuestos.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                resultpuestos.innerHTML += `<button onclick="feditdatapuesto(${data[i].iIdPuesto})" class="animated fadeIn border-left-primary list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back">${number} - ${data[i].sCodigoPuesto} - ${data[i].sNombrePuesto} <i class="fas fa-edit ml-2 text-warning fa-lg"></i> </button>`;
                            }
                        } else {
                            document.getElementById('noresultsjobs1').innerHTML = `
                                <div class="alert alert-danger text-center" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron puestos con el termino <b>${searchpuestokey.value}</b>
                                </div>
                            `;
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
    /* EJECUCION DE LA BUSQUEDA EN TIEMPO REAL */
    searchpuestokey.addEventListener('keyup', fsearchkeyuppuesto);
    /* GUARDA LA EDICION DE LOS PUESTOS */
    btnEdiPuesto.addEventListener('click', () => {
        const arrInputs = [edicodpuesto, edipuesto, edidescpuesto, ediproffamily, ediclasifpuesto, edicolect, edinivjerarpuesto, ediperfmanager, editabpuesto];
        let validate = 0;
        for (let i = 0; i < arrInputs.length; i++) {
            if (arrInputs[i].hasAttribute('tp-select')) {
                if (arrInputs[i].value == "0") {
                    const attrselect = arrInputs[i].getAttribute('tp-select');
                    fshowtypealert('Atención', 'Selecciona una opción para el campo ' + String(attrselect), 'warning', arrInputs[i], 0);
                    validate = 1;
                    break;
                }
            } else {
                if (arrInputs[i].value == "") {
                    fshowtypealert('Atención', 'Completa el campo ' + arrInputs[i].placeholder, 'warning', arrInputs[i], 0);
                    validate = 1;
                    break;
                }
            }
        }
        if (validate == 0) {
            const dataSend = {
                edicodpuesto: edicodpuesto.value, edipuesto: edipuesto.value, edidescpuesto: edidescpuesto.value, ediproffamily: ediproffamily.value,
                ediclasifpuesto: ediclasifpuesto.value, edicolect: edicolect.value,
                edinivjerarpuesto: edinivjerarpuesto.value, ediperfmanager: ediperfmanager.value, editabpuesto: editabpuesto.value, clvpuesto: clvpuesto.value
            }
            try {
                $.ajax({
                    url: "../EditDataGeneral/EditPuesto",
                    type: "POST",
                    data: dataSend,
                    success: (data) => {
                        if (data.result === "success") {
                            Swal.fire({
                                title: 'Puesto actualizado', icon: 'success',
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                $("#editpuesto").modal('hide');
                                setTimeout(() => {
                                    if (JSON.parse(localStorage.getItem('modalbtnpuesto')) != null) {
                                        $("#searchpuestobtn").modal('show');
                                        setTimeout(() => { searchpuestokey.focus(); }, 1000);
                                    }
                                }, 1000);
                            });
                        } else {
                            Swal.fire({
                                title: 'Error', text: 'Contacte a sistemas', icon: 'error',
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                fclearfieldsnewjob();
                                $("#editpuesto").modal('hide');
                                setTimeout(() => {
                                    if (JSON.parse(localStorage.getItem('modalbtnpuesto')) != null) {
                                        $("#searchpuestobtn").modal('show');
                                        setTimeout(() => { searchpuestokey.focus(); }, 1000);
                                    }
                                }, 1000);
                            });
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
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
    });
    /* FUNCION Y EJECUCIÓN DEL GUARDADO DE DATOS DE UN NUEVO PUESTO */
    btnSavePuesto.addEventListener('click', () => {
        const arrInputs = [typeregpuesto, regcodpuesto, regpuesto, regdescpuesto, proffamily, clasifpuesto, regcolect, nivjerarpuesto, perfmanager, tabpuesto];
        let validate = 0;
        for (let i = 0; i < arrInputs.length; i++) {
            if (arrInputs[i].hasAttribute('tp-select')) {
                if (arrInputs[i].value == "0") {
                    const attrselect = arrInputs[i].getAttribute('tp-select');
                    fshowtypealert('Atención', 'Selecciona una opción para el campo ' + String(attrselect), 'warning', arrInputs[i], 0);
                    validate = 1;
                    break;
                }
            } else {
                if (arrInputs[i].value == "") {
                    fshowtypealert('Atención', 'Completa el campo ' + arrInputs[i].placeholder, 'warning', arrInputs[i], 0);
                    validate = 1;
                    break;
                }
            }
        }
        if (validate == 0) {
            const valueTypeJob = typeregpuesto.value;
            const valuesArrays = valueTypeJob.split(",");
            const keyTypeJobSe = parseInt(valuesArrays[0]);
            const dataEnv = {
                typeregpuesto: keyTypeJobSe,
                regcodpuesto: regcodpuesto.value, regpuesto: regpuesto.value, regdescpuesto: regdescpuesto.value, proffamily: proffamily.value, clasifpuesto: clasifpuesto.value,
                regcolect: regcolect.value, nivjerarpuesto: nivjerarpuesto.value, perfmanager: perfmanager.value, tabpuesto: tabpuesto.value
            };  
            console.log(dataEnv);
            try {
                $.ajax({
                    url: "../SaveDataGeneral/SaveDataPuestos",
                    type: "POST",
                    data: dataEnv,
                    success: (data) => {
                        if (data.Bandera === true && data.MensajeError === "none") {
                            Swal.fire({
                                title: 'Registro correcto', text: "Codigo de puesto: " + data.Puesto, icon: 'success',
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                fclearfieldsnewjob();
                                $("#registerpuesto").modal('hide');
                                setTimeout(() => {
                                    if (JSON.parse(localStorage.getItem('modalbtnpuesto')) != null) {
                                        $("#searchpuestobtn").modal('show');
                                        setTimeout(() => { searchpuestokey.focus(); }, 1000);
                                    } else {
                                        $("#searchpuesto").modal('show');
                                        setTimeout(() => { searchpuestokeyadd.focus(); }, 1000);
                                    }
                                }, 1000);
                            });
                        } else if (data.sMensaje === "codexists") {
                            Swal.fire({
                                title: 'Atencion', text: 'El código ingresado ' + String(regcodpuesto.value) + ' ya se encuentra registrado.' , icon: 'warning',
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                setTimeout(() => {
                                    regcodpuesto.focus();
                                }, 1000);
                            });
                        } else {
                            Swal.fire({
                                title: 'Error', text: 'Contacte a sistemas', icon: 'error',
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                fclearfieldsnewjob();
                                $("#registerpuesto").modal('hide');
                                setTimeout(() => {
                                    if (JSON.parse(localStorage.getItem('modalbtnpuesto')) != null) {
                                        $("#searchpuestobtn").modal('show');
                                        setTimeout(() => { searchpuestokey.focus(); }, 1000);
                                    } else {
                                        $("#searchpuesto").modal('show');
                                        setTimeout(() => { searchpuestokeyadd.focus(); }, 1000);
                                    }
                                }, 1000);
                            });
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
    });
    /* FUNCION QUE CARGA LOS DATOS DEL PUESTO SELECCIONADO EN EL FORMULARIO DE REGISTRO DE UNA NUEVA POSICION */
    fselectpuestopos = (paramid, paramstr) => {
        try {
            searchpuestokeyadd.value   = '';
            resultpuestosadd.innerHTML = '';
            $("#searchpuesto").modal('hide');
            $("#registerposition").modal('show');
            puesid.value    = paramid;
            pueusureg.value = paramstr;
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
    /* FUNCION QUE REALIZA LA BUSQUEDA EN TIEMPO REAL DE PUESTOS AL MOMENTO DE REGISTRAR UNA NUEVA POSICION */
    fsearchkeyuppuestoadd = () => {
        try {
            resultpuestosadd.innerHTML = '';
            document.getElementById('noresultsjobs2').innerHTML = '';
            if (searchpuestokeyadd.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchPuesto",
                    type: "POST",
                    data: { wordsearch: searchpuestokeyadd.value },
                    success: (data) => {
                        resultpuestosadd.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                resultpuestosadd.innerHTML += `<button  onclick="fselectpuestopos(${data[i].iIdPuesto},'${data[i].sNombrePuesto}')" class="animated fadeIn list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back">${number} - ${data[i].sCodigoPuesto} - ${data[i].sNombrePuesto} <i class="fas fa-check-circle ml-2 col-ico fa-lg"></i> </button>`;
                            }
                        } else {
                            document.getElementById('noresultsjobs2').innerHTML = `
                                <div class="alert alert-danger text-center" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron puestos con el termino <b>${searchpuestokeyadd.value}</b>
                                </div>
                            `;
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                resultpuestosadd.innerHTML = '';
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
    /* EJECUCION DE FUNCION QUE REALIZA UNA BUSQUEDA EN TIEMPO REAL DE PUESTOS */
    searchpuestokeyadd.addEventListener('keyup', fsearchkeyuppuestoadd);

    /* FUNCION QUE ASIGNA EL CODIGO DE PUESTO AL CAMPO REGCODPUESTO */
    typeregpuesto.addEventListener('change', () => {
        try {
            const valueTypJob  = typeregpuesto.value;
            const valuesArray  = valueTypJob.split(",");
            const keyJobSelec  = parseInt(valuesArray[0]);
            const codJobSelec  = valuesArray[1];
            regcodpuesto.value = codJobSelec;
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
    });
});