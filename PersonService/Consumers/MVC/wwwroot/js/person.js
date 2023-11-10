$(document).ready(function () {

    // #region Inicialização

    loadPersonTypes()
    loadPersonGenders()

    var dataTable = new DataTable('#dataTable', {
        language: {
            url: '/lib/datatables/json/pt-BR.json',
        },
    });

    var logTable = new DataTable('#logTable', {
        language: {
            url: '/lib/datatables/json/pt-BR.json',
        },
    });

    $('[data-toggle="tooltip"]').tooltip();

    $('.cpf').mask('000.000.000-00', { reverse: true }); 
    $('.rg').mask('00.000.000-0', { reverse: true });

    $('.datepicker').datepicker({
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        nextText: 'Proximo',
        prevText: 'Anterior'
    });
    
    $(".datepicker").mask("99/99/9999");

    var aggregate = [
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
            PersonGenderId: $("#Gender").val(),

            MotherName: $("#MotherName").val(),
            FatherName: $("#FatherName").val(),
            SocialName: $("#SocialName").val(),

            Rg: replaceMask($("#Rg").val()),
            Cpf: replaceMask($("#Cpf").val()),
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
                        formatCpf(result[i].cpf),
                        formatRg(result[i].rg),
                        formatDateTime(result[i].created),
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

        console.log('$("#BirthDateSave").val()')

        var formData = {
            Id: $("#hdnId").val(),

            Name: $("#NameSave").val(),
            BirthDate: $("#BirthDateSave").val(),
            PersonGenderId: $("#GenderSave").val(),

            MotherName: $("#MotherNameSave").val(),
            FatherName: $("#FatherNameSave").val(),
            SocialName: $("#SocialNameSave").val(),

            Rg: replaceMask($("#RgSave").val()),
            Cpf: replaceMask($("#CpfSave").val()),
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
                console.log(result.birthDate)

                $('#NameView').text(result.name);
                $('#BirthDateView').text(formatDate(result.birthDate));
                $('#GenderView').val(result.personGenderId);

                $('#MotherNameView').text(result.motherName);
                $('#FatherNameView').text(result.fatherName);
                $('#SocialNameView').text(result.socialName);

                $('#RgView').text(formatRg(result.rg));
                $('#CpfView').text(formatCpf(result.cpf));

                $("#personDetailsModal").modal("show");

                const logData = result.changeLogs;

                if (logData.length > 0) {
                    document.getElementById('logSectionSeparator').style.display = 'block';
                    document.getElementById('logSection').style.display = 'block';

                    logData.forEach(function (log) {
                        logTable.row.add([
                            formatDateTime(log.created),
                            log.propertyName,
                            log.oldValue,
                            log.newValue,
                            log.sourceSystemName,
                            log.userId
                        ]).draw();
                    });
                }
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
                $('#PersonTypeSave').val(result.personAggregates[0].personTypeId);

                $('#NameSave').val(result.name);
                $('#BirthDateSave').val(formatDate(result.birthDate));
                $('#GenderSave').val(result.personGenderId);
                
                $('#MotherNameSave').val(result.motherName);
                $('#FatherNameSave').val(result.fatherName);
                $('#SocialNameSave').val(result.socialName);

                $('#RgSave').val(formatRg(result.rg));
                $('#CpfSave').val(formatCpf(result.cpf));

                setLayoutSaveModal('update')

                $("#personSaveModal").modal("show");
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    function loadPersonTypes() {

        $.ajax({
            url: "/PersonType/GetPersonTypes",
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                $('#PersonTypeSave').empty();

                console.log(data)
                console.log('data')

                $('#PersonTypeSave').append($('<option>', {
                    value: '',
                    text: 'Selecione...'
                }));

                $.each(data, function (index, item) {
                    $('#PersonTypeSave').append($('<option>', {
                        value: item.id,
                        text: item.description
                    }));
                });
            },
            error: function (error) {
                console.log('Erro ao carregar os dados:', error);
            }
        });
    }

    function loadPersonGenders() {

        $.ajax({
            url: "/PersonGender/GetPersonGenders",
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                $('#GenderSave').empty();

                console.log(data)
                console.log('data')

                $('#GenderSave').append($('<option>', {
                    value: '',
                    text: 'Selecione...'
                }));

                $.each(data, function (index, item) {
                    $('#GenderSave').append($('<option>', {
                        value: item.id,
                        text: item.description
                    }));
                });
            },
            error: function (error) {
                console.log('Erro ao carregar os dados:', error);
            }
        });
    }

    //#endregion

});