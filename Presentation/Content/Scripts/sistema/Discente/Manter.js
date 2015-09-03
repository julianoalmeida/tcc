var msg = "";

$(document).ready(function () {

    Eventos.MontaBinds();


});

Eventos = {

    MontaBinds: function () {

        if ($("#HiddenIdDiscente").val() == 0) {
            $("#Cidades").attr("disabled", "disabled");
        }

        $("#Preencher").bind('click', function (e) {
            e.preventDefault();
            preencherCamposTemp();
            return false;
        });

        $('#btnSalvar').bind('click', function (e) {
            e.preventDefault();

            var action = rootUrl + 'Discente/Index';

            if (validaCampos()) {

                var discente = $("#formDiscente").serialize();

                $.ajax({
                    url: rootUrl + 'Discente/Salvar',
                    dataType: 'json',
                    type: 'post',
                    cache: false,
                    async: false,
                    data: discente,
                    success: function (retorno) {
                        msg = '<div class="text-success">' + retorno.msg + '</div>';
                        if (retorno.retorno == 1) {
                            dialogSucesso(action, msg);
                        }
                        else if (retorno.retorno > 1) {
                            $("#divErro").show();
                            $("#MensagemErro").html(retorno.msg);
                            SobeScroll();
                            return false;
                        }
                    },
                });
            }
        });

        $('body').delegate('[id*=btnExcluir_]', 'click', function (e) {
            e.preventDefault();
            var codigo = $(this).attr('alt');
            $.ajax({
                url: rootUrl + 'Discente/ExcluirDisciplina',
                dataType: 'json',
                type: 'post',
                cache: false,
                async: false,
                data: { idDIsciplina: codigo },
                success: function (retorno) {
                    MontaTablePaginada();
                },
            });
        });

        $('#Estados').bind('change', function (e) {
            e.preventDefault();
            Eventos.CarregaComboCidades();
            return false;
        });

        $("#btnCancelar").bind('click', function (e) {
            window.location.href = rootUrl + "Discente/Index";
            return false;
        });

    },

    CarregaComboCidades: function () {

        $.ajax({
            url: rootUrl + 'Discente/ListarCidades',
            dataType: 'json',
            type: 'post',
            cache: false,
            async: false,
            data: { siglaEstado: $("#Estados").val() },
            success: function (objeto) {

                var html = '';
                html += "<option value = '' >Selecione</option>";
                $.each(objeto, function (key, valor) {
                    html += "<option value=" + valor.Id + ">" + (valor.Nome) + "</option>";
                });

                if (objeto != '') {
                    $('#Cidades').html(html);
                    $('#Cidades').removeAttr('disabled');
                }
                else {
                    $('#Cidades').attr('disabled', 'disabled');
                    html = "<option value = ''>Selecione</option>"
                    $('#Cidades').html(html);
                }
            },
            erro: function () {
            },
        });

    },

}

function preencherCamposTemp() {

    $("#Nome").val("Nome");
    $('#DataNascimento').val('29/04/2014');
    $("#Pessoa_Email").val('teste@teste.com.br');
    $("#CPF").val(gerarCPF());
    $("#Sexo").val('1');
    $("#EstadoCivil").val('1');
    $("#Celular").val('16982320455').blur();
    $("#Telefone").val('1698232045').blur();
    $('#Estados').val('AC');
    Eventos.CarregaComboCidades();
    $("#Escolaridades").val('1');
    $('#Cidades').val('10');
    //$('#Cidades').val('120020');
    $("#Logradouro").val('Rua Teste');
    $("#Numero").val('54 - A');
    $("#Bairro").val("Bairro Teste");
    $("#CEP").val('14820-000');
    $("#HiddenSexo").val($("#Sexo").val());
    $("#HiddenEstadoCivil").val($("#EstadoCivil").val());
    $("#HiddenCidade").val($("#Cidades").val());
    $("#HiddenEstado").val($("#Estados").val());
    $("#HiddenEscolaridade").val($("#Escolaridades").val());
}