$(function () {

    /*
     * Constantes
     */

    /*
     * Funciones
     */
    validarVacaciones = (tipoValidacion, Solicitud_id) => {
        if (tipoValidacion == 0) {
            $("#titulo-modal-respuesta").html("<div class=''>Autorizar Vacaciones</div>");
            $("#modalRespuesta").addClass("bg-success");
            $("#btnGuardarSolicitud").attr("onclick", "cerrarSolicitud(0, " + Solicitud_id + ");");
        } else {
            $("#titulo-modal-respuesta").html("<div class=''>NO Autorizar Vacaciones</div>");
            $("#modalRespuesta").addClass("bg-danger");
            $("#btnGuardarSolicitud").attr("onclick", "cerrarSolicitud(1, " + Solicitud_id + ");");
        }
        $("#modal-respuesta").modal("show");
    }

    loadSolicitudesSinAprobar = () => {
        $.ajax({
            url: "../KioskoM/getSolicitudesSinAprobar",
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var blocks = "";
                if (data.lenght < 1) {
                    blocks =
                        "<div class='alert alert-warning alert-dismissible fade show' role='alert'>" +
                        "<strong> No tiene solicitudes pendientes por aprobar! </strong>" +
                        "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                        "<span aria-hidden='true'>& times;</span>" +
                        "</button>" +
                        "</div >";
                    document.getElementById("solicitudesSinAprobar").innerHTML = blocks;
                } else {
                    for (var i = 0; i < data.length; i++) {

                        var block =
                            "<button type='button' class='list-group-item list-group-item-action shadow'>" +
                            "<div class='row'>" +
                            "<div class='col-md-10'>" +
                            "<div class='d-flex w-100 justify-content-between'>" +
                            "<h5 class='mb-1 font-weight-bold'><span class='text-info font-weight-bold'>" + data[i][0] + "</span>&nbsp;" + data[i][1] + "</h5>" +
                            "</div>" +
                            "<div class='col-md-12 m-0 p-0 h6'> " + data[i][2] + " a " + data[i][3] + " - " + data[i][4] + " DIAS</div>" +
                            "<div class='col-md-12 m-0 p-0 font-labels'><span class='m-0 p-0'>Empresa:</span>&nbsp;<small class='m-0 p-0 h6 small text-black font-weight-bold'>" + data[i][5] + "&nbsp;" + data[i][6] + "</small></div>" +
                            "<div class='col-md-12 m-0 p-0 font-labels'><span class='m-0 p-0'>Puesto:</span>&nbsp;<small class='m-0 p-0 h6 small'>" + data[i][7] + "</small></div>" +
                            "<div class='col-md-12 m-0 p-0 font-labels'><span class='m-0 p-0'>Departamento:</span>&nbsp;<small class='m-0 p-0 h6 small'>" + data[i][8] + "</small></div>" +
                            "</div>" +
                            "<div class='col-md-2 align-self-center'>" +
                            "<div class='btn btn-outline-success btn-block align-self-center' onclick='validarVacaciones(0, " + data[i][9] + "); '>" +
                            "<i class='fas fa-thumbs-up'></i>" +
                            "</div>" +
                            "<div class='btn btn-outline-danger btn-block align-self-center' onclick='validarVacaciones(1, " + data[i][9] + "); '>" +
                            "<i class='fas fa-thumbs-down'></i>" +
                            "</div>" +
                            "</div>" +
                            "</div>" +
                            "</button>";

                        blocks += block;
                    }
                    document.getElementById("solicitudesSinAprobar").innerHTML = blocks;
                }
            }
        });
    }

    loadSolicitudesAprobadas = () => {
        $.ajax({
            url: "../KioskoM/getSolicitudesAprobadas",
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var blocks = "";
                if (data.lenght < 1) {
                    blocks =
                        "<div class='alert alert-warning alert-dismissible fade show' role='alert'>" +
                        "<strong> No tiene solicitudes pendientes por aprobar! </strong>" +
                        "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                        "<span aria-hidden='true'>& times;</span>" +
                        "</button>" +
                        "</div >";
                    document.getElementById("solicitudesAprobadas").innerHTML = blocks;
                } else {
                    for (var i = 0; i < data.length; i++) {

                        var block =
                            "<button type='button' class='list-group-item list-group-item-action shadow'>" +
                            "<div class='row'>" +
                            "<div class='col-md-10'>" +
                            "<div class='d-flex w-100 justify-content-between'>" +
                            "<h5 class='mb-1 font-weight-bold'><span class='text-info font-weight-bold'>" + data[i][0] + "</span>&nbsp;" + data[i][1] + "</h5>" +
                            "</div>" +
                            "<div class='col-md-12 m-0 p-0 h6'> " + data[i][2] + " a " + data[i][3] + " - " + data[i][4] + " DIAS</div>" +
                            "<div class='col-md-12 m-0 p-0 font-labels'><span class='m-0 p-0'>Empresa:</span>&nbsp;<small class='m-0 p-0 h6 small text-black font-weight-bold'>" + data[i][5] + "&nbsp;" + data[i][6] + "</small></div>" +
                            "<div class='col-md-12 m-0 p-0 font-labels'><span class='m-0 p-0'>Puesto:</span>&nbsp;<small class='m-0 p-0 h6 small'>" + data[i][7] + "</small></div>" +
                            "<div class='col-md-12 m-0 p-0 font-labels'><span class='m-0 p-0'>Departamento:</span>&nbsp;<small class='m-0 p-0 h6 small'>" + data[i][8] + "</small></div>" +
                            "</div>" +
                            "<div class='col-md-2 rounded row align-items-center'>" +
                            "<div class='col-md-12 text-center'>" +
                            "<i class='fas fa-check text-info fa-5x'></i>" +
                            "</div>" +
                            "</div>" +
                            "</div>" +
                            "</button>";

                        blocks += block;
                    }
                    document.getElementById("solicitudesAprobadas").innerHTML = blocks;
                }
            }
        });
    }

    loadSolicitudesRechazadas = () => {
        $.ajax({
            url: "../KioskoM/getSolicitudesRechazadas",
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var blocks = "";
                if (data.lenght < 1) {
                    blocks =
                        "<div class='alert alert-warning alert-dismissible fade show' role='alert'>" +
                        "<strong> No tiene solicitudes pendientes por aprobar! </strong>" +
                        "<button type='button' class='close' data-dismiss='alert' aria-label='Close'>" +
                        "<span aria-hidden='true'>& times;</span>" +
                        "</button>" +
                        "</div >";
                    document.getElementById("solicitudesRechazadas").innerHTML = blocks;
                } else {
                    for (var i = 0; i < data.length; i++) {

                        var block =
                            "<button type='button' class='list-group-item list-group-item-action shadow'>" +
                            "<div class='row'>" +
                            "<div class='col-md-10'>" +
                            "<div class='d-flex w-100 justify-content-between'>" +
                            "<h5 class='mb-1 font-weight-bold'><span class='text-info font-weight-bold'>" + data[i][0] + "</span>&nbsp;" + data[i][1] + "</h5>" +
                            "</div>" +
                            "<div class='col-md-12 m-0 p-0 h6'> " + data[i][2] + " a " + data[i][3] + " - " + data[i][4] + " DIAS</div>" +
                            "<div class='col-md-12 m-0 p-0 font-labels'><span class='m-0 p-0'>Empresa:</span>&nbsp;<small class='m-0 p-0 h6 small text-black font-weight-bold'>" + data[i][5] + "&nbsp;" + data[i][6] + "</small></div>" +
                            "<div class='col-md-12 m-0 p-0 font-labels'><span class='m-0 p-0'>Puesto:</span>&nbsp;<small class='m-0 p-0 h6 small'>" + data[i][7] + "</small></div>" +
                            "<div class='col-md-12 m-0 p-0 font-labels'><span class='m-0 p-0'>Departamento:</span>&nbsp;<small class='m-0 p-0 h6 small'>" + data[i][8] + "</small></div>" +
                            "</div>" +
                            "<div class='col-md-2 align-self-center'>" +
                            "<div class='col-md-12 text-center'>" +
                            "<i class='fas fa-times text-danger fa-5x'></i>" +
                            "</div>" +
                            "</div>" +
                            "</div>" +
                            "</button>";

                        blocks += block;
                    }
                    document.getElementById("solicitudesRechazadas").innerHTML = blocks;
                }
            }
        });
    }

    /*
     * Ejecucion funciones
     */
    $('#modal-respuesta').on('hidden.bs.modal', function (e) {
        $("#modalRespuesta").removeClass("bg-success");
        $("#modalRespuesta").removeClass("bg-danger");
    });
});