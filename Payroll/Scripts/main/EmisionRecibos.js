$(function () {
    //// Delcaracion de variables

    const DropGrup = document.getElementById('DropGrup');
    const DropContEje = document.getElementById('DropContEje');
    const TextDEesp = document.getElementById('TextDEesp');
    const TextSelecEmpre = document.getElementById('TextSelecEmpre');
    const CheckEmpresa = document.getElementById('CheckEmpresa');
    const TextBTotalEmple = document.getElementById('TextBTotalEmple');
    const DroTipoPeriodo = document.getElementById('DroTipoPeriodo');
    const DropPerido = document.getElementById('DropPerido');
    const btnGeneraPDF = document.getElementById('btn-GeneraPDF');
    const TextBAnioProce = document.getElementById('TextBAnioProce');
    const DroTipoRecibo = document.getElementById('DroTipoRecibo');
    const TextBRuta = document.getElementById('TextBRuta');
    const btnVerEje = document.getElementById('btnVerEje');
    const btnEnviCorre = document.getElementById('btnEnviCorre');
    //const BtnSenCorreo = document.getElementById('BtnSenCorreo');
    //const TextCopiaa = document.getElementById('TextCopiaa');
    const TextBTotalEjecutados = document.getElementById('TextBTotalEjecutados');

    var VarCheckEmpresa = document.getElementById('CheckEmpresa');
    var Empresas;
    
    DropGrup.value = 1;
    DropContEje.value = 1;
    $('#DropContEje').change(function () {
        var descrip;
      //  console.log(DropContEje.value);
        if (DropContEje.value == "1") {
            LisEmpresa();
            descrip = DropContEje.options[DropContEje.selectedIndex].text;
            document.getElementById("TextDEesp").innerHTML += descrip;
        }
     
    });
       /// Llena el DropEmpresa con el listado de empresas correspondientes
    LisEmpresa = () => {

        $.ajax({
            url: "../Nomina/LisEmpresas",
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
                            { name: 'iIdEmpresa', type: 'int' },
                            { name: 'sNombreEmpresa', type: 'string' },

                        ],
                    datatype: "array",
                    updaterow: function (rowid, rowdata) {
                        // synchronize with the server - send update command   
                    }
                };
                var dataAdapter = new $.jqx.dataAdapter(source);
                $("#DropEmpresa").jqxDropDownList({ placeHolder: "Selecciona", autoOpen: true,checkboxes: true, source: dataAdapter, displayMember: "sNombreEmpresa", valueMember: "iIdEmpresa", width: 335, height: 30, });            
                //$("#DropEmpresa").jqxDropDownList('checkAll');

            }
        });

    };
    LisEmpresa();
    $("#DropEmpresa").on('checkChange', function (event) {
        if (event.args) {
            var item = event.args.item;
            if (item) {
                var valueelement = $("<div></div>");
                valueelement.text("Value: " + item.value);
                var labelelement = $("<div></div>");
                labelelement.text("Label: " + item.label);
                var checkedelement = $("<div></div>");
                checkedelement.text("Checked: " + item.checked);
              //  $("#selectionlog").children().remove();

                var items = $("#DropEmpresa").jqxDropDownList('getCheckedItems');
                var checkedItems = "";
                var checkids = "";
                checkedItemsIdEmpleados = "";
                $.each(items, function (index) {
                    checkids += this.value + " ";
                    checkedItems += this.label + "\n";
                    //checkedItemsIdEmpleados += this.value + ",";
                });
             
                limpTextarea();
                document.getElementById("TextSelecEmpre").innerHTML += checkedItems;
                Empresas = checkids;
                FNoEmpeleados(checkids);
                FTipodePeriodo(checkids, 0,0);
                FPeriodo(checkids, 1);
            }
        }
    });




    /// Limpias el campo de Texttarea
    limpTextarea = () => {
        var text = " ";
        var lines = $('#TextSelecEmpre').val().split('\n');

        lines = lines.filter(function (val) {
            if (val.match(text)) return false
            else return true

        })

        $('#TextSelecEmpre').text(lines.join('\n'))

    };

    FValorChec = () => {
       
        if (VarCheckEmpresa.checked == true) {
            
             $("#DropEmpresa").jqxDropDownList('checkAll');
        }
        if (VarCheckEmpresa.checked == false) {
      
            $("#DropEmpresa").jqxDropDownList('uncheckAll');

        }

    };
    CheckEmpresa.checked = false;
    CheckEmpresa.addEventListener('click', FValorChec);

     // suma el numero de empleados de las Empresas selecionadas
    FNoEmpeleados = (Empresas) => {
        const dataSend = { IdEmpresas: Empresas };

        $.ajax({
            url: "../Nomina/NoEmpleados",
            type: "POST",
            data: dataSend,
            success: function (data){
                TextBTotalEmple.value = data[0].iNoEmpleados;
            },
        });

    };

    // Verifica que todas las empresas contengan el mismo tipo de periodo y lo llena el drop
    FTipodePeriodo = (sdEMpresa, op,tp) => {

        if (tp == 0) {
            const dataSend = { IdEmpresas: sdEMpresa, OP: op };
            $("#DroTipoPeriodo").empty();
            $('#DroTipoPeriodo').append('<option value="0" selected="selected">Selecciona</option>');
            $.ajax({
                url: "../Nomina/TipoPPeriodoEmision",
                type: "POST",
                data: dataSend,
                success: function (data) {
                    for (i = 0; i < data.length; i++) {

                        document.getElementById("DroTipoPeriodo").innerHTML += `<option value='${data[i].iId}'>${data[i].iId} ${data[i].sValor} </option>`;
                    }
                },
            });


        }
        if (tp == 1) {
            const dataSend = { IdEmpresas: sdEMpresa, OP: op };
            $("#DroTipoPeriodo").empty();
            $('#DroTipoPeriodo').append('<option value="0" selected="selected">Selecciona</option>');
            $.ajax({
                url: "../Nomina/TipoPPeriodoEmision",
                type: "POST",
                data: dataSend,
                success: function (data) {
                    for (i = 0; i < data.length; i++) {
                        document.getElementById("DroTipoPeriodo").innerHTML += `<option value='${data[i].iId}'>${data[i].iId} ${data[i].sValor} </option>`;
                    }
                },
            });
            console.log(DroTipoPeriodo.contains);
            console.log(DroTipoPeriodo.length);
            for (var i = 0; i < DroTipoPeriodo.contains; i++) {
                console.log(DroTipoPeriodo.options[i].text + 'su valor ' + DroTipoPeriodo.value );

            }

        }



    };


    FPeriodo = (sEmpresas, op) => {
        const dataSend = { IdEmpresas: sEmpresas, OP: op };
        $("#DropPerido").empty();
        $('#DropPerido').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/TipoPPeriodoEmision",
            type: "POST",
            data: dataSend,
            success: function (data) {
                for (i = 0; i < data.length; i++) {
                 
                    document.getElementById("DropPerido").innerHTML += `<option value='${data[i].iPeriodo}'>${data[i].iPeriodo}  de ${data[i].sFechaInicio} al ${data[i].sFechaFinal} </option>`;
                }
            },
        });


    };

    /// Genera los pdf 
    FGeneraPDF = () => {

        if (TextBAnioProce.value != "" && TextBAnioProce.value != " " ) {

            if (DroTipoPeriodo.value > 0) {

                if (DropPerido.value > 0) {

                    if (Empresas.length > 0) {
                        FCargamasibaPDF(TextBAnioProce.value, DroTipoPeriodo.value, DropPerido.value, Empresas, TextDEesp.value)

                    }
                    else {
                        fshowtypealert('Emisión de recibos', "seleccionar una empresa por lo menos", 'warning');

                    }

                }
                else {
                    fshowtypealert('Emisión de recibos', "seleccionar un periodo", 'warning');

                }
                
            }
            //&&  && && 
            else {

                fshowtypealert('Emisión de recibos', "seleccionar un tipo de periodo", 'warning');
            }
        }
        else {
            
            fshowtypealert('Emisión de recibos', "agregar año", 'warning');
        }



    };
    btnGeneraPDF.addEventListener('click',FGeneraPDF);

    FCargamasibaPDF = (anio, tipoPer, Per, sEmpresas,descrip) => {
    
        const dataSend = { Anio: anio, TipoPeriodo: tipoPer, Perido: Per, sIdEmpresas: sEmpresas, iRecibo: DroTipoRecibo.value, sDEscripcion: descrip };
      
            btnEnviCorre.value= 1
        

        $.ajax({
            url: "../Empleados/GenPDF",
            type: "POST",
            data: dataSend,
            success: function (data) {            
                TextBRuta.value = data[0].sUrl;
                TextBTotalEmple.value = data[0].iNoEjecutados;
                fshowtypealert('Emisión de recibos', "PDF creados exitosa mente", 'succes');
            },
        });

    };

    /// Muestra en pantalla la ultima ejecucion realizada 


    FtheLastEje = () => {      
        $.ajax({
            url: "../Empleados/TheLastEjecution",
            type: "POST",
            data: JSON.stringify(),
            success:  (TablasDat)=> {
                if (TablasDat.TablasDat[0].sMensaje = "succes") {
                    for (var i = 0; i < DropGrup.length; i++) {
                        if (DropGrup.options[i].text == "IPSNet") {
                            // seleccionamos el valor que coincide
                            DropGrup.selectedIndex = i;
                        }
                    }
                    for (var i = 0; i < DropContEje.length; i++) {
                        if (DropContEje.options[i].text == TablasDat.TablasDat[0].sDescripcion) {
                            DropContEje.selectedIndex = i;
                        }
                    }                
                    TextBAnioProce.value = TablasDat.TablasDat[0].iAnio;
                    descrip = TablasDat.TablasDat[0].sDescripcion;
                    document.getElementById("TextDEesp").innerHTML += descrip;
                    LisEmpresa();              
                    $("#DropEmpresa").jqxDropDownList('checkItem', TablasDat.TablasDat[0].iIdempresa);
                    DroTipoRecibo.selectedIndex = TablasDat.TablasDat[0].iRecibo;
                    var Tp = "";
                    if (TablasDat.TablasDat[0].iTipoPeriodo == 1) {
                        Tp = " Semanal"; 
                    };
                    if (TablasDat.TablasDat[0].iTipoPeriodo == 3) {
                        Tp=" Quincenal"
                    };
                    $("#DroTipoPeriodo").empty();
                    document.getElementById("DroTipoPeriodo").innerHTML += `<option value='${TablasDat.TablasDat[0].iTipoPeriodo}'>${TablasDat.TablasDat[0].iTipoPeriodo} ${Tp} </option>`;

                    $("#DropPerido").empty();
                    document.getElementById("DropPerido").innerHTML += `<option value='${TablasDat.TablasDat[0].iPeriodo}'>${TablasDat.TablasDat[0].iPeriodo}  </option>`;

                    TextBTotalEjecutados.value = TablasDat.TablasDat[0].iNoEje;
                    
                }
                else {
                    btnEnviCorre.value = 0;
                    fshowtypealert('Error', 'Contacte a sistemas', 'error');

                };
                if (TablasDat.LSelloSat[0].sMensaje = "Succes") {
                    btnEnviCorre.value = 1;
                    if (TablasDat.TablasDat[0].iRecibo == "1") {
                        var source =
                        {
                            localdata: TablasDat.LSelloSat,
                            datatype: "array",
                            datafields:
                                [
                                    { name: 'iIdEmpresa', type: 'int' },
                                    { name: 'sNomEmpleado', type: 'string' },
                                    { name: 'ianio', type: 'int' },
                                    { name: 'iTipoPeriodo', type: 'int' },
                                    { name: 'iPeriodo', type: 'int' },
                                    { name: 'bEmailSent', type: 'string' },
                                    { name: 'sEmailSendSim', type: 'string' },
                                ]
                        };
                        var dataAdapter = new $.jqx.dataAdapter(source);
                        $("#TbEjeSend").jqxGrid(
                            {
                                width: 930,
                                source: dataAdapter,
                                columns: [
                                    { text: 'Empresa No', datafield: 'iIdEmpresa', width: 50 },
                                    { text: 'Nombre de Empleado', datafield: 'sNomEmpleado', width: 300 },
                                    { text: 'Año ', datafield: 'ianio', width: 100 },
                                    { text: 'Tipo de Periodo', datafield: 'iTipoPeriodo', width: 100 },
                                    { text: 'Periodo', datafield: 'iPeriodo', width: 80 },
                                    { text: 'Email', datafield: 'sEmailSent', width: 200 },
                                    { text: 'Email enviado', datafield: 'sEmailSendSim', width: 100 },
                                ]
                            });
                    }
                    if (TablasDat.TablasDat[0].iRecibo == "2") {
                        var source =
                        {
                            localdata: TablasDat.LSelloSat,
                            datatype: "array",
                            datafields:
                                [
                                    { name: 'iIdEmpresa', type: 'int' },
                                    { name: 'sNomEmpleado', type: 'string' },
                                    { name: 'ianio', type: 'int' },
                                    { name: 'iTipoPeriodo', type: 'int' },
                                    { name: 'iPeriodo', type: 'int' },
                                    { name: 'bEmailSent', type: 'string' },
                                    { name: 'sEmailSent', type: 'string' },
                                ]
                        };
                        var dataAdapter = new $.jqx.dataAdapter(source);
                        $("#TbEjeSend").jqxGrid(
                            {
                                width: 930,
                                source: dataAdapter,
                                columns: [
                                    { text: 'Empresa No', datafield: 'iIdEmpresa', width: 50 },
                                    { text: 'Nombre de Empleado', datafield: 'sNomEmpleado', width: 300 },
                                    { text: 'Año ', datafield: 'ianio', width: 100 },
                                    { text: 'Tipo de Periodo', datafield: 'iTipoPeriodo', width: 100 },
                                    { text: 'Periodo', datafield: 'iPeriodo', width: 80 },
                                    { text: 'Email', datafield: 'sEmailSent', width: 200 },
                                    { text: 'Email enviado', datafield: 'bEmailSent', width: 100 },
                                ]
                            });
                    }
               
                };

            }
        });
     
    };
    //muestra las facturas con sellos elaboradas 
    btnVerEje.addEventListener('click', FtheLastEje);


    // envia correos 
    FSenEmail = () => {
        console.log('envia correo');
        if (btnEnviCorre.value == 1) {
            
            const dataSend = { Anio: TextBAnioProce.value, TipoPeriodo: DroTipoPeriodo.value, Perido: DropPerido.value, sIdEmpresas: Empresas, iRecibo: DroTipoRecibo.value, sDEscripcion: TextDEesp.value}
            console.log(dataSend);
            $.ajax({
                url: "../Empleados/EnvioEmail",
                type: "POST",
                data: dataSend,
                success: function (data) {
                    console.log(data);
                    if (data[0].sMensaje == "Succes") {
                        if (data[0].iNoEnviados < 2) {
                            fshowtypealert('Emision de Recibos', 'Se enviaro un correos electronicos', 'success');
                            if (DroTipoRecibo.value == "1") {
                               
                                    var source =
                                    {
                                        localdata: data,
                                        datatype: "array",
                                        datafields:
                                            [
                                                { name: 'iIdEmpresa', type: 'int' },
                                                { name: 'sNomEmpleado', type: 'string' },
                                                { name: 'ianio', type: 'int' },
                                                { name: 'iTipoPeriodo', type: 'int' },
                                                { name: 'iPeriodo', type: 'int' },
                                                { name: 'bEmailSent', type: 'string' },
                                                { name: 'sEmailSendSim', type: 'string' },
                                            ]
                                    };
                                    var dataAdapter = new $.jqx.dataAdapter(source);
                                    $("#TbEjeSend").jqxGrid(
                                        {
                                            width: 930,
                                            source: dataAdapter,
                                            columns: [
                                                { text: 'Empresa No', datafield: 'iIdEmpresa', width: 50 },
                                                { text: 'Nombre de Empleado', datafield: 'sNomEmpleado', width: 300 },
                                                { text: 'Año ', datafield: 'ianio', width: 100 },
                                                { text: 'Tipo de Periodo', datafield: 'iTipoPeriodo', width: 100 },
                                                { text: 'Periodo', datafield: 'iPeriodo', width: 80 },
                                                { text: 'Email', datafield: 'sEmailSent', width: 200 },
                                                { text: 'Email enviado', datafield: 'sEmailSendSim', width: 100 },
                                            ]
                                        });
                                }
                                if (DroTipoRecibo.value == "2") {
                                    console.log('op2')
                                    var source =
                                    {
                                        localdata: data,
                                        datatype: "array",
                                        datafields:
                                            [
                                                { name: 'iIdEmpresa', type: 'int' },
                                                { name: 'sNomEmpleado', type: 'string' },
                                                { name: 'ianio', type: 'int' },
                                                { name: 'iTipoPeriodo', type: 'int' },
                                                { name: 'iPeriodo', type: 'int' },
                                                { name: 'bEmailSent', type: 'string' },
                                                { name: 'sEmailSent', type: 'string' },
                                            ]
                                    };
                                    var dataAdapter = new $.jqx.dataAdapter(source);
                                    $("#TbEjeSend").jqxGrid(
                                        {
                                            width: 930,
                                            source: dataAdapter,
                                            columns: [
                                                { text: 'Empresa No', datafield: 'iIdEmpresa', width: 50 },
                                                { text: 'Nombre de Empleado', datafield: 'sNomEmpleado', width: 300 },
                                                { text: 'Año ', datafield: 'ianio', width: 100 },
                                                { text: 'Tipo de Periodo', datafield: 'iTipoPeriodo', width: 100 },
                                                { text: 'Periodo', datafield: 'iPeriodo', width: 80 },
                                                { text: 'Email', datafield: 'sEmailSent', width: 200 },
                                                { text: 'Email enviado', datafield: 'bEmailSent', width: 100 },
                                            ]
                                        });
                                }
                        }
                        else {

                        fshowtypealert('Emision de Recibos', 'Se enviaron ' + data[0].iNoEnviados + ' correos electronicos', 'success');

                       }
                      
                    }
                    else{
                        fshowtypealert('Emision de Recibos', 'Error al enviar los correos electronicos favor de contactar a sistemas', 'Error');

                    }

                }
            });
        }
        else {
            fshowtypealert('Emision de Recibos', 'Realizar una ejecucion o visualizar la ultima ejecucion para realizar el envio de correos', 'warning');
        }
       
    };

  //  BtnSenCorreo.addEventListener('click', FSenEmail)
      btnEnviCorre.addEventListener('click',FSenEmail)

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

