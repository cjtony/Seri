$(function () {

    const searchemployekey = document.getElementById('searchemployekey');
    const resultemployekey = document.getElementById('resultemployekey');
    const labsearchemp     = document.getElementById('labsearchemp');
    const filtronumber     = document.getElementById('filtronumber');
    const filtroname       = document.getElementById('filtroname');

    /* FUNCION QUE COMPRUEBA QUE TIPO DE FILTRO SE APLICARA A LA BUSQUEDA DE EMPLEADOS */
    fselectfilterdsearchemploye = () => {
        const filtered = $("input:radio[name=filtroemp]:checked").val();
        if (filtered == "numero") {
            searchemployekey.placeholder = "NOMINA DEL EMPLEADO";
            labsearchemp.textContent = "Nomina";
        } else if (filtered == "nombre") {
            searchemployekey.placeholder = "NOMBRE DEL EMPLEADO";
            labsearchemp.textContent = "Nombre";
        }
        searchemployekey.value     = "";
        resultemployekey.innerHTML = "";
        setTimeout(() => { searchemployekey.focus() }, 500);
    }

    searchemployekey.style.transition = "1s";
    searchemployekey.style.cursor     = "pointer";
    filtroname.style.cursor           = "pointer";
    filtronumber.style.cursor         = "pointer";
    document.getElementById('labelfiltronumber').style.cursor   = "pointer";
    document.getElementById('labelfiltroname').style.cursor     = "pointer";
    document.getElementById('labelsearchemployee').style.cursor = "pointer";
    searchemployekey.addEventListener('mouseover',  () => { searchemployekey.classList.add('shadow');    });
    searchemployekey.addEventListener('mouseleave', () => { searchemployekey.classList.remove('shadow'); });

    /* FUNCION QUE EJECUTA LA BUSUQEDA REAL DE LOS EMPLEADOS */
    fsearchemployes = () => {
        const filtered = $("input:radio[name=filtroemp]:checked").val();
        try {
            resultemployekey.innerHTML = '';
            document.getElementById('noresultssearchemployees').innerHTML = "";
            if (searchemployekey.value != "") {
                $.ajax({
                    url: "../SearchDataCat/SearchEmployeeDown",
                    type: "POST",
                    data: { wordsearch: searchemployekey.value, filtered: filtered.trim() },
                    success: (data) => {
                        resultemployekey.innerHTML = '';
                        document.getElementById('noresultssearchemployees').innerHTML = "";
                        if (data.length > 0) {
                            let number = 0;
                            for (let i = 0; i < data.length; i++) {
                                number += 1;
                                resultemployekey.innerHTML += `
                                    <button id="resEmployee${data[i].iIdEmpleado}" onclick="fselectemploye(${data[i].iIdEmpleado}, '${data[i].sNombreEmpleado}')" class="animated fadeIn list-group-item d-flex justify-content-between mb-1 align-items-center shadow rounded cg-back border-left-primary">
                                        ${number}. ${data[i].iIdEmpleado} - ${data[i].sNombreEmpleado}
                                       <span> Activar
                                             <i title="Editar" class="fas fa-user-check ml-2 text-primary fa-lg shadow"></i> 
                                       </span>
                                    </button>`;
                            }
                        } else {
                            document.getElementById('noresultssearchemployees').innerHTML += `
                                <div class="alert alert-danger" role="alert">
                                  <i class="fas fa-times-circle mr-2"></i> No se encontraron Empleados activos con el termino: <b class="text-uppercase">${searchemployekey.value}</b>
                                </div>
                            `;
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                resultemployekey.innerHTML = '';
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

    /* FUNCION QUE CARGA LOS DATOS DEL EMPLEADO SELECCIONADO */
    fselectemploye = (paramid, paramstr) => {
        Swal.fire({
            title: 'Esta seguro?', text: 'De activar a ' + paramstr + '?', icon: 'question',
            showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
            confirmButtonText: "Aceptar", showCancelButton: true, cancelButtonText: "Cancelar",
            allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
        }).then((acepta) => {
            if (acepta.value) {
                searchemployekey.value = '';
                resultemployekey.innerHTML = '';
                $.ajax({
                    url: "../BajasEmpleados/ReactiveEmploye",
                    type: "POST",
                    data: { keyEmployee: parseInt(paramid) },
                    beforeSend: () => {
                        //document.getElementById('resEmployee' + String(paramid)).disabled = true;
                        searchemployekey.disabled = true;
                    }, success: (request) => {
                        if (request.Bandera == true && request.MensajeError == "none") {
                            searchemployekey.disabled  = false;
                            searchemployekey.value     = "";
                            resultemployekey.innerHTML = "";
                            setTimeout(() => {
                                fShowTypeAlert('Correcto!', 'El empleado ' + String(paramstr) + ' ha sido activado.', 'success', searchemployekey, 0);
                            }, 500);
                        } else {
                            fShowTypeAlert('Error!', 'Ocurrio un error interno en la aplicación', 'error', searchemployekey, 0);
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            } else {
                console.log('Cancelado 1');
            }
        });
    }

    /* EJECUCION DE FUNCION QUE APLICA FILTRO A LA BUSQUEDA DE LOS EMPLEADOS */
    filtroname.addEventListener('click', fselectfilterdsearchemploye);
    filtronumber.addEventListener('click', fselectfilterdsearchemploye);


    /* EJECUCION DE LA FUNCION QUE HACE LA BUSQUEDA DE EMPLEADOS EN TIEMPO REAL */
    searchemployekey.addEventListener('keyup', fsearchemployes);

});