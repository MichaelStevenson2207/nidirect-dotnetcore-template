$('#SearchPostCode').on('keyup keypress', function (e)
{
    var keyCode = e.keyCode || e.which;
    if (keyCode === 13)
    {
        getAddresses();
        e.preventDefault();
        return false;
    }

    return true;
});

function getAddresses() {
    let postCode = $("#SearchPostCode").val();
    $("#addressError").hide();

    if (postCode !== "") {
        $("#loadSpinner").show();

        $.get('/Pointer/GetAddresses/', { postCode: postCode }, function (data) {
            $("#SearchAddress").empty();
            $("#SearchAddress").append($("<option value=''>Select Address</option>"));

            $.each(data, function () {
                $(".govuk-error-summary").hide();
                $("#loadSpinner").hide();
                $("#SearchAddressList").show();
                $("#addressError").hide();
                $("#SearchAddress").append($("<option></option>").val(this["buildingNumber"]).html(this["buildingNumber"] + ' ' + this["primaryThorfare"] + ',' + this["town"] + ',' + this["postcode"]));
            });
        }).fail(function () {
            $(".govuk-error-summary").show();

            if ($(".error-items").length === 0) {
                $(".govuk-error-summary__list").append("<li><a class='error-items' href='#SearchPostCode'>Not a real postcode. Address could not be found.</a></li>");
            }

            $("#PostCodeSearchComponent").addClass("govuk-form-group--error");
            $("#SearchPostCode").addClass("govuk-input--error");
            $("#SearchPostCode").val("Not a postcode");
            $("#addressError").show();
            $("#loadSpinner").hide();
            $("#SearchAddressList").hide();
        });
    }
}

function fillAddressTextBoxes() {
    let myText = $("#SearchAddress :selected").text();

    if (myText !== "Select Address") {
        let addressArray = myText.split(',');

        $("#Address1").val("");
        $("#Address2").val("");
        $("#Address3").val("");
        $("#TownCity").val("");
        $("#PostCode").val("");

        $("#Address1").val(addressArray[0]);
        $("#TownCity").val(addressArray[1]);
        $("#PostCode").val(addressArray[2]);
    }
}