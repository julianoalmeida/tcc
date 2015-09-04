
var msg = "";

$(document).ready(function () {

    MontaTablePaginada();

    Eventos.MontaBinds();

});

Eventos = {

    MontaBinds: function () {

        if ($("#HiddenIdDocente").val() == 0) {
            $("#Cities").attr("disabled", "disabled");
        }

        $("#Preencher").bind('click', function (e) {
            e.preventDefault();
            preencherCamposTemp();
            return false;
        });

        $('#btnSalvar').bind('click', function (e) {
            e.preventDefault();

            var totalRegistros = $("#tbDisciplinas").dataTable().fnGetData().length;
            var action = rootUrl + 'Teacher/Index';
            if (totalRegistros == 0) {
                ExibirDialogConfirmacao();
                return false;
            }
            else {
                if (validaCampos()) {

                    var docente = $("#formDocente").serialize();
                    $.ajax({
                        url: rootUrl + 'Teacher/SaveAndReturn',
                        dataType: 'json',
                        type: 'post',
                        cache: false,
                        async: false,
                        data: docente,
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
            }
        });

        $("#btnAdicionarDisciplina").bind('click', function (e) {
            e.preventDefault();
            AdicionarDisciplina();
            return false;
        });

        $('body').delegate('[id*=btnExcluir_]', 'click', function (e) {
            e.preventDefault();
            var codigo = $(this).attr('alt');
            $.ajax({
                url: rootUrl + 'Teacher/ExcluirDisciplina',
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

        $('#States').bind('change', function (e) {
            e.preventDefault();
            $("#HiddenEstado").val($(this).val());
            CarregaComboCidades();
            return false;
        });

        $("#btnCancelar").bind('click', function (e) {
            window.location.href = rootUrl + "Teacher/Index";
            return false;
        });
    },
}

function AdicionarDisciplina() {

    var disciplina = $("#Courses").val();
    $.ajax({
        url: rootUrl + 'Teacher/AdicionarDisciplina',
        dataType: 'json',
        type: 'post',
        cache: false,
        async: false,
        data: { idDIsciplina: disciplina },
        success: function (retorno) {
            if (!retorno.duplicado) {
                MontaTablePaginada();
                $("#divErro").hide();
            }
            else {
                $("#divErro").show();
                $("#MensagemErro").html("Courses já adicionada");
                SobeScroll();
            }
        },
    });
}

function MontaTablePaginada() {

    var config = {
        "sAjaxSource": rootUrl + 'Teacher/ListarDisciplinas',
        "aSync": false,
        "aoColumns": [
                {
                    'mData': 'Description',
                    'sTitle': 'Courses',
                    'sWidth': '80%',
                    'sClass': 'quebra-linha',
                    'bSortable': false,
                    'fnRender': function (o, val) {
                        val = htmlEscape(val);
                        return val;
                    }
                },
                {
                    'mData': null,
                    'sTitle': 'Ação',
                    'sWidth': '20%',
                    'sClass': 'centro',
                    'bSortable': false,
                    'mRender': function (data, type, val) {
                        var html = "<div>"
                                      + "<a data-toggle='tooltip' id='btnExcluir_' title='Remove' alt='" + val.Id + "'  ><span class='icon-remove'></span></a>"
                                  + "</div>";
                        return html;
                    }
                }],
        "fnServerParams": function (aoData) {
        }

    };

    //config.bInfo = false;
    config.sZeroRecords = "Nenhuma course vinculada";
    $.extend(config, RecuperaConfiguracoesDataTable(config));
    $("#tbDisciplinas").dataTable(config);
}

function CarregaComboCidades() {

    $.ajax({
        url: rootUrl + 'Teacher/ListarCidades',
        dataType: 'json',
        type: 'post',
        cache: false,
        async: false,
        data: { siglaEstado: $("#States").val() },
        success: function (objeto) {

            var html = '';
            html += "<option value = '' >Selecione</option>";
            $.each(objeto, function (key, valor) {
                html += "<option value=" + valor.Id + ">" + (valor.Nome) + "</option>";
            });

            if (objeto != '') {
                $('#Cities').html(html);
                $('#Cities').removeAttr('disabled');
            }
            else {
                $('#Cities').attr('disabled', 'disabled');
                html = "<option value = ''>Selecione</option>"
                $('#Cities').html(html);
            }
        },
        erro: function () {
        },
    });

}

function preencherCamposTemp() {

    $("#Name").val("Name");
    $('#BirthDate').val('29/04/2014');
    $("#Pessoa_Email").val('teste@teste.com.br');
    $("#CPF").val(gerarCPF());
    $("#Sex").val('1');
    $("#MaritalState").val('1');
    $("#MobileNumber").val('16982320455').blur();
    $("#PhoneNumber").val('1698232045').blur();
    $('#States').val('AC');
    CarregaComboCidades();
    $("#Escolaridades").val('1');
    $('#Cities').val('10');
    //$('#Cities').val('120020');
    $("#Logradouro").val('Rua Teste');
    $("#Numero").val('54 - A');
    $("#Bairro").val("Bairro Teste");
    $("#CEP").val('14820-000');
    $("#HiddenSexo").val($("#Sex").val());
    $("#HiddenEstadoCivil").val($("#MaritalState").val());
    $("#HiddenCidade").val($("#Cities").val());
    $("#HiddenEstado").val($("#States").val());
    $("#HiddenEscolaridade").val($("#Escolaridades").val());
}

function ExibirDialogConfirmacao() {

    bootbox.dialog({
        message: '<div class="text-warning">Para cadastrar um Teacher é necessário vincular ao menos uma course.</div>',
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
