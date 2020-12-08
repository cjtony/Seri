﻿$(document).ready(function () {

    validateUploadFile = () => {
        var selectedFile = $("#file-toup").prop("files")[0];
        var selectedF = ($("#file-toup"))[0].files[0];
        var fileType = document.getElementById("file-type").value;

        if (!selectedF) {
            Swal.fire({
                icon: 'warning',
                title: 'Aviso!',
                text: 'Aun no selecciona un archivo'
            });
        } else {
            var datos = new FormData();
            datos.append("fileUpload", selectedF);
            datos.append("fileType", fileType);

            console.log(selectedF);

            $.ajax({
                url: "../Incidencias/LoadFile",
                type: "POST",
                data: datos,
                processData: false,
                contentType: false,
                async: false,
                success: function (data) {
                    if (data[0] == "0") {
                        var txt = "<div class='alert alert-warning col-md-12 my-3' role='alert' id='alert-validation'>" +
                            "<button type='button' class='close' data-dismiss='alert' aria-label='Close'> <span aria-hidden='true'>&times;</span></button>" +
                            "<strong> Atención </strong> Hubo errores en el archivo Layout de carga.\n Descargue en archivo log con los errores </div >";
                        var btn = "<div class='col-md-12 d-flex justify-content-center'><a class=' btn btn-success btn-sm btn-icon-split' href='" + data[1] + "' download><span class='icon'> <i class='fas fa-download text-white'></i> </span><span class='text'> Descargar archivo log .txt </span></a></div>";
                        $("#collapse-validation-cm").html(txt + btn);
                        $("#collapse-validation-cm").collapse("show");
                        $("a.btn-success").focus();

                        $("#file-toup").val('');
                        
                    }
                    if (data[0] == "1") {
                        var txt = "<div class='alert alert-success col-md-12 my-3' role='alert' id='alert-validation'>" +
                            "<button type='button' class='close' data-dismiss='alert' aria-label='Close'> <span aria-hidden='true'>&times;</span></button>" +
                            "<strong> Carga completa </strong> " + data[1] + " </div >";
                        $("#collapse-validation-cm").html(txt);
                        $("#collapse-validation-cm").collapse("show");
                        setTimeout(function () {
                            $("#alert-validation").remove();
                        }, 10000);

                        $("#file-toup").val('');
                    }


                }
            });

        }

    }


});