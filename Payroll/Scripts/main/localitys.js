$(function () {
    console.log('entrando')
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
                                resultlocalityadd.innerHTML += `<button class="list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back">${number}. ${data[i].iCodigoLocalidad} - ${data[i].sDescripcion} <i class="fas fa-check-circle ml-2 col-ico fa-lg" onclick="fselectlocality(${data[i].iIdLocalidad}, '${data[i].sDescripcion}',${data[i].iRegistroPatronal_id}, '${data[i].sRegistroPatronal}')"></i> </button>`;
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
});