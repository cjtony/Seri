$(function () {

    /// declaracion de varioables

    const DropEmpresa = document.getElementById('DropEmpresa');
    const TxtTurno = document.getElementById('TxtTurno');
    const Descripcionhr = document.getElementById('Descripcionhr');
    const DropCheckEmple = document.getElementById('DropCheckEmple');
    const TxtHrEnt = document.getElementById('TxtHrEnt');
    const TxtHrSal = document.getElementById('TxtHrSal');
    const DropChekComida = document.getElementById('DropChekComida');
    const TxtHrEntPau = document.getElementById('TxtHrEntPau');
    const TxtHrSalPau = document.getElementById('TxtHrSalPau');
    const DropChekTur = document.getElementById('DropChekTur');
    const DropChekPau = document.getElementById('DropChekPau');
    const DropDiasDesc = document.getElementById('DropDiasDesc');

    // pantalla Act

    const DropEmpAct = document.getElementById('DropEmpAct');
    const TxtTurnoAct = document.getElementById('TxtTurnoAct');
    const DescripcionhrAct = document.getElementById('DescripcionhrAct');
    const DropCheckEmpleAct = document.getElementById('DropCheckEmpleAct');
    const TxtHrEntAct = document.getElementById('TxtHrEntAct');
    const TxtHrSalAct = document.getElementById('TxtHrSalAct');
    const DropChekComAct = document.getElementById('DropChekComAct');
    const TxtHrEntPauAct = document.getElementById('TxtHrEntPauAct');
    const TxtHrSalPauAct = document.getElementById('TxtHrSalPauAct');
    const DropChekTurAct = document.getElementById('DropChekTurAct');
    const DropChekPauAct = document.getElementById('DropChekPauAct');
    const DropDiasDescAct = document.getElementById('DropDiasDescAct');


    const btninserHr = document.getElementById('btninserHr');
    const btnActHr = document.getElementById('btnActHr');


    FListadoEmpresa = () => {
        $("#DropEmpresa").empty();
        $('#DropEmpresa').append('<option value="0" selected="selected">Selecciona</option>');
        $("#DropEmpAct").empty();
        $('#DropEmpAct').append('<option value="0" selected="selected">Selecciona</option>');     
        $.ajax({
            url: "../Nomina/LisEmpresas",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DropEmpresa").innerHTML += `<option value='${data[i].iIdEmpresa}'>${data[i].sNombreEmpresa}</option>`;
                    document.getElementById("DropEmpAct").innerHTML += `<option value='${data[i].iIdEmpresa}'>${data[i].sNombreEmpresa}</option>`;
                }
            }
        });
    };
    FListadoEmpresa();

    FInsertHorario = () => {
       
      
        if (DropEmpresa.value != 0 && TxtTurno.value != " " && TxtTurno.value != "" && Descripcionhr.value != " " && Descripcionhr.value != "" + DropCheckEmple.value != 0
            && TxtHrEnt.value != " " && TxtHrEnt.value != "" && TxtHrSal.value != " " && TxtHrSal.value != "" && TxtHrEntPau.value != " " && TxtHrEntPau.value != "" && TxtHrSalPau.value != " "
            && TxtHrSalPau.value != "" && DropChekTur.value != 0 + DropChekPau.value != 0 && DropDiasDesc.value != 0 && DropChekComida.value !=0) {
            const DataInser = {
                IdEmpresa: DropEmpresa.value, turno: TxtTurno.value, sDescripcion: Descripcionhr.value, sHoraEntrada: TxtHrEnt.value, shoraSalida: TxtHrSal.value, sHrEntradaPa: TxtHrEntPau.value, sHrSalidaPa: TxtHrSalPau.value, iTipoTurnocheck: DropCheckEmple.value
                , iTipoPausacheck: DropChekComida.value, iDiasDes: DropDiasDesc.value, iCancelado: 0, iTipoTurno: DropChekTur.value, iTipoPausa: DropChekPau.value
            }

            $.ajax({
                url: "../RH/InsertHrsEmpresa",
                type: "POST",
                data: DataInser,
                success: (data) => {
                    if (data[0].sMensaje == "success") {

                        fshowtypealert('Horario', 'Guardado correctamente', 'success');
                    }

                    if (data[0].sMensaje == "error") {

                        fshowtypealert('Error', 'Contacte a sistemas', 'error');
                    }
                
                },
            });


        }
        else {

            fshowtypealert('Horario', 'ingresar todos los campos', 'warning');
        }

    };

    btninserHr.addEventListener('click', FInsertHorario);

    DTBHorarios = () => {

        $("#dTabHrDia").jqxGrid('clear')
        $.ajax({
            url: "../RH/RetrieveHorarios",
            type: "POST",
            data: JSON.stringify(),
            success: (data) => {
                console.log(data);
                if (data[0].sMensaje == "success") {
                   
                    var source =
                    {
                        localdata: data,
                        datatype: "array",
                        datafields:
                            [
                                { name: 'iIdHorario', type: 'int' },
                                { name: 'sNombreEmpresa', type: 'string' },
                                { name: 'iTurno', type: 'int' },
                                { name: 'sDescrip', type: 'string' },
                                { name: 'sHrEnt', type: 'string' },
                                { name: 'sHrSal', type: 'string' },
                                { name: 'sHrEntCom', type: 'string' },
                                { name: 'sHrSalCom', type: 'string' },
                                { name: 'iTipCheckNorm', type: 'int' },
                                { name: 'iTipCheckPausa', type: 'int' },
                                { name: 'iDiasDesc', type: 'int' },
                                { name: 'iCancelado', type: 'int' },
                                { name: 'iUsuario', type: 'int' },
                                { name: 'iTipoTurno', type: 'int' },
                                { name: 'iTipoPausa', type: 'int' }

                            ]
                    };
                    var dataAdapter = new $.jqx.dataAdapter(source);
                    $("#dTabHrDia").jqxGrid(
                        {
                            
                            width: 750,
                            source: dataAdapter,
                            columnsresize: true,
                            columns: [
                                { text: 'Empresa', datafield: 'sNombreEmpresa', width: 100 },
                                { text: 'Descripcion', datafield: 'sDescrip', width: 70 },
                                { text: 'No. Turno', datafield: 'iTurno', width: 60 },
                                { text: 'Hora Entrada ', datafield: 'sHrEnt', width: 80 },
                                { text: 'Hora Salida', datafield: 'sHrSal', width: 80 },
                                { text: 'Hora de Salida Pausa', datafield: 'sHrSalCom', width: 130 },
                                { text: 'Hora de Entrada pausa', datafield: 'sHrEntCom', width: 135 },
                                { text: 'Edit', datafield: 'Edit', columntype: 'button', width: 50, cellsrenderer: function () {
                                        return "Edit"; 
                                    }, buttonclick: function (row) {
                                        // open the popup window when the user clicks a button.
                                        editrow = row;
                                        var offset = $("#dTabHrDia").offset();
                                        var dataRecord = $("#dTabHrDia").jqxGrid('getrowdata', editrow);
                                        $("#btnFloActu").click();

                                        for (var i = 0; i < DropEmpAct.length; i++) {
                                            if (DropEmpAct.options[i].text == dataRecord.sNombreEmpresa) {
                                                // seleccionamos el valor que coincide
                                                DropEmpAct.selectedIndex = i;
                                            }

                                        }
                                        TxtTurnoAct.value = dataRecord.iTurno;
                                        DescripcionhrAct.value = dataRecord.sDescrip;
                                        DropCheckEmpleAct.selectedIndex = dataRecord.iTipCheckNorm;
                                        TxtHrEntAct.value = dataRecord.sHrEnt;
                                        TxtHrSalAct.value = dataRecord.sHrSal;                
                                        DropChekComAct.selectedIndex = dataRecord.iTipCheckPausa;
                                        TxtHrSalPauAct.value = dataRecord.sHrSalCom;
                                        TxtHrEntPauAct.value = dataRecord.sHrEntCom;
                                        DropChekTurAct.selectedIndex = dataRecord.iTipoTurno;
                                        DropChekPauAct.selectedIndex = dataRecord.iTipoPausa;
                                        DropDiasDescAct.selectedIndex = dataRecord.iDiasDesc;
                 
                                    }
                                },
                                {
                                    text: 'Eliminar', datafield: 'Delet', columntype: 'button', width: 50, cellsrenderer: function () {
                                        return "Delet";
                                    }, buttonclick: function (row) {
                                        // open the popup window when the user clicks a button.
                                        deletrow = row;
                                        var offset = $("#dTabHrDia").offset();
                                        var dataRecord2 = $("#dTabHrDia").jqxGrid('getrowdata', deletrow);              
                                        console.log('Eliminar registro'+dataRecord2.iIdHorario);
                                        FDelet();

                                    }
                                },
                               ]
                        });
                }
                else {
                    fshowtypealert('Error', 'Contacte a sistemas ', 'error');
                }
            }
        });
    }
    DTBHorarios();


    FActualiza = () => {
        var dataRecord = $("#dTabHrDia").jqxGrid('getrowdata', editrow);
        var idHr = dataRecord.iIdHorario
       
        if (TxtTurnoAct.value != dataRecord.iTurno || DescripcionhrAct.value != dataRecord.sDescrip
            || DropCheckEmpleAct.selectedIndex != dataRecord.iTipCheckNorm
            || TxtHrEntAct.value != dataRecord.sHrEnt
            || TxtHrSalAct.value != dataRecord.sHrSal
            || DropChekComAct.selectedIndex != dataRecord.iTipCheckPausa
            || TxtHrSalPauAct.value != dataRecord.sHrSalCom
            || TxtHrEntPauAct.value != dataRecord.sHrEntCom
            || DropChekTurAct.selectedIndex != dataRecord.iTipoTurno
            || DropChekPauAct.selectedIndex != dataRecord.iTipoPausa
            || DropDiasDescAct.selectedIndex != dataRecord.iDiasDesc) {

            const DataInser = {
                HrId: dataRecord.iIdHorario, turno: TxtTurnoAct.value, sDescripcion: DescripcionhrAct.value, sHoraEntrada: TxtHrEntAct.value, shoraSalida: TxtHrSalAct.value, sHrEntradaPa: TxtHrSalPauAct.value, sHrSalidaPa: TxtHrEntPauAct.value, iTipoTurnocheck: DropCheckEmpleAct.selectedIndex
                , iTipoPausacheck: DropChekComAct.selectedIndex, iDiasDes: DropDiasDescAct.selectedIndex, iTipoTurno: DropChekTurAct.selectedIndex, iTipoPausa: DropChekPauAct.selectedIndex
            }


            $.ajax({
                url: "../RH/UpdateHorario",
                type: "POST",
                data: DataInser,
                success: function (data) {
                    if (data.sMensaje == "success") {
                        DTBHorarios();
                        fshowtypealert('Horario', 'Actualización correcta', 'success');
                    }

                    if (data.sMensaje == "error") {

                        fshowtypealert('Error', 'Contacte a sistemas', 'error');
                    }
                },
            });
        }
        else {

            fshowtypealert('Horario', 'Ningun dato es distinto', 'warning');
        }


    };

    btnActHr.addEventListener('click', FActualiza);

    FDelet = () => {
        var dataRecord = $("#dTabHrDia").jqxGrid('getrowdata', deletrow);
        const Delet = {  HrId: dataRecord.iIdHorario}
        $.ajax({
            url: "../RH/DeletHorario",
                type: "POST",
                 data: Delet,
                success: function (data) {
                    if (data.sMensaje == "success") {
                        DTBHorarios();
                        fshowtypealert('Horario', 'Eliminado correcta', 'success');
                    }
                    if (data.sMensaje == "error") {

                        fshowtypealert('Error', 'Contacte a sistemas', 'error');
                    }
                },
        });
    }



    /////////////////// Horario Semanal //////////////////




    DTBHrSem = () => {

        $("#dTabHrSem").jqxGrid('clear')
        $.ajax({
            url: "../RH/RetrieveHrSemanl",
            type: "POST",
            data: JSON.stringify(),
            success: (data) => {
                if (data[0].sMensaje == "success") {
                    var source =
                    {
                        localdata: data,
                        datatype: "array",
                        datafields:
                            [
                                { name: 'iIdHrSM', type: 'int' },
                                { name: 'iEmpId', type: 'int' },
                                { name: 'iNoHr', type: 'int' },
                                { name: 'sdescrip', type: 'string' },
                                { name: 'iLu', type: 'int' },
                                { name: 'iMa', type: 'int' },
                                { name: 'iMe', type: 'int' },
                                { name: 'iJu', type: 'int' },
                                { name: 'iVi', type: 'int' },
                                { name: 'iSa', type: 'int' },
                                { name: 'iDo', type: 'int' },
                                { name: 'iUsuId', type: 'int' },
                                { name: 'iCancel', type: 'int' }

                            ]
                    };
                    var dataAdapter = new $.jqx.dataAdapter(source);
                    $("#dTabHrSem").jqxGrid(
                        {

                            width:730,
                            source: dataAdapter,
                            columnsresize: true,
                            columns: [
                                { text: 'No. Horario', datafield: 'iIdHrSM', width: 70 },
                                { text: 'No. Emp', datafield: 'iEmpId', width: 60 },
                                { text: 'Descripcion', datafield: 'sdescrip', width: 120 },
                                { text: 'Lunes', datafield: 'iLu', width: 40 },
                                { text: 'Martes', datafield: 'iMa', width: 50 },
                                { text: 'Miercoles', datafield: 'iMe', width: 60 },
                                { text: 'Jueves', datafield: 'iJu', width: 50 },
                                { text: 'viernes', datafield: 'iVi', width: 50 },
                                { text: 'Sabado', datafield: 'iSa', width: 50 },
                                { text: 'Domingo', datafield: 'iDo', width: 70 },
                                {
                                    text: 'Edit', datafield: 'Edit', columntype: 'button', width: 50, cellsrenderer: function () {
                                        return "Edit";
                                    }, buttonclick: function (row) {
                                        // open the popup window when the user clicks a button.
                                        editrow = row;
                                        var offset = $("#dTabHrSem").offset();
                                        var dataRecord = $("#dTabHrSem").jqxGrid('getrowdata', editrow);
                                        //$("#btnFloActu").click();

                                        for (var i = 0; i < DropEmpAct.length; i++) {
                                            if (DropEmpAct.options[i].text == dataRecord.sNombreEmpresa) {
                                                // seleccionamos el valor que coincide
                                                DropEmpAct.selectedIndex = i;
                                            }

                                        }
                                        TxtTurnoAct.value = dataRecord.iTurno;
                                        DescripcionhrAct.value = dataRecord.sDescrip;
                                        DropCheckEmpleAct.selectedIndex = dataRecord.iTipCheckNorm;
                                        TxtHrEntAct.value = dataRecord.sHrEnt;
                                        TxtHrSalAct.value = dataRecord.sHrSal;
                                        DropChekComAct.selectedIndex = dataRecord.iTipCheckPausa;
                                        TxtHrSalPauAct.value = dataRecord.sHrSalCom;
                                        TxtHrEntPauAct.value = dataRecord.sHrEntCom;
                                        DropChekTurAct.selectedIndex = dataRecord.iTipoTurno;
                                        DropChekPauAct.selectedIndex = dataRecord.iTipoPausa;
                                        DropDiasDescAct.selectedIndex = dataRecord.iDiasDesc;

                                    }
                                },
                                {
                                    text: 'Eliminar', datafield: 'Delet', columntype: 'button', width: 60, cellsrenderer: function () {
                                        return "Delet";
                                    }, buttonclick: function (row) {
                                        // open the popup window when the user clicks a button.
                                        deletrow = row;
                                        var offset = $("#dTabHrSem").offset();
                                        var dataRecord2 = $("#dTabHrSem").jqxGrid('getrowdata', deletrow);
                                      
                                     
                                    }
                                },
                            ]
                        });
                }
                else {
                    fshowtypealert('Error', 'Contacte a sistemas ', 'error');
                }
            }
        });
    }
    DTBHrSem();





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