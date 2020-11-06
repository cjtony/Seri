$(function () {
    /// declaracion de variables 
    const DropCategoria = document.getElementById('DropCategoria');
    const DroTipoEmple = document.getElementById('DroTipoEmple');
    const btnGnr = document.getElementById('btnGnra');
    const btnDowl = document.getElementById('btnDowlan');

    // Variable

    var IdNomina;
    var Idempresa;
    var rowscounts;

    $('#DropCategoria').change(function () {
        if (DropCategoria.value == 1) {
            LisTipoEmpleado();

        }

        if (DropCategoria.value == 2) {
            $("#DroTipoEmple").empty();
            $('#DroTipoEmple').append('<option value="0" selected="selected">Selecciona</option>');
            var TipoEmpleado = 0;
            FLoadEmpleados(TipoEmpleado, DropCategoria.value);

        }
        if (DropCategoria.value == 0) {
            $("#DroTipoEmple").empty();
            $('#DroTipoEmple').append('<option value="0" selected="selected">Selecciona</option>');
        }
         FLoadEmpleados();

    });




    LisTipoEmpleado = () => {
        $.ajax({
            url: "../Documentos/LisTipodeEmpleado",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                for (i = 0; i < data.length; i++) {
                    document.getElementById("DroTipoEmple").innerHTML += `<option value='${data[i].iIdTipoEmpleado}'>${data[i].sTipodeEmpleado}</option>`;
                }
            }
        });

    };
   
    $('#DroTipoEmple').change(function () {
        
        FLoadEmpleados(DroTipoEmple.value, DropCategoria.value);
       
    });



    // carga un listado con detalle de los empleados que pertenencen del tipo de empleado
    FLoadEmpleados = (TipodeEmpleado,baja) => {
        FDelettable();
        const dataSend = { IdTipoempleado: TipodeEmpleado, opBaja: baja }
        console.log(dataSend);
        $.ajax({
            url: "../Documentos/KitEmpleados",
            type: "POST",
            data: dataSend,
            success: (data) => {
                rowscounts = data.length;
                var source =
                {
                    localdata: data,
                    datatype: "array",
                    datafields:
                        [
                            { name: 'iIdEmpresa', type: 'int' },
                            { name: 'iIdNomina', type: 'int' },
                            { name: 'sNombreComp', type: 'string' },
                        ]
                };
                var dataAdapter = new $.jqx.dataAdapter(source);
                $("#TbKit").jqxGrid(
                    {
                       
                        width: 500,
                        source: dataAdapter,
                        selectionmode: 'multiplerowsextended',
                        sortable: true,
                        pageable: true,
                        autoheight: true,
                        columnsresize: true,
                        columns: [
                            { text: 'No Empresa', datafield: 'iIdEmpresa', width: 100 },
                            { text: 'Nomina', datafield: 'iIdNomina', width: 100 },
                            { text: 'Nompre del Empleado', datafield: 'sNombreComp', width: 300 },
                         
                        ]
                    });
            },
        
        });
   
    };

    ///Temas 
   // $("#TbKit").jqxDataTable({ theme: 'base' });

    // selesciona Empleado

     $("#TbKit").on('rowselect', function (event) {
         var args = event.args;
         var row = $("#TbKit").jqxGrid('getrowdata', args.rowindex);
         Idempresa = row['iIdEmpresa'];
         IdNomina = row['iIdNomina'];

    });

    /// LLenado de correspondencia del documento en word

    FGeneradoc = () => {
        console.log('generaword');
        const dataSend = { iIdempresa: Idempresa, iNomina: IdNomina, iCategoria: DropCategoria.value }  
        $.ajax({
                url: "../Documentos/KitDocbaja",
                type: "POST",
                data: dataSend,
                success: (data) => {
                    console.log(data);
                    if (data[0].sMensaje == "success") {
                        if (DropCategoria.value == 1) {
                            fshowtypealert('Kit Contratacion', "El documento se genero exitosamente", 'succes');
                            btnDowl.style.visibility = 'visible';
                            btnDowlan.value = data[0].sUrl;
                        }    
                        if (DropCategoria.value == 2) {
                            fshowtypealert('Kit Contratacion', "El documento se genero exitosamente", 'succes');
                            btnDowl.style.visibility = 'visible';
                            btnDowlan.value = data[0].sUrl;
                        
                        } 
                    }
                    if (data[0].sMensaje == "error") {

                        fshowtypealert('Kit Contratacion', "contacte a sistemas ", 'error');
                    }

                },

                error: function (jqXHR, exception) {
                    fcaptureaerrorsajax(jqXHR, exception);
                    fshowtypealert('Kit Contratacion', "contacte a sistemas ", 'error');
                }
            }); 
    };

    btnGnr.addEventListener('click',FGeneradoc)

    FDowlankit = () => {
        console.log('bajadocumento');
        if (DropCategoria.value == 1) {
            console.log('documento1' + btnDowl.value);
            var url = btnDowl.value;
            console.log(url);
            url = url.replace('%2520', ' ');
            console.log(url);
            url = url.replace('%20', ' ');
            url = url.replace('C:\\Users\\DELL\\Desktop\\gitproyecto\\Proyecto Actualizado AAA\\Seri\\Payroll', '')
            console.log(url);
            window.open(url);
        }
        if (DropCategoria.value == 2) {
            console.log('documento2' + btnDowl.value);
            var url =btnDowl.value;
            console.log(url);
            url = url.replace('%2520', ' ');
            console.log(url);
            url = url.replace('%20', ' ');
            url = url.replace('C:\\Users\\DELL\\Desktop\\gitproyecto\\Proyecto Actualizado AAA\\Seri\\Payroll','')
            console.log(url); 
            window.open(url);
        }

    };
    btnDowl.addEventListener('click', FDowlankit);


    FDelettable = () => {
        for (var i = 0; i <= rowscounts; i++) {

            $("#TbKit").jqxGrid('deleterow', i);
        }
       

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

});