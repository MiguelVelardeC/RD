
//Creating Angular controllers
function CreateControll(idJson, calendarImg, maxlegth, onLoad, onChange) {
    return function ($scope) {
        $scope.master = [];
        $scope.maxLength = maxlegth;
        $scope.getMaxlenght = function () {
            return $scope.maxLength;
        };
        $scope.replaceSpace = function (name) {
            return name.replace(" ", "_");
        };
        $scope.SaveChanges = function () {
            var stringMaster = JSON.stringify($scope.master).replace(/\,"\$\$hashKey":"[^"]+"/g, "");
            if (validateLength($scope.master, $scope.maxLength)) {
                $('#' + idJson).val(stringMaster);
            }
            if (onChange) { onChange($scope); }
        };
        $scope.LoadJson = function () {
            try {
                var jsonMaster = $.parseJSON($('#' + idJson).val());
                if (jsonMaster != null && jsonMaster != undefined && jsonMaster != "") {
                    $scope.master = jsonMaster;
                }
            } catch (q) { }
            setTimeout(function () {
                //$('input.date').datepicker({
                //    showOn: "both",
                //    buttonImage: calendarImg,
                //    buttonImageOnly: true,
                //});
            }, 1000);
            if (onLoad) { onLoad($scope); }
        };
    }
        function validateLength(master, maxLength) {
            var name = '';
            for (var i = 0; i < master.length; i++) {
                if (master[i].group != null && master[i].group != undefined) {
                    for (var j = 0; j < master[i].group.length; j++) {
                        if (master[i].group[j].value.length > maxLength) {
                            name = master[i].group[j].name;
                            break;
                        }
                        else
                        {
                            if (master[i].group[j].value.length == 0) {
                                name = master[i].group[j].name;
                                break;
                            }
                        }
                    }
                } else {
                    if (master[i].value.length == 0) {
                        name = master[i].name;
                        break;
                    }
                    else
                    {
                        if (master[i].value.length > maxLength) {
                            name = master[i].name;
                            break;
                        }
                    }
                }
            }
            if (name != '') {
                $('#validation' + name.replace(' ', '_')).show();
                return false;
            }
            $('span.angularValidation').hide();
            return true;
        }
}

function validateLength(master, maxLength) {
    var name = '';
    for (var i = 0; i < master.length; i++) {
        if (master[i].group != null && master[i].group != undefined) {
            for (var j = 0; j < master[i].group.length; j++) {
                if (master[i].group[j].value.length > maxLength) {
                    name = master[i].group[j].name;
                    break;
                }
                else
                {
                    if (master[i].group[j].value.length == 0) {
                        name = master[i].group[j].name;
                        break;
                    }
                }
            }
        } else {
            if (master[i].value.length > maxLength) {
                name = master[i].name;
                break;
            }
            else
            {
                if (master[i].value.length == 0) {
                    name = master[i].name;
                    break;
                }
            }
        }
    }
    if (name != '') {
        $('#validation' + name.replace(' ', '_')).show();
        return false;
    }
    $('span.angularValidation').hide();
    return true;
}

jQuery(function ($) {
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '&#x3c;Ant',
        nextText: 'Sig&#x3e;',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
        'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun',
        'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Mi&eacute;rcoles', 'Jueves', 'Viernes', 'S&aacute;bado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mi&eacute;', 'Juv', 'Vie', 'S&aacute;b'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'S&aacute;'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);
});
