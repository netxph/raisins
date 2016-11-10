var PaymentController = function () {
    var container;
    var paymentAction;
    var currencyDropdown;
    var paymentClassDropdown;
    var paymentTabs;
    var submitButton;

    var init = function (containerSelector) {
        container = $(containerSelector);
        initializeObjects();
        container.tabs();
        bindEvents();
        activatePrechosenTab();
        
    };

    var initializeObjects = function () {
        currencyDropdown = container.find(".js-currency-dropdown");
        paymentTabs = container.find(".js-payment-tab-button");
        submitButton = container.find("#submit");
        paymentAction = submitButton.data("action");
        paymentClassDropdown = container.find(".js-payment-class");
    }

    var activatePrechosenTab = function () {
        var originalValue = paymentClassDropdown.data("originalvalue");
        paymentTabs.eq(originalValue).trigger("click");
    }

    var bindEvents = function () {
        paymentTabs.off("click.setPaymentElementAttr").on("click.setPaymentElementAttr", setElementAttrBasedOnPaymentClass);
        submitButton.off("click.setPaymentElementAttr").on("click.setPaymentElementAttr", enableCurrencyDropdown);
    }

    var setElementAttrBasedOnPaymentClass = function () {
        var clickedTab = $(this);
        if (clickedTab.hasClass("js-local")) {
            currencyDropdown.prop("disabled", true);
            currencyDropdown.val(1);
            paymentClassDropdown.val(0);
            submitButton.val(paymentAction + " LOCAL");
        }
        else if (clickedTab.hasClass("js-external")) {
            currencyDropdown.prop("disabled", false);
            paymentClassDropdown.val(1);
            submitButton.val(paymentAction + " EXTERNAL");
        } else {
            currencyDropdown.prop("disabled", false);
            paymentClassDropdown.val(2);
            submitButton.val(paymentAction + " FOREIGN");
        }
    };

    var enableCurrencyDropdown = function () {
        currencyDropdown.prop("disabled", false);
    };

    return {
        init: init
    }

}();




