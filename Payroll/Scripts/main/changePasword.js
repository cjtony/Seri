$(function () {

    passwordValidate = () 

    SaveChanges = () => {

        $.ajax({
            url: "../login/changePassword",
            type: "POST",
            data: {},
            beforeSend: () => {
                btnlogin.disabled = true;
            },
            success: (data) => {
                if (data.Bandera != true) {
                    alert('Accion invalida');
                }
                else {
                }
                btnlogin.disabled = false;
            }, error: (jqXHR, exception) => {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });

    }
});