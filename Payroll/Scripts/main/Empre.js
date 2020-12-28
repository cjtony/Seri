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
            { "defaultContent": "<button class='btneditar btn btn-outline-success btn-sm text-center shadow rounded' title='Editar Registro' > <i class='fas fa-edit'></i> </button> <button class='btndelete btn btn-outline-danger btn-sm text-center shadow rounded' title='Eliminar' > <i class='fas fa-minus'></i> </button>" }
        ]
    });
    //Retorna los valores del reglon del registro patronal seleccionado para editar 
    $("#tabRegistrosPatronales tbody").on('click', 'button.btneditar', function () {
        var dato = tabRP.row($(this).parents('tr')).data();
        console.log(dato);
        var Afrp = document.getElementById("inAfiliacionImssRP");
        var Emrp = document.getElementById("inEmpresaRP");
        var Rtrp = document.getElementById("inRiesgoTrabajoRP");
        var NomRP = document.getElementById("inNombreAfiliacionRP");
        var id = document.getElementById("IdRegPat");
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
                id.value = data[0]["IdRegPat"];
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
    //$("#tabRegistrosPatronales tbody").on('click', 'button.btneditar', function () {
    //    var dato = tabRP.row($(this).parents('tr')).data();
    //    console.log(dato);
    //    var Afrp = document.getElementById("inAfiliacionImssRP");
    //    var Emrp = document.getElementById("inEmpresaRP");
    //    var Rtrp = document.getElementById("inRiesgoTrabajoRP");
    //    var NomRP = document.getElementById("inNombreAfiliacionRP");
    //    var id = document.getElementById("IdRegPat");
    //    Afrp.value = dato.Afiliacion_IMSS;
    //    $.ajax({
    //        url: "../Empresas/DeleteRegistroPatronal",
    //        type: "POST",
    //        data: JSON.stringify({ IdRegPat: dato.IdRegPat }),
    //        contentType: "application/json; charset=utf-8",
    //        success: (data) => {
    //            console.log(data);
    //            console.log(data[0]["Nombre_Afiliacion"]);
    //            NomRP.value = data[0]["Nombre_Afiliacion"];
    //            id.value = data[0]["IdRegPat"];
    //        }
    //    });


    //});
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
                document.getElementById("inClase").innerHTML = "";
                for (i = 0; i < data.length; i++) {
                    document.getElementById("inClase").innerHTML += `<option value='${data[i].IdClase}'>${data[i].Nombre_Clase}</option>`;

                    //document.getElementById("btnUpdateRP").classList.add("invisible");
                }
                document.getElementById("frmRegistrosPatronales").classList.remove("was-validated");
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
                    if (data[0] == '0') {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Aviso!',
                            text: data[1]
                        });
                    } else if (data[0] == '1') {
                        tabRP.ajax.reload();
                        form.reset();
                        Swal.fire({
                            icon: 'success',
                            title: 'Hecho!',
                            text: data[1],
                            timer: 1000
                        });
                    }
                   
                    
                    
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
        var Nomrp = document.getElementById("inNombreAfiliacionRP");
        var Clrp = document.getElementById("inClase");
        var Strp;
        if ($("#inStatusRP").prop("checked")) {
            Strp = 1;
        } else {
            Strp = 0;
        }
        var id = document.getElementById("IdRegPat");
        var datos = {
            Id: id.value
            , Afiliacion_IMSS: Afrp.value
            , NombreAfiliacion: Nomrp.value
            , Empresa_id: Emrp.value
            , Riesgo_Trabajo: Rtrp.value
            , ClasesRegPat: Clrp.value
            , Cancelado: Strp
        };
        var form = document.getElementById("frmRegistrosPatronales");
        if (form.checkValidity() === false) {
            alert("no valido");
            form.classList.add("was-validated");
        } else {
            console.log("valido");
            console.log(datos);
            $.ajax({
                url: "../Empresas/Update_Registro_Patronal",
                type: "POST",
                data: JSON.stringify(datos),
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    console.log(data);
                    if (data[0] == "1") {
                        document.getElementById("frmRegistrosPatronales").reset();
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
                        document.getElementById("btnGuardarRP").classList.remove("invisible");
                        document.getElementById("btnUpdateRP").classList.add("invisible");
                        document.getElementById("btnClearRP").classList.add("invisible");
                        Swal.fire({
                            icon: 'success',
                            title: 'Correcto!',
                            text: data[1]
                        }, false).then(() => {

                            setTimeout(function () {
                                
                                tabRP.ajax.reload();

                            }, 500);
                        });
                    } else if (data[0] == "0") {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Aviso',
                            text: '' + data[1]
                        }, false).then(() => {
                            Afrp.focus();
                            Afrp.classList.add("is-invalid");
                            setTimeout(function () {
                                Afrp.classList.remove("is-invalid");
                            }, 5500);
                        });
                    }

                }
            });
        }
        
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
                    if (data[0] == "1") {
                        tabLoc.ajax.reload();
                        form2.reset();
                        $("#inZoInLocalidad").html("");
                        Swal.fire({
                            icon: 'success',
                            title: 'Correcto!',
                            html: '<p>' + data[1] + "<br/><small>El registro se mostrara al final de la tabla.</small>",
                            timer: 2000
                        });
                    } else if (data[1] == "0") {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Aviso!',
                            text: '' + data[1]
                            
                        });
                    }
                    
                    
                }
            });

        }
    });
    //Retorna los valores del reglon de la localidad seleccionada para editar 
    $("#tabLocalidades tbody").on('click', 'button', function () {
        var datos = tabLoc.row($(this).parents('tr')).data();
        var DesLoc = document.getElementById("inDescLocalidad");
        var TaIvaLoc = document.getElementById("inTazaIvaLocalidad");
        var AfImssLoc = document.getElementById("inAfImssLocalidad");
        var ZoInLoc = document.getElementById("inZoInLocalidad");
        var RegLoc = document.getElementById("inRegLocalidad");
        var ZoEcLoc = document.getElementById("inZoEcLocalidad");
        var EstLoc = document.getElementById("inEstLocalidad");
        DesLoc.value = datos.Descripcion;
        TaIvaLoc.value = datos.Empresa_id;
        $("#inZoEcLocalidad option[value='" + datos.ZonaEconomica_id + "']").attr("selected", true);
        $("#inEstLocalidad option[value='" + datos.Estado_id + "']").attr("selected", true);
        $("#inRegLocalidad option[value='" + datos.Regional_id + "']").attr("selected", true);
        $("#inAfImssLocalidad option[value='" + datos.RegistroPatronal_id + "']").attr("selected", true);
        $("#inZoInLocalidad option[value='" + datos.Sucursal_id + "']").attr("selected", true);
        console.log(datos);
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
            scrollX: true
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
                
                if (data[0]["sDescripcionRegional"] == "true") {
                    $("#lbltitlelocalidades").html('<div class="alert alert-danger col-md-12" role="alert">No se tienen regionales registradas, para poder continuar con el registro de una localidad agrega al menos una regional desde el modulo de <strong>Catalogos-Regionales</strong> o desde el modulo de <strong>Empleado-Registro</strong></div>');
                    Swal.fire({
                        title: "Aviso!",
                        icon: "warning",
                        html: "<p>No se han encontrado Regionales para esta empresa, agrega al menos una para continuar y poder agregar <strong>Localidades</strong></p>"
                        //timer: 2000
                    });
                    $("#btnSaveLocalidad").attr("disabled",true);
                    document.getElementById("inRegLocalidad").innerHTML = "<option class='bg-danger text-dark' vlaue=''>Sin Regionales</option>";
                } else {
                    var clase = document.getElementById("inRegLocalidad");
                    clase.innerHTML = "<option vlaue=''>Selecciona</option>";
                    for (i = 0; i < data.length; i++) {
                        clase.innerHTML += `<option value='${data[i].iIdRegional}'>${data[i].iIdRegional + " - " + data[i].sDescripcionRegional}</option>`;
                    }
                }
                    
                
            }
        });
        //Sucursales
        //$.ajax({
        //    url: "../Empresas/LoadSucursales",
        //    type: "POST",
        //    data: {},
        //    contentType: "application/json; charset=utf-8",
        //    success: (data) => {
        //        //console.log(data);
        //        var sucursales = document.getElementById("inZoInLocalidad");
        //        sucursales.innerHTML = "<option value=''> Selecciona </option>";
        //        for (i = 0; i < data.length; i++) {

        //            sucursales.innerHTML += `<option value="${data[i].iIdSucursal}">${data[i].sClaveSucursal + " - " + data[i].sDescripcionSucursal}</option>`;

        //        }
        //    }
        //});
        //Zona Economica
        $.ajax({
            url: "../Empresas/LoadZonaEconomica",
            type: "POST",
            data: {},
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                
                var sucursales = document.getElementById("inZoEcLocalidad");
                sucursales.innerHTML = '<option value=""> Selecciona </option>';
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
                estados.innerHTML = '<option value=""> Selecciona </option>';
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
                document.getElementById("inClase").innerHTML = "<option vlaue=''>Selecciona</option>";
                for (i = 0; i < data.length; i++) {
                    document.getElementById("inClase").innerHTML += `<option value='${data[i].IdClase}'>${data[i].Nombre_Clase}</option>`;

                    //document.getElementById("btnUpdateRP").classList.add("invisible");
                }
            }
        });
    }
    /////////////////////////////////////////////////////
    /////////////////////////////////////////////////////
    // VISTA BANCOS
    loadview = () => {
        $("#lbltitulo").html("Bancos de la empresa " + $("#btnNameEmpresaSelected").html());
    }
    //
    //CARGA BANCOS
    LoadSelectBancos = (Banco_id) => {
        $.ajax({
            url: "../Empleados/LoadBanks",
            type: "POST",
            data: JSON.stringify({ keyban: Banco_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                
                var select = document.getElementById("newbanco");
                select.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    
                    select.innerHTML += "<option value='" + data[i]["iIdBanco"] + "'>" + data[i]["sNombreBanco"] + "</option>"
                }

            }
        });
    }
    //
    // CARGA TABLA DE BANCOS DE LA EMPRESA
    loadbancosempresa = (collapse,Empresa_id) => {
        $.ajax({
            url: "../Catalogos/LoadBancosEmpresa",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                document.getElementById(collapse).innerHTML = "";
                document.getElementById(collapse).innerHTML += "<table class='table table-sm table-in-fechas-periodos col-md-12 m-3'>" +
                    "<thead class='col-md-12'>" +
                    "<tr>" +
                    "<th>Banco Id</th>" +
                    "<th>Nombre Banco</th>" +
                    "<th># Cliente</th>" +
                    "<th>Plaza</th>" +
                    "<th># Cuenta Empresa</th>" +
                    "<th>Clabe</th>" +
                    "<th>Tipo Banco</th>" +
                    "<th class='text-center'>Activo</th>" +
                    "<th class=''>Acciones</th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='tab" + collapse + "' class=''></tbody>" + "</table>";
                for (var j = 0; j < data.length; j++) {
                    if (data[j]["Cancelado"] == "True") {
                        document.getElementById("tab" + collapse).innerHTML += "<tr>" +
                            "<td class=''>" + data[j]["Banco_id"] + "</td>" +
                            "<td>" + data[j]["Descripcion"] + "</td>" +
                            "<td>" + data[j]["Num_cliente"] + "</td>" +
                            "<td>" + data[j]["Plaza"] + "</td>" +
                            "<td>" + data[j]["Num_Cta_Empresa"] + "</td>" +
                            "<td>" + data[j]["Clabe"] + "</td>" +
                            "<td class=''>" + data[j]["tipo_banco"] + "</td>" +
                            "<td class='text-center'><div><i class='fas fa-eye-slash text-danger'></i> </div></td>" +
                            "<td class='row'>" +
                            "<div title='Activar' class='ml-1 badge badge-pill badge-dark btn btn-sm' onclick='editarbanco(" + 3 + "," + data[j]["idBanco_Emp"] + ",\"" + collapse + "\"," + data[j]["Empresa_id"] + ");'><i class='far fa-check-circle fa-lg'></i></div>" +
                            //"<div title='Eliminar' class='ml-1 badge badge-pill badge-danger btn' onclick='editarbanco(" + 2 + "," + data[j]["idBanco_Emp"] + ",\"" + collapse + "\");'><i class='far fa-trash-alt fa-lg'></i></div>" +
                            "<div class='ml-1 badge badge-pill badge-info btn' onclick='mostrarmodaleditarbanco(" + data[j]["Empresa_id"] + "," + data[j]["Banco_id"] + "," + data[j]["tipo_banco_id"] + "," + data[j]["idBanco_Emp"] + ",\"" + collapse + "\");'><i class='fas fa-edit fa-lg'></i> Editar</div>" +
                            "</td>" +
                            "</tr>";
                    } else {
                        document.getElementById("tab" + collapse).innerHTML += "<tr>" +
                            "<td class=''>" + data[j]["Banco_id"] + "</td>" +
                            "<td>" + data[j]["Descripcion"] + "</td>" +
                            "<td>" + data[j]["Num_cliente"] + "</td>" +
                            "<td>" + data[j]["Plaza"] + "</td>" +
                            "<td>" + data[j]["Num_Cta_Empresa"] + "</td>" +
                            "<td>" + data[j]["Clabe"] + "</td>" +
                            "<td class=''>" + data[j]["tipo_banco"] + "</td>" +
                            "<td class='text-center'><div><i class='fas fa-eye text-primary fa-lg'></i></div></td>" +
                            "<td class='row'>" +
                            "<div title='Desactivar' class='ml-1 badge badge-pill badge-dark btn btn-sm' onclick='editarbanco(" + 1 + "," + data[j]["idBanco_Emp"] + ",\"" + collapse + "\"," + data[j]["Empresa_id"] + ");'><i class='far fa-times-circle fa-lg'></i></div>" +
                            //"<div title='Eliminar' class='ml-1 badge badge-pill badge-danger btn' onclick='editarbanco(" + 2 + "," + data[j]["idBanco_Emp"] + ",\"" + collapse + "\");'><i class='far fa-trash-alt fa-lg'></i></div>" +
                            "<div class='ml-1 badge badge-pill badge-info btn' onclick='mostrarmodaleditarbanco(" + data[j]["Empresa_id"] + "," + data[j]["Banco_id"] + "," + data[j]["tipo_banco_id"] + "," + data[j]["idBanco_Emp"] + ",\"" + collapse + "\");'><i class='fas fa-edit fa-lg'></i> Editar</div>" +
                            "</td>" +
                            "</tr>";
                    }

                }
                //$(".collapse").collapse("hide");
                //$("#" + collapse).collapse("toggle");
            }
        });
    }
    //
    // MOSTRAR MODAL NUEVO BANCO EN EMPRESA
    mostrarmodalnuevo = (Empresa_id) => {
        $.ajax({
            url: "../Catalogos/LoadTipoBanco",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var select = document.getElementById("newtipobanco");
                select.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    select.innerHTML += "<option value='" + data[i]["iId"] + "'>" + data[i]["sValor"] + "</option>";
                }
                $("#newempresa").val(Empresa_id);
                $("#modal-nuevo-bancoempresa").modal("show");

            }
        });


    }
    //
    // FUNCION QUE REINICIA EL MODAL NUEVO
    $('#modal-nuevo-bancoempresa').on('hidden.bs.modal', function () {
        document.getElementById("formnewbanco").classList.remove("was-validated");
    });
    //
    // GUARDAR NUEVO BANCO EN EMPRESA
    $("#btnnewbanco").on("click", function () {

        var form = document.getElementById("formnewbanco");
        if (form.checkValidity() === false) {
            form.classList.add("was-validated");
        } else {

            var cliente, plaza;

            if (document.getElementById("newcliente").value == "") {
                cliente = "0";
            }
            if (document.getElementById("newplaza").value == "") {
                plaza = "0";
            }

            var tb = document.getElementById("newtipobanco");
            $.ajax({
                url: "../Catalogos/SaveNewBanco",
                type: "POST",
                data: JSON.stringify({
                    Empresa_id: document.getElementById("newempresa").value
                    , Banco_id: document.getElementById("newbanco").value
                    , TipoBanco: tb.value
                    , Cliente: cliente
                    , Plaza: plaza
                    , CuentaEmp: document.getElementById("newcuentaempresa").value
                    , Clabe: document.getElementById("newclabe").value
                }),
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    if (data[0] == '0') {
                        Swal.fire({
                            title: 'Error!',
                            text: data[1],
                            icon: 'warning',
                            timer: 1000
                        });
                    } else {
                        //console.log(data);
                        $("#modal-nuevo-bancoempresa").modal("hide");
                        loadbancosempresa("collapsebancos", document.getElementById("newempresa").value);
                        Swal.fire({
                            title: 'Completo!',
                            text: data[1],
                            icon: 'success',
                            timer: 1000
                        });
                    }

                }
            });
        }
    });
    //
    // MOSTRAR MODAL EDITAR BANCO
    mostrarmodaleditarbanco = (Empresa_id, Banco_id, TipoBanco, BancoEmp, collapse) => {
        $.ajax({
            url: "../Empleados/LoadBanks",
            type: "POST",
            data: JSON.stringify({ keyban: 0 }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                $("#editempresa").val(Empresa_id)
                var select = document.getElementById("editbanco");
                select.innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    if (Banco_id == data[i]["iIdBanco"]) {
                        select.innerHTML += "<option value='" + data[i]["iIdBanco"] + "' selected>" + data[i]["sNombreBanco"] + "</option>"
                    } else {
                        //select.innerHTML += "<option value='" + data[i]["iIdBanco"] + "'>" + data[i]["sNombreBanco"] + "</option>"
                    }
                }
                $.ajax({
                    url: "../Catalogos/LoadTipoBanco",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: (data) => {
                        var select = document.getElementById("edittipobanco");
                        select.innerHTML = "";
                        for (var j = 0; j < data.length; j++) {
                            if (TipoBanco == data[j]["iId"]) {
                                select.innerHTML += "<option value='" + data[j]["iId"] + "' selected>" + data[j]["sValor"] + "</option>";
                            } else {
                                select.innerHTML += "<option value='" + data[j]["iId"] + "'>" + data[j]["sValor"] + "</option>";
                            }
                        }
                        
                        
                        $.ajax({
                            url: "../Catalogos/LoadBancosEmpresa",
                            type: "POST",
                            data: JSON.stringify({ Empresa_id: Empresa_id }),
                            contentType: "application/json; charset=utf-8",
                            success: (data) => {
                                for (var i = 0; i < data.length; i++) {
                                    if (data[i]["Empresa_id"] == Empresa_id && data[i]["Banco_id"] == Banco_id) {
                                        $("#editcliente").val(data[i]["Num_cliente"]);
                                        $("#editplaza").val(data[i]["Plaza"]);
                                        $("#editcuentaempresa").val(data[i]["Num_Cta_Empresa"]);
                                        $("#editclabe").val(data[i]["Clabe"]);
                                       
                                    }
                                }
                                $("#editarid").val(BancoEmp);
                                $("#editarcollapse").val(collapse);
                                $("#modal-editar-bancoempresa").modal("show");

                            }
                        });
                        
                    }
                });
            }
        });
    }
    //
    // GUARDAR ACTUALIZACION DE BANCO EN LA EMPRESA
    $("#btneditarbanco").on("click", function () {
        var form = document.getElementById("formeditbanco");
        if (form.checkValidity() === false) {
            form.classList.add("was-validated");
        } else {

            var cliente = "", plaza = "";
            if ($("#editcliente").val() == "") {
                cliente = "0";
            } else {
                cliente = $("#editcliente").val();
            }

            if ($("#editplaza").val() == "") {
                plaza = "0";
            } else {
                plaza = $("#editplaza").val();
            }

            var tb = document.getElementById("edittipobanco");
            $.ajax({
                url: "../Catalogos/UpdateBancoEmpresa",
                type: "POST",
                data: JSON.stringify({
                    Banco_id: $("#editbanco").val()
                    , TipoBanco: tb.value
                    , Id: $("#editarid").val()
                    , Cliente: cliente
                    , Plaza: plaza
                    , CuentaEmp: $("#editcuentaempresa").val()
                    , Clabe: $("#editclabe").val()
                }),
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    if (data[0] == '0') {
                        Swal.fire({
                            title: 'Error!',
                            text: data[1],
                            icon: 'danger'
                        });
                    } else if (data[0] == '1') {
                        loadbancosempresa("collapsebancos", $("#editempresa").val());
                        $("#modal-editar-bancoempresa").modal("hide");
                        Swal.fire({
                            title: 'Correcto!',
                            text: data[1],
                            icon: 'success',
                            timer: 1000
                        });
                    } else if (data[0] == '2') {
                        Swal.fire({
                            title: 'Advertencia!',
                            text: data[1],
                            icon: 'warning'
                        });
                    }
                }
            });
        }
    });
    //
    // FUNCION QUE REINICIA EL MODAL NUEVO
    $('#modal-editar-bancoempresa').on('hidden.bs.modal', function () {
        document.getElementById("formeditbanco").classList.remove("was-validated");
    });
    //
    // EDIT BANK TWO OPTIONS 
    editarbanco = (key, Id, collapse) => {
        var texto = "";
        if (key == 1) {
            texto = "El banco será desactivado de la empresa"
        } else if (key == 2) {
            texto = "El banco será eliminado de la empresa"
        } else if (key == 3) {
            texto = "El banco será activado en la empresa"
        }
        Swal.fire({
            title: 'Estas seguro?',
            text: texto,
            icon: 'warning',
            showCancelButton: true,
            CancelButtonText: 'Cancelar',
            confirmButtonColor: '#d33',
            cancelButtonColor: '#98959B',
            confirmButtonText: 'Confirmar'
        }).then((result) => {
            if (result.value) {

                $.ajax({
                    url: "../Catalogos/UpdateBanco",
                    type: "POST",
                    data: JSON.stringify({ key: key, Id: Id }),
                    contentType: "application/json; charset=utf-8",
                    success: (data) => {
                        //$("#" + collapse).collapse("toggle");
                        loadbancosempresa("collapsebancos", $("#newempresa").val());
                        if (data[0] == 0) {
                            Swal.fire({
                                icon: 'warning',
                                title: 'Error!',
                                text: data[1],
                                timer: 3000
                            });
                        } else if (data[0] == 1) {
                            Swal.fire({
                                icon: 'success',
                                title: 'Hecho!',
                                text: data[1],
                                timer: 3000
                            });
                        } else if (data[0] == 2) {
                            Swal.fire({
                                icon: 'success',
                                title: 'Hecho!',
                                text: data[1],
                                timer: 3000
                            });
                        } else if (data[0] == 3) {
                            Swal.fire({
                                icon: 'success',
                                title: 'Hecho!',
                                text: data[1],
                                timer: 3000
                            });
                        }
                    }
                });
            }
        });


    }

    // CLICK AL BOTON BUSCAR SUCURSAL 
    $("#btnbuscarsucursal").on("click", function () {
        $("#modalbuscarsucursal").modal("show");
    });
    //Busqueda de sucursales
    $("#inputsearchsucursal").on("keyup", function () {
        $("#inputsearchsucursal").empty();
        var txt = $("#inputsearchsucursal").val();
        if ($("#inputsearchsucursal").val() != "") {
            //var txtSearch = { "txtSearch": txt };
            $.ajax({
                url: "../SearchDataCat/SearchOffices",
                type: "POST",
                cache: false,
                data: JSON.stringify({ wordsearch: txt}),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: (data) => {

                    //console.log(data[0]["iFlag"]);
                    $("#resultSearchSucursales").empty();
                    if (data.length != 0) {
                        for (var i = 0; i < data.length; i++) {
                            $("#resultSearchSucursales").append("<div class='list-group-item list-group-item-action btnListEmpleados shadow-sm font-labels  font-weight-bold' onclick='seleccionaSuc(" + data[i]["iIdSucursal"] + ",\"" + data[i]["sClaveSucursal"] + " : " + data[i]["sDescripcionSucursal"] + "\")'> <i class='far fa-user-circle text-primary'></i> " + data[i]["sClaveSucursal"] + " : " + data[i]["sDescripcionSucursal"] + "</div>");
                        }
                    }
                    else {
                        $("#resultSearchSucursales").append("<button type='button' class='list-group-item list-group-item-action btnListEmpleados font-labels'  >" + "No se encontraron Sucursales" + "<br><small class='text-muted'>" + "Intente buscando con otra clave o nombre de sucursal" + "</small> </button>");
                    }
                }
            });
        } else {
            $("#resultSearchSucursales").empty();
        }
    });
    seleccionaSuc = (Id,Descripcion) => {
        $("#inZoInLocalidad").html("<option value='" + Id + "'>" + Descripcion + "</option>");
        $("#modalbuscarsucursal").modal("hide");
    }
    $('#modalbuscarsucursal').on('hidden.bs.modal', function (e) {
        $("#inputsearchsucursal").val("");
        $("#resultSearchSucursales").html("");
    });
    // mostrar nueva regional 
    $("#btnagregarnewregional").on("click", function() {
        $("#modalnuevaregional").modal("show");
    });
    // FUNCION QUE REINICIA EL MODAL NUEVO
    $('#modalnuevaregional').on('hidden.bs.modal', function () {
        $("#newinputclave").val("");
        $("#newinputdescripcion").val("");
    });
    // guardar la nueva regional 
    $("#btnnewregional").on("click", function () {
        var form = document.getElementById("frmnewregional");
        if (form.checkValidity() === false) {
            form.classList.add("was-validated");
        } else {

            $.ajax({
                url: "../Catalogos/SaveNewRegionales",
                type: "POST",
                cache: false,
                data: JSON.stringify({
                    ClaveRegion: $("#newinputclave").val()
                    , Descripcion: $("#newinputdescripcion").val()
                }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    if (data[0] == '0') {
                        Swal.fire({
                            title: 'Error!',
                            text: data[1],
                            icon: 'warning',
                            timer: 1000
                        });
                    } else {
                        SelectLoaderFromLocalidades();
                        $("#modalnuevaregional").modal("hide");
                        Swal.fire({
                            title: 'Completo!',
                            text: data[1],
                            icon: 'success',
                            timer: 1000
                        });
                    }
                }
            });
        }
    });
});
