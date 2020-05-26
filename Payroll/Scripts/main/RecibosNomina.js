$(function () {

    //////////////////////////////////// Recibos Nomina ///////////////////////////////

    /// declaracion de variables globales

    const EmpresaNom = document.getElementById('EmpresaNom');
    const anoNom = document.getElementById('anoNom');
    const EmpleadosNom = document.getElementById('EmpleadosNom');
    const btnFloBuscar = document.getElementById('btnFloBuscar');
    const BtbGeneraPDF = document.getElementById('BtbGeneraPDF');
    const TipodePerdioRec = document.getElementById('TipodePerdioRec');
    const PeridoNom = document.getElementById('PeridoNom');
    const Emisor = document.getElementById('Emisor');
    
    const BtbGeneraXM = document.getElementById('BtbGeneraXML');

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
        const dataSend = { iIdEmpresa: IdEmpresa };
        $("#EmpleadosNom").empty();
        $('#EmpleadosNom').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Empleados/DataListEmpleado",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("EmpleadosNom").innerHTML += `<option value='${data[i].iIdEmpleado}'>${data[i].sNombreCompleto}</option>`;
                }
            }
        });
        const dataSend2 = { IdEmpresa: IdEmpresa };
        $("#TipodePerdioRec").empty();
        $('#TipodePerdioRec').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisTipPeriodo",
            type: "POST",
            data: dataSend2,
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

     ///// ListBox Perido//////

     $('#EmpleadosNom').change(function () {

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

     });

    //// Muestra fecha de inicio y fin de peridodos

     $('#PeridoNom').change(function () {

       
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

     //// FLlena del Grid con los datos de La nomina
    FBuscar = () => {

        FDelettable();

         IdEmpresa = EmpresaNom.value;
         NombreEmpleado = EmpleadosNom.options[EmpleadosNom.selectedIndex].text;
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
         NoEmpleado = EmpleadosNom.value;
         const dataSend2 = { iIdEmpresa: IdEmpresa, iIdEmpleado: NoEmpleado, iPeriodo: arreglosubcadena[0] };
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
                             { name: 'dPercepciones', type: 'string' },
                             { name: 'dDeducciones', type: 'decimal' },
                             { name: 'dSaldos', type: 'decimal' },
                             { name: 'dInformativos', type: 'decimal' }
                         ]
                 };

                 var dataAdapter = new $.jqx.dataAdapter(source);
                 $("#TbRecibosNomina").jqxGrid(
                     {
                         width: 718,

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
     };

     btnFloBuscar.addEventListener('click', FBuscar);

    FGenerarXML = () => {
        IdEmpresa = EmpresaNom.value;
        NombreEmpleado = EmpleadosNom.options[EmpleadosNom.selectedIndex].text;
        IdEmpresa = EmpresaNom.value;
        anio = anoNom.value;
        Tipoperiodo = TipodePerdioRec.value;
        datosPeriodo = PeridoNom.value;       
        const dataSend = { IdEmpresa: IdEmpresa, sNombreComple: NombreEmpleado, Periodo: datosPeriodo, anios: anio, Tipodeperido: Tipoperiodo };
        console.log(dataSend);

        $.ajax({
            url: "../Empleados/XMLNomina",
            type: "POST",
            data: dataSend,
            success: (data) => {

                var url = '\\Archivos\\certificados\\ZipXML.zip';
                window.open(url);
          
            }
        });



     
    };

   

    BtbGeneraXML.addEventListener('click', FGenerarXML);
  
});