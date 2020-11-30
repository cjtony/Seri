$(function () {
    //// Delcaracion de variables
    const DropEmpresa = document.getElementById('DropEmpresa');
    const TxtAnio = document.getElementById('TxtAnio');
    const TxtPorcentaje = document.getElementById('TxtPorcentaje');
    const DropTipodePerdio = document.getElementById('DropTipodePerdio');
    const DropPerido = document.getElementById('DropPerido');
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
                for (i = 0; i < data.length-1; i++) {
                    document.getElementById("DropPerido2").innerHTML += `<option value='${data[i].iPeriodo}'>${data[i].iPeriodo} </option>`;
                }

            },
        });
        
    });

 

    FComparativo = () => {
        if (DropEmpresa.value != 0 && TxtAnio.value != null && DropTipodePerdio.value != 0 && DropPerido.value  != 0 && DropPerido2.value != 0 ) {

            const DataSent = { CrtliIdEmpresa: DropEmpresa.value, CrtliAnio: TxtAnio.value, CrtliTipoPeriodo: DropTipodePerdio.value, CtrliPeriodo: DropPerido.value, CtrliPeriodo2: DropPerido2.value }
            console.log(DataSent);
            $.ajax({
                url: "../Nomina/NomiaDiferenciaxEmpresa",
                type: "POST",
                data: DataSent,
                success: (data) => {
                    console.log(data);
                    document.getElementById('content-tabledif').classList.remove("d-none");
                    var source =
                    {
                        localdata: data,
                        datatype: "array",
                        datafields:
                            [
                                { name: 'iIdEmpresa', type: 'int' },
                                { name: 'sNombreRenglon', type: 'string' },
                                { name: 'sTotal', type: 'string' },
                                { name: 'sTotal2', type: 'string' },
                                { name: 'sTotalDif', type: 'string' },
                                { name: 'iNoEmpleado', type: 'int'},
                            ]
                    };

                    var dataAdapter = new $.jqx.dataAdapter(source);

                    $("#TableDif").jqxDataTable({
                        width: 980,
                        source: dataAdapter,
                        pageable: true,
                        sortable: true,
                        altRows: true,             
                        columns: [
                            { text: 'ID Empresa', datafield: 'iIdEmpresa', width: 50 },
                            { text: 'Tipo de Liquido', datafield: 'sNombreRenglon', width: 200 },
                            { text: 'Periodo Actual', datafield: 'sTotal', whidth: 80 },
                            { text: 'Perido Anterior', datafield: 'sTotal2', whidt: 80 },
                            { text: 'Diferencia', datafield: 'sTotalDif', whidt: 100 },
                            { text: 'No Empleados', datafield: 'iNoEmpleado',whidt:100},
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
