$(function () {
    //DataTable de Registros Patronales 
    const tabRP = $("#tabRegistrosPatronales").DataTable({
        ajax: {
            method: "POST",
            url: "../Empresas/LoadRegistrosPatronales",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            dataSrc: "data",
            scroll: true,
            beforeSend: () => {
                SelectLoaderFromRegPat();
            }
        },
        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
        },
        columns: [
            { "data": "Afiliacion_IMSS" },
            { "data": "Empresa_id" },
            { "data": "ClasesRegPat_id" },
            { "data": "Riesgo_Trabajo" },
            { "data": "Cancelado" },
            { "defaultContent": "<button class='btneditar btn btn-outline-success btn-sm text-center shadow rounded' title='Editar Registro' > <i class='fas fa-edit'></i> </button>" }
        ]
    });
    //Retorna los valores del reglon del registro patronal seleccionado para editar 
    $("#tabRegistrosPatronales tbody").on('click', 'button', function () {
        var dato = tabRP.row($(this).parents('tr')).data();
        console.log(dato);
        var Afrp = document.getElementById("inAfiliacionImssRP");
        var Emrp = document.getElementById("inEmpresaRP");
        var Rtrp = document.getElementById("inRiesgoTrabajoRP");
        var NomRP = document.getElementById("inNombreAfiliacionRP");
        Afrp.value = dato.Afiliacion_IMSS;
        $.ajax({
            url: "../Empresas/LoadRegistroPatronal",
            type: "POST",
            data: JSON.stringify({ IdRegPat: dato.IdRegPat }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                console.log(data[0]["Nombre_Afiliacion"]);
                NomRP.value = data[0]["Nombre_Afiliacion"];
            }
        });


        $.ajax({
            url: "../Empresas/LoadClasesRP",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var clase = document.getElementById("inClase");
                clase.innerHTML = "<option vlaue=''>Selecciona</option>";
                for (i = 0; i < data.length; i++) {
                    console.log(data[i].IdClase + '-' + dato.ClasesRegPat_id);
                    if (data[i].IdClase == dato.ClasesRegPat_id) {
                        clase.innerHTML += `<option selected value='${data[i].IdClase}'>${data[i].Nombre_Clase}</option>`;
                    }
                    else {
                       clase.innerHTML += `<option value='${data[i].IdClase}'>${data[i].Nombre_Clase}</option>`;
                    }
                }
            }
        });
        

        var regresar = dato.Riesgo_Trabajo.toString().replace(/\,/g, '.');

        Rtrp.value = regresar;
        if (dato.Cancelado == "True") {
            if ($("#inStatusRP").is(":checked")) {
                //document.getElementById("lblinStatusRP").innerHTML = "Activo";
            } else {
                $("#inStatusRP").click();
                //document.getElementById("lblinStatusRP").innerHTML = "Activo";
            }
        } else {
            if ($("#inStatusRP").is(":checked")) {
                $("#inStatusRP").click();
                //document.getElementById("lblinStatusRP").innerHTML = "Inactivo";
            } else {
                //document.getElementById("lblinStatusRP").innerHTML = "Inactivo";
            }
        }
        Afrp.focus();
        //Funcion apaga el boton de guardado por default y activa el de actualizar
        
        document.getElementById("btnGuardarRP").classList.add("invisible");
        document.getElementById("btnUpdateRP").classList.remove("invisible");
        document.getElementById("btnClearRP").classList.remove("invisible");
    });
    //
    $("#btnClearRP").on("click", function () {
        document.getElementById("inAfiliacionImssRP").value = "";
        
        document.getElementById("inRiesgoTrabajoRP").value = "";
        document.getElementById("inNombreAfiliacionRP").value = "";
        document.getElementById("inClase").innerHTML = "";
        //Clases
        $.ajax({
            url: "../Empresas/LoadClasesRP",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                
                for (i = 0; i < data.length; i++) {
                    document.getElementById("inClase").innerHTML += `<option value='${data[i].IdClase}'>${data[i].Nombre_Clase}</option>`;

                    //document.getElementById("btnUpdateRP").classList.add("invisible");
                }
            }
        });
        document.getElementById("btnGuardarRP").classList.remove("invisible");
        document.getElementById("btnUpdateRP").classList.add("invisible");
        document.getElementById("btnClearRP").classList.add("invisible");
    });
    //
    $("#inStatusRP").on("click", function () {
        if ($(this).is(':checked')) {
            //document.getElementById("lblinStatusRP").innerHTML = "Activo";
        } else {
            //document.getElementById("lblinStatusRP").innerHTML = "Inactivo";
        }
    });
    // Funcionalidad del boton guardar Registro patronal
    $("#btnGuardarRP").on("click", function () {
        var Afrp = document.getElementById("inAfiliacionImssRP");
        var Emrp = document.getElementById("inEmpresaRP");
        var Rtrp = document.getElementById("inRiesgoTrabajoRP");
        var riesgop = Rtrp.value.toString().replace(/\./g, ',');
        var Clrp = document.getElementById("inClase");
        var Strp;
        var NomAf = document.getElementById("inNombreAfiliacionRP");
        if ($("#inStatusRP").is(':checked')) {
            Strp = 1;
        } else {

            Strp = 0;
        }
        var datos = { Afiliacion_IMSS: Afrp.value, Nombre_Afiliacion: NomAf.value, Empresa_id: Emrp.value, Riesgo_Trabajo: Rtrp.value, ClasesRegPat: Clrp.value, Status: Strp };
        var form = document.getElementById("frmRegistrosPatronales");
        if (form.checkValidity() === false) {
            //alert("no valido");
            form.classList.add("was-validated");
        } else {
            console.log("valido");
            $.ajax({
                url: "../Empresas/Insert_Registro_Patronal",
                type: "POST",
                data: JSON.stringify(datos),
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    console.log(data);
                    tabRP.ajax.reload();
                    form.reset();
                    
                }
            });

        }
    });
    //Funcionalidad boton Actualizar Registro patronal
    $("#btnUpdateRP").on("click", function () {
        var Afrp = document.getElementById("inAfiliacionImssRP");
        var Emrp = document.getElementById("inEmpresaRP");
        var Rtrp = document.getElementById("inRiesgoTrabajoRP");
        //var riesgop = Rtrp.value.toString().replace(/\./g, ',');
        var Clrp = document.getElementById("inClase");
        var Strp = document.getElementById("inStatusRP");
        var datos = { Afiliacion_IMSS: Afrp.value, Empresa_id: Emrp.value, Riesgo_Trabajo: Rtrp.value, ClasesRegPat: Clrp.value, Cancelado: Strp.value };
        var form = document.getElementById("frmRegistrosPatronales");
        if (form.checkValidity() === false) {
            alert("no valido");
            form.classList.add("was-validated");
        } else {
            console.log("valido");
            console.log(datos);
        }
        //$.ajax({
        //    url: "../Empresas/Update_Registro_Patronal",
        //    type: "POST",
        //    data: JSON.stringify(datos),
        //    contentType: "application/json; charset=utf-8",
        //    success: (data) => {
        //        console.log(data);
        //        if (data[0] == "1") {
        //            Swal.fire({
        //                icon: 'success',
        //                title: 'Correcto!',
        //                text: 'Registro Patronal actualizado con exito!'
        //            }, false).then(() => {

        //                setTimeout(function () {
        //                    $("#frmRegistrosPatronales").reset();
        //                }, 500);
        //            });
        //        } else if (data[0] == "0") {
        //            Swal.fire({
        //                icon: 'error',
        //                title: '',
        //                text: '' + data[1]
        //            }, false).then(() => {
        //                Afrp.focus();
        //                Afrp.classList.add("is-invalid");
        //                setTimeout(function () {
        //                    Afrp.classList.remove("is-invalid");
        //                }, 5500);
        //            });
        //        }

        //    }
        //});
    });
    //Funcionalidad boton Actualizar Localidad
    $("#btnSaveLocalidad").on("click", function() {
        var Desl = document.getElementById("inDescLocalidad");
        var Tasl = document.getElementById("inTazaIvaLocalidad");
        var Afil = document.getElementById("inAfImssLocalidad");
        var Sucl = document.getElementById("inZoInLocalidad");
        var Regl = document.getElementById("inRegLocalidad");
        var ZoEl = document.getElementById("inZoEcLocalidad");
        var Estl = document.getElementById("inEstLocalidad");
        var datos = { Descripcion: Desl.value, TasaIva: Tasl.value, Afiliacion_IMSS: Afil.value, Sucursal_id: Sucl.value, Regional_id: Regl.value, ZonaEconomica_id: ZoEl.value, Estado_id: Estl.value };
        var form2 = document.getElementById("frmLocalidades");
        if (form2.checkValidity() === false) {
            //alert("no valido");
            form2.classList.add("was-validated");
        } else {
            $.ajax({
                url: "../Empresas/Insert_Localidad",
                type: "POST",
                data: JSON.stringify(datos),
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    console.log(data[0]);
                    if (data[0] == "1") {
                        tabLoc.ajax.reload();
                        Swal.fire({
                            icon: 'success',
                            title: 'Correcto!',
                            text: '' + data[1]
                            
                        });
                    } else if (data[1] == "0") {
                        Swal.fire({
                            icon: 'error',
                            title: '',
                            text: '' + data[1]
                        
                        });
                    }
                    
                    
                }
            });

        }
    });
    //Retorna los valores del reglon de la localidad seleccionada para editar 
    $("#tabLocalidades tbody").on('click', 'button', function () {
        var datos = tabLc.row($(this).parents('tr')).data();
        var DesLoc = document.getElementById("inDescLocalidad");
        var TaIvaLoc = document.getElementById("inTazaIvaLocalidad");
        var AfImssLoc = document.getElementById("inAfImssLocalidad");
        var ZoInLoc = document.getElementById("inZoInLocalidad");
        var RegLoc = document.getElementById("inRegLocalidad");
        var ZoEcLoc = document.getElementById("inZoEcLocalidad");
        var EstLoc = document.getElementById("inEstLocalidad");
        DesLoc.value = datos.Descripcion;
        $.ajax({
            url: "../Empleados/LoadStates",
            type: "POST",
            data: {},
            contentType: "application/json; charset=utf-8",
            success: (data) => {

                for (i = 0; i < data.length; i++) {
                    if (datos.Estado_id == data[i].iId) {
                        EstLoc.innerHTML += `
                                <option selected value="${data[i].iId}">${data[i].sValor}</option>
                            `;
                    } else {
                        EstLoc.innerHTML += `
                                <option value="${data[i].iId}">${data[i].sValor}</option>
                            `;
                    }
                }
            }
        });

        console.log(data);
    });
    //DataTable de Localidades
    const tabLoc = $("#tabLocalidades").DataTable({
        ajax: {
            method: "POST",
            url: "../Empresas/LoadLocalidades",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            dataSrc: "data",
            scrollY: true ,
            scrollX: true,
            beforeSend: () => {
                SelectLoaderFromLocalidades();
            }
        },
        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
        },
        columns: [
            { "data": "Empresa_id" },
            { "data": "Codigo_Localidad" },
            { "data": "Descripcion" },
            { "data": "TasaIva" },
            { "data": "RegistroPatronal_id" },
            { "data": "Regional_id" },
            { "data": "ZonaEconomica_id" },
            { "data": "Sucursal_id" },
            { "data": "Estado_id" },
            { "defaultContent": "<button class='btneditar btn btn-outline-success btn-sm text-center shadow rounded' title='Editar Registro' > <i class='fas fa-edit'></i> </button>" }
        ]
    });

    SelectLoaderFromLocalidades = () => {
        //Regionales
        $.ajax({
            url: "../Empresas/LoadRegionalesEmp",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                
                var clase = document.getElementById("inRegLocalidad");
                clase.innerHTML = "<option vlaue=''>Selecciona</option>";
                for (i = 0; i < data.length; i++) {

                    clase.innerHTML += `<option value='${data[i].iIdRegional}'>${data[i].iIdRegional + " - " + data[i].sDescripcionRegional}</option>`;
                }
            }
        });
        //Sucursales
        $.ajax({
            url: "../Empresas/LoadSucursales",
            type: "POST",
            data: {},
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                
                var sucursales = document.getElementById("inZoInLocalidad");
                sucursales.innerHTML = '<option>Selecciona</option>';
                for (i = 0; i < data.length; i++) {

                    sucursales.innerHTML += `<option value="${data[i].iIdSucursal}">${data[i].sClaveSucursal + " - " + data[i].sDescripcionSucursal}</option>`;

                }
            }
        });
        //Zona Economica
        $.ajax({
            url: "../Empresas/LoadZonaEconomica",
            type: "POST",
            data: {},
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                
                var sucursales = document.getElementById("inZoEcLocalidad");
                sucursales.innerHTML = '<option>Selecciona</option>';
                for (i = 0; i < data.length; i++) {

                    sucursales.innerHTML += `<option value="${data[i].iIdZonaEconomica}">${data[i].iIdZonaEconomica + " - " + data[i].sDescripcion}</option>`;

                }
            }
        });
        //Estados
        $.ajax({
            url: "../Empleados/LoadStates",
            type: "POST",
            data: {},
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                
                var estados = document.getElementById("inEstLocalidad");
                estados.innerHTML = '<option>Selecciona</option>';
                for (i = 0; i < data.length; i++) {

                    estados.innerHTML += `<option value="${data[i].iId}">${data[i].sValor}</option>`;

                }
            }
        });
        //Registro Patronal
        $.ajax({
            url: "../Empresas/LoadRegPat",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                
                var clase = document.getElementById("inAfImssLocalidad");
                //clase.innerHTML = "<option vlaue=''>Selecciona</option>";
                for (i = 0; i < data.length; i++) {

                    clase.innerHTML += `<option value='${data[i].IdRegPat}'>${data[i].Afiliacion_IMSS}</option>`;
                }
            }
        });
    }
    SelectLoaderFromRegPat = () => {
        //Empresa
        $.ajax({
            url: "../Empresas/SearchEmpresa",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (dato) => {
                
                var Emrp = document.getElementById("inEmpresaRP");
                Emrp.innerHTML = "<option value='" + dato[0].IdEmpresa + "'>" + dato[0].IdEmpresa + " - " + dato[0].RazonSocial + "</option>";
            }
        });
        //Clases
        $.ajax({
            url: "../Empresas/LoadClasesRP",
            type: "POST",
            data: JSON.stringify(),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                
                for (i = 0; i < data.length; i++) {
                    document.getElementById("inClase").innerHTML += `<option value='${data[i].IdClase}'>${data[i].Nombre_Clase}</option>`;

                    //document.getElementById("btnUpdateRP").classList.add("invisible");
                }
            }
        });
    }
    
    
});
