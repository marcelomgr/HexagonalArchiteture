$(document).ready(function () {

    var dataTable = new DataTable('#dataTable', {
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/pt-BR.json',
        },
    });

    $("form").on("submit", function (event) {
        event.preventDefault();

        //$("#loading-spinner").removeClass("d-none");

        // Realize a busca de dados, por exemplo, usando AJAX

        // Quando a busca estiver concluída, oculte o spinner
        // Isso pode ser feito após receber a resposta AJAX ou após qualquer outra lógica de busca
        // Exemplo de ocultação após 2 segundos:
        //setTimeout(function () {
        //    $("#loading-spinner").addClass("d-none");
        //}, 2000);

    //    // Mostrar o spinner durante a busca
    //    $("#loading-spinner").removeClass("d-none");

        dataTable.clear().draw();
        
        // Obtenha os dados do formulário
        var formData = {
            Id: $("#Id").val(),
            Name: $("#Name").val()
        };

        $.ajax({
            type: "GET",
            url: "/Person/Search",
            data: formData,
            success: function (result) {

                //console.log(result)

                // Limpe os dados antigos da tabela
                dataTable.clear().draw();

                //// Adicione os novos dados à tabela
                for (var i = 0; i < result.length; i++) {
                    dataTable.row.add([
                        result[i].id,
                        result[i].name,
                        result[i].motherName,
                        result[i].cpf,
                        result[i].rg,
                        result[i].created
                    ]).draw();
                }
            },
            error: function (error) {
                dataTable.clear().draw();
                console.log(error)
            }
        });

    //    // Ocultar o spinner quando a busca for concluída
    //    $("#loading-spinner").addClass("d-none");
    });
});