$(function () {

    const btndownloadreportlocalitys = document.getElementById('btndownloadreportlocalitys');

    // Funcion que realiza la descarga del reporte de posiciones \\
    fGenerateReportLocalitys = () => {
        $("#modalDownloadCatalogs").modal("show");
        try {
            $.ajax({
                url: "../Reportes/GenerateReportCatalogs",
                type: "POST",
                data: { key: "LOCALIDADES" },
                beforeSend: () => {
                    $("#searchlocalidadbtn").modal("hide");
                    document.getElementById('contenDownloadReport').innerHTML = `
                        <div class="spinner-border text-primary" role="status">
                          <span class="sr-only">Loading...</span>
                        </div>
                    `;
                }, success: (request) => {
                    //console.log(request);
                    document.getElementById('nameReport').textContent = "LOCALIDADES";
                    if (request.Bandera == true) {
                        document.getElementById('contenDownloadReport').innerHTML = `
                            <a class='btn btn-primary btn-sm' download='${request.Archivo}' href='/Content/REPORTES/${request.Folder}/${request.Archivo}'> <i class='fas fa-file-download mr-2'></i> Descargar </a>
                        `;
                        document.getElementById('icoCloseDownloadReport').setAttribute("onclick", `fRestoreReportLC('${request.Archivo}', '${request.Folder}');`);
                        document.getElementById('btnCloseDownloadReport').setAttribute("onclick", `fRestoreReportLC('${request.Archivo}', '${request.Folder}');`);
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

    btndownloadreportlocalitys.addEventListener('click', fGenerateReportLocalitys);

    fRestoreReportLC = (archivo, folder) => {
        try {
            if (archivo != "" && folder != "") {
                const dataSend = { file: archivo, folder: folder };
                $.ajax({
                    url: "../Reportes/DeleteReportCatalogs",
                    type: "POST",
                    data: dataSend,
                    success: () => {
                        $("#searchlocalidadbtn").modal("show");
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

    fshowtypealertloc = (title, text, icon, element, clear) => {
        Swal.fire({
            title: title, text: text, icon: icon,
            showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
            confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
        }).then((acepta) => {
            $("html, body").animate({ scrollTop: $(`#${element.id}`).offset().top - 50 }, 1000);
            if (clear == 1) {
                setTimeout(() => {
                    element.focus();
                    setTimeout(() => { element.value = ""; }, 300);
                }, 1200);
            } else {
                setTimeout(() => { element.focus(); }, 1200);
            }
            if (element.id == "numpla") {
                setTimeout(() => {
                    $("#btn-search-table-num-posicion").click();
                }, 1500);
            }
        });
    };

    /* CONSTANTES DE LOCALIDADES EN FORMULARIO DE REGISTRO DE POSICIONES */
    const localityr       = document.getElementById('localityr');
    const localityrtxt    = document.getElementById('localityrtxt');
    const editlocalityr   = document.getElementById('editlocalityr');
    const edilocalityrtxt = document.getElementById('edilocalityrtxt');
    /* CONSTANTES BUSQUEDA DE LOCALIDADES AL REGISTRAR UNA NUEVA POSICION */
    const btnsearchlocalidad = document.getElementById('btn-search-localidad');
    const searchlocalityadd  = document.getElementById('searchlocalityadd');
    const resultlocalityadd  = document.getElementById('resultlocalityadd');
    const icoclosesearchlocalitys = document.getElementById('ico-close-search-localitys');
    const btnclosesearchlocalitys = document.getElementById('btn-close-search-localitys');
    /* EJECUCION DE EVENTO QUE OCULTA LA BUSQUEDA DE POSICIONES AL BUSCAR UNA LOCALIDAD */
    btnsearchlocalidad.addEventListener('click', () => { $("#registerposition").modal('hide'); setTimeout(() => { searchlocalityadd.focus(); }, 1000); });
    /* FUNCION QUE LIMPIA EL CAMPO DE BUSQUEDA Y LA LISTA DE RESULTADOS */
    fclearsearchresults = () => {
        searchlocalityadd.value = '';
        resultlocalityadd.innerHTML = '';
        document.getElementById('noresultslocality1').innerHTML = '';
        $("#searchlocality").modal('hide');
        $("#registerposition").modal('show');
        localStorage.removeItem('reglocality');
    }

    /* EJECUCION DE FUNCION QUE LIMPIA EL INPUT DE BUSQUEDA Y LA LISTA DE RESULTADOS ENCONTRADOS */
    btnclosesearchlocalitys.addEventListener('click', fclearsearchresults);
    icoclosesearchlocalitys.addEventListener('click', fclearsearchresults);
    /* FUNCION  QUE CARGA LOS DATOS DE LA LOCALIDAD SELECCIONADA EN EL FORMULARIO DE REGISTRO DE NUEVA POSICION */
    fselectlocality = (paramid, paramstr, paramregpat, paramstrregpat) => {
        try {
            $("#searchlocalidad").modal('hide'); 
            searchlocalityadd.value     = '';
            resultlocalityadd.innerHTML = '';
            $("#registerposition").modal('show');
            localityr.value    = paramid;
            localityrtxt.value = paramstr;
            document.getElementById('regpatcla').innerHTML = `<option value="${paramregpat}">${paramstrregpat}</option>`;
            localStorage.removeItem('reglocality');
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
    /* FUNCION QUE REALIZA LA BUSQUEDA EN TIEMPO REAL DE LAS LOCALIDADES */
    fsearchlocalitysadd = () => {
        try {
            noresultslocality1.innerHTML = '';
            resultlocalityadd.innerHTML = '';
            if (searchlocalityadd.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchLocalitys",
                    type: "POST",
                    data: { wordsearch: searchlocalityadd.value },
                    success: (data) => {
                        resultlocalityadd.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                resultlocalityadd.innerHTML += `<button onclick="fselectlocality(${data[i].iIdLocalidad}, '${data[i].sDescripcion}',${data[i].iRegistroPatronal_id}, '${data[i].sRegistroPatronal}')" class="animated fadeIn list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back">${number}. ${data[i].iCodigoLocalidad} - ${data[i].sDescripcion} <i class="fas fa-check-circle ml-2 col-ico fa-lg"></i> </button>`;
                            }
                        } else {
                            document.getElementById('noresultslocality1').innerHTML = `
                                <div class="alert alert-danger text-center" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron localidades con el termino <b>${searchlocalityadd.value}</b>
                                </div>
                            `;
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                resultlocalityadd.innerHTML = '';
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
    /* EJECUCION DEL EVENTO QUE EJECUTA LA FUNCION QUE REALIZA LA BUSQUEDA DE LOCALIDADES EN TIEMPO REAL */
    searchlocalityadd.addEventListener('keyup', fsearchlocalitysadd);

    /* FUNCION QUE REALIZA LA CARGA DE LOS REGISTROS PATRONALES DE LA EMPRESA EN SESION */
    floadregpatrlocalitys = async (element) => {
        try {
            await $.ajax({
                url: "../SearchDataCat/LoadRegPatLocalitys",
                type: "POST",
                data: {},
                success: (data) => {
                    if (data.Bandera === true && data.MensajeError === "none") {
                        if (data.Datos.length > 0) {
                            for (let i = 0; i < data.Datos.length; i++) {
                                element.innerHTML += `<option value="${data.Datos[i].iIdRegPat}">${data.Datos[i].sAfiliacionIMSS}</option>`;
                            }
                        } else {
                            alert('No hay registros patronales asignados a la empresa');
                        }
                    } else {
                        alert('Error al cargar los registros patronales de la empresa');
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

    /* FUNCION QUE REALIZA LA CARGA DE LAS ZONAS ECONOMINCAS */
    floadzoneconomica = async (element) => {
        try {
            await $.ajax({
                url: "../SearchDataCat/LoadZoneEconomic",
                type: "POST",
                data: {},
                success: (data) => {
                    if (data.Bandera === true && data.MensajeError === "none") {
                        if (data.Datos.length > 0) {
                            for (let i = 0; i < data.Datos.length; i++) {
                                element.innerHTML += `<option value="${data.Datos[i].iIdZonaEconomica}">${data.Datos[i].sDescripcion}</option>`;
                            }
                        } else {
                            alert('No hay registros de zonas economicas');
                        }
                    } else {
                        alert('Error al cargar las zonas economicas');
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

    /* FUNCION QUE REALIZA LA CARGA DE LOS ESTADOS DE LA REPUBLICA */
    floadestatesrepublic = async (element) => {
        try {
            await $.ajax({
                url: "../SearchDataCat/LoadEstatesRep",
                type: "POST",
                data: {},
                success: (data) => {
                    if (data.Bandera === true && data.MensajeError === "none") {
                        if (data.Datos.length > 0) {
                            for (let i = 0; i < data.Datos.length; i++) {
                                element.innerHTML += `<option value="${data.Datos[i].iId}">${data.Datos[i].sValor}</option>`;
                            }
                        } else {
                            alert('No hay registros de estados de la republica');
                        }
                    } else {
                        alert('Error al cargar los estados de la republica');
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

    /* APARTADO DE BUSQUEDA DE LOCALIDADES */

    /*
     * CONSTANTES LOCALIDADES 
     */

    const btnModalSearchLocalitys = document.getElementById('btn-modal-search-localitys');
    const keysearchlocalityadd    = document.getElementById('keysearchlocalityadd');
    const listresultslocalitysadd = document.getElementById('listresultslocalitysadd');
    const noresultslocality1btn   = document.getElementById('noresultslocality1btn');
    const btnCloseSearchLocaliBtn = document.getElementById('btn-close-search-localitys-btn');
    const icoCloseSearchLocaliBtn = document.getElementById('ico-close-search-localitys-btn');

    keysearchlocalityadd.style.transition = "1s";
    keysearchlocalityadd.style.cursor     = "pointer";
    keysearchlocalityadd.addEventListener('mouseover',  () => { keysearchlocalityadd.classList.add('shadow'); });
    keysearchlocalityadd.addEventListener('mouseleave', () => { keysearchlocalityadd.classList.remove('shadow'); });

    /*
     * Edicion 
     */

    const keylocalityedit   = document.getElementById('keylocalityedit');
    const codelocalityedit  = document.getElementById('codelocalityedit');
    const desclocalityedit  = document.getElementById('desclocalityedit');
    const ivalocalityedit   = document.getElementById('ivalocalityedit');
    const regpatrlocalityedit = document.getElementById('regpatrlocalityedit');
    const zoneecolocalityedit = document.getElementById('zoneecolocalityedit');
    const estatelocalityedit  = document.getElementById('estatelocalityedit');
    const reglocalityedit   = document.getElementById('reglocalityedit');
    const idreglocalityedit = document.getElementById('idreglocalityedit');
    const suclocalityedit   = document.getElementById('suclocalityedit');
    const idsuclocalityedit = document.getElementById('idsuclocalityedit');

    const btnsaveeditlocality = document.getElementById('btnsaveeditlocality');

    /*
     * FUNCIONES LOCALIDADES
     */

    /* FUNCION QUE LIMPIA LA CAJA DE BUSQUEDA Y LOS RESULTADOS DE LAS LOCALIDADES */
    fClearSearchResultLocalitys = () => {
        keysearchlocalityadd.value        = '';
        listresultslocalitysadd.innerHTML = '';
        noresultslocality1btn.innerHTML   = '';
    }

    /* FUNCION QUE REALIZA LA BUSQUEDA EN TIEMPO REAL */
    fSearchLocalitysBtnAdd = () => {
        try {
            noresultslocality1btn.innerHTML = '';
            listresultslocalitysadd.innerHTML = '';
            if (keysearchlocalityadd.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchLocalitys",
                    type: "POST",
                    data: { wordsearch: keysearchlocalityadd.value },
                    success: (data) => {
                        listresultslocalitysadd.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                listresultslocalitysadd.innerHTML += `<button onclick="fselectlocalityedit(${data[i].iIdLocalidad})" class="list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back border-left-primary animated fadeIn">${number}. ${data[i].iCodigoLocalidad} - ${data[i].sDescripcion} <i class="fas fa-edit ml-2 text-warning fa-lg"></i> </button>`;
                            }
                        } else {
                            noresultslocality1btn.innerHTML = `
                                <div class="alert alert-danger text-center" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron localidades con el termino <b>${keysearchlocalityadd.value}</b>
                                </div>
                            `;
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                listresultslocalitysadd.innerHTML = '';
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

    /* FUNCION QUE CARGA LOS DATOS DE LA LOCALIDAD A EDICION */
    fselectlocalityedit = async (paramid) => {
        $("#searchlocalidadbtn").modal('hide');
        fClearSearchResultLocalitys();
        try {
            if (parseInt(paramid) > 0) {
                await $.ajax({
                    url: "../SearchDataCat/DataLocalitySelect",
                    type: "POST",
                    data: { keyLocality: parseInt(paramid) },
                    success: (data) => {
                        if (data.Bandera === true && data.MensajeError === "none") {
                            //console.log(data.Datos);
                            $("#localityedit").modal('show');
                            keylocalityedit.readOnly  = true;
                            keylocalityedit.value     = data.Datos.IdLocalidad;
                            codelocalityedit.readOnly = true;
                            codelocalityedit.value    = data.Datos.Codigo_Localidad;
                            desclocalityedit.value    = data.Datos.Descripcion;
                            ivalocalityedit.value     = data.Datos.TasaIva;
                            regpatrlocalityedit.value = data.Datos.RegistroPatronal_id;
                            zoneecolocalityedit.value = data.Datos.ZonaEconomica_id;
                            if (data.Datos.Estado_id == 0) {
                                estatelocalityedit.value = "none";
                            } else {
                                estatelocalityedit.value = data.Datos.Estado_id;
                            }
                            reglocalityedit.value     = data.Datos.ClaveRegional;
                            idreglocalityedit.value   = data.Datos.Regional_id;
                            suclocalityedit.value     = data.Datos.ClaveSucursal;
                            idsuclocalityedit.value   = data.Datos.Sucursal_id;
                        } else {
                            alert('No se cargaro los datos de localidad a edicion');
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                alert('Accion no valida, no se cargaron los datos de localidad a edicion');
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

    /* REGIONALES LOCALIDAD EDICION */

    const btnSearchRegLocalityEdit = document.getElementById('btn-search-regionales-locality-edit');
    const searchregionalkeydynamic = document.getElementById('searchregionalkeydynamic');
    const resultregionalesdynamic = document.getElementById('resultregionalesdynamic');
    const noresultsregionales1dynamic = document.getElementById('noresultsregionales1dynamic');
    const icoCloseSearchRegionalesDy = document.getElementById('ico-close-search-regionales-dy');
    const btnCloseSearchRegionalesDy = document.getElementById('btn-close-search-regionales-dy')

    /* FUNCIONES LOCALIDAD EDICION */

    /* FUNCION QUE LIMPIA LA CAJA DE BUSQUEDA Y LOS RESULTADOS DE LAS REGIONALES, ELIMINA LOCALSTORE */
    fClearFieldsSearchRegionales = () => {
        searchregionalkeydynamic.value        = '';
        resultregionalesdynamic.innerHTML     = '';
        noresultsregionales1dynamic.innerHTML = '';
        localStorage.removeItem('meditreglocality');
        localStorage.removeItem('msavereglocality');
    }

    /* FUNCION QUE HACE LA BUSQUEDA EN TIEMPO REAL DE LAS REGIONALES */
    fSearchRegionalesLocalityDy = () => {
        resultregionalesdynamic.innerHTML = '';
        noresultsregionales1dynamic.innerHTML = '';
        try {
            if (searchregionalkeydynamic.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchRegionales",
                    type: "POST",
                    data: { wordsearch: searchregionalkeydynamic.value },
                    success: (data) => {
                        resultregionalesdynamic.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                resultregionalesdynamic.innerHTML += `<button title="Seleccionar" onclick="fselectregionallocalitydy(${data[i].iIdRegional}, '${data[i].sClaveRegional}')" class="animated fadeIn list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back">${number}. - ${data[i].sClaveRegional} - ${data[i].sDescripcionRegional} <i class="fas fa-check-circle ml-2 col-ico fa-lg"></i> </button>`;
                            }
                        } else {
                            noresultsregionales1dynamic.innerHTML = `
                                <div class="alert alert-danger text-center" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron regionales con el termino <b>${searchregionalkeydynamic.value}</b>
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

    /* FUNCION QUE SELECCIONA LA REGIONAL DE LA LOCALDIAD */
    fselectregionallocalitydy = (paramid, paramstr) => {
        try {
            if (localStorage.getItem("meditreglocality") == 1) {
                idreglocalityedit.value = paramid;
                reglocalityedit.value   = paramstr;
                localStorage.removeItem('meditreglocality');
                setTimeout(() => { $("#localityedit").modal('show'); }, 1000);
            }
            if (localStorage.getItem("msavereglocality") == 1) {
                idreglocalitysave.value = paramid;
                reglocalitysave.value = paramstr;
                localStorage.removeItem('msavereglocality');
                setTimeout(() => { $("#registerlocality").modal('show'); }, 1000);
            }
            fClearFieldsSearchRegionales();
            $("#searchregiondynamic").modal('hide');
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

    /* EJECUCION LOCALIDAD EDICION */
    btnSearchRegLocalityEdit.addEventListener('click', () => {
        $("#localityedit").modal('hide');
        setTimeout(() => {
            searchregionalkeydynamic.focus();
        }, 1000);
        localStorage.setItem('meditreglocality', 1);
    });

    searchregionalkeydynamic.addEventListener('keyup', fSearchRegionalesLocalityDy);

    btnCloseSearchRegionalesDy.addEventListener('click', fClearFieldsSearchRegionales);
    icoCloseSearchRegionalesDy.addEventListener('click', fClearFieldsSearchRegionales);

    /* SUCURSALES LOCALIDADES EDICION */

    const btnSearchSucLocEdit     = document.getElementById('btn-search-sucursales-locality-edit');
    const searchofficekeydynamic  = document.getElementById('searchofficekeydynamic');
    const resultofficesdynamic    = document.getElementById('resultofficesdynamic');
    const noresultoffices1dynamic = document.getElementById('noresultoffices1dynamic');
    const icoCloseSearchOfficesDy = document.getElementById('ico-close-search-offices-dy');
    const btnCloseSearchOfficesDy = document.getElementById('btn-close-search-offices-dy');

    /* FUNCIONES SUCURSALES LOCALIDADES EDICION */

    /* FUNCION QUE LIMPIA LA CAJA DE BUSQUEDA Y RESULTADOS DE LAS SUCURSALES (EDICION) */
    fClearFieldsSearchSucursales = () => {
        searchofficekeydynamic.value      = '';
        resultofficesdynamic.innerHTML    = '';
        noresultoffices1dynamic.innerHTML = '';
        localStorage.removeItem("meditsuclocality");
        localStorage.removeItem("msavesuclocality");
    }

    /* FUNCION QUE HACE LA BUSQUEDA EN TIEMPO REAL DE LAS SUCURSALES*/
    fSearchSucursalesDynamic = () => {
        resultofficesdynamic.innerHTML = '';
        noresultoffices1dynamic.innerHTML = '';
        try {
            if (searchofficekeydynamic.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchOffices",
                    type: "POST",
                    data: { wordsearch: searchofficekeydynamic.value },
                    success: (data) => {
                        resultofficesdynamic.innerHTML = '';
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                resultofficesdynamic.innerHTML += `<button title="Seleccionar" onclick="fselectofficedynamic(${data[i].iIdSucursal}, '${data[i].sClaveSucursal}')" class="animated fadeIn list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded">${number}. - ${data[i].sClaveSucursal} - ${data[i].sDescripcionSucursal} <i class="fas fa-check-circle ml-2 col-ico fa-lg"></i> </button>`;
                            }
                        } else {
                            noresultoffices1dynamic.innerHTML = `
                                <div class="alert alert-danger text-center" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron sucursales con el termino <b>${searchofficekeydynamic.value}</b>
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

    /* FUNCION QUE SELECCIONA LA SUCURSAL A EDITAR O GUARDAR */
    fselectofficedynamic = (paramid, paramstr) => {
        try {
            if (localStorage.getItem("meditsuclocality") == 1) {
                idsuclocalityedit.value = paramid;
                suclocalityedit.value = paramstr;
                localStorage.removeItem("meditsuclocality");
                setTimeout(() => {
                    $("#localityedit").modal('show');
                }, 1000);
            }
            if (localStorage.getItem("msavesuclocality") == 1) {
                idsuclocalitysave.value = paramid;
                suclocalitysave.value   = paramstr;
                localStorage.removeItem("msavesuclocality");
                setTimeout(() => {
                    $("#registerlocality").modal('show');
                }, 1000);
            }
            fClearFieldsSearchSucursales();
            $("#searchsucursalesdynamic").modal('hide');
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


    /* EJECUCION FUNCIONES SUCURSALES */

    btnSearchSucLocEdit.addEventListener('click', () => {
        $("#localityedit").modal('hide');
        setTimeout(() => {
            searchofficekeydynamic.focus();
        }, 1000);
        localStorage.setItem("meditsuclocality", 1);
    });

    searchofficekeydynamic.addEventListener('keyup', fSearchSucursalesDynamic);

    btnCloseSearchOfficesDy.addEventListener('click', fClearFieldsSearchSucursales);
    icoCloseSearchOfficesDy.addEventListener('click', fClearFieldsSearchSucursales);

    /* FUNCION QUE REALIZA EL GUARDADO DE LA EDICION DE LA LOCALIDAD */
    fSaveEditLocality = () => {
        try {
            if (desclocalityedit.value != "") {
                if (ivalocalityedit.value != "") {
                    if (regpatrlocalityedit.value != "none") {
                        if (zoneecolocalityedit.value != "none") {
                            if (estatelocalityedit.value != "none") {
                                if (idreglocalityedit.value != "") {
                                    if (idsuclocalityedit.value != "") {
                                        let valueState = estatelocalityedit.value;
                                        if (valueState == "none") {
                                            valueState = 0;
                                        }
                                        const dataSend = {
                                            keylocality: parseInt(keylocalityedit.value),
                                            desclocality: desclocalityedit.value,
                                            ivalocality: ivalocalityedit.value,
                                            regpatlocality: parseInt(regpatrlocalityedit.value),
                                            zonelocality: parseInt(zoneecolocalityedit.value),
                                            estatelocality: parseInt(estatelocalityedit.value),
                                            idreglocality: parseInt(idreglocalityedit.value),
                                            idsuclocality: parseInt(idsuclocalityedit.value)
                                        };
                                        //console.log(dataSend);
                                        $.ajax({
                                            url: "../SearchDataCat/SaveEditLocality",
                                            type: "POST",
                                            data: dataSend,
                                            beforeSend: () => {
                                                btnsaveeditlocality.disabled = true;
                                            }, success: (data) => {
                                                console.log(data);
                                                if (data.Bandera === true && data.MensajeError === "none") {
                                                    Swal.fire({
                                                        title: "Correcto!", text: "Localidad actualizada!", icon: "success",
                                                        showClass: { popup: 'animated fadeInDown faster' },
                                                        hideClass: { popup: 'animated fadeOutUp faster' },
                                                        confirmButtonText: "Aceptar",
                                                        allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                                    }).then((acepta) => {
                                                        $("#localityedit").modal('hide');
                                                        setTimeout(() => { $("#searchlocalidadbtn").modal('show'); }, 1000);
                                                        setTimeout(() => { keysearchlocalityadd.focus(); }, 2000);
                                                    });
                                                } else {
                                                    Swal.fire({
                                                        title: "Error!", text: "No se actualizo la localidad", icon: "error",
                                                        showClass: { popup: 'animated fadeInDown faster' },
                                                        hideClass: { popup: 'animated fadeOutUp faster' },
                                                        confirmButtonText: "Aceptar",
                                                        allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                                    }).then((acepta) => {
                                                        $("#localityedit").modal('hide');
                                                        setTimeout(() => { $("#searchlocalidadbtn").modal('show'); }, 1000);
                                                        setTimeout(() => { keysearchlocalityadd.focus(); }, 2000);
                                                    });
                                                }
                                                btnsaveeditlocality.disabled = false;
                                            }, error: (jqXHR, exception) => {
                                                fcaptureaerrorsajax(jqXHR, exception);
                                            }
                                        });
                                    } else {
                                        fshowtypealertloc('Atencion!', 'Completa el campo Sucursal', 'info', suclocalityedit, 0);
                                    }
                                } else {
                                    fshowtypealertloc('Atencion!', 'Completa el campo Regional', 'info', reglocalityedit, 0);
                                }
                            } else {
                                fshowtypealertloc('Atencion!', 'Completa el campo Estado', 'info', estatelocalityedit, 0);
                            }
                        } else {
                            fshowtypealertloc('Atencion!', 'Completa el campo Zona economica', 'info', zoneecolocalityedit, 0);
                        }
                    } else {
                        fshowtypealertloc('Atencion!', 'Completa el campo Registro patronal', 'info', regpatrlocalityedit, 0);
                    }
                } else {
                    fshowtypealertloc('Atencion!', 'Completa el campo ' + ivalocalityedit.placeholder, 'info', ivalocalityedit, 0);
                }
            } else {
                fshowtypealertloc('Atencion!', 'Completa el campo ' + desclocalityedit.placeholder, 'info', desclocalityedit, 0);
            }
        } catch (error) {
            if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else {
                console.error('Error: ', error.message);
            }
        }
    }


    /* APRTADO DE FUNCIONES DE GUARDADO DE LOCALIDADES */

    const btnregisterlocalitybtn = document.getElementById('btnregisterlocalitybtn');
    const codelocalitysave       = document.getElementById('codelocalitysave');
    const desclocalitysave       = document.getElementById('desclocalitysave');
    const ivalocalitysave        = document.getElementById('ivalocalitysave');
    const regpatrlocalitysave    = document.getElementById('regpatrlocalitysave');
    const zoneecolocalitysave    = document.getElementById('zoneecolocalitysave');
    const estatelocalitysave     = document.getElementById('estatelocalitysave');
    const idreglocalitysave      = document.getElementById('idreglocalitysave');
    const reglocalitysave        = document.getElementById('reglocalitysave');
    const idsuclocalitysave      = document.getElementById('idsuclocalitysave');
    const suclocalitysave        = document.getElementById('suclocalitysave');
    const btnSearchRegiLocSave   = document.getElementById('btn-search-regionales-locality-save');
    const btnSearchSucuLocSave   = document.getElementById('btn-search-sucursales-locality-save');

    const icoCloseLocalitySave = document.getElementById('ico-close-search-localitys-save');
    const btnCloseLocalitySave = document.getElementById('btn-close-search-localitys-save');
    const btnsavelocality      = document.getElementById('btnsavelocality');

    /* FUNCION QUE LIMPIA LOS CAMPOS DEL FORMULARIO DE GUARDADO */
    fClearFieldsSaveLocality = () => {
        codelocalitysave.value    = "";
        desclocalitysave.value    = "";
        ivalocalitysave.value     = "";
        regpatrlocalitysave.value = "none";
        zoneecolocalitysave.value = "none";
        estatelocalitysave.value  = "none";
        idreglocalitysave.value   = "";
        reglocalitysave.value     = "";
        idsuclocalitysave.value   = "";
        suclocalitysave.value     = "";
    }

    /* FUNCION QUE GUARDA LOS DATOS DE LA LOCALIDAD */
    fSaveDataLocality = () => {
        try {
            if (desclocalitysave.value != "") {
                if (ivalocalitysave.value != "") {
                    if (regpatrlocalitysave.value != "none") {
                        if (zoneecolocalitysave.value != "none") {
                            if (estatelocalitysave.value != "none") {
                                if (idreglocalitysave.value != "") {
                                    if (idsuclocalitysave.value != "") {
                                        const dataSend = {
                                            desclocality: desclocalitysave.value,
                                            ivalocality: ivalocalitysave.value,
                                            regpatlocality: parseInt(regpatrlocalitysave.value),
                                            zonelocality: parseInt(zoneecolocalitysave.value),
                                            estatelocality: parseInt(estatelocalitysave.value),
                                            idreglocality: parseInt(idreglocalitysave.value),
                                            idsuclocality: parseInt(idsuclocalitysave.value)
                                        };
                                        $.ajax({
                                            url: "../SearchDataCat/SaveDataLocality",
                                            type: "POST",
                                            data: dataSend,
                                            beforeSend: () => {
                                                btnsavelocality.disabled = true;
                                            }, success: (data) => {
                                                //console.log(data);
                                                if (data.Bandera === true && data.MensajeError === "none") {
                                                    Swal.fire({
                                                        title: "Correcto!", text: "Codigo de localidad: " + String(data.Codigo), icon: "success",
                                                        showClass: { popup: 'animated fadeInDown faster' },
                                                        hideClass: { popup: 'animated fadeOutUp faster' },
                                                        confirmButtonText: "Aceptar",
                                                        allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                                    }).then((acepta) => {
                                                        $("#registerlocality").modal('hide');
                                                        if (localStorage.getItem('reglocality') != null) {
                                                            $("#searchlocalidad").modal('show');
                                                            setTimeout(() => {
                                                                searchlocalityadd.focus();
                                                            }, 1000);
                                                        } else {
                                                            $("#searchlocalidadbtn").modal('show');
                                                            setTimeout(() => { keysearchlocalityadd.focus(); }, 1000);
                                                        }
                                                    });
                                                } else {
                                                    Swal.fire({
                                                        title: "Error!", text: "No se registro la localidad", icon: "error",
                                                        showClass: { popup: 'animated fadeInDown faster' },
                                                        hideClass: { popup: 'animated fadeOutUp faster' },
                                                        confirmButtonText: "Aceptar",
                                                        allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                                    }).then((acepta) => {
                                                        $("#registerlocality").modal('hide');
                                                        if (localStorage.getItem('reglocality') != null) {
                                                            $("#searchlocalidad").modal('show');
                                                            setTimeout(() => {
                                                                searchlocalityadd.focus();
                                                            }, 1000);
                                                        } else {
                                                            setTimeout(() => { $("#searchlocalidadbtn").modal('show'); }, 1000);
                                                            setTimeout(() => { keysearchlocalityadd.focus(); }, 2000);
                                                        }
                                                    });
                                                }
                                                btnsavelocality.disabled = false;
                                            }, error: (jqXHR, exception) => {
                                                fcaptureaerrorsajax(jqXHR, exception);
                                            }
                                        });
                                    } else {
                                        fshowtypealertloc('Atencion!', 'Completa el campo Sucursal', 'info', suclocalitysave, 0);
                                    }
                                } else {
                                    fshowtypealertloc('Atencion!', 'Completa el campo Regional', 'info', reglocalitysave, 0);
                                }
                            } else {
                                fshowtypealertloc('Atencion!', 'Completa el campo Estado', 'info', estatelocalitysave, 0);
                            }
                        } else {
                            fshowtypealertloc('Atencion!', 'Completa el campo Zona economica', 'info', zoneecolocalitysave, 0);
                        }
                    } else {
                        fshowtypealertloc('Atencion!', 'Completa el campo Registro patronal', 'info', regpatrlocalitysave, 0);
                    }
                } else {
                    fshowtypealertloc('Atencion!', 'Completa el campo ' + ivalocalitysave.placeholder, 'info', ivalocalitysave, 0);
                }
            } else {
                fshowtypealertloc('Atencion!', 'Completa el campo ' + desclocalitysave.placeholder, 'info', desclocalitysave, 0);
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

    /*
     * EJECUCION FUNCIONES
     */

    /* GUARDADO */
    floadregpatrlocalitys(regpatrlocalitysave);
    floadzoneconomica(zoneecolocalitysave);
    floadestatesrepublic(estatelocalitysave);

    /* EDICION */
    floadzoneconomica(zoneecolocalityedit);
    floadregpatrlocalitys(regpatrlocalityedit);
    floadestatesrepublic(estatelocalityedit);

    btnSearchRegiLocSave.addEventListener('click', () => {
        $("#registerlocality").modal('hide');
        setTimeout(() => {
            searchregionalkeydynamic.focus();
        }, 1000);
        localStorage.setItem("msavereglocality", 1);
    });

    btnSearchSucuLocSave.addEventListener('click', () => {
        $("#registerlocality").modal('hide');
        setTimeout(() => {
            searchofficekeydynamic.focus();
        }, 1000);
        localStorage.setItem("msavesuclocality", 1);
    });

    btnregisterlocalitybtn.addEventListener('click', () => {
        $("#searchlocalidadbtn").modal('hide');
        setTimeout(() => {
            desclocalitysave.focus();
        }, 1000);
        fClearSearchResultLocalitys();
    });

    btnModalSearchLocalitys.addEventListener('click', () => {
        setTimeout(() => {
            keysearchlocalityadd.focus();
        }, 1000);
    });

    btnCloseSearchLocaliBtn.addEventListener('click', fClearSearchResultLocalitys);
    icoCloseSearchLocaliBtn.addEventListener('click', fClearSearchResultLocalitys);

    btnCloseLocalitySave.addEventListener('click', fClearFieldsSaveLocality);
    icoCloseLocalitySave.addEventListener('click', fClearFieldsSaveLocality);

    keysearchlocalityadd.addEventListener('keyup', fSearchLocalitysBtnAdd);

    btnsaveeditlocality.addEventListener('click', fSaveEditLocality);

    btnsavelocality.addEventListener('click', fSaveDataLocality);

    const btnregisterlocality = document.getElementById('btnregisterlocality');

    localStorage.removeItem('reglocality');

    btnregisterlocality.addEventListener('click', () => {
        $("#searchlocalidad").modal('hide');
        setTimeout(() => { desclocalitysave.focus(); }, 1000);
        localStorage.setItem('reglocality', 1);
    });

});