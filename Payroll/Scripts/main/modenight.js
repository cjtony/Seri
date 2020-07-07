$(function () {

    // Menu principal
    const modeNightCheck = document.getElementById('modeNightCheck');
    const menutab = document.getElementById('menuTab');
    const childmenu = menutab.querySelectorAll('li.nav-item > a');
    const iconMenu = menutab.querySelectorAll('span.fas');

    // Meu secundario Empleado -> Datos generales, Imss, Datos de nomina, Estructura
    const fatherMenuSecond = document.getElementById('nav-tab');

    // Formularios datos
    const fatherForm1 = document.getElementById('nav-datagen');
    const childH1Frm1 = fatherForm1.querySelectorAll('h6.font-nav-child');
    const childLabel1 = fatherForm1.querySelectorAll('div.form-group > label');
    const childInput1 = fatherForm1.querySelectorAll('div.form-group > div > input');

    // Footer,
    const fatherFooter = document.querySelector('footer.text-center');
    const childTxtFoot = fatherFooter.querySelector('p.title-font.font-size-footer');

    //console.log(childInput1);
    //console.log(childH1Frm1);
    //console.log(childLabel1);
    //console.log(childmenu);
    //console.log(iconMenu);

    modeNightCheck.addEventListener('click', () => {
        if (modeNightCheck.checked) {
            document.getElementById('body-init').classList.add('bg-dark');
            document.getElementById('menuTab').classList.add('text-white');
            for (let i = 0; i < childmenu.length; i++) {
                childmenu[i].classList.add('text-white');
            }
            for (let i = 0; i < iconMenu.length; i++) {
                iconMenu[i].classList.add('text-white');
            }
            document.getElementById('nav-tabContent').classList.remove('bg-white');
            document.getElementById('nav-tabContent').classList.add('bg-dark');
            document.getElementById('nav-tabContent').style.transition = "1s";
            document.getElementById('name-employee-action').classList.add('text-white');
            document.getElementById('name-employee-action').style.transition = "1s";
            document.getElementById('label-mode-night').classList.add('text-white');
            document.getElementById('ico-mode-night').classList.add('text-white');
            document.getElementById('info-fields-req-1').classList.add('text-white');
            document.getElementById('info-fields-req-1').innerHTML = `<i class="fas fa-info-circle text-white mr-2"></i> Los campos en negritas son requeridos.`;
            document.getElementById('info-fields-req-1').style.transition = "1s";
            document.getElementById('btn-verif-codpost').classList.add('btn-info','text-white');
            for (let i = 0; i < childLabel1.length; i++) {
                childLabel1[i].classList.remove('col-ico');
                childLabel1[i].classList.add('text-white');
                childLabel1[i].style.transition = "1s";
            }
            for (let i = 0; i < childInput1.length; i++) {
                //childInput1[i].classList.add('bg-inpt-mode-night');
                childInput1[i].classList.add('bg-light','shadow');
            }
            for (let i = 0; i < childH1Frm1.length; i++) {
                childH1Frm1[i].classList.remove('col-ico');
                childH1Frm1[i].classList.add('text-white');
                childH1Frm1[i].style.transition = "1s";
            }
            childTxtFoot.classList.add('text-white');
            document.body.style.transition = "1s";
        } else {
            document.getElementById('body-init').classList.remove('bg-dark');
            for (let i = 0; i < childmenu.length; i++) {
                childmenu[i].classList.remove('text-white');
            }
            for (let i = 0; i < iconMenu.length; i++) {
                iconMenu[i].classList.remove('text-white');
            }
            document.getElementById('nav-tabContent').classList.remove('bg-dark');
            document.getElementById('nav-tabContent').classList.add('bg-white');
            document.getElementById('name-employee-action').classList.remove('text-white');
            document.getElementById('label-mode-night').classList.remove('text-white');
            document.getElementById('ico-mode-night').classList.remove('text-white');
            document.getElementById('info-fields-req-1').classList.remove('text-white');
            document.getElementById('info-fields-req-1').innerHTML = `<i class="fas fa-info-circle col-ico mr-2"></i> Los campos en color azul son requeridos.`;
            document.getElementById('btn-verif-codpost').classList.remove('btn-info', 'text-white');
            for (let i = 0; i < childLabel1.length; i++) {
                childLabel1[i].classList.remove('text-white');
                childLabel1[i].classList.add('col-ico');
            }
            for (let i = 0; i < childH1Frm1.length; i++) {
                childH1Frm1[i].classList.remove('text-white');
                childH1Frm1[i].classList.add('col-ico');
            }
            childTxtFoot.classList.remove('text-white');
        }
    });

});