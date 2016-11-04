var PaymentController = function () {
    var container;
    var paymentAction;
    var currencyDropdown;
    var paymentTabs;

    var init = function (containerSelector, payAction) {
        container = $(containerSelector);
        paymentAction = payAction;
        currencyDropdown = container.find(".js-currency-dropdown");
        paymentTabs = container.find(".js-payment-tab-button");

        container.tabs();
        bindEvents();
        paymentTabs.eq(0).trigger("click");
    };

    var bindEvents = function () {
        paymentTabs.off("click.setPaymentElementAttr").on("click.setPaymentElementAttr", setElementAttrBasedOnPaymentClass);
        container.off("click.setPaymentElementAttr").on("click.setPaymentElementAttr", "#submit", enableCurrencyDropdown);
    }

    var setElementAttrBasedOnPaymentClass = function () {
        var clickedTab = $(this);
        var submitButton = container.find("#submit");
        var paymentClassDropdown = container.find(".js-payment-class");
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




