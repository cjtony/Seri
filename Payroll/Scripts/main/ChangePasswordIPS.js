$(function () {
    var pass1 = $("#password1");
    var pass2 = $("#password2");
    var validacion = $("#validatediv");

    $(".pass").change(function () {
        if (pass1.val == pass2.val && pass1.val.length == 0) {
            validacion.html("<strong></strong>");
        }
    });

});