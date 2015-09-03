/**
* Registra a função maxlength como uma função jQuery, ela será usada para
* limitar o tamanho de elementos do tipo textArea.
* Exemplo: $(SELETOR).maxLength( { limit : 500 } );
*/
jQuery.fn.extend({
    maxLength: function (opts) {
        var defaults = {
            limit: 10
        },
        options = $.extend(defaults, opts),
        $textAreas = $(this);

        $textAreas.each(function () {
            var $textArea = $(this);
            $textArea.on('keyup keypress keydown change mouseover', function (event) {
                var valor = $textArea.val();
                var isIE = (typeof window.ActiveXObject != 'undefined');
                // Caso o navegador seja o IE keyCode é a chave, caso contrário a chave é which.
                var key = isIE ? event.keyCode : event.which;
                var lineBreaks = 0;

                if ((key >= 48 && key <= 112) || key == 13 || key == 32) {
                    if (valor.length >= options.limit) {
                        // Previne que a ação padrão de qualquer um dos eventos sejá disparada.
                        event.preventDefault();
                    }
                }
                for (var indice = 0; indice < valor.length; indice++) {
                    var caractere = valor.charAt(indice);
                    if (caractere.match(/\n|\r/)) {
                        lineBreaks++;
                    }
                }
                if ((valor.length + lineBreaks) > options.limit) {
                    $textArea.val($textArea.val().substring(0, options.limit - lineBreaks));
                    // Faz com que a barra de rolagem permaneça junto ao último caractere digitado.
                    this.scrollTop = this.scrollHeight;
                }
            });
        });
    }
});
