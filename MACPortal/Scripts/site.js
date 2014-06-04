var pagesLength = 0;
var lastUrl = "";
var goingBack = false;

if ($.cookie != null) {
    var cookiesLength = $.cookie("pagesLength", Number);
    var lastUrl = $.cookie("lastUrl");

    console.log("Cookies Length: " + cookiesLength);
    if (cookiesLength != null && typeof cookiesLength != "undefined") {
        pagesLength = cookiesLength;
        console.log("Pages Length: " + pagesLength);
    }
}

var unloading = false;
window.onbeforeunload = function (e) {
    unloading = true;
    var expiration = new Date();

    //Expires in 30 seconds! This should be enough for the next page to load and get this cookie.
    expiration.setSeconds(expiration.getSeconds() + 30);

    // Adds this page to the count
    if (lastUrl != window.location.href) {
        if (goingBack) {
            pagesLength--;
        } else {
            pagesLength++;
        }
    } else {
        if (goingBack) {
            pagesLength -= 2;
        }
    }
    $.cookie("pagesLength", pagesLength, { path: "/", expires: expiration });
    $.cookie("lastUrl", window.location.href, { path: "/", expires: expiration });
};


$(function () {
    var el = $(".back-bt");
    var altUrl = el.data("back-bt");
    console.log("Pages Length: " + pagesLength);
    if (pagesLength > 0) {
        console.log("NOT Setting HREF");
        $(el).click(function () {
            console.log("back-click");
            window.history.back();
            if (unloading) {
                goingBack = true;
            } else {
                window.location = window.location.origin + altUrl;
            }
        });
    } else {
        $(el).attr("href", window.location.origin + altUrl);
    }
});


/**
* Anchor Animate
*/
jQuery.fn.anchorAnimate = function(settings) {
    settings = jQuery.extend({
        speed: 1100
    }, settings);

    return this.each(function() {
        var destination = $(this).offset().top;
        $("html:not(:animated),body:not(:animated)").animate({ scrollTop: destination }, settings.speed, function() {

        });
        return false;
    });
};


/**
* Input Styles
*/
var processInputStyles = function () {
    $("div.input-style:not(.input-style-processed)").each(function (index) {
        console.log("Processing Input Style");
        var that = $(this);
        that.removeClass("input-style");
        var input = that.find("input:first, select:first");
        input.wrap('<div class="input-style input-style-processed"></div>');
        var parent = input.parent();
        input.focusin(function() {
            parent.addClass("selected");
        });
        input.focusout(function () {
             parent.removeClass("selected");
        });
    });
};

/**
* Input Styles
*/
var processAddButtons = function () {
    $(".add-button").each(function (index) {
        console.log("Processing Input Style");
        var that = $(this);
        that.removeClass("input-style");
        that.find("input:first").wrap('<div class="input-style"></div>');
    });
};

/**
* Components Initialization
*/
processInputStyles();


/**
* Custom Binding for input, label, validation
*/
ko.bindingHandlers.response = {
    init: function (el, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        console.log("RESPONSE");
        var item;
        var id = valueAccessor();
        console.log("Processing Response Bind with " + window.vm.RefundItems().length);
        for (var i = 0; i < window.vm.RefundItems().length; i++) {
            var current = window.vm.RefundItems()[i];
            console.log(current.RefundItemID() + " to " + id);
            if (current.RefundItemID().toString() == id) {
                item = current;
                break;
            }
        }

        if (item == null) {
            console.log("Could not find RefundItem with ID='" + id + "'");
            return false;
        }

        item.Comment = ko.observable("");
        item.Status(window.useItemStatus() ? item.Status() : window.startingResponse());
        item.ReceivedInvoice(item.ReceivedInvoice() == false ? "false" : "true");

        // Make a modified binding context, with a extra properties, and apply it to descendant elements
        var childBindingContext = bindingContext.createChildContext(item);
        ko.applyBindingsToDescendants(childBindingContext, el);

        // Also tell KO *not* to bind the descendants itself, otherwise they will be bound twice
        return { controlsDescendantBindings: true };
    }
};

fineBindings = new Hashtable();
ko.bindingHandlers.fineuploader = {
    init: function (el, valueAccessor, allBindingsAccessor, viewModel) {
        var fineUploader = $(el);
        window.proccessResponseFineUpload(fineUploader, viewModel);
    }
};

ko.bindingHandlers.input = {
    init: function (el, valueAccessor, allBindingsAccessor, viewModel) {
        console.log("INPUT");
        var contextName = valueAccessor();
        var modelID = ko.unwrap(viewModel.RefundItemID);
        $(el).attr("id", contextName + "_" + modelID + "_");
        $(el).attr("name", contextName + "[" + modelID + "]");
    }
};

ko.bindingHandlers.validation = {
    init: function (el, valueAccessor, allBindingsAccessor, viewModel) {
        var contextName = valueAccessor();
        var modelID = ko.unwrap(viewModel.RefundItemID);
        $(el).attr("data-valmsg-for", contextName + "[" + modelID + "]");
    }
};

ko.bindingHandlers.label = {
    init: function (el, valueAccessor, allBindingsAccessor, viewModel) {
        var contextName = valueAccessor();
        var modelID = ko.unwrap(viewModel.RefundItemID);
        $(el).attr("for", contextName + "[" + modelID + "]");
    }
};