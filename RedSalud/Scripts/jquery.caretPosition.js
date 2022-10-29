// Taken From:
// http://stackoverflow.com/questions/2897155/get-caret-position-within-an-text-input-field
new function ($) {
    $.fn.getCursorPosition = function () {
        var pos = 0;
        var input = $(this).get(0);
        // IE Support
        if (navigator.appVersion.indexOf("MSIE") != -1) { //document.selection) {
            //input.focus();
            var sel = document.selection.createRange();
            var selLen = document.selection.createRange().text.length;
            sel.moveStart('character', -input.value.length);
            pos = sel.text.length - selLen;
        }
            // Firefox support
        else if (input.selectionStart || input.selectionStart == '0')
            pos = input.selectionStart;

        return pos;
    }
}(jQuery);
