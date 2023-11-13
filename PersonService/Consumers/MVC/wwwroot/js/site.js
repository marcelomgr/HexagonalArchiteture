$(document).ready(function () {
    // Adicione um botão de alternância para recolher/expandir o menu em telas menores
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });
});

function loadSpineer(state) {
    if (state == 'on') {
        $("#loading-spinner").removeClass("d-none");
    }
    else if (state == 'off') {
        $("#loading-spinner").addClass("d-none");
    }
}

function showSpinner() {
    $(".overlay").fadeIn();
    var target = document.getElementById("spinner");
    var spinner = new Spinner().spin(target);
}

// Função para desativar o spinner
function hideSpinner() {
    $(".overlay").fadeOut();
}


function formatDateTime(data) {
    var date = new Date(data);
    var dia = String(date.getDate()).padStart(2, '0');
    var mes = String(date.getMonth() + 1).padStart(2, '0');
    var ano = date.getFullYear();
    var hora = String(date.getHours()).padStart(2, '0');
    var minutos = String(date.getMinutes()).padStart(2, '0');
    var segundos = String(date.getSeconds()).padStart(2, '0');

    return dia + '/' + mes + '/' + ano + ' ' + hora + ':' + minutos + ':' + segundos;
}

function formatDate(data) {
    var date = new Date(data);
    var dia = String(date.getDate()).padStart(2, '0');
    var mes = String(date.getMonth() + 1).padStart(2, '0');
    var ano = date.getFullYear();

    return dia + '/' + mes + '/' + ano;
}

function replaceMask(value) {
    return value.replace(/[-.]/g, '');
}

function formatCpf(cpf) {
    cpf = String(cpf);
    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
}

function formatRg(rg) {
    rg = String(rg);
    return rg.replace(/(\d{2})(\d{3})(\d{3})(\d{1})/, '$1.$2.$3-$4');
}