function dialogSucesso(action, mensagem) {
    limpar = false;
    title = "<h2 class=\"text-success\"><span class=\"icon-ok-sign\"></span> Sucesso</h2>";
    msg = "<div class=\"text-success centro\">" + mensagem + "</div>";
    bootbox.dialog({
        message: msg,
        title: title,
        buttons: {
            main: {
                label: "OK",
                className: "btn-primary",
                callback: function() {
                    window.location.href = action;
                }
            },
        }
    });
}

function dialogConfirmarCancelamento(action) {

    bootbox.dialog({
        message: "<div class=\"text-warning\">Os dados inseridos serão descartados.<br>Deseja continuar?</div>",
        title: "<h2 class=\"text-warning\"><span class=\"icon-exclamation-sign\"></span> Aviso</h2>",
        buttons: {
            main: {
                label: "Confirmar",
                className: "btn-primary",
                callback: function() {
                    window.location.href = action;
                }
            },
            cancelar: {
                label: "Cancelar",
                className: "btn",
                callback: function() {
                    bootbox.hideAll();
                }
            }
        }
    });
}

function Excluir(Id, metodo) {

    $.ajax({
        async: "false",
        type: "post",
        dataType: "Json",
        data: { id: Id },
        url: rootUrl + metodo,
        success: function(response) {
            title = "<h2 class=\"text-success\"><span class=\"icon-ok-sign\"></span> Sucesso</h2>";
            msg = "<div class=\"text-success\">Exclusão realizada com sucesso!</div>";
            bootbox.dialog({
                message: msg,
                title: title,
                buttons: {
                    main: {
                        label: "OK",
                        className: "btn-primary",
                        callback: function() {
                            MontaTablePaginada();
                        }
                    }
                }
            });
        }
    });
}