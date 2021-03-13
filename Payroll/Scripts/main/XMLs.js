
$(function () {

    //////////////////////////////////// Recibos Nomina ///////////////////////////////

    /// declaracion de variables globales

    const EmpresaNom = document.getElementById('EmpresaNom');
    const anoNom = document.getElementById('anoNom');
    const TipodePerdioRec = document.getElementById('TipodePerdioRec');
    const PeridoNom = document.getElementById('PeridoNom');
    const CheckXEmpleado = document.getElementById('CheckXEmpleado');
    const dropbusemple = document.getElementById('dropbusemple');
    

   
    const btnFloLimpiar = document.getElementById('btnFloLimpiar');
    const btnPDFms = document.getElementById('btnPDFms');
    const btnXmlms = document.getElementById('btnXmlms');

    const btnDowlan = document.getElementById('btnDowlan');

    var valorCheckxEmpleado = document.getElementById('CheckXEmpleado');
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
                type: "POST",
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
                    if (data[i].sNominaCerrada == "True") {
                        document.getElementById("PeridoNom").innerHTML += `<option value='${data[i].iId}'>${data[i].iPeriodo} Fecha del: ${data[i].sFechaInicio} al ${data[i].sFechaFinal}</option>`;
                    }
                    
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


    /// clic en el check xempleado

    FValorChecXemple = () => {
        console.log('valor de check' + valorCheckxEmpleado.checked)
        if (valorCheckxEmpleado.checked == true) {
            dropbusemple.style.visibility = 'visible';
        } 

        if (valorCheckxEmpleado.checked == false) {
            dropbusemple.style.visibility = 'hidden';
        } 
    };

    CheckXEmpleado.addEventListener('click', FValorChecXemple);


    /// descarga masiva de xml

    FdowXmlsMasivo = () => {
        console.log('entro a xml')
        if (valorCheckxEmpleado.checked == false) { 
            console.log('entro general');
            btnXmlms.value = 1;
            btnPDFms.value = 0;
            IdEmpresa = EmpresaNom.value;
            var nom = $('#jqxInput').jqxInput('val');
            NombreEmpleado = nom.label;
            IdEmpresa = EmpresaNom.value;
            anio = anoNom.value;
            Tipoperiodo = TipodePerdioRec.value;
            datosPeriodo = PeridoNom.value;

            const dataSend = { IdEmpresa: IdEmpresa, sNombreComple: 0, Periodo: datosPeriodo, anios: anio, Tipodeperido: Tipoperiodo, Masivo: 1 };
            $.ajax({
                url: "../Empleados/XMLNomina",
                type: "POST",
                data: dataSend,
                beforeSend: function (data) {
                    $('#jqxLoader2').jqxLoader('open');
                },
                success: function (data) {
                    if (data[0].sMensaje == "Error") {
                        $('#jqxLoader2').jqxLoader('close');
                        fshowtypealert('Error', 'El periodo no tiene un empleado con los calculos de nomina', 'error');
                    }
                    if (data[0].sMensaje != "Error") {
                    
                    if (data[0].sMensaje != "NorCert") {
                        btnDowlan.style.visibility = 'visible';
                        $('#jqxLoader2').jqxLoader('close');
                        fshowtypealert('Recibos de nomina', 'sean generado los archivos XML correctamente', 'success');
                    }
                    if (data[0].sMensaje == "NorCert") {
                        $('#jqxLoader2').jqxLoader('close');
                        fshowtypealert('Error', 'Contacte a sistemas falta el archivo certificado', 'error');
                    }
                    if (data.rowscount < 0 || data == null) {
                        $('#jqxLoader2').jqxLoader('close');
                        fshowtypealert('Error', 'Contacte a sistemas', 'error');
                    }
                    }
                }
            });


     
        }
        if (valorCheckxEmpleado.checked == true) {
            console.log('entro por uno');
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
        }

    };
    btnXmlms.addEventListener('click', FdowXmlsMasivo)

    /// descarga masiva de PDF

    //FdowPDFMasivos = () => {

    //    if (valorCheckxEmpleado.checked == false) {
    //        btnXmlms.value = 0;
    //        btnPDFms.value = 1;
    //        console.log('prueba');
    //        var oprefis = 0;

    //        IdEmpresa = EmpresaNom.value;
    //        var nom = $('#jqxInput').jqxInput('val');
    //        NombreEmpleado = nom.label;
    //        IdEmpresa = EmpresaNom.value;
    //        anio = anoNom.value;
    //        Tipoperiodo = TipodePerdioRec.value;
    //        datosPeriodo = PeridoNom.value;
    //        const dataSend = { IdEmpresa: IdEmpresa, sNombreComple: 0, Periodo: datosPeriodo, anios: anio, Tipodeperido: Tipoperiodo, iRecibo: 1 };
    //        $.ajax({
    //            url: "../Empleados/GPDFMasivos",
    //            type: "POST",
    //            data: dataSend,
    //            beforeSend: function (data) {
    //                $('#jqxLoader').jqxLoader('open');
    //            },
    //            success: function (data) {
    //                if (data[0].sMensaje != "NorCert") {
    //                    btnDowlan.style.visibility = 'visible';
    //                    btnDowlan.value = data[0].sUrl;
    //                    $('#jqxLoader').jqxLoader('close');
    //                    fshowtypealert('Recibos de nomina', 'sean generado el PDF correctamente', 'success');

    //                }
    //                else {

    //                    $('#jqxLoader').jqxLoader('close');
    //                    fshowtypealert('Error', 'Contacte a sistemas', 'error');
    //                }

    //            }
    //        });

    //    }
    //    if (valorCheckxEmpleado.checked == true) {


    //    }

    //};

    //btnPDFms.addEventListener('click', FdowPDFMasivos)

    FOpenFile = () => {
        if (btnPDFms.value == 1) {
            var periodo = PeridoNom.options[PeridoNom.selectedIndex].text;
            separador = " ",
                imite = 2,
                arreglosubcadena = periodo.split(separador, limite);

            var nombre = "RecibosNom_E" + EmpresaNom.value + "_P" + arreglosubcadena[0] + ".pdf";
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