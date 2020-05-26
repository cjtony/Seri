$(function () {

    /// Tab Ejecucion 

    // Declaracion de variables 

    //const EjeNombreDef = document.getElementById('EjeNombreDef');
    //const EjeCerrada = document.getElementById('EjeCerrada');
    const TxtBInicioClculo = document.getElementById('TxtBInicioClculo');
    const TxtBFinClculo = document.getElementById('TxtBFinClculo');
    //const btnlimpDat = document.getElementById('btnlimpDat');
    const btnFloGuardar = document.getElementById('btnFloGuardar');
    const navEjecuciontab = document.getElementById('nav-Ejecucion-tab');
    const navVisCalculotab = document.getElementById('nav-VisCalculo-tab');
    const btnFloEjecutar = document.getElementById('btnFloEjecutar');
    const TbAño = document.getElementById('TbAño');
    const TxbTipoPeriodo = document.getElementById('TxbTipoPeriodo');
    const PeridoEje = document.getElementById('PeridoEje');
    const DefinicionCal = document.getElementById('DefinicionCal');
    const TipoPeridoCal = document.getElementById('TipoPeridoCal');
    const PeriodoCal = document.getElementById('PeriodoCal');
    const EmpresaCal = document.getElementById('EmpresaCal');
    const PercepCal = document.getElementById('PercepCal');
    const DeduCal = document.getElementById('DeduCal');
    const totalCal = document.getElementById('totalCal');
    const btnPdfCal = document.getElementById('btnPdfCal');

    //const btnFloCerrarNom = document.getElementById('btnFloCerrarNom');
    var ValorChek = document.getElementById('ChNCerrada');
    var DatoEjeCerrada;
    var IdDropList;
    var AnioDropList;
    var TipodePeridoDroplip;
    var periodo;
    var empresa;
    var RowsGrid;
    var exitRow;
    var opTab = 1;
    var RosTabCountCalculo;

    // funcion llena lisbox de Ejecucin Nombre de Definicion

    FlistNombreDef = () => {

        $.ajax({
            url: "../Nomina/ListadoNomDefinicion",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var num = 0;

                for (i = 0; i < data.length; i++) {
                    num = num + 1;
                    document.getElementById("EjeNombreDef").innerHTML += `<option value='${num}'>${data[i].sNombreDefinicion}</option>`;
                }
            }
        });
    };
    FlistNombreDef();

    // Funcion muestra Grid Con los datos de TPDefinicion en del droplist definicion 

    FLlenaGrid = () => {

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
                        // synchronize with the server - send update command   
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

    $('#EjeNombreDef').change(function () {

       
    });

    // Funcion Limpia Campos

    //FLimpiaCampos = () => {

    //    EjeCerrada.value = "0";
    //    TxtBInicioClculo.value = '';
    //    TxtBFinClculo.value = '';
    //    IdDropList = 0;
    //    $("#2").empty();

    //};

    //btnlimpDat.addEventListener('click', FLimpiaCampos);

    // define tamaño del droplist

    $("#jqxdropdownbutton").jqxDropDownButton({
        width: 600, height: 30
    });

    // seleccion de linea de grip y la guarda en el droplist y carga los datos de tipo de perio y llena el drop de periodo
      

    $("#TpDefinicion").on('rowselect', function (event) {

        var args = event.args;
        var row = $("#TpDefinicion").jqxGrid('getrowdata', args.rowindex);
        IdDropList = row.iIdDefinicionhd;
        AnioDropList = row.iAno;
        DefinicionCal.value = row.iIdDefinicionhd + row.sNombreDefinicion;
        var dropDownContent = '<div id="2" style="position: relative; margin-left: 3px; margin-top: 6px;">' + row['iIdDefinicionhd'] + ' ' + row['sNombreDefinicion'] + '</div>';

        $("#jqxdropdownbutton").jqxDropDownButton('setContent', dropDownContent);
        TbAño.value = AnioDropList;
     
        const dataSend = { IdDefinicionHD: IdDropList, iperiodo: 0 };
        $.ajax({
            url: "../Nomina/TipoPeriodo",
            type: "POST",
            data: dataSend,
            success: (data) => {
                console.log('Resultado de periodo: '+data)
                TxbTipoPeriodo.value = data[0].iId + " " + data[0].sValor;
                TipoPeridoCal.value = data[0].iId + " " + data[0].sValor;
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

        $("#PeridoEje").empty();
        //$('#PeridoEje').append('<option value="0" selected="selected">Selecciona</option>');

        $.ajax({
            url: "../Nomina/ListPeriodoEmpresa",
            type: "POST",
            data: dataSend,
            success: (data) => {
                //for (i = 0; i < data.length; i++) {
                document.getElementById("PeridoEje").innerHTML += `<option value='${data[0].iId}'>${data[0].iPeriodo} Fecha del: ${data[0].sFechaInicio} al ${data[0].sFechaFinal}</option>`;
                periodo = data[0].iPeriodo;
                PeriodoCal.value = periodo;
                empresa = 0
                FllenagripTpDefinicionLN(periodo, empresa);

                //}
            },


        });

        const dataSend3 = { iIdDefinicionHd: IdDropList };
        $.ajax({
            url: "../Nomina/CompruRegistroExit",
            type: "POST",
            data: dataSend3,
            success: (data) => {

                if (data[0].iIdCalculosHd == 1) {   
                 
                    btnFloEjecutar.style.visibility = 'visible';
                    btnFloGuardar.style.visibility = 'hidden';
                }

                if (data[0].iIdCalculosHd == 0) {
                    btnFloGuardar.style.visibility = 'visible';
                    btnFloEjecutar.style.visibility = 'hidden';
               }

            },
        });

       
    });

    $("#TpDefinicion").jqxGrid('selectrow', 0);

    // Funcion de guardar 
    Fguardar = () => {

        DatoEjeCerrada = 0;
        if (IdDropList > 0) {
            const dataSend = { iIdDefinicionHd: IdDropList };
            $.ajax({
                url: "../Nomina/CompruRegistroExit",
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

                        //mensaje
                        Swal.fire({
                            title: 'Seguro que deseas actualizar la ejecución?',
                            text: "Si es asi da clic en aceptar!",
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Aceptar!'
                        }).then((result) => {
                            if (result.value) {
                                Swal.fire(
                                    'Ejecución!',
                                    'actualizada.',
                                    'success'
                                );
                                const dataSend3 = { iIdDefinicionHd: IdDropList, iNominaCerrada: DatoEjeCerrada };
                                $.ajax({
                                    url: "../Nomina/UpdateCalculoshd",
                                    type: "POST",
                                    data: dataSend3,
                                    success: (data) => {
                                        console.log('termino');
                                        if (data.sMensaje == "success") {
                                            console.log(data);
                                            $("#2").empty();
                                            $("#TpDefinicion").jqxGrid('clearselection');
                                            $("#PeridoEje").empty();
                                            $('#PeridoEje').append('<option value="0" selected="selected">Selecciona</option>');
                                            TbAño.value = "";
                                            TxbTipoPeriodo.value = "";
                                            ValorChek.checked = false;
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
                        });

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

    };

    btnFloGuardar.addEventListener('click', Fguardar);

    /// tab Calculo


    //var exampleTheme = theme;
    //var exampleTheme = theme;

    FllenagripTpDefinicionLN = (periodo, empresa) => {   
        var empresaid = empresa;
        
        // borrar por fila 
        for (var i = 0; i <= RosTabCountCalculo; i++) {

            $("#TbCalculos").jqxGrid('deleterow', i);
        }
        
        var tipoPeriodo = TxbTipoPeriodo.value;
            separador = " ",
            limite = 2,
            arreglosubcadena = tipoPeriodo.split(separador, limite);


        IdDropList;
        const dataSend = { iIdCalculosHd: IdDropList, iTipoPeriodo: arreglosubcadena[0] , iPeriodo: periodo, idEmpresa: empresaid };
        console.log(dataSend);
        var per;
        var dedu;
        var total;
        $.ajax({
                url: "../Nomina/ListTpCalculoln",
                type: "POST",
                data: dataSend,
                success: (data) => {
                    RosTabCountCalculo = data.length;
                    var dato = data[0].sMensaje;
                    if (dato == "No hay datos") {

                        fshowtypealert('Vista de Calculo', 'No contiene ningun calculo en la Definicion: ' + DefinicionCal.value + ', en el periodo: ' + PeriodoCal.value, 'warning');
                        $("#nav-Ejecucion-tab").addClass("active");
                        $("#nav-VisCalculo-tab").addClass("active");
                    
                    }

                    if (dato == "success") {
                        $("#nav-Ejecucion-tab").removeClass("active");
                        $("#nav-VisCalculo-tab").removeClass("active");
                      
                        console.log('tamaño de tabla:' + dato.length);
                        for (var i=0; i < data.length; i++) {
                           
                            if (data[i].iIdRenglon == 990) {
                                per = data[i].dTotal;
                                PercepCal.value = data[i].dTotal
                                
                            }

                            if (data[i].iIdRenglon == 1990) {
                                dedu = data[i].dTotal;
                                DeduCal.value = data[i].dTotal; 
                                total = per - dedu;
                                total = Math.round(total * 100);
                                total = total / 100;
                                totalCal.value = total;
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
                                    { name: 'dTotal', type: 'double' },

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
                                { text: 'Total ', datafield: 'dTotal', whidth: 200 },

                            ]
                        });               
                        if (empresaid == 0) {
                            TipodePeridoDroplip = TxbTipoPeriodo.value;
                            separador = " ",
                                limite = 2,
                                arreglosubcadena3 = TipodePeridoDroplip.split(separador, limite);
                            const dataSend2 = { iIdCalculosHd: IdDropList, iTipoPeriodo: arreglosubcadena3[0], iPeriodo: periodo };

                            $("#EmpresaCal").empty();
                            $('#EmpresaCal').append('<option value="0" selected="selected">Selecciona</option>');

                            $.ajax({
                                url: "../Nomina/EmpresaCal",
                                type: "POST",
                                data: dataSend2,
                                success: (data) => {

                                    console.log(data);
                                    for (i = 0; i < data.length; i++) {
                                        document.getElementById("EmpresaCal").innerHTML += `<option value='${data[i].iIdEmpresa}'>${data[i].iIdEmpresa}  ${data[i].sNombreEmpresa} </option>`;


                                    }
                                },


                            });
                        }
                    }

                },
            });
        
    };


    Ftabopcion1 = () => {

        btnFloGuardar.style.visibility = 'visible';
        //btnlimpDat.style.visibility = 'visible';
        btnFloEjecutar.style.visibility = 'visible';


    };
    Ftabopcion2 = () => {

        btnFloGuardar.style.visibility = 'hidden';
        //btnlimpDat.style.visibility = 'hidden';
        btnFloEjecutar.style.visibility = 'hidden';

    };

    navEjecuciontab.addEventListener('click', Ftabopcion1);
    navVisCalculotab.addEventListener('click', Ftabopcion2);

    // Procesos de Ejecucion 

    Fejecucion = () => {
          
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
        const dataSend2 = { IdDefinicionHD: IdDropList, anio: AnioDropList, iTipoPeriodo: arreglosubcadena3[0], iperiodo: arreglosubcadena2[0] };
        console.log("datos de ejecucion:"+dataSend2);
        $.ajax({
            url: "../Nomina/CompruRegistroExit",
            type: "POST",
            data: dataSend,
            success: (data) => {

                if (data[0].iIdCalculosHd == 1) {

                    console.log(dataSend);
                    fshowtypealert("Ejecucion", "El calculo de la nomina en ejecucion", "Right")
                    $.ajax({
                        url: "../Nomina/ProcesosPots",
                        type: "POST",
                        data: dataSend2,
                        success: (data) => {
          
                        }
                    });

                }

                if (data[0].iIdCalculosHd == 0) {
                    exitRow = "0";
                    fshowtypealert("Ejecucion", "La definicion de nomina seleccionada no esta guardada", "warning")
                }

            },
        });

    };

    btnFloEjecutar.addEventListener('click', Fejecucion);

    //// funcion de cerrar nomina 

  
    //btnFloCerrarNom.addEventListener('click', FCerrarNom);

    FValorChec = () => {
       

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
                    const dataSend = { iIdCalculosHd: IdDropList, iTipoPeriodo: TxbTipoPeriodo.value, iPeriodo: arreglosubcadena2[0], idEmpresa:0 };
                    console.log(dataSend);
                    var rows;
                    $.ajax({
                        url: "../Nomina/ListTpCalculoln",
                        type: "POST",
                        data: dataSend,
                        success: (data) => {
                            if (data[0].sMensaje == "success") {
                                rows = data.length;
                                console.log(rows);
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
                                    const dataSend3 = { iIdDefinicionHd: IdDropList, iPerido: arreglosubcadena[0], iNominaCerrada: 1 };
                               
                                    $.ajax({
                                        url: "../Nomina/UpdateCInicioFechasPeriodo",
                                        type: "POST",
                                        data: dataSend3,
                                        success: (data) => {

                                            if (data.sMensaje == "success") {
                                                console.log(data);
                                                $("#2").empty();
                                                $("#TpDefinicion").jqxGrid('clearselection'); 
                                                $("#PeridoEje").empty();
                                                $('#PeridoEje').append('<option value="0" selected="selected">Selecciona</option>');
                                                TbAño.value = "";
                                                TxbTipoPeriodo.value = "";
                                                ValorChek.checked = false;
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

            Swal.fire({
                title: 'Seguro que deseas abrir la Nomina?',
                text: "Si es asi da clic en aceptar!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Aceptar!'
            }).then((result) => {
                if (result.value) {
                    Swal.fire(
                        'Nomina!',
                        'Abierta.',
                        'success'
                    );
                    periodo = PeridoEje.options[PeridoEje.selectedIndex].text;
                    separador = " ",
                        limite = 2,
                        arreglosubcadena = periodo.split(separador, limite);
                    const dataSend3 = { iIdDefinicionHd: IdDropList, iPerido: arreglosubcadena[0], iNominaCerrada: 0 };
                    console.log('nominacerrada');
                    console.log(dataSend3);
                    $.ajax({
                        url: "../Nomina/UpdateCInicioFechasPeriodo",
                        type: "POST",
                        data: dataSend3,
                        success: (data) => {

                            if (data.sMensaje == "success") {
                                console.log(data);
                                $("#2").empty();
                                $("#TpDefinicion").jqxGrid('clearselection');    
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
            });


        }

    };

    ChNCerrada.addEventListener('click', FValorChec);
   


    $('#PeridoEje').change(function () {

        IdDropList;
        periodo = PeridoEje.options[PeridoEje.selectedIndex].text;
        separador = " ",
        limite = 2,
        arreglosubcadena = periodo.split(separador, limite);
        const dataSend = { IdDefinicionHD: IdDropList, iperiodo: arreglosubcadena[0] };  
        PeriodoCal.value = arreglosubcadena[0];
        
        $.ajax({
            url: "../Nomina/ListPeriodoEmpresa",
            type: "POST",
            data: dataSend,
            success: (data) => {
               
                var dato = data[0].sNominaCerrada;
                console.log(dato);
                if (dato == "True") {
                    console.log('valor correcto');
                    ValorChek.checked = true;
                }

                if (data[0].sNominaCerrada == "False") {
                    ValorChek.checked = false;
                }
          
            },


        });


        FllenagripTpDefinicionLN();


    });

    $('#EmpresaCal').change(function () {

        var idempresa = EmpresaCal.value;
        var perido = PeriodoCal.value;
        
        FllenagripTpDefinicionLN(periodo, idempresa);


    });

    //// Genera Caratula en PDF///
    //$('#btnPdfCal').on('click', function () {

    //    $.ajax({
    //        url: "../Nomina/PDFCaratula",
    //        type: "POST",
    //        data: JSON.stringify(),
    //        contentType: "application/json; charset=utf-8",
    //        success: (data) => {
                

             
    //        }
    //    });


    //});

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