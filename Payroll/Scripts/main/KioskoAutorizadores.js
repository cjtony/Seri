$(function () {

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

                    $("#resultSearchEmpleados").empty();
                    if (data[0]["iFlag"] == 0) {
                        for (var i = 0; i < data.length; i++) {
                            $("#resultSearchEmpleados").append("<div class='list-group-item list-group-item-action btnListEmpleados font-labels font-weight-bold py-1' onclick='AddEmpleadosAutorizados(" + data[i]["IdEmpleado"] + ");'> <i class='far fa-user-circle text-primary'></i> " + data[i]["Apellido_Paterno_Empleado"] + ' ' + data[i]["Apellido_Materno_Empleado"] + "  " + data[i]["Nombre_Empleado"] + "  -   <small class='text-muted'><i class='fas fa-briefcase text-warning'></i> " + data[i]["DescripcionDepartamento"] + "</small> - <small class='text-muted'>" + data[i]["DescripcionPuesto"] + "</small></div>");
                        }
                    }
                    else {
                        $("#resultSearchEmpleados").append("<button type='button' class='list-group-item list-group-item-action btnListEmpleados font-labels'>" + data[0]["Nombre_Empleado"] + "<br><small class='text-muted'>" + data[0]["DescripcionPuesto"] + "</small> </button>");
                    }
                }
            });
        } else {
            $("#resultSearchEmpleados").empty();
        }
    });

    AddEmpleadosAutorizados = (idE) => {
        $.ajax({
            url: "../Empleados/AddEmpleadosAutorizados",
            type: "POST",
            data: JSON.stringify({ "Empleado_id": idE }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                if (data[0] == "0") {
                    Swal.fire({
                        icon: 'success',
                        title: 'Correcto!',
                        text: data[1],
                        timer: 1000
                    });
                    $("#inputSearchEmpleados").val("");
                    $("#resultSearchEmpleados").empty();
                    cargarAutorizadores();
                } else if (data[0] == "1") {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Error!',
                        text: data[1],
                        timer: 1000
                    });
                }
            }
        });
    }

    cargarAutorizadores = () => {
        document.getElementById("autorizadores-body").innerHTML = ""
        $.ajax({
            url: "../Empleados/LoadEmpleadosAutorizados",
            type: "POST",
            data: JSON.stringify(),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                console.log(data);
                var btns = "";
                for (var i = 0; i < data.length; i++) {

                    btns += "<div class='alert alert-dark alert-dismissible fade show mx-auto' role='alert'>" +
                        "<div class='font-labels font-weight-bold'>" +
                        "<i class='fas fa-hashtag text-danger'></i> "+ data[i]["Empleado_id"] +" <br />" +
                        "<i class='fas fa-user text-primary'></i>  "+ data[i]["Nombre"] +" <br />" +
                        "<i class='fas fa-briefcase text-primary'></i> " + data[i]["Puesto"] +
                        "</div>" +
                        "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                        "<span aria-hidden='true'>&times;</span>" +
                        "</button>" +
                        "</div>";
                }
                document.getElementById("autorizadores-body").innerHTML += btns;
            }
        });
    }
    cargarAutorizadores();

    deleteAutorizadores = () => {

    }




});