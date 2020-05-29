$(function () {
    $("#pills-catalogos.nav-link.active").addClass('bg-secondary');
    $("#pills-catalogos.nav-link").on("click", function () {
        $("#pills-catalogos.nav-link").removeClass('bg-secondary');
        $(this).addClass('bg-secondary');
    });
    $(".btntab").hover(
        function () {
            $(this).children('.btntittle').removeClass('bg-primary').addClass('bg-dark'); /*.css("background-color", "pink");*/
            //$(this).removeClass('bg-white').addClass('bg-secondary');
            //alert('si');
        },
        function () {
            $(this).children('.btntittle').removeClass('bg-dark').addClass('bg-primary'); /*.css("background-color", "pink");*/
            //$(this).removeClass('bg-secondary').addClass('bg-white');
            //alert('no');
        }
    );
    $("#fechas-periodo").on("click", function () {
        $("#v-pills-FechasPeriodos-tab").click();
    });
    $("#politicas-vacaciones").on("click", function () {
        $("#v-pills-politicas-tab").click();
    });
    $("#v-pills-FechasPeriodos-tab").on("click", function () {
        LoadTabFechasPeriodos();
    });
    $("#v-pills-settings-tab").on("click", function (evt) {
        
        if ($("#lblEdicion").html() == '') {
            evt.preventDefault();
            Swal.fire({
                icon: 'warning',
                title: 'Atención!',
                text: 'Debe seleccionar una empresa dentro de un catalogo para editar antes de entrar'
            });
            $("#v-pills-lista-tab").click();
        } else {
            //Swal.fire({
            //    icon: 'success',
            //    title: 'Completado!',
            //    text: 'Oh yeah mother fucker'
            //});
        }
    });
    
    LoadTabFechasPeriodos = () => {
        $.ajax({
            url: "../Catalogos/LoadFechasPeriodos",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                
                var tab = document.getElementById("bodytab-fechas-periodos");
                tab.innerHTML = "";
                var empresa;
                for (var i = 0; i < data.length; i++) {
                    if (i == 0) {
                        empresa = data[i]["Empresa_id"];
                    }
                    if (data[i]["Empresa_id"] == empresa && data[i]["Periodo"] == 1) {
                        console.log(data[i]["Empresa_id"] + " - " + data[i]["Periodo"]);
                        console.log(data[i]);
                        tab.innerHTML += "" +
                            "<tr>" +
                            "<td colspan='3' >" +
                            "<div class='col-md-12'>" +
                            "<label>" + data[i]['Empresa_id'] + " " + data[i]['NombreEmpresa'] + " - " + data[i]['Tipo_Periodo_Id'] + " " + data[i]["DescripcionTipoPeriodo"] + " - Quincenal</label>&nbsp;&nbsp;&nbsp;<div class='badge badge-success btn' onclick='LoadDetalleFechasPeriodo(\"collapse-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ");'>Ver <i class='fas fa-plus'></i></div><div class='btn badge badge-info ml-2' onclick='editarFechasPeriodos(" + data[i]["Empresa_id"] +");'>Editar <i class='fas fa-edit'></i></div>" +
                            "<div id='collapse-"+data[i]["NombreEmpresa"]+"' class='collapse collapse-" + data[i]['NombreEmpresa'] + " col-md-12'>" +
                            "</div>" +
                            "</div>" +
                            "</td >" +
                            "</tr >";
                        
                    } else {
                        empresa = data[i]["Empresa_id"];
                        if (data[i]["Empresa_id"] == empresa && data[i]["Periodo"] == 1) {
                            //console.log(data[i]["Empresa_id"] + " - " + data[i]["Periodo"]);
                            tab.innerHTML += "" +
                                "<tr>" +
                                "<td colspan='3' >" +
                                "<div class='col-md-12'>" +
                                "<label>" + data[i]['Empresa_id'] + " " + data[i]['NombreEmpresa'] + " - " + data[i]['Tipo_Periodo_Id'] + " " + data[i]["DescripcionTipoPeriodo"] + " - Quincenal</label>&nbsp;&nbsp;&nbsp;<div class='badge badge-success btn' onclick='LoadDetalleFechasPeriodo(\"collapse-" + data[i]["NombreEmpresa"] + "\", " + data[i]["Empresa_id"] + ");'>Ver <i class='fas fa-plus'></i></div><div class='btn badge badge-info ml-2' onclick='editarFechasPeriodos(" + data[i]["Empresa_id"] +");'>Editar <i class='fas fa-edit'></i></div>" +
                                "<div id='collapse-" + data[i]["NombreEmpresa"] + "' class='collapse collapse-" + data[i]['NombreEmpresa'] + " col-md-12'>" +
                                "</div>" +
                                "</div>" +
                                "</td >" +
                                "</tr >";
                        }
                    }
                }
                //$("#v-pills-FechasPeriodos-tab").click();
            }
        });
    }
    
    LoadDetalleFechasPeriodo = (pilltab,Empresa_id) => {
        $.ajax({
            url: "../Catalogos/LoadFechasPeriodosDetalle",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                console.log(pilltab);
                document.getElementById(pilltab).innerHTML = "";
                document.getElementById(pilltab).innerHTML += "<table class='table table-sm table-in-fechas-periodos col-md-12'>" +
                    "<thead class='col-md-12'>" +
                    "<tr>" +
                    "<th class='col-md-2'>No. Periodo</th>" +
                    "<th class='col-md-2'>Fecha Inicio</th>" +
                    "<th class='col-md-2'>Fecha Final</th>" +
                    "<th class='col-md-2'>Fecha Proceso</th>" +
                    "<th class='col-md-2'>Fecha Pago</th>" +
                    "<th class='col-md-2'>Días Efectivos</th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='tab" + pilltab + "'></tbody>" + "</table>";
                for (var j = 0; j < data.length; j++) {

                    document.getElementById("tab" + pilltab).innerHTML += "<tr>" +
                            "<td class='col-md-2'>" + data[j]["Periodo"] + "</td>" +
                            "<td class='col-md-2'>" + data[j]["Fecha_Inicio"] + "</td>" +
                            "<td class='col-md-2'>" + data[j]["Fecha_Final"] + "</td>" +
                            "<td class='col-md-2'>" + data[j]["Fecha_Proceso"] + "</td>" +
                            "<td class='col-md-2'>" + data[j]["Fecha_Pago"] + "</td>" +
                            "<td class='col-md-2'>" + data[j]["Dias_Efectivos"] + "</td>" +
                        "</tr>";
                }
                
                $("#" + pilltab).collapse("toggle");
            }

        });
    }

    editarFechasPeriodos = (Empresa_id) => {

        $.ajax({
            url: "../Catalogos/LoadFechasPeriodosDetalle",
            type: "POST",
            data: JSON.stringify({ Empresa_id: Empresa_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                $("#lblEdicion").html("Fechas - Periodos");
                $("#Empresa_id_editar_fecha_periodo").val(data[0]["Empresa_id"]);
                $("#bodybotonagregar").html("<div class='btn btn-info btn-sm col-md-12 font-label' onclick='addRegistroFechasPeriodos();'>Agregar Registro</div>");
                $("#bodyEditarModulo").html(
                    "<table class='table table-sm'>" +
                    "<thead>" +
                    "<tr>" +
                    "<th scope='col'>Año</th>" +
                    "<th scope='col'>Tipo Periodo</th>" +
                    "<th scope='col'>Periodo</th>" +
                    "<th scope='col'>Inicio</th>" +
                    "<th scope='col'>Final</th>" +
                    "<th scope='col'>Proceso</th>" +
                    "<th scope='col'>Pago</th>" +
                    "<th scope='col'>Dias de pago</th>" +
                    "<th scope='col'>Acciones</th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='bodytabFechasPeriodos'>" +
                    "</tbody>" +
                    "</table>");
                document.getElementById("bodytabFechasPeriodos").innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    document.getElementById("bodytabFechasPeriodos").innerHTML += "" +
                        "<tr>" +
                        "<td>" + data[i]["Anio"] + "</td>" +
                        "<td>" + data[i]["Tipo_Periodo_Id"] + "</td>" +
                        "<td>" + data[i]["Periodo"] + "</td>" +
                        "<td>" + data[i]["Fecha_Inicio"] + "</td>" +
                        "<td>" + data[i]["Fecha_Final"] + "</td>" +
                        "<td>" + data[i]["Fecha_Proceso"] + "</td>" +
                        "<td>" + data[i]["Fecha_Pago"] + "</td>" +
                        "<td>" + data[i]["Dias_Efectivos"] + "</td>" +
                        "<td><div class='badge badge-success btn mx-1' title='Editar' onclick='cargaModalEditar(" + data[i]["id"] + ");'> <i class='fas fa-edit'></i></div><div class='badge badge-danger btn mx-1' title='Eliminar' onclick='eliminarFechaPeriodo(" + data[i]["id"] + ");'><i class='fas fa-minus'></i></div></td>" +
                        "</tr>";
                }

                $("#v-pills-editar-tab").click();
            }
        });


        
    }

    addRegistroFechasPeriodos = () => {
        //Swal.fire({
        //    icon: 'success',
        //    title: 'Se va agregar!',
        //    timer: '1000'
        //});
        llenaMinAnios();
        $("#modalAgregarFechaPeriodo").modal("show");
    }
    llenaMinAnios = () => {
        var ano = (new Date).getFullYear();
        $("#inano").attr("min", ano);
        $("#inano").val(ano);
    }
    // Reinicia el formulario de agregado de fechas periodo
    $('#modalAgregarFechaPeriodo').on('hidden.bs.modal', function (e) {
        document.getElementById("frmAddFechasPeriodos").reset();
    });

    // Guardar Fecha - Periodo
    $("#btnsavefechaperiodo").on("click", function () {

        var form = document.getElementById("frmAddFechasPeriodos");
        if (form.checkValidity() === false) {

            form.classList.add("was-validated");
            
        } else {
            var inano = document.getElementById("inano");
            var inperiodo = document.getElementById("inperiodo");
            var infinicio = document.getElementById("infinicio");
            var inffinal = document.getElementById("inffinal");
            var infproceso = document.getElementById("infproceso");
            var infpago = document.getElementById("infpago");
            var indiaspago = document.getElementById("indiaspago");
            var inEmpresa_id = document.getElementById("Empresa_id_editar_fecha_periodo");
            $.ajax({
                url: "../Catalogos/SaveNewPeriodo",
                type: "POST",
                data: JSON.stringify({
                    inEmpresa_id: inEmpresa_id.value,
                    inano: inano.value,
                    inperiodo: inperiodo.value,
                    infinicio: infinicio.value,
                    inffinal: inffinal.value,
                    infproceso: infproceso.value,
                    infpago: infpago.value,
                    indiaspago: indiaspago.value
                }),
                contentType: "application/json; charset=utf-8",
                beforeSend: () => {
                    form.classList.add("was-validated");
                },
                success: (data) => {
                    console.log(data);
                    if (data[0] == '0') {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Error!',
                            text: data[1],
                            timer: 3000
                        });
                    } else {
                        Swal.fire({
                            icon: 'success',
                            title: 'Correcto!',
                            text: data[1],
                            timer: 1500
                        });
                        editarFechasPeriodos(inEmpresa_id.value);
                        $("#modalAgregarFechaPeriodo").modal("hide");
                        document.getElementById("frmAddFechasPeriodos").reset();
                    }


                }
            });
        }
    });


    // Eliminar una Fecha - Periodo
    eliminarFechaPeriodo = (id) => {
        Swal.fire({
            title: 'Estas seguro?',
            text: "El registro sera borrado definitivamente!",
            icon: 'warning',
            showCancelButton: true,
            CancelButtonText: 'Cancelar',
            confirmButtonColor: '#d33',
            cancelButtonColor: '#98959B',
            confirmButtonText: 'Confirmar'
        }).then((result) => {
            if (result.value) {
                var Empresa_id = document.getElementById("Empresa_id_editar_fecha_periodo");
                $.ajax({
                    url: "../Catalogos/DeletePeriodo",
                    type: "POST",
                    data: JSON.stringify({
                        Empresa_id: Empresa_id.value,
                        Id: id
                    }),
                    contentType: "application/json; charset=utf-8",
                    success: (data) => {
                        if (data[0] == 0) {
                            Swal.fire({
                                title: 'Error!',
                                icon: 'warning',
                                text: data[1],
                                timer: 3000
                            });
                        } else {
                            editarFechasPeriodos(Empresa_id.value);
                            Swal.fire({
                                title: 'Correcto!',
                                icon: 'success',
                                text: data[1],
                                timer: 1500
                            });        
                        }
                    }
                });
            }
        });



        
    }
});