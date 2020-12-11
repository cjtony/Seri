$(function () {

    var CFija = document.getElementById("inCFija");
    var Porcentaje = document.getElementById("inPorcentaje");
    var AplicaEn = document.getElementById("inAplicaEn");
    var DescFiniquito = document.getElementById("inDescFiniquito");
    var Beneficiaria = document.getElementById("inBeneficiaria");
    var Banco = document.getElementById("inBanco");
    var Sucursal = document.getElementById("inSucursal");
    var TVales = document.getElementById("inTVales");
    var NOficio = document.getElementById("inNOficio");
    var FOficio = document.getElementById("inFOficio");
    var TCalculo = document.getElementById("inTCalculo");
    var AumentoSSMG = document.getElementById("inAumentoSegunSMG");
    var AumentaSAS = document.getElementById("inAumentarSegunAs");
    var CCheques = document.getElementById("inCCheques");
    var FBaja = document.getElementById("inFBaja");

    $("#modalLiveSearchEmpleado").modal("show");
    
    $("#btn-save-pension").on("click", function (evt) {
        var ch1, ch2, ch3;
        if ($("#inDescFiniquito").is(":checked")) { ch1 = 1; } else { ch1 = 0; }
        if ($("#inAumentoSegunSMG").is(":checked")) { ch2 = 1; } else { ch2 = 0; }
        if ($("#inAumentarSegunAs").is(":checked")) { ch3 = 1; } else { ch3 = 0; }
        if (Porcentaje.value == "") { Porcentaje.value = 0 }
        if (CFija.value == "") { CFija.value = "0" }
        var data = $("#frmPensionesAlimenticias").serialize();
        
        var form = document.getElementById("frmPensionesAlimenticias");
        if (form.checkValidity() === false) {
            evt.preventDefault();
            form.classList.add("was-validated");    
        } else {
            var benef;
            if (Beneficiaria.value == "") { benef = 0;  } else {    benef = Beneficiaria.value; }
            $.ajax({
                url: "../Incidencias/SavePension",
                type: "POST",
                data: JSON.stringify({
                    Cuota_fija: CFija.value,
                    Porcentaje: Porcentaje.value,
                    AplicaEn: AplicaEn.value,
                    Descontar_en_Finiquito: ch1,
                    No_Oficio: NOficio.value,
                    Fecha_Oficio: FOficio.value,
                    Tipo_Calculo: TCalculo.value,
                    Aumentar_segun_salario_minimo_general: ch2,
                    Aumentar_segun_aumento_de_sueldo: ch3,
                    Beneficiaria: benef,
                    Banco: Banco.value,
                    Sucursal: Sucursal.value,
                    Tarjeta_vales: TVales.value,
                    Cuenta_cheques: CCheques.value,
                    Fecha_baja: FBaja.value
                }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    if (data[0] == '0') {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Aviso!',
                            text: data[1]
                        });
                    }
                    else {
                        tabPensiones();
                        form.reset();
                        setTimeout(() => {
                            
                            CFija.focus();
                        }, 1200);
                        Swal.fire({
                            icon: 'success',
                            title: 'Completado!',
                            text: data[1],
                            timer: 1000
                        });
                    }
                    
                    


                    
                }
            });

        }
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
                    
                    $("#resultSearchEmpleados").empty();
                    if (data[0]["iFlag"] == 0) {
                        for (var i = 0; i < data.length; i++) {
                            $("#resultSearchEmpleados").append("<div class='list-group-item list-group-item-action btnListEmpleados font-labels  font-weight-bold' onclick='MostrarDatosEmpleado(" + data[i]["IdEmpleado"] + ")'> <i class='far fa-user-circle text-primary'></i> " + data[i]["Apellido_Paterno_Empleado"] + ' ' + data[i]["Apellido_Materno_Empleado"] + "  " + data[i]["Nombre_Empleado"] + "  -   <small class='text-muted'><i class='fas fa-briefcase text-warning'></i> " + data[i]["DescripcionDepartamento"] + "</small> - <small class='text-muted'>" + data[i]["DescripcionPuesto"] + "</small></div>");
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

    MostrarDatosEmpleado = (idE) => {
        var txtIdEmpleado = { "IdEmpleado": idE };
        $.ajax({
            url: "../Empleados/SearchEmpleado",
            type: "POST",
            data: JSON.stringify(txtIdEmpleado),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: (data) => {
                var iconb = "";
                var colorb = "";
                if (data[0]["TipoEmpleado"] > 163) {
                    colorb = "badge-danger";
                    iconb = "fa-times-circle";
                } else {
                    colorb = "badge-success";
                    iconb = "fa-check-circle";
                }
                document.getElementById("EmpDes").innerHTML = "<i class='far fa-user-circle text-primary'></i> " + data[0]["Nombre_Empleado"] + " " + data[0]["Apellido_Paterno_Empleado"] + ' ' + data[0]["Apellido_Materno_Empleado"] + " - <small class='text-muted'>" + data[0]["DescripcionPuesto"] + "</small>&nbsp;&nbsp;<div class='badge " + colorb + "'><i class='fas " + iconb + "'></i>&nbsp;" + data[0]["TipoEmpleado"] + "&nbsp;-&nbsp;" + data[0]["DescTipoEmpleado"] + "</div>";
                $("#modalLiveSearchEmpleado").modal("hide");
                document.getElementById("resultSearchEmpleados").innerHTML = "";
                document.getElementById("inputSearchEmpleados").value = "";
                tabPensiones();
                LoadSelectAplicaEn();
            }
        });
        
    }
    //Funcion para validar solo numeros 
    $('.input-number').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    tabPensiones = () => {
        $.ajax({
            method: "POST",
            url: "../Incidencias/LoadPensiones",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (data) => {
                console.log(data);
                $("#tabody").empty();
                for (var i = 0; i < data.length; i++) {
                    document.getElementById("tabody").innerHTML += "<tr>"
                        + "<td>" + data[i]["Beneficiaria"] + "</td>"
                        + "<td>" + data[i]["No_Oficio"] + "</td>"
                        + "<td>" + data[i]["Fecha_Oficio"] + "</td>"
                        //+ "<td>" + data[i]["Fecha_baja"] + "</td>"
                        + "<td>$ " + data[i]["Cuota_Fija"] + " - % " + data[i]["Porcentaje"] + "</td>"
                        + "<td><div class='btn badge badge-danger btn-editar-pensiones ' onclick='eliminarPension( " + data[i]["IdPension"] + "," + data[i]["IncidenciaProgramada_id"] + ");'><i class='fas fa-minus'></i></div></td>"
                        + "</tr>";
                    
                }
                

            }
        });
    }
    LoadSelectBancos = (Banco_id) => {
        $.ajax({
            url: "../Empleados/LoadBanks",
            type: "POST",
            data: JSON.stringify({ keyban: Banco_id }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {

                var select = document.getElementById("inBanco");
                select.innerHTML = "";
                for (var i = 0; i < data.length; i++) {

                    select.innerHTML += "<option value='" + data[i]["iIdBanco"] + "'>" + data[i]["sNombreBanco"] + "</option>"
                }

            }
        });
    }
    LoadSelectBancos(0);
    // ELIMINAR (DESACTIVA) PENSION ALIMENTARIA
    eliminarPension = (Pension_id, IncidenciaP_id) => {
        Swal.fire({
            title: 'Quieres eliminar la Pensión?',
            text: "No podras recuperarla!",
            icon: 'warning',
            showCancelButton: true, 
            confirmButtonColor: '#A52A0F',
            cancelButtonColor: 'secondary',
            confirmButtonText: 'Eliminar!',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    method: "POST",
                    data: JSON.stringify({ Pension_id: Pension_id, IncidenciaP_id: IncidenciaP_id }),
                    url: "../Incidencias/DeletePension",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: (data) => {
                        if (data[0] == '0') {
                            Swal.fire({
                                title: 'Error!',
                                text: data[1],
                                icon: 'warning',
                                timer: 1000
                            });
                        } else {
                            document.getElementById("tabody").innerHTML = "";
                            tabPensiones();
                            setTimeout(() => {
                                CFija.focus();
                            }, 1200);
                            Swal.fire({
                                icon: 'success',
                                title: 'Borrado!',
                                text: data[1],
                                timer: 1000
                            });
                        }
                    }
                });
            }
        });
    }
    // CARGA SELECT DE APLICA EN
    LoadSelectAplicaEn = () => {
        $.ajax({
            url: "../Incidencias/LoadAplicaEn",
            type: "POST",
            data: JSON.stringify({ CampoCatalogo_id: 40 }),
            contentType: "application/json; charset=utf-8",
            success: (data) => {

                var select = document.getElementById("inAplicaEn");
                select.innerHTML = "";
                select.innerHTML += "<option value=''> Selecciona </option>";
                for (var i = 0; i < data.length; i++) {

                    select.innerHTML += "<option value='" + data[i]["iId"] + "'>" + data[i]["sValor"] + "</option>";
                }

            }
        });
    }
});
