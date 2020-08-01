$(function () {

    /* FUNCION QUE CARGA LOS DATOS DE LA POSICION SELECCIONADA POR EL USUARIO */
    fselectregional = (param) => {
        try {
            $.ajax({
                url: "../SearchDataCat/DataSelectRegional",
                type: "POST",
                data: { clvregional: param },
                success: (data) => {
                    $("#editregion").modal('show');
                    $("#searchregion").modal('hide');
                    clvregion.value = data.iIdRegional;
                    descregionedit.value = data.sDescripcionRegional;
                    claregionedit.value = data.sClaveRegional;
                    setTimeout(() => { descregionedit.focus(); }, 1000);
                    searchregionalkey.value = '';
                    resultregionales.innerHTML = ''; 
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
    const btnregisterregionbtn = document.getElementById('btnregisterregionbtn');
    /* CONSTANTES BUSQUEDA DE REGIONALES */
    const searchregionalkey = document.getElementById('searchregionalkey');
    const resultregionales = document.getElementById('resultregionales');

    searchregionalkey.style.transition = "1s";
    searchregionalkey.style.cursor = "pointer";
    searchregionalkey.addEventListener('mouseover',  () => { searchregionalkey.classList.add('shadow'); });
    searchregionalkey.addEventListener('mouseleave', () => { searchregionalkey.classList.remove('shadow'); });

    /* CONSTANTES DE EDICION DE REGIONALES */
    const clvregion = document.getElementById('clvregion');
    const descregionedit = document.getElementById('descregionedit');
    const claregionedit = document.getElementById('claregionedit');
    const btnsaveergionedit = document.getElementById('btnsaveergionedit');
    /* CONSTANTES DE BOTONES PERTENECIENTES A REGIONALES */
    const btnModalSearchRegion = document.getElementById('btn-modal-search-regionales');
    /* CONSTANTES DEL REGISTRO DE REGIONALES */
    const descregion = document.getElementById('descregion');
    const claregion = document.getElementById('claregion');
    const btnsaveergion = document.getElementById('btnsaveergion');
    /* CONSTANTES DE LIMPIA DE FORMULARIO DE REGIONALES AL REGISTRAR */
    const btnclearfieldsregiones = document.getElementById('btn-clear-fields-regiones');
    const icoClearFieldsregiones = document.getElementById('ico-clear-fields-region');
    /* CONSTANTES BOTONES DE CIERRE DE MODAL */
    const btnCloseSearchRegionales = document.getElementById('btn-close-search-regionales');
    const icoCloseSearchRegionales = document.getElementById('ico-close-search-regionales');
    /* EJECUCION DE EVENTOS QUE CREAN Y ELIMINAN EL LOCALSTORAGE DE REGIONALES Y LIMPIA LA BUSQUEDA PREVIA */
    localStorage.removeItem('modalregionalesbtn');
    btnModalSearchRegion.addEventListener('click', () => {
        localStorage.setItem('modalregionalesbtn', 1);
        setTimeout(() => { searchregionalkey.focus(); }, 1000);
    });
    btnCloseSearchRegionales.addEventListener('click', () => { localStorage.removeItem('modalregionalesbtn'); searchregionalkey.value = ''; resultregionales.innerHTML = ''; document.getElementById('noresultsregionales1').innerHTML = ''; });
    icoCloseSearchRegionales.addEventListener('click', () => { localStorage.removeItem('modalregionalesbtn'); searchregionalkey.value = ''; resultregionales.innerHTML = ''; document.getElementById('noresultsregionales1').innerHTML = ''; });
    /* FUNCION QUE LIMPIA LOS CAMPOS DEL FORMULARIO DE REGISTRO DE REGIONALES */
    fclearfieldsregionales = () => {
        descregion.value = '';
        claregion.value  = '';
        if (localStorage.getItem('modalregionalesbtn') != null) {
            $("#searchregion").modal('show');
        }
        setTimeout(() => { searchregionalkey.focus(); }, 1000);
    }
    /* EJECUCION DE LIMPIEZA DE FORMULARIO REGIONALES AL CERRAR EL FORMULARIO */
    btnclearfieldsregiones.addEventListener('click', fclearfieldsregionales);
    icoClearFieldsregiones.addEventListener('click', fclearfieldsregionales);
    /* FUNCION QUE ACTIVA EL PRIMER INPUT AL SELECCIONAR UN NUEVO REGISTRO DE REGIONAL */
    btnregisterregionbtn.addEventListener('click', () => {
        $("#searchregion").modal('hide');
        searchregionalkey.value    = '';
        resultregionales.innerHTML = '';
        document.getElementById('noresultsregionales1').innerHTML = '';
        setTimeout(() => {
            descregion.focus();
        }, 1000);
    });
    /* FUNCION QUE HACE LA BUSQUEDA EN TIEMPO REAL */
    fsearchkeyupregionales = () => {
        resultregionales.innerHTML = '';
        document.getElementById('noresultsregionales1').innerHTML = '';
        try {
            if (searchregionalkey.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchRegionales",
                    type: "POST",
                    data: { wordsearch: searchregionalkey.value },
                    success: (data) => {
                        resultregionales.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                resultregionales.innerHTML += `<button title="Editar" onclick="fselectregional(${data[i].iIdRegional})" class="list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back animated fadeIn border-left-primary">${number}. - ${data[i].sClaveRegional} - ${data[i].sDescripcionRegional} <i class="fas fa-edit ml-2 text-warning fa-lg"></i> </button>`;
                            }
                        } else {
                            document.getElementById('noresultsregionales1').innerHTML = `
                                <div class="alert alert-danger text-center" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron regionales con el termino <b>${searchregionalkey.value}</b>
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
    searchregionalkey.addEventListener('keyup', fsearchkeyupregionales);
    /* GUARDA LA EDICION DE LA REGIONAL */
    btnsaveergionedit.addEventListener('click', () => {
        try {
            const arrInput = [descregionedit, claregionedit];
            let validate = 0;
            for (let i = 0; i < arrInput.length; i++) {
                if (arrInput[i].value == "") {
                    fshowtypealert('Atención', 'Completa el campo ' + arrInput[i].placeholder, 'warning', arrInput[i], 0);
                    validate = 1;
                    break;
                }
            }
            if (validate == 0) {
                const dataSend = { descregionedit: descregionedit.value, claregionedit: claregionedit.value, clvregion: clvregion.value };
                $.ajax({
                    url: "../EditDataGeneral/EditRegionales",
                    type: "POST",
                    data: dataSend,
                    success: (data) => {
                        if (data.result === "success") {
                            Swal.fire({
                                title: 'Region editada', icon: 'success',
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                $("#editregion").modal('hide');
                                fclearfieldsregionales();
                                setTimeout(() => {
                                    if (JSON.parse(localStorage.getItem('modalregionalesbtn')) != null) {
                                        $("#searchregion").modal('show');
                                        setTimeout(() => { searchregionalkey.focus(); }, 1000);
                                    }
                                    //else {
                                    //    $("#searchpositionstab").modal('show');
                                    //}
                                }, 1000);
                            });
                        } else {
                            Swal.fire({
                                title: 'Error', text: 'Contacte a sistemas', icon: 'error',
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                $("#editregion").modal('hide');
                                fclearfieldsregionales();
                                setTimeout(() => {
                                    if (JSON.parse(localStorage.getItem('modalregionalesbtn')) != null) {
                                        $("#searchregion").modal('show');
                                        setTimeout(() => { searchregionalkey.focus(); }, 1000);
                                    }
                                    //else {
                                    //    $("#searchpositionstab").modal('show');
                                    //}
                                }, 1000);
                            });
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            }
        } catch (error) {
            if (error instanceof RangeError) {
                console.log('RangeErro ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    });
    /* EJECUCION DEL GUARDADO DE LOS DATOS DE UNA NUEVA REGIONAL */
    btnsaveergion.addEventListener('click', () => {
        try {
            const arrInput = [descregion, claregion];
            let validate = 0;
            for (let i = 0; i < arrInput.length; i++) {
                if (arrInput[i].value == "") {
                    fshowtypealert('Atención', 'Completa el campo ' + arrInput[i].placeholder, 'warning', arrInput[i], 0);
                    validate = 1;
                    break;
                }
            }
            if (validate == 0) {
                const dataSend = { descregion: descregion.value, claregion: claregion.value };
                $.ajax({
                    url: "../SaveDataGeneral/SaveRegionales",
                    type: "POST",
                    data: dataSend,
                    success: (data) => {
                        if (data.result === "success") {
                            Swal.fire({
                                title: 'Region registrada', icon: 'success',
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                $("#registerregion").modal('hide');
                                fclearfieldsregionales();
                                setTimeout(() => {
                                    if (JSON.parse(localStorage.getItem('modalregionalesbtn')) != null) {
                                        $("#searchregion").modal('show');
                                        setTimeout(() => { searchregionalkey.focus(); }, 1000);
                                    } else {
                                        $("#searchsucursalestab").modal('show');
                                    }
                                }, 1000);
                            });
                        } else {
                            Swal.fire({
                                title: 'Error', text: 'Contacte a sistemas', icon: 'error',
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                $("#registerregion").modal('hide');
                                fclearfieldsregionales();
                                setTimeout(() => {
                                    if (JSON.parse(localStorage.getItem('modalregionalesbtn')) != null) {
                                        $("#searchregion").modal('show');
                                        setTimeout(() => { searchregionalkey.focus(); }, 1000);
                                    } else {
                                        $("#searchsucursalestab").modal('show');
                                    }
                                }, 1000);
                            });
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            }
        } catch (error) {
            if (error instanceof RangeError) {
                console.log('RangeErro ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    });

});