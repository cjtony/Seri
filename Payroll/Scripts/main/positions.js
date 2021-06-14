$(function () {

    const btnSearchPositions1 = document.getElementById('btnSearchPositions1');

    fselectfilterdsearchpositionsGN = (nameInpt, searchKey, labelPs, resultContent) => {
        const filtered = $("input:radio[name=" + nameInpt + "]:checked").val();
        if (filtered == "codigo") {
            document.getElementById(searchKey).placeholder = "CODIGO DE LA POSICION";
            document.getElementById(searchKey).type = "number";
            document.getElementById(labelPs).textContent = "Código";
        } else if (filtered == "puesto") {
            document.getElementById(searchKey).placeholder = "NOMBRE DEL PUESTO";
            document.getElementById(searchKey).type = "text";
            document.getElementById(labelPs).textContent = "Puesto";
        }
        document.getElementById(searchKey).value = "";
        document.getElementById(resultContent).innerHTML = "";
        setTimeout(() => { document.getElementById(searchKey).focus() }, 500);
    }

    //const labelsearchposition1 = document.getElementById('labelsearchposition1');
    //const filtrocode1   = document.getElementById('filtrocode1');
    //const filtropuesto1 = document.getElementById('filtropuesto1');

    //filtrocode1.style.cursor   = "pointer";
    //filtropuesto1.style.cursor = "pointer";
    //document.getElementById('labelfiltrocode1').style.cursor = "pointer";
    //document.getElementById('labelfiltropost1').style.cursor = "pointer";

    //filtrocode1.addEventListener('click', () => {
    //    fselectfilterdsearchpositionsGN('filtrosposition1', 'searchpositionkeyadd', 'labelsearchposition1', 'resultpositionsadd');
    //});
    //filtropuesto1.addEventListener('click', () => {
    //    fselectfilterdsearchpositionsGN('filtrosposition1', 'searchpositionkeyadd', 'labelsearchposition1', 'resultpositionsadd');
    //});

    const labelsearchposition = document.getElementById('labelsearchposition');
    const filtrocode = document.getElementById('filtrocode');
    const filtropuesto = document.getElementById('filtropuesto');

    filtrocode.style.cursor = "pointer";
    filtropuesto.style.cursor = "pointer";
    document.getElementById('labelfiltrocode').style.cursor = "pointer";
    document.getElementById('labelfiltropost').style.cursor = "pointer";

    /* FUNCION QUE COMPRUEBA QUE TIPO DE FILTRO SE APLICARA A LA BUSQUEDA DE POSICIONES */


    /* EJECUCION DE FUNCION QUE APLICA FILTRO A LA BUSQUEDA DE Las posiciones */
    filtrocode.addEventListener('click', () => {
        fselectfilterdsearchpositionsGN('filtrosposition', 'searchpositionkeybtn', 'labelsearchposition', 'resultpositionsbtn');
    });
    filtropuesto.addEventListener('click', () => {
        fselectfilterdsearchpositionsGN('filtrosposition', 'searchpositionkeybtn', 'labelsearchposition', 'resultpositionsbtn');
    });

    const btndownloadreportpositions = document.getElementById('btndownloadreportpositions');

    // Funcion que realiza la descarga del reporte de posiciones \\
    fGenerateReportPositions = () => {
        $("#modalDownloadCatalogs").modal("show");
        try {
            $.ajax({
                url: "../Reportes/GenerateReportCatalogs",
                type: "POST",
                data: { key: "POSICIONES" },
                beforeSend: () => {
                    $("#searchpositions").modal("hide");
                    document.getElementById('contenDownloadReport').innerHTML = `
                        <div class="spinner-border text-primary" role="status">
                          <span class="sr-only">Loading...</span>
                        </div>
                    `;
                }, success: (request) => {
                    //console.log(request);
                    document.getElementById('nameReport').textContent = "POSICIONES";
                    if (request.Bandera == true) {
                        document.getElementById('contenDownloadReport').innerHTML = `
                            <a class='btn btn-primary btn-sm' download='${request.Archivo}' href='/Content/REPORTES/${request.Folder}/${request.Archivo}'> <i class='fas fa-file-download mr-2'></i> Descargar </a>
                        `;
                        document.getElementById('icoCloseDownloadReport').setAttribute("onclick", `fRestoreReportPS('${request.Archivo}', '${request.Folder}');`);
                        document.getElementById('btnCloseDownloadReport').setAttribute("onclick", `fRestoreReportPS('${request.Archivo}', '${request.Folder}');`);
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

    btndownloadreportpositions.addEventListener('click', fGenerateReportPositions);

    fRestoreReportPS = (archivo, folder) => {
        try {
            if (archivo != "" && folder != "") {
                const dataSend = { file: archivo, folder: folder };
                $.ajax({
                    url: "../Reportes/DeleteReportCatalogs",
                    type: "POST",
                    data: dataSend,
                    success: () => {
                        $("#searchpositions").modal("show");
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

    /* FUNCION QUE CARGA LOS DATOS DE LA POSICION SELECCIONADA POR EL USUARIO */
    fselectposition = (param) => {
        //console.log(param);
        try {
            searchpositionkey.value = '';
            resultpositions.innerHTML = '';
            $.ajax({
                url: "../SearchDataCat/DataSelectPosition",
                type: "POST",
                data: { clvposition: param },
                success: (data) => {
                    if (data.iIdPosicion != 0) {
                        document.getElementById('depart').value = data.sNombreDepartamento;
                        document.getElementById('pueusu').value = data.sNombrePuesto;
                        document.getElementById('numpla').value = data.sPosicionCodigo;
                        document.getElementById('clvstr').value = data.iIdPosicion;
                        document.getElementById('emprep').value = data.sRegistroPat;
                        document.getElementById('localty').value = data.sLocalidad;
                        document.getElementById('report').value = data.iIdReportaAPosicion;
                        //document.getElementById('msjfech').classList.remove('d-none');
                        $("#searchpositionstab").modal('hide');
                    } else {
                        Swal.fire({
                            title: "Atencion", text: "No se encontro resultado con tu busqueda", icon: "warning",
                            allowEnterKey: false, allowEscapeKey: false, allowOutsideClick: false
                        }).then((acepta) => {
                            setTimeout(() => { searchpositionkey.focus(); }, 1000);
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
    }
    /* CONSTANTES REGISTRO DE POSICIONES */
    const codposic = document.getElementById('codposic');
    const regpatcla = document.getElementById('regpatcla');
    regpatcla.disabled = true;
    const departreg = document.getElementById('departreg');
    const pueusureg = document.getElementById('pueusureg');
    const depaid = document.getElementById('depaid');
    const puesid = document.getElementById('puesid');
    const fechefectpos = document.getElementById('fechefectpos');
    const localityr = document.getElementById('localityr');
    const localityrtxt = document.getElementById('localityrtxt');
    const emprepreg = document.getElementById('emprepreg');
    const emprepregtxt = document.getElementById('emprepregtxt');
    const report = document.getElementById('report');
    const reportempr = document.getElementById('reportempr');
    reportempr.disabled = true;
    /* CONSTANTES EDICION DE POSICIONES */
    const codtxtinf = document.getElementById('codtxtinf');
    const clvposition = document.getElementById('clvposition');
    const edicodposic = document.getElementById('edicodposic');
    const depaidedit = document.getElementById('depaidedit');
    const departedit = document.getElementById('departedit');
    const puesidedit = document.getElementById('puesidedit');
    const pueusuedit = document.getElementById('pueusuedit');
    const editatcla = document.getElementById('editatcla');
    const editlocalityr = document.getElementById('editlocalityr');
    const edilocalityrtxt = document.getElementById('edilocalityrtxt');
    const emprepedit = document.getElementById('emprepedit');
    const emprepregtxtedit = document.getElementById('emprepregtxtedit');
    const edireportempr = document.getElementById('edireportempr');
    const btnsearchdepartamentoedit = document.getElementById('btn-search-departamento-edit');
    const btnsearchpuestoedit = document.getElementById('btn-search-puesto-edit');
    const btnsearchlocalityedit = document.getElementById('btn-search-localidad-edit');
    const btnsearchpositionedit = document.getElementById('btn-search-positionadd-edit');
    const btnclearpositionsedit = document.getElementById('btn-clear-positions-edit');
    const icoclearpositionsedit = document.getElementById('ico-clear-positions-edit');
    const btnsavepositionedit = document.getElementById('btnsavepositionedit');
    /* FUNCION QUE ELIMINA EL LOCALSTORAGE MODALBTNPOSITIONS */
    fremovelocalstclear = () => {
        searchpositionkeybtn.value = '';
        resultpositionsbtn.innerHTML = '';
        $("#searchpositions").modal('show');
        setTimeout(() => { searchpositionkeybtn.focus(); }, 1000);
    }
    /* EJECUCION DE EVENTO QUE REMUEVE EL LOCALSTORAGE MODALBTNPOSITIONS */
    btnclearpositionsedit.addEventListener('click', fremovelocalstclear);
    icoclearpositionsedit.addEventListener('click', fremovelocalstclear);
    /* EJECUCION DE EVENTO QUE OCULTA LA VENTANA MODAL DE EDICION */
    //btnsearchdepartamentoedit.addEventListener('click', () => {
    //    $("#editposition").modal('hide');
    //    setTimeout(() => { searchdepartmentkeyadd.focus(); }, 1000);
    //});
    //btnsearchpuestoedit.addEventListener('click', () => {
    //    $("#editposition").modal('hide');
    //    setTimeout(() => { searchpuestokeyadd.focus(); }, 1000);
    //});
    //btnsearchlocalityedit.addEventListener('click', () => {
    //    $("#editposition").modal('hide');
    //    setTimeout(() => { searchlocalityadd.focus(); }, 1000);
    //});
    //btnsearchpositionedit.addEventListener('click', () => {
    //    $("#editposition").modal('hide');
    //    setTimeout(() => { searchpositionkeyadd.focus(); }, 1000);
    //});
    /* CONSTANTES BOTONES REGISTRO DE POSICIONES */
    const btnclearfieldspositions = document.getElementById('btn-clear-fields-positions');
    const icoclearfieldspositions = document.getElementById('ico-clear-fields-positions');
    const btnsaveposition = document.getElementById('btnsaveposition');
    /* FUNCION QUE LIMPIA LOS CAMPOS DE REGISTRO DE UNA NUEVA POSICION */
    fclearfieldsregpositions = () => {
        codposic.value = '';
        depaid.value = '';
        departreg.value = '';
        puesid.value = '';
        pueusureg.value = '';
        regpatcla.value = '0';
        localityr.value = '';
        localityrtxt.value = '';
        emprepreg.value = '';
        emprepregtxt.value = '';
        reportempr.value = '0';
    }
    /* EJECUCION DE FUNCION QUE LIMPIA LOS CAMPOS DE REGISTRO DE UNA NUEVA POSICION */
    btnclearfieldspositions.addEventListener('click', fclearfieldsregpositions);
    icoclearfieldspositions.addEventListener('click', fclearfieldsregpositions);
    /* CONSTANTES BUSQUEDA DE POSICIONES */
    const searchpositions = document.getElementById('searchpositionkey');
    const resultpositions = document.getElementById('resultpositions');
    /* CONSTANTES BOTONES DE BUSQUEDA DE POSICIONES */
    const btnregisterpositions = document.getElementById('btnregisterpositions');
    /* CONSTANTES BOTONES DE CIERRE DE MODAL */
    const btnCloseSearchPositions = document.getElementById('btn-close-search-positions');
    const icoCloseSearchPositions = document.getElementById('ico-close-search-positions');
    /* CONSTANTES BUSQUEDA DE POSICION */
    const btnsearchtableposition = document.getElementById('btn-search-table-num-posicion');
    /* CONSTANTES BUSQUEDA DE POSICION AL AÑADIR UNA NUEVA */
    const searchpositionkeyadd = document.getElementById('searchpositionkeyadd');
    const resultpositionsadd = document.getElementById('resultpositionsadd');
    const btnsearchpositionadd = document.getElementById('btn-search-positionadd');
    const icoclosesearchpositionsadd = document.getElementById('ico-close-search-positionsadd');
    const btnclosesearchpositionsadd = document.getElementById('btn-close-search-positionsadd');
    /* FUNCION QUE LIMPIA EL CAMPO DE BUSQUEDA Y LA LISTA DE RESULTADOS */
    fclearsearchresults = () => {
        searchpositionkeyadd.value = '';
        resultpositionsadd.innerHTML = '';
        document.getElementById('noresultpositions2').innerHTML = '';
        $("#searchpositionsadd").modal('hide');
        $("#registerposition").modal('show');
        setTimeout(() => { emprepreg.focus(); }, 1000);
    }
    btnclosesearchpositionsadd.addEventListener('click', fclearsearchresults);
    icoclosesearchpositionsadd.addEventListener('click', fclearsearchresults);
    /* EJECUCION DE EVENTO QUE ACTIVA LA CAJA DE BUSQUEDA DE POSICION AL AÑADIR UNA NUEVA */
    btnsearchpositionadd.addEventListener('click', () => {
        $("#registerposition").modal('hide');
        setTimeout(() => { searchpositionkeyadd.focus(); }, 1000);
    });
    /* CONSTANTES DE BUSQUEDA DE POSICIONES POR MEDIO DE BOTON */
    const btnmodalsearchpositions = document.getElementById('btn-modal-search-positions');
    const searchpositionkeybtn = document.getElementById('searchpositionkeybtn');
    const resultpositionsbtn = document.getElementById('resultpositionsbtn');
    const icoclosesearchpositionsbtn = document.getElementById('ico-close-searchpositions-btn');
    const btnclosesearchpositionsbtn = document.getElementById('btn-close-searchpositions-btn');
    const btnregisterpositionbtnadd = document.getElementById('btnregisterpositionbtnadd');
    /* EJECUCION DE EVENTO QUE ACTIVA EL CAMPO DE BUSQUEDA AL HACER CLICK EN EL BOTON DE POSICIONES */
    btnmodalsearchpositions.addEventListener('click', () => {
        localStorage.setItem('modalbtnpositions', 1);
        setTimeout(() => { searchpositionkeybtn.focus(); }, 1000);
    });
    /* FUNCION QUE LIMPIA EL CAMPO DE BUSQUEDA Y LA LISTA DE RESULTADOS DE LAS POSICIONES BTN */
    fclearsearchresultsbtn = () => {
        searchpositionkeybtn.value = '';
        resultpositionsbtn.innerHTML = '';
        document.getElementById('noresultpositions3').innerHTML = '';
    }

    searchpositionkeybtn.style.transition = "1s";
    searchpositionkeybtn.style.cursor = "pointer";
    searchpositionkeybtn.addEventListener('mouseover', () => { searchpositionkeybtn.classList.add('shadow'); });
    searchpositionkeybtn.addEventListener('mouseleave', () => { searchpositionkeybtn.classList.remove('shadow'); });

    localStorage.removeItem('modalbtnpositions');
    /* EJECUCION DE FUNCION */
    icoclosesearchpositionsbtn.addEventListener('click', () => {
        fclearsearchresultsbtn();
        localStorage.removeItem('modalbtnpositions');
    });
    btnclosesearchpositionsbtn.addEventListener('click', () => {
        fclearsearchresultsbtn();
        localStorage.removeItem('modalbtnpositions');
    });
    btnregisterpositionbtnadd.addEventListener('click', () => {
        fclearsearchresultsbtn();
        $("#searchpositions").modal('hide');
        setTimeout(() => { codposic.focus(); }, 1000);
    });
    /* CONSTANTES BOTONES DE REGISTRO POSICION */
    const btnsearchnumposition = document.getElementById('btn-search-num-position');
    const btnSearchDepartament = document.getElementById('btn-search-departamento');
    /* LIMPIA LA BUSQUEDA AL CERRAR LA MDOAL */
    btnCloseSearchPositions.addEventListener('click', () => {
        searchpositions.value = ''; resultpositions.innerHTML = '';
        document.getElementById('noresultpositions').innerHTML = '';
    });
    icoCloseSearchPositions.addEventListener('click', () => {
        searchpositions.value = ''; resultpositions.innerHTML = '';
        document.getElementById('noresultpositions').innerHTML = '';
    });
    /* EJECUCION DE FUNCION QUE OCULTA LA BUSUQUEDA DE POSICIONES */
    btnregisterpositions.addEventListener('click', () => {
        searchpositions.value = '';
        resultpositions.innerHTML = '';
        document.getElementById('noresultpositions').innerHTML = '';
        $("#searchpositionstab").modal('hide');
        setTimeout(() => { codposic.focus(); }, 1000);
    });
    /* EJECUCION DE EVENTO QUE OCULTA EL REGISTRO DE POSICIONES Y MUESTRA LOS DEPARTAMENTOS DISPONIBLES PARA ASIGNARLOS A UNA POSICION */
    btnSearchDepartament.addEventListener('click', () => { $("#registerposition").modal('hide'); });
    /* EJECUCION DE EVENTO QUE ACTIVA EL INPUT DE LA BUSQUEDA DE POSICIONES */
    btnsearchtableposition.addEventListener('click', () => { setTimeout(() => { searchpositionkey.focus(); }, 1000); });
    /* FUNCION QUE BUSCA EL NUMERO DE POSICION EN TIEMPO REAL */
    fsearchpositionsig = () => {
        try {
            $.ajax({
                url: "../SearchDataCat/SearchNumPosition",
                type: "POST",
                data: {},
                success: (data) => {
                    if (data.mesage == "success") {
                        codposic.value = data.result;
                    }
                    //console.log(data);
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
    /* EJECUCION DE FUNCION QUE BUSCA LA POSICION SIGUIENTE */
    btnsearchnumposition.addEventListener('click', fsearchpositionsig);
    /* FUNCION QUE HACE LA BUSQUEDA EN TIEMPO REAL */
    fsearchkeyuppositions = () => { 
        resultpositions.innerHTML = '';
        document.getElementById('noresultpositions').innerHTML = '';
        try {
            if (searchpositions.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchPositions",
                    type: "POST",
                    data: { wordsearch: searchpositions.value, type: 'EMPR', filter: 'puesto' },
                    success: (data) => {
                        resultpositions.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                resultpositions.innerHTML += `<button onclick="fselectposition(${data[i].iIdPosicion})" class="animated fadeIn list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back">${number}. ${data[i].sPosicionCodigo} - ${data[i].sNombrePuesto} <i class="fas fa-check-circle ml-2 col-ico fa-lg"></i> </button>`;
                            }
                        } else {
                            document.getElementById('noresultpositions').innerHTML = `
                                <div class="alert alert-danger text-center" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron posiciones con el termino <b>${searchpositions.value}</b>
                                </div>
                            `;
                        }
                        //console.log(data);
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
    searchpositions.addEventListener('keyup', fsearchkeyuppositions);
    /* FUNCION QUE CARGA LOS REGISTROS PATRONALES DE LAS EMPRESAS */
    floadregpatclases = (element) => {
        try {
            $.ajax({
                url: "../Empleados/LoadRegPatCla",
                type: "POST",
                data: {},
                success: (data) => {
                    const quantity = data.length;
                    if (quantity > 0) {
                        for (let i = 0; i < data.length; i++) {
                            element.innerHTML += `<option value="${data[i].iIdRegPat}">${data[i].sAfiliacionIMSS}</option>`;
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
    /* EJECUCION DE FUNCION QUE CARGA LOS REGISTROS PATRONALES */
    //floadregpatclases(regpatcla);
    /* FUNCION QUE CARGA LOS DATOS DE LA POSICION SELECCIONADA AL AÑADIR UNA NUEVA */
    fselectpositionadd = (paramid, paramstr, parameid) => {
        try {
            searchpositionkeyadd.value = '';
            resultpositionsadd.innerHTML = '';
            $("#searchpositionsadd").modal('hide');
            $("#registerposition").modal('show');
            emprepreg.value = paramid;
            emprepregtxt.value = paramstr;
            reportempr.value = parameid;
            //if (localStorage.getItem('modalbtnpositions') != null) {
            //    $("#editposition").modal('show');
            //    emprepedit.value       = paramid;
            //    emprepregtxtedit.value = paramstr;
            //} else {
            //    $("#registerposition").modal('show');
            //    emprepreg.value    = paramid;
            //    emprepregtxt.value = paramstr;
            //}
            setTimeout(() => { emprepregtxt.focus(); }, 1000);
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

    const btnSearchPositionKeyAdd1 = document.getElementById('btnSearchPositionKeyAdd1');

    /* FUNCION QUE REALIZA LA BUSQUEDA DE POSICIONES EN TIEMPO REAL AL MOMENTO DE AGREGAR UNA NUEVA POSICION */
    fsearchkeyuppositionsadd = () => {
        //const filtered = $("input:radio[name=filtrosposition1]:checked").val();
        const filtered = "codigo";
        try {
            resultpositionsadd.innerHTML = '';
            document.getElementById('noresultpositions2').innerHTML = '';
            if (searchpositionkeyadd.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchPositions",
                    type: "POST",
                    data: { wordsearch: searchpositionkeyadd.value, type: 'ALL', filter: filtered },
                    beforeSend: () => {
                        btnSearchPositionKeyAdd1.disabled = true;
                    }, success: (data) => {
                        btnSearchPositionKeyAdd1.disabled = false;
                        resultpositionsadd.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                resultpositionsadd.innerHTML += `<button onclick="fselectpositionadd(${data[i].iIdPosicion}, '${data[i].sNombreE}', ${data[i].iEmpresa_id})" class="animated fadeIn list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back">${number}. ${data[i].sPosicionCodigo} - ${data[i].sNombreE} <i class="fas fa-check-circle ml-2 col-ico fa-lg"></i> </button>`;
                            }
                        } else {
                            document.getElementById('noresultpositions2').innerHTML = `
                                <div class="alert alert-danger text-center" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron posiciones con el termino <b>${searchpositionkeyadd.value}</b>
                                </div>
                            `;
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                resultpositionsadd.innerHTML = '';
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
    /* EJECUCION DE FUNCION QUE REALIZA LA BUSQUEDA DE POSICIONES */
    btnSearchPositionKeyAdd1.addEventListener('click', fsearchkeyuppositionsadd);
    // Funcion que carga las empresas del sistema \\
    floadbusiness = (state, type, keyemp, elementid) => {
        try {
            $.ajax({
                url: "../CatalogsTables/Business",
                type: "POST",
                data: { state: state, type: type, keyemp: keyemp },
                success: (data) => {
                    const quantity = data.length;
                    if (quantity > 0) {
                        for (let i = 0; i < data.length; i++) {
                            elementid.innerHTML += `<option value="${data[i].iIdEmpresa}">${data[i].sNombreEmpresa}</option>`;
                        }
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
    floadbusiness(0, 'Active/Desactive', 0, reportempr);
    /* EJECUCION DE EVENTO QUE REGISTRA UNA NUEVA POSICION */
    btnsaveposition.addEventListener('click', () => {
        try {
            const arrInput = [codposic, depaid, puesid, regpatcla, localityr, emprepreg, reportempr];
            let validate = 0;
            for (let i = 0; i < arrInput.length; i++) {
                if (arrInput[i].hasAttribute('tp-select')) {
                    if (arrInput[i].value == "0") {
                        const attrselect = arrInput[i].getAttribute('tp-select');
                        fshowtypealert('Atención', 'Selecciona una opción para el campo ' + String(attrselect), 'warning', arrInput[i], 0);
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
                const dataSend = {
                    codposic: codposic.value, depaid: depaid.value, puesid: puesid.value, regpatcla: regpatcla.value,
                    localityr: localityr.value, emprepreg: emprepreg.value, reportempr: reportempr.value
                };
                //console.log(dataSend);
                $.ajax({
                    url: "../SaveDataGeneral/SavePositions",
                    type: "POST",
                    data: dataSend,
                    success: (data) => {
                        if (data.result === "success") {
                            Swal.fire({
                                title: 'Posicion registrada con codigo: ' + codposic.value, icon: 'success',
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                $("#registerposition").modal('hide');
                                setTimeout(() => {
                                    if (JSON.parse(localStorage.getItem('modalbtnpositions')) != null) {
                                        $("#searchpositionstab").modal('show');
                                        searchpositionkey.value = data.Puesto;
                                        searchpositionkey.value = data.Puesto;
                                        setTimeout(() => {
                                            searchpositionkey.focus();
                                            fsearchkeyuppositions();
                                        }, 1000);
                                    } else {
                                        $("#searchpositionstab").modal('show');
                                        searchpositionkey.value = data.Puesto;
                                        setTimeout(() => {
                                            searchpositionkey.focus();
                                            fsearchkeyuppositions();
                                        }, 1000);
                                    }
                                }, 1000);
                                setTimeout(() => {
                                    fclearfieldsregpositions();
                                }, 1500);
                            });
                        } else {
                            Swal.fire({
                                title: 'Error', text: 'Contacte a sistemas', icon: 'error',
                                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                $("#registerposition").modal('hide');
                                fclearfieldsregpositions();
                                setTimeout(() => {
                                    if (JSON.parse(localStorage.getItem('modalbtnpositions')) != null) {
                                        $("#searchpositionstab").modal('show');
                                        setTimeout(() => { searchpositionkeybtn.focus(); }, 1000);
                                    } else {
                                        $("#searchpositionstab").modal('show');
                                        setTimeout(() => { searchpositionkey.focus(); }, 1000);
                                    }
                                }, 1000);
                            });
                        }
                    }, error: (jqxHR, exception) => {
                        fcaptureaerrorsajax(jqxHR, exception);
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
    });
    /* FUNCION QUE CARGA LOS DATOS DE LA POSICION SELECCIONADA PARA SER EDITADA */
    fviewdatailspos = (paramid) => {
        //floadregpatclases(editatcla);
        floadbusiness(0, 'Active/Desactive', 0, edireportempr);
        $("#searchpositions").modal('hide');
        try {
            if (paramid != 0) {
                $.ajax({
                    url: "../SearchDataCat/DataSelectPosition",
                    type: "POST",
                    data: { clvposition: paramid },
                    success: (data) => {
                        console.log(data);
                        $("#editposition").modal('show');
                        clvposition.value = data.iIdPosicion;
                        codtxtinf.textContent = data.sPosicionCodigo;
                        edicodposic.value = data.sPosicionCodigo;
                        depaidedit.value  = data.iDepartamento_id;
                        departedit.value = data.sNombreDepartamento;
                        puesidedit.value  = data.iPuesto_id;
                        pueusuedit.value = data.sNombrePuesto;
                        editatcla.innerHTML = `<option value="${data.iIdRegistroPat}">${data.sRegistroPat}</option>`;
                        editlocalityr.value = data.iIdLocalidad;
                        edilocalityrtxt.value = data.sLocalidad;
                        //emprepedit.value = data.iIdReportaAPosicion;
                        emprepregtxtedit.value = data.sCodRepPosicion;
                        edireportempr.value = data.iIdReportaAEmpresa;
                        //setTimeout(() => { edicodposic.focus(); }, 1000);
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
    /* FUNCION QUE REALIZA LA BUSQUEDA EN TIEMPO REAL DE POSICIONES AL DAR CLICK EN EL BOTON DE POSICIONES */
    fsearchkeyuppositionsbtn = async () => {
        const filtered = $("input:radio[name=filtrosposition]:checked").val();
        try {
            resultpositionsbtn.innerHTML = '';
            document.getElementById('noresultpositions3').innerHTML = '';
            if (searchpositionkeybtn.value != "") {
                await $.ajax({
                    url: "../SearchDataCat/SearchPositionsList",
                    type: "POST",
                    data: { wordsearch: searchpositionkeybtn.value, search: filtered },
                    success: (data) => {
                        resultpositionsbtn.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                resultpositionsbtn.innerHTML += `<button onclick="fviewdatailspos(${data[i].iIdPosicion})" class="animated fadeIn border-left-primary list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back" title="Detalles">${number}. ${data[i].sPosicionCodigo} - ${data[i].sNombrePuesto} <i class="fas fa-eye ml-2 col-ico fa-lg"></i> </button>`;
                            }
                        } else {
                            document.getElementById('noresultpositions3').innerHTML = `
                                <div class="alert alert-danger text-center" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron posiciones con el termino <b>${searchpositionkeybtn.value}</b>
                                </div>
                            `;
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                resultpositionsbtn.innerHTML = '';
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
    btnSearchPositions1.addEventListener('click', fsearchkeyuppositionsbtn);
    /* EJECUCION DEL EVENTO QUE GUARDA LA EDICION DE LA POSICION */
    //btnsavepositionedit.addEventListener('click', () => {
    //    try {
    //        const arrInput = [edicodposic, depaidedit, puesidedit, editatcla, editlocalityr, emprepedit, edireportempr];
    //        let validate = 0;
    //        for (let i = 0; i < arrInput.length; i++) {
    //            if (arrInput[i].hasAttribute('tp-select')) {
    //                if (arrInput[i].value == "0") {
    //                    const attrselect = arrInput[i].getAttribute('tp-select');
    //                    fshowtypealert('Atención', 'Selecciona una opción para el campo ' + String(attrselect), 'warning', arrInput[i], 0);
    //                    validate = 1;
    //                    break;
    //                }
    //            } else {
    //                if (arrInput[i].value == "") {
    //                    fshowtypealert('Atención', 'Completa el campo ' + arrInput[i].placeholder, 'warning', arrInput[i], 0);
    //                    validate = 1;
    //                    break;
    //                }
    //            }
    //        }
    //        if (validate == 0) {
    //            const dataSend = {
    //                edicodposic: edicodposic.value, depaidedit: depaidedit.value, puesidedit: puesidedit.value, editatcla: editatcla.value,
    //                editlocalityr: editlocalityr.value, emprepedit: emprepedit.value, edireportempr: edireportempr.value,
    //                clvposition: clvposition.value
    //            };
    //            console.log(dataSend);
    //            //$.ajax({
    //            //    url: "../SaveDataGeneral/SavePositions",
    //            //    type: "POST",
    //            //    data: dataSend,
    //            //    success: (data) => {
    //            //        if (data.result === "success") {
    //            //            Swal.fire({
    //            //                title: 'Posicion registrada', icon: 'success',
    //            //                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
    //            //                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
    //            //            }).then((acepta) => {
    //            //                $("#registerposition").modal('hide');
    //            //                setTimeout(() => {
    //            //                    if (JSON.parse(localStorage.getItem('modalbtnpositions')) != null) {
    //            //                        $("#searchpositions").modal('show');
    //            //                    } else {
    //            //                        $("#searchpositionstab").modal('show');
    //            //                    }
    //            //                }, 1000);
    //            //            });
    //            //        } else {
    //            //            Swal.fire({
    //            //                title: 'Error', text: 'Contacte a sistemas', icon: 'error',
    //            //                showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
    //            //                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
    //            //            }).then((acepta) => {
    //            //                $("#registerposition").modal('hide');
    //            //                setTimeout(() => {
    //            //                    if (JSON.parse(localStorage.getItem('modalbtnpositions')) != null) {
    //            //                        $("#searchpositions").modal('show');
    //            //                    } else {
    //            //                        $("#searchpositionstab").modal('show');
    //            //                    }
    //            //                }, 1000);
    //            //            });
    //            //        }
    //            //    }, error: (jqxHR, exception) => {
    //            //        fcaptureaerrorsajax(jqxHR, exception);
    //            //    }
    //            //});
    //        }
    //    } catch (error) {
    //        if (error instanceof TypeError) {
    //            console.log('TypeError ', error);
    //        } else if (error instanceof EvalError) {
    //            console.log('EvalError ', error);
    //        } else if (error instanceof RangeError) {
    //            console.log('RangeError ', error);
    //        } else {
    //            console.log('Error ', error);
    //        }
    //    }
    //});

    // new code

    const btnSearchEditPuesto = document.getElementById('btn-search-puesto-edit');
    const searchpuestokeyaddedit = document.getElementById('searchpuestokeyaddedit');
    const btnCloseSearchPuestoEdit = document.getElementById('btn-close-search-puestosaddedit');
    const icoCloseSearchPuestoEdit = document.getElementById('ico-close-search-puestosaddedit');
    const resultpuestosaddedit = document.getElementById('resultpuestosaddedit');
    const noresultsjobs2edit = document.getElementById('noresultsjobs2edit');

    const btnSearchEditDepartament = document.getElementById('btn-search-departament-edit');
    const searchdepartmentkeyaddedit = document.getElementById('searchdepartmentkeyaddedit');
    const icoCloseSearchDepartamentsEdit = document.getElementById('ico-close-search-departamentsaddedit');
    const btnCloseSearchDepartamentsEdit = document.getElementById('btn-close-search-departamentsaddedit');
    const resultdepartmentsaddedit = document.getElementById('resultdepartmentsaddedit');
    const noresultsdepartamentaddedit = document.getElementById('noresultsdepartamentaddedit');

    const btnSearchEditLocality = document.getElementById('btn-search-localidad-edit');
    const searchlocalityaddedit = document.getElementById('searchlocalityaddedit');
    const icoCloseSearchLocalitysEdit = document.getElementById('ico-close-search-localitys-edit');
    const btnCloseSearchLocalitysEdit = document.getElementById('btn-close-search-localitys-edit');
    const noresultslocalityedit = document.getElementById('noresultslocalityedit');
    const resultlocalityaddedit = document.getElementById('resultlocalityaddedit');

    const btnsaveeditposition = document.getElementById('btnsaveeditposition');

    btnSearchEditPuesto.addEventListener('click', () => {
        $("#editposition").modal("hide");
        setTimeout(() => {
            searchpuestokeyaddedit.focus();
        }, 500);
    });

    fCloseSearchPuestoEdit = () => {
        $("#searchpuestoedit").modal("hide");
        setTimeout(() => {
            $("#editposition").modal("show");
        }, 500);
    };

    icoCloseSearchPuestoEdit.addEventListener('click', fCloseSearchPuestoEdit);
    btnCloseSearchPuestoEdit.addEventListener('click', fCloseSearchPuestoEdit);

    btnSearchEditDepartament.addEventListener('click', () => {
        $("#editposition").modal("hide");
        setTimeout(() => {
            searchdepartmentkeyaddedit.focus();
        }, 500);
    });

    fCloseSearchDepartamentsEdit = () => {
        $("#searchdepartamentedit").modal("hide");
        setTimeout(() => {
            $("#editposition").modal("show");
        }, 500);
    }

    icoCloseSearchDepartamentsEdit.addEventListener('click', fCloseSearchDepartamentsEdit);
    btnCloseSearchDepartamentsEdit.addEventListener('click', fCloseSearchDepartamentsEdit);

    btnSearchEditLocality.addEventListener('click', () => {
        $("#editposition").modal("hide");
        setTimeout(() => {
            searchlocalityaddedit.focus();
        }, 500);
    });

    fCloseSearchLocalitysEdit = () => {
        $("#searchlocalidadedit").modal("hide");
        setTimeout(() => {
            $("#editposition").modal("show");
        }, 500);
    }

    icoCloseSearchLocalitysEdit.addEventListener('click', fCloseSearchLocalitysEdit);
    btnCloseSearchLocalitysEdit.addEventListener('click', fCloseSearchLocalitysEdit);

    fsearchlocalitysaddedit = () => {
        try {
            noresultslocalityedit.innerHTML = '';
            resultlocalityaddedit.innerHTML = '';
            if (searchlocalityaddedit.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchLocalitys",
                    type: "POST",
                    data: { wordsearch: searchlocalityaddedit.value },
                    success: (data) => {
                        resultlocalityaddedit.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                resultlocalityaddedit.innerHTML += `<button onclick="fselectlocalityeditp(${data[i].iIdLocalidad}, '${data[i].sDescripcion}',${data[i].iRegistroPatronal_id}, '${data[i].sRegistroPatronal}')" class="animated fadeIn list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back">${number}. ${data[i].iCodigoLocalidad} - ${data[i].sDescripcion} <i class="fas fa-check-circle ml-2 col-ico fa-lg"></i> </button>`;
                            }
                        } else {
                            document.getElementById('noresultslocalityedit').innerHTML = `
                                <div class="alert alert-danger text-center" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron localidades con el termino <b>${searchlocalityaddedit.value}</b>
                                </div>
                            `;
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                resultlocalityaddedit.innerHTML = '';
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

    searchlocalityaddedit.addEventListener('keyup', fsearchlocalitysaddedit);

    fselectlocalityeditp = (paramid, paramstr, paramregpat, paramstrregpat) => {
        try {
            $("#searchlocalidadedit").modal('hide');
            searchlocalityaddedit.value = '';
            resultlocalityaddedit.innerHTML = '';
            $("#editposition").modal('show');
            document.getElementById('editlocalityrnew').value = paramid;
            edilocalityrtxt.value = paramstr;
            document.getElementById('editatcla').innerHTML = `<option value="${paramregpat}">${paramstrregpat}</option>`;
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

    /* FUNCION QUE CARGA LOS DEPARTAMENTOS AL MOMENTO DE CREAR UNA NUEVA POSICION */
    fsearchdepartamentsaddedit = () => {
        try {
            resultdepartmentsaddedit.innerHTML = '';
            document.getElementById('noresultsdepartamentaddedit').innerHTML = '';
            if (searchdepartmentkeyaddedit.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchDepartaments",
                    type: "POST",
                    data: { wordsearch: searchdepartmentkeyaddedit.value, type: 'EMPR' },
                    success: (data) => {
                        resultdepartmentsaddedit.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                resultdepartmentsaddedit.innerHTML += `<button onclick="fselectoptionnewpositionedit(${data[i].iIdDepartamento},'${data[i].sDeptoCodigo}')" class="animated fadeIn list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back">${number}. - ${data[i].sDeptoCodigo} - ${data[i].sDescripcionDepartamento} <i class="fas fa-check-circle ml-2 col-ico fa-lg"></i> </button>`;
                            }
                        } else {
                            document.getElementById('noresultsdepartamentaddedit').innerHTML = `
                                <div class="alert alert-danger text-center" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron departamentos con el termino <b>${searchdepartmentkeyaddedit.value}</b>
                                </div>
                            `;
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                resultdepartmentsaddedit.innerHTML = '';
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

    fselectoptionnewpositionedit = (paramid, paramstr) => {
        try {
            searchdepartmentkeyaddedit.value = "";
            resultdepartmentsaddedit.innerHTML = '';
            document.getElementById('noresultsdepartamentaddedit').innerHTML = '';
            $("#searchdepartamentedit").modal('hide');
            $("#editposition").modal('show');
            document.getElementById('depaideditnew').value = paramid;
            departedit.value = paramstr;
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

    searchdepartmentkeyaddedit.addEventListener('keyup', fsearchdepartamentsaddedit);

    /* FUNCION QUE REALIZA LA BUSQUEDA EN TIEMPO REAL DE PUESTOS AL MOMENTO DE REGISTRAR UNA NUEVA POSICION */
    fsearchkeyuppuestoaddedit = () => {
        try {
            resultpuestosaddedit.innerHTML = '';
            document.getElementById('noresultsjobs2edit').innerHTML = '';
            if (searchpuestokeyaddedit.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchPuesto",
                    type: "POST",
                    data: { wordsearch: searchpuestokeyaddedit.value },
                    success: (data) => {
                        resultpuestosaddedit.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                resultpuestosaddedit.innerHTML += `<button onclick="fselectpuestoposedit(${data[i].iIdPuesto},'${data[i].sNombrePuesto}')" class="animated fadeIn list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back">${number} - ${data[i].sCodigoPuesto} - ${data[i].sNombrePuesto} <i class="fas fa-check-circle ml-2 col-ico fa-lg"></i> </button>`;
                            }
                        } else {
                            document.getElementById('noresultsjobs2edit').innerHTML = `
                                <div class="alert alert-danger text-center" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron puestos con el termino <b>${searchpuestokeyaddedit.value}</b>
                                </div>
                            `;
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                resultpuestosaddedit.innerHTML = '';
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

    fselectpuestoposedit = (paramid, paramstr) => {
        try {
            searchpuestokeyaddedit.value = '';
            resultpuestosaddedit.innerHTML = '';
            $("#searchpuestoedit").modal('hide');
            $("#editposition").modal('show');
            document.getElementById('puesideditnew').value = paramid;
            pueusuedit.value = paramstr;
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

    searchpuestokeyaddedit.addEventListener('keyup', fsearchkeyuppuestoaddedit);

    // Funcion que guarda la edicion de la posicion
    fSaveEditPosition = () => {
        try {
            const newLocality = document.getElementById('editlocalityrnew');
            const newDepartament = document.getElementById('depaideditnew');
            const newPost = document.getElementById('puesideditnew');
            if (newLocality.value != "0" || newDepartament.value != "0" || newPost.value != "0") {
                if (parseInt(newLocality.value) != parseInt(editlocalityr.value) || parseInt(newDepartament.value) != parseInt(depaidedit.value) || parseInt(newPost.value) != parseInt(puesidedit.value)) {
                    let valueLocality = (parseInt(newLocality.value) == 0) ? parseInt(editlocalityr.value) : parseInt(newLocality.value);
                    let valueDepartament = (parseInt(newDepartament.value) == 0) ? parseInt(depaidedit.value) : parseInt(newDepartament.value);
                    let valuePost = (parseInt(newPost.value) == 0) ? parseInt(puesidedit.value) : parseInt(newPost.value);
                    const dataSend = { newLocality: valueLocality, newDepartament: valueDepartament, newPost: valuePost, position: parseInt(clvposition.value) };
                    //console.log(dataSend);
                    $.ajax({
                        url: "../SaveDataGeneral/SaveEditPosition",
                        type: "POST",
                        data: dataSend,
                        beforeSend: () => {
                            btnsaveeditposition.disabled = true;
                        }, success: (data) => {
                            console.log(data);
                            if (data.Bandera == true && data.MensajeError == "none") {
                                Swal.fire({
                                    title: 'Posicion Actualizada', icon: 'success',
                                    showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                    confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                }).then((acepta) => {
                                    btnsaveeditposition.disabled = false;
                                    editlocalityr.value = valueLocality;
                                    depaidedit.value = valueDepartament;
                                    puesidedit.value = valuePost;
                                });
                            } else {
                                Swal.fire({
                                    title: 'Error', text: "Ocurrio un error interno en la aplicación", icon: 'success',
                                    showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                                    confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                }).then((acepta) => {
                                    $("#editposition").modal('hide');
                                    btnsaveeditposition.disabled = false;
                                });
                            }
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    });
                } else {
                    Swal.fire({
                        title: 'Atención!', text: 'No hay cambios que actualizar', icon: 'warning',
                        showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                        confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                    });
                }
            } else {
                Swal.fire({
                    title: 'Atención!', text: 'No hay cambios que actualizar', icon: 'warning',
                    showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
                    confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
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

    btnsaveeditposition.addEventListener('click', fSaveEditPosition);

});