$(function () {


    /*
     * CONSTANTES
     */

    const fileUploadMasiveUp = document.getElementById('file-upload-masive-up');
    const btnSaveFileMasiveUp = document.getElementById('btn-save-file-masive-up');

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
                        success: (data) => {
                            console.log(data);
                            if (data.Bandera == true && data.MensajeError == "none" && data.Log == false) {

                            } else {

                            }
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

    /*
     * EJECUCION DE FUNCIONES
     */

    fileUploadMasiveUp.addEventListener('click', () => {
        document.getElementById('name-file-up-masive').classList.remove('fadeInDown', 'text-danger');
    });

    fileUploadMasiveUp.addEventListener('change', fSelectValueFile);

    btnSaveFileMasiveUp.addEventListener('click', fUploadFileMasiveUpEmployees);

});