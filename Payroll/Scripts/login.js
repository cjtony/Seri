//document.addEventListener('DOMContentLoaded', () => {
$(function () { 

    // *-* DECLARACION DE VARIABLES FORMULARIO *-* \\

    const username = document.getElementById('username');
    const password = document.getElementById('password');

    // *-* DECLARACION DE VARIABLES BOTONES *-* \\

    const btnlogin = document.getElementById('btnlogin');

    //  INICIALIZACION DE VALORES VACIOS EN INPUTS  \\

    username.value = "";
    password.value = "";

    // OCULTA EL DIV QUE MUESTRA MENSAJE DE ERROR DE AJAX \\

    $("#diverror").hide();

    // *-* DECLARACION DE FUNCIONES *-* \\

    // FUNCION QUE MUESTRA ALERTAS AL USUARIO DEPENDIENDO LA ACCION QUE SE REALIZE \\

    fshowalerts = (icon, title, text, element, validate, success) => {
        if (validate === 1) {
            Swal.fire({
                title: title, text: text, icon: icon,
                showClass: { popup: 'animated fadeInDown faster' },
                hideClass: { popup: 'animated fadeOutUp faster' },
                confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
            }).then((acepta) => {
                if (success === "yes") {
                    location.href = "../../ControlPayroll/Home";
                } else {
                    setTimeout(() => {
                        element.value = "";
                        element.focus();
                    }, 800);
                }
            });
        } else {
            Swal.fire({
                icon: icon, title: title, text: text
            }).then((param) => {
                if (param) {
                    setTimeout(() => {
                        element.focus();
                    }, 1000);
                }
            });
        }
    }

    // FUNCION QUE VALIDA QUE LOS DATOS INGRESADOS EN EL LOGIN SEAN VERDADEROS \\

    fvalidatelogin = (e) => {
        if (username.value != "") {
            if (password.value != "") {
                const userupper = username.value.toUpperCase();
                const dataSend = { username: userupper, password: password.value };
                $.ajax({
                    url: "../Login/LoginValidate",
                    type: "POST",
                    data: JSON.stringify(dataSend),
                    contentType: "application/json; charset=utf-8",
                    dataType: "JSON",
                    beforeSend: () => {
                        btnlogin.disabled = true;
                        document.getElementById('msjvalidateinfo').classList.remove('d-none');
                    },
                    success: (request) => {
                        document.getElementById('msjvalidateinfo').classList.add('d-none');
                        btnlogin.disabled = false;
                        if (request.sMensaje === "usererror") {
                            fshowalerts("warning", "Atención!", "El usuario ingresado es incorrecto o se encuentra cancelado", username, 1, "n");
                        } else if (request.sMensaje === "passerror") {
                            fshowalerts("warning", "Atención!", "La contraseña ingresada es incorrecta", password, 1, "n");
                        } else if (request.sMensaje === "success") {
                            fshowalerts("success", "Correcto!", "Iniciando sesion, preciona Aceptar...", "", 1, "yes");
                        }
                    }, error: (jqXHR, exception) => {
                        let msg = '';
                        if (jqXHR.status === 0) {
                            msg = "No conectado. \n Verifica tu conexión de red.";
                        } else if (jqXHR.status === 404) {
                            msg = 'Página solicitada no encontrada. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Error interno del servidor [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'El análisis JSON solicitado falló.';
                        } else if (exception === 'timeout') {
                            msg = 'Error de tiempo de espera.';
                        } else if (exception === 'abort') {
                            msg = 'Solicitud de Ajax abortada.';
                        } else {
                            msg = 'Error no detectado.\n' + jqXHR.responseText;
                        }
                        document.getElementById('msgerror').textContent = msg + " Si el error persiste por favor contacte a sistemas.";
                        $("#diverror").show(1000);
                        username.value = "";
                        password.value = "";
                        setTimeout(() => {
                            $("#diverror").hide(1000);
                            username.focus();
                        }, 5000);
                    }
                });
            } else {
                fshowalerts("warning", "Atención!", "Ingrese su contraseña...", password, 0, "n");
            }
        } else {
            fshowalerts("warning","Atención!","Ingrese su nombre de usuario...", username, 0, "n");
        }
        e.preventDefault();
    }

    // *-* EJECUCION DE FUNCIONES *-* \\

    btnlogin.addEventListener('click', fvalidatelogin);

    document.addEventListener('keydown', function (event) {
        const mayus = event.getModifierState && event.getModifierState('CapsLock');
        if (mayus) {
            document.getElementById('msjmaysact').classList.remove('d-none');
        } else {
            document.getElementById('msjmaysact').classList.add('d-none');
        }
    });

});