$(function () {
    //// Delcaracion de variables
    const DropEmpresa = document.getElementById('DropEmpresa');
    const TxtAnio = document.getElementById('TxtAnio');
    const TxtPorcentaje = document.getElementById('TxtPorcentaje');
    const DropTipodePerdio = document.getElementById('DropTipodePerdio');
    const DropPerido = document.getElementById('DropPerido');
    const DropEmpresa2 = document.getElementById('DropEmpresa2');
    const TxtAnio2 = document.getElementById('TxtAnio2');
    const TxtPorcentaje2 = document.getElementById('TxtPorcentaje2');
    const DropTipodePerdio2 = document.getElementById('DropTipodePerdio2');
    const DropPerido2 = document.getElementById('DropPerido2');


    FListadoEmpresa = () => {
        $("#DropEmpresa").empty();
        $('#DropEmpresa').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisEmpresas",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DropEmpresa").innerHTML += `<option value='${data[i].iIdEmpresa}'>${data[i].sNombreEmpresa}</option>`;
                    document.getElementById("DropEmpresa2").innerHTML += `<option value='${data[i].iIdEmpresa}'>${data[i].sNombreEmpresa}</option>`;

                }
            }
        });
    };
    FListadoEmpresa();

    FListTipoDePeriodo = () => {
        const dataSend = { IdEmpresa: DropEmpresa.value };
        $("#DropTipodePerdio").empty();
        $('#DropTipodePerdio').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisTipPeriodo",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DropTipodePerdio").innerHTML += `<option value='${data[i].iId}'>${data[i].sValor}</option>`;
                }
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

    };
    FListTipoDePeriodo();
    $('#DropEmpresa').change(function () {
        FListTipoDePeriodo();
    });

    $('#DropTipodePerdio').change(function () {
        const dataSend = { iIdEmpresesas: DropEmpresa.value, ianio: TxtAnio.value, iTipoPeriodo: DropTipodePerdio.value };
        console.log('periodo');
        $("#DropPerido").empty();
        $('#DropPerido').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/ListPeriodoComp",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DropPerido").innerHTML += `<option value='${data[i].iPeriodo}'>${data[i].iPeriodo} </option>`;
                }
            },
        });
        
    });


    FListTipoDePeriodo2 = () => {
        const dataSend = { IdEmpresa: DropEmpresa2.value };
        $("#DropTipodePerdio2").empty();
        $('#DropTipodePerdio2').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisTipPeriodo",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DropTipodePerdio2").innerHTML += `<option value='${data[i].iId}'>${data[i].sValor}</option>`;
                }
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

    };
    FListTipoDePeriodo2();
    $('#DropEmpresa2').change(function () {
        FListTipoDePeriodo2();
    });
    $('#DropTipodePerdio2').change(function () {
        const dataSend = { iIdEmpresesas: DropEmpresa2.value, ianio: TxtAnio2.value, iTipoPeriodo: DropTipodePerdio2.value };
        console.log('periodo');
        $("#DropPerido").empty();
        $('#DropPerido').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/ListPeriodoComp",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DropPerido2").innerHTML += `<option value='${data[i].iPeriodo}'>${data[i].iPeriodo} </option>`;
                }
            },
        });

    });

    /* FUNCION QUE MUESTRA ALERTAS */
    fshowtypealert = (title, text, icon) => {
        Swal.fire({
            title: title, text: text, icon: icon,
            showClass: { popup: 'animated fadeInDown faster' },
            hideClass: { popup: 'animated fadeOutUp faster' },
            confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
        }).then((acepta) => {

        });
    };

});
