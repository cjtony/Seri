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
    const numCodeBank = document.getElementById('num-code-bank');
    const depositsInterbank = document.getElementById('deposits-interbank');
    const depositsBank = document.getElementById('deposits-bank');
    const icoCloseConfigBank = document.getElementById('ico-close-config-bank');
    const btnCloseConfigBank = document.getElementById('btn-close-config-bank');
    const btnSaveConfigBank = document.getElementById('btn-save-config-bank');

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

    // Funcion que carga los datos bancarios en la tabla
    fLoadTableDataBanks = () => {
        try {
            $.ajax({
                url: "../ConfigDataBank/LoadDataTableBanks",
                type: "POST",
                data: {},
                success: (data) => {
                    console.log('Datos bancarios');
                    console.log(data);
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
                                    <td class="text-center"><button class="btn btn-warning btn-sm text-white"> <i class="fas fa-file"></i></button></td>
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
            numCodeBank.value = paramcodigo;
            if (paraminterface == 0) {
                depositsInterbank.setAttribute('checked', 'true');
            }
            if (paraminterface == 1) {
                depositsBank.setAttribute('checked', 'true');
            }
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
        numCodeBank.value = '';
        depositsInterbank.removeAttribute('checked');
        depositsBank.removeAttribute('checked');
    }

    // Funcion que guarda los cambios actualizados en el banco

    fUpdateConfigBank = () => {
        try {
            const arrInputs = [numClientBank, numBillBank, numSquareBank, numClabeBank, numCodeBank];
            let validate = 0;
            for (let i = 0; i < arrInputs.length; i++) {
                if (arrInputs[i].value == "") {
                    fshowtypealert('Atención', 'Completa el campo ' + arrInputs[i].placeholder, 'warning', arrInputs[i], 0);
                    validate = 1;
                    break;
                }
            }
            if (validate == 0) {
                const valueDis = $("input:radio[name=optionbank]:checked").val();
                let valueSend = "";
                if (valueDis == "0") {
                    valueSend = 0;
                } else if (valueDis == "1") {
                    valueSend = 1;
                }
                const dataSend = {
                    keyBank: keyBank.value,
                    numClientBank: numClientBank.value,
                    numBillBank: numBillBank.value,
                    numSquareBank: numSquareBank.value,
                    numClabeBank: numClabeBank.value,
                    numCodeBank: numCodeBank.value,
                    interfaceGen: valueSend
                };
                $.ajax({
                    url: "../ConfigDataBank/UpdateConfigBank",
                    type: "POST",
                    data: dataSend,
                    beforeSend: () => {
                        console.log('validando');
                    }, success: (data) => {
                        if (data.Bandera == true && data.MensajeError == "none") {
                            Swal.fire({
                                title: "Correcto", text: "Datos actualizados!", icon: "success",
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false
                            }).then((acepta) => {
                                tableDataBank.innerHTML = '';
                                fLoadTableDataBanks();
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