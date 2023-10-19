$(document).ready(function () {

    var dataTable = new DataTable('#dataTable', {
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/pt-BR.json',
        },
    });

    $("#btnCadastroModal").click(function () {
        $("#myModal").modal('show');
    });

    $(".close").on("click", function () {
        $("#myModal").modal("hide");
    });

    $("form").on("submit", function (event) {
        event.preventDefault();

        //spin on
        loadSpineer('on');

        //limpeza datatable
        dataTable.clear().draw();

        // Obtenha os dados do formulário
        var formData = {
            Id: $("#Id").val(),
            Rg: $("#Rg").val(),
            Cpf: $("#Cpf").val(),
            Name: $("#Name").val(),
            MotherName: $("#MotherName").val()
        };

        $.ajax({
            type: "GET",
            url: "/Person/Search",
            data: formData,
            success: function (result) {

                for (var i = 0; i < result.length; i++) {
                    dataTable.row.add([
                        result[i].id,
                        result[i].name,
                        result[i].motherName,
                        result[i].cpf,
                        result[i].rg,
                        formatarData(result[i].created)
                    ]).draw();
                }
            },
            error: function (error) {
                console.log(error)
            }
        });

        // spin off
        setTimeout(function () {
            loadSpineer('off');
        }, 0);
    });
});