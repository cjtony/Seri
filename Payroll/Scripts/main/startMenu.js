$(document).ready(function () {
    // Carga el contenido del las tablas del menu \\
    function loadNavTable() {
        try {
            $.ajax({
                url: "../ControlPayroll/MenuInit",
                type: "POST",
                data: {},
                contentType: "application/json; charset=utf-8",
                success: (data) => {

                    $(".NavMenu").html(data);

                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        } catch (err) {
            if (err instanceof TypeError) {
                console.log("TypeError ", err);
            } else if (err instanceof EvalError) {
                console.log("EvalError ", err);
            } else if (err instanceof RangeError) {
                console.log("RangeError ", err);
            } else {
                console.log("Error ", err);
            }
        }
    }
    loadNavTable();
});




