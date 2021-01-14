$(function () {

    //////////////////////////////////// Recibos Nomina ///////////////////////////////

    /// declaracion de variables globales

    const EmpresaNom = document.getElementById('EmpresaNom');
    const anoNom = document.getElementById('anoNom');
    const btnFloBuscar = document.getElementById('btnFloBuscar');
    const BtbGeneraPDF = document.getElementById('BtbGeneraPDF');
    const TipodePerdioRec = document.getElementById('TipodePerdioRec');
    const PeridoNom = document.getElementById('PeridoNom');
    const Emisor = document.getElementById('Emisor');
    const BtbGeneraXM = document.getElementById('BtbGeneraXML');
    const btnFloLimpiar = document.getElementById('btnFloLimpiar');
    const LaTotalPer = document.getElementById('LaTotalPer');
    const LaTotalDedu = document.getElementById('LaTotalDedu');
    const LaTotalNom = document.getElementById('LaTotalNom');
    const btnPDFms = document.getElementById('btnPDFms');
    const btnXmlms = document.getElementById('btnXmlms');
    const ChecRecibo2 = document.getElementById('CheckRecibo2');
    const CheckFiniquito = document.getElementById('CheckFiniquito');
    const divchecFiniquito = document.getElementById('divchecFiniquito');
    const dropFiniquito = document.getElementById('dropFiniquito'); 
    const LaDropFiniquito = document.getElementById('LaDropFiniquito');
    const LachecRecibo2 = document.getElementById('LachecRecibo2');
    /*const CheckRecibosFis = document.getElementById('CheckRecibosFis')*/;
    const btnDowlan = document.getElementById('btnDowlan');
    const LaFiniquito = document.getElementById('LaFiniquito');
    const dropPeriodoEmple = document.getElementById('dropPeriodoEmple');
    const LaPeriodoEmple = document.getElementById('LaPeriodoEmple');

    var ValorChek = document.getElementById('CheckRecibo2');
    var valorChekFint = document.getElementById('CheckFiniquito');
    //var ValorChekReFis = document.getElementById('CheckRecibosFis');
    var EmpresNom;
    var IdEmpresa;
    var NombreEmpleado;
    var NoEmpleado;
    var anio;
    var Tipoperiodo;
    var datosPeriodo;
    var datainformations;
    var rowscounts = 0;

    /// saca la fecha del dia 

    n = new Date();
    //Año
    y = n.getFullYear();
  

    anoNom.value = y;

    ///Listbox de Empresas 

    $("#jqxInput").jqxInput({ placeHolder: " Nombre del Empleado", width: 250, height: 30, minLength: 1 });
   

    FListadoEmpresa = () => {
        $("#EmpresaNom").empty();
        $('#EmpresaNom').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisEmpresas",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("EmpresaNom").innerHTML += `<option value='${data[i].iIdEmpresa}'>${data[i].sNombreEmpresa}</option>`;
                }
            }
        });
    };
    FListadoEmpresa();

    // ////  ListBox Empleado

    $('#EmpresaNom').change(function () {
     
        IdEmpresa = EmpresaNom.value;
        const dataSend = { IdEmpresa: IdEmpresa };
        $("#TipodePerdioRec").empty();
        $('#TipodePerdioRec').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisTipPeriodo",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("TipodePerdioRec").innerHTML += `<option value='${data[i].iId}'>${data[i].sValor}</option>`;
                }
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });


    });

 

    //// Muestra fecha de inicio y fin de peridodos

    $('#TipodePerdioRec').change(function () {
       // ListPeriodoEmpresa
        IdEmpresa = EmpresaNom.value;
        anio = anoNom.value;
        Tipodeperiodo = TipodePerdioRec.value;
        const dataSend = { iIdEmpresesas: IdEmpresa, ianio: anio, iTipoPeriodo: Tipodeperiodo };
        console.log(dataSend);
        $("#PeridoNom").empty();
        $('#PeridoNom').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/ListPeriodo",
            type: "POST",
            data: dataSend,
            success: (data) => {
                console.log(data);
                for (i = 0; i < data.length; i++) {
                  document.getElementById("PeridoNom").innerHTML += `<option value='${data[i].iId}'>${data[i].iPeriodo} Fecha del: ${data[i].sFechaInicio} al ${data[i].sFechaFinal}</option>`;                        
                }
            },
        });
        $("#jqxInput").empty();
        $("#jqxInput").jqxInput({ source: null, placeHolder: " Nombre del Empleado", displayMember: "sNombreCompleto", valueMember: "iIdEmpleado", width: 250, height: 30, minLength: 1 });

    });

    $('#PeridoNom').change(function () {
    

         IdEmpresa = EmpresaNom.value;
         Tipodeperiodo = TipodePerdioRec.value;
         var periodo = PeridoNom.options[PeridoNom.selectedIndex].text;
         if (PeridoNom.value == 0) {
             $("#jqxInput").empty();
             $("#jqxInput").jqxInput({ source: null, placeHolder: "Nombre del Empleado", displayMember: "sNombreCompleto", valueMember: "iIdEmpleado", width: 250, height: 30, minLength: 1 });
                        
         }
         if (periodo != "Selecciona") {
             separador = " ",
             limite = 2,
             arreglosubcadena = periodo.split(separador, limite);

             const dataSend = { iIdEmpresa: IdEmpresa, TipoPeriodo: Tipodeperiodo, periodo: arreglosubcadena[0], Anio: anoNom.value };   
             $.ajax({
                     url: "../Empleados/DataListEmpleado",
                     type: "POST",
                     data: dataSend,
                     success: (data) => {
                         if (data.length > 0) {
                             var source =
                             {

                                 localdata: data,
                                 datatype: "array",
                                 datafields:
                                     [
                                         { name: 'iIdEmpleado' },
                                         { name: 'sNombreCompleto' }

                                     ]
                             };
                             var dataAdapter = new $.jqx.dataAdapter(source);
                             $("#jqxInput").empty();
                             $("#jqxInput").jqxInput({ source: dataAdapter, placeHolder: " Nombre del Empleado", displayMember: "sNombreCompleto", valueMember: "iIdEmpleado", width: 250, height: 30, minLength: 1 });
                             $("#jqxInput").on('select', function (event) {
                                 if (event.args) {
                                     var item = event.args.item;
                                     if (item) {
                                         var valueelement = $("<div></div>");
                                         valueelement.text("Value: " + item.value);
                                         var labelelement = $("<div></div>");
                                         labelelement.text("Label: " + item.label);
                                         NoEmpleado = item.value;
                                         NombreEmpleado = item.label;
                                     }
                                 }

                             });

                         }
                         else {
                             $("#jqxInput").empty();
                             $("#jqxInput").jqxInput({ source: null, placeHolder: " Nombre del Empleado", displayMember: "sNombreCompleto", valueMember: "iIdEmpleado", width: 250, height: 30, minLength: 1 });
                         }

                     }
                 });
             

         }
        
       
    });


     /// validacion de año

     $("#iAnoDe").keyup(function () {
         this.value = (this.value + '').replace(/[^0-9]/g, '');
     });


    FDelettable = () => {
        if (rowscounts > 0) {
            console.log('elimina datos de tabla');
           var datainformations = $('#TbRecibosNomina').jqxGrid('getdatainformation');
           var rowscounts = datainformations.rowscount;
            console.log(rowscounts);
            for (var i = 0; i <= rowscounts; i++) {

                $("#TbRecibosNomina").jqxGrid('deleterow', i);
            }
        }

    };
    /// seleciona al empleado 
    $("#jqxInput").on('select', function (event) {
        if (event.args) {
            var item = event.args.item;
            if (item) {
                var valueelement = $("<div></div>");
                valueelement.text("Value: " + item.value);
                var labelelement = $("<div></div>");
                labelelement.text("Label: " + item.label);
                NoEmpleado= item.value;
                NombreEmpleado = item.label;
            }
        }

    });
     //// FLlena del Grid con los datos de La nomina

    FBuscar = () => {

        FDelettable();
        var TotalPercep=0;
        var TotalDedu=0;
        var Total=0;
        IdEmpresa = EmpresaNom.value;
        NombreEmpleado;
        var periodo = PeridoNom.options[PeridoNom.selectedIndex].text;
        

        const dataSend = { IdEmpresa: IdEmpresa, sNombreComple: NombreEmpleado };
         $.ajax({
             url: "../Empleados/EmisorEmpresa",
             type: "POST",
             data: dataSend,
             success: function (data) {
                 if (data[0].sMensaje == "success") {
                     EmpresNom = data[0].sNombreComp + ' ' + 'RFC: ' + data[0].sRFCEmpleado + '  en el periodo: ' + periodo;
                     $('#Emisor').html(EmpresNom);
                 }
                 else {
                     fshowtypealert('Error', 'Contacte a sistemas', 'error');
                 }
                                         
             }
         });      
         separador = " ",
         limite = 2,
         arreglosubcadena = periodo.split(separador, limite);
         NoEmpleado;
        
        const dataSendFinQ = { iIdEmpresa: IdEmpresa, TipoPeriodo: TipodePerdioRec.value, periodo: arreglosubcadena[0], Anio: anoNom.value, IdEmpleado: NoEmpleado };
        console.log(dataSendFinQ);
        $.ajax({
            url: "../Empleados/ListEmpleadoFin",
            type: "POST",
            data: dataSendFinQ,
            success: (data) => {
                if (data[0].iIdEmpleado == 0) {
                    console.log('no tiene ');
                    CheckFiniquito.style.visibility = 'hidden';
                    LaFiniquito.style.visibility = 'hidden';
            
                  
                };
                if (data[0].iIdEmpleado == 1) {
                    console.log('tienefini');
                    CheckFiniquito.style.visibility = "visible";
                    LaFiniquito.style.visibility = "visible";
                
                };
            }


        });
        const dataSend2 = { iIdEmpresa: IdEmpresa, iIdEmpleado: NoEmpleado, ianio: anoNom.value, iTipodePerido: TipodePerdioRec.value, iPeriodo: arreglosubcadena[0], iespejo: 0, idTipFiniquito: 0 };
        FGridRecibos(dataSend2);
      
        FPeriodosEmpleado(IdEmpresa, anoNom.value, TipodePerdioRec.value, NoEmpleado);
    };

    /// Periodo de empleados 

    FPeriodosEmpleado = (empresa, anios, iTipoPeriodo, NoEmpleado) => {
        const dataSend = { Idempresa: empresa, Anio: anios, TipoPeriodo: iTipoPeriodo, idEmpleado:NoEmpleado };
      
        $("#dropPeriodoEmple").empty();
        $('#dropPeriodoEmple').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/PeriodosEmpleados",
            type: "POST",
            data: dataSend,
            success: (data) => {

                for (i = 0; i < data.length; i++) {
                  document.getElementById("dropPeriodoEmple").innerHTML += `<option value='${data[i].iPeriodo}'>${data[i].iPeriodo}</option>`;
                }
            },
        });

    };

    $('#dropPeriodoEmple').change(function () {

        var periodoEmple = dropPeriodoEmple.value;
        FBuscar2(periodoEmple);

    });

    FBuscar2 = (periodoEmple) => {
        FDelettable();
        var TotalPercep = 0;
        var TotalDedu = 0;
        var Total = 0;
        IdEmpresa = EmpresaNom.value;
        NombreEmpleado;
        NoEmpleado;

        const dataSendFinQ = { iIdEmpresa: IdEmpresa, TipoPeriodo: TipodePerdioRec.value, periodo: periodoEmple, Anio: anoNom.value, IdEmpleado: NoEmpleado };
       
        $.ajax({
            url: "../Empleados/ListEmpleadoFin",
            type: "POST",
            data: dataSendFinQ,
            success: (data) => {
                if (data[0].iIdEmpleado == 0) {
                    console.log('no tiene ');
                    CheckFiniquito.style.visibility = 'hidden';
                    LaFiniquito.style.visibility = 'hidden';


                };
                if (data[0].iIdEmpleado == 1) {
                    console.log('tienefini');
                    CheckFiniquito.style.visibility = "visible";
                    LaFiniquito.style.visibility = "visible";

                };
            }


        });
        const dataSend2 = { iIdEmpresa: IdEmpresa, iIdEmpleado: NoEmpleado, ianio: anoNom.value, iTipodePerido: TipodePerdioRec.value, iPeriodo: periodoEmple, iespejo: 0, idTipFiniquito: 0 };
        FGridRecibos(dataSend2);

        FPeriodosEmpleado(IdEmpresa, anoNom.value, TipodePerdioRec.value, NoEmpleado);
    };



    /// llemna grid de los calculos de nomina 
    FGridRecibos = (dataSend2) => {
        if (CheckFiniquito.checked == true) {
            $.ajax({
                url: "../Empleados/ReciboFiniquito",
                type: "POST",
                data: dataSend2,
                success: (Result) => {
                    var source =
                    {
                        localdata: Result.Result,
                        datatype: "array",
                        datafields:
                            [
                                { name: 'sConcepto', type: 'string' },
                                { name: 'dPercepciones', type: 'decimal' },
                                { name: 'dDeducciones', type: 'decimal' },
                                { name: 'dSaldos', type: 'decimal' },
                                { name: 'dInformativos', type: 'decimal' }
                            ]
                    };

                    var dataAdapter = new $.jqx.dataAdapter(source);
                    $("#TbRecibosNomina").jqxGrid(
                        {

                            width: 720,
                            height: 250,
                            source: dataAdapter,
                            columnsresize: true,
                            columns: [
                                { text: 'Concepto', datafield: 'sConcepto', width: 300 },
                                { text: 'Percepciones', datafield: 'dPercepciones', cellsformat: 'c2', width: 100 },
                                { text: 'Deducciones ', datafield: 'dDeducciones', cellsformat: 'c2', width: 100 },
                                { text: 'Saldos', datafield: 'dSaldos', cellsformat: 'c2', width: 100 },
                                { text: 'Informativos', datafield: 'dInformativos', width: 100 }
                            ]
                        });

                    for (i = 0; i < Result.LsTotal.length; i++) {
                        if (Result.LsTotal[i].iIdRenglon == 990) {
                            TotalPercep = Result.LsTotal[i].dSaldo;
                            $('#LaTotalPer').html(TotalPercep);
                        }
                        if (Result.LsTotal[i].iIdRenglon == 1990) {

                            TotalDedu = Result.LsTotal[i].dSaldo;
                            $('#LaTotalDedu').html(TotalDedu);
                        }
                    }
                      BtbGeneraPDF.value = Result.LsTotal[0].iIdFiniquito;
                      Total = TotalPercep - TotalDedu;
                      $('#LaTotalNom').html(Total);
                      FTipoFiniquito();
                }
            });
        }

        if (CheckFiniquito.checked == false) {
            $.ajax({
                url: "../Empleados/ReciboNomina",
                type: "POST",
                data: dataSend2,
                success: (data) => {
                    var source =
                    {
                        localdata: data,
                        datatype: "array",
                        datafields:
                            [
                                { name: 'sConcepto', type: 'string' },
                                { name: 'dPercepciones', type: 'decimal' },
                                { name: 'dDeducciones', type: 'decimal' },
                                { name: 'dSaldos', type: 'decimal' },
                                { name: 'dInformativos', type: 'decimal' }
                            ]
                    };

                    var dataAdapter = new $.jqx.dataAdapter(source);
                    $("#TbRecibosNomina").jqxGrid(
                        {

                            width: 720,
                            height: 250,
                            source: dataAdapter,
                            columnsresize: true,
                            columns: [
                                { text: 'Concepto', datafield: 'sConcepto', width: 300 },
                                { text: 'Percepciones', datafield: 'dPercepciones', cellsformat: 'c2', width: 100 },
                                { text: 'Deducciones ', datafield: 'dDeducciones', cellsformat: 'c2', width: 100 },
                                { text: 'Saldos', datafield: 'dSaldos', cellsformat: 'c2', width: 100 },
                                { text: 'Informativos', datafield: 'dInformativos', cellsformat: 'c2', width: 100 }
                            ]
                        });
                }
            });

            $.ajax({
                url: "../Empleados/TotalesRecibo",
                type: "POST",
                data: dataSend2,
                success: (data) => {
                    if (data.length > 0) {
                        for (i = 0; i < data.length; i++) {
                            if (data[i].iIdRenglon == 990) {
                                TotalPercep = data[i].dSaldo
                                $('#LaTotalPer').html(new Intl.NumberFormat("en-IN").format(TotalPercep));
                            }
                            if (data[i].iIdRenglon == 1990) {

                                TotalDedu = data[i].dSaldo
                                $('#LaTotalDedu').html(new Intl.NumberFormat("en-IN").format(TotalDedu));
                            }
                        }
                        Total = TotalPercep - TotalDedu;
                        $('#LaTotalNom').html(new Intl.NumberFormat("en-IN").format(Total));

                    }
                }
            }); 
        }    
    };

    FTipoFiniquito = () => {
     
       IdEmpresa = EmpresaNom.value;
        NombreEmpleado;
        var periodo = PeridoNom.options[PeridoNom.selectedIndex].text;
        separador = " ",
        limite = 2,
        arreglosubcadena = periodo.split(separador, limite);
        arreglosubcadena2 = NombreEmpleado.split(separador, limite);

        const dataSend = { iIdEmpresa: IdEmpresa, iIdEmpleado: arreglosubcadena2[0], Anio: anoNom.value, periodo: arreglosubcadena[0] };
      
        $.ajax({
            url: "../Empleados/ListFiniquito",
            type: "POST",
            data: dataSend,
            success: function (data) {
                if (data[0].sMensaje == "success") {
                    $("#dropFiniquito").empty();
                    for (i = 0; i < data.length; i++) {
                        document.getElementById("dropFiniquito").innerHTML += `<option value='${data[i].iIdTipoFiniquito}'>${data[i].iIdTipoFiniquito} ${data[i].sNombreFiniquito}</option>`;
                    }

                }
                if (data[0].sMensaje == "error"){
                    $("#EmpresaNom").empty();
                    $('#EmpresaNom').append('<option value="0" selected="selected">Selecciona</option>');
                }
                
            }
        });      


    };

     btnFloBuscar.addEventListener('click', FBuscar);
    /// Genera archivo XML
    FGenerarXML = () => {
        IdEmpresa = EmpresaNom.value;
        var nom = $('#jqxInput').jqxInput('val');
        NombreEmpleado = nom.label;
        IdEmpresa = EmpresaNom.value;
        anio = anoNom.value;
        Tipoperiodo = TipodePerdioRec.value;
        datosPeriodo = PeridoNom.value;
        const dataSend = { IdEmpresa: IdEmpresa, sNombreComple: NombreEmpleado, Periodo: datosPeriodo, anios: anio, Tipodeperido: Tipoperiodo, Masivo: 0 };
        $.ajax({
            url: "../Empleados/XMLNomina",
            type: "POST",
            data: dataSend,
            success: function (data) {
                if (data[0].sMensaje != "NorCert") {
                    var url = '\\Archivos\\ZipXML.zip';
                    window.open(url);

                }
                else {
                    fshowtypealert('Error', 'Contacte a sistemas', 'error');
                }

            }
        });

    };

    BtbGeneraXML.addEventListener('click', FGenerarXML);

    btnFloLimpiar.addEventListener('click', function () {
         console.log('lipia datos');
        FLimpCamp();
    });

    /// Limpia Campos

    FLimpCamp = () => {
        console.log('lipia datos');
        EmpresaNom.value = 0;
        anoNom.value = "";
        TipodePerdioRec.value = 0;
        PeridoNom.value = 0;
        $("#jqxInput").empty();
        $("#jqxInput").jqxInput('clear');
        $("#jqxInput").jqxInput({ source: null, placeHolder: " Nombre del Empleado", displayMember: "", valueMember: "", width: 250, height: 30, minLength: 1 });

    };
    

    /// muestra el Recibo 2

    FValorChec = () => {
        var TotalPercep = 0;
        var TotalDedu = 0;
        var Total = 0;
        IdEmpresa = EmpresaNom.value;
        NombreEmpleado;
        var periodo = PeridoNom.options[PeridoNom.selectedIndex].text;
        separador = " ",
            limite = 2,
            arreglosubcadena = periodo.split(separador, limite);
        if (ValorChek.checked == true) {

      
            const dataSend2 = { iIdEmpresa: IdEmpresa, iIdEmpleado: NoEmpleado, ianio: anoNom.value, iTipodePerido: TipodePerdioRec.value, iPeriodo: arreglosubcadena[0], iespejo: 1 };
            FGridRecibos(dataSend2);


        }

        if (ValorChek.checked == false) {
          
            const dataSend2 = { iIdEmpresa: IdEmpresa, iIdEmpleado: NoEmpleado, ianio: anoNom.value, iTipodePerido: TipodePerdioRec.value, iPeriodo: arreglosubcadena[0], iespejo: 0 };

            FGridRecibos(dataSend2);
        }

    };

    ChecRecibo2.addEventListener('click', FValorChec);

    /// seleciona el tipo de finiquito y muestra el los calculos 

    
    $('#LaDropFiniquito').change(function () {

        IdEmpresa = EmpresaNom.value;
        NombreEmpleado;
        var periodo = PeridoNom.options[PeridoNom.selectedIndex].text;
        separador = " ",
        limite = 2,
        arreglosubcadena = periodo.split(separador, limite);
        NoEmpleado;

        const dataSend2 = { iIdEmpresa: IdEmpresa, iIdEmpleado: NoEmpleado, ianio: anoNom.value, iTipodePerido: TipodePerdioRec.value, iPeriodo: arreglosubcadena[0], iespejo: 0, idTipFiniquito: LaDropFiniquito};
        FGridRecibos(dataSend2);


    });



    /// selecciona tipo de recibo normal o finiquito 

    FvalorChechFin = () => {
     
        if (valorChekFint.checked == true) {
         
            LaDropFiniquito.style.visibility = 'visible';
            dropFiniquito.style.visibility = 'visible';
            CheckRecibo2.style.visibility = 'hidden';
            LachecRecibo2.style.visibility = 'hidden';
            BtbGeneraXML.style.visibility = 'hidden';
            dropPeriodoEmple.style.visibility = 'hidden';
            LaPeriodoEmple.style.visibility = 'hidden';
            
            const dataSend2 = { iIdEmpresa: IdEmpresa, iIdEmpleado: NoEmpleado, ianio: anoNom.value, iTipodePerido: TipodePerdioRec.value, iPeriodo: arreglosubcadena[0], iespejo: 0, idTipFiniquito: 1 };
            FGridRecibos(dataSend2);

        }
        if (valorChekFint.checked == false) {
           
            dropFiniquito.style.visibility = 'hidden';
            LaDropFiniquito.style.visibility = 'hidden';
            CheckRecibo2.style.visibility = 'visible';
            LachecRecibo2.style.visibility = 'visible';
            BtbGeneraXML.style.visibility = 'visible';
            dropPeriodoEmple.style.visibility = 'visible';
            LaPeriodoEmple.style.visibility = 'visible';

            const dataSend2 = { iIdEmpresa: IdEmpresa, iIdEmpleado: NoEmpleado, ianio: anoNom.value, iTipodePerido: TipodePerdioRec.value, iPeriodo: arreglosubcadena[0], iespejo: 0, idTipFiniquito: 0 };
            FGridRecibos(dataSend2);
         
          
        }

    }

    CheckFiniquito.addEventListener('click', FvalorChechFin);


    /// descarga masiva de xml

    FdowXmlsMasivo = () => {
        btnXmlms.value = 1;
        btnPDFms.value = 0;
        IdEmpresa = EmpresaNom.value;
        var nom = $('#jqxInput').jqxInput('val');
        NombreEmpleado = nom.label;
        IdEmpresa = EmpresaNom.value;
        anio = anoNom.value;
        Tipoperiodo = TipodePerdioRec.value;
        datosPeriodo = PeridoNom.value;
        const dataSend = { IdEmpresa: IdEmpresa, sNombreComple: 0, Periodo: datosPeriodo, anios: anio, Tipodeperido: Tipoperiodo, Masivo:1 };
        $.ajax({
            url: "../Empleados/XMLNomina",
            type: "POST",
            data: dataSend,
            beforeSend: function (data) {
                $('#jqxLoader2').jqxLoader('open');
            },
            success: function (data) {

                if (data[0].sMensaje != "NorCert") {
                    btnDowlan.style.visibility = 'visible';
                    $('#jqxLoader2').jqxLoader('close');
                    fshowtypealert('Recibos de nomina', 'sean generado los archivos XML correctamente', 'success');

                }
                else {
                    $('#jqxLoader2').jqxLoader('close');
                    fshowtypealert('Error', 'Contacte a sistemas', 'error');
                }

            }
        });

    };

    btnXmlms.addEventListener('click',FdowXmlsMasivo)

    /// descarga masiva de PDF

  
    FdowPDFMasivos = () => {
        btnXmlms.value = 0;
        btnPDFms.value = 1;
        console.log('prueba');
        var oprefis = 0;
        
        IdEmpresa = EmpresaNom.value;
        var nom = $('#jqxInput').jqxInput('val');
        NombreEmpleado = nom.label;
        IdEmpresa = EmpresaNom.value;
        anio = anoNom.value;
        Tipoperiodo = TipodePerdioRec.value;
        datosPeriodo = PeridoNom.value;
        const dataSend = { IdEmpresa: IdEmpresa, sNombreComple: 0, Periodo: datosPeriodo, anios: anio, Tipodeperido: Tipoperiodo, iRecibo: 1 };
        $.ajax({
            url: "../Empleados/GPDFMasivos",
            type: "POST",
            data: dataSend,
            beforeSend: function (data) {
                $('#jqxLoader').jqxLoader('open');
            },
            success: function (data) {
                if (data[0].sMensaje != "NorCert") {
                    btnDowlan.style.visibility = 'visible';
                    btnDowlan.value = data[0].sUrl;
                    $('#jqxLoader').jqxLoader('close');
                    fshowtypealert('Recibos de nomina', 'sean generado el PDF correctamente', 'success');         
                  
                }
                else {
                   
                    $('#jqxLoader').jqxLoader('close');
                    fshowtypealert('Error', 'Contacte a sistemas', 'error');
                }
                
            }
        });

   
    };

    btnPDFms.addEventListener('click', FdowPDFMasivos)

    FOpenFile = () => {


        if (btnPDFms.value == 1) {
            var periodo = PeridoNom.options[PeridoNom.selectedIndex].text;
            separador = " ",
            imite = 2,
            arreglosubcadena = periodo.split(separador, limite);

            var nombre = "RecibosNom_E" + EmpresaNom.value+ "_P" + arreglosubcadena[0]+".pdf";   
            var url = '\\Archivos\\' + nombre;
            window.open(url);
            btnDowlan.style.visibility = 'hidden';
        };

        if (btnXmlms.value == 1) {
            var url = '\\Archivos\\ZipXML.zip';
            window.open(url);
            btnDowlan.style.visibility = 'hidden';
        };

    };

    btnDowlan.addEventListener('click', FOpenFile);


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

    $("#jqxLoader").jqxLoader({ text: "Generando PDF", width: 160, height: 80 });
    $("#jqxLoader2").jqxLoader({ text: "Generando XML", width: 160, height: 80 });
});