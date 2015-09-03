var msg = "";
$(document).ready(function () {

    Eventos.MontaBinds();

});

Eventos = {

    MontaBinds: function () {

        $("#Preencher").bind('click', function (e) {
            e.preventDefault();
            preencherCamposTemp();
            return false;
        });

        $('#btnSalvar').bind('click', function (e) {
            e.preventDefault();

            var action = rootUrl + "Administrador/Index";

            if (validaCampos()) {

                var adm = $("#formAdm").serialize();

                $.ajax({
                    url: rootUrl + 'Administrador/Salvar',
                    dataType: 'json',
                    type: 'post',
                    cache: false,
                    async: false,
                    data: adm,
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

        $('#Estados').bind('change', function (e) {
            $("#HiddenEstado").val($(this).val());
            CarregaComboCidades();
            return false;
        });

        if ($("#HiddenIdAdministrador").val() == 0) {
            $("#Cidades").attr("disabled", "disabled");
        }

        $("#btnCancelar").bind('click', function (e) {
            window.location.href = rootUrl + "Administrador/Index";
            return false;
        });

    },
}

function CarregaComboCidades() {

    $.ajax({
        url: rootUrl + 'Administrador/ListarCidades',
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
    });

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
    CarregaComboCidades();
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
}