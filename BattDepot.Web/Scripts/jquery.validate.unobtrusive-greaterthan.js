(function($) {
    $.validator.unobtrusive.adapters.add('greaterthaninteger', ['other'], function(options) {
        var getModelPrefix = function(fieldName) {
            return fieldName.substr(0, fieldName.lastIndexOf(".") + 1);
        };

        var appendModelPrefix = function(value, prefix) {
            if (value.indexOf("*.") === 0) {
                value = value.replace("*.", prefix);
            }
            return value;
        };

        var prefix = getModelPrefix(options.element.name),
            other = options.params.other,
            fullOtherName = appendModelPrefix(other, prefix),
            element = $(options.form).find(":input[name=" + fullOtherName + "]")[0];

        options.rules['greaterthaninteger'] = element;
        if (options.message != null) {
            options.messages['greaterthaninteger'] = options.message;
        }
    });

    $.validator.addMethod('greaterthaninteger', function (value, element, params) {
        var userValue = parseInt(value, 10);
        var minValue = parseInt($(params).val(), 10);

        if (isNaN(userValue) || isNaN(minValue)) {
            return false;
        }

        return userValue > minValue;
    });

})(jQuery);

(function ($) {
    $.validator.unobtrusive.adapters.add('greaterthanfloat', ['other'], function (options) {
        var getModelPrefix = function (fieldName) {
            return fieldName.substr(0, fieldName.lastIndexOf(".") + 1);
        };

        var appendModelPrefix = function (value, prefix) {
            if (value.indexOf("*.") === 0) {
                value = value.replace("*.", prefix);
            }
            return value;
        };

        var prefix = getModelPrefix(options.element.name),
            other = options.params.other,
            fullOtherName = appendModelPrefix(other, prefix),
            element = $(options.form).find(":input[name=" + fullOtherName + "]")[0];

        options.rules['greaterthanfloat'] = element;
        if (options.message != null) {
            options.messages['greaterthanfloat'] = options.message;
        }
    });

    $.validator.addMethod('greaterthanfloat', function (value, element, params) {
        var userValue = parseFloat(value, 10);
        var minValue = parseFloat($(params).val(), 10);

        if (isNaN(userValue) || isNaN(minValue)) {
            return false;
        }

        return userValue > minValue;
    });

})(jQuery);

(function($) {

    function setValidationValues(options, ruleName, value) {
        options.rules[ruleName] = value;
        if (options.message) {
            options.messages[ruleName] = options.message;
        }
    }

    $.validator.addMethod("greaterthan", function (value, element, param) {
        // check if dependency is met
        if (!this.depend(param, element))
            return "dependency-mismatch";
        return element.checked;
    });

    $.validator.unobtrusive.adapters.add("greaterthan", function(options) {
        setValidationValues(options, "greaterthan", true);
    });

    $.validator.unobtrusive.parse();

})(jQuery);