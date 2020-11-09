$(function () {

    /*
     * Constantes
     */

    const receipt = document.getElementById('receipt');
    const period  = document.getElementById('period');
    const email   = document.getElementById('email');
    const btnCheckReceipt = document.getElementById('btn-check-receipt');

    /*
     * Funciones
     */

    // f validacion correo

    fValidateEmail = () => {
        const regExp = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
        if (email.value.length > 0) {
            if (!regExp.test(email.value)) {
                btnCheckReceipt.disabled = true;
            } else {
                btnCheckReceipt.disabled = false;
            }
        }
    }

    fCheckReceiptData = () => {
        try {
            if (receipt.value != "none") {
                if (period.value != "none") {
                    if (email.value != "") {
                        alert('Todo Ok');
                    } else {
                        alert('Completa el campo correo electronico');
                    }
                } else {
                    alert('Completa el campo recibo');
                }
            } else {
                alert('Completa el campo recibo');
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

    email.addEventListener('keyup', fValidateEmail);

    btnCheckReceipt.addEventListener('click', fCheckReceiptData);

});