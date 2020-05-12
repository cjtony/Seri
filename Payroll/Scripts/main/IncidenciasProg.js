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
                document.getElementById("tabIncProg").innerHTML += "<tr><td>" + data[i]["Renglon"] + "</td><td>" + data[i]["Cantidad"] + "</td><td>" + data[i]["Plazos"] + "</td><td>" + data[i]["Leyenda"] + "</td><td>" + data[i]["Fecha_Aplicacion"] + "</td><td><button type='button' class='btn btn-success btn-sm' tittle=''><i class='fas fa-pen-square'></i></button><button type='button' class='btn btn-danger btn-sm' tittle='Eliminar incidencia'><i class='far fa-trash-alt'></i></button></td></tr>";
            }
        }
    });
    alert(document.getElementById("lblPeriodoId"));

});