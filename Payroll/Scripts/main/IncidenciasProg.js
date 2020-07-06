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
                //console.log(i);
                //console.log(data[i]['Renglon']);
                document.getElementById("tabIncProg").innerHTML += "<tr><td>" + data[i]["Id"] + "</td><td>" + data[i]["Nombre_Empleado"] + "&nbsp;" + data[i]["Apellido_Paterno_Empleado"] + "&nbsp;" + data[i]["Apellido_Materno_Empleado"] + "</td><td>" + data[i]["Empleado_id"] + "</td><td>" + data[i]["Nombre_Renglon"] + "</td><td>" + data[i]["Renglon_id"] + "</td><td>" + data[i]["Monto_aplicar"] + "</td><td>" + data[i]["Numero_dias"] + "</td><td><button type='button' class='btn btn-sm badge badge-warning' tittle='Desactivar' ><i class='fas fa-eye-slash'></i></button></td></tr>";
            }
            $(".table").DataTable({
                language: {
                    search: 'Buscar',
                    paginate: {
                        first: 'Primero',
                        previous: 'Anterior',
                        next: 'Siguiente',
                        last: 'Ultimo'
                    }
                }
            });
        }
    });
    //alert(document.getElementById("lblPeriodoId"));

});