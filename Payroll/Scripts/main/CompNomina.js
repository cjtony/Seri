$(function () {
    //// Delcaracion de variables
    const DropEmpresa = document.getElementById('DropEmpresa');
    const TxtAnio = document.getElementById('TxtAnio');
    const TxtPorcentaje = document.getElementById('TxtPorcentaje');
    const DropTipodePerdio = document.getElementById('DropTipodePerdio');
    const DropPerido = document.getElementById('DropPerido');
    const TxtAnio2 = document.getElementById('TxtAnio2');
    const DropTipodePerdio2 = document.getElementById('DropTipodePerdio2');
    const DropPerido2 = document.getElementById('DropPerido2');
    const btnFloBuscar = document.getElementById('btnFloBuscar');


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
        const dataSend = { IdEmpresa: DropEmpresa.value };
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

    $('#DropTipodePerdio2').change(function () {
        const dataSend = { iIdEmpresesas: DropEmpresa.value, ianio: TxtAnio2.value, iTipoPeriodo: DropTipodePerdio2.value };
      
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

    FComparativo = () => {
        if (DropEmpresa.value != 0 && TxtAnio.value != null && DropTipodePerdio.value != 0 && DropPerido.value && TxtAnio2.value != null && DropTipodePerdio2.value != 0 && DropPerido2.value != 0 ) {

            const DataSent = { CrtliIdEmpresa: DropEmpresa.value, CrtliAnio: TxtAnio.value, CrtliTipoPeriodo: DropTipodePerdio.value, CtrliPeriodo: DropPerido.value, CrtliAnio2: TxtAnio2.value, CrtliTipoPeriodo2: DropTipodePerdio2.value, CtrliPeriodo2: DropPerido2.value }
            console.log(DataSent);
            $.ajax({
                url: "../Nomina/NomiaDiferencia",
                type: "POST",
                data: DataSent,
                success: (data) => {
                    var source =
                    {
                        localdata: data,
                        datatype: "array",
                        datafields:
                            [
                                { name: 'iIdEmpresa', type: 'int' },
                                { name: 'sNombreRenglon', type: 'string' },
                                { name: 'sTotal', type: 'string' },
                                { name: 'sTotal2', type: 'int' },
                                { name: 'sTotalDif', type: 'string' },
                            ]
                    };

                    var dataAdapter = new $.jqx.dataAdapter(source);

                    $("#TpDefinicion").jqxGrid({
                        width: 980,
                        source: dataAdapter,
                        columnsresize: true,
                        autorowheight: true,
                        autoheight: true,
                        columns: [
                            { text: 'ID Empresa', datafield: 'iIdEmpresa', width: 50 },
                            { text: 'Nombre de Renglón', datafield: 'sNombreRenglon', width: 230 },
                            { text: ' ', datafield: 'sTotal', whidth: 500 },
                            { text: 'año', datafield: 'iAno', whidt: 80 },
                            { text: 'Cancelado', datafield: 'iCancelado', whidt: 50 },
                        ]
                    });
                },
            });


        }

        else {
            fshowtypealert('Comparativo Nomina', 'selecionar todos los campos', 'warning');


        }
        

    };

    btnFloBuscar.addEventListener('click',FComparativo)



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
