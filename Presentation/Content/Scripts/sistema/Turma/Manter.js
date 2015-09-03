
var discentesSelecionados = [];

$(document).ready(function () {

    Eventos.MontaBinds();

});

Eventos = {

    MontaBinds: function () {

        $.jshuttle("#DiscentesNaoSelecionados", "#DiscentesSelecionados", {
            add: "#btnAdd",
            remove: "#btnRemove",
            addAll: "#btnAddAll",
            removeAll: "#btnRemoveAll"
        });

        $('#btnSalvar').click(function (e) {
            e.preventDefault();

            var action = rootUrl + 'Turma/Index';
            var camposValidos = validaCampos();
            var pickListValido = Validacoes.validaPickList();
            if (camposValidos && !pickListValido) {

                var model = $("#formTurma").serialize();

                $.ajax({
                    url: rootUrl + 'Turma/Salvar',
                    dataType: 'json',
                    type: 'post',
                    cache: false,
                    async: false,
                    data: model,
                    success: function (retorno) {
                        msg = '<div class="text-success">' + retorno.msg + '</div>';
                        if (retorno.sucesso == 1) {
                            dialogSucesso(action, msg);
                        }
                        else if (retorno.sucesso == 2) {
                            $("#divErro").show();
                            $("#MensagemErro").html('Turma já Cadastrada');
                            SobeScroll();
                            return false;
                        }
                        else if (retorno.sucesso == 4) {
                            $("#divErro").show();
                            $("#MensagemErro").html(retorno.msg);
                            SobeScroll();
                            return false;
                        }
                    },
                });
            }
        });

    },
}

Validacoes = {

    validaPickList: function () {
        discentesSelecionados = [];
        var erro = false;
        if ($('#DiscentesSelecionados').children().length) {
            $('#DiscentesSelecionados').children().each(function () {
                $(this).prop('selected', true);
                $('#DiscentesSelecionados').next().addClass('hide');
                $(this).parent().parent().removeClass('error');
                discentesSelecionados.push($(this).val());
                $(".discenteObrigatorio").removeClass('discenteObrigatorio')
            });
        } else {
            $('#DiscentesSelecionados').parent().parent().addClass('error');
            $('#DiscentesSelecionados').next().removeClass('hide');
            $(".discenteObrigatorio").addClass('discenteObrigatorio');
            erro = true;
        }

        $("#IdsSelecionados").val(discentesSelecionados);
        return erro;
    }
}

