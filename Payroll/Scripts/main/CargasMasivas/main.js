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