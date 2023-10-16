$(document).ready(function () {
    // Adicione um botão de alternância para recolher/expandir o menu em telas menores
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });
});