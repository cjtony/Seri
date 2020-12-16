$(document).ready(function () {
    
    beforeValidarFile = () => {

        $("#btnCargaMasiva").html("<span class='spinner-grow spinner-grow-sm' role='status' aria-hidden='true'></span> Cargando...");
        document.getElementById("btnCargaMasiva").disabled = true;
        //$("#loadingCargaMasiva").modal("show");

        setTimeout(function () {
            validateUploadFile();
        }, 5000);
    }

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
                        var btn = "<div class='col-md-12 d-flex justify-content-center'><a class=' btn btn-success btn-sm btn-icon-split' href='" + data[1] + "' download><span class='icon'> <i class='fas fa-download text-white'></i> </span><span class='text'> Descargar archivo log .txt </span></a></div>";
                        var txt = "<div class='alert alert-warning col-md-12 my-3' role='alert' id='alert-validation'>" +
                            "<button type='button' class='close' data-dismiss='alert' aria-label='Close'> <span aria-hidden='true'>&times;</span></button>" +
                            "<strong> Atención </strong> Hubo errores en el archivo Layout de carga.\n Descargue en archivo log con los errores " + btn + "</div >";

                        $("#collapse-validation-cm").html(txt);
                        $("#collapse-validation-cm").collapse("show");
                        $("a.btn-success").focus();

                        $("#file-toup").val('');

                    }
                    if (data[0] == "1") {
                        var txt = "<div class='alert alert-success col-md-12 my-3' role='alert' id='alert-validation'>" +
                            "<button type='button' class='close' data-dismiss='alert' aria-label='Close'> <span aria-hidden='true'>&times;</span></button>" +
                            "<strong> Listo </strong> " + data[1] + " </div >";
                        $("#collapse-validation-cm").html(txt);
                        $("#collapse-validation-cm").collapse("show");
                        //setTimeout(function () {
                        //    $("#alert-validation").remove();
                        //}, 10000);

                        $("#file-toup").val('');
                    }

                    //$("#btnCargaMasiva").removeClass("invisible");
                }
            });

        }
        document.getElementById("btnCargaMasiva").disabled = false;
        $("#btnCargaMasiva").html("<i class='fas fa-check-circle mr-2'></i> Cargar archivo");

        //$("#btnCargaMasiva").removeClass("invisible");
        //$("#loadingCargaMasiva").modal("hide");
    }

    $("#btnDownLoadCM").mouseenter(function () {
        //alert("ENTRA");
        $("#btnDownLoadCM").append("<span> Descargar Layout </span>");
        //document.getElementById("btnDownLoadCM").style.width = 50;
    });
    $("#btnDownLoadCM").mouseleave(function () {
        //alert("SALE");
        $("#btnDownLoadCM").find("span").remove();
        //document.getElementById("btnDownLoadCM").style.width = 30;
    });

    loadLayout = () => {
        $.ajax({
            url: "../Incidencias/loadLayout",
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                console.log(data[0]);
                //$("#btnDownLoadCM").attr("href", data[0].substring(2, data[0].Length));
            }
        });
    }
    loadLayout();
});