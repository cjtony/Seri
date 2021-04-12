$(function () {

    // Declaracion de variables

    const anoxml = document.getElementById('anoxml');
    const TipoPeriodoxml = document.getElementById('TipoPeriodoxml');
    const Periodoxml = document.getElementById('Periodoxml');
    const fileUpload = document.getElementById('fileUpload');
    var Path;
    const  versionxml = "12";
    /// carga el nombre del archivo en upfile

    $('.custom-file input').change(function () {
        
        var filePath = $(this).val();
        console.log(filePath);
        var files = [];
        for (var i = 0; i < $(this)[0].files.length; i++) {
            files.push($(this)[0].files[i].name);
            
            
        }
        $(this).next('.custom-file-label').html(files.join(', '));

    });

    // declaracion de boton de timbrado
    btnTiembrar = document.getElementById('btnTiembrar');

    
    $('#btnTiembrar').on('click', function () {

        var aniox = anoxml.value;
        var TipPeriodox = TipoPeriodoxml.value;
        var Peridox = Periodoxml.value;
        var versionx = versionxml;
       
        if (aniox.value != "" && TipPeriodox != "" && Peridox !="" && versionx !="" ) {        
            var selectFile = $("#fileUpload")[0].files[0];
            var dataString = new FormData();
            var NomArch = selectFile.name;
            var UbiArch = selectFile.Path;
            console.log(UbiArch);
            separador = ".",
            limite = 2,
            arreglosubcadena = NomArch.split(separador, limite);
            dataString.append("fileUpload", selectFile);
            if (arreglosubcadena[1] == "zip") {
                $.ajax({
                    url: "../Empleados/LoadFile",
                    type: "POST",
                    data: dataString,
                    contentType: false,
                    processData: false,
                    async: false,
                    success: function (data) {
                        if (typeof data.Value != "undefined") {
                            fshowtypealert('Timbrado XML', data.Message, 'warning');
                            const dataSend = { Anio: aniox, TipoPeriodo: TipPeriodox, Perido: Peridox, Version: versionx, NomArchivo: NomArch };
                            console.log("dato:" + dataSend);
                            $.ajax({
                                url: "../Empleados/TimbXML",
                                type: "POST",
                                data: dataSend,
                                success: (data) => {
                                    if (data[0].sMensaje !=null) {
                                        var url = '\\Archivos\\Pdfzio.zip';
                                        window.open(url);
                                    }


                                }
                            });

                        }
                        else {
                            fshowtypealert('Timbrado XML', "Error no identificado en la carga del archivo Winzip", 'warning');
                        }

                    },
                    Error: function (data) {

                    }
                });
            }
            if (arreglosubcadena[1] != "zip") {

                console.log('Favor de subir archivos ZIP');
                fshowtypealert('Timbrado XML', 'El archivo que seleccionó no es un winzip favor de seleccionar uno ', 'warning');
            }

        }

        else {
            console.log('error');
            fshowtypealert('Timbrado XML', ' El año,  Tipo de periodo y periodo son campos obligatorio', 'warning');
        }

    });

    /// Validaciones
    $("#anoxml").keyup(function () {
        this.value = (this.value + '').replace(/[^0-9]/g, '');
    });
    $("#TipoPeriodoxml").keyup(function () {
        this.value = (this.value + '').replace(/[^0-9]/g, '');
    });
    $("#Periodoxml").keyup(function () {
        this.value = (this.value + '').replace(/[^0-9]/g, '');
    });
    /* FUNCION QUE MUESTRA ALERTAS */
    fshowtypealert = (title, text, icon) => {
        Swal.fire({
            title: title, text: text, icon: icon,
            showClass: { popup: 'animated fadeInDown faster' },
            hideClass: { popup: 'animated fadeOutUp faster' },
            confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
        }).then((acepta) => {

            //  Nombrede.value       = '';
            // Descripcionde.value  = '';
            //iAnode.value         = '';
            //cande.value          = '';
            //$("html, body").animate({
            //    scrollTop: $(`#${element.id}`).offset().top - 50
            //}, 1000);
            //if (clear == 1) {
            //    setTimeout(() => {
            //        element.focus();
            //        setTimeout(() => { element.value = ""; }, 300);
            //    }, 1200);
            //} else {
            //    setTimeout(() => {
            //        element.focus();
            //    }, 1200);
            //}
        });
    };

});