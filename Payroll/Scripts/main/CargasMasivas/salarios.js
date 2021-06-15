$(function () {

    /*
     * CONSTANTES
     */

    const fileUploadMasiveUpSalary = document.getElementById('file-upload-salary-up');
    const btnSaveFileMasiveUpSalary = document.getElementById('btn-save-file-salary-up');
    const divShowLoadFileSalary = document.getElementById('div-show-load-file-salary');
    const divContentLoadInfo1Salary = document.getElementById('div-content-load-info-1-salary');
    const divContentLoadInfo2Salary = document.getElementById('div-content-load-info-2-salary');

    document.getElementById('label-file-upload-mu-salary').style.cursor = "pointer";
    btnSaveFileMasiveUpSalary.disabled = true;


    /*
    * FUNCIONES
    */

    // Funcion que captura los errores de ajax que se puedan generar
    fcaptureaerrorsajax = (jq, exc) => {
        let msg = "";
        if (jq.status === 0) {
            msg = "No conectado. \n Verifica tu conexión de red.";
        } else if (jq.status === 404) {
            msg = 'Página solicitada no encontrada. [404]';
        } else if (jq.status == 500) {
            msg = 'Error interno del servidor [500].';
        } else if (exc === 'parsererror') {
            msg = 'El análisis JSON solicitado falló.';
        } else if (exc === 'timeout') {
            msg = 'Error de tiempo de espera.';
        } else if (exc === 'abort') {
            msg = 'Solicitud de Ajax abortada.';
        } else {
            msg = 'Error no detectado.\n' + jq.responseText;
        }
        console.log(msg);
    }

    // Funcion que muestra alertas de forma dinamica
    fShowTypeAlert = (title, text, icon, element, clear) => {
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
            } else if (clear == 2) {
                setTimeout(() => { element.focus(); }, 1200);
            }
        });
    }

    // Funcion que obtiene el valor del archivo seleccionado
    fSelectValueFile = () => {
        const nameFile = fileUploadMasiveUpSalary.files[0].name;
        const arrNFile = nameFile.split(".");
        const extValid = "xlsx";
        if (extValid == arrNFile[1]) {
            document.getElementById('name-file-up-salary').innerHTML = `<i class="fas fa-file-excel mr-2"></i>` + nameFile;
            document.getElementById('name-file-up-salary').classList.add('fadeInDown');
            document.getElementById('name-file-up-salary').classList.add('text-primary');
            $("html, body").animate({ scrollTop: $(`#${divShowLoadFileSalary.id}`).offset().top - 50 }, 1000);
            btnSaveFileMasiveUpSalary.disabled = false;
        } else {
            document.getElementById('name-file-up-salary').innerHTML = `El archivo con extension .${arrNFile[1]} no es valido.`;
            document.getElementById('name-file-up-salary').classList.remove('text-primary');
            document.getElementById('name-file-up-salary').classList.add('text-danger');
            fileUploadMasiveUpSalary.value = "";
            btnSaveFileMasiveUpSalary.disabled = true;
        }
        //console.log(fileUploadMasiveUpSalary.files[0]);
    }

    // Funcion que carga el archivo de carga masiva
    fUploadFileMasiveUpSalary = () => {
        try {
            const valueInptFile = fileUploadMasiveUpSalary.value;
            const allowedExtensions = /(.xlsx)$/i;
            if (valueInptFile != "") {
                if (!allowedExtensions.exec(valueInptFile)) {
                    fileUploadMasiveUpSalary.value = "";
                    fShowTypeAlert("Atencion", "El archivo no es valido", "warning", fileUploadMasiveUpSalary, 0);
                } else {
                    const selectFile = ($("#file-upload-salary-up"))[0].files[0];
                    let dataString = new FormData();
                    dataString.append("fileUpload", selectFile);
                    dataString.append("typeFile", "SALARIO");
                    $.ajax({
                        url: "/MassiveUpsAndDowns/UploadFileMasiveUpSalary",
                        type: "POST",
                        data: dataString,
                        contentType: false,
                        processData: false,
                        async: false,
                        beforeSend: () => {
                            btnSaveFileMasiveUpSalary.disabled = true;
                            fileUploadMasiveUpSalary.disabled = true;
                            divShowLoadFileSalary.classList.remove('fadeOut');
                            divShowLoadFileSalary.classList.add('fadeIn');
                            divShowLoadFileSalary.innerHTML = `
                                <div class="spinner-border text-primary" role="status">
                                  <span class="sr-only">Cargando...</span>
                                </div>
                                <h6 class="text-primary font-weight-bold mt-2">Cargando...</h6>
                            `;
                        }, success: (data) => {
                            console.log(data);
                            setTimeout(() => {
                                divShowLoadFileSalary.classList.remove('fadeIn');
                                divShowLoadFileSalary.classList.add('fadeOut');
                                setTimeout(() => { divShowLoadFileSalary.innerHTML = ""; }, 500);
                                if (data.Bandera == true && data.MensajeError == "none" && data.Log == false) {
                                    $("html, body").animate({ scrollTop: $(`#${divContentLoadInfo1Salary.id}`).offset().top - 50 }, 1000);
                                    setTimeout(() => {
                                        divContentLoadInfo1Salary.classList.add('fadeInDown');
                                        divContentLoadInfo1Salary.innerHTML = `
                                            <div class="card shadow p-2 border-left-primary">
                                                <b class="card-title text-center font-weight-bold text-success mt-3 mb-3"> <i class="fas fa-check-circle mr-2"></i> Carga correcta! </b>
                                                <hr class="mb-0 mt-0" />
                                                <p class="mt-0 text-center font-weight-bold text-info mt-3">Los datos estan siendo insertados. </p>
                                            </div>
                                        `;
                                        divContentLoadInfo2Salary.classList.add('fadeInDown');
                                        divContentLoadInfo2Salary.innerHTML = `
                                            <div class="card shadow p-2 border-right-primary">
                                                <b class="card-title text-center font-weight-bold text-success mt-3 mb-3"> <i class="fas fa-file-excel mr-2"></i> ${data.ArchivoCarga} </b>
                                                <hr class="mb-0 mt-0" />
                                                <p class="mt-0 text-center font-weight-bold text-info mt-3">Estado del registro: 
                                                    <span class="badge badge-info p-1" id="status-load">En proceso...</span>
                                                </p>
                                            </div>
                                        `;
                                        // Ejecutar funcion para la insercion
                                        setTimeout(() => {
                                            fInsertDataFileMasiveUpsSalary(String(data.ArchivoCarga), parseInt(data.Llave));
                                        }, 2000);
                                    }, 1000);
                                } else {
                                    setTimeout(() => {
                                        divContentLoadInfo1Salary.classList.add('fadeInDown');
                                        divContentLoadInfo1Salary.innerHTML = `
                                            <div class="card shadow p-2 border-left-primary">
                                                <b class="card-title text-center font-weight-bold text-danger mt-3 mb-3"> <i class="fas fa-times-circle mr-2"></i> Error en carga </b>
                                                <hr class="mb-0 mt-0" />
                                                <p class="mt-0 text-center font-weight-bold text-info mt-3"> <i class="fas fa-laptop-code mr-2"></i> Contacte al área de TI </p>
                                            </div>
                                        `;
                                        divContentLoadInfo2Salary.classList.add('fadeInDown');
                                        divContentLoadInfo2Salary.innerHTML = `
                                            <div class="card shadow p-2 border-right-primary">
                                                <b class="card-title text-center font-weight-bold text-danger mt-3 mb-3"> <i class="fas fa-file-excel mr-2"></i> ${data.ArchivoCarga} </b>
                                                <hr class="mb-0 mt-0" />
                                                <p class="mt-0 text-center font-weight-bold text-info mt-3">Estado del registro: 
                                                    <span class="badge badge-danger p-1" id="status-load">Sin procesar...</span>
                                                </p>
                                            </div>
                                        `;
                                        btnSaveFileMasiveUpSalary.disabled = false;
                                        fileUploadMasiveUpSalary.disabled = false;
                                    }, 1000);
                                }
                            }, 2000);
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    });
                }
            } else {
                fShowTypeAlert("Atencion", "Selecciona un archivo", "warning", fileUploadMasiveUpSalary, 0);
            }
        } catch (error) {
            if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }

    // Funcion que realiza la insercion masiva
    fInsertDataFileMasiveUpsSalary = (paramstr, paramint) => {
        try {
            if (String(paramstr) != "" && parseInt(paramint) != 0) {
                const dataSend = { nameFile: String(paramstr), keyFile: parseInt(paramint) };
                $.ajax({
                    url: "../MassiveUpsAndDowns/InsertDataFileMasiveUpsSalary",
                    type: "POST",
                    data: dataSend,
                    beforeSend: () => {

                    }, success: (data) => {
                        //fileUploadMasiveUp.disabled  = false;
                        //btnSaveFileMasiveUp.disabled = false;
                        if (data.BanderaBusqueda == true && data.Bandera == true) {
                            if (data.BanderaH == true && data.BanderaC == true) {
                                $("html, body").animate({ scrollTop: $('#div-content-download-log-file-salary').offset().top - 10 }, 1000);
                                if (data.BanderaInsercion == true) {
                                    document.getElementById('status-load').classList.remove('badge-info');
                                    document.getElementById('status-load').classList.add('badge-success');
                                    document.getElementById('status-load').textContent = "Terminado!";
                                } else {
                                    document.getElementById('status-load').classList.remove('badge-info');
                                    document.getElementById('status-load').classList.add('badge-danger');
                                    document.getElementById('status-load').textContent = "Error!";
                                }
                                document.getElementById('div-content-download-log-file-salary').innerHTML += `
                                    <div class="card border-left-success shadow h-100 py-2 animated fadeInDown delay-1s">
                                        <div class="card-body">
                                            <div class="row no-gutters align-items-center">
                                                <div class="col mr-2">
                                                    <div class="text-xs font-weight-bold text-success text-uppercase mb-1">${data.ArchivoLog}</div>
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col-auto">
                                                            <div class="h5 mb-0 mr-3 font-weight-bold text-gray-800">100%</div>
                                                        </div>
                                                        <div class="col">
                                                            <div class="progress progress-sm mr-2">
                                                                <div class="progress-bar bg-success" role="progressbar" style="width: 100%" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-auto">
                                                    <a href="/Content/FilesUpsSalary/${data.FolderLog}/${data.ArchivoLog}" download="${data.ArchivoLog}"><i class="fas fa-download fa-2x text-gray-300"></i></a>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-12">
                                                    <ul class="list-group">
                                                      <li class="list-group-item d-flex justify-content-between align-items-center">
                                                        <b> <i class="fas fa-file-alt mr-2 text-primary"></i> Registros indicados</b>
                                                        <span class="badge badge-primary badge-pill">${data.FilasIn}</span>
                                                      </li>
                                                      <li class="list-group-item d-flex justify-content-between align-items-center">
                                                        <b> <i class="fas fa-check-circle mr-2 text-success"></i> Filas insertadas</b>
                                                        <span class="badge badge-success badge-pill">${data.FilasOk}</span>
                                                      </li>
                                                      <li class="list-group-item d-flex justify-content-between align-items-center">
                                                        <b> <i class="fas fa-times-circle mr-2 text-danger"></i> Filas con errores</b>
                                                        <span class="badge badge-danger badge-pill">${data.FilasEr}</span>
                                                      </li>
                                                    </ul>
                                                </div>
                                                <div class="col-md-6 offset-3 mt-3 animated fadeIn delay-2s">
                                                    <button onclick="fClearFormFileUpSalary();" class="btn btn-sm btn-block btn-primary shadow rounded"> <i class="fas fa-undo mr-2"></i> Limpiar formulario </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>`;
                                //downloadFileLog.setAttribute("href", "/Content/FilesUps/" + data.FolderLog + "/" + data.ArchivoLog);
                                //downloadFileLog.setAttribute("download", data.ArchivoLog);
                            } else {
                                alert('El nombre de la hoja es incorrecto, comprube que los codigos sean correctos');
                                fClearFormFileUpSalary();
                            }
                        } else {
                            alert('No se encontro el archivo para la carga');
                            fClearFormFileUpSalary();
                        }
                        console.log(data);
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
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }

    // Funcion que limpia el formulario
    fClearFormFileUpSalary = () => {
        document.getElementById('div-content-download-log-file-salary').classList.add('animated', 'fadeOut');
        setTimeout(() => {
            document.getElementById('div-content-download-log-file-salary').innerHTML = "";
            document.getElementById('div-content-download-log-file-salary').classList.remove("animated", "fadeOut", "delay-1s");
            divContentLoadInfo1Salary.classList.remove('fadeInDown');
            divContentLoadInfo2Salary.classList.remove('fadeInDown');
            divContentLoadInfo1Salary.classList.add('fadeOut');
            divContentLoadInfo2Salary.classList.add('fadeOut');
            setTimeout(() => {
                divContentLoadInfo1Salary.innerHTML = "";
                divContentLoadInfo2Salary.innerHTML = "";
                divContentLoadInfo1Salary.classList.remove('fadeOut');
                divContentLoadInfo2Salary.classList.remove('fadeOut');
                fileUploadMasiveUpSalary.value = "";
                document.getElementById('name-file-up-salary').innerHTML = "";
                $("html, body").animate({ scrollTop: $('#body-init').offset().top - 50 }, 1000);
                fileUploadMasiveUpSalary.disabled = false;
                btnSaveFileMasiveUpSalary.disabled = false;
            }, 1000);
        }, 1000);
    }

    /*
     * EJECUCION DE FUNCIONES
     */

    fileUploadMasiveUpSalary.addEventListener('click', () => {
        document.getElementById('name-file-up-salary').classList.remove('fadeInDown', 'text-danger');
    });

    fileUploadMasiveUpSalary.addEventListener('change', fSelectValueFile);

    btnSaveFileMasiveUpSalary.addEventListener('click', fUploadFileMasiveUpSalary);


});