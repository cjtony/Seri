$(function () {
    // Comentar cuando el proyecto este en produccion \\
    //const idefectivo = 1115;
    //const idcuentach = 1116;
    //const idcajeroau = 1117;
    //const idcuentaah = 1118;

    // Descomentar cuando el proyecto este en produccion \\
    const idefectivo = 218;
    const idcuentach = 219;
    const idcajeroau = 220;
    const idcuentaah = 221;

    const navDataGenTab = document.getElementById('nav-datagen-tab'),
        navImssTab = document.getElementById('nav-imss-tab'),
        navDataNomTab = document.getElementById('nav-datanom-tab'),
        navEstructureTab = document.getElementById('nav-estructure-tab');

    //const btnsaveedit = document.getElementById('btn-save-edit');

    const icoedit1 = document.getElementById('icoedit1'),
        icoedit2 = document.getElementById('icoedit2'),
        icoedit3 = document.getElementById('icoedit3'),
        icoedit4 = document.getElementById('icoedit4'),
        txtedit1 = document.getElementById('txtedit1'),
        txtedit2 = document.getElementById('txtedit2'),
        txtedit3 = document.getElementById('txtedit3'),
        txtedit4 = document.getElementById('txtedit4');

    fcleareditstyle = () => {
        icoedit1.classList.remove('text-success');
        icoedit1.classList.remove('text-danger');
        icoedit2.classList.remove('text-success');
        icoedit2.classList.remove('text-danger');
        icoedit3.classList.remove('text-success');
        icoedit3.classList.remove('text-danger');
        icoedit4.classList.remove('text-success');
        icoedit4.classList.remove('text-danger');
        txtedit1.textContent = '';
        txtedit2.textContent = '';
        txtedit3.textContent = '';
        txtedit4.textContent = '';
    }

    const icosave1 = document.getElementById('icosave1'),
        icosave2 = document.getElementById('icosave2'),
        icosave3 = document.getElementById('icosave3'),
        icosave4 = document.getElementById('icosave4'),
        txtsave1 = document.getElementById('txtsave1'),
        txtsave2 = document.getElementById('txtsave2'),
        txtsave3 = document.getElementById('txtsave3'),
        txtsave4 = document.getElementById('txtsave4');

    fclearsavestyle = () => {
        icosave1.classList.remove('text-success');
        icosave1.classList.remove('text-danger');
        icosave2.classList.remove('text-success');
        icosave2.classList.remove('text-danger');
        icosave3.classList.remove('text-success');
        icosave3.classList.remove('text-danger');
        icosave4.classList.remove('text-success');
        icosave4.classList.remove('text-danger');
        txtsave1.textContent = '';
        txtsave2.textContent = '';
        txtsave3.textContent = '';
        txtsave4.textContent = '';
    }

    let objectDataTabDataGen = {},
        objectDataTabImss = {},
        objectDataTabNom = {},
        objectDataTabEstructure = {};

    fformatdate = (date) => {
        const format = date;
        let fyear = format.substring(6, 10);
        let fmont = format.substring(3, 5);
        let fday = format.substring(0, 2);
        let dateret = fyear + "-" + fmont + "-" + fday;
        if (dateret == "1900-01-01") {
            dateret = "";
        }
        return dateret;
    }

    flocalstodatatabgen = () => {
        const dataLocStoGen = {
            key: 'general', data: {
                clvemp: clvemp.value,
                name: name.value, apep: apepat.value,
                apem: apemat.value, fnaci: fnaci.value,
                lnaci: lnaci.value, title: title.value,
                sex: sex.value, nacion: nacion.value,
                estciv: estciv.value, codpost: codpost.value,
                state: state.value, city: city.value,
                colony: colony.value, street: street.value,
                numberst: numberst.value, telfij: telfij.value,
                telmov: telmov.value, mailus: mailus.value
            }
        };
        document.getElementById('icouser').classList.remove('d-none');
        document.getElementById('nameuser').textContent = "Editando al empleado: " + name.value + " " + apepat.value + " " + apemat.value + ".";
        objectDataTabDataGen.datagen = dataLocStoGen;
        localStorage.setItem('objectTabDataGen', JSON.stringify(objectDataTabDataGen));
        localStorage.setItem('tabSelected', 'imss');
        navImssTab.classList.remove('disabled');
        flocalstodatatabimss();
    }

    flocalstodatatabimss = () => {
        const dataLocSto = {
            key: 'imss', data: {
                clvimss: clvimss.value,
                imss: regimss.value,
                rfc: rfc.value,
                curp: curp.value,
                nivest: nivest.value,
                nivsoc: nivsoc.value,
            }
        };
        objectDataTabImss.dataimss = dataLocSto;
        localStorage.setItem('objectDataTabImss', JSON.stringify(objectDataTabImss));
        localStorage.setItem('tabSelected', 'datanom');
        navDataNomTab.classList.remove('disabled');
    }

    flocalstodatatabnomina = () => {
        const dataLocSto = {
            key: 'nom', data: {
                clvnom: clvnom.value,
                numnom: numnom.value, salmen: salmen.value,
                pagret: pagret.value,
                tipemp: tipemp.value, tipmon: tipmon.value,
                nivemp: nivemp.value, tipjor: tipjor.value,
                tipcon: tipcon.value, fecing: fecing.value,
                fecant: fecant.value, vencon: vencon.value,
                //estats: estats.value,
            }
        };
        objectDataTabNom.datanom = dataLocSto;
        localStorage.setItem('objectDataTabNom', JSON.stringify(objectDataTabNom));
        localStorage.setItem('tabSelected', 'dataestructure');
        navEstructureTab.classList.remove('disabled');
    }

    flocalstodatatabstructure = () => {
        const dataLocSto = {
            key: 'estructure', data: {
                clvstr: clvstr.value,
                numpla: numpla.value,
                depaid: depaid.value,
                depart: depart.value,
                puesid: puesid.value,
                pueusu: pueusu.value,
                emprep: emprep.value,
                report: report.value,
                tippag: tippag.value,
                banuse: banuse.value,
                cunuse: cunuse.value,
                clvbank: clvbank.textContent
            }
        };
        objectDataTabEstructure.dataestructure = dataLocSto;
        localStorage.setItem('objectDataTabEstructure', JSON.stringify(objectDataTabEstructure));
        localStorage.setItem('tabSelected', 'dataestructure');
    }

    floaddatatabstructure = (paramid) => {
        try {
            $.ajax({
                url: "../Empleados/DataTabStructureEmploye",
                type: "POST",
                data: { keyemploye: paramid },
                success: (data) => {
                    console.log("Datos de la estructura del empleado con el id: " + String(paramid));
                    console.log(data);
                    if (data.sMensaje === "success") {
                        clvstr.value = data.iIdPosicion;
                        numpla.value = data.iPosicion;
                        puesid.value = data.iPuesto_id;
                        pueusu.value = data.sNombrePuesto;
                        depaid.value = data.iDepartamento_id;
                        depart.value = data.sNombreDepartamento;
                        emprep.value = data.sEmpresaReporta;
                        report.value = data.sReporta;
                        tippag.value = data.sTipoPago;
                        banuse.value = data.iBanco_id;
                        cunuse.value = data.sCuenta;
                        localStorage.setItem('modedit', 'true');
                        document.getElementById('btn-save-data-all').disabled = true;
                        flocalstodatatabstructure();
                        fchecklocalstotab();
                    }
                }, error: (jqXHR, exception) => { fcaptureaerrorsajax(jqXHR, exception); }
            });
        } catch (error) {
            if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }

    floaddatatabnomina = (paramid) => {
        try {
            $.ajax({
                url: "../Empleados/DataTabNominaEmploye",
                type: "POST",
                data: { keyemploye: paramid },
                success: (data) => {
                    console.log('Datos de nomina del empleado con el id: ' + String(paramid));
                    console.log(data);
                    if (data.sMensaje === "success") {
                        clvnom.value = data.iIdNomina;
                        numnom.value = data.iNumeroNomina;
                        salmen.value = data.dSalarioMensual;
                        pagret.value = data.sPagoRetroactivo;
                        tipemp.value = data.iTipoEmpleado_id;
                        tipmon.value = data.sTipoMoneda;
                        nivemp.value = data.iNivelEmpleado_id;
                        tipjor.value = data.iTipoJornada_id;
                        tipcon.value = data.iTipoContrato_id;
                        fecing.value = fformatdate(data.sFechaIngreso);
                        fecant.value = fformatdate(data.sFechaAntiguedad);
                        vencon.value = fformatdate(data.sVencimientoContrato);
                        //estats.value = data.sTipoAlta;
                        floaddatatabstructure(paramid);
                        flocalstodatatabnomina();
                    }
                }, error: (jqXHR, exception) => { fcaptureaerrorsajax(jqXHR, exception); }
            });
        } catch (error) {
            if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }

    floaddatatabimss = (paramid) => {
        try {
            $.ajax({
                url: "../Empleados/DataTabImssEmploye",
                type: "POST",
                data: { keyemploye: paramid },
                success: (data) => {
                    console.log('Datos del imss del empleado con el id: ' + String(paramid));
                    console.log(data);
                    if (data.sMensaje === "success") {
                        clvimss.value = data.iIdImss;
                        regimss.value = data.sRegistroImss;
                        rfc.value = data.sRfc;
                        curp.value = data.sCurp;
                        nivest.value = data.iNivelEstudio_id;
                        nivsoc.value = data.iNivelSocioeconomico_id;
                        floaddatatabnomina(paramid);
                        flocalstodatatabimss();
                    }
                }, error: (jqXHR, exception) => { fcaptureaerrorsajax(jqXHR, exception); }
            });
        } catch (error) {
            if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }

    floaddatatabgeneral = (paramid) => {
        try {
            $.ajax({
                url: "../Empleados/DataTabGenEmploye",
                type: "POST",
                data: { keyemploye: paramid },
                success: (data) => {
                    console.log('Datos del empleado con el id: ' + String(paramid));
                    console.log(data);
                    if (data.sMensaje === "success") {
                        clvemp.value = data.iIdEmpleado;
                        name.value = data.sNombreEmpleado;
                        apepat.value = data.sApellidoPaterno;
                        apemat.value = data.sApellidoMaterno;
                        fnaci.value = fformatdate(data.sFechaNacimiento);
                        lnaci.value = data.sLugarNacimiento;
                        title.value = data.iTitulo_id;
                        sex.value = data.iGeneroEmpleado;
                        nacion.value = data.sNacionalidad;
                        estciv.value = data.iEstadoCivil;
                        codpost.value = data.sCodigoPostal;
                        state.value = data.iEstado_id;
                        city.value = data.sCiudad;
                        street.value = data.sCalle;
                        numberst.value = data.sNumeroCalle;
                        telfij.value = data.sTelefonoFijo;
                        telmov.value = data.sTelefonoMovil;
                        mailus.value = data.sCorreoElectronico;
                        fvalidatestatecodpost();
                        setTimeout(() => {
                            floaddatatabimss(paramid);
                            flocalstodatatabimss();
                            colony.value = data.iColonia_id;
                            flocalstodatatabgen();
                        }, 1200);
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        } catch (error) {
            if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }

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
            "next": "<button class='ml-2 btn btn-sm btn-primary'>Siguiente </button>",
            "previous": "<button class='mr-2 btn btn-sm btn-primary'>Anterior</button>"
        }
    };

    const tableEmployes = $("#table-employes").DataTable({
        responsive: true,
        destroy: true,
        ajax: {
            method: "POST",
            url: "../Empleados/LoadEmployes",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            dataSrc: "data"
        },
        columns: [
            { "data": "sNombreEmpleado" },
            { "data": "iNumeroNomina" },
            { "defaultContent": "<button class='btn btn-outline-warning text-center shadow rounded' title='Editar usuario'> <i class='fas fa-edit'></i> </button>" }
        ],
        language: spanish
    });


    $("#table-employes tbody").on('click', 'button', function () {
        var data = tableEmployes.row($(this).parents('tr')).data();
        Swal.fire({
            title: 'Esta seguro?', text: 'De editar a: ' + data.sNombreEmpleado + '?', icon: 'question',
            showClass: { popup: 'animated fadeInDown faster' }, hideClass: { popup: 'animated fadeOutUp faster' },
            confirmButtonText: "Aceptar", showCancelButton: true, cancelButtonText: "Cancelar",
            allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
        }).then((acepta) => {
            if (acepta.value) {
                $("#searchemploye").modal('hide');
                localStorage.removeItem('tabSelected');
                localStorage.removeItem('objectTabDataGen');
                localStorage.removeItem('objectDataTabImss');
                localStorage.removeItem('objectDataTabNom');
                localStorage.removeItem('objectDataTabEstructure');
                let timerInterval;
                Swal.fire({
                    title: 'Cargando información',
                    html: 'Terminando en <b></b> segundos.',
                    timer: 5000, timerProgressBar: true,
                    onBeforeOpen: () => {
                        Swal.showLoading();
                        timerInterval = setInterval(() => {
                            Swal.getContent().querySelector('b').textContent = Swal.getTimerLeft();
                        }, 100)
                    },
                    onClose: () => { clearInterval(timerInterval); }
                }).then((result) => {
                    if (result.dismiss === Swal.DismissReason.timer) {
                        floaddatatabgeneral(data.iIdEmpleado);
                        setTimeout(() => {
                            $("#nav-datagen-tab").click();
                        }, 1000);
                    }
                })
            }
        });
    });

    /*
     * Variables apartado datos generales
     */
    const clvemp = document.getElementById('clvemp');
    const name = document.getElementById('name');
    const apepat = document.getElementById('apepat');
    const apemat = document.getElementById('apemat');
    const sex = document.getElementById('sex');
    const estciv = document.getElementById('estciv');
    const fnaci = document.getElementById('fnaci');
    const lnaci = document.getElementById('lnaci');
    const title = document.getElementById('title');
    const nacion = document.getElementById('nacion');
    const state = document.getElementById('state');
    const codpost = document.getElementById('codpost');
    const city = document.getElementById('city');
    const colony = document.getElementById('colony');
    const street = document.getElementById('street');
    const numberst = document.getElementById('numberst');
    const telfij = document.getElementById('telfij');
    const telmov = document.getElementById('telmov');
    const mailus = document.getElementById('mailus');
    const tipsan = document.getElementById('tipsan');
    const fecmat = document.getElementById('fecmat');

    /*
     * Variables de botones 
     */
    //const btnsavedata = document.getElementById('btn-save-data');
    const btnsavedataall = document.getElementById('btn-save-data-all');

    /*
     * Funciones para enviar los datos a la bd 
     */

    fsavedatageneral = () => {
        const dataSend = {
            name: name.value, apepat: apepat.value, apemat: apemat.value, sex: sex.value, estciv: estciv.value, fnaci: fnaci.value, lnaci: lnaci.value,
            title: title.value, nacion: nacion.value, state: state.value, codpost: codpost.value, city: city.value, colony: colony.value, street: street.value,
            numberst: numberst.value, telfij: telfij.value, telmov: telmov.value, email: mailus.value, tipsan: tipsan.value, fecmat: fecmat.value
        };
        console.log('Datos generales');
        console.log(dataSend);
        try {
            document.getElementById('txtsave1').textContent = 'Guardando...';
            $.ajax({
                url: "../SaveDataGeneral/DataGeneral",
                type: "POST",
                data: dataSend,
                success: (data) => {
                    if (data.sMensaje == "success") {
                        icosave1.classList.add('text-success');
                        txtsave1.textContent = 'Guardado';
                        console.log('Guardado datos generales');
                        setTimeout(() => {
                            fsavedataimss();
                        }, 1500);
                    } else if (data.sMensaje == "empexists") {
                        Swal.fire({
                            title: 'Atencion', text: 'El empleado ' + String(name.value) + " " + String(apepat.value) + " " + String(apemat.value) + ' ya se encuentra registrado.', icon: 'warning',
                            showClass: { popup: 'animated fadeInDown faster' },
                            hideClass: { popup: 'animated fadeOutUp faster' },
                            confirmButtonText: "Aceptar", allowOutsideClick: false, allowEscapeKey: false, allowEnterKey: false,
                        }).then((acepta) => {
                            $("#nav-datagen-tab").click();
                            setTimeout(() => { name.focus(); }, 1000);
                        });
                    } else {
                        icosave1.classList.add('text-danger');
                        txtsave1.textContent = 'Error';
                        setTimeout(() => {
                            $("#processsave").modal('hide');
                            fclearsavestyle();
                        }, 1500);
                        console.log(data);
                    }
                }, error: (jqXHR, exception) => { fcaptureaerrorsajax(jqXHR, exception); }
            });
        } catch (error) {
            if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }

    /*
     * Variables apartado imss
     */
    const clvimss = document.getElementById('clvimss');
    const fecefe = document.getElementById('fecefe');
    const regimss = document.getElementById('regimss');
    const rfc = document.getElementById('rfc');
    const curp = document.getElementById('curp');
    const nivest = document.getElementById('nivest');
    const nivsoc = document.getElementById('nivsoc');

    /*
     * Funcion que guarda los datos del imss
     */
    fsavedataimss = () => {
        const dataSend = {
            fecefe: fecefe.value, regimss: regimss.value, rfc: rfc.value, curp: curp.value, nivest: nivest.value, nivsoc: nivsoc.value, empleado: name.value,
            apepat: apepat.value, apemat: apemat.value, fechanaci: fnaci.value
        };
        console.log('Datos de imss');
        console.log(dataSend);
        try {
            document.getElementById('txtsave2').textContent = 'Guardando...';
            $.ajax({
                url: "../SaveDataGeneral/DataImss",
                type: "POST",
                data: dataSend,
                success: (data) => {
                    if (data.result == "success") {
                        icosave2.classList.add('text-success');
                        txtsave2.textContent = 'Guardado';
                        console.log('Guardado imss');
                        setTimeout(() => {
                            fsavedatanomina();
                        }, 1500);
                    } else {
                        icosave2.classList.add('text-danger');
                        txtsave2.textContent = 'Error!';
                        setTimeout(() => {
                            $("#processsave").modal('hide');
                            fclearsavestyle();
                        }, 1500);
                        console.log(data);
                    }
                }, error: (jqXHR, exception) => { fcaptureaerrorsajax(jqXHR, exception); }
            });
        } catch (error) {
            if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }

    /*
     * Variables del apartado datos de nomina
     */
    const clvnom = document.getElementById('clvnom');
    const fecefecnom = document.getElementById('fecefecnom');
    const salmen = document.getElementById('salmen');
    const tipper = document.getElementById('tipper');
    const tipemp = document.getElementById('tipemp');
    const tipmon = document.getElementById('tipmon');
    const nivemp = document.getElementById('nivemp');
    const tipjor = document.getElementById('tipjor');
    const tipcon = document.getElementById('tipcon');
    const fecing = document.getElementById('fecing');
    const fecant = document.getElementById('fecant');
    const vencon = document.getElementById('vencon');
    const tipcontra = document.getElementById('tipcontra');
    const tippag = document.getElementById('tippag');
    const banuse = document.getElementById('banuse');
    const cunuse = document.getElementById('cunuse');
    const clvstr = document.getElementById('clvstr');
    const tiposueldo = document.getElementById('tiposueldo');
    const politica   = document.getElementById('politica');
    const diferencia = document.getElementById('diferencia');
    const transporte = document.getElementById('transporte');
    const retroactivo = document.getElementById('retroactivo');
    const conFondo = document.getElementById('con_fondo');
    const categoriaEm = document.getElementById('categoria_emp');
    const pagoPorEmpl = document.getElementById('pago_por');

    /*
     * Funcion que guarda los datos del apartado datos de nomina
     */
    fsavedatanomina = () => {
        let banco;
        if (banuse.value == "0") {
            banco = 999;
        } else {
            banco = banuse.value;
        }
        let retroactivoSend = 0;
        if (retroactivo.checked) {
            retroactivoSend = 1;
        }
        let conFondoSend = 0;
        if (conFondo.checked) {
            conFondoSend = 1;
        }
        const dataSend = {
            fecefecnom: fecefecnom.value, salmen: salmen.value, tipemp: tipemp.value, nivemp: nivemp.value,
            tipjor: tipjor.value, tipcon: tipcon.value, fecing: fecing.value, fecant: fecant.value, vencon: vencon.value,
            //estats: estats.value,
            empleado: name.value, apepat: apepat.value, apemat: apemat.value, fechanaci: fnaci.value, tipper: tipper.value, tipcontra: tipcontra.value,
            //motinc: motinc.value,
            tippag: tippag.value, banuse: banco, cunuse: cunuse.value, position: clvstr.value, clvemp: 0, tiposueldo: tiposueldo.value, politica: politica.value,
            diferencia: diferencia.value, transporte: transporte.value, retroactivo: retroactivoSend, flagSal: false, motMoviSal: "none", fechMoviSal: "none", salmenact: 0.00, categoria: categoriaEm.value, pagopor: pagoPorEmpl.value, fondo: conFondoSend
        };
        //console.log('Datos de nomina');
        //console.log(dataSend);
        try {
            document.getElementById('txtsave3').textContent = 'Guardando';
            console.log(dataSend);
            $.ajax({
                url: "../SaveDataGeneral/DataNomina",
                type: "POST",
                data: dataSend,
                success: (data) => {
                    if (data.Bandera === true && data.MensajeError === "none") {
                        icosave3.classList.add('text-success');
                        txtsave3.textContent = 'Guardado';
                        setTimeout(() => {
                            fsavedataestructure();
                        }, 1500);
                        console.log('Guardo nomina!');
                    } else {
                        icosave3.classList.add('text-danger');
                        txtsave3.textContent = 'Error!';
                        setTimeout(() => {
                            $("#processsave").modal('hide');
                            fclearsavestyle();
                        }, 1500);
                        console.log(data);
                    }
                }, error: (jqXHR, exception) => { fcaptureaerrorsajax(jqXHR, exception); }
            });
        } catch (error) {
            if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }

    /*
     * Variables apartado estructura
     */
    const fechefectpos = document.getElementById('fechefectpos');
    const fechinipos = document.getElementById('fechinipos');
    const numpla = document.getElementById('numpla');
    const depaid = document.getElementById('depaid');
    const depart = document.getElementById('depart');
    const puesid = document.getElementById('puesid');
    const pueusu = document.getElementById('pueusu');
    const emprep = document.getElementById('emprep');
    const report = document.getElementById('report');

    /*
     * Funcion que guarda los datos del apartado estructura
     */
    fsavedataestructure = () => {
        var acepta = 0;
        const dataSend = {
            clvstr: clvstr.value, fechefectpos: fechefectpos.value, fechinipos: fechinipos.value,
            empleado: name.value, apepat: apepat.value, apemat: apemat.value, fechanaci: fnaci.value
        };
        try {
            document.getElementById('txtsave4').textContent = 'Guardando';
            fsearchpositionsig();
            $.ajax({
                url: "../SaveDataGeneral/DataEstructura",
                type: "POST",
                data: dataSend,
                success: (data) => {
                    if (data.result == "success") {
                        icosave4.classList.add('text-success');
                        txtsave4.textContent = 'Guardado';
                        document.getElementById('successdiv').classList.remove('d-none');
                        console.log('Guardado todo');
                        setTimeout(() => {
                            $("#processsave").modal('hide');
                            document.getElementById('successdiv').classList.add('d-none');
                            tableEmployes.ajax.reload();
                            fclearlocsto(2);
                            fclearsavestyle();
                        }, 1500);
                    } else {
                        console.log(data);
                        setTimeout(() => {
                            $("#processsave").modal('hide');
                            fclearsavestyle();
                        }, 1500);
                    }
                }, error: (jqXHR, exception) => { fcaptureaerrorsajax(jqXHR, exception); }
            });
        } catch (error) {
            if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else {
                console.log('Error ', error);
            }
        }
        return acepta;
    }

    /*
     * Ejecucion del guardado de los datos 
     */
    //btnsavedata.addEventListener('click', fsavedataestructure);

    /*
     * Funcion que guarda los datos generales del empleado
     */

    fsavedataall = () => {
        try {
            let validatedatagen = 0, validatedataimss = 0, validatedatanom = 0, validatedatastru = 0;
            const arrInput = [name, apepat, sex, estciv, fnaci, lnaci, title, nacion, state];
            for (let a = 0; a < arrInput.length; a++) {
                if (arrInput[a].hasAttribute('tp-select')) {
                    if (arrInput[a].value == '0') {
                        const attrselect = arrInput[a].getAttribute('tp-select');
                        fshowtypealert('Atencion', 'Selecciona una opción de ' + String(attrselect), 'warning', arrInput[a], 0);
                        validatedatagen = 1;
                        break;
                    }
                    if (arrInput[a].id == 'state' && arrInput[a].value != '0') {
                        arrInput.push(codpost);
                    }
                } else {
                    if (arrInput[a].hasAttribute('tp-date')) {
                        const attrdate = arrInput[a].getAttribute('tp-date');
                        if (arrInput[a].value != "" && attrdate == 'less') {
                            const ds = new Date();
                            const fechAct = ds.getFullYear() + "-" + (ds.getMonth() + 1) + "-" + ds.getDate();
                            if (arrInput[a].value > fechAct) {
                                fshowtypealert('Atencion', 'La fecha de nacimiento ' + arrInput[a].value + ' es incorrecta, no debe de ser mayor a la fecha actual', 'warning', arrInput[a], 1);
                                validatedatagen = 1;
                                break;
                            }
                        }
                        else {
                            fshowtypealert('Atencion', 'Completa el campo ' + String(arrInput[a].placeholder), 'warning', arrInput[a], 0);
                            validatedatagen = 1;
                            break;
                        }
                    } else {
                        if (arrInput[a].value == '') {
                            fshowtypealert('Atencion', 'Completa el campo ' + String(arrInput[a].placeholder), 'warning', arrInput[a], 0);
                            validatedatagen = 1;
                            break;
                        }
                    }
                    if (arrInput[a].id == 'codpost' && arrInput[a].value.length < 5 || arrInput[a].id == 'codpost' && arrInput[a].value.length > 5) {
                        fshowtypealert('Atencion', 'La longitud del codigo postal debe de ser de 5 digitos', 'warning', arrInput[a], 1);
                        validatedatagen = 1;
                        break;
                    }
                    if (arrInput[a].id == 'codpost' && arrInput[a].value.length == 5) {
                        arrInput.push(colony, street, telmov, mailus);
                    }
                    if (arrInput[a].id == 'mailus' && arrInput[a].value != "") {
                        const emailvalidate = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
                        if (!emailvalidate.test(arrInput[a].value)) {
                            fshowtypealert('Atencion', 'El correo ingresado ' + arrInput[a].value + ' no contiene un formato valido', 'warning', arrInput[a], 1);
                            validatedatagen = 1;
                            break;
                        }
                    }
                }
            }
            if (validatedatagen == 0) {
                const arrInput = [regimss, rfc, curp, nivest, nivsoc];
                for (let i = 0; i < arrInput.length; i++) {
                    if (arrInput[i].hasAttribute("tp-select")) {
                        if (arrInput[i].value == "0") {
                            const attrselect = arrInput[i].getAttribute('tp-select');
                            fshowtypealert('Atención', 'Selecciona una opción de ' + String(attrselect), 'warning', arrInput[i], 0);
                            validatedataimss = 1;
                            break;
                        }
                    } else {
                        if (arrInput[i].value == "") {
                            fshowtypealert('Atención', 'Completa el campo ' + arrInput[i].placeholder, 'warning', arrInput[i], 0);
                            validatedataimss = 1;
                            break;
                        }
                    }
                }
                if (validatedataimss == 0) {
                    const arrInput = [fecefecnom, salmen, tipper, tipemp, nivemp, tipjor, tipcon, fecing, fecant, tipcontra, tiposueldo, politica, diferencia, transporte, tippag, categoriaEm, pagoPorEmpl];
                    for (let t = 0; t < arrInput.length; t++) {
                        if (arrInput[t].hasAttribute("tp-select")) {
                            let textpag;
                            if (arrInput[t].value == "n") {
                                const attrselect = arrInput[t].getAttribute('tp-select');
                                fshowtypealert('Atención', 'Selecciona una opción de ' + String(attrselect), 'warning', arrInput[t], 0);
                                validatedatanom = 1;
                                break;
                            }
                            if (arrInput[t].id == "tippag") {
                                textpag = $('select[id="tippag"] option:selected').text();
                            }
                            if (arrInput[t].value == idcuentach || arrInput[t].value == idcuentaah) {
                                arrInput.push(banuse, cunuse);
                            }
                            if (arrInput[t].id == "tipemp" && arrInput[t].value == 76) {
                                arrInput.push(vencon);
                            }
                            if (arrInput[t].value == idcuentach) {
                                if (cunuse.value.length < 10) {
                                    fshowtypealert('Atencion', 'El numero de cuenta de cheques debe contener 10 digitos y solo tiene ' + String(cunuse.value.length) + ' digitos.', 'warning', cunuse, 0);
                                    validatedatanom = 1;
                                    break;
                                }
                            }
                            if (arrInput[t].value == idcuentaah) {
                                if (cunuse.value.length < 15) {
                                    fshowtypealert('Atención', 'El numero de cuenta de ahorro debe de contener 18 digitos y solo tiene ' + String(cunuse.value.length) + ' digitos.', 'warning', cunuse, 0);
                                    validatedatanom = 1;
                                    break;
                                }
                            }
                        } else {
                            if (arrInput[t].hasAttribute("tp-date")) {
                                const attrdate = arrInput[t].getAttribute("tp-date");
                                const d = new Date();
                                let fechAct;
                                if (d.getMonth() + 1 < 10) {
                                    fechAct = d.getFullYear() + "-" + "0" + (d.getMonth() + 1) + "-" + d.getDate();
                                } else {
                                    fechAct = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();
                                }
                            } else {
                                if (arrInput[t].value == "") {
                                    fshowtypealert('Atención', 'Completa el campo ' + arrInput[t].placeholder, 'warning', arrInput[t], 0);
                                    validatedatanom = 1;
                                    break;
                                }
                            }
                        }
                    }
                    if (fecant.value != "" && fecing.value != "") {
                        if (fecant.value > fecing.value) {
                            fshowtypealert('Atención', 'La fecha de antiguedad no puede ser mayor a la fecha de ingreso', 'warning', fecant, 0);
                            validatedatanom = 1;
                        }
                    }
                    if (validatedatanom == 0) {
                        const arrInput = [numpla, depart, pueusu, emprep, report, tippag];
                        for (let i = 0; i < arrInput.length; i++) {
                            if (arrInput[i].hasAttribute('tp-select')) {
                                if (arrInput[i].value == "0") {
                                    const attrselect = arrInput[i].getAttribute('tp-select');
                                    fshowtypealert('Atención', 'Selecciona una opción para ' + String(attrselect), 'warning', arrInput[i], 0);
                                    validatedatastru = 1;
                                    break;
                                }
                                if (arrInput[i].value == "Cuenta cheques" || arrInput[i].value == "Cuenta clabe") {
                                    arrInput.push(banuse, cunuse);
                                }
                            } else {
                                if (arrInput[i].value == "") {
                                    fshowtypealert('Atención', 'Completa el campo ' + arrInput[i].placeholder, 'warning', arrInput[i], 0);
                                    validatedatastru = 1;
                                    break;
                                }
                            }
                        }
                        if (validatedatastru == 0) {
                            const arrInput = [clvstr, fechefectpos, fechinipos];
                            let validateSend = 0;
                            for (let a = 0; a < arrInput.length; a++) {
                                if (arrInput[a].hasAttribute('tp-date')) {
                                    const attrdate = arrInput[a].getAttribute('tp-date');
                                    if (arrInput[a].value != "" && attrdate == 'higher') {
                                        const ds = new Date();
                                        let fechAct;
                                        let datetod;
                                        if (ds.getDate() < 10) {
                                            datetod = "0" + ds.getDate();
                                        } else {
                                            datetod = ds.getDate();
                                        }
                                        if (ds.getMonth() + 1 < 10) {
                                            fechAct = ds.getFullYear() + "-" + "0" + (ds.getMonth() + 1) + "-" + datetod;
                                        } else {
                                            fechAct = ds.getFullYear() + "-" + (ds.getMonth() + 1) + "-" + datetod;
                                        }
                                    }
                                    else {
                                        fshowtypealert('Atencion', 'Completa el campo ' + String(arrInput[a].placeholder), 'warning', arrInput[a], 0);
                                        validateSend = 1;
                                        break;
                                    }
                                } else {
                                    if (arrInput[a].value == '') {
                                        fshowtypealert('Atencion', 'Completa el campo ' + String(arrInput[a].placeholder), 'warning', arrInput[a], 0);
                                        validateSend = 1;
                                        break;
                                    }
                                }
                            }
                            if (validateSend == 0) {
                                $("#processsave").modal('show');
                                setTimeout(() => {
                                    fsavedatageneral();
                                }, 1000);
                            }
                        } else { $("#nav-estructure-tab").click(); }
                    } else { $("#nav-datanom-tab").click(); }
                } else { $("#nav-imss-tab").click(); }
            } else { $("#nav-datagen-tab").click(); }
        } catch (error) {
            if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }


    btnsavedataall.addEventListener('click', () => {
        $.ajax({
            url: "../SaveDataGeneral/ValidateEmployeeReg",
            type: "POST",
            data: { fieldCurp: curp.value, fieldRfc: rfc.value },
            success: (data) => {
                if (data.Bandera === true && data.MensajeError === "none") {
                    fsavedataall();
                } else {
                    fshowtypealert('Atención', 'Los datos del curp y rfc ya se encuentran registrados', 'warning', curp, 0);
                    fclearlocsto();
                }
            }, error: (jqXHR, exception) => {
                fcaptureaerrorsajax(jqXHR, exception);
            }
        });
    });

    fsaveeditdatastructure = () => {
        var acepta = 0;
        let banco;
        if (banuse.value == "0") {
            banco = 999;
        } else {
            banco = banuse.value;
        }
        const dataSendEditNomina = {
            numpla: numpla.value, depaid: depaid.value, puesid: puesid.value, emprep: emprep.value, report: report.value, tippag: tippag.value,
            banuse: banco, cunuse: cunuse.value, clvstr: clvstr.value
        };
        try {
            $.ajax({
                url: "../EditDataGeneral/EditDataStructure",
                type: "POST",
                data: dataSendEditNomina,
                success: (data) => {
                    if (data.result === "success") {
                        icoedit4.classList.add('text-success');
                        txtedit4.textContent = 'Correcto!';
                        document.getElementById('successeditdiv').classList.remove('d-none');
                        setTimeout(() => {
                            tableEmployes.ajax.reload();
                            fclearlocsto();
                            $("#processedit").modal('hide');
                            document.getElementById('successeditdiv').classList.add('d-none');
                            fclearsavestyle();
                        }, 1500);
                    } else {
                        icoedit4.classList.add('text-danger');
                        txtedit4.textContent = 'Error!';
                        setTimeout(() => {
                            $("#processedit").modal('hide');
                            fcleareditstyle();
                        }, 1500);
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        } catch (error) {
            if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }

    fsaveeditdatanomina = () => {
        const dataSendNominaEdit = {
            numnom: numnom.value, salmen: salmen.value, pagret: pagret.value, tipemp: tipemp.value, tipmon: tipmon.value, nivemp: nivemp.value,
            tipjor: tipjor.value, tipcon: tipcon.value, fecing: fecing.value, fecant: fecant.value, vencon: vencon.value,
            //estats: estats.value,
            clvnom: clvnom.value
        };
        try {
            $.ajax({
                url: "../EditDataGeneral/EditDataNomina",
                type: "POST",
                data: dataSendNominaEdit,
                success: (data) => {
                    if (data.result === "success") {
                        icoedit3.classList.add('text-success');
                        txtedit3.textContent = 'Correcto!';
                        setTimeout(() => {
                            fsaveeditdatastructure();
                        }, 1000);
                    } else {
                        icoedit3.classList.add('text-danger');
                        txtedit3.textContent = 'Error!';
                        setTimeout(() => {
                            $("#processedit").modal('hide');
                            fcleareditstyle();
                        }, 1500);
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            });
        } catch (error) {
            if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }

    fsaveeditdataimss = () => {
        const dataSendImssEdit = {
            regimss: regimss.value, rfc: rfc.value, curp: curp.value, nivest: nivest.value, nivsoc: nivsoc.value, clvimss: clvimss.value
        };
        try {
            $.ajax({
                url: "../EditDataGeneral/EditDataImss",
                type: "POST",
                data: dataSendImssEdit,
                success: (data) => {
                    if (data.result === "success") {
                        icoedit2.classList.add('text-success');
                        txtedit2.textContent = 'Correcto!';
                        setTimeout(() => {
                            fsaveeditdatanomina();
                        }, 1000);
                    } else {
                        icoedit2.classList.add('text-danger');
                        txtedit2.textContent = 'Error!';
                        setTimeout(() => {
                            $("#processedit").modal('hide');
                            fcleareditstyle();
                        }, 1500);
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            })
        } catch (error) {
            if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }

    fsaveeditdatagen = () => {
        const dataSendGenEdit = {
            name: name.value, apepat: apepat.value, apemat: apemat.value, sex: sex.value, estciv: estciv.value, fnaci: fnaci.value, lnaci: lnaci.value,
            title: title.value, nacion: nacion.value, state: state.value, codpost: codpost.value, city: city.value, colony: colony.value, street: street.value,
            numberst: numberst.value, telfij: telfij.value, telmov: telmov.value, email: mailus.value, clvemp: clvemp.value
        };
        try {
            $.ajax({
                url: "../EditDataGeneral/EditDataGeneral",
                type: "POST",
                data: dataSendGenEdit,
                success: (data) => {
                    if (data.result === "success") {
                        icoedit1.classList.add('text-success');
                        txtedit1.textContent = "Correcto!";
                        setTimeout(() => {
                            fsaveeditdataimss();
                        }, 1000);
                    } else {
                        icoedit1.classList.add('text-danger');
                        txtedit1.textContent = "Error!";
                        setTimeout(() => {
                            $("#processedit").modal('hide');
                            fcleareditstyle();
                        }, 1500);
                    }
                }, error: (jqXHR, exception) => {
                    fcaptureaerrorsajax(jqXHR, exception);
                }
            })
        } catch (error) {
            if (error instanceof TypeError) {
                console.log('TypeError ', error);
            } else if (error instanceof EvalError) {
                console.log('EvalError ', error);
            } else if (error instanceof RangeError) {
                console.log('RangeError ', error);
            } else {
                console.log('Error ', error);
            }
        }
    }

    fvalidatefieldsedit = () => {
        let validatedatagen = 0, validatedataimss = 0, validatedatanom = 0, validatedatastru = 0;
        const arrInput = [name, apepat, sex, estciv, fnaci, lnaci, title, nacion, state];
        for (let a = 0; a < arrInput.length; a++) {
            if (arrInput[a].hasAttribute('tp-select')) {
                if (arrInput[a].value == '0') {
                    const attrselect = arrInput[a].getAttribute('tp-select');
                    fshowtypealert('Atencion', 'Selecciona una opción de ' + String(attrselect), 'warning', arrInput[a], 0);
                    validatedatagen = 1;
                    break;
                }
                if (arrInput[a].id == 'state' && arrInput[a].value != '0') {
                    arrInput.push(codpost);
                }
            } else {
                if (arrInput[a].hasAttribute('tp-date')) {
                    const attrdate = arrInput[a].getAttribute('tp-date');
                    if (arrInput[a].value != "" && attrdate == 'less') {
                        const ds = new Date();
                        const fechAct = ds.getFullYear() + "-" + (ds.getMonth() + 1) + "-" + ds.getDate();
                        if (arrInput[a].value > fechAct) {
                            fshowtypealert('Atencion', 'La fecha de nacimiento ' + arrInput[a].value + ' es incorrecta, no debe de ser mayor a la fecha actual', 'warning', arrInput[a], 1);
                            validatedatagen = 1;
                            break;
                        }
                    }
                    else {
                        fshowtypealert('Atencion', 'Completa el campo ' + String(arrInput[a].placeholder), 'warning', arrInput[a], 0);
                        validatedatagen = 1;
                        break;
                    }
                } else {
                    if (arrInput[a].value == '') {
                        fshowtypealert('Atencion', 'Completa el campo ' + String(arrInput[a].placeholder), 'warning', arrInput[a], 0);
                        validatedatagen = 1;
                        break;
                    }
                }
                if (arrInput[a].id == 'codpost' && arrInput[a].value.length < 5 || arrInput[a].id == 'codpost' && arrInput[a].value.length > 5) {
                    fshowtypealert('Atencion', 'La longitud del codigo postal debe de ser de 5 digitos', 'warning', arrInput[a], 1);
                    validatedatagen = 1;
                    break;
                }
                if (arrInput[a].id == 'codpost' && arrInput[a].value.length == 5) {
                    arrInput.push(colony, street, telmov, mailus);
                }
                if (arrInput[a].id == 'mailus' && arrInput[a].value != "") {
                    const emailvalidate = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
                    if (!emailvalidate.test(arrInput[a].value)) {
                        fshowtypealert('Atencion', 'El correo ingresado ' + arrInput[a].value + ' no contiene un formato valido', 'warning', arrInput[a], 1);
                        validatedatagen = 1;
                        break;
                    }
                }
            }
        }
        if (validatedatagen == 0) {
            console.log('Listo para guardar edicion datos generales');
            const arrInput = [regimss, rfc, curp, nivest, nivsoc];
            for (let i = 0; i < arrInput.length; i++) {
                if (arrInput[i].hasAttribute("tp-select")) {
                    if (arrInput[i].value == "0") {
                        const attrselect = arrInput[i].getAttribute('tp-select');
                        fshowtypealert('Atención', 'Selecciona una opción de ' + String(attrselect), 'warning', arrInput[i], 0);
                        validatedataimss = 1;
                        break;
                    }
                } else {
                    if (arrInput[i].value == "") {
                        fshowtypealert('Atención', 'Completa el campo ' + arrInput[i].placeholder, 'warning', arrInput[i], 0);
                        validatedataimss = 1;
                        break;
                    }
                }
            }
            if (validatedataimss == 0) {
                console.log('Listo para guardar edicion datos imss');
                const arrInput = [numnom, salmen, pagret, tipemp, nivemp, tipjor, tipcon, fecing];
                for (let t = 0; t < arrInput.length; t++) {
                    if (arrInput[t].hasAttribute("tp-select")) {
                        if (arrInput[t].value == "0") {
                            const attrselect = arrInput[t].getAttribute('tp-select');
                            fshowtypealert('Atención', 'Selecciona una opción de ' + String(attrselect), 'warning', arrInput[t], 0);
                            validatedatanom = 1;
                            break;
                        }
                        if (arrInput[t].id == "tipemp" && arrInput[t].value == 76) {
                            arrInput.push(vencon);
                        }
                    } else {
                        if (arrInput[t].hasAttribute("tp-date")) {
                            const attrdate = arrInput[t].getAttribute("tp-date");
                            const d = new Date();
                            const fechAct = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();
                            if (arrInput[t].value != "" && attrdate == "less") {
                                if (arrInput[t].value > fechAct) {
                                    fshowtypealert('Atención', 'La ' + arrInput[t].placeholder + ' seleccionada ' + arrInput[t].value + ' no puede ser mayor a la fecha actual', 'warning', arrInput[t], 1);
                                    validatedatanom = 1;
                                    break;
                                }
                            } else if (arrInput[t].value != "" && attrdate == "higher") {
                                //if (arrInput[t].value < fechAct) {
                                //    fshowtypealert('Atención', 'La fecha de ' + arrInput[t].placeholder + ' seleccionada ' + arrInput[t].value + ' no puede ser menor a la fecha actual', 'warning', arrInput[t], 1);
                                //    validatedatanom = 1;
                                //    break;
                                //}
                            } else {
                                fshowtypealert('Atención', 'Completa el campo ' + String(arrInput[t].placeholder), 'warning', arrInput[t], 0);
                                validatedatanom = 1;
                                break;
                            }
                        } else {
                            if (arrInput[t].value == "") {
                                fshowtypealert('Atención', 'Completa el campo ' + arrInput[t].placeholder, 'warning', arrInput[t], 0);
                                validatedatanom = 1;
                                break;
                            }
                        }
                    }
                }
                if (validatedatanom == 0) {
                    console.log('Listo para editar datos de nomina');
                    const arrInput = [numpla, depart, pueusu, emprep, report, tippag];
                    for (let i = 0; i < arrInput.length; i++) {
                        if (arrInput[i].hasAttribute('tp-select')) {
                            if (arrInput[i].value == "0") {
                                const attrselect = arrInput[i].getAttribute('tp-select');
                                fshowtypealert('Atención', 'Selecciona una opción para ' + String(attrselect), 'warning', arrInput[i], 0);
                                validatedatastru = 1;
                                break;
                            }
                            if (arrInput[i].value == "Cuenta cheques" || arrInput[i].value == "Cuenta clabe") {
                                arrInput.push(banuse, cunuse);
                            }
                        } else {
                            if (arrInput[i].value == "") {
                                fshowtypealert('Atención', 'Completa el campo ' + arrInput[i].placeholder, 'warning', arrInput[i], 0);
                                validatedatastru = 1;
                                break;
                            }
                        }
                    }
                    if (validatedatastru == 0) {
                        $("#processedit").modal('show');
                        setTimeout(() => {
                            fsaveeditdatagen();
                        }, 1000);
                    } else { $("#nav-estructure-tab").click(); }
                } else { $("#nav-datanom-tab").click(); }
            } else { $("#nav-imss-tab").click(); }
        } else { $("#nav-datagen-tab").click(); }
    }

    const date = new Date();
    let fechAct;
    let day = date.getDay();
    if (date.getDay() < 10) {
        day = "0" + date.getDay();
    }
    if (date.getMonth() + 1 < 10) {
        fechAct = date.getFullYear() + "-" + "0" + (date.getMonth() + 1) + "-" + day;
    } else {
        fechAct = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + day;
    }

    const modeLocSto = localStorage.getItem("modeedit");
    if (modeLocSto == null) {
        fecefe.value = fechAct;
        fecefecnom.value = fechAct;
        fechefectpos.value = fechAct;
    }

   //btnsaveedit.addEventListener('click', fvalidatefieldsedit);

});