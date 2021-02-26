$(function () {

    //------------------------ Pantalla de Consulta --------------------------------------------//
    // declaracion de variables 
    //var DeNombre = document.getElementById('DeNombre');
    //var DeCancelados = document.getElementById('DeCancelados');
    var dato;
    var empresaSelect = document.getElementById("btnNameEmpresaSelected").innerHTML; 
    var IdEmpresaSess = '<%= Session["IdEmpresa"] %>';

    // declaracion de Botone

    //const BAgregar = document.getElementById('BAgregar');
    const BActu = document.getElementById('BActu');
    const btnFloAgre = document.getElementById('btnFloAgre');
 

    // Funcion llena el grip de Nomina Definicion.

    Fllenagrip = () => {

        $.ajax({
            url: "../Nomina/TpDefinicionNomina",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
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
                        ]
                };

                var dataAdapter = new $.jqx.dataAdapter(source);
                var items = new Array();
                if (data.length > 0) {
                    for (i = 0; i < data.length;i++) {
                        items.push(data[i].sNombreDefinicion);
                    }
                }
                
               
                $("#TpDefinicion").jqxGrid(
                    {
                        theme:'bootstrap',
                        width: 715,
                        source: dataAdapter,
                        showfilterrow: true,
                        filterable: true,
                        sortable: true,
                        pageable: true,
                        autoheight: true,
                        autoloadstate: false,
                        autosavestate: false,
                        columnsresize: true,
                        showtoolbar: true,
                        rendertoolbar: function (statusbar) {
                            var container = $("<div style='overflow: hidden; position: relative; height: 100%; width: 100%;'></div>");
                            statusbar.append(container);
                            container.append("<input id='actbut' type='button'title='Actualizar' data-toggle='modal' data-target='#AgregarDefinicion' ></div>");
                            $("#actbut").jqxButton({ template: "link", width: 60, height: 35, imgSrc: "../../Scripts/jqxGrid/jqwidgets/styles/images/icon-edit.png" });
                            $("#actbut").on('click', function () {
                                $("#BActu").click();
                            });

                        },
                        columns: [
                            { text: 'No. Registro', datafield: 'iIdDefinicionhd', width: 80 },
                            { text: 'Nombre de Definición', filtertype: 'list', filteritems: items, datafield: 'sNombreDefinicion', width: 200 },
                            { text: 'Descripción ', datafield: 'sDescripcion', width: 295 },
                            { text: 'año', datafield: 'iAno', width: 80 },
                            { text: 'Cancelado', datafield: 'iCancelado', width: 60 },
                        ]
                    });


            }
        });

    };

    // Abre ventana de Actualizacion

    botonActu = () => {

        btnAgregarDefinicion.style.visibility = 'hidden';
        btnActualizarDefinicion.style.visibility = 'visible';

        $("#TpDefinicion").click();

        Nombrede.value = DatoNombrede;
        Descripcionde.value = DatoDescripcion;
        iAnode.value = Datoano;

        for (var i = 0; i < cande.length; i++) {
            if (cande.options[i].text == DatoCancel) {
                // seleccionamos el valor que coincide
                cande.selectedIndex = i;
            }
        }
    };
    BActu.addEventListener('click', botonActu);
    BActu.style.visibility = 'hidden';
    // Fucion llena grid dependiendo que seleccionen en listBox DeNombre

    FRecargaGrip = () => {

        var opDeNombre = DeNombre.options[DeNombre.selectedIndex].text;
        var opDeNombreint = DeNombre.value;
        var opDeCancelados = DeCancelados.value;

        const dataSend = { sNombreDefinicion: opDeNombre, iCancelado: opDeCancelados };
        $.ajax({
            url: "../Nomina/TpDefinicionNominaQry",
            type: "POST",
            data: dataSend,
            success: (data) => {
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
                        ]
                };

                var dataAdapter = new $.jqx.dataAdapter(source);

                $("#TpDefinicion").jqxGrid({
                    width: 715,
                    source: dataAdapter,
                    columnsresize: true,
                    autorowheight: true,
                    autoheight: true,
                    columns: [
                        { text: 'No. Registro', datafield: 'iIdDefinicionhd', width: 80 },
                        { text: 'Nombre de Definición', filtertype: 'list', filteritems: items, datafield: 'sNombreDefinicion', width: 200 },
                        { text: 'Descripción ', datafield: 'sDescripcion', width: 295 },
                        { text: 'año', datafield: 'iAno', width: 80 },
                        { text: 'Cancelado', datafield: 'iCancelado', width: 60 }
                    ]
                });
            },
        });
    };

    Fllenagrip();



    FSelectDefinicion = () => {

        $("#TpDefinicion").on('rowselect', function (event) {
            var args = event.args;
            $("#selectrowindex").text(event.args.rowindex);
        });
        // display unselected row index.
        $("#TpDefinicion").on('rowselect', function (event) {
            
            document.getElementById('content-blockDedu/per').classList.remove("d-none");
            var args = event.args;
            var row = $("#TpDefinicion").jqxGrid('getrowdata', args.rowindex);        
            $("#unselectrowindex").text(row['iIdDefinicionhd'] + row['sNombreDefinicion']);
            dato = row['sNombreDefinicion'];
            IdDh = row['iIdDefinicionhd'];
            DatoNombrede = row['sNombreDefinicion'];
            DatoDescripcion = row['sDescripcion'];
            Datoano = row['iAno'];
            DatoCancel = row['iCancelado'];
            FcargaPercepciones();
            FcargaDeducionesGrip();
        });

    };
    FSelectDefinicion();

    // abre ventana del agregar 

    FAgrega = () => {

        btnAgregarDefinicion.style.visibility = 'visible';
        btnActualizarDefinicion.style.visibility = 'hidden';

    };

    //BAgregar.addEventListener('click', FAgrega);
    btnFloAgre.addEventListener('click', FAgrega);
    // pantalla de agregar definicion

    //    declaracion de variables

    const Nombrede = document.getElementById('NombreDe');
    const Descripcionde = document.getElementById('DescripcionDe');
    const iAnode = document.getElementById('iAnoDe');
    const cande = document.getElementById('DCancelado');
    var IdDh;
    var DatoNombrede;
    var DatoDescripcion;
    var Datoano;
    var DatoCancel;

    const btnAgregarDefinicion = document.getElementById('btnAgregarDefinicion');
    const btnCierraDefinicion = document.getElementById('btnCierraDefinicion');
    const btnActualizarDefinicion = document.getElementById('btnActualizarDefinicion');

    // FAgrega datos de definicion en BD

    FAgregaDef = () => {
        if (Nombrede.value != "" && Nombrede.value != " " && Descripcionde.value != "" && Descripcionde.value != " " && iAnode.value != "" && iAnode.value != " ") {
            const dataSend = {
                sNombreDefinicion: Nombrede.value, sDescripcion: Descripcionde.value,
                iAno: iAnode.value, iCancelado: cande.value
            };

            $.ajax({
                url: "../Nomina/DefiNomina",
                type: "POST",
                data: dataSend,
                success: function (data) {
                    if (data.sMensaje == "success") {

                        fshowtypealert('Registro correcto!', 'Definicion plantilla guardada', 'success');
                        Nombrede.value = '';
                        Descripcionde.value = '';
                        iAnode.value = '';
                        cande.value = '';
                        Fllenagrip();
                        FRecargaGrip();

                    } else {
                        fshowtypealert('Error', 'Contacte a sistemas', 'error');
                    }
                },
                error: function (jqXHR, exception) {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });

        }
        else {
            console.log('mesaje de error');
            fshowtypealert('warning', 'Introduce todos los campos', 'warning');

        }
    };

    btnAgregarDefinicion.addEventListener('click', FAgregaDef);

    // Fcerrar Cierra la pantallad  definicion

    CerrarPantDef = () => {
        console.log('limpia');
        Nombrede.value = '';
        Descripcionde.value = '';
        iAnode.value = '';
        cande.value = '';
    };

    btnCierraDefinicion.addEventListener('click', CerrarPantDef);

    // Actualiza Definicion de platilla 

    FActualiza = () => {
        var opcion = 0;

        var opselesc = cande.options[cande.selectedIndex].text;
        if (DatoNombrede != Nombrede.value || DatoDescripcion != Descripcionde.value || Datoano != iAnode.value || DatoCancel != opselesc) {
            if (Datoano != iAnode.value) {
                opcion = 1;
            }
            const dataSend = {
                sNombreDefinicion: Nombrede.value, sDescripcion: Descripcionde.value,
                iAno: iAnode.value, iCancelado: cande.value, iIdDefinicionhd: IdDh, OptAnio: opcion,
            };

            $.ajax({
                url: "../Nomina/UpdatePtDefinicion",
                type: "POST",
                data: dataSend,
                success: function (data) {
                    if (data.sMensaje == "success") {
                        fshowtypealert('Actualizacion correcta!', 'Definicion plantilla actualizada', 'success');
                        Fllenagrip();
                        FRecargaGrip();


                    } else {
                        fshowtypealert('Error', 'Contacte a sistemas', 'error');
                    }
                },
                error: function (jqXHR, exception) {    
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        }
    };

    btnActualizarDefinicion.addEventListener('click', FActualiza);

                   // Pantalla  Percepciones

    // llena el grip de Percepciones

    const navPercepcionestab = document.getElementById('nav-Percepciones-tab');
    var RosCountPer;

    FcargaPercepciones = () => {

        for (var i = 0; i <= RosCountPer; i++) {

            $("#TbPercepciones").jqxGrid('deleterow', i);
        }

        navPercepcionestab.style.visibility = "visible";

        const dataSend = { iIdDefinicionln: IdDh };
        $.ajax({
            url: "../Nomina/listdatosPercesiones",
            type: "POST",
            data: dataSend,
            success: function (data) {
                if (data[0].sMensaje == "success") {
                    if (data.length > 0) {
                        RosCountPer = data.length;
                    }
                    var source =
                    {
                        localdata: data,
                        datatype: "array",
                        datafields:
                            [
                                { name: 'iIdDefinicionln', type: 'string' },
                                { name: 'IdEmpresa', type: 'string' },
                                { name: 'iRenglon', type: 'string' },
                                { name: 'iTipodeperiodo', type: 'string' },
                                { name: 'iIdAcumulado', type: 'string' },
                                { name: 'iEsespejo', type: 'string' }
                            ]
                    };

                    var dataAdapter = new $.jqx.dataAdapter(source);

                    $("#TbPercepciones").jqxGrid(
                        {
                            width: 620,
                            source: dataAdapter,
                            selectionmode: 'multiplerowsextended',
                            sortable: true,
                            pageable: true,
                            autoheight: true,
                            autoloadstate: false,
                            autosavestate: false,
                            columnsresize: true,
                            showtoolbar: true,
                            rendertoolbar: function (statusbar) {
                                var container = $("<div style='overflow: hidden; position: relative; margin: 4px;'></div>");
                                var addButton = $("<div style='float: left; '><img style='position: relative;' src='../../Scripts/jqxGrid/jqwidgets/styles/images/icon-plus.png'/></div>");
                                var ActuButton = $("<div style='float: left; '><img style='position: relative; ' src='../../Scripts/jqxGrid/jqwidgets/styles/images/icon-edit.png'/></div>");
                                var DeletButton = $("<div style='float: left; '><img style='position: relative; ' src='../../Scripts/jqxGrid/jqwidgets/styles/images/icon-delete.png'/></div>");
                                container.append(addButton);
                                container.append(ActuButton);
                                container.append(DeletButton);
                                statusbar.append(container);
                                addButton.jqxButton({ template: "link", width: 40, height: 25 });
                                ActuButton.jqxButton({ template: "link", width: 40, height: 25 });
                                DeletButton.jqxButton({ template: "link", width: 40, height: 25 });
                                addButton.click(function (event) {
                                    $("#BAgregarPer").click();
                                });
                                ActuButton.click(function (event) {
                                    $("#BActuPer").click();
                                });
                                DeletButton.click(function (event) {
                                    $("#BEliminarPer").click();
                                });


                            },
                            columns: [
                                { text: 'No.Linea', datafield: 'iIdDefinicionln', width: 80 },
                                { text: 'Empresa', datafield: 'IdEmpresa', width: 120 },
                                { text: 'Renglon', datafield: 'iRenglon', width: 180 },
                                { text: 'Tipo de periodo', datafield: 'iTipodeperiodo', width: 80 },
                                { text: 'Acumulado', datafield: 'iIdAcumulado', width:80},
                                { text: 'Esespejo', datafield: 'iEsespejo', width: 80 }
                            ]
                        });
                }
                if (data[0].sMensaje == "NotDat") {
                    if (data.length > 0) {
                        RosCountPer = data.length;
                    }
                    var source =
                    {
                        localdata: data,
                        datatype: "array",
                        datafields:
                            [
                                { name: 'iIdDefinicionln', type: 'string' },
                                { name: 'IdEmpresa', type: 'string' },
                                { name: 'iRenglon', type: 'string' },
                                { name: 'iTipodeperiodo', type: 'string' },
                                { name: 'iIdAcumulado', type: 'string' },
                                { name: 'iEsespejo', type: 'string' }
                            ]
                    };

                    var dataAdapter = new $.jqx.dataAdapter(source);

                    $("#TbPercepciones").jqxGrid(
                        {
                            width: 840,
                            source: dataAdapter,
                            selectionmode: 'multiplerowsextended',
                            sortable: true,
                            pageable: true,
                            autoheight: true,
                            autoloadstate: false,
                            autosavestate: false,
                            columnsresize: true,
                            showtoolbar: true,
                            rendertoolbar: function (statusbar) {
                                var container = $("<div style='overflow: hidden; position: relative; margin: 4px;'></div>");
                                var addButton = $("<div style='float: left; '><img style='position: relative;' src='../../Scripts/jqxGrid/jqwidgets/styles/images/icon-plus.png'/></div>");
                                var ActuButton = $("<div style='float: left; '><img style='position: relative; ' src='../../Scripts/jqxGrid/jqwidgets/styles/images/icon-edit.png'/></div>");
                                var DeletButton = $("<div style='float: left; '><img style='position: relative; ' src='../../Scripts/jqxGrid/jqwidgets/styles/images/icon-delete.png'/></div>");
                                container.append(addButton);
                                container.append(ActuButton);
                                container.append(DeletButton);
                                statusbar.append(container);
                                addButton.jqxButton({ template: "link", width: 40, height: 25 });
                                ActuButton.jqxButton({ template: "link", width: 40, height: 25 });
                                DeletButton.jqxButton({ template: "link", width: 40, height: 25 });
                                addButton.click(function (event) {
                                    $("#BAgregarPer").click();
                                });
                                ActuButton.click(function (event) {
                                    $("#BActuPer").click();
                                });
                                DeletButton.click(function (event) {
                                    $("#BEliminarPer").click();
                                });


                            },
                            columns: [
                                { text: 'No.Linea', datafield: 'iIdDefinicionln', width: 75 },
                                { text: 'Empresa', datafield: 'IdEmpresa', width: 120 },
                                { text: 'Renglon', datafield: 'iRenglon', width: 180 },
                                { text: 'Tipo de periodo', datafield: 'iTipodeperiodo', whidth: 30 },
                                { text: 'Acumulado', datafield: 'iIdAcumulado', whidt: 200 },
                                { text: 'Esespejo', datafield: 'iEsespejo', whidt: 30 }
                            ]
                        });

                }

          
            }
        });
    };

    var RegEmpresa = document.getElementById('RegEmpresa');
    var RegRenglon = document.getElementById('RegRenglon');
    var Tpoperiodo1 = document.getElementById('RegTipoperiodo1');
    var iRegEspejo = document.getElementById('RegEspejo');
    var iAcumulado = document.getElementById('RegAcumulado');
    var AnioPre;
    var cancelado;
    var IdMaxDefNom;
    var DatoiId;
    var DatoEmpresa;
    var DatoRenglon;
    var datotipoperiodo;
    var datoespejo;
    var datoacumulado;

    const btnAgregarPercep = document.getElementById('btnAgregarPercep');
    const BActuPer = document.getElementById('BActuPer');
    const btnActualizarPercep = document.getElementById('btnActualizarPercep');
    const BEliminarPer = document.getElementById('BEliminarPer');
    const btnCierrapercepcion = document.getElementById('btnCierrapercepcion');
    const BAgregarPer = document.getElementById('BAgregarPer');

    // fincion de llenado el droplist de empresa de persepciones
    LisEmpresa = () =>
    {
        $.ajax({
            url: "../Nomina/LisEmpresas",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                BAgregarPer.value = data[0].iIdEmpresaSess;
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegEmpresa").innerHTML += `<option value='${data[i].iIdEmpresa}'>${data[i].sNombreEmpresa}</option>`;
                }
            }
        });
    };

    LisEmpresa();

    // llenado de lista droplist de tipo de periodo
    RecargaLisperiodo = () => {

        var op = RegEmpresa.options[RegEmpresa.selectedIndex].text;
        var opval = RegEmpresa.value;
        const dataSend = { IdEmpresa: opval };
        $("#RegTipoperiodo1").empty();
        $('#RegTipoperiodo1').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisTipPeriodo",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegTipoperiodo1").innerHTML += `<option value='${data[i].iId} '>${data[i].sValor}</option>`;
                }
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });
    };

    // llenado del droplist de renglon

    RecargaLisRenglon = () => {

        var OpRenglon = RegEmpresa.options[RegEmpresa.selectedIndex].text;
        var opval = RegEmpresa.value;
        const dataSend = { IdEmpresa: opval, iElemntoNOm: 1 };
        $("#RegRenglon").empty();
        $('#RegRenglon').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisRenglon",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegRenglon").innerHTML += `<option value='${data[i].iIdRenglon} - ${data[i].iEspejo}'>${data[i].sNombreRenglon}</option>`;
                }
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

    };

    RecargaLisRenglon2 = ( IdEmpresa) => {

        
        const dataSend = { IdEmpresa: IdEmpresa, iElemntoNOm: 1 };
        $("#RegRenglon").empty();
        $('#RegRenglon').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisRenglon",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegRenglon").innerHTML += `<option value='${data[i].iIdRenglon} - ${data[i].iEspejo}'>${data[i].sNombreRenglon}</option>`;
                }
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

    };

    $('#RegEmpresa').change(function () {

        RecargaLisperiodo();
        RecargaLisRenglon();
    });

    // Funcion que llenado del droplist Acumulado

    RecargaLisAcumulado = () => {

        var OpRenglon = RegEmpresa.options[RegEmpresa.selectedIndex].text;
        var opEmpresaval = RegEmpresa.value;
        var OpRenglonint = RegRenglon.options[RegRenglon.selectedIndex].text;
        separador = "-",
        limite = 2,
        arreglosubcadena = RegRenglon.value.split(separador, limite);
        const opRenglonTex = arreglosubcadena[0];
        const dataSend = { iIdEmpresa: opEmpresaval, iIdRenglon: opRenglonTex };
        $("#RegAcumulado").empty();
        $('#RegAcumulado').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisAcumulado",
            type: "POST",
            data: dataSend,
            success: (data) => {

                console.log(data);
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegAcumulado").innerHTML += `<option value='${data[i].iIdAcumulado}'>${data[i].sDesAcumulado}</option>`;


                }
            },


        });
     
        if (arreglosubcadena[1] == 0) {
            RegEspejo.selectedIndex = 2;
            
        }
        if (arreglosubcadena[1] == 1) {
            RegEspejo.selectedIndex = 1;
        }

    };

    $('#RegRenglon').change(function () {

        RecargaLisAcumulado();

    });



    // FAgrega datos de percepcion en BD

    FAgregaper = () => {

        var op = RegTipoperiodo1.options[RegTipoperiodo1.selectedIndex].text;
        if (RegEmpresa.value != "0" && RegRenglon.value != "0" && iRegEspejo.value != "0" && op != "Selecciona") {
            var ispejo;
            if (iRegEspejo.value == "1") {
                ispejo = "1";
            }
            else if (iRegEspejo.value == "2") {
                ispejo = "0";
            }
            $("#TpDefinicion").click();
            const dataSend = { iIdFinicion: IdDh };

            $.ajax({
                url: "../Nomina/DefCancelado",
                type: "POST",
                data: dataSend,
                success: (data) => {
                    for (i = 0; i < data.length; i++) {
                        cancelado = data[i].iCancelado;
                    }
                    var opcanel1;
                    if (cancelado == "True" || cancelado == "true") {
                        opcanel1 = 1;
                    }
                    else if (cancelado == "False" || cancelado == "false") {
                        opcanel1 = 0;
                    }


                    const idempresa = RegEmpresa.value;
                    const idTipoPeriodo = RegTipoperiodo1.value;
                    //const idPeriodo = op2;
                    separador = "-",
                    limite = 2,
                    arreglosubcadena = RegRenglon.value.split(separador, limite);
                    const idRenglon = arreglosubcadena[0];
                    const icance = opcanel1;
                    const iEleNom = '39';
                    const idAcumulado = iAcumulado.value;
                    const dataSend2 = {

                        iIdDefinicionHd: IdDh, iIdEmpresa: idempresa,
                        iTipodeperiodo: idTipoPeriodo, /*iIdperiodo: idPeriodo,*/
                        iRenglon: idRenglon, iCancelado: icance, iElementonomina: iEleNom,
                        iEsespejo: ispejo, iIdAcumulado: idAcumulado
                    };
                    const dataSend3 = {
                        iIdDefinicionHd: IdDh, iIdEmpresa: idempresa,
                        iRenglon: idRenglon, iElementonomina: iEleNom
                    };
                    $.ajax({
                        url: "../Nomina/ExiteRenglon",
                        type: "POST",
                        data: dataSend3,
                        success: function (data) {
                            console.log(data[0].iIdDefinicionHd);
                            if (data[0].iIdDefinicionHd == 0) {
                                $.ajax({
                                    url: "../Nomina/insertDefinicioNl",
                                    type: "POST",
                                    data: dataSend2,
                                    success: function (data) {
                                        if (data.sMensaje == "success") {
                                            $("#TpDefinicion").click();
                                            FcargaPercepciones();
                                            fshowtypealert('Registro correcto!', ' Percepción Guardada', 'success');
                                            RegEmpresa.value = "0";
                                            RegRenglon.value = "0";
                                            RegTipoperiodo1.value = "0";
                                            RegEspejo.value = "0";
                                            RegAcumulado.value = "0";

                                        } else {
                                            fshowtypealert('Error', 'Contacte a sistemas', 'error');
                                        }
                                    },
                                    error: function (jqXHR, exception) {
                                        fcaptureaerrorsajax(jqXHR, exception);
                                    }
                                });

                            }
                            if (data[0].iIdDefinicionHd > 0) {
                                fshowtypealert("Nomina/Percepción", "La percepcion ya esta ingresada favor de ingresar otra percepción diferente", "warning")
                            }


                        },
                        error: function (jqXHR, exception) {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    });

                },
            });
        }

        else {
            console.log('mesaje de error')
            fshowtypealert('warning', 'Los campos: Empresa, Renglo, Tipo de periodo y Es espejo son obligatorios', 'warning');

        }

    };

    btnAgregarPercep.addEventListener('click', FAgregaper);

    // abre ventana y carga datos 
    botonActuPer = () => {
        $("#TbPercepciones").click();
        btnAgregarPercep.style.visibility = 'hidden';
        btnActualizarPercep.style.visibility = 'visible';
        for (var i = 0; i < RegEmpresa.length; i++) {
            
            if (RegEmpresa.options[i].text == DatoEmpresa) {
                // seleccionamos el valor que coincide
                RegEmpresa.selectedIndex = i;
            }

        }
        var opvalEmpresa = RegEmpresa.value;
        const dataSend = { IdEmpresa: opvalEmpresa, iElemntoNOm: 1 };


        $("#RegRenglon").empty();
        $('#RegRenglon').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisRenglon",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    console.log('imprimerenglon');
                    document.getElementById("RegRenglon").innerHTML += `<option value='${data[i].iIdRenglon}'>${data[i].sNombreRenglon}</option>`;
                }

                for (i = 0; i < RegRenglon.length; i++) {
                    if (RegRenglon.options[i].text == DatoRenglon) {
                        // seleccionamos el valor que coincide
                        RegRenglon.selectedIndex = i;
                    }
                }


                var opEmpresaval = RegEmpresa.value;
                OpRenglon = RegEmpresa.options[RegEmpresa.selectedIndex].text;
                const opRenglonTex = RegRenglon.value;
                const dataSend3 = { iIdEmpresa: opEmpresaval, iIdRenglon: opRenglonTex };
                console.log(dataSend3);
                $("#RegAcumulado").empty();
                $('#RegAcumulado').append('<option value="0" selected="selected">Selecciona</option>');
                $.ajax({
                    url: "../Nomina/LisAcumulado",
                    type: "POST",
                    data: dataSend3,
                    success: (data) => {

                        for (i = 0; i < data.length; i++) {
                            document.getElementById("RegAcumulado").innerHTML += `<option value='${data[i].iIdAcumulado}'>${data[i].sDesAcumulado}</option>`;
                        }
                        for (var i = 0; i < RegAcumulado.length; i++) {
                            if (RegAcumulado.options[i].text == datoacumulado) {
                                // seleccionamos el valor que coincide
                                RegAcumulado.selectedIndex = i;
                            }
                        }
                    },
                });
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

        var op = RegEmpresa.options[RegEmpresa.selectedIndex].text;
        var opval = RegEmpresa.value;
        const dataSend2 = { IdEmpresa: opval };
        $("#RegTipoperiodo1").empty();
        $('#RegTipoperiodo1').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisTipPeriodo",
            type: "POST",
            data: dataSend2,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegTipoperiodo1").innerHTML += `<option value='${data[i].iId}'>${data[i].sValor}</option>`;
                }
                for (var i = 0; i < RegTipoperiodo1.length; i++) {
                    if (RegTipoperiodo1.options[i].text == datotipoperiodo) {
                        // seleccionamos el valor que coincide
                        RegTipoperiodo1.selectedIndex = i;
                    }

                }

                $("#TpDefinicion").click();
                const dataSend4 = { iIdFinicion: IdDh };
                $.ajax({
                    url: "../Nomina/DefCancelado",
                    type: "POST",
                    data: dataSend4,
                    success: (data) => {
                        for (i = 0; i < data.length; i++) {
                            AnioPre = data[i].iAno;
                        }
                        var OpEmpresa = RegEmpresa.value;
                        const OpIdTipoperiodo = RegTipoperiodo1.value;
                        const anio = AnioPre;
                        const dataSend = { iIdEmpresesas: OpEmpresa, ianio: anio, iTipoPeriodo: OpIdTipoperiodo };
                        $("#RegPeridoPer").empty();
                        $('#RegPeridoPer').append('<option value="0" selected="selected">Selecciona</option>');
                        $.ajax({
                            url: "../Nomina/ListPeriodo",
                            type: "POST",
                            data: dataSend,
                            success: (data) => {
                                if (data.length > 0) {
                                    for (i = 0; i < data.length; i++) {
                                        document.getElementById("RegPeridoPer").innerHTML += `<option value='${data[i].iId}'>${data[i].iPeriodo}</option>`;

                                    }
                                    for (i = 0; i < RegPeridoPer.length; i++) {
                                        if (RegPeridoPer.options[i].text == datoperiodo) {
                                            // seleccionamos el valor que coincide
                                            RegPeridoPer.selectedIndex = i;
                                        }

                                    }
                                }
                            },


                        });

                    }
                });

            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

        for (i = 0; i < RegEspejo.length; i++) {
            if (RegEspejo.options[i].text == datoespejo) {
                // seleccionamos el valor que coincide
                RegEspejo.selectedIndex = i;
            }

        }


    };
    BActuPer.addEventListener('click', botonActuPer);

    BActuPer.style.visibility = 'hidden';
    BAgregarPer.style.visibility = 'hidden';

    $("#TbPercepciones").on('rowselect', function (event) {
        var args = event.args;
        var row = $("#TbPercepciones").jqxGrid('getrowdata', args.rowindex);

        DatoiId = row['iIdDefinicionln'];
        DatoEmpresa = row['IdEmpresa'];
        DatoRenglon = row['iRenglon'];
        datotipoperiodo = row['iTipodeperiodo'];
        datoespejo = row['iEsespejo'];
        datoacumulado = row['iIdAcumulado'];
        //datoperiodo = row['iIdperiodo'];

    });

    // Funcion limpia los campos de percepcion de la pantalla de agregar Percepciones
    FlimpicamposPEr = () => {

        RegEmpresa.value = "0";
        RegRenglon.value = "0";
        RegTipoperiodo1.value = "0";
        RegEspejo.value = "0";
        RegAcumulado.value = "0";
        //RegPeridoPer.value = "0";

    };


    btnCierrapercepcion.addEventListener('click', FlimpicamposPEr);

    //Funcion que guarda la actulalizacion de la percepcion en el BD

    FactulizaPer = () => {

        var EmpresaPer = RegEmpresa.options[RegEmpresa.selectedIndex].text;
        var EmpresaIDper = RegEmpresa.value;
        var RenglonPre = RegRenglon.options[RegRenglon.selectedIndex].text;
        separador = "-",
        limite = 2,
        arreglosubcadena = RegRenglon.value.split(separador, limite);
        var RenglonPerId = arreglosubcadena[0];
        var TipoPeriodoPre = Tpoperiodo1.options[Tpoperiodo1.selectedIndex].text;

        var TipoPeriodoPreId = Tpoperiodo1.value;
        var EspejoPre = iRegEspejo.options[iRegEspejo.selectedIndex].text;
        var EspejoPreId = iRegEspejo.value;
        var AcumuladoPre = iAcumulado.options[iAcumulado.selectedIndex].text;
        var AcumuladoPreId = iAcumulado.value;
        //var PeriodoPer = iRegPeridoPer.options[iRegPeridoPer.selectedIndex].text;
        //var PeriodoPerId = iRegPeridoPer.value;

        $("#TbPercepciones").click();


        if (DatoRenglon != RenglonPre && DatoEmpresa != EmpresaPer || datotipoperiodo != TipoPeriodoPre || datoespejo != EspejoPre || datoacumulado != AcumuladoPre /*|| datoperiodo != PeriodoPer*/) {

            if (EspejoPreId == 2) { EspejoPreId = 0; }
            const dataSend2 = {
                iIdDefinicionln: DatoiId, iIdEmpresa: EmpresaIDper,
                iTipodeperiodo: TipoPeriodoPreId, /*iIdperiodo: PeriodoPer,*/
                iRenglon: RenglonPerId, iEsespejo: EspejoPreId, iIdAcumulado: AcumuladoPreId
            };
            const dataSend3 = {
                iIdDefinicionHd: IdDh, iIdEmpresa: EmpresaIDper,
                iRenglon: RenglonPerId, iElementonomina: 39
            };

            $.ajax({
                url: "../Nomina/ExiteRenglon",
                type: "POST",
                data: dataSend3,
                success: function (data) {
                    console.log(data[0].iIdDefinicionHd);
                    if (data[0].iIdDefinicionHd == 0) {

                        $.ajax({
                            url: "../Nomina/UpdatePtDefinicionNl",
                            type: "POST",
                            data: dataSend2,
                            success: function (data) {
                                if (data.sMensaje == "success") {

                                    $("#TpDefinicion").click();
                                    FcargaPercepciones();
                                    fshowtypealert('Registro Actualizado!', 'Definicion de Percepción', 'success');

                                } else {
                                    fshowtypealert('Error', 'Contacte a sistemas', 'error');
                                }
                            },
                            error: function (jqXHR, exception) {
                                fcaptureaerrorsajax(jqXHR, exception);
                            }
                        });

                    }
                    if (data[0].iIdDefinicionHd == DatoiId) {

                        $.ajax({
                            url: "../Nomina/UpdatePtDefinicionNl",
                            type: "POST",
                            data: dataSend2,
                            success: function (data) {
                                if (data.sMensaje == "success") {

                                    $("#TpDefinicion").click();
                                    FcargaPercepciones();
                                    fshowtypealert('Registro Actualizado!', 'Percepción actualizada', 'success');

                                } else {
                                    fshowtypealert('Error', 'Contacte a sistemas', 'error');
                                }
                            },
                            error: function (jqXHR, exception) {
                                fcaptureaerrorsajax(jqXHR, exception);
                            }
                        });

                    }
                    if (data[0].iIdDefinicionHd != DatoiId) {
                        fshowtypealert("Nomina/Percepción", "La percepcion ya esta registrada ", "warning")

                    }

                },
                error: function (jqXHR, exception) {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        }

        else {
            console.log('datos iguales');

        }

    };

    btnActualizarPercep.addEventListener('click', FactulizaPer);

    FDeleteDefinicionNLPer = () => {
        $("#TbPercepciones").click();
        console.log(DatoiId);
        const dataSend = {
            iIdDefinicion: DatoiId
        };
        console.log(dataSend);
        $.ajax({
            url: "../Nomina/UpdateRenglonDefNl",
            type: "POST",
            data: dataSend,
            success: function (data) {
                if (data.sMensaje == "success") {
                    fshowtypealert('Percepción!', 'Regitro Eliminado', 'success');
                    $("#TpDefinicion").click();
                    FcargaPercepciones();


                } else {
                    fshowtypealert('Error', 'Contacte a sistemas', 'error');
                }
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });
    }


    BEliminarPer.addEventListener('click', FDeleteDefinicionNLPer);
    BEliminarPer.style.visibility = 'hidden';
    FVisualizacionBotones = () => {
        btnAgregarPercep.style.visibility = "visible";
        btnActualizarPercep.style.visibility = "hidden";
        for (var i = 0; i < RegEmpresa.length; i++) {
            var datoempresa= RegEmpresa.options[i].text ;
               separador = " ",
                limite = 2,
                arreglosubcadena = datoempresa.split(separador, limite);
            if (arreglosubcadena[0] == BAgregarPer.value) {
                // seleccionamos el valor que coincide
                RegEmpresa.selectedIndex = i;
                RecargaLisRenglon2(arreglosubcadena[0]);
             
            }

        }
        var opvalEmpresa = RegEmpresa.value;
        const dataSend = { IdEmpresa: opvalEmpresa, iElemntoNOm: 1 };
        $("#RegRenglon").empty();
        $('#RegRenglon').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisRenglon",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    console.log('imprimerenglon');
                    document.getElementById("RegRenglon").innerHTML += `<option value='${data[i].iIdRenglon}'>${data[i].sNombreRenglon}</option>`;
                }        
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });
        var opval = RegEmpresa.value;
        const dataSend2 = { IdEmpresa: opval };
        $("#RegTipoperiodo1").empty();
        $('#RegTipoperiodo1').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisTipPeriodo",
            type: "POST",
            data: dataSend2,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegTipoperiodo1").innerHTML += `<option value='${data[i].iId}'>${data[i].sValor}</option>`;
                }
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });
       
    };

    BAgregarPer.addEventListener('click', FVisualizacionBotones);
    BAgregarPer.style.visibility = 'hidden';
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

    // Percepciones y deducciones

    //  Tab Deducciones

    var RosCountdedu;

    FcargaDeducionesGrip = () => {

        $("#TpDefinicion").click();
        const dataSend = { iIdDefinicionln: IdDh };

        for (var i = 0; i <= RosCountdedu; i++) {

            $("#TbDeducciones").jqxGrid('deleterow', i);
        }

        $.ajax({
            url: "../Nomina/listdatosDeducuiones",
            type: "POST",
            data: dataSend,
            success: function (data) {
                if (data[0].sMensaje == "success") {
                    if (data.length > 0) {
                        RosCountdedu = data.length;
                    }
                    var source =
                    {
                        localdata: data,
                        datatype: "array",
                        datafields:
                            [
                                { name: 'iIdDefinicionln', type: 'string' },
                                { name: 'IdEmpresa', type: 'string' },
                                { name: 'iRenglon', type: 'string' },
                                { name: 'iTipodeperiodo', type: 'string' },
                                { name: 'iIdAcumulado', type: 'string' },
                                { name: 'iEsespejo', type: 'string' }
                            ]
                    };
                    var dataAdapter = new $.jqx.dataAdapter(source);
                    $("#TbDeducciones").jqxGrid(
                        {
                            width: 620,
                            source: dataAdapter,
                            selectionmode: 'multiplerowsextended',
                            sortable: true,
                            pageable: true,
                            autoheight: true,
                            autoloadstate: false,
                            autosavestate: false,
                            columnsresize: true,
                            showtoolbar: true,
                            rendertoolbar: function (statusbar) {
                                var container = $("<div style='overflow: hidden; position: relative; margin: 4px;'></div>");
                                var addButton2 = $("<div style='float: left; '><img style='position: relative;' src='../../Scripts/jqxGrid/jqwidgets/styles/images/icon-plus.png'/></div>");
                                var ActuButton2 = $("<div style='float: left; '><img style='position: relative; ' src='../../Scripts/jqxGrid/jqwidgets/styles/images/icon-edit.png'/></div>");
                                var DeletButton2 = $("<div style='float: left; '><img style='position: relative; ' src='../../Scripts/jqxGrid/jqwidgets/styles/images/icon-delete.png'/></div>");
                                container.append(addButton2);
                                container.append(ActuButton2);
                                container.append(DeletButton2);
                                statusbar.append(container);
                                addButton2.jqxButton({ template: "link", width: 40, height: 25 });
                                ActuButton2.jqxButton({ template: "link", width: 40, height: 25 });
                                DeletButton2.jqxButton({ template: "link", width: 40, height: 25 });
                                addButton2.click(function (event) {
                                    $("#BAgregardedu2").click();
                                });
                                ActuButton2.click(function (event) {
                                    $("#BActudedu").click();
                                });
                                DeletButton2.click(function (event) {
                                    $("#BEliminardedu").click();
                                });
                            },
                            columns: [
                                { text: 'No.Linea', datafield: 'iIdDefinicionln', width: 80 },
                                { text: 'Empresa', datafield: 'IdEmpresa', width: 120 },
                                { text: 'Renglon', datafield: 'iRenglon', width: 180 },
                                { text: 'Tipo de periodo', datafield: 'iTipodeperiodo', width: 80 },
                                { text: 'Acumulado', datafield: 'iIdAcumulado', width: 80 },
                                { text: 'Esespejo', datafield: 'iEsespejo', width: 80 }
                            ]
                        });
                }
                if (data[0].sMensaje == "NotDat") {
                    if (data.length > 0) {
                        RosCountdedu = data.length;
                    }
                    var source =
                    {
                        localdata: data,
                        datatype: "array",
                        datafields:
                            [
                                { name: 'iIdDefinicionln', type: 'string' },
                                { name: 'IdEmpresa', type: 'string' },
                                { name: 'iRenglon', type: 'string' },
                                { name: 'iTipodeperiodo', type: 'string' },
                                { name: 'iIdAcumulado', type: 'string' },
                                { name: 'iEsespejo', type: 'string' }
                            ]
                    };
                    var dataAdapter = new $.jqx.dataAdapter(source);
                    $("#TbDeducciones").jqxGrid(
                        {
                            width: 620,
                            source: dataAdapter,
                            selectionmode: 'multiplerowsextended',
                            sortable: true,
                            pageable: true,
                            autoheight: true,
                            autoloadstate: false,
                            autosavestate: false,
                            columnsresize: true,
                            showtoolbar: true,
                            rendertoolbar: function (statusbar) {
                                var container = $("<div style='overflow: hidden; position: relative; margin: 4px;'></div>");
                                var addButton2 = $("<div style='float: left; '><img style='position: relative;' src='../../Scripts/jqxGrid/jqwidgets/styles/images/icon-plus.png'/></div>");
                                var ActuButton2 = $("<div style='float: left; '><img style='position: relative; ' src='../../Scripts/jqxGrid/jqwidgets/styles/images/icon-edit.png'/></div>");
                                var DeletButton2 = $("<div style='float: left; '><img style='position: relative; ' src='../../Scripts/jqxGrid/jqwidgets/styles/images/icon-delete.png'/></div>");
                                container.append(addButton2);
                                container.append(ActuButton2);
                                container.append(DeletButton2);
                                statusbar.append(container);
                                addButton2.jqxButton({ template: "link", width: 40, height: 25 });
                                ActuButton2.jqxButton({ template: "link", width: 40, height: 25 });
                                DeletButton2.jqxButton({ template: "link", width: 40, height: 25 });
                                addButton2.click(function (event) {
                                    $("#BAgregardedu2").click();
                                });
                                ActuButton2.click(function (event) {
                                    $("#BActudedu").click();
                                });
                                DeletButton2.click(function (event) {
                                    $("#BEliminardedu").click();
                                });
                            },
                            columns: [
                                { text: 'No.Linea', datafield: 'iIdDefinicionln', width: 80 },
                                { text: 'Empresa', datafield: 'IdEmpresa', width: 120 },
                                { text: 'Renglon', datafield: 'iRenglon', width: 180 },
                                { text: 'Tipo de periodo', datafield: 'iTipodeperiodo', width: 80 },
                                { text: 'Acumulado', datafield: 'iIdAcumulado', width: 80 },
                                { text: 'Esespejo', datafield: 'iEsespejo', width: 80 }
                            ]
                        });
                }
         
            }
        });

    };

    // declaracion de Variables 

    var RegEmpresade = document.getElementById('RegEmpresaDe');
    var RegRenglonde = document.getElementById('RegRenglonDe');
    var Tpoperiodode = document.getElementById('RegTipoperiodoDe');
    const iRegEspejode = document.getElementById('RegEspejoDe');
    var iAcumuladode = document.getElementById('RegAcumuladoDe');
    //var iRegPeridoDe = document.getElementById('RegPeridoDe');
    var AnioDeduc;
    var canceladode;


    // declaracion de botones 
    const BAgregardedu2 = document.getElementById('BAgregardedu2');
    const btnAgregarDedu = document.getElementById('btnAgregarDedu');
    const btnCierraDedu = document.getElementById('btnCierraDedu');
    const BActudedu = document.getElementById('BActudedu');
    const btnActualizarDedu = document.getElementById('btnActualizarDedu');
    const BEliminardedu = document.getElementById('BEliminardedu');

    // llena el drop lis de Empresa de Deduccion
    LisEmpresaDe = () => {

        $.ajax({
            url: "../Nomina/LisEmpresas",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegEmpresaDe").innerHTML += `<option value='${data[i].iIdEmpresa}'>${data[i].sNombreEmpresa}</option>`;
                }
            }
        });
    };

    LisEmpresaDe();

    // llenado de lista droplist de tipo de periodo 
    RecargaLisperiodoDe = () => {
        var op = RegEmpresade.options[RegEmpresade.selectedIndex].text;
        var opvalde = RegEmpresade.value;
        const dataSend = { IdEmpresa: opvalde };

        $("#RegTipoperiodoDe").empty();
        $('#RegTipoperiodoDe').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisTipPeriodo",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegTipoperiodoDe").innerHTML += `<option value='${data[i].iId}'>${data[i].sValor}</option>`;
                }
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

    };

    // llenado del droplist de renglon
    RecargaLisRenglonDe = () => {
        var OpRenglon = RegEmpresade.options[RegEmpresade.selectedIndex].text;
        var opvalEmpresaDe = RegEmpresade.value;
        const dataSend = { IdEmpresa: opvalEmpresaDe, iElemntoNOm: 2 };
        $("#RegRenglonDe").empty();
        $('#RegRenglonDe').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisRenglon",
            type: "POST",
            data: dataSend,
            success: (data) => {
                console.log(data+'1');
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegRenglonDe").innerHTML += `<option value='${data[i].iIdRenglon} - ${data[i].iEspejo}'>${data[i].sNombreRenglon}</option>`;
                }
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });
    };

    // llenado del droplist de renglon
    RecargaLisRenglonDe2 = (Idempresade) => {
        var OpRenglon = RegEmpresade.options[RegEmpresade.selectedIndex].text;
       
        const dataSend = { IdEmpresa: Idempresade, iElemntoNOm: 2 };
        $("#RegRenglonDe").empty();
        $('#RegRenglonDe').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisRenglon",
            type: "POST",
            data: dataSend,
            success: (data) => {
                console.log('2 vuelve a entrar aqui');
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegRenglonDe").innerHTML += `<option value='${data[i].iIdRenglon} - ${data[i].iEspejo}'>${data[i].sNombreRenglon}</option>`;
                }
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });
    };




    $('#RegEmpresaDe').change(function () {

        RecargaLisperiodoDe();
        RecargaLisRenglonDe();
    });

    // Funcion que llenado del droplist Acumulado

    RecargaLisAcumuladoDe = () => {

        var OpRenglon = RegEmpresaDe.options[RegEmpresaDe.selectedIndex].text;
        var opEmpresaval = RegEmpresaDe.value;
        var OpRenglonint = RegRenglonDe.options[RegRenglonDe.selectedIndex].text;
        separador = "-",
        limite = 2,
        arreglosubcadena2 = RegRenglonDe.value.split(separador, limite);

        const opRenglonTex = arreglosubcadena2[0] ;
        const dataSend = { iIdEmpresa: opEmpresaval, iIdRenglon: opRenglonTex };
        $("#RegAcumuladoDe").empty();
        $('#RegAcumuladoDe').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisAcumulado",
            type: "POST",
            data: dataSend,
            success: (data) => {

                console.log(data);
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegAcumuladoDe").innerHTML += `<option value='${data[i].iIdAcumulado}'>${data[i].sDesAcumulado}</option>`;


                }
            },


        });
        if (arreglosubcadena[1] == 0) {
            iRegEspejode.selectedIndex = 2;
        }
        if (arreglosubcadena[1] == 1) {
            iRegEspejode.selectedIndex = 1;
        }

    };

    $('#RegRenglonDe').change(function () {

        RecargaLisAcumuladoDe();

    });

    /// Elimina Dedución de la tabla TDefiniciones 
    FDeleteDefinicionNLdedu = () => {

        $("#TbDeducciones").click();
      
        const dataSend = {
            iIdDefinicion: DatoiIdde
        };
        console.log(dataSend);
        $.ajax({
            url: "../Nomina/UpdateRenglonDefNl",
            type: "POST",
            data: dataSend,
            success: function (data) {
                if (data.sMensaje == "success") {
                    fshowtypealert('Deducción!', 'Regitro Eliminado', 'success');
                    $("#TpDefinicion").click();
                    FcargaDeducionesGrip();


                } else {
                    fshowtypealert('Error', 'Contacte a sistemas', 'error');
                }
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

    };

    BEliminardedu.addEventListener('click', FDeleteDefinicionNLdedu)
    BEliminardedu.style.visibility = 'hidden';

    // Funcion llenado el drop list de periodo de dedudcion

    //FrecargaPeridoDeduc = () => {

    //    $("#TpDefinicion").click();

    //    const dataSend = { iIdFinicion: IdDh };

    //            $.ajax({
    //                url: "../Nomina/DefCancelado",
    //                type: "POST",
    //                data: dataSend,
    //                success: (data) => {

    //                    console.log(data);
    //                    for (i = 0; i < data.length; i++) {
    //                        AnioDeduc = data[i].iAno;

    //                    }

    //                    var OpEmpresa = RegEmpresade.value;
    //                    const OpIdTipoperiodo = RegTipoperiodoDe.value;
    //                    console.log(AnioDeduc);
    //                    const anio = AnioDeduc;
    //                    const dataSend = { iIdEmpresesas: OpEmpresa, ianio: anio, iTipoPeriodo: OpIdTipoperiodo };
    //                    $("#RegPeridoDe").empty();
    //                    //$('#RegPeridoDe').append('<option value="0" selected="selected">Selecciona</option>');
    //                    console.log(dataSend);
    //                    $.ajax({
    //                        url: "../Nomina/ListPeriodo",
    //                        type: "POST",
    //                        data: dataSend,
    //                        success: (data) => {
    //                            console.log(data);
    //                            intpe = data.length - 1
    //                          //$('#RegPeridoDe').append('<option value="0" selected="selected">Selecciona</option>');
    //                            $('#RegPeridoDe').append(`<option value=" ${data[intpe].iId} " selected="selected">${data[intpe].iPeriodo}</option>`)
    //                            //for (i = 0; i < data.length; i++) {
    //                            //    document.getElementById("RegPeridoDe").innerHTML += `<option value='${data[i].iId}'>${data[i].iPeriodo}</option>`;

    //                            //}
    //                        },
    //                    });
    //                }
    //            });



    //}
    //$('#RegTipoperiodoDe').change(function () {

    //    FrecargaPeridoDeduc();

    //});

    // Funcion Guarda Deducion en BD

    FGuardarDedBD = () => {
        var op = RegTipoperiodoDe.options[RegTipoperiodoDe.selectedIndex].text;
        console.log(op);
        if (RegEmpresade.value != "0" && RegRenglonde.value != "0" && iRegEspejode.value != "0" && op != "Selecciona") {

            $("#TpDefinicion").click();
            var ispejode;
            if (iRegEspejode.value == "1") {
                ispejode = "1";
            }
            else if (iRegEspejode.value == "2") {
                ispejode = "0";
            }
            const dataSend = { iIdFinicion: IdDh };
            $.ajax({
                url: "../Nomina/DefCancelado",
                type: "POST",
                data: dataSend,
                success: (data) => {

                    for (i = 0; i < data.length; i++) {
                        canceladode = data[i].iCancelado;
                    }
                    var opcanel;
                    if (canceladode == "True" || canceladode == "true") {
                        opcanel = 1;
                    }
                    else if (canceladode == "False" || canceladode == "false") {
                        opcanel = 0;
                    }
                    //var op2 = RegPeridoDe.options[RegPeridoDe.selectedIndex].text;
                    //if (op2 == "Selecciona") {
                    //    op2 = "0";
                    //}
                    const idempresade = RegEmpresade.value;
                    const idTipoPeriodode = Tpoperiodode.value;
                    //const idPeriodode = op2;
                    separador = "-",
                    limite = 2,
                    arreglosubcadena2 = RegRenglonDe.value.split(separador, limite);
                    const idRenglonde = arreglosubcadena2[0];
                    const icancede = opcanel;
                    const iEleNomde = '40';
                    const idAcumuladode = iAcumuladode.value;

                    const dataSend2 = {
                        iIdDefinicionHd: IdDh, iIdEmpresa: idempresade,
                        iTipodeperiodo: idTipoPeriodode,/* iIdperiodo: idPeriodode,*/
                        iRenglon: idRenglonde, iCancelado: icancede, iElementonomina: iEleNomde,
                        iEsespejo: ispejode, iIdAcumulado: idAcumuladode
                    };

                    const dataSend3 = {
                        iIdDefinicionHd: IdDh, iIdEmpresa: idempresade,
                        iRenglon: idRenglonde, iElementonomina: iEleNomde
                    };

                    $.ajax({
                        url: "../Nomina/ExiteRenglon",
                        type: "POST",
                        data: dataSend3,
                        success: function (data) {
                            console.log(data[0].iIdDefinicionHd);
                            if (data[0].iIdDefinicionHd == 0) {

                                $.ajax({
                                    url: "../Nomina/insertDefinicioNl",
                                    type: "POST",
                                    data: dataSend2,
                                    success: function (data) {
                                        if (data.sMensaje == "success") {
                                            fshowtypealert('Registro correcto!', 'Deduccion guardada', 'success');
                                            $("#TpDefinicion").click();
                                            FcargaDeducionesGrip();
                                            RegEmpresade.value = "0";
                                            RegRenglonde.value = "0";
                                            Tpoperiodode.value = "0";
                                            iRegEspejode.value = "0";
                                            iAcumuladode.value = "0";
                                            //iRegPeridoDe.value = "0";

                                        } else {
                                            fshowtypealert('Error', 'Contacte a sistemas', 'error');
                                        }
                                    },
                                    error: function (jqXHR, exception) {
                                        fcaptureaerrorsajax(jqXHR, exception);
                                    }
                                });
                            }
                            if (data[0].iIdDefinicionHd > 0) {
                                fshowtypealert("Nomina/Deducción", "La deducción ya esta ingresada favor de ingresar otra deducción diferente", "warning")
                            }
                        },
                        error: function (jqXHR, exception) {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    });


                },
                error: function (jqXHR, exception) {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        }
        else {
            console.log('mesaje de error')
            fshowtypealert('warning', 'Los campos: Empresa, Renglo, Tipo de periodo, Periodo y Es espejo son obligatorios', 'warning');
        }
    };
    btnAgregarDedu.addEventListener('click', FGuardarDedBD);

    // Funcion limpia campos

    FLimpiCamposDedu = () => {

        RegEmpresade.value = "0";
        RegRenglonde.value = "0";
        Tpoperiodode.value = "0";
        iRegEspejode.value = "0";
        iAcumuladode.value = "0";
        //iRegPeridoDe.value = "0";
    };

    btnCierraDedu.addEventListener('click', FLimpiCamposDedu);

    // Funcion desaprece el boton de agregar y aparece el boton de actualizar y recarga datos
    FActualizaboton = () => {

        btnAgregarDedu.style.visibility = "hidden";
        btnActualizarDedu.style.visibility = "visible";
        $("#TbDeducciones").click();

        for (var i = 0; i < RegEmpresade.length; i++) {
            if (RegEmpresade.options[i].text == DatoEmpresade) {
                // seleccionamos el valor que coincide
                RegEmpresade.selectedIndex = i;
            }

        };

        var OpRenglon = RegEmpresaDe.options[RegEmpresaDe.selectedIndex].text;
        var opvalEmpresa = RegEmpresaDe.value;
        const dataSend = { IdEmpresa: opvalEmpresa, iElemntoNOm: 2 };


        $("#RegRenglonDe").empty();
        $('#RegRenglonDe').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({

            url: "../Nomina/LisRenglon",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegRenglonDe").innerHTML += `<option value='${data[i].iIdRenglon}'>${data[i].sNombreRenglon}</option>`;
                }
                console.log('Reno dedu' + RegRenglonDe.length);
                for (i = 0; i < RegRenglonDe.length; i++) {
                    console.log(RegRenglonDe.options[i].text + '=' + DatoRenglonde);
                    if (RegRenglonDe.options[i].text == DatoRenglonde) {
                        console.log('Entro' + i);
                        // seleccionamos el valor que coincide
              
                        RegRenglonDe.selectedIndex = i;
                        i = 100000;
                    }
                }

                var opEmpresaval = RegEmpresaDe.value;
                OpRenglon = RegEmpresaDe.options[RegEmpresaDe.selectedIndex].text;
                const opRenglonTex = RegRenglonde.value;
                const dataSend3 = { iIdEmpresa: opEmpresaval, iIdRenglon: opRenglonTex };
                $("#RegAcumuladoDe").empty();
                $('#RegAcumuladoDe').append('<option value="0" selected="selected">Selecciona</option>');
                $.ajax({
                    url: "../Nomina/LisAcumulado",
                    type: "POST",
                    data: dataSend3,
                    success: (data) => {

                        for (i = 0; i < data.length; i++) {
                            document.getElementById("RegAcumuladoDe").innerHTML += `<option value='${data[i].iIdAcumulado}'>${data[i].sDesAcumulado}</option>`;
                        }

                        for (i = 0; i < RegAcumuladoDe.length; i++) {
                            if (RegAcumuladoDe.options[i].text == datoacumuladode) {
                                // seleccionamos el valor que coincide
                                RegAcumuladoDe.selectedIndex = i;
                            }

                        }
                    },
                });


            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });


        var op = RegEmpresaDe.options[RegEmpresaDe.selectedIndex].text;
        var opvalde = RegEmpresaDe.value;
        const dataSend2 = { IdEmpresa: opvalde };

        $("#RegTipoperiodoDe").empty();
        $('#RegTipoperiodoDe').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisTipPeriodo",
            type: "POST",
            data: dataSend2,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegTipoperiodoDe").innerHTML += `<option value='${data[i].iId}'>${data[i].sValor}</option>`;
                }

                for (var i = 0; i < RegTipoperiodoDe.length; i++) {
                    if (RegTipoperiodoDe.options[i].text == datotipoperiodode) {
                        // seleccionamos el valor que coincide
                        RegTipoperiodoDe.selectedIndex = i;
                       
                    }

                }
          
                $("#TpDefinicion").click();

                //const dataSend4 = { iIdFinicion: IdDh };
                //$.ajax({
                //    url: "../Nomina/DefCancelado",
                //    type: "POST",
                //    data: dataSend4,
                //    success: (data) => {
                //        for (i = 0; i < data.length; i++) {
                //            AnioPre = data[i].iAno;
                //        }
                //        var OpEmpresa = RegEmpresaDe.value;
                //        const OpIdTipoperiodo = RegTipoperiodoDe.value;
                //        const anio = AnioPre;
                //        const dataSend = { iIdEmpresesas: OpEmpresa, ianio: anio, iTipoPeriodo: OpIdTipoperiodo };
                //        $("#RegPeridoDe").empty();
                //        $('#RegPeridoDe').append('<option value="0" selected="selected">Selecciona</option>');
                //        console.log(dataSend);
                //        $.ajax({
                //            url: "../Nomina/ListPeriodo",
                //            type: "POST",
                //            data: dataSend,
                //            success: (data) => {

                //                console.log(data);
                //                for (i = 0; i < data.length; i++) {
                //                    document.getElementById("RegPeridoDe").innerHTML += `<option value='${data[i].iId}'>${data[i].iPeriodo}</option>`;

                //                }

                //                for (i = 0; i < RegPeridoDe.length; i++) {
                //                    if (RegPeridoDe.options[i].text == datoperiodode) {
                //                        // seleccionamos el valor que coincide
                //                        RegPeridoDe.selectedIndex = i;
                //                    }

                //                }

                //            },


                //        });

                //    }
                //});

            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

        for (i = 0; i < RegEspejoDe.length; i++) {
            if (RegEspejoDe.options[i].text == datoespejode) {
                // seleccionamos el valor que coincide
                RegEspejoDe.selectedIndex = i;
            }

        }


    };
    BActudedu.addEventListener('click', FActualizaboton);

    BActudedu.style.visibility = 'hidden';
    // Funcion desaprece el boton de actualizar y aparece el boton de agregar
    Fagregarboton = () => {

        btnAgregarDedu.style.visibility = "visible";
        btnActualizarDedu.style.visibility = "hidden";
        for (var i = 0; i < RegEmpresade.length; i++) {
            var datoempresa = RegEmpresade.options[i].text;
                separador = " ",
                limite = 2,
                arreglosubcadena = datoempresa.split(separador, limite);
            if (arreglosubcadena[1] == empresaSelect) {
                // seleccionamos el valor que coincide
                RegEmpresade.selectedIndex = i;
            }

        };
        var opvalEmpresa = RegEmpresaDe.value;
        const dataSend = { IdEmpresa: opvalEmpresa, iElemntoNOm: 2 };
        $("#RegRenglonDe").empty();
        $('#RegRenglonDe').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({

            url: "../Nomina/LisRenglon",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegRenglonDe").innerHTML += `<option value='${data[i].iIdRenglon} - ${data[i].iEspejo}'>${data[i].sNombreRenglon}</option>`;
                }           

            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });
        var opvalde = RegEmpresaDe.value;
        const dataSend2 = { IdEmpresa: opvalde };
        $("#RegTipoperiodoDe").empty();
        $('#RegTipoperiodoDe').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisTipPeriodo",
            type: "POST",
            data: dataSend2,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegTipoperiodoDe").innerHTML += `<option value='${data[i].iId}'>${data[i].sValor}</option>`;
                }   
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

    };

    BAgregardedu2.addEventListener('click', Fagregarboton);
    BAgregardedu2.style.visibility = 'hidden';

    /// valores seleccionados en tablaDedu

    $("#TbDeducciones").on('rowselect', function (event) {
        var args = event.args;
        var row = $("#TbDeducciones").jqxGrid('getrowdata', args.rowindex);

        DatoiIdde = row['iIdDefinicionln'];
        DatoEmpresade = row['IdEmpresa'];
        DatoRenglonde = row['iRenglon'];
        datotipoperiodode = row['iTipodeperiodo'];
        datoespejode = row['iEsespejo'];
        datoacumuladode = row['iIdAcumulado'];
        //datoperiodode = row['iIdperiodo']

    });


    //// FGuardar Actualizacion de Deduccion en la BD

    FActualizaDed = () => {

        var Empresaded = RegEmpresaDe.options[RegEmpresaDe.selectedIndex].text;
        var EmpresaIDded = RegEmpresaDe.value;
        var Renglonded = RegRenglonDe.options[RegRenglonDe.selectedIndex].text;
        separador = "-",
        limite = 2,
        arreglosubcadena = RegRenglonDe.value.split(separador, limite);
        var RenglondedId = arreglosubcadena[0];
        var TipoPeriododed = RegTipoperiodoDe.options[RegTipoperiodoDe.selectedIndex].text;
        var TipoPeriododedId = RegTipoperiodoDe.value;
        var Espejoded = RegEspejoDe.options[RegEspejoDe.selectedIndex].text;
        var EspejodedId = RegEspejoDe.value
        var Acumuladoded = RegAcumuladoDe.options[RegAcumuladoDe.selectedIndex].text;
        var AcumuladodedId = RegAcumuladoDe.value;
        //var Periododed = RegPeridoDe.options[RegPeridoDe.selectedIndex].text;
        //var PeriododedId = RegPeridoDe.value;

        if (DatoRenglonde != Renglonded && DatoEmpresade != Empresaded || datotipoperiodode != TipoPeriododed || datoespejode != Espejoded || datoacumuladode != Acumuladoded /*|| datoperiodode != Periododed*/) {
            $("#TbDeducciones").click();
            if (EspejodedId == 2) { EspejodedId = 0 }
            const dataSend2 = {
                iIdDefinicionln: DatoiIdde, iIdEmpresa: EmpresaIDded,
                iTipodeperiodo: TipoPeriododedId,/* iIdperiodo: Periododed,*/
                iRenglon: RenglondedId, iEsespejo: EspejodedId, iIdAcumulado: AcumuladodedId
            };
            const dataSend3 = {
                iIdDefinicionHd: IdDh, iIdEmpresa: EmpresaIDded,
                iRenglon: RenglondedId, iElementonomina: 40
            };
            $.ajax({
                url: "../Nomina/ExiteRenglon",
                type: "POST",
                data: dataSend3,
                success: function (data) {
                    if (data[0].iIdDefinicionHd == 0) {
                        $.ajax({
                            url: "../Nomina/UpdatePtDefinicionNl",
                            type: "POST",
                            data: dataSend2,
                            success: function (data) {
                                if (data.sMensaje == "success") {
                                    console.log('aaa');
                                    $("#TpDefinicion").click();
                                    FcargaDeducionesGrip();
                                    fshowtypealert('Registro Actualizado!', ' Deducción', 'success');

                                } else {
                                    fshowtypealert('Error', 'Contacte a sistemas', 'error');
                                }
                            },
                            error: function (jqXHR, exception) {
                                fcaptureaerrorsajax(jqXHR, exception);
                            }
                        });

                    }
                    if (data[0].iIdDefinicionHd == DatoiIdde) {
                        $.ajax({
                            url: "../Nomina/UpdatePtDefinicionNl",
                            type: "POST",
                            data: dataSend2,
                            success: function (data) {
                                if (data.sMensaje == "success") {
                                    console.log('aaa');
                                    $("#TpDefinicion").click();
                                    FcargaDeducionesGrip();
                                    fshowtypealert('Registro Actualizado!', ' Deducción', 'success');

                                } else {
                                    fshowtypealert('Error', 'Contacte a sistemas', 'error');
                                }
                            },
                            error: function (jqXHR, exception) {
                                fcaptureaerrorsajax(jqXHR, exception);
                            }
                        });

                    }
                    if (data[0].iIdDefinicionHd != DatoiIdde) {
                        fshowtypealert("Nomina/Deducción", "La deducción ya esta ingresada favor de ingresar otra deducción diferente", "warning")
                    }
                },
                error: function (jqXHR, exception) {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        }

        else {
            console.log('datos iguales');

        }
    };

    btnActualizarDedu.addEventListener('click', FActualizaDed);

    // validaciones

    $("#iAnoDe").keyup(function () {
        this.value = (this.value + '').replace(/[^0-9]/g, '');
    });

});