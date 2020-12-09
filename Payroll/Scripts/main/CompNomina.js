$(function () {
    //// Delcaracion de variables
    const DropEmpresa = document.getElementById('DropEmpresa');
    const TxtAnio = document.getElementById('TxtAnio');
    const TxtPorcentaje = document.getElementById('TxtPorcentaje');
    const DropTipodePerdio = document.getElementById('DropTipodePerdio');
    const DropPerido = document.getElementById('DropPerido');
    const DropPerido2 = document.getElementById('DropPerido2');
    const btnFloBuscar = document.getElementById('btnFloBuscar');
    const btnLimpiarCamp = document.getElementById('btnLimpiarCamp');

   
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
                                { name: 'sTotal', type: 'float' },
                                { name: 'sTotal2', type: 'float' },
                                { name: 'sTotalDif', type: 'float' },
                                { name: 'iNoEmpleado', type: 'int'},
                            ],
                       
                    };
                    var cellClass = function (row, dataField, cellText, rowData) {
                        var cellValue = rowData[dataField];

                        switch (dataField) {
                            case "sTotalDif":
                                if (cellValue > 0) {
                                    return "min";
                                }
                                return "max";
                            case "sTotal2":
                                if (cellValue < 0) {
                                    return "min";
                                }
                                return "max";
                        } 
                    }

                    var dataAdapter = new $.jqx.dataAdapter(source, {
                        downloadComplete: function (data, xhr) { },
                        loadComplete: function (data) { },
                        loadError: function (xhr, error) { }
                    });

                    $("#TableDif").jqxDataTable({
                        width: 680,
                        source: dataAdapter,
                        pageable: true,
                        sortable: true,
                        altRows: true,
                        enableHover: false,
                        columns: [
                            { text: 'ID Empresa', datafield: 'iIdEmpresa', width: 100 },
                            { text: 'Tipo de Liquido', datafield: 'sNombreRenglon', width: 180 },
                            { text: 'Periodo Actual', datafield: 'sTotal',cellsformat:'c2', width:100},
                            { text: 'Perido Anterior', datafield: 'sTotal2',cellsformat:'c2', width: 100 },
                            { text: 'Diferencia', datafield: 'sTotalDif', cellClassName: cellClass,cellsformat:'c2', width: 100 },
                            { text: 'No Empleados', datafield: 'iNoEmpleado',width:100},
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


    $("#TableDif").on('rowDoubleClick', function (event) {
        var args = event.args;
        var index = args.index;
        var row = args.row;
        var idTipopago = row.sNombreRenglon;
        separador = " ",
        limite = 2,
        arreglosubcadena = idTipopago.split(separador, limite);
        FComparativoXEmpleado(arreglosubcadena[0],0)

    });

    FComparativoXEmpleado = (idTipoPAgo,idRecibo) => {
        const DataSent = { CrtliIdEmpresa: DropEmpresa.value, CrtliAnio: TxtAnio.value, CrtliTipoPeriodo: DropTipodePerdio.value, CtrliPeriodo: DropPerido.value, CtrliPeriodo2: DropPerido2.value, CtrliTipoPAgo: idTipoPAgo, recibo: idRecibo }     
        $.ajax({
            url: "../Nomina/NomiaDiferenciaxEmpleado",
            type: "POST",
            data: DataSent,
            success: (data) => {
              
                document.getElementById('content-tabledifEmpleado').classList.remove("d-none");
                var source =
                {
                    localdata: data,
                    datatype: "array",
                    datafields:
                        [
                            { name: 'iIdEmpleado', type: 'int' },
                            { name: 'sNombreEmpleado', type: 'string' },
                            { name: 'sTotal', type: 'float' },
                            { name: 'sTotal2', type: 'float' },
                            { name: 'sTotalDif', type: 'float' },
                        ],

                };
                var cellClass = function (row, dataField, cellText, rowData) {
                    var cellValue = rowData[dataField];

                    switch (dataField) {
                        case "sTotalDif":
                            if (cellValue > 0) {
                                return "min";
                            }
                            return "max";
                        case "sTotal2":
                            if (cellValue < 0) {
                                return "min";
                            }
                            return "max";
                    }
                }

                var dataAdapter = new $.jqx.dataAdapter(source, {
                    downloadComplete: function (data, xhr) { },
                    loadComplete: function (data) { },
                    loadError: function (xhr, error) { }
                });

                $("#TableDifEmpleado").jqxDataTable({
                    width: 580,
                    source: dataAdapter,
                    pageable: true,
                    sortable: true,
                    altRows: true,
                    enableHover: false,
                    columns: [
                        { text: 'ID Empleado', datafield: 'iIdEmpleado', width: 100 },
                        { text: 'Nombre Empleado', datafield: 'sNombreEmpleado', width: 180 },
                        { text: 'Periodo Actual', datafield: 'sTotal', cellsformat: 'c2', width: 100 },
                        { text: 'Perido Anterior', datafield: 'sTotal2', cellsformat: 'c2', width: 100 },
                        { text: 'Diferencia', datafield: 'sTotalDif', cellClassName: cellClass, cellsformat: 'c2', width: 100 },              
                    ]
                });
            },
        });

    };


    $("#TableDifEmpleado").on('rowDoubleClick', function (event) {
        var args = event.args;
        var index = args.index;
        var row = args.row;
        var idEmpleado = row.iIdEmpleado;
        FComparativoNominaEmpleado(idEmpleado,0);
    });

    FComparativoNominaEmpleado = (idEmpleado, idRecibo) => {

        const DataSent = { CrtliIdEmpresa: DropEmpresa.value, CrtliAnio: TxtAnio.value, CrtliTipoPeriodo: DropTipodePerdio.value, CtrliPeriodo: DropPerido.value, CtrliPeriodoAnte: DropPerido2.value, CtrliIdEmpleado: idEmpleado, CtrliEspejo: idRecibo }
        $.ajax({
            url: "../Nomina/NomiaDiferencia",
            type: "POST",
            data: DataSent,
            success: (data) => {

                document.getElementById('content-tabledifEmpleNomina').classList.remove("d-none");
                var source =
                {
                    localdata: data,
                    datatype: "array",
                    datafields:
                        [
                            { name: 'iIdRenglon', type: 'int' },
                            { name: 'sNombreRenglon', type: 'string' },
                            { name: 'sTotal', type: 'float' },
                            { name: 'sTotal2', type: 'float' },
                            { name: 'sTotalDif', type: 'float' },
                        ],

                };
                var cellClass = function (row, dataField, cellText, rowData) {
                    var cellValue = rowData[dataField];

                    switch (dataField) {
                        case "sTotalDif":
                            if (cellValue > 0) {
                                return "min";
                            }
                            return "max";
                        case "sTotal2":
                            if (cellValue < 0) {
                                return "min";
                            }
                            return "max";
                    }
                }

                var dataAdapter = new $.jqx.dataAdapter(source, {
                    downloadComplete: function (data, xhr) { },
                    loadComplete: function (data) { },
                    loadError: function (xhr, error) { }
                });

                $("#TableDifEmplNomina").jqxDataTable({
                    width: 580,
                    source: dataAdapter,
                    pageable: true,
                    sortable: true,
                    altRows: true,
                    enableHover: false,
                    columns: [
                        { text: 'ID Renglón', datafield: 'iIdRenglon', width: 100 },
                        { text: 'Descripción del renglón', datafield: 'sNombreRenglon', width: 180 },
                        { text: 'Periodo Actual', datafield: 'sTotal', cellsformat: 'c2', width: 100 },
                        { text: 'Perido Anterior', datafield: 'sTotal2', cellsformat: 'c2', width: 100 },
                        { text: 'Diferencia', datafield: 'sTotalDif', cellClassName: cellClass, cellsformat: 'c2', width: 100 },
                    ]
                });
            },
        });


    };

    /// Limpia Campos

    FLimpCamp = () => {

        DropEmpresa.value = 0;
        TxtAnio.value = "";
        DropTipodePerdio.value = 0;
        DropPerido.value = 0;
        DropPerido2.value = 0;
        TxtPorcentaje.value = "";
        $("#TableDif").jqxDataTable('clear');
        document.getElementById('content-tabledif').classList.add("d-none");
        $("#TableDifEmpleado").jqxDataTable('clear');
        document.getElementById('content-tabledifEmpleado').classList.add("d-none");
        $("#TableDifEmplNomina").jqxDataTable('clear');
        document.getElementById('content-tabledifEmpleNomina').classList.add("d-none");
    };

    btnLimpiarCamp.addEventListener('click', FLimpCamp);



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
