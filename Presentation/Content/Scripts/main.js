$(document).ready(function () {

    $(window).ready(function () {

        $("#btnSairSistema").click(function (e) {
            window.location.href = rootUrl + "Login/Sair";
            return false;
        });

        //$('input, textarea').placeholder();
        BackToTop({
            autoShowOffset: '200',
            text: '<span class="icon-chevron-up"></span>',
            effectScroll: 'linear',
            appearMethod: 'fade'
        });
        $('.menu a[data-toggle=collapse]').click(function () {
            if (!$(this).parent().hasClass('active')) {
                $(this).parent().addClass('active');
                $(this).children('.open-collapse')
                        .removeClass('icon-angle-down')
                        .addClass('icon-angle-up');
            } else {
                $(this).parent().removeClass('active');
                $(this).children('.open-collapse')
                        .removeClass('icon-angle-up')
                        .addClass('icon-angle-down');
            }
        });
        $('#menu-comando').click(function () {
            if ($('#navegacao').hasClass('hide')) {
                localStorage.setItem('navegacaoOpened', true);
                $('#navegacao').removeClass('hide').addClass('show');
                $('#conteudo').removeClass('full-width');
            } else {
                localStorage.setItem('navegacaoOpened', false);
                $('#navegacao').removeClass('show').addClass('hide');
                $('#conteudo').addClass('full-width');
            }
        });
        $('[data-toggle=tooltip]').tooltip();
    });


    //carrega imagem de loading
    $(document).ajaxStart(function () {

        var div = '<div id="load"><div id="mask"></div><div id="ajax">';

        $('body').prepend(div + '<img src="../Content/Images/background/loaderBlock.GIF"/></div></div>');

        CriaDivCarregando();

    }).ajaxComplete(function () {
        $('#load').remove();
    });


    $('.input-append .btn').click(function () {
        $(this).prev().focus();
    });

    $('.labelDate').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
    });

    $('#indexEcu').click(function () {
        var controller = $('#controllerEcu').val();
        window.location.href = rootUrl + controller + "/Index";
    });

    $('input[type=text]').blur(function () {
        var inputTrim = $.trim($(this).val());
        if (inputTrim == "") {
            $(this).val("");
        }
    });

    //meio-mask
    //$.mask.rules.n = /[0-9]?/;
    $.mask.masks.cnpj = { mask: '99.999.999/9999-99' };
    $.mask.masks.telefone = { mask: '(99) 9999-9999' };
    $.mask.masks.celular = { mask: '(99) 99999-9999' };
    $.mask.masks.cpf = { mask: '999.999.999-99' };
    $.mask.masks.capacidade = { mask: '999999' };
    $.mask.masks.data = { mask: '39/19/9999' };
    $.mask.masks.numero = { mask: '99999' };
    $.mask.masks.capacidadeTurma = { mask: '999' };
    $.mask.masks.cargaHoraria = { mask: '9999' };
    $.mask.masks.porcentagem = { mask: '999' };
    $.mask.masks.hora = { mask: '99:99' };

    $('input[type="text"]').setMask();
    $('input[type="text"]').on('blur, focus, keypress, keyup, keydown', function () {
        $(this).setMask();
    });

    //coloca a máscara de 8 ou 9 dígitos no celular
    $('input[alt="celular"]').bind("keyup paste blur focus", function () {
        var phone, element;
        element = $(this);
        element.unsetMask();
        phone = element.val().replace(/\D/g, '');
        if (phone.length > 10) {
            element.setMask("(99) 99999-999?");
        } else {
            element.setMask("(99) 9999-9999?");
        }
    });

    //dispara o evento blur para colocar a máscara no celular
    $('input[alt="celular"]').blur();

    //máscara textarea
    $('textarea:not([alt="none"])').each(function () {
        $(this).maxLength({ limit: parseInt($(this).attr("alt")) });
    });
});


function gerarCPF() {

    var cpf = "";
    var comPontos = true;

    var n = 9;
    var n1 = randomiza(n);
    var n2 = randomiza(n);
    var n3 = randomiza(n);
    var n4 = randomiza(n);
    var n5 = randomiza(n);
    var n6 = randomiza(n);
    var n7 = randomiza(n);
    var n8 = randomiza(n);
    var n9 = randomiza(n);
    var d1 = n9 * 2 + n8 * 3 + n7 * 4 + n6 * 5 + n5 * 6 + n4 * 7 + n3 * 8 + n2 * 9 + n1 * 10;
    d1 = 11 - (mod(d1, 11));
    if (d1 >= 10) d1 = 0;
    var d2 = d1 * 2 + n9 * 3 + n8 * 4 + n7 * 5 + n6 * 6 + n5 * 7 + n4 * 8 + n3 * 9 + n2 * 10 + n1 * 11;
    d2 = 11 - (mod(d2, 11));
    if (d2 >= 10) d2 = 0;
    retorno = '';
    if (comPontos) cpf = '' + n1 + n2 + n3 + '.' + n4 + n5 + n6 + '.' + n7 + n8 + n9 + '-' + d1 + d2;
    else cpf = '' + n1 + n2 + n3 + n4 + n5 + n6 + n7 + n8 + n9 + d1 + d2;

    return cpf;

}

function randomiza(n) {
    var ranNum = Math.round(Math.random() * n);
    return ranNum;
}

function mod(dividendo, divisor) {
    return Math.round(dividendo - (Math.floor(dividendo / divisor) * divisor));
}



//cria o css para a imagem de carregando.
function CriaDivCarregando() {
    $('#ajax').css({
        width: '70px',
        height: '70px',
        position: 'fixed',
        top: '50%',
        left: '50%',
        marginTop: '-32px',
        marginRight: '0px',
        marginBottom: '0px',
        marginLeft: '-31px',
        zIndex: 9999
    });

    $('#mask').css({
        backgroundColor: '#fff',
        opacity: '0.6',
        top: '0',
        left: '0',
        width: '100%',
        height: $(document).height(),
        position: 'fixed',
        zIndex: 9998
    });
}


/* DATATABLES */
function RecuperaConfiguracoesDataTable(config) {

    var configBaseDataTable = {
        bJQueryUI: false,
        bFilter: false,

        "bPaginate": config.bPaginate,
        "bProcessing": false,
        "iDisplayLength": config.iDisplayLength || 10, // total de registros por página
        "sPaginationType": "full_numbers",
        "bServerSide": true,
        "aaSorting": config.aaSorting || [[0, 'asc']],
        'bFilter': false,
        "bDeferRender": true,
        "bDestroy": true,
        "bAutoWidth": false,
        "bLengthChange": false,
        "bInfo": config.bInfo,
        "oLanguage": config.oLanguage || {
            "sProcessing": "Processando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": config.sZeroRecords || "Nenhum registro encontrado",
            "sInfo": config.sInfo || "Nº de registros  <b> _TOTAL_</b>",
            "sInfoEmpty": "Nº de registros 0",
            "sInfoFiltered": "",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Início",
                "sPrevious": "Anterior",
                "sNext": "Próximo",
                "sLast": "Fim"
            }
        }
    };

    return configBaseDataTable;
}
/* DATATABLES */

function Mascaracpf(texto) {

    texto = texto.replace(/\D/g, "");
    texto = texto.replace(/(\d{3})(\d)/, "$1.$2");
    texto = texto.replace(/(\d{3})(\d)/, "$1.$2");
    texto = texto.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    return texto;
}

//Funcão usada para Desabilitar Todos os  componente da tela
//Realiza chamadas para o metodo "DesabilitaElemento" para cada elemento da tela.
function DesabilitarCamposTela() {

    $('form input').each(function () {
        DesabilitaElemento($(this).attr('id'));
    })

    $('form select').each(function () {
        $('#' + $(this).attr('id')).attr('disabled', 'disabled');
    });

    $('textarea').each(function () {
        $(this).attr('disabled', 'disabled');
    });
}

//Funcão usada para habilitar um componente da tela
//Recebe como parametro o Id 
//Habilita o componente de acordo com seu tipo
function HabilitaElemento(idElemento) {

    var id = '#' + idElemento;

    if ($(id).is("input[type=text]") || $(id).is("input[type=password]")) {
        $(id).removeAttr('readonly');
    }
    else if ($(id).is("input[type=radio]")) {
        $(id).removeAttr('disabled');
    }
    if ($(id).is("input[type=button],input[type=submit]")) {
        $(id).removeClass('disabled')
            .removeAttr('disabled');
    }
    else {
        $(id).removeClass('disabled');
    }

};

//valida o campo passado como parametro e exibe mensagem de erro
function validaCampo($campo) {
    if ($campo.val() == '' || $campo.val() == null) {
        $campo.parent().parent().addClass('error');
        $campo.next().removeClass('hide');
        return false;
    } else {
        $campo.parent().parent().removeClass('error');
        $campo.next().addClass('hide');
        return true;
    }
}

//valida os campos do formulário
function validaCampos() {
    var erro = false;

    $('.required').each(function () {
        if (($(this).val() == '' || $(this).val() == null) && !$(this).attr('disabled')) {
            erro = true;
            $(this).parent().parent().addClass('error');
            $(this).next().removeClass('hide');
        } else {
            $(this).parent().parent().removeClass('error');
            $(this).next().addClass('hide');
        }
    });

    //validacao especifica do campo Data ( com o cate-picker )
    //ajuste para exibir a mensagem de erro e manter o incone do calendario
    $('.required-data').each(function () {
        if ($(this).val() == '') {
            erro = true;
            $(this).parent().parent().addClass('error');
            $(this).parent().next().removeClass('hide');
        } else {
            $(this).parent().parent().removeClass('error');
            $(this).parent().next().addClass('hide');
        }
    });


    //valida os campos email
    $(".email").each(function () {
        if (($.trim($(this).val()) != "") && (!EmailValido($(this).val()))) {
            erro = true;
            $(this).parent().parent().addClass('error');
            $(this).next().removeClass('hide');
        } else {
            $(this).parent().parent().removeClass('error');
            $(this).next().addClass('hide');
        }
    });

    SobeScroll();

    if (!erro) {
        return true;
    } else {
        return false;
    }
}


//Funcão usada para Desabilitar um componente da tela
//Recebe como parametro o Id 
//Desabilita o componente de acordo com seu tipo
function DesabilitaElemento(idElemento) {

    var id = '#' + idElemento;
    if ($(id).is("input[type=radio]")) {
        $(id).attr('disabled', 'disabled');
    }
    else if ($(id).is("input[type=text]")) {
        $(id).attr('readonly', 'readonly');
    }
    else if ($(id).is("input[type=button],input[type=submit]")) {
        $(id).attr('disabled', 'disabled');
    }
    else {
        $(id).addClass('disabled').attr('disabled', 'disabled');
    }
};

//Função para subir o scroll da tela quando necessario
//Chamar função em paginas com bastantes campos onde a função fara uma anima~ção do ponto atual até o topo
function SobeScroll() {

    $("html, body").animate({ scrollTop: 0 }, "slow");

}

function gerenciarMigalha(tituloEcu, indexEcu, paginaAtual, controllerEcu) {


    if (paginaAtual == "CMCE") {
        $("#dividerCMCE").hide();
        $("#barraIndexEcu").hide();
        $("#CMCE").hide();
        $("#paginaAtual").text('CMCE');
        $('#tituloEcu').text("Sistema de Convidados e Eventos");

    } else {
        $('#tituloEcu').append(tituloEcu);
        $('#indexEcu').text(indexEcu);
        $('#controllerEcu').val(controllerEcu);
        $('#paginaAtual').text(paginaAtual);

        if (paginaAtual == indexEcu) {
            $('#indexEcu').text('');
            $('#barraIndexEcu').hide();
        }
    }
}

//remove item do array
Array.prototype.remove = function (item) {
    var indiceItem = this.indexOf(item);
    if (indiceItem > -1) {
        for (var indice = indiceItem; indice < this.length - 1; indice++) {
            this[indice] = this[indice + 1];
        }
        this.pop();
    }
};


function htmlEscape(str) {
    return String(str)
            .replace(/&/g, '&amp;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&#39;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;');
}

//VALIDAÇÕES
function EmailValido(email) {
    var exp = /^([\w]+)(\.[\w]+)*@([\w\-]+\.){1,5}([A-Za-z]){2,4}$/;
    return exp.test(email);
} RecuperaConfiguracoesDataTable