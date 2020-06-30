$(function () { 
                 /// Declaracion de variables 

    const TxIdRenglon = document.getElementById('TxIdRenglon');
    const txNomRenglon = document.getElementById('txNomRenglon');
    const DropEmpresa = document.getElementById('DropEmpresa');
    const DropTipoReng = document.getElementById('DropTipoReng');
    const DropElemNom = document.getElementById('DropElemNom');
    const txReporte = document.getElementById('txReporte');
    const DropAcumulado = document.getElementById('DropAcumulado');
    const DroplisCalculo = document.getElementById('DroplisCalculo');
    const txCueCont = document.getElementById('txCueCont');
    const txDespCuent = document.getElementById('txDespCuent');
    const txCargCuent = document.getElementById('txCargCuent');
    const DroplisSat = document.getElementById('DroplisSat');
    const BtnAgreRe = document.getElementById('BtnAgreRe');

    var Cancel;
    var espejo;

    // Lis empresa 
    LisEmpresa = () => {
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
    LisEmpresa();
     // Lista de renglon
    TipoRenglon = () => {
        $.ajax({
            url: "../Catalogos/ListTipoRenglon",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DropTipoReng").innerHTML += `<option value='${data[i].iIdRenglon}'>${data[i].sTipoRenglon}</option>`;
                }
            }
        });
    };
    TipoRenglon();

     // List Elemeto Nomina
    ElemNom = () => {
        $.ajax({
            url: "../Catalogos/ListEleNom",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DropElemNom").innerHTML += `<option value='${data[i].iIdValor}'>${data[i].sValor}</option>`;
                }
            }
        });
    };
    ElemNom();

     // list Calculo
    LisCalculo = () => {
        $.ajax({
            url: "../Catalogos/ListCalcu",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DroplisCalculo").innerHTML += `<option value='${data[i].iIdCalculo}'>${data[i].sNombreCalculo}</option>`;
                }
            }
        });
    };
    LisCalculo();

    /// Lista de Acumulados 
    $('#DropElemNom').change(function () {

        LisAcumu(DropEmpresa.value, DropElemNom.value);

    });

    LisAcumu = ( IdRenglon, idElemnto) => {
        const dataSend = { iIdEmpresa: IdRenglon, iElementoNom: idElemnto };
        $.ajax({
            url: "../Catalogos/LisAcumu",
            type: "POST",
            data: dataSend,
            success: (data) => {
                $("#DropAcumulado").empty();
                      for (i = 0; i < data.length; i++) {
                          document.getElementById("DropAcumulado").innerHTML += `<option value='${data[i].iIdRenglon}'>${data[i].sNombreRenglon} </option>`;

                        }
                    }
        });
    }


    /// Lista SAt 

    LisSat = () => {
        $.ajax({
            url: "../Catalogos/ListSat",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DroplisSat").innerHTML += `<option value='${data[i].idSat}'> ${data[i].idSat} ${data[i].sSat}</option>`;
                }
            }
        });
    };
    LisSat();



    FAgrega = () => {
        var op1 = Cancel;
        var op2 = espejo;
        const dataSend = {
            iIdRenglon: TxIdRenglon.value, sNombreRenglon: txNomRenglon.value,
            iIdEmpresa: DropEmpresa.value, iTipodeRenglon: DropTipoReng.value,
            iElementoNom: DropElemNom.value, iIdReporte: txReporte, IdAcumulado: DropAcumulado.value,
            idCalculo: DroplisCalculo.value, sCuentaCont: txCueCont.value, sDespCuenta: txDespCuent.value,
            sCargaCuenta: txCargCuent.value, iIdSat: DroplisSat.value, icancel: op1,
            iEspejo: espejo

        };
        console.log(dataSend);
    };

    //btnAgreRenglon.addEventListener('click', FAgregaReng);
    BtnAgreRe.addEventListener('click', FAgrega);
              

    //themas 
    $("#dTabCrenglones").jqxDataTable({ theme: 'bootstrap' })
    $("#ChecCancel").jqxCheckBox({ theme: 'bootstrap' })
    $("#ChecEspejo").jqxCheckBox({ theme: 'bootstrap' })

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
                            { name: 'sIdEmpresa', type: 'cadena' },
                            { name: 'sNombreRenglon', type: 'cadena' },
                            { name: 'sIdElementoNomina', type: 'cadena' },
                            { name: 'iIdSeccionReporte', type: 'cadena' },
                            { name: 'sIdAcumulado', type: 'cadena' },
                            { name: 'sCancelado', type: 'cadena' },
                            { name: 'sTipodeRenglon', type: 'cadena' },
                            { name: 'sEspejo', type: 'cadena' },
                            { name: 'slistCalculos', type: 'cadena' },
                            { name: 'sCuentaCont', type: 'cadena' },
                            { name: 'sDespCuCont', type: 'cadena' },
                            { name: 'sCargAbCuenta', type: 'cadena' },
                            { name: 'sIdSat', type: 'cadena' }
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
                            { text: 'Empresa', datafield: 'sIdEmpresa', width: 150 },
                            { text: 'Nombre de renglón', datafield: 'sNombreRenglon', width: 200 },
                            { text: 'Elemento de Nomina', datafield: 'sIdElementoNomina', whidth: 200 },
                            { text: 'Id de Reporte', datafield: 'iIdSeccionReporte', whidt: 150 },
                            { text: 'Acumulado', datafield: 'sIdAcumulado', width: 200 },
                            { text: 'Cancelado', datafield: 'sCancelado', whidth: 50 },
                            { text: 'Tipo de renglón', datafield: 'sTipodeperiodo', whidt: 200 },
                            { text: 'Espejo', datafield: 'sEspejo', width: 200 },
                            { text: 'Lista Calculos', datafield: 'slistCalculos', width: 200 },
                            { text: 'Cuenta Contable', datafield: 'sCuentaCont', width: 200 },
                            { text: 'Descripción de cuenta', datafield: 'sDespCuCont', width: 200 },
                            { text: 'carga de cuenta', datafield: 'sCargAbCuenta', width: 200 },
                            { text: 'Id Sat', datafield: 'sIdSat', width: 200 }
                            //{ text: 'Es espejo', datafield: 'iEsespejo', whidt: 30 }           
                        ]
                    });
               
               
            }
        });                   
    };

    FTabCRenglones();
   
    $("#ChecCancel").jqxCheckBox({ width: 120, height: 25 });
    $("#ChecCancel").bind('change', function (event) {
        var checked = event.args.checked;
        if (checked == true) {
            Cancel = 1;
        }
        if (checked == false) {
            Cancel = 0;
        }
      

    });
    $("#ChecEspejo").jqxCheckBox({ width: 120, height: 25 });
    $("#ChecEspejo").bind('change', function (event) {
        var checked = event.args.checked;
        if (checked == true) {
            espejo = 1;
        }
        if (checked == false) {
            espejo = 0;
        }
      

    });
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