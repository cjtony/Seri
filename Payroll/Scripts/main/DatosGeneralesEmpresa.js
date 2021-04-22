// patron del RFC, persona moral
_rfc_pattern_pm = "^(([A-ZÑ&]{3})([0-9]{2})([0][13578]|[1][02])(([0][1-9]|[12][\\d])|[3][01])([A-Z0-9]{3}))|" +
    "(([A-ZÑ&]{3})([0-9]{2})([0][13456789]|[1][012])(([0][1-9]|[12][\\d])|[3][0])([A-Z0-9]{3}))|" +
    "(([A-ZÑ&]{3})([02468][048]|[13579][26])[0][2]([0][1-9]|[12][\\d])([A-Z0-9]{3}))|" +
    "(([A-ZÑ&]{3})([0-9]{2})[0][2]([0][1-9]|[1][0-9]|[2][0-8])([A-Z0-9]{3}))$";
// patron del RFC, persona fisica
_rfc_pattern_pf = "^(([A-ZÑ&]{4})([0-9]{2})([0][13578]|[1][02])(([0][1-9]|[12][\\d])|[3][01])([A-Z0-9]{3}))|" +
    "(([A-ZÑ&]{4})([0-9]{2})([0][13456789]|[1][012])(([0][1-9]|[12][\\d])|[3][0])([A-Z0-9]{3}))|" +
    "(([A-ZÑ&]{4})([02468][048]|[13579][26])[0][2]([0][1-9]|[12][\\d])([A-Z0-9]{3}))|" +
    "(([A-ZÑ&]{4})([0-9]{2})[0][2]([0][1-9]|[1][0-9]|[2][0-8])([A-Z0-9]{3}))$";
//
var codpost = document.getElementById('inCodigo_postal');
var city = document.getElementById('inCiudad_empresa');
var colony = document.getElementById('inColonia_empresa');
var municipio = document.getElementById("inMunicipio_empresa");
var states = document.getElementById("inEstado_empresa");
var btnvalidacpstate = document.getElementById("btnValidaCpEstado");

var edcodpost = document.getElementById('edCodigo_postal');
var edcity = document.getElementById('edCiudad_empresa');
var edcolony = document.getElementById('edColonia_empresa');
var edmunicipio = document.getElementById("edMunicipio_empresa");
var edstates = document.getElementById("edEstado_empresa");
var edbtnvalidacpstate = document.getElementById("btnValidaCpEstadoed");
$(document).ready(function () {
    btnvalidacpstate.disabled = true;
    city.disabled = true;
    municipio.disabled = true;
    states.disabled = true;
    colony.disabled = true;

    edbtnvalidacpstate.disabled = true;
    edcity.disabled = true;
    edmunicipio.disabled = true;
    edstates.disabled = true;
    edcolony.disabled = true;

    //EVENTO CLICK EN VALIDAR CODIGO POSTAL
    $("#btnValidaCpEstado").on("click", function () {
        fvalidatestatecodpost();
    });
    //EVENTO CLICK EN VALIDAR CODIGO POSTAL EN EDICION DE LA EMPRESA
    $("#btnValidaCpEstadoed").on("click", function () {
        fvalidatestatecodposted();
    });
    // FUNCION QUE VALIDA CODIGO POSTAL 
    fvalidatestatecodpost = () => {
        if (codpost.value.length === 5) {
            setTimeout(() => {
                $.ajax({
                    url: "../Empleados/LoadInformationHome2",
                    type: "POST",
                    data: { codepost: codpost.value },

                    success: (data) => {
                        //console.log(data);
                        if (data.length > 0) {
                            city.disabled = false;
                            municipio.disabled = false;
                            colony.disabled = false;
                            states.disabled = false;
                            $("#inColonia_empresa").empty();
                            for (i = 0; i < data.length; i++) {
                                if (data[i].sColonia != "") {
                                    colony.innerHTML += `<option value='${data[i].iIdColonia}'>${data[i].sColonia}</option>`;
                                    municipio.innerHTML = `<option value='${data[i].iIdMunicipio}' >${data[i].sMunicipio}</option>`;
                                    states.innerHTML = `<option value='${data[i].iIdEstado}' >${data[i].sEstado}</option>`;
                                    city.innerHTML = `<option value='${data[i].sCiudad}'>${data[i].sCiudad}</option>`;

                                }
                            }
                            setTimeout(() => {
                                colony.focus();
                            }, 500);
                        } else {
                            Swal.fire({
                                title: "Atención",
                                text: "El código postal ingresado es incorrecto",
                                icon: "warning",
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                setTimeout(() => { codpost.focus(); }, 800)
                            });
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            }, 1500);
        } else {
            Swal.fire({
                title: "Atención", text: "El código postal debe de contener 5 caracteres", icon: "warning",
                showClass: { popup: 'animated fadeInDown faster' },
                hideClass: { popup: 'animated fadeOutUp faster' },
                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
            }).then((acepta) => {

                setTimeout(() => { codpost.focus(); }, 800);
            });
        }
    }

    // FUNCION QUE VALIDA CODIGO POSTAL EN EDITAR DE LA EMPRESA
    fvalidatestatecodposted = () => {
        if (edcodpost.value.length === 5) {
            setTimeout(() => {
                $.ajax({
                    url: "../Empleados/LoadInformationHome2",
                    type: "POST",
                    data: { codepost: edcodpost.value },

                    success: (data) => {
                        //console.log(data);
                        if (data.length > 0) {
                            edcity.disabled = false;
                            edmunicipio.disabled = false;
                            edcolony.disabled = false;
                            edstates.disabled = false;
                            $("#edColonia_empresa").empty();
                            for (i = 0; i < data.length; i++) {
                                if (data[i].sColonia != "") {
                                    edcolony.innerHTML += `<option value='${data[i].iIdColonia}'>${data[i].sColonia}</option>`;
                                    edmunicipio.innerHTML = `<option value='${data[i].iIdMunicipio}' >${data[i].sMunicipio}</option>`;
                                    edstates.innerHTML = `<option value='${data[i].iIdEstado}' >${data[i].sEstado}</option>`;
                                    edcity.innerHTML = `<option value='${data[i].sCiudad}'>${data[i].sCiudad}</option>`;

                                }
                            }
                            setTimeout(() => {
                                edcolony.focus();
                            }, 500);
                        } else {
                            Swal.fire({
                                title: "Atención",
                                text: "El código postal ingresado es incorrecto",
                                icon: "warning",
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                setTimeout(() => { codpost.focus(); }, 800)
                            });
                        }
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            }, 1500);
        } else {
            Swal.fire({
                title: "Atención", text: "El código postal debe de contener 5 caracteres", icon: "warning",
                showClass: { popup: 'animated fadeInDown faster' },
                hideClass: { popup: 'animated fadeOutUp faster' },
                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
            }).then((acepta) => {

                setTimeout(() => { codpost.focus(); }, 800);
            });
        }
    }
});
//document.getElementById("btnClear").classList.add("invisible");
//document.getElementById("btnUpdate").classList.add("invisible");
$("#inCodigo_postal").on("keyup", function (evt) {
    var i = $(this).val();
    if (i.length == 5) {
        btnvalidacpstate.disabled = false;
        $("#btnValidaCpEstado").removeClass("btn-secondary");
        $("#btnValidaCpEstado").addClass("btn-info");
    } else {
        btnvalidacpstate.disabled = true;
        $("#btnValidaCpEstado").removeClass("btn-info");
        $("#btnValidaCpEstado").addClass("btn-secondary");
    }
});
$("#edCodigo_postal").on("keyup", function (evt) {
    var i = $(this).val();
    if (i.length == 5) {
        document.getElementById("btnValidaCpEstadoed").disabled = false;
        $("#btnValidaCpEstadoed").removeClass("btn-secondary");
        $("#btnValidaCpEstadoed").addClass("btn-info");
    } else {
        document.getElementById("btnValidaCpEstadoed").disabled = true;
        $("#btnValidaCpEstadoed").removeClass("btn-info");
        $("#btnValidaCpEstadoed").addClass("btn-secondary");
    }
});
$("#inRfc_empresa").on("keyup", function (evt) {
    var rfc = $("#inRfc_empresa").val();
    if (rfc.length === 12) {
        ValidaRFC();
    } else {
        if ($("#inRfc_empresa").hasClass("is-invalid")) { }
        else { $("#inRfc_empresa").addClass("is-invalid"); }
    }
    function ValidaRFC() {
        if (rfc.match(_rfc_pattern_pm)) {
            $("#inRfc_empresa").removeClass("is-invalid");
            $("#inRfc_empresa").addClass("is-valid");
            return true;
        } else {
            $("#inRfc_empresa").removeClass("is-valid");
            $("#inRfc_empresa").addClass("is-invalid");
            return false;
        }
    }
});

$("#edRfc_empresa").on("keyup", function (evt) {
    var rfc = $("#edRfc_empresa").val();
    if (rfc.length === 12) {
        ValidaRFC();
    } else {
        if ($("#edRfc_empresa").hasClass("is-invalid")) { }
        else { $("#edRfc_empresa").addClass("is-invalid"); }
    }
    function ValidaRFC() {
        if (rfc.match(_rfc_pattern_pm)) {
            $("#edRfc_empresa").removeClass("is-invalid");
            $("#edRfc_empresa").addClass("is-valid");
            return true;
        } else {
            $("#edRfc_empresa").removeClass("is-valid");
            $("#edRfc_empresa").addClass("is-invalid");
            return false;
        }
    }
});

$('.input-number').on('input', function () {
    this.value = this.value.replace(/[^0-9]/g, '');
});
$.ajax({
    url: "../Catalogos/LoadGruposEmpresas",
    type: "POST",
    contentType: "application/json; charset=utf-8",
    success: (data) => {
        //console.log(data);
        var select = document.getElementById("inGrupo_empresa");
        select.innerHTML = "";
        for (var i = 0; i < data.length; i++) {
            select.innerHTML += "<option value='" + data[i][0] + "'>" + data[i][1] + "</option>";
        }
    }
});
loadDatosEmpresa = () => {
    $.ajax({
        url: "../Empresas/LoadDatosEmpresa",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: (data) => {
            //console.log("datos empresa");
            //console.log(data);
            document.getElementById("l-01").innerHTML = data[0];
            document.getElementById("l-02").innerHTML = data[1];
            document.getElementById("l-03").innerHTML = data[2];
            document.getElementById("l-04").innerHTML = data[3];
            document.getElementById("l-05").innerHTML = data[4];
            document.getElementById("l-06").innerHTML = data[5];
            document.getElementById("l-07").innerHTML = data[6];
            document.getElementById("l-08").innerHTML = data[7];
            document.getElementById("l-09").innerHTML = data[8];
            document.getElementById("l-10").innerHTML = data[9];
            document.getElementById("l-11").innerHTML = data[10];
            document.getElementById("l-12").innerHTML = data[11].substring(0, 10);
            document.getElementById("l-13").innerHTML = data[12];
            document.getElementById("l-14").innerHTML = data[13];
        }
    });
}
loadDatosEmpresa();

$.ajax({
    url: "../Empresas/LoadClasesRP",
    type: "POST",
    data: JSON.stringify(),
    contentType: "application/json; charset=utf-8",
    success: (data) => {
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            document.getElementById("inClase").innerHTML += `<option value='${data[i].IdClase}'>${data[i].Nombre_Clase}</option>`;
        }
    }
});
$.ajax({
    url: "../Empresas/LoadRegimenesFiscales",
    type: "POST",
    data: JSON.stringify(),
    contentType: "application/json; charset=utf-8",
    success: (data) => {
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            document.getElementById("inRFiscal_empresa").innerHTML += `<option value='${data[i].IdRegimenFiscal}'>${data[i].Descripcion}</option>`;
        }
    }
});
//LLENA SELECT EMPRESAS
$.ajax({
    url: "../Empresas/LoadSEmp",
    type: "POST",
    contentType: "application/json; charset=utf-8",
    success: (data) => {
        //console.log(data);
        if (data.length != 0) {
            var select = document.getElementById("inclonar");
            select.innerHTML = "<option value='0'>No clonar</option>";
            for (var i = 0; i < data.length; i++) {
                select.innerHTML += "<option value='" + data[i]["IdEmpresa"] + "'>" + data[i]["IdEmpresa"] + "&nbsp;&nbsp;" + data[i]["NombreEmpresa"] + "</option>";
            }
        }

    }
});
// INSERTA EMPRESA
$("#form_inEmpresa").submit(function (evt) {

    var emp = document.getElementById("form_inEmpresa");
    if (emp.checkValidity() === false) {
        evt.preventDefault();
        evt.stopPropagation();
        emp.classList.add("was-validated");
        setTimeout(function () {
            $("#form_inEmpresa").removeClass("was-validated");
        }, 5000);
    } else {
        evt.preventDefault();
        evt.stopPropagation();

        var nomEmp = document.getElementById("inNombre_empresa");
        var nomEmpc = document.getElementById("inNomCorto_empresa");
        var rfcemp = document.getElementById("inRfc_empresa");
        var giroEmp = document.getElementById("inGiro_empresa");
        var cpEmp = document.getElementById("inCodigo_postal");
        var estEmp = document.getElementById("inEstado_empresa");
        var munEmp = document.getElementById("inMunicipio_empresa");
        var ciuEmp = document.getElementById("inCiudad_empresa");
        var colEmp = document.getElementById("inColonia_empresa");
        var delEmp = document.getElementById("inDelegacion_Empresa");
        var callEmp = document.getElementById("inCalle_Empresa");
        var rfEmp = document.getElementById("inRFiscal_empresa");
        var noafiEmp = document.getElementById("inNombreAfiliacionIMSS");
        var afImss = document.getElementById("inAfiliacionIMSS");
        var riesgot = document.getElementById("inRiesgoTrabajo");
        var claserp = document.getElementById("inClase");
        //var bancoin = document.getElementById("inRBanco_interbancarios");
        var regimss = document.getElementById("inRegistro_imss");
        var clonar = document.getElementById("inclonar");
        var grupoe = document.getElementById("inGrupo_empresa");
        //var banco_nom = document.getElementById("inBanco_nomina");

        //Variables para primer fecha periodo
        var finicio = document.getElementById("finicio");
        var ffinal = document.getElementById("ffinal");
        var fpago = document.getElementById("fpago");
        var fproceso = document.getElementById("fproceso");
        var diaspagados = document.getElementById("diaspagados");
        var periodo = document.getElementById("noperiodo");
        var tipoPeriodo;

        /*/
        Valida que tipo de periodo es la empresa
        /*/
        if ($('#inlineR1').prop('checked')) {
            tipoPeriodo = document.getElementById("inlineR1").value;
        }
        if ($('#inlineR2').prop('checked')) {
            tipoPeriodo = document.getElementById("inlineR2").value;
        }
        if ($('#inlineR3').prop('checked')) {
            tipoPeriodo = document.getElementById("inlineR3").value;
        }

        //-------------------------------------------------------

        var datos = {
            inNombre_empresa: nomEmp.value
            , inNomCorto_empresa: nomEmpc.value
            , inRfc_empresa: rfcemp.value
            , inGiro_empresa: giroEmp.value
            , inRegimenFiscal_Empresa: rfEmp.value
            , inCodigo_postal: cpEmp.value
            , inEstado_empresa: estEmp.value
            , inMunicipio_empresa: munEmp.value
            , inCiudad_empresa: ciuEmp.value
            , inColonia_empresa: colEmp.value
            , inDelegacion_Empresa: delEmp.value
            , inCalle_Empresa: callEmp.value
            , inAfiliacionIMSS: afImss.value
            , inNombre_Afiliacion: noafiEmp.value
            , inRiesgoTrabajo: riesgot.value
            , inClase: claserp.value

            , infinicio: finicio.value
            , inffinal: ffinal.value
            , infpago: fpago.value
            , infproceso: fproceso.value
            , indiaspagados: diaspagados.value
            , intipoperiodo: tipoPeriodo
            , inregimss: regimss.value
            , inclonar: clonar.value
            , ingrupoe: grupoe.value
            , innoperiodo: periodo.value
        };
        //console.log(datos);
        $.ajax({
            url: "../Empresas/Insert_Empresa_FirstStep",
            type: "POST",
            data: JSON.stringify(datos),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                //console.log(data);
                if (data[0] == "True") {
                    Swal.fire({
                        icon: 'success',
                        title: 'Correcto!',
                        text: 'Empresa agregada con exito!'
                    }, false).then(() => {

                        setTimeout(function () {
                            emp.reset();
                            $("#bodySubmenus").load("/Empresas/Registros_Patronales");
                        }, 1500);
                    });
                } else if (data[0] == "False") {
                    Swal.fire({
                        icon: 'error',
                        title: '',
                        text: '' + data[1]
                    }, false).then(() => {
                        nom.focus();
                        nom.classList.add("is-invalid");
                        rfc.classList.add("is-invalid");
                        setTimeout(function () {
                            nom.classList.remove("is-invalid");
                            rfc.classList.remove("is-invalid");
                        }, 5500);
                    });
                }

            }
        });
    }
});
$(".collapse").on("show", function () {
    //document.getElementById("btnadd").innerHTML = "Ocultar agregar empresa";
    //console.log("show");
    $("#inNombre_empresa").focus();
    //document.getElementById("inNombre_empresa").focus();
});
$("#btnadd").on("click", function () {
    document.getElementById("btnadd").innerHTML = "Ocultar agregar empresa <i class='text-danger fas fa-minus-circle'></i>";
    //console.log("show");

});
$('#collapseAddEmpresa').on('hidden.bs.collapse', function () {
    document.getElementById("btnadd").innerHTML = "Agregar empresa <i class='text-success fas fa-plus-circle'></i>";
    //console.log("was hide");
});

$("#btnEditarEmpresa").on("click", function () {

    $("#modalEditDatosEmpresa").modal("show");

    var nomEmp = document.getElementById("edNombre");
    var nomEmpc = document.getElementById("edNombrecorto");
    var rfcemp = document.getElementById("edRFC");
    var giroEmp = document.getElementById("edGiro");
    var cpEmp = document.getElementById("edCodigo_postal");
    var estEmp = document.getElementById("edEstado_empresa");
    var munEmp = document.getElementById("edMunicipio_empresa");
    var ciuEmp = document.getElementById("edCiudad_empresa");
    var colEmp = document.getElementById("edColonia_empresa");
    var delEmp = document.getElementById("edDelegacion_Empresa");
    var callEmp = document.getElementById("edCalle_Empresa");
    var rfEmp = document.getElementById("edRFiscal_empresa");
    var afImss = document.getElementById("edRegistroimss");

    $.ajax({
        url: "../Empresas/LoadEmpresa",
        type: "POST",
        data: JSON.stringify(),
        contentType: "application/json; charset=utf-8",
        success: (empresa) => {
            $.ajax({
                url: "../Empresas/LoadRegimenesFiscales",
                type: "POST",
                data: JSON.stringify(),
                contentType: "application/json; charset=utf-8",
                success: (regf) => {
                    document.getElementById("edRFiscal_empresa").innerHTML = "";
                    for (i = 0; i < regf.length; i++) {
                        if (empresa[12] == regf[i].IdRegimenFiscal) {
                            document.getElementById("edRFiscal_empresa").innerHTML += `<option selected value='${regf[i].IdRegimenFiscal}'>${regf[i].Descripcion}</option>`;
                        } else {
                            document.getElementById("edRFiscal_empresa").innerHTML += `<option value='${regf[i].IdRegimenFiscal}'>${regf[i].Descripcion}</option>`;
                        }
                    }
                }
            });
            nomEmp.value = empresa[2];
            nomEmpc.value = empresa[1];
            rfcemp.value = empresa[9];
            giroEmp.value = empresa[8];
            delEmp.value = empresa[6];
            callEmp.value = empresa[8];
            afImss.value = empresa[12];
            if (empresa[3].length < 5) {
                cpEmp.value = "0" + empresa[3];
            } else {
                cpEmp.value = empresa[3];
            }
            $("#btnValidaCpEstadoed").click();
            setTimeout(function () {
                $("#edNombre").focus();
            }, 2500);

        }
    });
});

$("#btnActualizarDatosEmpresa").click(function () {
    var emp = document.getElementById("frmEditarDatosEmpresa");
    if (emp.checkValidity() === false) {
        //evt.preventDefault();
        //evt.stopPropagation();
        emp.classList.add("was-validated");
        setTimeout(function () {
            $("#frmEditarDatosEmpresa").removeClass("was-validated");
        }, 5000);
    } else {
        var nomEmp = document.getElementById("edNombre");
        var nomEmpc = document.getElementById("edNombrecorto");
        var rfcemp = document.getElementById("edRFC");
        var giroEmp = document.getElementById("edGiro");
        var rfEmp = document.getElementById("edRFiscal_empresa");
        var regImss = document.getElementById("edRegistroimss");
        var codposEmp = document.getElementById("edCodigo_postal");
        var estEmp = document.getElementById("edEstado_empresa");
        var ciuEmp = document.getElementById("edCiudad_empresa");
        var colEmp = document.getElementById("edColonia_empresa");
        var delEmp = document.getElementById("edDelegacion_Empresa");
        var callEmp = document.getElementById("edCalle_Empresa");

        $.ajax({
            url: "../Empresas/UpdateEmpresa",
            type: "POST",
            data: JSON.stringify({
                edNombre: nomEmp.value,
                edNombrecorto: nomEmpc.value,
                edRFC: rfcemp.value,
                edGiro: giroEmp.value,
                edRegimenFiscal: rfEmp.value,
                edRegistroimss: regImss.value,
                edCodigo_postal: codposEmp.value,
                edEstado_empresa: estEmp.value,
                edCiudad_empresa: ciuEmp.value,
                edColonia_empresa: colEmp.value,
                edDelegacion_Empresa: delEmp.value,
                edCalle_Empresa: callEmp.value,
            }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                //console.log(data);
                if (data[0] == "0") {
                    loadDatosEmpresa();
                    Swal.fire({
                        icon: 'success',
                        title: 'Correcto!',
                        text: data[1],
                        timer: 1500
                    });
                    $("#modalEditDatosEmpresa").modal("hide");
                } else if (data[0] == "1") {
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

