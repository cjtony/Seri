$(function () {

    const name = document.getElementById('name'),
        apep = document.getElementById('apepat'),
        apem = document.getElementById('apemat'),
        fnaci = document.getElementById('fnaci'),
        lnaci = document.getElementById('lnaci'),
        title = document.getElementById('title'),
        sex = document.getElementById('sex'),
        nacion = document.getElementById('nacion'),
        estciv = document.getElementById('estciv'),
        codpost = document.getElementById('codpost'),
        state = document.getElementById('state'),
        city = document.getElementById('city'),
        colony = document.getElementById('colony'),
        street = document.getElementById('street'),
        numberst = document.getElementById('numberst'),
        telfij = document.getElementById('telfij'),
        telmov = document.getElementById('telmov'),
        mailus = document.getElementById('mailus'),
        tipsan = document.getElementById('tipsan'),
        fecmat = document.getElementById('fecmat');
    

    const dataGeneral = [name, apep, apem, fnaci, lnaci, title, sex, nacion, estciv, codpost, state, city, colony, street, numberst, telfij, telmov, mailus, tipsan, fecmat];

    fActiveAnimation = (element, time) => {
        element.addEventListener('mouseover', () => {
            element.classList.add('shadow', 'headShake');
        });
    }

    fDesactiveAnimation = (element) => {
        element.addEventListener('mouseleave', () => {
            element.classList.remove('shadow', 'headShake');
        });
    }

    for (let i = 0; i < dataGeneral.length; i++) {
        dataGeneral[i].style.transition = "1s";
        dataGeneral[i].style.cursor = "pointer";
        dataGeneral[i].classList.add('animated');
        fActiveAnimation(dataGeneral[i], 500);
        fDesactiveAnimation(dataGeneral[i]);
    }

    const imss = document.getElementById('regimss'),
        rfc = document.getElementById('rfc'),
        curp = document.getElementById('curp'),
        nivest = document.getElementById('nivest'),
        nivsoc = document.getElementById('nivsoc'),
        fecefe = document.getElementById('fecefe');

    const dataImss = [imss, rfc, curp, nivest, nivsoc, fecefe];

    for (let i = 0; i < dataImss.length; i++) {
        dataImss[i].style.transition = "1s";
        dataImss[i].style.cursor = "pointer";
        dataImss[i].classList.add('animated');
        fActiveAnimation(dataImss[i], 500);
        fDesactiveAnimation(dataImss[i]);
    }

    const fecefecnom = document.getElementById('fecefecnom'),
        salmen = document.getElementById('salmen'),
        tipper = document.getElementById('tipper'),
        tipemp = document.getElementById('tipemp'),
        nivemp = document.getElementById('nivemp'),
        tipjor = document.getElementById('tipjor'),
        tipcon = document.getElementById('tipcon'),
        fecing = document.getElementById('fecing'),
        fecant = document.getElementById('fecant'),
        vencon = document.getElementById('vencon'),
        tipcontra = document.getElementById('tipcontra'),
        tippag = document.getElementById('tippag'),
        cunuse = document.getElementById('cunuse'),
        banuse = document.getElementById('banuse');

    const tiposueldo = document.getElementById('tiposueldo');

    const dataNomina = [fecefecnom, salmen, tipper, tipemp, nivemp, tipjor, tipcon, fecing, fecant, vencon, tipcontra, tippag, cunuse, banuse, tiposueldo];

    for (let i = 0; i < dataNomina.length; i++) {
        dataNomina[i].style.transition = "1s";
        dataNomina[i].style.cursor     = "pointer";
        dataNomina[i].classList.add('animated');
        fActiveAnimation(dataNomina[i], 500);
        fDesactiveAnimation(dataNomina[i]);
    }

    const fechefectpos = document.getElementById('fechefectpos');

    const dataEstructura = [fechefectpos];

    if (localStorage.getItem('modeedit') != null) {
        const motmovi = document.getElementById('motmovi');
        const fechmovi = document.getElementById('fechmovi');
        dataEstructura.push(motmovi);
        dataEstructura.push(fechmovi);
    }

    for (let i = 0; i < dataEstructura.length; i++) {
        dataEstructura[i].style.transition = "1s";
        dataEstructura[i].style.cursor = "pointer";
        dataEstructura[i].classList.add('animated');
        fActiveAnimation(dataEstructura[i], 500);
        fDesactiveAnimation(dataEstructura[i]);
    }

});