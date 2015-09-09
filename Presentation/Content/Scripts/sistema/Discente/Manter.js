var msg = "";

$(document).ready(function() {

    Eventos.MontaBinds();


});

Eventos = {
    MontaBinds: function() {

        if ($("#HiddenIdDiscente").val() == 0) {
            $("#Cities").attr("disabled", "disabled");
        }

        $("#Preencher").bind("click", function(e) {
            e.preventDefault();
            preencherCamposTemp();
            return false;
        });

        $("#btnSalvar").bind("click", function(e) {
            e.preventDefault();

            var action = rootUrl + "Student/Index";

            if (validaCampos()) {

                var discente = $("#formDiscente").serialize();

                $.ajax({
                    url: rootUrl + "Student/SaveAndReturn",
                    dataType: "json",
                    type: "post",
                    cache: false,
                    async: false,
                    data: discente,
                    success: function(retorno) {
                        msg = "<div class=\"text-success\">" + retorno.msg + "</div>";
                        if (retorno.retorno == 1) {
                            dialogSucesso(action, msg);
                        } else if (retorno.retorno > 1) {
                            $("#divErro").show();
                            $("#MensagemErro").html(retorno.msg);
                            SobeScroll();
                            return false;
                        }
                    },
                });
            }
        });

        $("body").delegate("[id*=btnExcluir_]", "click", function(e) {
            e.preventDefault();
            var codigo = $(this).attr("alt");
            $.ajax({
                url: rootUrl + "Student/ExcluirDisciplina",
                dataType: "json",
                type: "post",
                cache: false,
                async: false,
                data: { idDIsciplina: codigo },
                success: function(retorno) {
                    MontaTablePaginada();
                },
            });
        });

        $("#States").bind("change", function(e) {
            e.preventDefault();
            Eventos.CarregaComboCidades();
            return false;
        });

        $("#btnCancelar").bind("click", function(e) {
            window.location.href = rootUrl + "Student/Index";
            return false;
        });

    },

    CarregaComboCidades: function() {

        $.ajax({
            url: rootUrl + "Student/ListarCidades",
            dataType: "json",
            type: "post",
            cache: false,
            async: false,
            data: { siglaEstado: $("#States").val() },
            success: function(objeto) {

                var html = "";
                html += "<option value = '' >Selecione</option>";
                $.each(objeto, function(key, valor) {
                    html += "<option value=" + valor.Id + ">" + (valor.Nome) + "</option>";
                });

                if (objeto != "") {
                    $("#Cities").html(html);
                    $("#Cities").removeAttr("disabled");
                } else {
                    $("#Cities").attr("disabled", "disabled");
                    html = "<option value = ''>Selecione</option>";
                    $("#Cities").html(html);
                }
            },
            erro: function() {
            },
        });

    },

};

function preencherCamposTemp() {

    $("#Name").val("Name");
    $("#BirthDate").val("29/04/2014");
    $("#Pessoa_Email").val("teste@teste.com.br");
    $("#CPF").val(gerarCPF());
    $("#Sex").val("1");
    $("#MaritalState").val("1");
    $("#MobileNumber").val("16982320455").blur();
    $("#PhoneNumber").val("1698232045").blur();
    $("#States").val("AC");
    Eventos.CarregaComboCidades();
    $("#Escolaridades").val("1");
    $("#Cities").val("10");
    //$('#Cities').val('120020');
    $("#Logradouro").val("Rua Teste");
    $("#Numero").val("54 - A");
    $("#Bairro").val("Bairro Teste");
    $("#CEP").val("14820-000");
    $("#HiddenSexo").val($("#Sex").val());
    $("#HiddenEstadoCivil").val($("#MaritalState").val());
    $("#HiddenCidade").val($("#Cities").val());
    $("#HiddenEstado").val($("#States").val());
    $("#HiddenEscolaridade").val($("#Escolaridades").val());
}