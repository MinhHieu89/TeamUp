var MemberEditController = function() {

    var positionToggle = function(container) {
        $(container).on("change", "input[type='checkbox']", positionToggleHandler);
    };

    var positionToggleHandler = function(e) {
        var id = $(e.target).attr("id");
        var item = $("label[for='" + id + "'].positions-item");
        item.toggleClass("positions-item--selected");
    };

    var setDatePicker = function(selector) {
        $(selector).datepicker({
            changeMonth: true,
            changeYear: true,
            yearRange: "-100:+0"
        });
    };

    return {
        positionToggle: positionToggle,
        setDatePicker: setDatePicker
    };

}();