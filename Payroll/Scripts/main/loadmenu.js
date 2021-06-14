$(function () {

    // Carga el contenido del menu \\
    
    loadmenu = () => {
        try {
            $.ajax({
                url: "",
                type: "POST",
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                async: false,
                cache: false,
                success: (data) => {
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

    loadmenu();

});