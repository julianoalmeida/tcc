
$(document).ready(function () {

    $("#semCadastro").click(function () {
        var msg = "Favor entrar em contato com o Administrator do Sistema.";
        ExibirDialogConfirmacao(msg);
    });

    $("#btnLogar").click(function () {
        Logar();
        return false;
    });

});

function ExibirDialogConfirmacao(msg) {

    bootbox.dialog({
        message: '<div class="text-warning">' + msg + '</div>',
        title: '<h2 class="text-warning"><span class="icon-exclamation-sign"></span> Aviso</h2>',
        buttons: {
            main: {
                label: "Confirmar",
                className: "btn-primary",
                callback: function () {
                    bootbox.hideAll();
                }
            },
        }
    });


}

function Logar() {

    $.ajax({
        url: rootUrl + 'Login/Logar',
        dataType: 'json',
        type: 'post',
        cache: false,
        async: false,
        data: { login: $("#Login").val(), senha: $("#Password").val() },
        success: function (retorno) {

            if (retorno.retorno == 1) {
                window.location.href = rootUrl + "Base/Index";
            }
            else {
                var msg = "Dados inválidos!";
                ExibirDialogConfirmacao(msg);
            }

        },
    });

}