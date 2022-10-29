var showing = null;
var esc = true;
function createPopup(divContainer, openButton, closeButton, openCallBack, closeCallBack) {
    $(divContainer).hide();
    if (esc) {
        $(document).keypress(function (e) {
            e = e || window.event;
            if (e.keyCode == 27 && $(".popup_Mask").is(':visible')) {
                $(".popup_Mask, .Default_Popup").fadeOut(500);
                showing = null;
            }
        }); esc = false;
    }

    $(openButton).click(function (e) {
        e.preventDefault();
        showPopup(divContainer);
        if (openCallBack) {
            openCallBack($(openButton));
        }
        return false;
    });

    function clickToClose(e) {
        e.preventDefault();
        $(".popup_Mask, .Default_Popup").fadeOut(500);
        showing = null;
        if (closeCallBack) {
            closeCallBack();
        }
        return false;
    }
    $('div.popup_Mask').click(function (e) {
        e.preventDefault();
        $(".popup_Mask, .Default_Popup").fadeOut(500);
        showing = null;
        return false;
    });
    $(window).resize(function () {
        if (showing) {
            var winH = $(window).height();
            var winW = $(window).width();

            $(showing).css('top', window.pageYOffset + Math.abs(winH / 2 - $(divContainer).height() / 2));
            $(showing).css('left', winW / 2 - $(divContainer).width() / 2);
        }
    });

    if (closeButton != true) {
        $(closeButton).click(clickToClose);
        closeButton = false;
    }

    //if (isCompany) {
    //    createCompanyTitle(divContainer, closeButton);
    //} else {
    //    createTitle(divContainer, closeButton);
    //}

    if (closeButton) {
        $('#' + divContainer.replace('#', '') + '_closePopupButton').click(clickToClose);
    }
}
function createPopuptitle(divContainer, closeButton) {
    //if (isCompany) {
    //    createCompanyTitle(divContainer, closeButton);
    //} else {
    //    createTitle(divContainer, closeButton);
    //}
    if (closeButton) {
        $('#' + divContainer.replace('#', '') + '_closePopupButton').click(function (e) {
            e.preventDefault();
            hidePopup(divContainer);
            return false;
        });
    }
}
function createTitle(divContainer, closeButton) {
    var divParent = divContainer + '>div';
    var title = $(divParent + ' span.title');
    $(divParent).prepend($('<div class="columnHead columnHeadTitle"></div>'));
    divParent = divParent + '>div.columnHead';
    if (closeButton) {
        $(divParent).append(title);
        $(divParent).append($('<div class="commands">\n' +
         '\t<a id="' + divContainer.replace('#', '') + '_closePopupButton" class="secondaryButton" href="#">' +
         (closeButtonText ? closeButtonText : 'X') + '</a>\n' +
         '</div>'));
    }
}
function createCompanyTitle(divContainer, closeButton) {
    var divParent = divContainer + '>div';
    var title = $(divParent + ' span.title');
    if ($(divParent + '>div.columnHead').length <= 0) {
        $(divParent).prepend($('<div class="columnHead"></div>'));
    }
    divParent = divParent + '>div.columnHead';
    if ($(divParent + '>span.title').length <= 0) {
        $(divParent).append(title);
    }
    if (closeButton) {
        $(divParent).append($('<div class="commands">\n' +
         '\t<a id="' + divContainer.replace('#', '') + '_closePopupButton" class="secondaryButton" href="#">' +
         (closeButtonText ? closeButtonText : 'X') + '</a>\n' +
         '</div>'));
    }
}

function showPopup(divContainer) {
        var winH = $(window).height();
        var winW = $(window).width();
        if ($(divContainer).height() > winH) {
            $(divContainer).css('top', window.pageYOffset + 5);
        } else {
            $(divContainer).css('top', window.pageYOffset + Math.abs(winH / 2 - $(divContainer).height() / 2));
        }
        $(divContainer).css('left', winW / 2 - $(divContainer).width() / 2);

        $(divContainer + ", .popup_Mask").fadeIn(500);
        showing = divContainer;
}
function hidePopup(divContainer) {
    $(divContainer + ", .popup_Mask, .Default_Popup").fadeOut(500);
    showing = null;
}