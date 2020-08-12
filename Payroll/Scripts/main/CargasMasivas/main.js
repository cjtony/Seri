$(function () {


    /*
     * CONSTANTES
     */

    const fileUploadMasiveUp  = document.getElementById('file-upload-masive-up');
    const btnSaveFileMasiveUp = document.getElementById('btn-save-file-masive-up');
    const divShowLoadFile     = document.getElementById('div-show-load-file');
    const divContentLoadInfo1 = document.getElementById('div-content-load-info-1');
    const divContentLoadInfo2 = document.getElementById('div-content-load-info-2');
    const downloadFileLog     = document.getElementById('download-file-log');

    document.getElementById('label-file-upload-mu').style.cursor = "pointer";
    btnSaveFileMasiveUp.disabled = true;


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
        const nameFile = fileUploadMasiveUp.files[0].name;
        const arrNFile = nameFile.split(".");
        const extValid = "xlsx";
        if (extValid == arrNFile[1]) {
            document.getElementById('name-file-up-masive').innerHTML = `<i class="fas fa-file-excel mr-2"></i>` + nameFile;
            document.getElementById('name-file-up-masive').classList.add('fadeInDown');
            document.getElementById('name-file-up-masive').classList.add('text-primary');
            btnSaveFileMasiveUp.disabled = false;
        } else {
            document.getElementById('name-file-up-masive').innerHTML = `El archivo con extension .${arrNFile[1]} no es valido.`;
            document.getElementById('name-file-up-masive').classList.remove('text-primary');
            document.getElementById('name-file-up-masive').classList.add('text-danger');
            fileUploadMasiveUp.value = "";
            btnSaveFileMasiveUp.disabled = true;
        }
        //console.log(fileUploadMasiveUp.files[0]);
    }

    // Funcion que carga el archivo de carga masiva
    fUploadFileMasiveUpEmployees = () => {
        try {
            const valueInptFile = fileUploadMasiveUp.value;
            const allowedExtensions = /(.xlsx)$/i;
            if (valueInptFile != "") {
                if (!allowedExtensions.exec(valueInptFile)) {
                    fileUploadMasiveUp.value = "";
                    fShowTypeAlert("Atencion", "El archivo no es valido", "warning", fileUploadMasiveUp, 0);
                } else {
                    const selectFile = ($("#file-upload-masive-up"))[0].files[0];
                    let dataString   = new FormData();
                    dataString.append("fileUpload", selectFile);
                    dataString.append("typeFile", "CARGA");
                    $.ajax({
                        url: "/MassiveUpsAndDowns/UploadFileMasiveUpEmployees",
                        type: "POST",
                        data: dataString,
                        contentType: false,
                        processData: false,
                        async: false,
                        beforeSend: () => {
                            btnSaveFileMasiveUp.disabled = true;
                            fileUploadMasiveUp.disabled  = true;
                            divShowLoadFile.classList.remove('fadeOut');
                            divShowLoadFile.classList.add('fadeIn');
                            divShowLoadFile.innerHTML = `
                                <div class="spinner-border text-primary" role="status">
                                  <span class="sr-only">Cargando...</span>
                                </div>
                                <h6 class="text-primary font-weight-bold mt-2">Cargando...</h6>
                            `;
                        }, success: (data) => {
                            console.log(data);
                            setTimeout(() => {
                                divShowLoadFile.classList.remove('fadeIn');
                                divShowLoadFile.classList.add('fadeOut');
                                setTimeout(() => { divShowLoadFile.innerHTML = ""; }, 500);
                                if (data.Bandera == true && data.MensajeError == "none" && data.Log == false) {
                                    setTimeout(() => {
                                        divContentLoadInfo1.classList.add('fadeInDown');
                                        divContentLoadInfo1.innerHTML = `
                                            <div class="card shadow p-2 border-left-primary">
                                                <b class="card-title text-center font-weight-bold text-success mt-3 mb-3"> <i class="fas fa-check-circle mr-2"></i> Carga correcta! </b>
                                                <hr class="mb-0 mt-0" />
                                                <p class="mt-0 text-center font-weight-bold text-info mt-3">Los datos estan siendo insertados. </p>
                                            </div>
                                        `;
                                        divContentLoadInfo2.classList.add('fadeInDown');
                                        divContentLoadInfo2.innerHTML = `
                                            <div class="card shadow p-2 border-right-primary">
                                                <b class="card-title text-center font-weight-bold text-success mt-3 mb-3"> <i class="fas fa-file-excel mr-2"></i> ${data.ArchivoCarga} </b>
                                                <hr class="mb-0 mt-0" />
                                                <p class="mt-0 text-center font-weight-bold text-info mt-3">Estado del registro: 
                                                    <span class="badge badge-info p-1" id="status-load">En proceso...</span>
                                                </p>
                                            </div>
                                        `;
                                        // Ejecutar funcion para la insercion
                                        fInsertDataFileMasiveUps(String(data.ArchivoCarga), parseInt(data.Llave));
                                    }, 1000);
                                } else {
                                    setTimeout(() => {
                                        divContentLoadInfo1.classList.add('fadeInDown');
                                        divContentLoadInfo1.innerHTML = `
                                            <div class="card shadow p-2 border-left-primary">
                                                <b class="card-title text-center font-weight-bold text-danger mt-3 mb-3"> <i class="fas fa-times-circle mr-2"></i> Error en carga </b>
                                                <hr class="mb-0 mt-0" />
                                                <p class="mt-0 text-center font-weight-bold text-info mt-3"> <i class="fas fa-laptop-code mr-2"></i> Contacte al área de TI </p>
                                            </div>
                                        `;
                                        divContentLoadInfo2.classList.add('fadeInDown');
                                        divContentLoadInfo2.innerHTML = `
                                            <div class="card shadow p-2 border-right-primary">
                                                <b class="card-title text-center font-weight-bold text-danger mt-3 mb-3"> <i class="fas fa-file-excel mr-2"></i> ${data.ArchivoCarga} </b>
                                                <hr class="mb-0 mt-0" />
                                                <p class="mt-0 text-center font-weight-bold text-info mt-3">Estado del registro: 
                                                    <span class="badge badge-danger p-1" id="status-load">Sin procesar...</span>
                                                </p>
                                            </div>
                                        `;
                                        btnSaveFileMasiveUp.disabled = false;
                                        fileUploadMasiveUp.disabled = false;
                                    }, 1000);
                                }
                            }, 2000);
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    });
                }
            } else {
                fShowTypeAlert("Atencion", "Selecciona un archivo", "warning", fileUploadMasiveUp, 0);
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

    //const intervalo = setInterval(() => { console.log('d'); }, 2000);

    // Funcion que realiza la insercion masiva
    fInsertDataFileMasiveUps = (paramstr, paramint) => {
        try {
            if (String(paramstr) != "" && parseInt(paramint) != 0) {
                const dataSend = { nameFile: String(paramstr), keyFile: parseInt(paramint) };
                $.ajax({
                    url: "../MassiveUpsAndDowns/InsertDataFileMasiveUps",
                    type: "POST",
                    data: dataSend,
                    beforeSend: () => {

                    }, success: (data) => {
                        fileUploadMasiveUp.disabled  = false;
                        btnSaveFileMasiveUp.disabled = false;
                        if (data.BanderaBusqueda == true && data.Bandera == true) {
                            if (data.BanderaH == true && data.BanderaC == true) {
                                if (data.BanderaInsercion == true) {
                                    document.getElementById('status-load').classList.remove('badge-info');
                                    document.getElementById('status-load').classList.add('badge-success');
                                    document.getElementById('status-load').textContent = "Terminado!";
                                }
                                document.getElementById('div-content-download-log-file').innerHTML += `
                                    <div class="card border-left-success shadow h-100 py-2 animated fadeIn">
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
                                                    <a href="/Content/FilesUps/${data.FolderLog}/${data.ArchivoLog}" download="${data.ArchivoLog}"><i class="fas fa-download fa-2x text-gray-300"></i></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>`;
                                //downloadFileLog.setAttribute("href", "/Content/FilesUps/" + data.FolderLog + "/" + data.ArchivoLog);
                                //downloadFileLog.setAttribute("download", data.ArchivoLog);
                            } else {
                                alert('El nombre de la hoja es incorrecto, comprube que los codigos sean correctos');
                            }
                        } else {
                            alert('No se encontro el archivo para la carga');
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

    /*
     * EJECUCION DE FUNCIONES
     */

    fileUploadMasiveUp.addEventListener('click', () => {
        document.getElementById('name-file-up-masive').classList.remove('fadeInDown', 'text-danger');
    });

    fileUploadMasiveUp.addEventListener('change', fSelectValueFile);

    btnSaveFileMasiveUp.addEventListener('click', fUploadFileMasiveUpEmployees);

});