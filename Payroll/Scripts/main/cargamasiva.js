$(document).ready(function () {
    
    validateUploadFile = () => {
        var selectedFile = ($("#file-toup"))[0].files[0];
        var fileType = document.getElementById("file-type").value;

        if (!selectedFile) {
            Swal.fire({
                icon: 'warning',
                title: 'Aviso!',
                text: 'Aun no selecciona un archivo'
            });
        } else {
            var datos = new FormData();
            datos.append("fileUpload", selectedFile);
            datos.append("fileType", fileType);
            $.ajax({
                url: "../Incidencias/LoadFile",
                type: "POST",
                data: datos,
                processData: false,
                contentType: false,
                async: false,
                //contentType: "application/json; charset=utf-8",
                //dataType: "json",
                success: function (data) {
                    console.log(data);
                }
            });

        }

    }


});