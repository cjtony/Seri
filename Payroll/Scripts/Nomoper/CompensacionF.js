$(function () {

    const btnNuevaComp = document.getElementById('btnNuevaComp');
    const DropEmpresa = document.getElementById('DropEmpresa');
    const DropPuesto = document.getElementById('DropPuesto');
    const DropRenglon = document.getElementById('DropRenglon');
    const ChekPYA = document.getElementById('ChekPYA');
    const TxtImporte = document.getElementById('TxtImporte');
    const TxtDesp = document.getElementById('TxtDesp');
    const btnRegistrar = document.getElementById('btnRegistrar');


    var valorCheckpya = document.getElementById('ChekPYA')

    /// carga tabla de compesacion fija 
    FTbCompensacion = () => {
        console.log('tabla');
        $.ajax({
            url: "../Nomina/CompFijasEmple",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                document.getElementById('content-tableComp').classList.remove("d-none");
                if (data[0].sMensaje == "success") {
                    console.log('muestra tabla');
                    var source =
                    {
                        localdata: data,
                        datatype: "array",
                        datafields:
                            [
                                { name: 'iId', type: 'int' },
                                { name: 'sNombreEmpresa', type: 'string' },
                                { name: 'iIdPuesto', type: 'int' },
                                { name: 'iPremioPyA', type: 'int' },
                                { name: 'sNombreRenglon', type: 'string' },
                                { name: 'iImporte', type: 'int' },
                                { name: 'sDescripcion', type: 'string' },

                            ]
                    };
                    var dataAdapter = new $.jqx.dataAdapter(source);
                    $("#TBCompensacion").jqxGrid(
                      {
                            width: 700,
                            source: dataAdapter,
                            autoheight: true,
                            pageable: true,
                            altRows: true,
                            filterable: true,
                            pagerButtonsCount: 10,
                            columnsResize: true,
                            columns: [
                                { text: 'No. Compensación', datafield: 'iId', width: 100 },
                                { text: 'Nombre de empresa', datafield: 'sNombreEmpresa', width: 100 },
                                { text: 'Premio de PyA', datafield: 'iPremioPyA', width: 100 },
                                { text: 'Puesto', datafield: 'iIdPuesto', width: 100 },
                                { text: 'Nombre de renglón', datafield: 'sNombreRenglon', width: 100 },
                                { text: 'iImporte', dataield: 'iImporte', width: 100 },
                                { text: 'Descripción', datafield: 'sDescripcion', whidt: 700}

                            ]
                        });
                }
                if (data[0].sMensaje == "error") {
                    document.getElementById('content-tableComp').classList.add("d-none");
                }
                
            }
        });

    };
    FTbCompensacion();
         
    // visualiza el bloque de insert
    FBlockInsert = () => {
        document.getElementById('content-blockInsert').classList.remove("d-none");
        FListadoEmpresa();
    };

    FListadoEmpresa = () => {
        $("#DropEmpresa").empty();
        $('#DropEmpresa').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisEmpresas",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DropEmpresa").innerHTML += `<option value='${data[i].iIdEmpresa}'>${data[i].sNombreEmpresa}</option>`;

                }
            }
        });
    };

    $('#DropEmpresa').change(function () {
        FListPuesto();
        FLisRenglon();
    });

    FListPuesto = () => {
        console.log('activa');
        const dataSend = { iIdEmpresa: DropEmpresa.value };
        $("#DropPuesto").empty();
        $('#DropPuesto').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisPuestosEmpresa",
            type: "POST",
            data: dataSend,
            success: (data) => {
                console.log('entra');
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DropPuesto").innerHTML += `<option value='${data[i].iIdPuesto}'>${data[i].sNombrePuesto}</option>`;
                }
            },        
        });

    };

    FLisRenglon = () => {

        const dataSend = { IdEmpresa: DropEmpresa.value, iElemntoNOm: 0 };
        $("#DropRenglon").empty();
        $('#DropRenglon').append('<option value="0" selected="selected">Selecciona</option>');
        $.ajax({
            url: "../Nomina/LisRenglon",
            type: "POST",
            data: dataSend,
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DropRenglon").innerHTML += `<option value='${data[i].iIdRenglon}'>${data[i].sNombreRenglon}</option>`;
                }
            },
            error: function (jqXHR, exception) {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

    };

    btnNuevaComp.addEventListener('click', FBlockInsert);

    // inserta Compensancion 
    FNewCompensacion = () => {

        var dataSendComp = { iIdempresa: DropEmpresa.value, iPyA: 0, iIdpuesto: DropPuesto.value, iIdRenglon: DropRenglon.value, iImporte: TxtImporte.value, sDescripcion: TxtDesp.value };

        if (valorCheckpya.checked == true) {
            dataSendComp = { iIdempresa: DropEmpresa.value, iPyA: 1, iIdpuesto: DropPuesto.value, iIdRenglon: 0, iImporte: 0, sDescripcion: TxtDesp.value };
        }
        if (valorCheckpya.checked == false) {

            dataSendComp = { iIdempresa: DropEmpresa.value, iPyA: 0, iIdpuesto: DropPuesto.value, iIdRenglon: DropRenglon.value, iImporte: TxtImporte.value, sDescripcion: TxtDesp.value };
        }
    
        $.ajax({
            url: "../Nomina/NewCompFija",
            type: "POST",
            data: dataSendComp,
            success: (data) => {
                if (data.sMensaje == "success") {
                    fshowtypealert('Compensación fija!', 'Guardada', 'success');
                }
                else {
                    fshowtypealert('Error', 'Contacte a sistemas', 'error');
                }
            },
        });
    };

    btnRegistrar.addEventListener('click', FNewCompensacion);

    /// Premio y asistencia 

    FClicpya = () => {
        if (valorCheckpya.checked == true) {
            $("#DropRenglon").attr('readonly', true).trigger('chosen:updated'); 
            $("#TxtImporte").attr('readonly', true).trigger('chosen:updated'); 
        }
        if (valorCheckpya.checked == false) {
            $("#DropRenglon").attr('readonly', false).trigger('chosen:updated'); 
            $("#TxtImporte").attr('readonly', false).trigger('chosen:updated'); 
        }

    }

    ChekPYA.addEventListener('click',FClicpya)

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
