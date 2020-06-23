$(function () {

    fshowtypealert = (title, text, icon, element, clear) => {
        Swal.fire({
            title: title, text: text, icon: icon,
            showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
            confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
        }).then((acepta) => {
            $("html, body").animate({ scrollTop: $(`#${element.id}`).offset().top - 50 }, 1000);
            if (clear == 1) {
                setTimeout(() => {
                    element.focus();
                    setTimeout(() => { element.value = ""; }, 300);
                }, 1200);
            } else {
                setTimeout(() => { element.focus(); }, 1200);
            }
            if (element.id == "numpla") {
                setTimeout(() => {
                    $("#btn-search-table-num-posicion").click();
                }, 1500);
            }
        });
    };

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

    const icoCloseSearchNivEstructure = document.getElementById('ico-close-search-niv-estructure');
    const btnCloseSearchNivEstructure = document.getElementById('btn-close-search-niv-estructure');
    const btnModalSearchNivEstructure = document.getElementById('btn-modal-search-niv-estructure');

    icoCloseSearchNivEstructure.addEventListener('click', () => { localStorage.removeItem('newnivestructurebtn'); });
    btnCloseSearchNivEstructure.addEventListener('click', () => { localStorage.removeItem('newnivestructurebtn'); });

    const btnNewNivEstructure  = document.getElementById('btn-new-nivel-estructure');
    const nivestructure        = document.getElementById('nivestructure');
    const descnivestructure    = document.getElementById('descnivestructure')
    const btnClearFieldsNEstr  = document.getElementById('btn-clear-fields-nivestructure-save');
    const icoClearFieldsNEstr  = document.getElementById('ico-clear-fields-nivestructure-save');
    const btnsavenivestructure = document.getElementById('btnsavenivestructure');    

    btnNewNivEstructure.addEventListener('click', () => {
        $("#registerdepartament").modal('hide');
        setTimeout(() => {
            nivestructure.focus();
        }, 1000);
    });

    const btnnewnivestructurebtn = document.getElementById('btnnewnivestructurebtn');

    localStorage.removeItem('newnivestructurebtn');

    btnModalSearchNivEstructure.addEventListener('click', () => {
        localStorage.setItem('newnivestructurebtn', 1);
    });

    btnnewnivestructurebtn.addEventListener('click', () => {
        $("#btnsearchnivestructure").modal('hide');
        setTimeout(() => {
            nivestructure.focus();
        }, 1000);
    });

    const nivestuc = document.getElementById('nivestuc');

    const keynivestructureedit  = document.getElementById('keynivestructureedit');
    const nivestructureedit     = document.getElementById('nivestructureedit');
    const descnivestructureedit = document.getElementById('descnivestructureedit');
    const btnsaveeditestructure = document.getElementById('btnsaveeditestructure');
    const btnClearFieldsEditNEs = document.getElementById('btn-clear-fields-nivestructure-edit');
    const icoClearFieldsEditNEs = document.getElementById('ico-clear-fields-nivestructure-edit');

    // Funcion que edita el nivel de estructura seleccionado
    fEditDataNivEstructure = (paramid, paramstr, paramdesc) => {
        try {
            if (parseInt(paramid) != 0) {
                $("#btnsearchnivestructure").modal('hide');
                setTimeout(() => {
                    $("#editnivestructure").modal('show');
                }, 500);
                keynivestructureedit.value = paramid;
                nivestructureedit.value = paramstr;
                descnivestructureedit.value = paramdesc;
                setTimeout(() => {
                    nivestructureedit.focus();
                },1000);
            } else {
                alert('Accion invalida');
                location.reload();
            }
        } catch (error) {
            if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }

    // Funcion que carga los niveles de estructura en la tabla
    fLoadNivEstructureTable = async () => {
        document.getElementById('data-nivestructure').innerHTML = "";
        try {
            await $.ajax({
                url: "../CatalogsTables/NivEstruct",
                type: "POST",
                data: { state: 0, type: 'Active/Desactive', keyniv: 0 },
                success: (data) => {
                    const dataLength = data.length;
                    let   lengthData = 0;
                    for (let i = 0; i < dataLength; i++) {
                        document.getElementById('data-nivestructure').innerHTML += `
                                <tr>
                                    <td>${data[i].sNivelEstructura}</td>
                                    <td>${data[i].sDescripcion}</td>
                                    <td>
                                        <button type="button" class="btn btn-warning btn-sm btn-icon-split shadow" onclick="fEditDataNivEstructure(${data[i].iIdNivelEstructura}, '${data[i].sNivelEstructura}', '${data[i].sDescripcion}')">
                                            <span class="icon text-white-50">
                                                <i class="fas fa-edit"></i>
                                            </span>
                                            <span class="text text-white">Editar</span>
                                        </button>
                                    </td>
                                </tr>
                            `;
                        lengthData += 1;
                    }
                    if (lengthData == dataLength) {
                        $("#dataTableNivEstructure").DataTable({
                            language: spanish,
                            pageLength: 5
                        });
                    }
                }, error: (jqxHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        } catch (error) {
            if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }

    fLoadNivEstructureTable();

    // Funcion que limpia los campos del registro de nivel de estructura
    fClearFieldsNewNivEstructure = () => {
        nivestructure.value     = "";
        descnivestructure.value = "";
        if (localStorage.getItem("newnivestructurebtn") != null) {
            $("#btnsearchnivestructure").modal("show");
        } else if (localStorage.getItem("modalbtndepartament") != null) {
            $("#registerdepartament").modal("show");
        } else {
            $("#registerdepartament").modal("show");
        }
    }

    // Funcion que guarda el nuevo nivel de estructura
    fSaveDataNivEstructure = () => {
        try {
            if (nivestructure.value != "") {
                if (descnivestructure.value != "") {
                    const dataSend = {
                        nivEstructure: nivestructure.value,
                        descNEstructure: descnivestructure.value 
                    };
                    $.ajax({
                        url: "../CatalogsTables/SaveNivEstructure",
                        type: "POST",
                        data: dataSend,
                        beforeSend: () => {
                            btnsavenivestructure.disabled = true;
                            btnClearFieldsNEstr.disabled  = true;
                            icoClearFieldsNEstr.disabled  = true;
                        }, success: (data) => {
                            console.log(data);
                            if (data.Bandera === true && data.MensajeError === "none") {
                                Swal.fire({
                                    title: 'Registro correcto', icon: 'success',
                                    showClass: { popup: 'animated fadeInDown faster' },
                                    hideClass: { popup: 'animated fadeOutUp faster' },
                                    confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                }).then((acepta) => {
                                    floadnivestuc(0, 'Active/Desactive', 0, nivestuc, 0);
                                    $("#newnivelestructure").modal('hide');
                                    let tableData = $("#dataTableNivEstructure").DataTable();
                                    tableData.destroy();
                                    if (JSON.parse(localStorage.getItem('modalbtndepartament')) != null) {
                                        $("#registerdepartament").modal('show');
                                        setTimeout(() => { nivestuc.focus(); }, 1000);
                                    } else if (localStorage.getItem('newnivestructurebtn') != null) {
                                        setTimeout(() => {
                                            $("#btnsearchnivestructure").modal('show');
                                            fLoadNivEstructureTable();
                                        }, 1000);
                                    } else {
                                        $("#registerdepartament").modal('show');
                                        setTimeout(() => { nivestuc.focus(); }, 1000);
                                        fLoadNivEstructureTable();
                                    }
                                });
                            } else {
                                Swal.fire({
                                    title: 'Error', text: 'Contacte a sistemas', icon: 'error',
                                    showClass: { popup: 'animated fadeInDown faster' },
                                    hideClass: { popup: 'animated fadeOutUp faster' },
                                    confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                }).then((acepta) => {
                                    $("#newnivelestructure").modal('hide');
                                    setTimeout(() => {
                                        if (JSON.parse(localStorage.getItem('modalbtndepartament')) != null) {
                                            $("#registerdepartament").modal('show');
                                        }
                                    }, 1000);
                                });
                            }
                            nivestructure.value = "";
                            descnivestructure.value = "";
                            btnsavenivestructure.disabled = false;
                            btnClearFieldsNEstr.disabled  = false;
                            icoClearFieldsNEstr.disabled  = false;
                        }, error: (jqXHR, exception) => {
                            fcaptureaerrorsajax(jqXHR, exception);
                        }
                    });
                } else {
                    fshowtypealert('Atención', 'Completa el campo descripcion estructura', 'info', descnivestructure, 0);
                }
            } else {
                fshowtypealert('Atención', 'Completa el campo nivel estructura', 'info', nivestructure, 0);
            }
        } catch (error) {
            if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }

    // Funcion que guarda los cambios en el nivel de estructura
    fSaveEditDataNivEstructure = () => {
        try {
            if (parseInt(keynivestructureedit.value) != 0) {
                if (nivestructureedit.value != "") {
                    if (descnivestructureedit.value != "") {
                        const dataSend = {
                            keyNivEstructure: parseInt(keynivestructureedit.value),
                            nivEstructure: nivestructureedit.value,
                            descNivEstructure: descnivestructureedit.value
                        };
                        $.ajax({
                            url: "../CatalogsTables/EditNivEstructure",
                            type: "POST",
                            data: dataSend,
                            beforeSend: () => {
                                btnsaveeditestructure.disabled = true;
                                btnClearFieldsEditNEs.disabled = true;
                                icoClearFieldsEditNEs.disabled = true;
                            }, success: (data) => {
                                if (data.Bandera === true && data.MensajeError === "none") {
                                    floadnivestuc(0, 'Active/Desactive', 0, nivestuc, 0);
                                    Swal.fire({
                                        title: 'Registro actualizado',  icon: 'success',
                                        showClass: { popup: 'animated fadeInDown faster' },
                                        hideClass: { popup: 'animated fadeOutUp faster' },
                                        confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                    }).then((acepta) => {
                                        $("#editnivestructure").modal('hide');
                                        let tableData = $("#dataTableNivEstructure").DataTable();
                                        tableData.destroy();
                                        setTimeout(() => {
                                            $("#btnsearchnivestructure").modal('show');
                                            fLoadNivEstructureTable();
                                        }, 1000);
                                    });
                                } else {
                                    Swal.fire({
                                        title: 'Error', text: 'Contacte a sistemas', icon: 'error',
                                        showClass: { popup: 'animated fadeInDown faster' },
                                        hideClass: { popup: 'animated fadeOutUp faster' },
                                        confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                                    }).then((acepta) => {
                                        $("#editnivestructure").modal('hide');
                                        setTimeout(() => {
                                            $("#btnsearchnivestructure").modal('show');
                                        }, 1000);
                                    });
                                }
                                nivestructureedit.value = "";
                                descnivestructureedit.value = "";
                                btnsaveeditestructure.disabled = false;
                                btnClearFieldsEditNEs.disabled = false;
                                icoClearFieldsEditNEs.disabled = false;
                            }, error: (jqXHR, exception) => {
                                fcaptureaerrorsajax(jqXHR, exception);
                            }
                        });
                    } else {
                        fshowtypealert('Atencion', 'Completa el campo descripcion', 'info', descnivestructureedit, 0);
                    }
                } else {
                    fshowtypealert('Atencion', 'Completa el campo nivel estructura', 'info', nivestructureedit, 0);
                }
            } else {
                alert('Accion invalida');
                location.reload();
            }
        } catch (error) {
            if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }

    btnClearFieldsNEstr.addEventListener('click', fClearFieldsNewNivEstructure);
    icoClearFieldsNEstr.addEventListener('click', fClearFieldsNewNivEstructure);

    btnClearFieldsEditNEs.addEventListener('click', () => {
        nivestructureedit.value = "";
        descnivestructureedit.value = "";
        $("#btnsearchnivestructure").modal('show');
    });

    icoClearFieldsEditNEs.addEventListener('click', () => {
        nivestructureedit.value = "";
        descnivestructureedit.value = "";
        $("#btnsearchnivestructure").modal('show');
    });

    btnsavenivestructure.addEventListener('click', fSaveDataNivEstructure);

    btnsaveeditestructure.addEventListener('click', fSaveEditDataNivEstructure);

});