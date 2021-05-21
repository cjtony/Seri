$(function () {
    encrypt = () => {
        var form = document.getElementById("frm_encrypt");
        if (form.checkValidity() === false) {
            form.classList.add("was-validated");
        } else {
            $.ajax({
                url: "../Catalogos/Encrypt",
                type: "POST",
                data: JSON.stringify({
                    txt: $("#txt_to_encrypt").val()
                }),
                contentType: "application/json; charset=utf-8",
                beforeSend: () => {
                    form.classList.add("was-validated");
                },
                success: (data) => {
                    console.log(data);
                    $("#txt_encrypted").val(data);
                }
            });
        }
    }
    decrypt = () => {
        var form = document.getElementById("frm_dencrypt");
        if (form.checkValidity() === false) {
            form.classList.add("was-validated");
        } else {
            $.ajax({
                url: "../Catalogos/Decrypt",
                type: "POST",
                data: JSON.stringify({
                    txt: $("#txt_to_decrypt").val()
                }),
                contentType: "application/json; charset=utf-8",
                beforeSend: () => {
                    form.classList.add("was-validated");
                },
                success: (data) => {
                    console.log(data);
                    $("#txt_decrypted").val(data);
                }
            });
        }
    }
});