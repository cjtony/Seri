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
   
    const btninserHr = document.getElementById('btninserHr');




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
        $.ajax({
            url: "../RH/RetrieveHorarios",
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
                                { name: 'iIdHorario', type: 'int' },
                                { name: 'iEmpresaId', type: 'int' },
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
                                { name: 'iTipoPausa', type: 'int' },
                                { name: 'iTipoPausa', type: 'int' }

                            ]
                    };
                    var dataAdapter = new $.jqx.dataAdapter(source);
                    $("#dTabHrSem").jqxGrid(
                        {
                            width: 950,
                            source: dataAdapter,
                            columnsresize: true,
                            columns: [

                                { text: 'Id Horario', datafield: 'iIdHorario', width: 100 },
                                { text: 'Empresa', datafield: 'iEmpresaId', width: 200 },
                                { text: 'Descripcion', datafield: 'sDescrip', whidt: 100 },
                                { text: 'No. Turno', datafield: 'iTurno', whidth: 190 },
                                { text: 'Tipo de chequeo', sHrEnt: 'iTipCheckNorm', whidt: 100 },
                                { text: 'Hora Entrada ', datafield: 'sHrEnt', width: 100 },
                                { text: 'Hora Salida', datafield: 'sHrSal', width: 200 },
                                { text: 'Tipo chequeo Pauda', datafield: 'sUsuario', whidth: 190 },
                                { text: 'Fecha inicio', datafield: 'sFechaIni', whidt: 100 },
                                { text: 'Fecha Final', datafield: 'sFechaFinal', whidt: 100 },
                                { text: 'Estatus', datafield: 'sEstatusFinal', whidt: 80 },
                                { text: 'Id Horario', datafield: 'iIdTarea', width: 100 },
                                { text: 'Definicion', datafield: 'sNombreDefinicion', width: 200 },
                                { text: 'Usuario', datafield: 'sUsuario', whidth: 190 },
                                { text: 'Fecha inicio', datafield: 'sFechaIni', whidt: 100 },
                                { text: 'Fecha Final', datafield: 'sFechaFinal', whidt: 100 },
                                { text: 'Estatus', datafield: 'sEstatusFinal', whidt: 80 },
                                { text: 'No Tarea', datafield: 'iIdTarea', width: 100 },
                                { text: 'Definicion', datafield: 'sNombreDefinicion', width: 200 },
                                { text: 'Usuario', datafield: 'sUsuario', whidth: 190 },
                                { text: 'Fecha inicio', datafield: 'sFechaIni', whidt: 100 },
                                { text: 'Fecha Final', datafield: 'sFechaFinal', whidt: 100 },
                                { text: 'Estatus', datafield: 'sEstatusFinal', whidt: 80 },
                            ]
                        });
                }
                else {
                    fshowtypealert('Error', 'Contacte a sistemas ', 'error');
                }
            }
        });
    }

   


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