(function(e) {
    e.BackToTop = {
        defaults: { text: "Back to top", autoShow: true, autoShowOffset: 0, timeEffect: 500, effectScroll: "linear", appearMethod: "slide", bottom: 10, opacity: .9 },
        init: function(t) {
            opts = e.extend({}, e.BackToTop.defaults, t), e.BackToTop._constructLink();
            if (opts.autoShow)
                e(window).scroll(function() {
                    if (e(this).scrollTop() > opts.autoShowOffset) {
                        switch (opts.appearMethod) {
                        case"fade":
                            divBack.stop(true, true).fadeIn("fast");
                            break;
                        case"slide":
                            divBack.stop(true, true).slideDown("fast");
                            break;
                        default:
                            divBack.stop(true, true).show()
                        }
                    } else {
                        switch (opts.appearMethod) {
                        case"fade":
                            divBack.stop(true, true).fadeOut("fast");
                            break;
                        case"slide":
                            divBack.stop(true, true).slideUp("fast");
                            break;
                        default:
                            divBack.stop(true, true).hide()
                        }
                    }
                });
            e("#BackToTop").click(function(t) {
                t.preventDefault();
                e("body,html").animate({ scrollTop: 0 }, opts.timeEffect, opts.effectScroll)
            })
        },
        _constructLink: function() {
            divBack = e("<a />", { id: "BackToTop", href: "#body", "class": "btn btn-inverse", css: { bottom: opts.bottom, opacity: opts.opacity }, html: "<span>" + opts.text + "</span>" }).prependTo("body");
            if (!opts.autoShow)divBack.show()
        }
    };
    BackToTop = function(t) { e.BackToTop.init(t) }
})(jQuery)