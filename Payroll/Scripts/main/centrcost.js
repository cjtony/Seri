$(function () {

    /*
     * Constantes de los centros de costos  
     */

    const keysearchcentrcosts    = document.getElementById('keysearchcentrcosts');
    const listresultcentrcosts   = document.getElementById('listresultcentrcosts');
    const noresultscentrcostbtn1 = document.getElementById('noresultscentrcostbtn1');
    const btnSearchModalCentCost = document.getElementById('btn-modal-search-centr-cost');
    const icoCloseSearchCCostBtn = document.getElementById('ico-close-search-centrcost-btn');
    const btnCloseSearchCCostBtn = document.getElementById('btn-close-search-centrcost-btn');
    const btnsavecentrcostedit   = document.getElementById('btnsavecentrcostedit');
    const icoClearFieldsCCoSEdit = document.getElementById('ico-clear-fields-centrcost-edit');
    const btnClearFieldsCCosEdit = document.getElementById('btn-clear-fields-centrcost-edit');

    const btnregistercentrcost   = document.getElementById('btnregistercentrcost');
    const centrcostsavebtn       = document.getElementById('centrcostsavebtn');
    const desccentrcostsavebtn   = document.getElementById('desccentrcostsavebtn');
    const btnsavecentrcost       = document.getElementById('btnsavecentrcost');
    const btnClearFCentrCostSave = document.getElementById('btn-clear-fields-centrcost-save');
    const icoClearFCentrCostSave = document.getElementById('ico-clear-fields-centrcost-save');

    /*
     * Funciones
     */

    /* *- FUNCIONES DE BUSQUEDA Y EDICION -* */


    /* FUNCION QUE LIMPIA LA CAJA DE BUSQUEDA Y RESULTADOS DE LOS CENTROS DE COSTOS */
    fClearFieldsSearchCentrCost = () => {
        keysearchcentrcosts.value = '';
        listresultcentrcosts.innerHTML = '';
        noresultscentrcostbtn1.innerHTML = '';
    }

    /* FUNCION QUE LIMPIA LOS CAMPOS DE EDICION DE CENTROS DE COSTOS */
    fClearFieldsEditCentrCost = () => {
        document.getElementById('clvcentrcost').value     = '';
        document.getElementById('centrcosteditbtn').value = '';
        document.getElementById('desccentrcostbtn').value = '';
    }

    /* FUNCION QUE REALIZA LA BUSQUEDA EN TIEMPO REAL DE LOS CENTROS DE COSTOS */
    fsearchcentrcostosbtn = () => {
        listresultcentrcosts.innerHTML = '';
        try {
            if (keysearchcentrcosts.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchCentrCost",
                    type: "POST",
                    data: { wordsearch: keysearchcentrcosts.value },
                    success: (data) => {
                        listresultcentrcosts.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                listresultcentrcosts.innerHTML += `<button class="list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back">${number}. - ${data[i].sCentroCosto} <i class="fas fa-edit ml-2 text-warning fa-lg" onclick="fselecteditcentrcost(${data[i].iIdCentroCosto},'${data[i].sCentroCosto}')"></i> </button>`;
                            }
                        } else {
                            noresultscentrcostbtn1.innerHTML = `
                                <div class="alert alert-danger text-center" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron centros de costo con el termino <b>${keysearchcentrcosts.value}</b>
                                </div>
                            `;
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                resultcentrcosts.innerHTML = '';
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

    /* FUNCION QUE CARGA LA INFORMACION DEL CENTRO DE COSTO PARA QUE PUEDA SER EDITADO */
    fselecteditcentrcost = async (paramid, paramstr) => {
        $("#btnsearchcentrscost").modal('hide');
        keysearchcentrcosts.value        = '';
        listresultcentrcosts.innerHTML   = '';
        noresultscentrcostbtn1.innerHTML = '';
        try {
            if (paramid != 0) {
                await $.ajax({
                    url: "../SearchDataCat/DataSelectCentrCost",
                    type: "POST",
                    data: { keycentrcost: parseInt(paramid) },
                    beforeSend: () => {
                        console.log('Cargando datos');
                    }, success: (data) => {
                        if (data.Bandera === true && data.MensajeError === "none") {
                            $("#editcentrcost").modal('show');
                            document.getElementById('clvcentrcost').value     = data.Datos.iIdCentroCosto;
                            document.getElementById('centrcosteditbtn').value = data.Datos.sCentroCosto;
                            document.getElementById('desccentrcostbtn').value = data.Datos.sDescripcionCentroCosto;
                            setTimeout(() => {
                                document.getElementById('centrcosteditbtn').focus();
                            }, 1000);
                        } else {
                            alert('No se cargaron los datos del centro de costo seleccionado');
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                alert('No valido');
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

    /* FUNCION QUE GUARDA LOS CAMBIOS GENERADOS EN UN CENTRO DE COSTOS */
    fsaveeditcentrcost = async () => {
        try {
            const keycentrcost = document.getElementById('clvcentrcost');
            const ncentrocosto = document.getElementById('centrcosteditbtn');
            const dcentrocosto = document.getElementById('desccentrcostbtn');
            if (keycentrcost.value != "" || keycentrcost.value > 0) {
                if (ncentrocosto.value != "") {
                    if (dcentrocosto.value != "") {
                        await $.ajax({
                            url: "../SearchDataCat/SaveEditCentrCost",
                            type: "POST",
                            data: {
                                keycentrcost: parseInt(keycentrcost.value),
                                ncentrocosto: ncentrocosto.value,
                                dcentrocosto: dcentrocosto.value
                            },
                            beforeSend: () => {
                                console.log('Guardando datos');
                            }, success: (data) => {
                                if (data.Bandera === true && data.MensajeError === "none") {
                                    Swal.fire({
                                        title: "Correcto", text: "Datos actualizados", icon: "success",
                                        showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                        confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                    }).then((acepta) => {
                                        $("#editcentrcost").modal('hide');
                                        setTimeout(() => {
                                            $("#btnsearchcentrscost").modal('show');
                                        }, 1000);
                                        setTimeout(() => {
                                            keysearchcentrcosts.focus();
                                        }, 1500);
                                    });
                                } else {
                                    Swal.fire({
                                        title: "Error", text: "Ocurrio un problema al actualizar", icon: "error",
                                        showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                        confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                    }).then((acepta) => {
                                        location.reload();
                                    });
                                }
                            }, error: (jqXHR, exception) => {
                                fcaptureaerrorsajax(jqXHR, exception);
                            }
                        });
                    } else {
                        Swal.fire({
                            title: "Atencion", text: "Ingrese la descripcion del centro de costo", icon: "info",
                            showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                            confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                        }).then((acepta) => {
                            $("html, body").animate({ scrollTop: $(`#${dcentrocosto.id}`).offset().top - 50 }, 1000);
                            setTimeout(() => {
                                dcentrocosto.focus();
                            }, 1000);
                        });
                    }
                } else {
                    Swal.fire({
                        title: "Atencion", text: "Ingrese el nombre del centro de costo", icon: "info",
                        showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                        confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                    }).then((acepta) => {
                        $("html, body").animate({ scrollTop: $(`#${ncentrocosto.id}`).offset().top - 50 }, 1000);
                        setTimeout(() => {
                            ncentrocosto.focus();
                        }, 1000);
                    });
                }
            } else {
                alert('No valido para guardar la informacion');
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

    /* *- FUNCIONES DE REGISTRO -* */

    /* FUNCION QUE LIMPIA LOS CAMPOS DEL REGISTRO */
    fClearFieldsSaveCentrCost = () => {
        centrcostsavebtn.value     = '';
        desccentrcostsavebtn.value = '';
    }

    /* FUNCION QUE GUARDA UN NUEVO CENTRO DE COSTO */
    fSaveDataCentrCost = () => {
        try {
            if (centrcostsavebtn.value != "") {
                if (desccentrcostsavebtn.value != "") {
                    const dataSend = {
                        ncentrcost: centrcostsavebtn.value,
                        dcentrcost: desccentrcostsavebtn.value
                    };
                    $.ajax({
                        url: "../SearchDataCat/SaveDataCentrCost",
                        type: "POST",
                        data: dataSend,
                        beforeSend: () => {
                            console.log('Guardando centro de costo');
                        }, success: (data) => {
                            console.log(data);
                            if (data.Bandera === true && data.MensajeError === "none") {
                                Swal.fire({
                                    title: "Correcto", text: "Datos registrados", icon: "success",
                                    showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                    confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                }).then((acepta) => {
                                    $("#registercentrcost").modal('hide');
                                    setTimeout(() => {
                                        $("#btnsearchcentrscost").modal('show');
                                    }, 1000);
                                    setTimeout(() => {
                                        keysearchcentrcosts.focus();
                                    }, 1500);
                                });
                            } else {
                                Swal.fire({
                                    title: "Error", text: "Ocurrio un problema al actualizar", icon: "error",
                                    showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                    confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                }).then((acepta) => {
                                    location.reload();
                                });
                            }
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception)
                        }
                    });
                } else {
                    Swal.fire({
                        title: "Atencion", text: "Ingrese la descripcion del centro de costo", icon: "info",
                        showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                        confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                    }).then((acepta) => {
                        $("html, body").animate({ scrollTop: $(`#${desccentrcostsavebtn.id}`).offset().top - 50 }, 1000);
                        setTimeout(() => {
                            desccentrcostsavebtn.focus();
                        }, 1000);
                    });
                }
            } else {
                Swal.fire({
                    title: "Atencion", text: "Ingrese el nombre del centro de costo", icon: "info",
                    showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                    confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                }).then((acepta) => {
                    $("html, body").animate({ scrollTop: $(`#${centrcostsavebtn.id}`).offset().top - 50 }, 1000);
                    setTimeout(() => {
                        centrcostsavebtn.focus();
                    }, 1000);
                });
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


    /*
     * Ejecucion de funciones
     */

    btnregistercentrcost.addEventListener('click', () => {
        $("#btnsearchcentrscost").modal('hide');
        setTimeout(() => {
            centrcostsavebtn.focus();
        }, 1000); 
    });

    btnSearchModalCentCost.addEventListener('click', () => {
        setTimeout(() => {
            keysearchcentrcosts.focus();
        }, 1000);
    });

    keysearchcentrcosts.addEventListener('keyup', fsearchcentrcostosbtn);

    btnCloseSearchCCostBtn.addEventListener('click', fClearFieldsSearchCentrCost);
    icoCloseSearchCCostBtn.addEventListener('click', fClearFieldsSearchCentrCost);

    btnClearFieldsCCosEdit.addEventListener('click', fClearFieldsEditCentrCost);
    icoClearFieldsCCoSEdit.addEventListener('click', fClearFieldsEditCentrCost);

    btnsavecentrcostedit.addEventListener('click', fsaveeditcentrcost);

    btnClearFCentrCostSave.addEventListener('click', fClearFieldsSaveCentrCost);
    icoClearFCentrCostSave.addEventListener('click', fClearFieldsSaveCentrCost);

    btnsavecentrcost.addEventListener('click', fSaveDataCentrCost);

});