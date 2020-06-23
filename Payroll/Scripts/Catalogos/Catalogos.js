$(function () { 

    //themas 
    $("#dTabCrenglones").jqxDataTable({ theme: 'darkblue' })
    $("#ChecCancel").jqxCheckBox({ theme: 'darkblue' })
    $("#ChecEspejo").jqxCheckBox({ theme: 'darkblue' })



    console.log('hola munfo');
    /// Carga Tb Renglones 
    FTabCRenglones = () => {
        //const tabledep = document.getElementById('data-body-dpercepciones');
       
        const dataSend = { IdEmpresa: 0, iElemntoNOm:0 };
        console.log(dataSend);
    
        $.ajax({
            url: "../Catalogos/datRenglones",
            type: "POST",
            data: dataSend,
            success: (data) => {
                var source =
                {
                    localdata: data,
                    datatype: "array",
                    datafields:
                        [
                            { name: 'iIdEmpresa', type: 'cadena' },
                            { name: 'iIdRenglon', type: 'cadena' },
                            { name: 'sNombreRenglon', type: 'cadena' },
                            { name: 'iIdElementoNomina', type: 'cadena' },
                            { name: 'iIdSeccionReporte', type: 'cadena' },
                            { name: 'iIdAcumulado', type: 'cadena' },
                            { name: 'iTipodeRenglon', type: 'cadena' },
                            { name: 'sCancelado', type: 'cadena' },
                            { name: 'iEspejo', type: 'cadena' },
                            { name: 'ilistCalclos', type: 'cadena' },
                            { name: 'sCuentaCont', type: 'cadena' },
                            { name: 'sDespCuCont', type: 'cadena' },
                            { name: 'sCargAbCuenta', type: 'cadena' },
                        ]
                };

                var dataAdapter = new $.jqx.dataAdapter(source);
                
                $("#dTabCrenglones").jqxDataTable(
                    {
                        source: dataAdapter,
                        width: 800,
                        pageable: true,
                        altRows: true,
                        filterable: true,
                        //filterMode: 'advanced',
                        pagerButtonsCount: 10,
                        columnsResize: true,
                        pagerButtonsCount: 8,
                        columns: [
                            { text: 'Empresa', datafield: 'IdEmpresa', width: 200 },
                            { text: 'Renglon', datafield: 'iIdRenglon', width: 300 },
                            { text: 'Nombre de renglon', datafield: 'sNombreRenglon', whidth: 30 },
                            { text: 'Id de Reporte', datafield: 'iIdSeccionReporte', whidt: 400 },
                            { text: 'Id Renglon', datafield: 'iTipodeRenglon', width: 300 },
                            { text: 'Cancelado', datafield: 'sCancelado', whidth: 30 },
                            { text: 'Lista Calculos', datafield: 'ilistCalclos', whidt: 400 },
                            { text: 'Cuenta de contable', datafield: 'sCuentaCont', width: 200 },
                            { text: 'Id Renglon', datafield: 'iRenglon', width: 300 },
                            { text: 'Tipo de periodo', datafield: 'iTipodeperiodo', whidth: 30 },
                            { text: 'Acumulado', datafield: 'iIdAcumulado', whidt: 400 },
                            { text: 'Es espejo', datafield: 'iEsespejo', whidt: 30 }           
                        ]
                    });
               
               
            }
        });                   
    };
    FTabCRenglones();
    ChecEspejo
        $("#ChecCancel").jqxCheckBox({ width: 120, height: 25 });
        $("#ChecEspejo").jqxCheckBox({ width: 120, height: 25 });
        $("#orderID").jqxInput({ disabled: true, width: 150, height: 30 });
        $("#save").jqxButton({ height: 30, width: 80 });
        $("#cancel").jqxButton({ height: 30, width: 80 });
        $("#cancel").mousedown(function () {
            // close jqxWindow.
            $("#dialog").jqxWindow('close');
        });
        $("#save").mousedown(function () {
            // close jqxWindow.
            $("#dialog").jqxWindow('close');
            // update edited row.
            var editRow = parseInt($("#dialog").attr('data-row'));
            var rowData = {
                OrderID: $("#orderID").val()
            };
            $("#dTabCrenglones").jqxDataTable('updateRow', editRow, rowData);

        });
        $("#dialog").on('close', function () {
            // enable jqxDataTable.

            $("#dTabCrenglones").jqxDataTable({ disabled: false });

        });
        $("#dialog").jqxWindow({
            theme: 'darkblue',
            resizable: false,

            position: { left: $("#dTabCrenglones").offset().left + 75, top: $("#dTabCrenglones").offset().top + -805 },

            width: 270, height: 230,

            autoOpen: false

        });
        $("#dialog").css('visibility', 'visible');
        $("#dTabCrenglones").on('rowDoubleClick', function (event) {
        var args = event.args;
        var index = args.index;
        var row = args.row;
        // update the widgets inside jqxWindow.
        $("#dialog").jqxWindow('setTitle', "Renglon: " + row.iIdRenglon);
        $("#dialog").jqxWindow('open');
        $("#dialog").attr('data-row', index);
        $("#dTabCrenglones").jqxDataTable({ disabled: true });
        $("#orderID").val(row.OrderID);
  
    });
        $("#button1").jqxButton({ width: 120, imgPosition: "left", textPosition: "left", imgSrc: "../../images/facebook.png", textImageRelation: "imageBeforeText" });





});