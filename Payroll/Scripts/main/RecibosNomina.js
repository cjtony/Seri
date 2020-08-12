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
   
    var ValorChek = document.getElementById('CheckRecibo2');
    var valorChekFint = document.getElementById('CheckFiniquito');

    var EmpresNom;
    var IdEmpresa;
    var NombreEmpleado;
    var NoEmpleado;
    var anio;
    var Tipoperiodo;
    var datosPeriodo;
    var datainformations;
    var rowscounts = 0;
    ///Listbox de Empresas 

    $("#jqxInput").jqxInput({ placeHolder: " Nombre del Empleado", width: 350, height: 30, minLength: 1 });
   

    FListadoEmpresa = () => {

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
                    if (valorChekFint.checked == false) {
                        if (data[i].sNominaCerrada == "True") {
                            document.getElementById("PeridoNom").innerHTML += `<option value='${data[i].iId}'>${data[i].iPeriodo} Fecha del: ${data[i].sFechaInicio} al ${data[i].sFechaFinal}</option>`;
                        }

                    }
                    if (valorChekFint.checked == true) {
                        
                            document.getElementById("PeridoNom").innerHTML += `<option value='${data[i].iId}'>${data[i].iPeriodo} Fecha del: ${data[i].sFechaInicio} al ${data[i].sFechaFinal}</option>`;
                        

                    }
                  
                    
                }
            },
        });
        $("#jqxInput").empty();
        $("#jqxInput").jqxInput({ source: null, placeHolder: " Nombre del Empleado", displayMember: "sNombreCompleto", valueMember: "iIdEmpleado", width: 350, height: 30, minLength: 1 });

    });

    $('#PeridoNom').change(function () {
        btnPDFms.style.visibility = 'hidden';
        btnXmlms.style.visibility = 'hidden';

         IdEmpresa = EmpresaNom.value;
         Tipodeperiodo = TipodePerdioRec.value;
         var periodo = PeridoNom.options[PeridoNom.selectedIndex].text;
         if (PeridoNom.value == 0) {
             $("#jqxInput").empty();
             $("#jqxInput").jqxInput({ source: null, placeHolder: "Nombre del Empleado", displayMember: "sNombreCompleto", valueMember: "iIdEmpleado", width: 350, height: 30, minLength: 1 });
           
             
         }
         if (periodo != "Selecciona") {
             separador = " ",
             limite = 2,
             arreglosubcadena = periodo.split(separador, limite);

             const dataSend = { iIdEmpresa: IdEmpresa, TipoPeriodo: Tipodeperiodo, periodo: arreglosubcadena[0], Anio: anoNom.value };
             if (CheckFiniquito.checked == true) {
                 btnPDFms.style.visibility = 'hidden';
                 btnXmlms.style.visibility = 'hidden';
                 $.ajax({
                     url: "../Empleados/ListEmpleadoFin",
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
                             $("#jqxInput").jqxInput({ source: dataAdapter, placeHolder: " Nombre del Empleado", displayMember: "sNombreCompleto", valueMember: "iIdEmpleado", width: 350, height: 30, minLength: 1 });
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
                             $("#jqxInput").jqxInput({ source: null, placeHolder: " Nombre del Empleado", displayMember: "sNombreCompleto", valueMember: "iIdEmpleado", width: 350, height: 30, minLength: 1 });
                         }

                     }
                 });



             }
             if (CheckFiniquito.checked == false) {

                 btnPDFms.style.visibility = 'visible';
                 btnXmlms.style.visibility = 'visible';
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
                             $("#jqxInput").jqxInput({ source: dataAdapter, placeHolder: " Nombre del Empleado", displayMember: "sNombreCompleto", valueMember: "iIdEmpleado", width: 350, height: 30, minLength: 1 });
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
                             $("#jqxInput").jqxInput({ source: null, placeHolder: " Nombre del Empleado", displayMember: "sNombreCompleto", valueMember: "iIdEmpleado", width: 350, height: 30, minLength: 1 });
                         }

                     }
                 });
             }
            


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
         console.log(dataSend);
         $.ajax({
             url: "../Empleados/EmisorEmpresa",
             type: "POST",
             data: dataSend,
             success: (data) => {
                 console.log(data);
                 EmpresNom = data[0].sNombreComp + ' ' + 'RFC: ' + data[0].sRFCEmpleado + '  en el periodo: '+ periodo;
                 $('#Emisor').html(EmpresNom);                             
             }
         });      
         separador = " ",
         limite = 2,
         arreglosubcadena = periodo.split(separador, limite);
        NoEmpleado;
        const dataSend2 = { iIdEmpresa: IdEmpresa, iIdEmpleado: NoEmpleado, ianio: anoNom.value, iTipodePerido: TipodePerdioRec.value, iPeriodo: arreglosubcadena[0], iespejo: 0 };

         FGridRecibos(dataSend2);

    };

    FGridRecibos = (dataSend2) => {
        $.ajax({
            url: "../Empleados/ReciboNomina",
            type: "POST",
            data: dataSend2,
            success: (data) => {
                console.log(data);
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
                            { text: 'Percepciones', datafield: 'dPercepciones', width: 100 },
                            { text: 'Deducciones ', datafield: 'dDeducciones', width: 100 },
                            { text: 'Saldos', datafield: 'dSaldos', width: 100 },
                            { text: 'Informativos', datafield: 'dInformativos', width: 100 }
                        ]
                    });
            }
        });
        $.ajax({
            url: "../Empleados/TotalesRecibo",
            type: "POST",
            data: dataSend2,
            success: (data) => {
                console.log(data);
                if (data.length > 0) {
                    for (i = 0; i < data.length; i++) {
                        if (data[i].iIdRenglon == 990) {
                            TotalPercep = data[i].dSaldo
                            $('#LaTotalPer').html(TotalPercep);
                        }
                        if (data[i].iIdRenglon == 1990) {

                            TotalDedu = data[i].dSaldo
                            $('#LaTotalDedu').html(TotalDedu);
                        }
                    }
                    Total = TotalPercep - TotalDedu;
                    $('#LaTotalNom').html(Total);

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
        const dataSend = { IdEmpresa: IdEmpresa, sNombreComple: NombreEmpleado, Periodo: datosPeriodo, anios: anio, Tipodeperido: Tipoperiodo };
        $.ajax({
            url: "../Empleados/XMLNomina",
            type: "POST",
            data: dataSend,
            success: function (data) {
                console.log(data);
                console.log(data[0].sMensaje);
                if (data[0].sMensaje != "NorCert") {
                    var url = '\\Archivos\\certificados\\ZipXML.zip';
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
   
        FLimpCamp();
    });

    /// Limpia Campos

    FLimpCamp = () => {
        EmpresaNom.value = 0;
        anoNom.value = "";
        TipodePerdioRec.value = 0;
        PeridoNom.value = 0;
        $("#jqxInput").empty();
        $("#jqxInput").jqxInput('clear');
        $("#jqxInput").jqxInput({ source: null, placeHolder: " Nombre del Empleado", displayMember: "", valueMember: "", width: 350, height: 30, minLength: 1 });



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


    /// selecciona tipo de recibo normal o finiquito 

    FvalorChechFin = () => {
        console.log('Selecciona recibo');
        if (valorChekFint.checked == true) {
            btnPDFms.style.visibility = 'hidden';
            btnXmlms.style.visibility = 'hidden';
            FLimpCamp();
       

        }
        if (valorChekFint.checked == false) {
            btnPDFms.style.visibility = 'hidden';
            btnXmlms.style.visibility = 'hidden';
            FLimpCamp();
          
        }

    }

    CheckFiniquito.addEventListener('click', FvalorChechFin);

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