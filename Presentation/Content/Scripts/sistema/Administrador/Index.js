
$(document).ready(function () {

    MontaTablePaginada();

    Eventos.MontaBinds();

});

Eventos = {

    MontaBinds: function () {

        $('body').delegate('[id*="btnExcluir_"]', 'click', function () {

            var id = $(this).attr('alt');
            var metodo = "Administrador/Excluir";

            bootbox.dialog({
                message: '<div class="text-warning">Confirma exclusão?</div>',
                title: '<h2 class="text-warning"><span class="icon-exclamation-sign"></span> Aviso</h2>',
                buttons: {
                    main: {
                        label: "Confirmar",
                        className: "btn-primary",
                        callback: function () {
                            Excluir(id, metodo);
                        }
                    },
                    cancelar: {
                        label: "Cancelar",
                        className: "btn",
                        callback: function () {
                            bootbox.hideAll();
                        }
                    }
                }
            });

            return false;

        });

        $("#btnPesquisar").bind('click', function () {
            MontaTablePaginada();
            return false;
        });

        $('.cpf').setMask('999.999.999-99');

        $("#btnIncluir").bind('click', function () {
            window.location.href = rootUrl + "Administrador/Manter?id=";
        });
    },

}

function MontaTablePaginada() {

    var config = {
        "sAjaxSource": rootUrl + 'Administrador/ListarPaginado',
        "aSync": false,
        "aoColumns": [
                {
                    'mData': 'Pessoa.Nome',
                    'sTitle': 'Nome',
                    'sWidth': '28%',
                    'sClass': 'quebra-linha',
                    'bSortable': false,
                    'fnRender': function (o, val) {
                        val = htmlEscape(val);
                        return val;
                    }
                },
                {
                    'mData': 'Pessoa.Cpf',
                    'sTitle': 'CPF',
                    'sWidth': '28%',
                    'sClass': 'quebra-linha centro cpf',
                    'bSortable': false,
                    'fnRender': function (o, val) {
                        val = htmlEscape(val);
                        return Mascaracpf(val);
                    }
                },
                {
                    'mData': null,
                    'sTitle': 'Ações',
                    'sWidth': '20%',
                    'sClass': 'centro',
                    'bSortable': false,
                    'mRender': function (data, type, val) {
                        var html = "<div>"
                                      + "<a onclick=\"location.href = '" + rootUrl + "Administrador/Manter?id=" + val.Id + "'; return false;\" data-toggle='tooltip' title='Editar'><span class='icon-pencil'></span></a>"
                                      + "<a data-toggle='tooltip' id='btnExcluir_' title='Excluir' alt='" + val.Id + "'  ><span class='icon-remove'></span></a>"
                                  + "</div>";
                        return html;
                    }
                }],
        "fnServerParams": function (aoData) {
            aoData.push({
                'name': 'Nome',
                'value': $("#Nome").val()
            });
        }
    };

    $.extend(config, RecuperaConfiguracoesDataTable(config));
    $("#TbAdministrador").dataTable(config);
}