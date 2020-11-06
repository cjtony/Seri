$(function () { 
                 /// Declaracion de variables 

    const TxIdRenglon = document.getElementById('TxIdRenglon');
    const txNomRenglon = document.getElementById('txNomRenglon');
    //const DropEmpresa = document.getElementById('DropEmpresa');
    const DropEmpresa2 = document.getElementById('DropEmpresa2');
    //const DropTipoReng = document.getElementById('DropTipoReng');
    //const DropElemNom = document.getElementById('DropElemNom');
    const txReporte = document.getElementById('txReporte');
    const DropAcumulado = document.getElementById('DropAcumulado');
    //const DroplisCalculo = document.getElementById('DroplisCalculo');
    const txCueCont = document.getElementById('txCueCont');
    const txDespCuent = document.getElementById('txDespCuent');
    const txCargCuent = document.getElementById('txCargCuent');
    const DroplisSat = document.getElementById('DroplisSat');
    const BtnAgreRe = document.getElementById('BtnAgreRe');
    const DropReporte = document.getElementById('DropReporte');
    const ActuRengl = document.getElementById('ActuRenglon');
    const Latit = document.getElementById('Latitu');
    const AgregarRenglo = document.getElementById('AgregarRenglon');
    const BtnActualiza = document.getElementById('BtnActualiza');
    const LaTipoReng = document.getElementById('LaTipoReng');
    const LaEleNom = document.getElementById('LaEleNom');
    const LalisCalculo = document.getElementById('LalisCalculo');

    var Cancel=0;
    var espejo = 0;
   var titu = "Reglon Nuevo";
    $('#Latitu').html(titu);  
    // Lis empresa 
    LisEmpresa = () => {
        $.ajax({
            url: "../Nomina/LisEmpresas",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    //document.getElementById("DropEmpresa").innerHTML += `<option value='${data[i].iIdEmpresa}'>${data[i].sNombreEmpresa}</option>`;
                    document.getElementById("DropEmpresa2").innerHTML += `<option value='${data[i].iIdEmpresa}'>${data[i].sNombreEmpresa}</option>`;
                    

                }
            }
        });
       
      
    };
    LisEmpresa();
     // Lista de renglon
    //TipoRenglon = () => {
    //    $.ajax({
    //        url: "../Catalogos/ListTipoRenglon",
    //        type: "POST",
    //        data: JSON.stringify(),
    //        contentType: "application/json; charset=utf-8",
    //        success: (data) => {
    //            for (i = 0; i < data.length; i++) {
    //                document.getElementById("DropTipoReng").innerHTML += `<option value='${data[i].iIdRenglon}'>${data[i].sTipoRenglon}</option>`;
    //            }
    //        }
    //    });
    //};
    //TipoRenglon();

     // List Elemeto Nomina
    //ElemNom = () => {
    //    $.ajax({
    //        url: "../Catalogos/ListEleNom",
    //        type: "POST",
    //        data: JSON.stringify(),
    //        contentType: "application/json; charset=utf-8",
    //        success: (data) => {
    //            for (i = 0; i < data.length; i++) {
    //                document.getElementById("DropElemNom").innerHTML += `<option value='${data[i].iIdValor}'>${data[i].sValor}</option>`;
    //            }
    //        }
    //    });
    //};
    //ElemNom();

    // list reporte
    Lisreporte = () => {
        $.ajax({
            url: "../Catalogos/ListReporte",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DropReporte").innerHTML += `<option value='${data[i].iIdValor}'>${data[i].iIdValor} ${data[i].SNombreReporte}</option>`;
                }
            }
        });
    };
    Lisreporte();

    // // list Calculo
    //LisCalculo = () => {
    //    $.ajax({
    //        url: "../Catalogos/ListCalcu",
    //        type: "POST",
    //        data: JSON.stringify(),
    //        contentType: "application/json; charset=utf-8",
    //        success: (data) => {
    //            for (i = 0; i < data.length; i++) {
    //                document.getElementById("DroplisCalculo").innerHTML += `<option value='${data[i].iIdCalculo}'>${data[i].sNombreCalculo}</option>`;
    //            }
    //        }
    //    });
    //};
    //LisCalculo();

    /// Lista de Acumulados 
    //$('#DropElemNom').change(function () {

    //    LisAcumu(DropEmpresa2.value, DropElemNom.value);

    //});

    $('#DropEmpresa2').change(function () {
        FTabCRenglones(DropEmpresa2.value,0);
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


   
    Fbotones = () => {

        BtnActualiza.style.visibility = "visible";
        BtnAgreRe.style.visibility = "hidden";
    }
    ActuRenglon.addEventListener('click', Fbotones);

    FAgrega = () => {
        var op1 = Cancel;
        var op2 = espejo;
        const dataSend = {
            iIdRenglon: TxIdRenglon.value, sNombreRenglon: txNomRenglon.value,
            iIdEmpresa: DropEmpresa2.value, iTipodeRenglon: DropTipoReng.value,
            iElementoNom: DropElemNom.value, iIdReporte: DropReporte.value, IdAcumulado: DropAcumulado.value,
            idCalculo: DroplisCalculo.value, sCuentaCont: txCueCont.value, sDespCuenta: txDespCuent.value,
            sCargaCuenta: txCargCuent.value, iIdSat: DroplisSat.value, icancel: op1,
            iEspejo: espejo, PenAlin: 0

        };

        $.ajax({
            url: "../Catalogos/SaveRenglon",
            type: "POST",
            data: dataSend,
            success: (data) => {
                if (data[0].sMensaje == "success") {
                    FTabCRenglones(DropEmpresa2.value, 0);
                    fshowtypealert('Renglon !', 'Nuevo renglón Guardado', 'success');
                  
                }
                else {
                }
                fshowtypealert('Error', 'Contacte a sistemas', 'error');
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });


    };

    //btnAgreRenglon.addEventListener('click', FAgregaReng);
    BtnAgreRe.addEventListener('click', FAgrega);

    // Actualiza renglon
    FActuRenglon = () => {

        var op1 = Cancel;
        var op2 = espejo;
        const dataSend = {
            iIdRenglon: TxIdRenglon.value, sNombreRenglon: txNomRenglon.value,
            iIdEmpresa: DropEmpresa2.value,  iIdReporte: DropReporte.value, IdAcumulado: DropAcumulado.value,
             sCuentaCont: txCueCont.value, sDespCuenta: txDespCuent.value,
            sCargaCuenta: txCargCuent.value, iIdSat: DroplisSat.value, icancel: op1,
            iEspejo: espejo, PenAlin: 0

        };
        $.ajax({
            url: "../Catalogos/UpdateRenglon",
            type: "POST",
            data: dataSend,
            success: function (data) {
                if (data.sMensaje == "success") {
                    FTabCRenglones(DropEmpresa2.value, 0);
                    fshowtypealert('Renglon Actualizado!', 'Renglón actualizado ', 'success');
                }
                if (data.sMensaje == "error") {

                    fshowtypealert('Error', 'Contacte a sistemas', 'error');
                }
             },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

    };

    BtnActualiza.addEventListener('click', FActuRenglon);


    FTitu = () => {
        BtnActualiza.style.visibility = "hidden";
        BtnAgreRe.style.visibility = "visible";
        titu = 'Nuevo Renglón';
        $('#Latitu').html(titu);
        $("#TxIdRenglon").removeAttr("readonly");
        $("#txNomRenglon").removeAttr("readonly");
        DropTipoReng.style.visibility = 'visible';
        DropElemNom.style.visibility = 'visible';
        DroplisCalculo.style.visibility = 'visible';
        LaTipoReng.style.visibility = 'visible';
        LaEleNom.style.visibility = 'visible';
        LalisCalculo.style.visibility = 'visible';
    }
    AgregarRenglo.addEventListener('click', FTitu);

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


    // validaciones

    $("#txCueCont").keyup(function () {
        this.value = (this.value + '').replace(/[^0-9]/g, '');
    });

    $("#txCargCuent").keyup(function () {
        this.value = (this.value + '').replace(/[^C,A,a,c]/g, '');
    });


    //themas 
    $("#dTabCrenglones").jqxDataTable({ theme: 'bootstrap' })
    $("#ChecCancel").jqxCheckBox({ theme: 'bootstrap' })
    $("#ChecEspejo").jqxCheckBox({ theme: 'bootstrap' })

    /// Carga Tb Renglones 
    FTabCRenglones = (idempre,iElement) => {

       
        const dataSend = { IdEmpresa: idempre, iElemntoNOm: iElement };
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
                            { name: 'sIdSeccionReporte', type: 'cadena' },
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
                        pagerButtonsCount: 10,
                        columnsResize: true,
                        filterMode: 'simple',
                        columns: [
                            //{ text: 'Empresa', datafield: 'sIdEmpresa', width: 150 },
                            { text: 'Nombre de renglón', datafield: 'sNombreRenglon', width: 200 },
                            { text: 'Elemento de Nomina', datafield: 'sIdElementoNomina', whidth: 200 },
                            { text: 'Reporte', datafield: 'sIdSeccionReporte', whidt: 150 },
                            { text: 'Acumulado', datafield: 'sIdAcumulado', width: 200 },
                            { text: 'Cancelado', datafield: 'sCancelado', whidth: 50 },
                            //{ text: 'Tipo de renglón', datafield: 'sTipodeperiodo', whidt: 200 },
                            { text: 'Espejo', datafield: 'sEspejo', width: 200 },
                            //{ text: 'Lista Calculos', datafield: 'slistCalculos', width: 200 },
                            { text: 'Cuenta Contable', datafield: 'sCuentaCont', width: 200 },
                            { text: 'Descripción de cuenta', datafield: 'sDespCuCont', width: 200 },
                            //{ text: 'carga de cuenta', datafield: 'sCargAbCuenta', width: 200 },
                            { text: 'Id Sat', datafield: 'sIdSat', width: 200 }         
                        ]
                    });
               
               
            }
        });                   
    };
    FTabCRenglones(0,0);
    $("#dTabCrenglones").on('rowDoubleClick', function (event) {
        var args = event.args;
        var index = args.index;
        var row = args.row;

        $("#TxIdRenglon").attr("readonly", "readonly");
        $("#txNomRenglon").attr("readonly", "readonly");
        //DropTipoReng.style.visibility = 'hidden';
        //DropElemNom.style.visibility = 'hidden';
        //DroplisCalculo.style.visibility = 'hidden';
        //LaTipoReng.style.visibility = 'hidden';
        //LaEleNom.style.visibility = 'hidden';
        //LalisCalculo.style.visibility = 'hidden';
        //// update the widgets inside jqxWindow.
        //$("#dialog").jqxWindow('setTitle', "Renglon: " + row.iIdRenglon);
        titu = 'Actualizar Renglon';
     

        var NombreRenglon = row.sNombreRenglon;
      
        separador = " ",
        limite = 5,
        arreglosubcadena = NombreRenglon.split(separador, limite);
        TxIdRenglon.value = arreglosubcadena[0];
        if (arreglosubcadena.length > 2) {
            var text = arreglosubcadena[1]
            i=2
            for (i; i < arreglosubcadena.length; i++) {
                text = text + " "+ arreglosubcadena[i];
            }
            console.log(text);
            txNomRenglon.value = text;
        }
        else {
            txNomRenglon.value = arreglosubcadena[1];
        }
       
        //for (var i = 0; i < DropTipoReng.length; i++) {
        //    if (DropTipoReng.options[i].text == row.sTipodeperiodo) {
        //        // seleccionamos el valor que coincide
        //        DropTipoReng.selectedIndex = i;
        //    }
          

        //};
        //for (var i = 0; i < DropElemNom.length; i++) {
        //    if (DropElemNom.options[i].text == row.sIdElementoNomina) {
        //        // seleccionamos el valor que coincide
        //        DropElemNom.selectedIndex = i;
        //    }
          

        //};
        var elem = 0;
        if (row.sIdElementoNomina == "Percepciones") {
            elem = 1
        }
        if (row.sIdElementoNomina == "Deducciones") {
            elem = 2
        }
        if (row.sIdElementoNomina == "Saldos") {
            elem = 5
        }
        $('#Latitu').html(titu);
        $("#ActuRenglon").click();
        for (var i = 0; i < DropReporte.length; i++) {
            if (DropReporte.options[i].text == row.sIdSeccionReporte) {
                // seleccionamos el valor que coincide
                DropReporte.selectedIndex = i;
            }
        };
        //DropReporte.selectedIndex = row.iIdSeccionReporte;
        LisAcumu(DropEmpresa2.value, elem);
        for (var i = 0; i < DropAcumulado.length; i++) {
            if (DropAcumulado.options[i].text == row.sIdAcumulado) {
                // seleccionamos el valor que coincide
                DropAcumulado.selectedIndex = i;
            }
        };
        //for (var i = 0; i < DroplisCalculo.length; i++) {
        //    if (DroplisCalculo.options[i].text == row.slistCalculos) {
        //        // seleccionamos el valor que coincide
        //        DroplisCalculo.selectedIndex = i;
        //    }
        //};
        for (var i = 0; i < DroplisSat.length;) {
            if (DroplisSat.options[i].text == row.sIdSat) {
                // seleccionamos el valor que coincide
                DroplisSat.selectedIndex = i;
            }
            i++;
        };

        txCueCont.value = row.sCuentaCont;
        txDespCuent.value = row.sDespCuCont;
        txCargCuent.value = row.sCargAbCuenta; 
        console.log('EsEspejo' + row.sEspejo);
        if (row.sEspejo == 0) {
            $("#ChecEspejo").jqxCheckBox({  checked: false });
        }
        if (row.sEspejo == 1) {
            $("#ChecEspejo").jqxCheckBox({ checked: true });
        }
        if (row.sCancelado == "False") {
            $("#ChecEspejo").jqxCheckBox({ checked: false });
        }
        if (row.sCancelado == "True") {
            $("#ChecEspejo").jqxCheckBox({ checked: true });
        }


    });

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
        $("#button1").jqxButton({ width: 120, imgPosition: "left", textPosition: "left", imgSrc: "../../images/facebook.png", textImageRelation: "imageBeforeText" });

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