$(function () {
    //var ren_incidencia = document.getElementById("inRenglon");
    //var concepto_incidencia = document.getElementById("inConcepto_incidencia");

    $("#modalLiveSearchEmpleado").modal("show");

    //Eventos 
    $("#btnSaveCredito").on("click", function () {
        var tdescuento = document.getElementById("inTipoDescuento");
        var aseguro = document.getElementById("inAplicaSeguro");
        var descuento = document.getElementById("inDescuento");
        var ncredito = document.getElementById("inNoCredito");
        var fechaa = document.getElementById("inFechaAprovacionCredito");
        var descontar = document.getElementById("inDescontar");
        var fechab = document.getElementById("inFechaBajaCredito");
        var fechar = document.getElementById("inFechaReinicioCredito");
        var aseg;
        if ($("#inAplicaSeguro").is(":checked")) {
            aseg = 1;
        } else {
            aseg = 0;
        }
        $.ajax({
            url: "../Incidencias/SaveCredito",
            data: JSON.stringify({
                TipoDescuento: tdescuento.value,
                SeguroVivienda: aseg,
                Descuento: descuento.value,
                NoCredito: ncredito.value,
                FechaAprovacion: fechaa.value,
                Descontar: descontar.value,
                FechaBaja: fechab.value,
                FechaReinicio: fechar.value
            }),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                //Refresca la tabla
                //$.ajax({
                //    method: "POST",
                //    url: "../Incidencias/LoadCreditosEmpleado",
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: (data) => {
                //        document.getElementById("tcbody").innerHTML = "";
                //        for (var i = 0; i < data.length; i++) {
                //            document.getElementById("tcbody").innerHTML += "<tr><td>" + data[i]["NoCredito"] + "</td><td>" + data[i]["TipoDescuento"] + "</td><td>" + data[i]["Descuento"] + "</td><td>" + data[i]["FechaBaja"] + "</td><td><div class='btn badge badge-danger'><i class='far fa-check-circle' tittle='Desactivar' ></i></div></td></tr>";
                //        }
                //    }
                //});
                createTab();


                Swal.fire({
                    icon: 'success',
                    title: 'Completado!',
                    text: 'Credito agregado con éxito'
                });
            }
        });


    });
    //Busqueda de empleado
    $("#inputSearchEmpleados").on("keyup", function () {
        $("#inputSearchEmpleados").empty();
        var txt = $("#inputSearchEmpleados").val();
        if ($("#inputSearchEmpleados").val() != "") {
            var txtSearch = { "txtSearch": txt };
            $.ajax({
                url: "../Empleados/SearchEmpleados",
                type: "POST",
                cache: false,
                data: JSON.stringify(txtSearch),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: (data) => {

                    console.log(data[0]["iFlag"]);
                    $("#resultSearchEmpleados").empty();
                    if (data[0]["iFlag"] == 0) {
                        for (var i = 0; i < data.length; i++) {
                            $("#resultSearchEmpleados").append("<div class='list-group-item list-group-item-action btnListEmpleados font-labels  font-weight-bold' onclick='MostrarDatosEmpleado(" + data[i]["IdEmpleado"] + ")'> <i class='far fa-user-circle text-primary'></i> " + data[i]["Nombre_Empleado"] + " " + data[i]["Apellido_Paterno_Empleado"] + ' ' + data[i]["Apellido_Materno_Empleado"] + "   -   <small class='text-muted'><i class='fas fa-briefcase text-warning'></i> " + data[i]["DescripcionDepartamento"] + "</small> - <small class='text-muted'>" + data[i]["DescripcionPuesto"] + "</small></div>");
                        }
                    }
                    else {
                        $("#resultSearchEmpleados").append("<button type='button' class='list-group-item list-group-item-action btnListEmpleados font-labels'  >" + data[0]["Nombre_Empleado"] + "<br><small class='text-muted'>" + data[0]["DescripcionPuesto"] + "</small> </button>");
                    }
                }
            });
        } else {
            $("#resultSearchEmpleados").empty();
        }
    });
    //VALIDACION DE CAMPOS NUMERICOS
    $('.input-number').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    //Funciones
    MostrarDatosEmpleado = (idE) => {
        var txtIdEmpleado = { "IdEmpleado": idE };
        $.ajax({
            url: "../Empleados/SearchEmpleado",
            type: "POST",
            data: JSON.stringify(txtIdEmpleado),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                document.getElementById("EmpDes").innerHTML = "<i class='far fa-user-circle text-primary'></i> " + data[0]["Nombre_Empleado"] + " " + data[0]["Apellido_Paterno_Empleado"] + ' ' + data[0]["Apellido_Materno_Empleado"] + "   -   <small class='text-muted'> " + data[0]["DescripcionDepartamento"] + "</small> - <small class='text-muted'>" + data[0]["DescripcionPuesto"] + "</small>";
                $("#modalLiveSearchEmpleado").modal("hide");
                createTab();
            }

        });

    }
    createTab = () => {
        $.ajax({
            method: "POST",
            url: "../Incidencias/LoadCreditosEmpleado",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (data) => {
                //console.log(data["data"][0]["Empresa_id"]);
                console.log(data[0]["Empresa_id"]);
                //$("#tcbody").empty();
                document.getElementById("tcbody").innerHTML = "";
                for (var i = 0; i < data.length; i++) {
                    document.getElementById("tcbody").innerHTML += "<tr><td>" + data[i]["NoCredito"] + "</td><td>" + data[i]["TipoDescuento"] + "</td><td>" + data[i]["Descuento"] + "</td><td>" + data[i]["FechaBaja"] + "</td><td><div class='btn badge badge-danger text-center' data-toggle='tooltip' data-placement='left' title='Desactivar'><i class='fas fa-minus'></i> </div></td></tr>";
                }


            }
        });

    }

});