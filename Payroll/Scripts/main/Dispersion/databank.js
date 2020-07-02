$(function () {

    /**
     * Constantes de la configuracion de bancos 
     **/

    const tableDataBank = document.getElementById('table-data-bank');
    const keyBank = document.getElementById('key-bank');
    const nameBankConfig = document.getElementById('name-bank-config');
    const numClientBank = document.getElementById('num-client-bank');
    const numBillBank = document.getElementById('num-bill-bank');
    const numSquareBank = document.getElementById('num-square-bank');
    const numClabeBank = document.getElementById('num-clabe-bank');
    //const numCodeBank = document.getElementById('num-code-bank');
    //const depositsInterbank = document.getElementById('deposits-interbank');
    //const depositsBank = document.getElementById('deposits-bank');
    const icoCloseConfigBank = document.getElementById('ico-close-config-bank');
    const btnCloseConfigBank = document.getElementById('btn-close-config-bank');
    const btnSaveConfigBank = document.getElementById('btn-save-config-bank');

    const typeDispersionBank = document.getElementById('type-dispersion-bank');

    const spanish = {
        "decimal": "",
        "emptyTable": "No hay información",
        "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
        "infoEmpty": "Mostrando 0 t 0 of 0 Entradas",
        "infoFiltered": "(Filtrado de _MAX_ total entradas)",
        "infoPostFix": "",
        "thousands": ",",
        "lengthMenu": "Mostrar _MENU_ Entradas",
        "loadingRecords": "Cargando...",
        "processing": "Procesando...",
        "search": "Buscar:",
        "zeroRecords": "Sin resultados encontrados",
        "paginate": {
            "first": "Primero",
            "last": "Ultimo",
            "next": "Siguiente",
            "previous": "Anterior"
        }
    };

    //setTimeout(() => {
    //    $("#table-test").DataTable({
    //        language: spanish
    //    });
    //}, 2000)

    /**
     * Funciones
     **/

    // Funcion que muestra tipos de alertas

    fshowtypealert = (title, text, icon, element, clear) => {
        Swal.fire({
            title: title, text: text, icon: icon,
            showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
            confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
        }).then((acepta) => {
            $("html, body").animate({ scrollTop: $(`#${element.id}`).offset().top - 50 }, 1000);
            if (clear == 1) { setTimeout(() => { element.focus(); setTimeout(() => { element.value = ""; }, 300); }, 1200); }
            else { setTimeout(() => { element.focus(); }, 1200); }
            if (element.id == "numpla") { setTimeout(() => { $("#btn-search-table-num-posicion").click(); }, 1500); }
        });
    }

    // Funcion que carga los tipos de dispersion
    fLoadTypeDispersion = async () => {
        try {
            await $.ajax({
                url: "../ConfigDataBank/LoadTypeDispersion",
                type: "POST",
                data: {},
                success: (data) => {
                    if (data.Bandera === true && data.MensajeError === "none") {
                        for (let i = 0; i < data.DatosDispersion.length; i++) {
                            var tDis = data.DatosDispersion[i];
                            typeDispersionBank.innerHTML += `<option value="${tDis.iId}">${tDis.sValor}</option>`;
                        }
                    } else {
                        alert('Error al cargar los tipos de dispersion');
                        location.reload();
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        } catch (error) {
            if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }

    fLoadTypeDispersion();

    // Funcion que carga los datos bancarios en la tabla
    fLoadTableDataBanks = () => {
        tableDataBank.innerHTML = "";
        try {
            $.ajax({
                url: "../ConfigDataBank/LoadDataTableBanks",
                type: "POST",
                data: {},
                success: (data) => {
                    if (data.Bandera == true && data.MensajeError == "none") {
                        const dataLength = data.DatosBancos.length;
                        let lengthData = 0;
                        for (let i = 0; i < data.DatosBancos.length; i++) {
                            tableDataBank.innerHTML += `
                                <tr>
                                    <td>${data.DatosBancos[i].iIdBanco}</td>
                                    <td>${data.DatosBancos[i].sNombreBanco}</td>
                                    <td>${data.DatosBancos[i].sNumeroCliente}</td>
                                    <td>${data.DatosBancos[i].sNumeroCuenta}</td>
                                    <td>${data.DatosBancos[i].sNumeroPlaza}</td>
                                    <td class="text-center">${data.DatosBancos[i].sValor}</td>
                                    <td class="text-center">
                                        <button type="button" onclick="fChangeTypeBank(${data.DatosBancos[i].iIdBancoEmpresa}, '${data.DatosBancos[i].sNombreBanco}',${data.DatosBancos[i].sNumeroCliente}, ${data.DatosBancos[i].sNumeroCuenta}, ${data.DatosBancos[i].sNumeroPlaza}, ${data.DatosBancos[i].sClabe}, ${data.DatosBancos[i].iCg_tipo_dispersion})" class="btn btn-warning btn-sm btn-icon-split shadow">
                                            <span class="icon text-white-50">
                                                <i class="fas fa-edit"></i>
                                            </span>
                                            <span class="text">Editar</span>
                                        </button>
                                    </td>
                                </tr>
                            `;
                            lengthData += 1;
                        }
                        if (dataLength == lengthData) {
                            setTimeout(() => {
                                $("#table-test").DataTable({
                                    language: spanish
                                });
                            }, 1000);
                        }
                        //<td class="text-center"><button class="btn btn-warning btn-sm text-white" onclick="fShowDetailsBank(${data.DatosBancos[i].iIdBanco}, '${data.DatosBancos[i].sNombreBanco}', ${data.DatosBancos[i].sNumeroCliente}, ${data.DatosBancos[i].sNumeroCuenta}, ${data.DatosBancos[i].sNumeroPlaza}, ${data.DatosBancos[i].sClabe}, ${data.DatosBancos[i].iCodigoBanco}, ${data.DatosBancos[i].iGeneraInterface})"> <i class="fas fa-file"></i></button></td>
                    } else {
                        //Swal.fire({
                        //    title: "Ocurrio un error", text: "Reporte el problema al area de TI indicando el siguiente código: #CODERRfLoadTableDataBanksDATABANKDIS# ", icon: "error",
                        //    showClass: { popup: 'animated fadeInDown faster' },
                        //    hideClass: { popup: 'animated fadeOutUp faster' },
                        //    confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false
                        //}).then((acepta) => {
                        //    location.reload();
                        //});
                        setTimeout(() => {
                            $("#table-test").DataTable({
                                language: spanish
                            });
                        }, 1000);
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        } catch (error) {
            if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else {
                console.error('Error: ', error.message);
            }
        }
    }

    // Funcion que cambia el tipo de dispersion del banco 
    fChangeTypeBank = (paramid, paramnamebank, paramnclient, paramnbill, paramsquare, paramclab, paramtypedis) => {
        try {
            if (parseInt(paramid) > 0) {
                $("#details-config-bank").modal('show');
                keyBank.value = parseInt(paramid);
                nameBankConfig.textContent = paramnamebank;
                numClientBank.value        = paramnclient;
                numBillBank.value          = paramnbill;
                numSquareBank.value        = paramsquare;
                numClabeBank.value         = paramclab;
                typeDispersionBank.value   = paramtypedis;
                setTimeout(() => { numClientBank.focus(); }, 1000);
            } else {
                alert('Accion invalida!');
                location.reload();
            }
        } catch (error) {
            if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }

    // Funcion que muestra los detalles de el banco seleccionado

    fShowDetailsBank = (paramid, paramnombre, paramcliente, paramcuenta, paramplaza, paramclabe, paramcodigo, paraminterface) => {
        try {
            $("#details-config-bank").modal('show');
            keyBank.value = paramid;
            nameBankConfig.textContent = paramnombre;
            numClientBank.value = paramcliente;
            numBillBank.value = paramcuenta;
            numSquareBank.value = paramplaza;
            numClabeBank.value = paramclabe;
            //numCodeBank.value = paramcodigo;
            //if (paraminterface == 0) {
            //    depositsInterbank.setAttribute('checked', 'true');
            //}
            //if (paraminterface == 1) {
            //    depositsBank.setAttribute('checked', 'true');
            //}
        } catch (error) {
            if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else {
                console.error('Error: ', error.message);
            }
        }
    }

    // Funcion que limpia la ventana de configuracion de bancos

    fClearFieldsConfigBank = () => {
        nameBankConfig.textContent = '';
        numClientBank.value = '';
        numBillBank.value = '';
        numSquareBank.value = '';
        numClabeBank.value = '';
        //numCodeBank.value = '';
        //depositsInterbank.removeAttribute('checked');
        //depositsBank.removeAttribute('checked');
    }

    // Funcion que guarda los cambios actualizados en el banco

    fUpdateConfigBank = () => {
        try {
            const arrInputs = [numClientBank, numBillBank, numSquareBank, numClabeBank];
            let validate = 0;
            for (let i = 0; i < arrInputs.length; i++) {
                if (arrInputs[i].value == "") {
                    fshowtypealert('Atención', 'Completa el campo ' + arrInputs[i].placeholder, 'warning', arrInputs[i], 0);
                    validate = 1;
                    break;
                }
            }
            if (typeDispersionBank.value == 0) {
                fshowtypealert('Atención', 'Selecciona una opcion de tipo', 'warning', typeDispersionBank, 0);
                validate = 1
            }
            if (validate == 0) {
                ////const valueDis = $("input:radio[name=optionbank]:checked").val();
                ////let valueSend = "";
                ////if (valueDis == "0") {
                ////    valueSend = 0;
                ////} else if (valueDis == "1") {
                ////    valueSend = 1;
                ////}
                const dataSend = {
                    keyBank: keyBank.value,
                    numClientBank: numClientBank.value,
                    numBillBank: numBillBank.value,
                    numSquareBank: numSquareBank.value,
                    numClabeBank: numClabeBank.value,
                    //numCodeBank: numCodeBank.value,
                    interfaceGen: typeDispersionBank.value
                };
                $.ajax({
                    url: "../ConfigDataBank/UpdateConfigBank",
                    type: "POST",
                    data: dataSend,
                    beforeSend: () => {
                        btnSaveConfigBank.disabled = true;
                    }, success: (data) => {
                        console.log(data);
                        if (data.Bandera == true && data.MensajeError == "none" && data.Validacion == false) {
                            const tableData = $("#table-test").DataTable();
                            tableData.destroy();
                            setTimeout(() => { fLoadTableDataBanks(); }, 1000);
                            Swal.fire({
                                title: "Correcto", text: "Datos actualizados!", icon: "success",
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false
                            }).then((acepta) => {
                                $("#details-config-bank").modal('hide');
                                fClearFieldsConfigBank();
                            });
                        } else if (data.Bandera == false && data.MensajeError == "none" && data.Validacion == true) {
                            Swal.fire({
                                title: "Atención!", text: "No puedes tener dos bancos interbancarios!", icon: "info",
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false
                            }).then((acepta) => {
                                $("#details-config-bank").modal('hide');
                                fClearFieldsConfigBank();
                            });
                        } else {
                            Swal.fire({
                                title: "Ocurrio un error", text: "Reporte el problema al area de TI indicando el siguiente código: #CODERRfUpdateConfigBankDATABANKDIS# ", icon: "error",
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false
                            }).then((acepta) => {
                                location.reload();
                            });
                        }
                        btnSaveConfigBank.disabled = false;
                        setTimeout(() => {
                            document.getElementById('body-init').style.paddingRight = '0px';
                            //document.getElementById('body-init').removeAttribute("style");
                        }, 2000);
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            }
        } catch (error) {
            if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else {
                console.error('Error: ', error.message);
            }
        }
    }

    /**
     * Ejecucion de funciones
     **/

    fLoadTableDataBanks();

    icoCloseConfigBank.addEventListener('click', fClearFieldsConfigBank);
    btnCloseConfigBank.addEventListener('click', fClearFieldsConfigBank);

    btnSaveConfigBank.addEventListener('click', fUpdateConfigBank);

    //let reloadtable = setInterval(() => {
    //    if (tableDataBank.innerHTML == "") {
    //        console.log('vacio')
    //    } else {
    //        fLoadTableDataBanks();
    //    }
    //}, 1000);

});