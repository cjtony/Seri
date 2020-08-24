$(document).ready(function () {
    var datos = $("#frmUploadIncidencias");
    var url = "";
    $("#frmUploadIncidencias").on("submit", function (e) {
        e.preventDefault();
        var formd = $("frmUploadIncidencias")[0].files[0];
        //formData.append("url", "aqui estaria la url");
        console.log(formd);
    });

    //$("#incidenciasfile").change(function () {
    //    //if ($("#incidenciasfile").val() != '') {

    //    //}
    //});
    console.log(url);

    loadIncidenciasFile = () => {
        var oReq = new XMLHttpRequest();
        oReq.open("GET", url, true);
        oReq.responseType = "arraybuffer";
        oReq.onload = function (e) {
            var info = readData();
            console.log(info);
        }

        function readData() {
            var arraybuffer = oReq.response;
            /* convert data to binary string */
            var data = new Uint8Array(arraybuffer);
            var arr = new Array();
            for (var i = 0; i != data.length; i++) {
                arr[i] = String.fromCharCode(data[i]);
            }
            var bsrt = arr.join("");
            var workbook = XLSX.read(bstr, { type: "binary" });

            /*  */
            var first_sheet_name = workbook.SheetNames[0];

            var worksheet = workbook.Sheets[first_sheet_name];

            var info = XLSX.utils.sheet_to_json(worksheet, { raw: true });

            return info;
        }
    }




});