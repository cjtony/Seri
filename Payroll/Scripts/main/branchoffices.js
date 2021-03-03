$(function () {

    const btndownloadreportcenterscost = document.getElementById('btndownloadreportcenterscost');

    // Funcion que realiza la descarga del reporte de posiciones \\
    fGenerateReportCentersCost = () => {
        $("#modalDownloadCatalogs").modal("show");
        try {
            $.ajax({
                url: "../Reportes/GenerateReportCatalogs",
                type: "POST",
                data: { key: "CENTROSCOSTO" },
                beforeSend: () => {
                    $("#btnsearchcentrscost").modal("hide");
                    document.getElementById('contenDownloadReport').innerHTML = `
                        <div class="spinner-border text-primary" role="status">
                          <span class="sr-only">Loading...</span>
                        </div>
                    `;
                }, success: (request) => {
                    console.log(request);
                    document.getElementById('nameReport').textContent = "CENTROS de COSTO";
                    if (request.Bandera == true) {
                        document.getElementById('contenDownloadReport').innerHTML = `
                            <a class='btn btn-primary btn-sm' download='${request.Archivo}' href='/Content/REPORTES/${request.Folder}/${request.Archivo}'> <i class='fas fa-file-download mr-2'></i> Descargar </a>
                        `;
                        document.getElementById('icoCloseDownloadReport').setAttribute("onclick", `fRestoreReportCC('${request.Archivo}', '${request.Folder}');`);
                        document.getElementById('btnCloseDownloadReport').setAttribute("onclick", `fRestoreReportCC('${request.Archivo}', '${request.Folder}');`);
                    } else {
                        document.getElementById('contenDownloadReport').innerHTML = `
                            <div class="alert alert-danger" role="alert">
                              Ocurrio un error al generar el reporte
                            </div>
                        `;
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

    btndownloadreportcenterscost.addEventListener('click', fGenerateReportCentersCost);

    fRestoreReportCC = (archivo, folder) => {
        try {
            if (archivo != "" && folder != "") {
                const dataSend = { file: archivo, folder: folder };
                $.ajax({
                    url: "../Reportes/DeleteReportCatalogs",
                    type: "POST",
                    data: dataSend,
                    success: () => {
                        $("#btnsearchcentrscost").modal("show");
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
            } else if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }

    localStorage.removeItem('modalbtnsucursal');
    /* FUNCION QUE CARGA LOS DATOS DE LA SUCURSAL SELECCIONADA POR EL USUARIO */
    fselectoffice = (param) => {
        console.log(param);
        try {
            $.ajax({
                url: "../SearchDataCat/DataSelectOffices",
                type: "POST",
                data: { clvsucursal: param },
                success: (data) => {
                    $("#searchsucursales").modal('hide');
                    $("#editsucursal").modal('show');
                    console.log(data);
                    clvsucursal.value = data.iIdSucursal;
                    descsucursaledit.value = data.sDescripcionSucursal;
                    clasucursaledit.value = data.sClaveSucursal;
                    setTimeout(() => { descsucursaledit.focus(); }, 1000);
                    searchofficekey.value = ''; resultoffices.innerHTML = '';
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
    /* CONSTANTES BUSQUEDA DE SUCURSALES */
    const searchofficekey  = document.getElementById('searchofficekey');
    const resultoffices    = document.getElementById('resultoffices');
    const clvsucursal      = document.getElementById('clvsucursal');
    const descsucursaledit = document.getElementById('descsucursaledit');
    const clasucursaledit  = document.getElementById('clasucursaledit');
    const descsucursal     = document.getElementById('descsucursal');
    const clasucursal      = document.getElementById('clasucursal');
    const btnsavesucursal  = document.getElementById('btnsavesucursal');
    const btnsavesucursaledit = document.getElementById('btnsavesucursaledit');
    const btnmodalsearchsucursales = document.getElementById('btn-modal-search-sucursales');
    const btnregistersucursalbtn   = document.getElementById('btnregistersucursalbtn');
    /* CONSTANTES BOTONES DE CIERRE DE MODAL */
    const btnCloseSearchOffices = document.getElementById('btn-close-search-offices');
    const icoCloseSearchOffices = document.getElementById('ico-close-search-offices');
    /* CONSTANTES BOTONES CIERRE MODAL AL REGISTRAR UNA NUEVA */
    const btnclearfieldssucursal = document.getElementById('btn-clear-fields-sucursal');
    const icoclearfieldssucursal = document.getElementById('ico-clear-fields-sucursal');
    /* LIMPIA LA BUSQUEDA AL CERRAR LA MDOAL */
    btnCloseSearchOffices.addEventListener('click', () => { searchofficekey.value = ''; resultoffices.innerHTML = ''; localStorage.removeItem('modalbtnsucursal'); document.getElementById('noresultoffices1').innerHTML = ''; });
    icoCloseSearchOffices.addEventListener('click', () => { searchofficekey.value = ''; resultoffices.innerHTML = ''; localStorage.removeItem('modalbtnsucursal'); document.getElementById('noresultoffices1').innerHTML = ''; });
    /* FUNCION QUE LIMPIA LOS CAMPOS AL MOMENTO DE REGISTRAR UNA SUCURSAL */
    fclearfieldssucursal = () => {
        descsucursal.value = '';
        clasucursal.value = '';
        $("#searchsucursales").modal('show');
        setTimeout(() => { searchofficekey.focus(); }, 1000);
    }

    searchofficekey.style.transition = "1s";
    searchofficekey.style.cursor     = "pointer";
    searchofficekey.addEventListener('mouseover',  () => { searchofficekey.classList.add('shadow'); });
    searchofficekey.addEventListener('mouseleave', () => { searchofficekey.classList.remove('shadow'); });

    /* EJECUCION DE FUNCION QUE LIMPIA LOS CAMPOS DE REGISTRO DE UNA SUCURSAL AL CERRAR LA VENTANA MODAL */
    btnclearfieldssucursal.addEventListener('click', fclearfieldssucursal);
    icoclearfieldssucursal.addEventListener('click', fclearfieldssucursal);
    localStorage.removeItem('modalbtnsucursal');
    /* EJECUCION DE EVENTO QUE ACTIVA EL CAMPO DE BUSQUEDA DE UNA SUCURSAL */
    btnmodalsearchsucursales.addEventListener('click', () => {
        localStorage.setItem('modalbtnsucursal', 1);
        setTimeout(() => { searchofficekey.focus(); }, 1000);
    })
    /* EJECUCION DE EVENTO QUE OCULTA LA VENTANA MODAL DE BUSQUEDA DE SUCURSALES */
    btnregistersucursalbtn.addEventListener('click', () => {
        $("#searchsucursales").modal('hide');
        searchofficekey.value = '';
        resultoffices.innerHTML = '';
        document.getElementById('noresultoffices1').innerHTML = '';
        setTimeout(() => { descsucursal.focus(); }, 1000);
    });
    /* FUNCION QUE HACE LA BUSQUEDA EN TIEMPO REAL */
    fsearchkeyupoffices = () => {
        resultoffices.innerHTML = '';
        document.getElementById('noresultoffices1').innerHTML = '';
        try {
            if (searchofficekey.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchOffices",
                    type: "POST",
                    data: { wordsearch: searchofficekey.value },
                    success: (data) => {
                        resultoffices.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                console.log(data[i]);
                                resultoffices.innerHTML += `<button title="Editar" onclick="fselectoffice(${data[i].iIdSucursal})" class="animated fadeIn border-left-primary list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded">${number}. - ${data[i].sClaveSucursal} - ${data[i].sDescripcionSucursal} <i class="fas fa-edit ml-2 text-warning fa-lg"></i> </button>`;
                            }
                        } else {
                            document.getElementById('noresultoffices1').innerHTML = `
                                <div class="alert alert-danger text-center" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron sucursales con el termino <b>${searchofficekey.value}</b>
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
    searchofficekey.addEventListener('keyup', fsearchkeyupoffices);
    /* GUARDA EL REGISTRO DE UNA NUEVA SUCURSAL */
    btnsavesucursal.addEventListener('click', () => {
        try {
            const arrInput = [descsucursal, clasucursal];
            let validate = 0;
            for (let i = 0; i < arrInput.length; i++) {
                if (arrInput[i].value == "") {
                    fshowtypealert('Atención', 'Completa el campo ' + arrInput[i].placeholder, 'warning', arrInput[i], 0);
                    validate = 1;
                    break;
                }
            }
            if (validate == 0) {
                const dataSend = { desc: descsucursal.value, clav: clasucursal.value };
                $.ajax({
                    url: "../CatalogsTables/SaveSucursal",
                    type: "POST",
                    data: dataSend,
                    success: (data) => {
                        if (data.sMensaje === "success") {
                            Swal.fire({
                                title: 'Sucursal registrada', icon: 'success',
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                $("#registersucursal").modal('hide');
                                fclearfieldssucursal();
                                setTimeout(() => {
                                    if (JSON.parse(localStorage.getItem('modalbtnsucursal')) != null) {
                                        $("#searchsucursales").modal('show');
                                        setTimeout(() => { searchofficekey.focus(); }, 1000);
                                    }
                                }, 1000);
                            });
                        } else if (data.sMensaje === "existe") {
                            Swal.fire({
                                title: 'La sucursal con clave ' + String(clasucursal.value) + " ya se encuentra registrada", icon: 'warning',
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                setTimeout(() => {
                                    clasucursal.focus();
                                }, 1000);
                            });
                        } else if (data.sMensaje === "error") {
                            Swal.fire({
                                title: 'Error', text: 'Contacte a sistemas', icon: 'error',
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                $("#registersucursal").modal('hide');
                                fclearfieldssucursal();
                                setTimeout(() => {
                                    if (JSON.parse(localStorage.getItem('modalbtnsucursal')) != null) {
                                        $("#searchsucursales").modal('show');
                                        setTimeout(() => { searchofficekey.focus(); }, 1000);
                                    }
                                }, 1000);
                            });
                        } else {
                            console.log(data.sMensaje);
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
    });
    /* GUARDA LA EDICION DE LA SUCURSAL */
    btnsavesucursaledit.addEventListener('click', () => {
        try {
            const arrInput = [descsucursaledit, clasucursaledit];
            let validate = 0;
            for (let i = 0; i < arrInput.length; i++) {
                if (arrInput[i].value == "") {
                    fshowtypealert('Atención', 'Completa el campo ' + arrInput[i].placeholder, 'warning', arrInput[i], 0);
                    validate = 1;
                    break;
                }
            }
            if (validate == 0) {
                const dataSend = { descsucursaledit: descsucursaledit.value, clasucursaledit: clasucursaledit.value, clvsucursal: clvsucursal.value };
                $.ajax({
                    url: "../EditDataGeneral/EditSucursales",
                    type: "POST",
                    data: dataSend,
                    success: (data) => {
                        if (data.result === "success") {
                            Swal.fire({
                                title: 'Sucursal editada', icon: 'success',
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                $("#editsucursal").modal('hide');
                                fclearfieldssucursal();
                                setTimeout(() => {
                                    if (JSON.parse(localStorage.getItem('modalbtnsucursal')) != null) {
                                        $("#searchsucursales").modal('show');
                                        setTimeout(() => { searchofficekey.focus(); }, 1000);
                                    } 
                                }, 1000);
                            });
                        } else {
                            Swal.fire({
                                title: 'Error', text: 'Contacte a sistemas', icon: 'error',
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                $("#editsucursal").modal('hide');
                                fclearfieldssucursal();
                                setTimeout(() => {
                                    if (JSON.parse(localStorage.getItem('modalbtnsucursal')) != null) {
                                        $("#searchsucursales").modal('show');
                                        setTimeout(() => { searchofficekey.focus(); }, 1000);
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