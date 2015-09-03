
$(document).ready(function () {

    $("#semCadastro").click(function(){
        ExibirDialogConfirmacao();
    });

});

function ExibirDialogConfirmacao() {

    bootbox.dialog({
        message: '<div class="text-warning">Favor entrar em contato com o Administrador do Sistema.</div>',
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