$(function () {

      /// declaracion de varibles de tab

    const navEjecuciontab = document.getElementById('nav-Ejecucion-tab');
    const navVisCalculotab = document.getElementById('nav-VisCalculo-tab');
    const navNomCetab = document.getElementById('nav-NomCe-tab');
    const navVisNominatab = document.getElementById('nav-VisNomina-tab');
    const navNomPeritab = document.getElementById('nav-NomPeri-tab');

    Ftabopcion1 = () => {

        //btnFloGuardar.style.visibility = 'visible';
        btnFloLimpiar.style.visibility = 'visible';
        btnFloEjecutar.style.visibility = 'visible';
        btnFloBuscar.style.visibility = 'hidden';

    };
    Ftabopcion2 = () => {

        btnFloGuardar.style.visibility = 'hidden';
        btnFloEjecutar.style.visibility = 'hidden';
        btnFloBuscar.style.visibility = 'hidden';
        btnFloLimpiar.style.visibility = 'hidden';
    };
    Ftabopcion3 = () => {

        btnFloGuardar.style.visibility = 'hidden';
        btnFloEjecutar.style.visibility = 'hidden';
        btnFloBuscar.style.visibility = 'hidden';
        btnFloLimpiar.style.visibility = 'hidden';
        FDelettable();
    };
    FTanopcion4 = () => {
        btnFloGuardar.style.visibility = 'hidden';
        btnFloEjecutar.style.visibility = 'hidden';
        btnFloLimpiar.style.visibility = 'hidden';
        btnFloBuscar.style.visibility = 'visible';
    };
    FTanopcion5 = () => {
        btnFloGuardar.style.visibility = 'hidden';
        btnFloEjecutar.style.visibility = 'hidden';
        btnFloLimpiar.style.visibility = 'hidden';
        btnFloBuscar.style.visibility = 'hidden';
    };

    // Declaracion de variables

    const TxtBInicioClculo = document.getElementById('TxtBInicioClculo');
    const TxtBFinClculo = document.getElementById('TxtBFinClculo');



    const DefinicionCal = document.getElementById('DefinicionCal');
    const TipoPeridoCal = document.getElementById('TipoPeridoCal');
    const PeriodoCal = document.getElementById('PeriodoCal');
    const EmpresaCal = document.getElementById('EmpresaCal');

    const LaTotalPer = document.getElementById('LaTotalPer');
    const PercepCal = document.getElementById('PercepCal');
    const LaTotDeduc = document.getElementById('LaTotDeduc');
    const DeduCal = document.getElementById('DeduCal');
    const Latotal = document.getElementById('Latotal');
    const totalCal = document.getElementById('totalCal');
   
    const btnPdfCal = document.getElementById('btnPdfCal');

    const btnPdfCal2 = document.getElementById('btnPdfCal2');

    const TbAñoNoCe = document.getElementById('TbAñoNoCe');
    const TipoPeriodoNoCe = document.getElementById('TipoPeriodoNoCe');
    const PeridoEjeNomCe = document.getElementById('PeridoEjeNomCe');
    const PercepCalNomCe = document.getElementById('PercepCalNomCe');
    const deduCalNomCe = document.getElementById('deduCalNomCe');
    const TotalNomCe = document.getElementById('TotalNomCe');
    const LaTotalPerNoCe = document.getElementById('LaTotalPerNoCe');
    const LadeduCalNomCe = document.getElementById('LadeduCalNomCe');
    const LaTotalNomCe = document.getElementById('LaTotalNomCe');
    const LaEmpresaNoCe = document.getElementById('LaEmpresaNoCe');
    const EmpresaNoCe = document.getElementById('EmpresaNoCe');
    const EmpresaNom = document.getElementById('EmpresaNom');
    const BntBusRecibo = document.getElementById('btnFloBuscar');
    const ChekEnFirme = document.getElementById('ChekEnFirme');



    const Empleadoseje = document.getElementById('Empleadoseje');
    const dropEmpledos = document.getElementById('DropLitEmple');
    //const LaEmplea = document.getElementById('LaEmpleado');
    //const switchButtonEmp = document.getElementById('switchButtonEmple');
    const checkedItemsLog = document.getElementById('checkedItemsLog');
    const CheckRecibo2 = document.getElementById('CheckRecibo2');
    const btnFloActualiza = document.getElementById('btnFloActualiza');
    const Tbtotal = document.getElementById('Tbtotal');
    const LaTotal = document.getElementById('LaTotal');

    const CheckXempleado = document.getElementById('CheckXempleado');
    const LaCheckXEmpleado = document.getElementById('LaCheckXEmpleado');
    const btnFloLimpiar = document.getElementById('btnFloLimpiar');


    //const btnFloCerrarNom = document.getElementById('btnFloCerrarNom');
    var ValorChek = document.getElementById('ChNCerrada');
    var valorCheckRec = document.getElementById('CheckRecibo2');
    var valorCheckXempleado = document.getElementById('CheckXempleado');
    var ValorChekEnFirme = document.getElementById('ChekEnFirme');
    



    var DatoEjeCerrada;
    var IdDropList = 0;
    var IdDropList2;
    var AnioDropList;
    var Tipoperiodocal;
    var TipodePeridoDroplip;
    var periodo;
    var empresa;
    var RowsGrid;
    var exitRow;
    var opTab = 1;
    var RosTabCountCalculo;
    var RosTabCountCalculo2;
    var NombreEmpleado;
    var NoEmpleado;
    var CheckCalculoEmpresa = 0;
    var checkCalculoEmplado = 0;
    var checkedItemsIdEmpleados = "";







                              /// Tab Ejecucion 

    const LanombreDef = document.getElementById('LanombreDef');
    const jqxdropdown = document.getElementById('jqxdropdownbutton');
    const btnFloGuardar = document.getElementById('btnFloGuardar');
    const btnFloEjecutar = document.getElementById('btnFloEjecutar');
    const CheckXEmpresa = document.getElementById('CheckXEmpresa');
    const TbAño = document.getElementById('TbAño');
    const NombEmpre = document.getElementById('NombEmpre');
    const TxbTipoPeriodo = document.getElementById('TxbTipoPeriodo');
    const PeridoEje = document.getElementById('PeridoEje');
    const EjeEmpresa = document.getElementById('EjeEmpresa');


    var empresas = "";
    var seconds = 0;
    var cantidad;
    var valorCeckxempresa = document.getElementById('CheckXEmpresa');

    btnFloEjecutar.value = 0;
    /* desaparece botones de ejecucion dependiendo el tab que se eligan */

    /// quita logo de carga 

    $('#jqxLoader').jqxLoader('close');

    $("#jqxdropdownbutton").jqxDropDownButton({
        width: 600, height: 30
    });

    //// llenad el grid de Definicion 

   FLlenaGrid = () => {
        //seconds = 15;
        //clearInterval(interval);
        //$("#timerNotification").jqxNotification("closeLast");
        //$(".timer").text(60);
        for (var i = 0; i <= RowsGrid; i++) {

            $("#TpDefinicion").jqxGrid('deleterow', i);
        }

        var opDeNombre = "Selecciona"; /*EjeNombreDef.options[EjeNombreDef.selectedIndex].text*/;
        var opDeCancelados = 2;
        const dataSend = { sNombreDefinicion: opDeNombre, iCancelado: opDeCancelados };
        $.ajax({
            url: "../Nomina/TpDefinicionNominaQry",
            type: "POST",
            data: dataSend,
            success: (data) => {
                if (data.length > 0) {
                    RowsGrid = data.length;
                }
                var source =
                {
                    localdata: data,
                    datatype: "array",
                    datafields:
                        [
                            { name: 'iIdDefinicionhd', type: 'int' },
                            { name: 'sNombreDefinicion', type: 'string' },
                            { name: 'sDescripcion', type: 'string' },
                            { name: 'iAno', type: 'int' },
                            { name: 'iCancelado', type: 'string' },
                        ],
                    datatype: "array",
                    updaterow: function (rowid, rowdata) {
                    }
                };
                var dataAdapter = new $.jqx.dataAdapter(source);
                $("#TpDefinicion").jqxGrid({
                    width: 550,
                    source: dataAdapter,
                    columnsresize: true,
                    columns: [
                        { text: 'No. Registro', datafield: 'iIdDefinicionhd', width: 50 },
                        { text: 'Nombre de Definición', datafield: 'sNombreDefinicion', width: 100 },
                        { text: 'Descripción ', datafield: 'sDescripcion', whidth: 300 },
                        { text: 'Año', datafield: 'iAno', whidt: 50 },
                        { text: 'Cancelado', datafield: 'iCancelado', whidt: 50 },
                    ]
                });
            },
        });
    };
   FLlenaGrid();

    // seleccion de linea de grip y la guarda en el droplist y carga los datos de tipo de perio y llena el drop de periodo

    $("#TpDefinicion").on('rowselect', function (event) {
        btnFloEjecutar.value = 0;
        seconds = 0;
        $('#jqxLoader').jqxLoader('close');
        var args = event.args;
        var row = $("#TpDefinicion").jqxGrid('getrowdata', args.rowindex);
        IdDropList = row.iIdDefinicionhd;
        AnioDropList = row.iAno;
        DefinicionCal.value = row.iIdDefinicionhd + row.sNombreDefinicion;
        var dropDownContent = '<div id="2" style="position: relative; margin-left: 3px; margin-top: 6px;">' + row['iIdDefinicionhd'] + ' ' + row['sNombreDefinicion'] + '</div>';

        /// llena el drop de finicion con la selecion del droplist de definicion 

        $("#jqxdropdownbutton").jqxDropDownButton('setContent', dropDownContent);
        TbAño.value = AnioDropList;
        const dataSend = { IdDefinicionHD: IdDropList, iperiodo: 0 };
        /// comprueba si la definicion selecionada esta guardada en tbCalculos Hd

        const dataSend3 = { iIdDefinicionHd: IdDropList, iperiodo: 0, NomCerr: 0, Anio: AnioDropList };
        $.ajax({
            url: "../Nomina/CompruRegistroExit",
            type: "POST",
            data: dataSend3,
            success: (Result) => {
                if (Result.Result[0].iIdCalculosHd == 1) {
           
                    btnFloEjecutar.style.visibility = 'visible';
                    btnFloGuardar.style.visibility = 'hidden';
                     /// tipo de periodo de la definicion

                    Tipoperiodocal = Result.LTP[0].iId + " " + Result.LTP[0].sValor;
                    TxbTipoPeriodo.value = Result.LTP[0].iId + " " + Result.LTP[0].sValor;
                    TipoPeridoCal.value = Result.LTP[0].iId + " " + Result.LTP[0].sValor;
                     /// ingresa el periodo
                    if (Result.LPe[0].sMensaje == "success") {
                      $("#PeridoEje").empty();
                      document.getElementById("PeridoEje").innerHTML += `<option value='${Result.LPe[0].iId}'>${Result.LPe[0].iPeriodo} Fecha del: ${Result.LPe[0].sFechaInicio} al ${Result.LPe[0].sFechaFinal}</option>`;
                      periodo = Result.LPe[0].iPeriodo;
                      PeriodoCal.value = Result.LPe[0].iPeriodo;
                        /// si tiene calculos la definicion del periodo actual  los muestra  
                       var empresa = 0;
                       FllenaCalculos(periodo, empresa, Tipoperiodocal);
                    }
                    if (Result.LPe[0].sMensaje == "error") {

                        fshowtypealert('Ejecucion', 'Periodo no existe favor de crearlo ', 'warning')
                    }
                }
                if (Result.Result[0].iIdCalculosHd == 0) {
                   
                    btnFloGuardar.style.visibility = 'visible';
                    btnFloEjecutar.style.visibility = 'hidden';
                }
            },
        });
        LisEmpresa(IdDropList);

    });

    //$("#TpDefinicion").jqxGrid('selectrow', 0);

    // Limpa campos de tab Ejecucion

    FLimpiaCamp = () => {

        $("#2").empty();
        $("#TpDefinicion").jqxGrid('clearselection');
        $("#PeridoEje").empty();
        $('#PeridoEje').append('<option value="0" selected="selected">Selecciona</option>');
        TbAño.value = "";
        TxbTipoPeriodo.value = "";
        ValorChek.checked = false;
        if (valorCeckxempresa.checked == true) {
            $("#CheckXEmpresa").click();
        }

    };
    btnFloLimpiar.addEventListener('click', FLimpiaCamp);


    /// llena tabla de calculo
    FllenaCalculos = (periodo, empresa, Tperiodo) => {

        $('#jqxLoader').jqxLoader('close');
        btnFloEjecutar.style.visibility = 'visible';
        seconds = 15;
        var empresaid = empresa;
        // borrar por fila 
        for (var i = 0; i <= RosTabCountCalculo; i++) {
            $("#TbCalculos").jqxGrid('deleterow', i);
        }
        if (Tperiodo != "0") {
            var tipoPeriodo = Tperiodo
            separador = " ",
                limite = 2,
                arreglosubcadena = tipoPeriodo.split(separador, limite);
        }
        if (Tperiodo == "0") {
            var tipoPeriodo = TxbTipoPeriodo.value;
            separador = " ",
                limite = 2,
                arreglosubcadena = tipoPeriodo.split(separador, limite);
        }
        IdDropList;
        const dataSend = { iIdCalculosHd: IdDropList, iTipoPeriodo: arreglosubcadena[0], iPeriodo: periodo, idEmpresa: empresaid, Anio: TbAño.value };
        const dataSend2 = { iIdCalculosHd: IdDropList, iTipoPeriodo: arreglosubcadena[0], iPeriodo: periodo, idEmpresa: empresaid, anio: TbAño.value };
        var per;
        var dedu;
        var total;
        $.ajax({

            url: "../Nomina/ListTpCalculoln",
            type: "POST",
            data: dataSend,
            success: (Result) => {
                RosTabCountCalculo = Result.Result.length;
                var dato = Result.Result[0].sMensaje;
                console.log(dato);
                if (dato == "No hay datos") {
                    var dato = Result.LProce[0].sMensaje;
                    if (dato == "No hay datos") {
                        btnFloActualiza.style.visibility = 'hidden';
                        btnFloEjecutar.style.visibility = 'visible';
                        fshowtypealert('La definicion', 'No contiene ningun calculo', 'success');
                        $("#nav-VisCalculo-tab").addClass("disabled");
                        $("#nav-VisNomina-tab").addClass("disabled");
                    }
                   

                }
                if (dato == "success") {
                     if (dato == "success") {
                        if (Result.LProce[0].sEstatusJobs == "En Cola") {

                            $('#jqxLoader').jqxLoader('open');
                            btnFloEjecutar.style.visibility = 'hidden';
                            btnFloActualiza.style.visibility = 'visible';
                            $("#nav-VisCalculo-tab").addClass("disabled");
                            $("#nav-VisNomina-tab").addClass("disabled");
                        }
                        if (Result.LProce[0].sEstatusJobs == "Procesando") {
                            $('#jqxLoader').jqxLoader('open');
                            btnFloEjecutar.style.visibility = 'hidden';
                            btnFloActualiza.style.visibility = 'visible';
                            $("#nav-VisCalculo-tab").addClass("disabled");
                            $("#nav-VisNomina-tab").addClass("disabled");
                        }
                        if (Result.LProce[0].sEstatusJobs == "Terminado") {
                            console.log('mensaje de terminado');
                            if (btnFloEjecutar.value > 0) {
                                $("#messageNotification").jqxNotification("open");
                                btnFloEjecutar.value = 0;

                            }
                            btnFloActualiza.style.visibility = 'hidden';
                            $("#nav-VisCalculo-tab").addClass("disabled");
                            $("#nav-VisNomina-tab").addClass("disabled");
                            if (ValorChekEnFirme.checked == true) {

                                FLimpiaCamp();
                            }


                        }
                    }


                    if (empresaid != 0) {
                        PercepCal.style.visibility = 'visible';
                        DeduCal.style.visibility = 'visible';
                        totalCal.style.visibility = 'visible';
                        btnPdfCal.style.visibility = 'visible';
                        LaTotalPer.style.visibility = 'visible';
                        LaTotDeduc.style.visibility = 'visible';

                        for (var i = 0; i < Result.Result.length; i++) {
                            if (Result.Result[i].iIdRenglon == 990) {
                                per = Result.Result[i].dTotal;
                                PercepCal.value = Result.Result[i].sTotal;
                            }
                            if (Result.Result[i].iIdRenglon == 1990) {
                                dedu = Result.Result[i].dTotal;
                                DeduCal.value = Result.Result[i].sTotal;
                                total = per - dedu;
                                total = Math.round(total * 100);
                                total = total / 100;

                            }
                            if (Result.Result[i].iIdRenglon == 9999) {
                                totalCal.value = Result.Result[i].sTotal;

                            }
                        }
                        var dato = Result.LProce[0].sMensaje;
                        if (dato == "success") {
                            console.log('datos' + btnFloEjecutar.value)
                            if (Result.LProce[0].sEstatusJobs == "Terminado") {
                                console.log('datos' + btnFloEjecutar.value);
                                btnFloActualiza.style.visibility = 'hidden';
                                if (btnFloEjecutar.value > 0) {
                                    $("#messageNotification").jqxNotification("open");
                                    btnFloEjecutar.value = 0;
                                }
                            }
                            if (Result.LProce[0].sEstatusJobs == "En Cola") {
                                btnFloActualiza.style.visibility = 'visible';
                                $('#jqxLoader').jqxLoader('open');
                                btnFloEjecutar.value = 1;
                                btnFloEjecutar.style.visibility = 'hidden';

                            }
                            if (Result.LProce[0].sEstatusJobs == "Procesando") {
                                btnFloActualiza.style.visibility = 'visible';
                                $('#jqxLoader').jqxLoader('open');
                                btnFloEjecutar.style.visibility = 'hidden';
                                btnFloEjecutar.value = 1;
                            }
                        }
                        var source =
                        {
                            localdata: Result.Result,
                            datatype: "array",
                            datafields:
                                [

                                    { name: 'iIdRenglon', type: 'int' },
                                    { name: 'sNombreRenglon', type: 'string' },
                                    { name: 'sTotal', type: 'string' },

                                ],
                            updaterow: function (rowid, rowdata) {
                                // synchronize with the server - send update command   
                            }
                        };
                        var dataAdapter = new $.jqx.dataAdapter(source);
                        var buildFilterPanel = function (filterPanel, datafield) {
                            var textInput = $("<input style='margin:5px;'/>");
                            var applyinput = $("<div class='filter' style='height: 25px; margin-left: 20px; margin-top: 7px;'></div>");
                            var filterbutton = $('<span tabindex="0" style="padding: 4px 12px; margin-left: 2px;">Filtrar</span>');
                            applyinput.append(filterbutton);
                            var filterclearbutton = $('<span tabindex="0" style="padding: 4px 12px; margin-left: 5px;">Limpiar</span>');
                            applyinput.append(filterclearbutton);
                            filterPanel.append(textInput);
                            filterPanel.append(applyinput);
                            filterbutton.jqxButton({ theme: exampleTheme, height: 20 });
                            filterclearbutton.jqxButton({ theme: exampleTheme, height: 20 });
                            var dataSource =
                            {
                                localdata: adapter.records,
                                datatype: "array",
                                async: false
                            };
                            var dataadapter = new $.jqx.dataAdapter(dataSource,
                                {
                                    autoBind: false,
                                    autoSort: true,
                                    autoSortField: datafield,
                                    async: false,
                                    uniqueDataFields: [datafield]
                                });
                            var column = $("#TbCalculos").jqxGrid('getcolumn', datafield);
                            textInput.jqxInput({ theme: exampleTheme, placeHolder: "Enter " + column.text, popupZIndex: 9999999, displayMember: datafield, source: dataadapter, height: 23, width: 175 });
                            textInput.keyup(function (event) {
                                if (event.keyCode === 13) {
                                    filterbutton.trigger('click');
                                }
                            });
                            filterbutton.click(function () {
                                var filtergroup = new $.jqx.filter();
                                var filter_or_operator = 1;
                                var filtervalue = textInput.val();
                                var filtercondition = 'contains';
                                var filter1 = filtergroup.createfilter('stringfilter', filtervalue, filtercondition);
                                filtergroup.addfilter(filter_or_operator, filter1);
                                // add the filters.
                                $("#TbCalculos").jqxGrid('addfilter', datafield, filtergroup);
                                // apply the filters.
                                $("#TbCalculos").jqxGrid('applyfilters');
                                $("#TbCalculos").jqxGrid('closemenu');
                            });
                            filterbutton.keydown(function (event) {
                                if (event.keyCode === 13) {
                                    filterbutton.trigger('click');
                                }
                            });
                            filterclearbutton.click(function () {
                                $("#TbCalculos").jqxGrid('removefilter', datafield);
                                // apply the filters.
                                $("#TbCalculos").jqxGrid('applyfilters');
                                $("#TbCalculos").jqxGrid('closemenu');
                            });
                            filterclearbutton.keydown(function (event) {
                                if (event.keyCode === 13) {
                                    filterclearbutton.trigger('click');
                                }
                                textInput.val("");
                            });
                        };
                        $("#TbCalculos").jqxGrid({
                            width: 600,
                            height: 325,
                            source: dataAdapter,
                            columnsresize: true,
                            source: dataAdapter,
                            columnsresize: true,
                            filterable: true,
                            sortable: true,
                            //autoheight: true,
                            //autowidth:true,
                            //columns: columns,
                            sortable: true,
                            filterable: true,
                            altrows: true,
                            sortable: true,
                            ready: function () {
                            },
                            columns: [
                                { text: 'IdREnglon', datafield: 'iIdRenglon', width: 100 },
                                { text: 'Renglon', datafield: 'sNombreRenglon', width: 300 },
                                { text: 'Total ', datafield: 'sTotal', whidth: 200 },

                            ]
                        });

                    };    
                    if (empresaid == 0) {
                        TipodePeridoDroplip = TxbTipoPeriodo.value;
                        separador = " ",
                            limite = 2,
                            arreglosubcadena3 = TipodePeridoDroplip.split(separador, limite);
                        $("#EmpresaCal").empty();
                        $('#EmpresaCal').append('<option value="0" selected="selected">Selecciona</option>');
                        $("#EmpresaNom").empty();
                        
                        if (Result.LisEmpreCal.length > 0)
                            for (i = 0; i < Result.LisEmpreCal.length; i++) {
                                document.getElementById("EmpresaCal").innerHTML += `<option value='${Result.LisEmpreCal[i].iIdEmpresa}'>${Result.LisEmpreCal[i].iIdEmpresa}  ${Result.LisEmpreCal[i].sNombreEmpresa} </option>`;
                                document.getElementById("EmpresaNom").innerHTML += `<option value='${Result.LisEmpreCal[i].iIdEmpresa}'>${Result.LisEmpreCal[i].iIdEmpresa}  ${Result.LisEmpreCal[i].sNombreEmpresa} </option>`;
                            }
                        var periodo = PeridoEje.options[PeridoEje.selectedIndex].text;
                        if (periodo == "Selecciona") {
                            $("#jqxInput").empty();
                            $("#jqxInput").jqxInput({ source: null, placeHolder: "Nombre del Empleado", displayMember: "sNombreCompleto", valueMember: "iIdEmpleado", width: 250, height: 30, minLength: 1 });
                        }
                        if (periodo != "Selecciona") {
                            separador = " ",
                                limite = 2,
                                arreglosubcadena2 = periodo.split(separador, limite);
                            const dataSend5 = { iIdEmpresa: Result.LisEmpreCal[0].iIdEmpresa, TipoPeriodo: arreglosubcadena[0], periodo: arreglosubcadena2[0], Anio: TbAño.value };
                            $.ajax({
                                url: "../Empleados/DataListEmpleado",
                                type: "POST",
                                data: dataSend5,
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
                        PercepCal.style.visibility = 'hidden';
                        DeduCal.style.visibility = 'hidden';
                        totalCal.style.visibility = 'hidden';
                        btnPdfCal.style.visibility = 'hidden';
                        LaTotalPer.style.visibility = 'hidden';
                        LaTotDeduc.style.visibility = 'hidden';
                        Latotal.style.visibility = 'hidden';

                    }

                    $("#nav-VisCalculo-tab").removeClass("disabled");
                    $("#nav-VisNomina-tab").removeClass("disabled");
                }
            },
        });

    };

    /// llena el drop de empresa en la pantalla ejecucion

    LisEmpresa = (IdDrop) => {
        empresas = "";
        const dataSend2 = { iIdCalculosHd: IdDrop, iTipoPeriodo: 0, iPeriodo: 0, idEmpresa: 0, anio: 0 };
        $("#EjeEmpresa").empty();
        $('#EjeEmpresa').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/EmpresaCal",
            type: "POST",
            data: dataSend2,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("EjeEmpresa").innerHTML += `<option value='${data[i].iIdEmpresa}'>${data[i].iIdEmpresa}  ${data[i].sNombreEmpresa} </option>`;
                    empresa = empresa + "," + data[i].iIdEmpresa;
                }
            },
        });
    };

    // Funcion de guardar 
    Fguardar = () => {
        console.log('Guardar');
        const dataSend = { iIdDefinicionHd: IdDropList };
        $.ajax({
            url: "../Nomina/ExitPerODedu",
            type: "POST",
            data: dataSend,
            success: (data) => {
                if (data[0] == 1) {
                    DatoEjeCerrada = 0;
                    if (IdDropList > 0) {
                        const dataSend = { iIdDefinicionHd: IdDropList };
                        $.ajax({
                            url: "../Nomina/CompruRegistroExitdef",
                            type: "POST",
                            data: dataSend,
                            success: (data) => {

                                if (data[0].iIdCalculosHd == 1) {
                                    exitRow = "1";
                                }
                                if (data[0].iIdCalculosHd == 0) {
                                    exitRow = "0";
                                }
                                if (exitRow == "1") {

                                }
                                if (exitRow == "0") {
                                    const dataSend2 = { iIdDefinicionHd: IdDropList, iNominaCerrada: DatoEjeCerrada };
                                    $.ajax({
                                        url: "../Nomina/InsertDatTpCalculos",
                                        type: "POST",
                                        data: dataSend2,
                                        success: (data) => {
                                            console.log('termino');
                                            if (data.sMensaje == "success") {
                                                $("#2").empty();
                                                $("#TpDefinicion").jqxGrid('clearselection');
                                                $("#PeridoEje").empty();
                                                $('#PeridoEje').append('<option value="0" selected="selected">Selecciona</option>');
                                                TbAño.value = "";
                                                TxbTipoPeriodo.value = "";
                                                ValorChek.checked = false;


                                                fshowtypealert('Registro correcto!', 'Calculo guardado', 'success');

                                            }
                                            else {
                                                fshowtypealert('Error', 'Contacte a sistemas', 'error');

                                            }
                                        },
                                        error: function (jqXHR, exception) {
                                            fcaptureaerrorsajax(jqXHR, exception);
                                        }
                                    });

                                }
                            },
                        });

                    }
                    else {
                        fshowtypealert('Selecciona un nombre definición !', '', 'warning');
                    }


                }
                if (data[0] == 0) {

                    fshowtypealert('Ejecución', 'La definicon debe tener por lo menos una percepcion o una deduccion para ser registrada y ejecutada', 'warning')
                }
            },
        });
    };
    btnFloGuardar.addEventListener('click', Fguardar);

    ///Cehca si termina de realizar la ejecucion

    Frefresh = () => {
        btnFloEjecutar.style.visibility = 'visible';
        btnFloEjecutar.value = 1;
        $('#jqxLoader').jqxLoader('close');
        TipodePeridoDroplip = TxbTipoPeriodo.value;
        periodo = PeridoEje.options[PeridoEje.selectedIndex].text;
        separador = " ",
        limite = 2,
        arreglosubcadena2 = periodo.split(separador, limite);
        FllenaCalculos(arreglosubcadena2[0], 0, TipodePeridoDroplip);
    };

    btnFloActualiza.addEventListener('click', Frefresh)


    /* Proceso para cerrar nomina  */

    FValorChec = () => {

        if (ValorChekEnFirme.checked == false) {
            if (ValorChek.checked == true) {

                Swal.fire({
                    title: 'Seguro que deseas cerrar la Nomina?',
                    text: "Si es asi da clic en aceptar!",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Aceptar!'
                }).then((result) => {
                    if (result.value) {
                        console.log('proceso de cerrar nomina');
                        periodo = PeridoEje.options[PeridoEje.selectedIndex].text;
                        separador = " ",
                            limite = 2,
                            arreglosubcadena2 = periodo.split(separador, limite);
                        periodo = PeridoEje.options[PeridoEje.selectedIndex].text;
                        var tipPer = TxbTipoPeriodo.value
                        separador = " ",
                            limite = 2,
                            arreglosubcadena3 = tipPer.split(separador, limite);

                        const dataSend = { iIdCalculosHd: IdDropList, iTipoPeriodo: arreglosubcadena3[0], iPeriodo: arreglosubcadena2[0], idEmpresa: 0, Anio: TbAño.value };

                        var rows;
                        $.ajax({
                            url: "../Nomina/ListTpCalculoln",
                            type: "POST",
                            data: dataSend,
                            success: (Result) => {
                                if (Result.Result[0].sMensaje == "success") {
                                    rows = Result.Result.length;

                                    if (rows > 0) {

                                        Swal.fire(
                                            'Nomina!',
                                            'Cerrada.',
                                            'success'
                                        );
                                        periodo = PeridoEje.options[PeridoEje.selectedIndex].text;
                                        separador = " ",
                                            limite = 2,
                                            arreglosubcadena = periodo.split(separador, limite);
                                        const dataSend3 = { iIdDefinicionHd: IdDropList, iPerido: arreglosubcadena[0], iNominaCerrada: 1, Anio: TbAño.value, IdTipoPeriodo: 0, IdEmpresa: 0 };

                                        $.ajax({
                                            url: "../Nomina/UpdateCInicioFechasPeriodo",
                                            type: "POST",
                                            data: dataSend3,
                                            success: (data) => {

                                                if (data.sMensaje == "success") {
                                                    FLimpiaCamp();

                                                }
                                                else {
                                                    fshowtypealert('Error', 'Contacte a sistemas', 'error');

                                                }
                                            },

                                            error: function (jqXHR, exception) {
                                                fcaptureaerrorsajax(jqXHR, exception);
                                            }
                                        });

                                    }
                                }
                                else {
                                    ValorChek.checked = false;
                                    console.log('no hay calculos');
                                    Swal.fire('La Nomina!', 'No contiene ningun calculo , no se puede cerrar', 'warning');

                                }
                            }
                        });
                    }
                    else {
                        ValorChek.checked = false;

                    }
                });

            }
            if (ValorChek.checked == false) {

                //Swal.fire({
                //    title: 'Seguro que deseas abrir la Nomina?',
                //    text: "Si es asi da clic en aceptar!",
                //    icon: 'warning',
                //    showCancelButton: true,
                //    confirmButtonColor: '#3085d6',
                //    cancelButtonColor: '#d33',
                //    confirmButtonText: 'Aceptar!'
                //}).then((result) => {
                //    if (result.value) {
                //        Swal.fire(
                //            'Nomina!',
                //            'Abierta.',
                //            'success'
                //        );
                //        periodo = PeridoEje.options[PeridoEje.selectedIndex].text;
                //        separador = " ",
                //            limite = 2,
                //            arreglosubcadena = periodo.split(separador, limite);
                //        const dataSend3 = { iIdDefinicionHd: IdDropList, iPerido: arreglosubcadena[0], iNominaCerrada: 0 };
                //        console.log('nominacerrada');
                //        console.log(dataSend3);
                //        $.ajax({
                //            url: "../Nomina/UpdateCInicioFechasPeriodo",
                //            type: "POST",
                //            data: dataSend3,
                //            success: (data) => {

                //                if (data.sMensaje == "success") {
                //                    console.log(data);
                //                    $("#2").empty();
                //                    $("#TpDefinicion").jqxGrid('clearselection');    
                //                }
                //                else {
                //                    fshowtypealert('Error', 'Contacte a sistemas', 'error');

                //                }
                //            },

                //            error: function (jqXHR, exception) {
                //                fcaptureaerrorsajax(jqXHR, exception);
                //            }
                //        });
                //    }
                //});


            }
        }

        if (ValorChekEnFirme.checked == true) {

            Swal.fire({
                title: 'Seguro que deseas correr la Nomina en firme?',
                text: "Si es asi da clic en aceptar!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Aceptar!'
            }).then((result) => {
                if (result.value) {
                    console.log('proceso de cerrar nomina');
                    periodo = PeridoEje.options[PeridoEje.selectedIndex].text;
                    separador = " ",
                        limite = 2,
                        arreglosubcadena2 = periodo.split(separador, limite);
                    periodo = PeridoEje.options[PeridoEje.selectedIndex].text;
                    var tipPer = TxbTipoPeriodo.value
                    separador = " ",
                        limite = 2,
                        arreglosubcadena3 = tipPer.split(separador, limite);

                    const dataSend = { iIdCalculosHd: IdDropList, iTipoPeriodo: arreglosubcadena3[0], iPeriodo: arreglosubcadena2[0], idEmpresa: 0, Anio: TbAño.value };

                    var rows;
                    $.ajax({
                        url: "../Nomina/ListTpCalculoln",
                        type: "POST",
                        data: dataSend,
                        success: (Result) => {
                            if (Result.Result[0].sMensaje == "success") {
                                rows = Result.Result.length;

                                if (rows > 0) {

                                    Swal.fire(
                                        'Nomina!',
                                        'Realizando los calculos en firme',
                                        'success'
                                    );
                                    btnFloEjecutar.value = 1;
                                    IdDropList;
                                    AnioDropList;
                                    TipodePeridoDroplip = TxbTipoPeriodo.value;
                                    separador = " ",
                                    limite = 2,
                                    arreglosubcadena3 = TipodePeridoDroplip.split(separador, limite);
                                    periodo = PeridoEje.options[PeridoEje.selectedIndex].text;
                                    separador = " ",
                                    limite = 2,
                                    arreglosubcadena = periodo.split(separador, limite);
                                    const dataSend3 = { iIdDefinicionHd: IdDropList, iPerido: arreglosubcadena[0], iNominaCerrada: 1, Anio: TbAño.value, IdTipoPeriodo: 0, IdEmpresa: 0 };
                                    var dataSend2 = { IdDefinicionHD: IdDropList, anio: AnioDropList, iTipoPeriodo: arreglosubcadena3[0], iperiodo: arreglosubcadena2[0], iIdempresa: 0, iCalEmpleado: checkCalculoEmplado };

                                    $.ajax({
                                        url: "../Nomina/UpdateCInicioFechasPeriodo",
                                        type: "POST",
                                        data: dataSend3,
                                        success: (data) => {

                                            if (data.sMensaje == "success") {
                                                console.log('manda a ejecuccion 23')
                                                $.ajax({
                                                    timeout: 5000,
                                                    url: "../Nomina/ProcesosPots",
                                                    type: "POST",
                                                    data: dataSend2,
                                                    success: (data) => {
                                                        FllenaCalculos(arreglosubcadena2[0], 0, TipodePeridoDroplip);
                                                      
                                                    }
                                                });                   
                                            }
                                            else {
                                                fshowtypealert('Error', 'Contacte a sistemas', 'error');

                                            }
                                        },

                                        error: function (jqXHR, exception) {
                                            fcaptureaerrorsajax(jqXHR, exception);
                                        }
                                    });

                                }
                            }
                            else {
                                ValorChek.checked = false;
                                console.log('no hay calculos');
                                Swal.fire('La Nomina!', 'No contiene ningun calculo , no se puede cerrar', 'warning');

                            }
                        }
                    });
                }
                else {
                    ValorChek.checked = false;

                }
            });
        };
        
    };
    ChNCerrada.addEventListener('click', FValorChec);

    ChekEnFirme.addEventListener('click', FValorChec);
    /*  Procesos de Ejecucion */

    Fejecucion = () => {
        
        btnFloEjecutar.value = 1;
        IdDropList;
        AnioDropList;
        TipodePeridoDroplip = TxbTipoPeriodo.value;
        separador = " ",
            limite = 2,
            arreglosubcadena3 = TipodePeridoDroplip.split(separador, limite);
        periodo = PeridoEje.options[PeridoEje.selectedIndex].text;
        separador = " ",
            limite = 2,

            arreglosubcadena2 = periodo.split(separador, limite);
        const dataSend = { iIdDefinicionHd: IdDropList };
        var dataSend2 = { IdDefinicionHD: IdDropList, anio: AnioDropList, iTipoPeriodo: arreglosubcadena3[0], iperiodo: arreglosubcadena2[0], iIdempresa: 0, iCalEmpleado: checkCalculoEmplado };
        var dataSend3 = { Idempresas: empresa, anio: AnioDropList, Tipodeperido: arreglosubcadena3[0], Periodo: arreglosubcadena2[0], IdDefinicionHD: IdDropList };

        $.ajax({
            url: "../Nomina/ProcesEjecuEsta",
            type: "POST",
            data: dataSend2,
            success: (data) => {
                if (data[0].sEstatusJobs == "0") {
                    if (CheckCalculoEmpresa == 0) {
                        if (IdDropList != 0) {
                            dataSend2 = { IdDefinicionHD: IdDropList, anio: AnioDropList, iTipoPeriodo: arreglosubcadena3[0], iperiodo: arreglosubcadena2[0], iIdempresa: 0, iCalEmpleado: checkCalculoEmplado };
                            FejecutarProceso(dataSend, dataSend2, dataSend3);
                        }
                        else {
                            fshowtypealert("Ejecucion", " Favor de seleccionar una definicion", "warning")
                        }
                    }
                    if (CheckCalculoEmpresa == 1) {
                        if (checkCalculoEmplado == 0) {
                            if (EjeEmpresa.value != 0) {
                                dataSend2 = { IdDefinicionHD: IdDropList, anio: AnioDropList, iTipoPeriodo: arreglosubcadena3[0], iperiodo: arreglosubcadena2[0], iIdempresa: EjeEmpresa.value, iCalEmpleado: checkCalculoEmplado };
                                FejecutarProceso(dataSend, dataSend2, dataSend3);
                            }
                            else {
                                fshowtypealert("Ejecucion", " Favor de seleccionar una empresa", "warning")
                            }
                        }
                        if (checkCalculoEmplado == 1) {
                            var NumItemsEmpleado = checkedItemsIdEmpleados.length;
                            console.log(checkedItemsIdEmpleados)
                            console.log(NumItemsEmpleado)
                            if (NumItemsEmpleado != 0) {
                                const dataSend4 = { IdEmpresa: EjeEmpresa.value, iAnio: AnioDropList, TipoPeriodo: arreglosubcadena3[0], iPeriodo: arreglosubcadena2[0], sIdEmpleados: checkedItemsIdEmpleados };
                                console.log(dataSend4)
                                $.ajax({
                                    url: "../Nomina/SaveEmpleados",
                                    type: "POST",
                                    data: dataSend4,
                                    success: function (data) {
                                        if (data.sMensaje == "success") {

                                            FejecutarProceso(dataSend, dataSend2, dataSend3);
                                        }
                                        if (data.sMensaje == "error") {
                                            fshowtypealert("Ejecucion", " Contacte a sistemas", "warning")

                                        }
                                    }
                                });
                            }
                            else {
                                fshowtypealert("Ejecucion", " Favor de seleccionar por lo menos un empleado", "warning")
                            }
                        }
                    }
                }

                if (data[0].sEstatusJobs == "1") {

                    fshowtypealert("Calculo de Nomina", "se está realizando los calculo de una definicion de nómina espera 10 min y vuelva intentarlo", "warning")

                }
            },
        });
    };
    btnFloEjecutar.addEventListener('click', Fejecucion);

    /////////////////////////////////////////////////

    /* Funcion de ejecucion */

    FejecutarProceso = (dataSend, dataSend2, dataSend3) => {

        console.log('entra aqui');
        //$.ajax({
        //    url: "../Nomina/ExitCalculos",
        //    type: "POST",
        //    data: dataSend3,
        //    success: function (data) {
        //        if (data[0].sMensaje == "success") {
                    $.ajax({
                        url: "../Nomina/CompruRegistroExitdef",
                        type: "POST",
                        data: dataSend,
                        success: (data) => {
                            if (data[0].iIdCalculosHd == 1) {
                                fshowtypealert2("Ejecucion", "Los calculos de la nomina se estan realizando", "success")
                                $.ajax({
                                    timeout: 5000,
                                    url: "../Nomina/ProcesosPots",
                                    type: "POST",
                                    data: dataSend2,
                                    success: (data) => { 
                                       console.log('manda a ejecuccion ' + data)
                                       FllenaCalculos(arreglosubcadena2[0], 0, TipodePeridoDroplip);
                                    }
                                });
                            }

                            if (data[0].iIdCalculosHd == 0) {
                                exitRow = "0";
                                fshowtypealert("Ejecucion", "La definicion de nomina seleccionada no esta guardada", "warning")
                            }

                        },
                    });
                //}
                //if (data[0].sMensaje == "error") {

                //    Swal.fire({
                //        title: 'Ya se realizarón calculos de este periodo  ',
                //        text: "si deseas continuar se perderan los calculos anteriores, ¿deseas continuar?",
                //        icon: 'warning',
                //        showCancelButton: true,
                //        confirmButtonColor: '#3085d6',
                //        cancelButtonColor: '#d33',
                //        confirmButtonText: 'Aceptar!'
                //    }).then((result) => {
                //        if (result.value) {
                //            $.ajax({
                //                url: "../Nomina/CompruRegistroExit",
                //                type: "POST",
                //                data: dataSend,
                //                success: (data) => {

                //                    if (data[0].iIdCalculosHd == 1) {
                //                        fshowtypealert("Ejecucion", "Los calculos de la nomina se estan realizando", "success")
                //                        $.ajax({
                //                            url: "../Nomina/ProcesosPots",
                //                            type: "POST",
                //                            data: dataSend2,
                //                            success: (data) => {
                //                                FllenaCalculos(arreglosubcadena2[0], 0, TipodePeridoDroplip);

                //                            }
                //                        });

                //                    }

                //                    if (data[0].iIdCalculosHd == 0) {
                //                        exitRow = "0";
                //                        fshowtypealert("Ejecucion", "La definicion de nomina seleccionada no esta guardada", "warning")
                //                    }

                //                },
                //            });
                //        }
                //        else {
                //            fshowtypealert("Ejecucion", "Los cálculos de la nómina se han cancelado exitosamente", "success")

                //        }
                //    });

                //}
        //    }
        //});
    };



                     /// tab Calculo

    navEjecuciontab.addEventListener('click', Ftabopcion1);
    navVisCalculotab.addEventListener('click', Ftabopcion2);
    navNomCetab.addEventListener('click', Ftabopcion3);
    navVisNominatab.addEventListener('click', FTanopcion4);
    navNomPeritab.addEventListener('click', FTanopcion5);

               //////////////////////////////


    ///// llena del dropList de empleados 

    $('#EjeEmpresa').change(function () {

        EmpleadoDEmp(EjeEmpresa.value);
    });

    EmpleadoDEmp = (IdEmpresa) => {
        var source = " ";
        const dataSend2 = { iIdEmpresa: IdEmpresa };
        $.ajax({
            url: "../Nomina/ListConIDEmplados",
            type: "POST",
            data: dataSend2,
            success: (data) => {
                source =
                {
                    localdata: data,
                    datatype: "array",
                    datafields:
                        [
                            { name: 'iIdEmpleado', type: 'int' },
                            { name: 'sNombreCompleto', type: 'string' },

                        ],
                    datatype: "array",
                    //updaterow: function (rowid, rowdata) {  
                    //}
                };
                var dataAdapter = new $.jqx.dataAdapter(source);
                $("#DropLitEmple").jqxDropDownList({ checkboxes: true, filterable: true, searchMode: "containsignorecase", selectedIndex: 0, source: dataAdapter, displayMember: "sNombreCompleto", valueMember: "iIdEmpleado", width: 300, height: 30, });



            },
        });
    };

    //////// Filtro de caraturla pro empresa en el tab de vista de calculo

    $('#EmpresaCal').change(function () {

        var idempresa = EmpresaCal.value;
        var perido = PeriodoCal.value;
        Tipoperiodocal = 0;
        FllenaCalculos(periodo, idempresa, Tipoperiodocal);
    });

    ////// selecccion de los empleado de la empresa


       /* muestra calculos de nomina del empleado */

      // funcion del buscador del empleado 
    FListEmpleado = (iIdempresa,iIdTipoPerido, iIdPeriodo) => {
        const ListEmple = { iIdEmpresa: iIdempresa, TipoPeriodo: iIdTipoPerido, periodo: iIdPeriodo, Anio: TbAño.value };
        $.ajax({
            url: "../Empleados/DataListEmpleado",
            type: "POST",
            data: ListEmple,
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


    };

    $('#EmpresaNom').change(function () {

        var tipoPeriodo = TxbTipoPeriodo.value
        separador = " ",
            limite = 2,
            arreglosubcadena = tipoPeriodo.split(separador, limite);
        const dataSend5 = { iIdEmpresa: EmpresaNom.value, TipoPeriodo: arreglosubcadena[0], periodo: PeriodoCal.value, Anio: TbAño.value };
        FListEmpleado(EmpresaNom.value, arreglosubcadena[0], PeriodoCal.value);
    });

    FBusNom = () => {
      
        //FDelettable();
        var TotalPercep = 0;
        var TotalDedu = 0;
        var Total = 0;
        var IdEmpresa = EmpresaNom.value;
        NombreEmpleado ;
        var periodo = PeridoEje.options[PeridoEje.selectedIndex].text;
        separador = " ",
        limite = 2,
        arreglosubcadena = periodo.split(separador, limite);
        NoEmpleado;
        TipodePeridoDroplip = TxbTipoPeriodo.value;
        separador = " ",
        limite = 2,
        arreglosubcadena3 = TipodePeridoDroplip.split(separador, limite);
        const dataSend2 = { iIdEmpresa: IdEmpresa, iIdEmpleado: NoEmpleado, ianio: AnioDropList, iTipodePerido: arreglosubcadena3[0], iPeriodo: arreglosubcadena[0], iespejo:0 };
        FGridCalculos(dataSend2);
        btnFloGuardar.style.visibility = 'hidden';
        btnFloEjecutar.style.visibility = 'hidden';

    };
    
    FGridCalculos = (dataSend2) => {
        
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
                            { name: 'dPercepciones', type: 'number' },
                            { name: 'dDeducciones', type: 'number' },
                            { name: 'dGravados', type: 'number' },
                            { name: 'dExcento', type: 'number' }
                        ]
                };

                var dataAdapter = new $.jqx.dataAdapter(source);
                $("#TbCalculosNom").jqxGrid(
                    {
                        theme: 'bootstrap',
                        width: 700,
                        source: dataAdapter,
                        showfilterrow: true,
                        filterable: true,
                        sortable: true,
                        pageable: true,
                        autoloadstate: false,
                        autosavestate: false,
                        columnsresize: true,
                        showgroupaggregates: true,
                        showstatusbar: true,
                        showaggregates: true,
                        statusbarheight: 25,
                        columns: [
                            { text: 'Concepto', datafield: 'sConcepto', width: 280 },
                            {
                                text: 'Percepciones', datafield: 'dPercepciones', aggregates: ["sum"], width: 100, cellsformat: 'c2', cellsrenderer: function (row, column, value, defaultRender, column, rowData) {

                                    if (value.toString().indexOf("Sum") >= 0) {

                                        return defaultRender.replace("Sum", "Total");

                                    }

                                },
                                aggregatesrenderer: function (aggregates, column, element) {
                                    var renderstring = '<div style=" margin-top: 4px; margin-right:5px; text-align: left; overflow: hidden;">' + "Total" + ': ' + aggregates.sum + '</div>';

                                    return renderstring;

                                }
                            },
                            {
                                text: 'Deducciones ', datafield: 'dDeducciones', aggregates: ["sum"], width: 100, cellsformat: 'c2', cellsrenderer: function (row, column, value, defaultRender, column, rowData) {

                                    if (value.toString().indexOf("Sum") >= 0) {

                                        return defaultRender.replace("Sum", "Total");

                                    }

                                },

                                aggregatesrenderer: function (aggregates, column, element) {

                                    var renderstring = '<div style=" margin-top: 4px; margin-right:5px; text-align:left; overflow: hidden;">' + "Total" + ': ' + aggregates.sum + '</div>';

                                    return renderstring;

                                }
                            },
                            {
                                text: 'Gravado', datafield: 'dGravados', /*aggregates: ["sum"],*/ width: 100, cellsformat: 'c2' /*cellsrenderer: function (row, column, value, defaultRender, column, rowData) {*/

                                //    //if (value.toString().indexOf("Sum") >= 0) {

                                //    //    return defaultRender.replace("Sum", "Total");

                                //    //}

                                //},

                                //aggregatesrenderer: function (aggregates, column, element) {

                                //    var renderstring = '<div style=" margin-top: 4px; margin-right:5px; text-align: left; overflow: hidden;">' + "Total" + ': ' + aggregates.sum + '</div>';

                                //    return renderstring;

                                //}
                            },
                            {
                                text: 'Excento', datafield: 'dExcento', /*aggregates: ["sum"],*/ width: 100, cellsformat: 'c2'
                                //cellsrenderer: function (row, column, value, defaultRender, column, rowData) {

                                //    //if (value.toString().indexOf("Sum") >= 0) {

                                //    //    return defaultRender.replace("Sum", "Total");

                                //    //}

                                //},

                                //aggregatesrenderer: function (aggregates, column, element) {

                                //    var renderstring = '<div style=" margin-top: 4px; margin-right:15px;text-align: left ; overflow: hidden;">' + "Total" + ': ' + aggregates.sum + '</div>';

                                //    return renderstring;

                                //}

                            }

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
                            //$('#LaTotalPer').html(TotalPercep);
                        }
                        if (data[i].iIdRenglon == 1990) {

                            TotalDedu = data[i].dSaldo
                            //$('#LaTotalDedu').html(TotalDedu);
                        }
                    }

                    Tbtotal.style.visibility = 'visible';
                    LaTotal.style.visibility = 'visible';
                    Total = TotalPercep - TotalDedu;
                   Tbtotal.value = formatter.format(Total)

                }
            }
        }); 
    };

    BntBusRecibo.addEventListener('click', FBusNom)

    /// llena  grid de calculos recibo dos
    FRecibo2 = () => {
        if (valorCheckRec.checked == true) {
            const dataSend2 = { iIdEmpresa: EmpresaNom.value, iIdEmpleado: NoEmpleado, ianio: AnioDropList, iTipodePerido: arreglosubcadena3[0], iPeriodo: arreglosubcadena[0], iespejo: 1 };
            FGridCalculos(dataSend2);  
        }
        if (valorCheckRec.checked == false) {
            const dataSend2 = { iIdEmpresa: EmpresaNom.value, iIdEmpleado: NoEmpleado, ianio: AnioDropList, iTipodePerido: arreglosubcadena3[0], iPeriodo: arreglosubcadena[0], iespejo: 0 };
            FGridCalculos(dataSend2);        
        }
    };

    CheckRecibo2.addEventListener('click',FRecibo2)


               /* Borra tabla de Nom */

    FDelettable = () => {
        var datainformations = $('#TbCalculosNom').jqxGrid('getdatainformation');
        var rowscounts = datainformations.rowscount;
        if (rowscounts > 0) {
            for (var i = 0; i <= rowscounts; i++) {

                $("#TbCalculosNom").jqxGrid('deleterow', i);
            }
        }


    };

///////////////////////////////////////////////////////////////////




                             /* Tab Nom Cerradas */


    /* Funcion muestra Grid Con los datos de TPDefinicion en del droplist definicion */

       FLlenaGrid2 = () => {

        for (var i = 0; i <= RowsGrid; i++) {

            $("#TpDefinicion2").jqxGrid('deleterow', i);
        }

        var opDeNombre = "Selecciona"; /*EjeNombreDef.options[EjeNombreDef.selectedIndex].text*/;
        var opDeCancelados = 2;
        $.ajax({
            url: "../Nomina/QryDifinicionPeriodoCerrado",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                if (data.length > 0) {
                    RowsGrid = data.length;
                }
                var source =
                {
                    localdata: data,
                    datatype: "array",
                    datafields:
                        [
                            { name: 'iIdDefinicionhd', type: 'int' },
                            { name: 'sNombreDefinicion', type: 'string' },
                            { name: 'sDescripcion', type: 'string' },
                            { name: 'iAno', type: 'int' },
                            { name: 'iCancelado', type: 'string' },
                        ],
                    datatype: "array",
                    updaterow: function (rowid, rowdata) {
                        // synchronize with the server - send update command   
                    }
                };

                var dataAdapter = new $.jqx.dataAdapter(source);
                //////////////////////////////////////
                $("#TpDefinicion2").jqxGrid({
                    width: 550,
                    height:200,
                    source: dataAdapter,
                    columnsresize: true,
                    columns: [
                        { text: 'No. Registro', datafield: 'iIdDefinicionhd', width: 50 },
                        { text: 'Nombre de Definición', datafield: 'sNombreDefinicion', width: 100 },
                        { text: 'Descripción ', datafield: 'sDescripcion', whidth: 300 },
                        { text: 'Año', datafield: 'iAno', whidt: 50 },
                        { text: 'Cancelado', datafield: 'iCancelado', whidt: 50 },
                    ]
                });
            },
        });
    };
       FLlenaGrid2();
      $("#jqxdropdownbutton2").jqxDropDownButton({
        width: 600, height: 30
    });

                    /*Selesccion de definicion de periodos cerrados  */
     $("#TpDefinicion2").on('rowselect', function (event) {
        console.log('imprime');
        var args2 = event.args;
        var row2 = $("#TpDefinicion2").jqxGrid('getrowdata', args2.rowindex);
        IdDropList2 = row2.iIdDefinicionhd;
        TbAñoNoCe.value = row2.iAno;
        var dropDownContent = '<div id="2" style="position: relative; margin-left: 3px; margin-top: 6px;">' + row2['iIdDefinicionhd'] + ' ' + row2['sNombreDefinicion'] + '</div>';
        $("#jqxdropdownbutton2").jqxDropDownButton('setContent', dropDownContent);
        const dataSend = { IdDefinicionHD: IdDropList2, iperiodo: 0 };
      
                /*  carga el tipo de periodo en pantalla */
        $.ajax({
            url: "../Nomina/TipoPeriodo",
            type: "POST",
            data: dataSend,
            success: (data) => {
                TipoPeriodoNoCe.value = data[0].iId + " " + data[0].sValor;
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

        $("#PeridoEjeNomCe").empty();
        $('#PeridoEjeNomCe').append('<option value="0" selected="selected">Selecciona</option>');
         const dataSend2 = { IdDefinicionHD: IdDropList2, iperiodo: 0, NomCerr: 1, Anio:TbAñoNoCe.value };

        $.ajax({
            url: "../Nomina/ListPeriodoEmpresa",
            type: "POST",
            data: dataSend2,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("PeridoEjeNomCe").innerHTML += `<option value='${data[i].iId}'>${data[i].iPeriodo} Fecha del: ${data[i].sFechaInicio} al ${data[i].sFechaFinal}</option>`;                
                }
            },

        });
    }); 

   //$("#TpDefinicion2").jqxGrid('selectrow', 0);   
  ////////////////////////////////////////////////////////////////////////


    $('#PeridoEjeNomCe').change(function () {
        document.getElementById('Block_calculos').classList.remove("d-none");
        IdDropList2;
        periodo = PeridoEjeNomCe.options[PeridoEjeNomCe.selectedIndex].text;
        separador = " ",
        limite = 2,
        arreglosubcadena = periodo.split(separador, limite);

        /// llenar tabla de calculos

        // borrar por fila 
        for (var i = 0; i <= RosTabCountCalculo2; i++) {

            $("#TbCalculos2").jqxGrid('deleterow', i);
        }
        var empresaid = 0;
        var tipoPeriodo = TipoPeriodoNoCe.value;
        separador = " ",
        limite = 2,
        arreglosubcadena2 = tipoPeriodo.split(separador, limite);
        const dataSend = { iIdCalculosHd: IdDropList2, iTipoPeriodo: arreglosubcadena2[0], iPeriodo: arreglosubcadena[0], idEmpresa: empresaid, Anio: TbAñoNoCe.value };
        console.log(dataSend);
        var per;
        var dedu;
        var total;
        $.ajax({
            url: "../Nomina/ListTpCalculoln",
            type: "POST",
            data: dataSend,
            success: (data) => {
                console.log(data);
                RosTabCountCalculo2 = data.length;
                var dato = data[0].sMensaje;
                if (dato == "No hay datos") {
                    fshowtypealert('Vista de Calculo', 'No contiene ningun calculo', 'warning');    
                }
                if (dato == "success") {
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].iIdRenglon == 990) {
                            per = data[i].dTotal;
                            PercepCalNomCe.style.visibility = 'visible';
                            LaTotalPerNoCe.style.visibility = 'visible';
                            PercepCalNomCe.value = "$ " + new Intl.NumberFormat("en-IN").format(data[i].dTotal);

                        }
                        if (data[i].iIdRenglon == 1990) {
                            dedu = data[i].dTotal;
                            LadeduCalNomCe.style.visibility = 'visible';
                            deduCalNomCe.style.visibility = 'visible';
                            deduCalNomCe.value = "$ " + new Intl.NumberFormat("en-IN").format(data[i].dTotal);
                            total = per - dedu;
                            total = Math.round(total * 100);
                            total = total / 100;
                            LaTotalNomCe.style.visibility = 'visible';
                            TotalNomCe.style.visibility='visible'
                            TotalNomCe.value = "$ " + new Intl.NumberFormat("en-IN").format(total);
                        }
                    }
                    var source =
                    {
                        localdata: data,
                        datatype: "array",
                        datafields:
                            [

                                { name: 'iIdRenglon', type: 'int' },
                                { name: 'sNombreRenglon', type: 'string' },
                                { name: 'sTotal', type: 'string' },

                            ],

                        updaterow: function (rowid, rowdata) {
                            // synchronize with the server - send update command   
                        }
                    };
                    var dataAdapter = new $.jqx.dataAdapter(source);
                    var buildFilterPanel = function (filterPanel, datafield) {
                        var textInput = $("<input style='margin:5px;'/>");
                        var applyinput = $("<div class='filter' style='height: 25px; margin-left: 20px; margin-top: 7px;'></div>");
                        var filterbutton = $('<span tabindex="0" style="padding: 4px 12px; margin-left: 2px;">Filtrar</span>');
                        applyinput.append(filterbutton);
                        var filterclearbutton = $('<span tabindex="0" style="padding: 4px 12px; margin-left: 5px;">Limpiar</span>');
                        applyinput.append(filterclearbutton);
                        filterPanel.append(textInput);
                        filterPanel.append(applyinput);
                        filterbutton.jqxButton({ theme: exampleTheme, height: 20 });
                        filterclearbutton.jqxButton({ theme: exampleTheme, height: 20 });
                        var dataSource =
                        {
                            localdata: adapter.records,
                            datatype: "array",
                            async: false
                        };
                        var dataadapter = new $.jqx.dataAdapter(dataSource,
                            {
                                autoBind: false,
                                autoSort: true,
                                autoSortField: datafield,
                                async: false,
                                uniqueDataFields: [datafield]
                            });
                        var column = $("#TbCalculos").jqxGrid('getcolumn', datafield);
                        textInput.jqxInput({ theme: exampleTheme, placeHolder: "Enter " + column.text, popupZIndex: 9999999, displayMember: datafield, source: dataadapter, height: 23, width: 175 });
                        textInput.keyup(function (event) {
                            if (event.keyCode === 13) {
                                filterbutton.trigger('click');
                            }
                        });
                        filterbutton.click(function () {
                            var filtergroup = new $.jqx.filter();
                            var filter_or_operator = 1;
                            var filtervalue = textInput.val();
                            var filtercondition = 'contains';
                            var filter1 = filtergroup.createfilter('stringfilter', filtervalue, filtercondition);
                            filtergroup.addfilter(filter_or_operator, filter1);
                            // add the filters.
                            $("#TbCalculos").jqxGrid('addfilter', datafield, filtergroup);
                            // apply the filters.
                            $("#TbCalculos").jqxGrid('applyfilters');
                            $("#TbCalculos").jqxGrid('closemenu');
                        });
                        filterbutton.keydown(function (event) {
                            if (event.keyCode === 13) {
                                filterbutton.trigger('click');
                            }
                        });
                        filterclearbutton.click(function () {
                            $("#TbCalculos").jqxGrid('removefilter', datafield);
                            // apply the filters.
                            $("#TbCalculos").jqxGrid('applyfilters');
                            $("#TbCalculos").jqxGrid('closemenu');
                        });
                        filterclearbutton.keydown(function (event) {
                            if (event.keyCode === 13) {
                                filterclearbutton.trigger('click');
                            }
                            textInput.val("");
                        });
                    };
                    $("#TbCalculos2").jqxGrid({
                        width: 600,
                        height: 325,
                        source: dataAdapter,
                        columnsresize: true,
                        source: dataAdapter,
                        columnsresize: true,
                        filterable: true,
                        sortable: true,
                        //autoheight: true,
                        //autowidth:true,
                        //columns: columns,
                        sortable: true,
                        filterable: true,
                        altrows: true,
                        sortable: true,
                        ready: function () {
                        },

                        columns: [
                            { text: 'IdREnglon', datafield: 'iIdRenglon', width: 100 },
                            { text: 'Renglon', datafield: 'sNombreRenglon', width: 300 },
                            { text: 'Total ', datafield: 'sTotal', whidth: 200 },

                        ]
                    });
                    if (empresaid == 0) {

                        EmpresaNoCe.style.visibility = 'visible';
                        LaEmpresaNoCe.style.visibility = 'visible';
                        TipodePeridoDroplip = TipoPeriodoNoCe.value;
                        separador = " ",
                        limite = 2,
                        arreglosubcadena3 = TipodePeridoDroplip.split(separador, limite);
                        const dataSend2 = { iIdCalculosHd: IdDropList2, iTipoPeriodo: arreglosubcadena3[0], iPeriodo: arreglosubcadena[0] };

                        $("#EmpresaNoCe").empty();
                        $('#EmpresaNoCe').append('<option value="0" selected="selected">Selecciona</option>');
                        LisEmpresaNoce(IdDropList2);
                      
                    }
                }

            },
        });

    });
    ///////////////////////////////////////////////////////////
    $('#EmpresaNoCe').change(function () {
        IdDropList2;
        periodo = PeridoEjeNomCe.options[PeridoEjeNomCe.selectedIndex].text;
        separador = " ",
        limite = 2,
        arreglosubcadena = periodo.split(separador, limite);
        for (var i = 0; i <= RosTabCountCalculo2; i++) {

            $("#TbCalculos2").jqxGrid('deleterow', i);
        }
        var empresaid = EmpresaNoCe.value;      
        var tipoPeriodo = TipoPeriodoNoCe.value;
        separador = " ",
        limite = 2,
        arreglosubcadena2 = tipoPeriodo.split(separador, limite);
        arreglosubcadena3 = empresaid.split(separador, limite)
        const dataSend4 = { iIdCalculosHd: IdDropList2, iTipoPeriodo: arreglosubcadena2[0], iPeriodo: arreglosubcadena[0], idEmpresa: arreglosubcadena3[0], Anio: TbAñoNoCe.value};
        console.log(dataSend4);
        var per;
        var dedu;
        var total;
        $.ajax({
            url: "../Nomina/ListTpCalculoln",
            type: "POST",
            data: dataSend4,
            success: (data) => {
                console.log(data);
                RosTabCountCalculo2 = data.length;
                var dato = data[0].sMensaje;
                if (dato == "No hay datos") {
                    fshowtypealert('Vista de Calculo', 'No contiene ningun calculo', 'warning');
                }
                if (dato == "success") {
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].iIdRenglon == 990) {
                            per = data[i].dTotal;
                            PercepCalNomCe.style.visibility = 'visible';
                            LaTotalPerNoCe.style.visibility = 'visible';
                            PercepCalNomCe.value = "$ " + new Intl.NumberFormat("en-IN").format(data[i].dTotal);

                        }
                        if (data[i].iIdRenglon == 1990) {
                            dedu = data[i].dTotal;
                            LadeduCalNomCe.style.visibility = 'visible';
                            deduCalNomCe.style.visibility = 'visible';
                            deduCalNomCe.value = "$ " + new Intl.NumberFormat("en-IN").format(data[i].dTotal);
                            total = per - dedu;
                            total = Math.round(total * 100);
                            total = total / 100;
                            LaTotalNomCe.style.visibility = 'visible';
                            TotalNomCe.style.visibility = 'visible'
                            TotalNomCe.value = "$ " + new Intl.NumberFormat("en-IN").format(total);
                        }
                    }
                    var source =
                    {
                        localdata: data,
                        datatype: "array",
                        datafields:
                            [

                                { name: 'iIdRenglon', type: 'int' },
                                { name: 'sNombreRenglon', type: 'string' },
                                { name: 'sTotal', type: 'string' },

                            ],

                        updaterow: function (rowid, rowdata) {
                            // synchronize with the server - send update command   
                        }
                    };
                    var dataAdapter = new $.jqx.dataAdapter(source);
                    var buildFilterPanel = function (filterPanel, datafield) {
                        var textInput = $("<input style='margin:5px;'/>");
                        var applyinput = $("<div class='filter' style='height: 25px; margin-left: 20px; margin-top: 7px;'></div>");
                        var filterbutton = $('<span tabindex="0" style="padding: 4px 12px; margin-left: 2px;">Filtrar</span>');
                        applyinput.append(filterbutton);
                        var filterclearbutton = $('<span tabindex="0" style="padding: 4px 12px; margin-left: 5px;">Limpiar</span>');
                        applyinput.append(filterclearbutton);
                        filterPanel.append(textInput);
                        filterPanel.append(applyinput);
                        filterbutton.jqxButton({ theme: exampleTheme, height: 20 });
                        filterclearbutton.jqxButton({ theme: exampleTheme, height: 20 });
                        var dataSource =
                        {
                            localdata: adapter.records,
                            datatype: "array",
                            async: false
                        };
                        var dataadapter = new $.jqx.dataAdapter(dataSource,
                            {
                                autoBind: false,
                                autoSort: true,
                                autoSortField: datafield,
                                async: false,
                                uniqueDataFields: [datafield]
                            });
                        var column = $("#TbCalculos").jqxGrid('getcolumn', datafield);
                        textInput.jqxInput({ theme: exampleTheme, placeHolder: "Enter " + column.text, popupZIndex: 9999999, displayMember: datafield, source: dataadapter, height: 23, width: 175 });
                        textInput.keyup(function (event) {
                            if (event.keyCode === 13) {
                                filterbutton.trigger('click');
                            }
                        });
                        filterbutton.click(function () {
                            var filtergroup = new $.jqx.filter();
                            var filter_or_operator = 1;
                            var filtervalue = textInput.val();
                            var filtercondition = 'contains';
                            var filter1 = filtergroup.createfilter('stringfilter', filtervalue, filtercondition);
                            filtergroup.addfilter(filter_or_operator, filter1);
                            // add the filters.
                            $("#TbCalculos").jqxGrid('addfilter', datafield, filtergroup);
                            // apply the filters.
                            $("#TbCalculos").jqxGrid('applyfilters');
                            $("#TbCalculos").jqxGrid('closemenu');
                        });
                        filterbutton.keydown(function (event) {
                            if (event.keyCode === 13) {
                                filterbutton.trigger('click');
                            }
                        });
                        filterclearbutton.click(function () {
                            $("#TbCalculos").jqxGrid('removefilter', datafield);
                            // apply the filters.
                            $("#TbCalculos").jqxGrid('applyfilters');
                            $("#TbCalculos").jqxGrid('closemenu');
                        });
                        filterclearbutton.keydown(function (event) {
                            if (event.keyCode === 13) {
                                filterclearbutton.trigger('click');
                            }
                            textInput.val("");
                        });
                    };
                    $("#TbCalculos2").jqxGrid({
                        width: 600,
                        height: 325,
                        source: dataAdapter,
                        columnsresize: true,
                        source: dataAdapter,
                        columnsresize: true,
                        filterable: true,
                        sortable: true,
                        //autoheight: true,
                        //autowidth:true,
                        //columns: columns,
                        sortable: true,
                        filterable: true,
                        altrows: true,
                        sortable: true,
                        ready: function () {
                        },

                        columns: [
                            { text: 'IdREnglon', datafield: 'iIdRenglon', width: 100 },
                            { text: 'Renglon', datafield: 'sNombreRenglon', width: 300 },
                            { text: 'Total ', datafield: 'sTotal', whidth: 200 },

                        ]
                    });
                    if (empresaid == 0) {

                        EmpresaNoCe.style.visibility = 'visible';
                        LaEmpresaNoCe.style.visibility = 'visible';
                        TipodePeridoDroplip = TipoPeriodoNoCe.value;
                        separador = " ",
                        limite = 2,
                        arreglosubcadena3 = TipodePeridoDroplip.split(separador, limite);
                        const dataSend2 = { iIdCalculosHd: IdDropList2, iTipoPeriodo: arreglosubcadena3[0], iPeriodo: arreglosubcadena[0] };

                        $("#EmpresaNoCe").empty();
                        $('#EmpresaNoCe').append('<option value="0" selected="selected">Selecciona</option>');

                        $.ajax({
                            url: "../Nomina/EmpresaCal",
                            type: "POST",
                            data: dataSend2,
                            success: (data) => {

                                console.log(data);
                                for (i = 0; i < data.length; i++) {
                                    document.getElementById("EmpresaNoCe").innerHTML += `<option value='${data[i].iIdEmpresa}'>${data[i].iIdEmpresa}  ${data[i].sNombreEmpresa} </option>`;

                                }
                            },


                        });
                    }
                }

            },
        });

    });
    LisEmpresaNoce = (IdDropList2) => {
        
        const dataSend2 = { iIdCalculosHd: IdDropList2, iTipoPeriodo: 0, iPeriodo: 0, idEmpresa: 0, anio: 0 };
  
        $.ajax({
            url: "../Nomina/EmpresaCal",
            type: "POST",
            data: dataSend2,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("EmpresaNoCe").innerHTML += `<option value='${data[i].iIdEmpresa}'>${data[i].iIdEmpresa}  ${data[i].sNombreEmpresa} </option>`;
                    
                }
            },
        });
    };


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

    fshowtypealert2 = (title, text, icon) => {
        Swal.fire({
            title: title, text: text, icon: icon,
            showConfirmButton: false,
            timer: 5000,
            timerProgressBar: true
        });
    };


                     /* Notificaciones*/

    $("#messageNotification").jqxNotification({
        
        theme: 'bootstrap',
        width: 250, position: "top-right", opacity: 0.9,
        autoOpen: false, animationOpenDelay: 800, autoClose: true, autoCloseDelay: 3000, template: "info"

    });
    $("#messageNotification2").jqxNotification({
        theme: 'bootstrap',
        width: 250, position: "top-right", opacity: 0.9,
        autoOpen: false, animationOpenDelay: 800, autoClose: true, autoCloseDelay: 3000, template: "info"

    });

    $("#timerNotification").jqxNotification("closeLast");
    $("#jqxLoader").jqxLoader({ text: "Realizando calculos", width: 160, height: 80 });

    var notificationWidth = 300;
    $("#timerNotification").jqxNotification({ width: notificationWidth, position: "top-right", autoOpen: false, closeOnClick: false, autoClose: true, template: "seconds"});


    /// Selecciona calculo por empresa

    FCheckXempresa = () => {

        if (valorCeckxempresa.checked == true) {
            CheckXempleado.style.visibility = 'visible';
            LaCheckXEmpleado.style.visibility = 'visible';
            CheckCalculoEmpresa = 1;
            EjeEmpresa
            $("#DropLitEmple").jqxDropDownList('uncheckAll');
           // LaEmplea.style.visibility = 'visible';
            $("#switchButtonEmple").toggle();
            NombEmpre.style.visibility = 'visible';
            EjeEmpresa.style.visibility = 'visible';
            EjeEmpresa.value = 0;
            EmpleadoDEmp(EjeEmpresa.value);
            Empleadoseje.style.visibility = 'hidden';
            dropEmpledos.style.visibility = 'hidden';
            valorCheckXempleado.checked = false;
        }

        if (valorCeckxempresa.checked == false) {
            CheckXempleado.style.visibility = 'hidden';
            LaCheckXEmpleado.style.visibility = 'hidden';
            CheckCalculoEmpresa = 0;
            $("#DropLitEmple").jqxDropDownList('uncheckAll');
            //LaEmplea.style.visibility = 'hidden';
            $("#switchButtonEmple").toggle();
            //switchButtonEmp.style.visibility = 'hidden';
            NombEmpre.style.visibility = 'hidden';
            EjeEmpresa.style.visibility = 'hidden';
            EmpleadoDEmp(EjeEmpresa.value);
            Empleadoseje.style.visibility = 'hidden';
            dropEmpledos.style.visibility = 'hidden';
            valorCheckXempleado.checked = false;
        }

    };

    CheckXEmpresa.addEventListener('click', FCheckXempresa);

    /// selecciona calculo por empleado

    Fcheckxempleado = () => {
        if (valorCheckXempleado.checked == true) {
            checkCalculoEmplado = 1;
            $("#DropLitEmple").jqxDropDownList('uncheckAll');
            Empleadoseje.style.visibility = 'visible';
            dropEmpledos.style.visibility = 'visible';

        };
        if (valorCheckXempleado.checked == false) {
            checkCalculoEmplado = 0;
            $("#DropLitEmple").jqxDropDownList('uncheckAll');
            Empleadoseje.style.visibility = 'hidden';
            dropEmpledos.style.visibility = 'hidden';

        };
    };
    CheckXempleado.addEventListener('click',Fcheckxempleado)


    
    $("#DropLitEmple").on('checkChange', function (event) {
        if (event.args) {
            var item = event.args.item;
            if (item) {
                var valueelement = $("<div></div>");
                valueelement.text("Value: " + item.value);
                var labelelement = $("<div></div>");
                labelelement.text("Label: " + item.label);
                var checkedelement = $("<div></div>");
                checkedelement.text("Checked: " + item.checked);
                $("#selectionlog").children().remove();

                var items = $("#DropLitEmple").jqxDropDownList('getCheckedItems');
                var checkedItems = "";
                checkedItemsIdEmpleados = "";
                $.each(items, function (index) {
                    checkedItems += this.label + ", ";
                    checkedItemsIdEmpleados += this.value + ",";
                });
                $("#checkedItemsLog").text(checkedItems);
            }
        }
    });

         //// selesciona el empleado
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


    // Funcion muestra Grid Con los datos de TPDefinicion en del droplist definicion 
    //$("#switchButtonEmple").toggle();

    const formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD',
        minimumFractionDigits: 0
    });


    /// tab abrir no mina 

    const DropEmpresaPeri = document.getElementById('DropEmpresaPeri');
    const TxtAnioPer = document.getElementById('TxtAnioPer');
    const DropTipodePerdioPer = document.getElementById('DropTipodePerdioPer');
    const DropPeridoPer = document.getElementById('DropPeridoPer');
    const btnOpenPeriodo = document.getElementById('btnOpenPeriodo');


    FListadoEmpresaOpen = () => {
        $("#DropEmpresaPeri").empty();
        $('#DropEmpresaPeri').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisEmpresas",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                for (i = 0; i < data.length; i++) {


                    document.getElementById("DropEmpresaPeri").innerHTML += `<option value='${data[i].iIdEmpresa}'>${data[i].sNombreEmpresa}</option>`;

                }
            }
        });
    };
    FListadoEmpresaOpen();

    FListTipoDePeriodoOpen = () => {
        const dataSend = { IdEmpresa: DropEmpresaPeri.value };
        $("#DropTipodePerdioPer").empty();
        $('#DropTipodePerdioPer').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisTipPeriodo",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DropTipodePerdioPer").innerHTML += `<option value='${data[i].iId}'>${data[i].sValor}</option>`;
                }
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

    };

    $('#DropEmpresaPeri').change(function () {
        FListTipoDePeriodoOpen();
    });

    $('#DropTipodePerdioPer').change(function () {
        const dataSend = { iIdEmpresesas: DropEmpresaPeri.value, ianio: TxtAnioPer.value, iTipoPeriodo: DropTipodePerdioPer.value };
        $("#DropPeridoPer").empty();
        $('#DropPeridoPer').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/ListPeriodoComp",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    if (data[i].sNominaCerrada == "True") {
                        document.getElementById("DropPeridoPer").innerHTML += `<option value='${data[i].iPeriodo}'>${data[i].iPeriodo} </option>`;

                    }  
                }
                for (i = 0; i < data.length - 1; i++) {
                    document.getElementById("DropPerido2").innerHTML += `<option value='${data[i].iPeriodo}'>${data[i].iPeriodo} </option>`;
                }

            },
        });

    });


    FAbrirNomina = () => {
        console.log('abrir nomina');
        const dataSendopen = { iIdDefinicionHd: 0, IdEmpresa: DropEmpresaPeri.value, iPerido: DropPeridoPer.value, iNominaCerrada: 0, Anio: TxtAnioPer.value, IdTipoPeriodo: DropTipodePerdioPer.value };

        $.ajax({
            url: "../Nomina/UpdateCInicioFechasPeriodo",
            type: "POST",
            data: dataSendopen,
            success: (data) => {

                if (data.sMensaje == "success") {
                    fshowtypealert('Nomina', 'El Periodo se Abrio', 'correcto');

                    $('#DropEmpresaPeri').append('<option value="0" selected="selected">Selecciona</option>');
                    TxtAnioPer.value = "";
                    $('#DropTipodePerdioPer').append('<option value="0" selected="selected">Selecciona</option>');
                    $('#DropPeridoPer').append('<option value="0" selected="selected">Selecciona</option>');


                    
                }
                else {
                    fshowtypealert('Error', 'Contacte a sistemas', 'error');

                }
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

    };

    btnOpenPeriodo.addEventListener('click', FAbrirNomina);

});



