var AccountCreateController = function () {
    var contianer;
    var roleDropdown;
    var beneficiaryDropdown;
    var currencyDropdown;

    var init = function (containerSelector) {
        container = $(containerSelector);
        initializeObjects();
        bindEvents();
    };

    var initializeObjects = function () {
        roleDropdown = container.find(".js-role-dropdown");
        beneficiaryDropdown = container.find(".js-beneficiary-dropdown");
        currencyDropdown = container.find(".js-currency-dropdown");
    };

    var bindEvents = function () {
        roleDropdown.on("change", toggleDropdownsVisibility);
    }

    var toggleDropdownsVisibility = function () {
        if ($(this).val() == 1) {
            beneficiaryDropdown.find("select").val(1);
            beneficiaryDropdown.hide();
            currencyDropdown.find("select").val(1);
            currencyDropdown.hide();
        } else {
            beneficiaryDropdown.show();
            currencyDropdown.show();
        }
    }

    return {
        init: init
    }
}();