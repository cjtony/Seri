$(function () {
    // variables
    let name = document.getElementById('name');
    const state = document.getElementById('inEstado_empresa');
    const codpost = document.getElementById('inCodigo_postal');
    const city = document.getElementById('inCiudad_empresa');
    const colony = document.getElementById('inColonia_empresa');
    const street = document.getElementById('street');
    const numberst = document.getElementById('numberst');
    const banuse = document.getElementById('banuse');
    const cunuse = document.getElementById('cunuse');
    const tippag = document.getElementById('tippag');
    const nivtab = document.getElementById('nivtab');
    const clvbank = document.getElementById('clvbank');
    const curp = document.getElementById('curp');

    const btnVerifCodPost = document.getElementById('btn-verif-codpost');

    //Funcion para validar solo numeros 
    $('.input-number').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    // INICIO FUNCIONALIDADES ESTADOS \\
    let getDataTabDataGen;
    // ** Funcion que carga los estados ** \\
    floadstates = () => {
        try {
            $.ajax({
                url: "../Empleados/LoadStates",
                type: "POST",
                data: {},
                contentType: "application/json; charset=utf-8",
                success: (data) => {
                    const quantity = data.length;
                    let stated;
                    for (t in getDataTabDataGen) {
                        if (getDataTabDataGen[t].key === "general") {
                            stated = getDataTabDataGen[t].data.state;
                        }
                    }
                    if (quantity > 0) {
                        for (i = 0; i < data.length; i++) {
                            if (stated == data[i].iId) {
                                state.innerHTML += `
                                    <option selected value="${data[i].iId}">${data[i].sValor}</option>
                                `;
                            } else {
                                state.innerHTML += `
                                    <option value="${data[i].iId}">${data[i].sValor}</option>
                                `;
                            }
                        }
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        } catch (error) {
            if (error instanceof TypeError) {
                console.log("TypeError ", error);
            } else if (error instanceof RangeError) {
                console.log("RangeError ", error);
            } else if (error instanceof EvalError) {
                console.log("EvalError", error);
            }
        }
    }

    //floadstates();

    fvalidatestate = () => {
        colony.innerHTML = '<option value="0">Selecciona</option>'
        fdisabledfields(true);
        city.value = "";
        street.value = "";
        numberst.value = "";
        if (state.value != "0") {
            codpost.disabled = false;
            setTimeout(() => { codpost.focus() }, 500);
        } else {
            codpost.disabled = true;
            codpost.value = '';
        }
    }

    //state.addEventListener('change', fvalidatestate);

    fvalidatecodpost = () => {
        if (codpost.value.length == 5) {
            btnVerifCodPost.disabled = false;
            btnVerifCodPost.classList.add('btn-info');
        } else {
            btnVerifCodPost.disabled = true;
            btnVerifCodPost.classList.remove('btn-info');
            colony.value = "0"; city.value = "", street.value = "", numberst.value = "";
        }
    }

    //codpost.addEventListener('keyup', fvalidatecodpost);

    fvalidatestatecodpost = () => {
        if (codpost.value.length === 5) {
            document.getElementById('load-spinner').classList.remove('d-none');
            btnVerifCodPost.classList.add('d-none');
            setTimeout(() => {
                $.ajax({
                    url: "../Empleados/LoadInformationHome",
                    type: "POST",
                    data: { codepost: codpost.value, state: state.value },
                    success: (data) => {
                        console.log(data.length);
                        console.log('El estado es: ' + state.value);
                        console.log('El codigo postal es: ' + codpost.value);
                        if (data.length > 0) {
                            fdisabledfields(false);
                            for (i = 0; i < data.length; i++) {
                                city.value = data[i].sCiudad;
                                if (data[i].sColonia != "") {
                                    colony.innerHTML += `<option>${data[i].sColonia}</option>`;
                                }
                            }
                            setTimeout(() => {
                                colony.focus();
                            }, 500);
                        } else {
                            Swal.fire({
                                title: "Atención",
                                text: "El código postal ingresado es incorrecto no pertenece al estado seleccionado",
                                icon: "warning",
                                showClass: { popup: 'animated fadeInDown faster' },
                                hideClass: { popup: 'animated fadeOutUp faster' },
                                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                            }).then((acepta) => {
                                setTimeout(() => { codpost.focus(); }, 800)
                            });
                        }
                        btnVerifCodPost.classList.remove('d-none');
                        document.getElementById('load-spinner').classList.add('d-none')
                    }, error: (jqXHR, exception) => {
                        fcaptureaerrorsajax(jqXHR, exception);
                    }
                });
            }, 2000);
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

    //btnVerifCodPost.addEventListener('click', fvalidatestatecodpost);

    // FIN FUNCIONALIDADES ESTADOS \\
    
    
});
