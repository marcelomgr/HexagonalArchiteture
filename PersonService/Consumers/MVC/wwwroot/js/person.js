$(document).ready(function () {

    // #region Inicialização

    var dataTable = new DataTable('#dataTable', {
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/pt-BR.json',
        },
    });

    $('[data-toggle="tooltip"]').tooltip();

    //$('.cpf').inputmask("999.999.999-99");

    var aggregate =  [
        {
            "created": "2023-11-09T18:21:51.358Z",
            "userId": 1,
            "requisitionId": 1,
            "consumerId": 1,
            "sourceSystemId": 1,
            "personTypeId": 1
    }
    ]
    //#endregion

    // #region Modal

    $("#btnCadastroModal").click(function () {
        setLayoutSaveModal('insert')
        $("#personSaveModal").modal('show');
    });

    $(".close").on("click", function () {
        $("#personSaveModal").modal("hide");
    });

    $(".closeDetails").on("click", function () {
        $("#personDetailsModal").modal("hide");
    });

    function setLayoutSaveModal(action) {
        if (action == 'insert') {
            $('#SaveForm').trigger('reset');
            $("#hdnId").val('0')
            $('#SaveTitleModal').text('Cadastrar Usuário')
            $('#btnSave').html('<i class="fas fa-check"></i> Cadastrar')
        }
        else if (action == 'update') {
            $('#SaveTitleModal').text('Editar Usuário')
            $('#btnSave').html('<i class="fas fa-check"></i> Editar')
        }
    }

    //#endregion

    // #region Actions

    $("#searchForm").on("submit", function (event) {
        event.preventDefault();

        //spin on
        loadSpineer('on');

        //limpeza datatable
        dataTable.clear().draw();

        // Coleta dos dados do formulário
        var formData = {
            Name: $("#Name").val(),
            BirthDate: $("#BirthDate").val(),
            Gender: $("#Gender").val(),

            MotherName: $("#MotherName").val(),
            FatherName: $("#FatherName").val(),
            SocialName: $("#SocialName").val(),

            Rg: $("#Rg").val(),
            Cpf: $("#Cpf").val()
        };

        $.ajax({
            type: "GET",
            url: "/Person/GetPersons",
            data: formData,
            success: function (result) {
                console.log(result)
                for (var i = 0; i < result.length; i++) {
                    dataTable.row.add([
                        result[i].id,
                        result[i].name,
                        result[i].motherName,
                        result[i].cpf,
                        result[i].rg,
                        formatarData(result[i].created),
                        '<button class="btn btn-primary view-button" data-id="' + result[i].id + '"><i class="fas fa-eye"></i></button>&nbsp;&nbsp;' +
                        '<button class="btn btn-warning edit-button" data-id="' + result[i].id + '"><i class="fas fa-edit"></i></button>'
                    ]).draw();
                }

                $('.view-button').click(function () {
                    var id = $(this).data('id');
                    viewPerson(id);
                });

                // Evento de clique para editar
                $('.edit-button').click(function () {
                    var id = $(this).data('id');
                    editPerson(id);
                });
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

    $("#btnSave").on("click", function (event) {

        event.preventDefault();

        const isRegister = $("#hdnId").val() == 0

        var formData = {
            Id: $("#hdnId").val(),

            Name: $("#NameSave").val(),
            BirthDate: $("#BirthDateSave").val(),
            Gender: $("#GenderSave").val(),

            MotherName: $("#MotherNameSave").val(),
            FatherName: $("#FatherNameSave").val(),
            SocialName: $("#SocialNameSave").val(),

            Rg: $("#RgSave").val(),
            Cpf: $("#CpfSave").val(),
            PersonAggregates: aggregate
        };

        $.ajax({
            type: "POST",
            url: "/Person/SavePerson",
            data: formData,
            success: function (result) {
                $("#personSaveModal").modal("hide");

                if (isRegister) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Sucesso!',
                        text: 'Pessoa cadastrada com sucesso.',
                    });
                }
                else {
                    Swal.fire({
                        icon: 'success',
                        title: 'Sucesso!',
                        text: 'Pessoa atualizada com sucesso.',
                    });
                }
            },
            error: function (error) {

                let msg;

                if (error.responseJSON.errorCode == 5) {
                    msg = 'Digite um Cpf válido.'
                }
                else {
                    msg = 'Erro ao cadastrar pessoa.'
                }

                Swal.fire({
                    icon: 'error',
                    title: 'Erro!',
                    text: msg,
                });
            }

        });
    });

    function viewPerson(id) {

        var formData = { Id: id };

        $.ajax({
            type: "GET",
            url: "/Person/GetPersonById",
            data: formData,
            success: function (result) {
                console.log(result)

                $('#rgView').text(result.rg);
                $('#cpfView').text(result.cpf);
                $('#nameView').text(result.name);
                $('#motherNameView').text(result.motherName);
                $('#createdView').text(formatarData(result.created));
                $('#DateView').text(result.condemnationDate);
                $('#CourtView').text(result.condemnationCourt);
                $('#RegisterView').text(result.condemnedRegister);
                $('#ArticleView').text(result.condemnationArticle);
                $('#ProccessView').text(result.condemnationProccess);

                $("#personDetailsModal").modal("show");
            },
            error: function (error) {
                console.log(error);

                Swal.fire({
                    icon: 'error',
                    title: 'Erro!',
                    text: 'Erro ao cadastrar pessoa.',
                });
            }
        });
    };

    function editPerson(id) {
        var formData = { Id: id };

        $.ajax({
            type: "GET",
            url: "/Person/GetPersonById",
            data: formData,
            success: function (result) {
                console.log(result)

                $('#hdnId').val(result.id);
                $('#NameSave').val(result.name);
                $('#BirthDateSave').val(result.birthdate);
                $('#GenderSave').val(result.gender);

                $('#MotherNameSave').val(result.motherName);
                $('#FatherNameSave').val(result.fatherName);
                $('#SocialNameSave').val(result.socialName);

                $('#RgSave').val(result.rg);
                $('#CpfSave').val(result.cpf);

                setLayoutSaveModal('update')

                $("#personSaveModal").modal("show");
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
    //#endregion
});