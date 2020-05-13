$(function () {
    /*CARGA LA TABLA DE INCIDENCIAS PROGRAMADAS*/
    $.ajax({
        method: "POST",
        url: "../Incidencias/LoadIncidenciasProgramadas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: (data) => {
            console.log(data);
            document.getElementById("tabIncProg").innerHTML = "";
            for (var i = 0; i < data.length; i++) {
                console.log(i);
                console.log(data[i]['Renglon']);
                document.getElementById("tabIncProg").innerHTML += "<tr><td>" + data[i]["Id"] + "</td><td>" + data[i]["Nombre_Empleado"] + data[i]["Apellido_Paterno_Empleado"] + data[i]["Apellido_Materno_Empleado"] + "</td><td>" + data[i]["Empleado_id"] + "</td><td>" + data[i]["Nombre_Renglon"] + "</td><td>" + data[i]["Renglon_id"] + "</td><td>" + data[i]["Monto_aplicar"] + "</td><td>" + data[i]["Numero_dias"] + "</td><td><button type='button' class='btn btn-success btn-sm' tittle=''><i class='fas fa-pen-square'></i></button><button type='button' class='btn btn-danger btn-sm' tittle='Eliminar incidencia'><i class='far fa-trash-alt'></i></button></td></tr>";
                $(".table").DataTable();
            }
        }
    });
    //alert(document.getElementById("lblPeriodoId"));

});