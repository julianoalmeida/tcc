(function(e, t, n, r) {
    e.jshuttle = function(t, r, i) {
        function o(t, n) {
            return e(":selected", e(t)).detach().appendTo(e(n));
        }

        function u(t, n) {
            return e(t).children().detach().appendTo(e(n));
        }

        var s = e.extend({
            add: null,
            addAll: null,
            remove: null,
            removeAll: null,
            moveOnDblClick: true,
            onshuttle: function() {
            }
        }, i);
        if (s["moveOnDblClick"]) {
            e(n).delegate(t, "dblclick.shuttle", function() {
                var n;
                o(t, r);
                s["onshuttle"].call(e, n, "add");
            });
            e(n).delegate(r, "dblclick.shuttle", function() {
                var n;
                o(r, t);
                s["onshuttle"].call(e, n, "remove");
            });
        }
        if (s["add"]) {
            e(n).delegate(s["add"], "click.shuttle", function() {
                var n;
                n = o(t, r);
                s["onshuttle"].call(e, n, "add");
            });
        }
        if (s["remove"]) {
            e(n).delegate(s["remove"], "click.shuttle", function() {
                var n;
                n = o(r, t);
                s["onshuttle"].call(e, n, "remove");
            });
        }
        if (s["addAll"]) {
            e(n).delegate(s["addAll"], "click.shuttle", function() {
                var n;
                n = u(t, r);
                s["onshuttle"].call(e, n, "add");
            });
        }
        if (s["removeAll"]) {
            e(n).delegate(s["removeAll"], "click.shuttle", function() {
                var n;
                n = u(r, t);
                s["onshuttle"].call(e, n, "remove");
            });
        }
        return this;
    };
})(jQuery, window, document)