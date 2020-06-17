$(function () {

    // Declaracion de variables de la pantalla Definicionhd

    const Nombrede = document.getElementById('NombreDe');
    const Descripcionde = document.getElementById('DescripcionDe');
    const iAnode = document.getElementById('iAnoDe');
    const cande = document.getElementById('DCancelado');
  
    
             // declaracion de botones 

    const btnGuardarDefinicion = document.getElementById('btnGuardarDefinicion');
    const btnFloGuardar = document.getElementById('btnFloGuardar');
    const btnlimpDefinicion = document.getElementById('btnlimpDefinicion');
    const navDefiniciontab = document.getElementById('nav-Definicion-tab');
    const navPercepcionestab = document.getElementById('nav-Percepciones-tab');
    const navDeducciontab = document.getElementById('nav-Deduccion-tab');


    // Declaracion de variables de Definicion de pantalla precepciones

    var RegEmpresa = document.getElementById('RegEmpresa');
    var RegRenglon = document.getElementById('RegRenglon');
    var Tpoperiodo1 = document.getElementById('RegTipoperiodo1');
    var iRegEspejo = document.getElementById('RegEspejo');
    var iAcumulado = document.getElementById('RegAcumulado');
    //var iRegPeridoPer = document.getElementById('RegPeridoPer');
    var AnioPre;
    var cancelado;
    var IdMaxDefNom;

         // declaracion de botones y objetos  pantalla Percepcion

 
    const btnContPercepciones = document.getElementById('btnContPercepciones');
    const btnContDeducciones = document.getElementById('btnContDeducciones');
    const tableDpercepciones = document.getElementById('table-Dpercepciones');


    // Declaracion de variables de Definicion de pantalla  Deducciones

    var RegEmpresade = document.getElementById('RegEmpresaDe');
    var RegRenglonde = document.getElementById('RegRenglonDe');
    var Tpoperiodode = document.getElementById('RegTipoperiodoDe');
    const iRegEspejode = document.getElementById('RegEspejoDe');
    var iAcumuladode = document.getElementById('RegAcumuladoDe');
    //var iRegPeridoDe = document.getElementById('RegPeridoDe');
    var AnioDeduc;
    var canceladode;
    var IdMaxDefNomde;

    ///Botones  
    const btnPercepciones = document.getElementById('btnContPercepciones');
    const btnDeducciones = document.getElementById('btnContDeducciones');


    // funcion de control de botondes de direccionamiento para percepciones y deducione

    FdirPer = () => {
        $("#nav-Percepciones-tab").click();
    };
    btnContPercepciones.addEventListener('click', FdirPer);

    Fdirde = () => {
        $("#nav-Deduccion-tab").click();
    };
    btnContDeducciones.addEventListener('click', Fdirde);

    //funcion que  guarda los datos en la BD

    $('#btnGuardarDefinicion').on("click", function () {

      
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
                      
                        $("#NombreDe").attr("readonly", "readonly");
                        $("#DescripcionDe").attr("readonly", "readonly");
                        $("#iAnoDe").attr("readonly", "readonly");
                        $("#CanceladoDe").attr("readonly", "readonly");
                        btnContPercepciones.style.visibility = 'visible';
                        btnContDeducciones.style.visibility = 'visible';
                        $("#nav-Percepciones-tab").removeClass("active");
                        $("#nav-Deduccion-tab").removeClass("active");
                        btnGuardarDefinicion.value = "2";
                        btnGuardarDefinicion.style.visibility = 'hidden';
                        $("nav - Deduccion - tab").attr("value", "2");
                        $("btnGuardarDefinicion").attr("value", "2");
                        FllenaGrip();
                    } else {
                        fshowtypealert('Error', 'Contacte a sistemas', 'error');
                    }
                },
                error: function (jqXHR, exception) {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
            $.ajax({
                url: "../Nomina/IdmaxDefiniconNom",
                type: "POST",
                data: JSON.stringify(),
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    console.log(data);
                    for (i = 0; i < data.length; i++) {
                        IdMaxDefNomde = data[i].iIdDefinicionhd;                                   
                    }
                }
            });
        }
        else {
            console.log('mesaje de error');
            fshowtypealert('warning', 'Introduce todos los campos', 'warning');

        }
        

    });

    // avalida Año solo acepta numeros

    $("#iAnoDe").keyup(function () {
        this.value = (this.value + '').replace(/[^0-9]/g, '');
    });

       // funcion que  guarda los datos en la BD con boton flotante

    $('#btnFloGuardar').on("click", function () {

       
        if (btnFloGuardar.value == "1" && btnGuardarDefinicion.value == "1") {
          
            if (Nombrede.value != "" && Nombrede.value != " " && Descripcionde.value != "" && Descripcionde.value != " " && iAnode.value != "" && iAnode.value != " ") {
          
                const dataSend = {
                    sNombreDefinicion: Nombrede.value, sDescripcion: Descripcionde.value,
                    iAno: iAnode.value, iCancelado: cande.value
                };
                console.log(dataSend);
                $.ajax({
                    url: "../Nomina/DefiNomina",
                    type: "POST",
                    data: dataSend,
                    success: function (data) {
                        if (data.sMensaje == "success") {
                            fshowtypealert('Registro correcto!', 'Definicion plantilla guardada', 'success');                        
                            $("#NombreDe").attr("readonly", "readonly");
                            $("#DescripcionDe").attr("readonly", "readonly");
                            $("#iAnoDe").attr("readonly", "readonly");
                            $("#DCancelado").attr("readonly", "readonly");
                            btnContPercepciones.style.visibility = 'visible';
                            btnContDeducciones.style.visibility = 'visible';
                            $("#nav-Percepciones-tab").removeClass("active");
                            $("#nav-Deduccion-tab").removeClass("active");
                            btnGuardarDefinicion.value = "2";
                            btnGuardarDefinicion.style.visibility = 'hidden'; 
                            $("nav - Deduccion - tab").attr("value", "2");

                           
                        } else {
                            fshowtypealert('Error', 'Contacte a sistemas', 'error');
                        }
                    },
                    error: function (jqXHR, exception) {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
                $.ajax({
                    url: "../Nomina/IdmaxDefiniconNom",
                    type: "POST",
                    data: JSON.stringify(),
                    contentType: "application/json; charset=utf-8",
                    success: (data) => {
                        console.log(data);
                        for (i = 0; i < data.length; i++) {
                            IdMaxDefNomde = data[i].iIdDefinicionhd;         
                        }
                    }
                });
            }
             else {
               
                fshowtypealert('warning', 'Introduce todos los campos', 'warning');

            }
            
        }

        if (btnFloGuardar.value == "2" && btnGuardarDefinicion.value == "2") {   
            FSavePres();
            FllenaGripPer();
        }

        if (btnFloGuardar.value == "3" && btnGuardarDefinicion.value == "2") {
           
            FSavededu();
            FllenaGripDed();
        }

       

    });

    /////// funcion que limpia campos de definicion

    FLimpiaCampos = () => {

        console.log('valor de boton:'+  btnlimpDefinicion.value);
        if (btnlimpDefinicion.value == "1") {
            console.log('limpia campos');
                //Nombrede.value = '';
                //Descripcionde.value = '';
                //iAnode.value = '';
                //cande.value = '';
            $("#nav-Percepciones-tab").removeClass("active");
            $("#nav-Deduccion-tab").removeClass("active");
            btnGuardarDefinicion.style.visibility = 'hidden';
            btnGuardarDefinicion.value = "2";
            $.ajax({
                url: "../Nomina/IdmaxDefiniconNom",
                type: "POST",
                data: JSON.stringify(),
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    console.log(data);
                    for (i = 0; i < data.length; i++) {
                        IdMaxDefNomde = data[i].iIdDefinicionhd;
                    }
                }
            });



            }

            if (btnlimpDefinicion.value == "2") {

                RegEmpresa.value = "0";
                RegRenglon.value = "0";
                RegTipoperiodo1.value ="0";
                RegEspejo.value = "0";
                RegAcumulado.value = "0";
                //RegPeridoPer.value = "0";
           
            }

            if (btnlimpDefinicion.value == "3") {

                RegEmpresade.value = "0";
                RegRenglonde.value = "0";
                Tpoperiodode.value = "0";
                iRegEspejode.value = "0";
                iAcumuladode.value = "0";
                //iRegPeridoDe.value = "0";
           
            }

            //$("#nav-Percepciones-tab").removeClass("active");
            //$("#nav-Deduccion-tab").removeClass("active");
            //btnGuardarDefinicion.style.visibility = 'hidden';
            //btnGuardarDefinicion.value = "2";
            
        };

    btnlimpDefinicion.addEventListener('click', FLimpiaCampos);


   // asigna los botones flotantes a la pantlla del hd

    FAsignaBotonesDh = () => {

        $("#btnFloGuardar").attr("value", "1");
        console.log(btnFloGuardar.value);
        $("#btnlimpDefinicion").attr("value", "1");


    };

    navDefiniciontab.addEventListener('click', FAsignaBotonesDh);

     // asigna los botones flotantes a la pantlla del Precepcion

    FAsignaBotonesDPre = () => {

        $("#btnFloGuardar").attr("value", "2");
        console.log(btnFloGuardar.value)
        $("#btnlimpDefinicion").attr("value", "2");
    };

    navPercepcionestab.addEventListener('click', FAsignaBotonesDPre);

     // asigna los botones flotantes a la pantlla del Deduccion

    FAsignaBotonesDe = () => {

        $("#btnFloGuardar").attr("value", "3");
        $("#btnlimpDefinicion").attr("value", "3");
        console.log(btnFloGuardar.value);

    };

    navDeducciontab.addEventListener('click', FAsignaBotonesDe);


         /////// pantalla Percepcion


    // fincion de llenado el droplist de empresa

    LisEmpresa = () => {

        $.ajax({
            url: "../Nomina/LisEmpresas",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
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

                console.log(data);
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegTipoperiodo1").innerHTML += `<option value='${data[i].iId}'>${data[i].sValor}</option>`;

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
        var opvalEmpresa = RegEmpresa.value;
        const dataSend = { IdEmpresa: opvalEmpresa, iElemntoNOm: 1 };
        $("#RegRenglon").empty();
        $('#RegRenglon').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisRenglon",
            type: "POST",
            data: dataSend,
            success: (data) => {
                console.log(data);
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegRenglon").innerHTML += `<option value='${data[i].iIdRenglon}'>${data[i].sNombreRenglon}</option>`;
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
        const opRenglonTex = RegRenglon.value;
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
    };

    $('#RegRenglon').change(function () {
        
        RecargaLisAcumulado();
   
    });
    $('#Tpoperiodo1').change(function () {

    });

      //// Guarda persepciones 
    FSavePres = () => {

        var op = RegTipoperiodo1.options[RegTipoperiodo1.selectedIndex].text;
    
        if (RegEmpresa.value != "0" && RegRenglon.value != "0" && iRegEspejo.value != "0" && op != "Selecciona") {

               console.log("numero de definicion: " + IdMaxDefNomde);               
                    var ispejo;                 
                    if (iRegEspejo.value == "1") {
                        ispejo = "1";
                    }
                    else if (iRegEspejo.value == "2") {
                        ispejo = "0";
                    }
                    const dataSend = { iIdFinicion: IdMaxDefNomde };
                     
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
                            const idRenglon = RegRenglon.value;
                            const icance = opcanel1;
                            const iEleNom = '39';
                            const idAcumulado = iAcumulado.value;

                            const dataSend2 = { 

                                iIdDefinicionHd: IdMaxDefNomde, iIdEmpresa: idempresa,
                                iTipodeperiodo: idTipoPeriodo, /*iIdperiodo: idPeriodo,*/
                                iRenglon: idRenglon, iCancelado: icance, iElementonomina: iEleNom,
                                iEsespejo: ispejo, iIdAcumulado: idAcumulado
                            };

                            const dataSend3 = {
                                iIdDefinicionHd: IdMaxDefNomde, iIdEmpresa: idempresa,
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
                                                  fshowtypealert('Registro correcto!', 'Definicion plantilla guardada', 'success');

                                                 } else {
                                                    fshowtypealert('Error', 'Contacte a sistemas', 'error');
                                                 }
                                               },
                                            error: function (jqXHR, exception) {
                                                 fcaptureaerrorsajax(jqXHR, exception);
                                               }
                                          });
                                    }
                                    if (data[0].iIdDefinicionHd == 1) {
                                        fshowtypealert("Nomina/Percepción", "La percepcion ya esta ingresada favor de ingresar otra percepción diferente","warning")
                                    }

                                    
                                },
                                error: function (jqXHR, exception) {
                                    fcaptureaerrorsajax(jqXHR, exception);
                                }
                            });                

                        },
                        error: function (jqXHR, exception) {
                            //fcaptureaerrorsajax(jqXHR, exception);
                        }
                        
                    });            

        }

        else {
            console.log('mesaje de error');
            fshowtypealert('warning', 'Los campos: Empresa, Renglo, Tipo de periodo y Es espejo son obligatorios', 'warning');
        }
    };

    FactivaGuaryLlegrippre = () => {
        FSavePres();
        FllenaGripPer();
    };
    
    btnGuardarPercepcion.addEventListener('click', FactivaGuaryLlegrippre);



     // Funcion carga datos en tabla

    FllenaGripPer = () => {
           //const tabledep = document.getElementById('data-body-dpercepciones');       
          const dataSend = { iIdDefinicionln: IdMaxDefNomde };  
          $.ajax({
                    url: "../Nomina/listdatosPercesiones",
                    type: "POST",
                    data: dataSend,
                    success: (data) => {
                        var source =
                        {
                            localdata: data,
                            datatype: "array",
                            datafields:
                                [
                                    { name: 'IdEmpresa', type: 'string' },
                                    { name: 'iRenglon', type: 'string' },
                                    { name: 'iTipodeperiodo', type: 'string' },
                                    { name: 'iIdAcumulado', type: 'string' },
                                    { name: 'iEsespejo', type: 'string' }
                                ]
                        };

                        var dataAdapter = new $.jqx.dataAdapter(source);

                        $("#jqxgrid").jqxGrid(
                            {
                                width: 980,

                                source: dataAdapter,
                                columnsresize: true,
                                columns: [
                                    { text: 'Empresa', datafield: 'IdEmpresa', width: 200 },
                                    { text: 'Renglon', datafield: 'iRenglon', width: 300 },
                                    { text: 'Tipo de periodo', datafield: 'iTipodeperiodo', whidth: 30 },
                                    { text: 'Acumulado', datafield: 'iIdAcumulado', whidt: 400 },
                                    { text: 'Esespejo', datafield: 'iEsespejo', whidt: 30 }
                                ]
                            });
                    }
                });
    };

                            // Pantalla Deduccion

    // llena el drop lis de Empresa de Deduccion
    LisEmpresaDe = () => {
        $.ajax({
            url: "../Nomina/LisEmpresas",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
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

                console.log(data);
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
                for (i = 0; i < data.length; i++) {
                    document.getElementById("RegRenglonDe").innerHTML += `<option value='${data[i].iIdRenglon}'>${data[i].sNombreRenglon}</option>`;
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
        const opRenglonTex = RegRenglonDe.value;
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

    };

    $('#RegRenglonDe').change(function () {

        RecargaLisAcumuladoDe();

    });

      // funcion de Guadar Deducciones


    // Funcion Guarda refistros en la base de datos de la pantalla Deducciones
    FSavededu = () => {
        var op = RegTipoperiodoDe.options[RegTipoperiodoDe.selectedIndex].text;
        if (RegEmpresade.value != "0" && RegRenglonde.value != "0" && iRegEspejode.value != "0" && op != "Selecciona") {
            console.log('deducion : ' + IdMaxDefNomde);
            var ispejode;
            if (iRegEspejode.value == "1") {
                ispejode = "1";
            }
            else if (iRegEspejode.value == "2") {
                ispejode = "0";
            }
            const dataSend = { iIdFinicion: IdMaxDefNomde };

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

                   
                    const idempresade = RegEmpresade.value;
                    const idTipoPeriodode = Tpoperiodode.value;
                    //const idPeriodode = op2;
                    const idRenglonde = RegRenglonde.value;
                    const icancede = opcanel;
                    const iEleNomde = '40';
                    const idAcumuladode = iAcumuladode.value;
        

                    const dataSend2 = {
                        iIdDefinicionHd: IdMaxDefNomde, iIdEmpresa: idempresade,
                        iTipodeperiodo: idTipoPeriodode, /*iIdperiodo: idPeriodode,*/
                        iRenglon: idRenglonde, iCancelado: icancede, iElementonomina: iEleNomde,
                        iEsespejo: ispejode, iIdAcumulado: idAcumuladode

                    };

                    const dataSend3 = {
                        iIdDefinicionHd: IdMaxDefNomde, iIdEmpresa: idempresade,
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
                                            fshowtypealert('Registro correcto!', 'Definicion plantilla guardada', 'success');

                                        } else {
                                            fshowtypealert('Error', 'Contacte a sistemas', 'error');
                                        }
                                    },
                                    error: function (jqXHR, exception) {
                                        fcaptureaerrorsajax(jqXHR, exception);
                                    }
                                });

                            }
                            if (data[0].iIdDefinicionHd == 1) {
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
            fshowtypealert('warning', 'Los campos: Empresa, Renglo, Tipo de periodo y Es espejo son obligatorios', 'warning');

        }
    };

    FactivaGuaryLlegrip = () => {
        FSavededu();
        FllenaGripDed();
    };

    btnRegistrodeduccion.addEventListener('click', FactivaGuaryLlegrip);

    FllenaGripDed = () => {
        //const tabledep = document.getElementById('data-body-dpercepciones');
        console.log(IdMaxDefNomde);
        const dataSend = { iIdDefinicionln: IdMaxDefNomde };
        console.log(dataSend);
         $.ajax({
                    url: "../Nomina/listdatosDeducuiones",
                    type: "POST",
                    data: dataSend,
                    success: (data) => {
                        console.log('info de Base');
                        console.log(data);
                        var source =
                        {
                            localdata: data,
                            datatype: "array",
                            datafields:
                                [
                                    { name: 'IdEmpresa', type: 'string' },
                                    { name: 'iRenglon', type: 'string' },
                                    { name: 'iTipodeperiodo', type: 'string' },
                                    { name: 'iIdAcumulado', type: 'string' },
                                    { name: 'iEsespejo', type: 'string' }
                                ]
                        };

                        var dataAdapter = new $.jqx.dataAdapter(source);

                        $("#jqxgrid2").jqxGrid(
                            {
                                width: 980,

                                source: dataAdapter,
                                columnsresize: true,
                                columns: [
                                    { text: 'Empresa', datafield: 'IdEmpresa', width: 200 },
                                    { text: 'Renglon', datafield: 'iRenglon', width: 300 },
                                    { text: 'Tipo de periodo', datafield: 'iTipodeperiodo', whidth: 30 },
                                    { text: 'Acumulado', datafield: 'iIdAcumulado', whidt: 400 },
                                    { text: 'Esespejo', datafield: 'iEsespejo', whidt: 30 }
                                ]
                            });

                    }
                });
    };

    // validaciones

    $("#iAnoDe").keyup(function () {
        this.value = (this.value + '').replace(/[^0-9]/g, '');
    });

    $("#CanceladoDe").keyup(function () {
        this.value = (this.value + '').replace(/[^SI,NO,si,no,Si,No]/g, '');
    });

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


