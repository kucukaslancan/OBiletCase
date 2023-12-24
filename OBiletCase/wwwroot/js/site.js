function AjaxCall(metodName, data, returnFunction, submitType) {
    $.ajax({
        type: submitType,
        url: window.location.protocol + '//' + window.location.host + '/' + metodName,
        /*   async: true,*/
        data: data,
        beforeSend: function (xhr) {
        },
        success: function (msg, xhr) {
            eval(returnFunction);
        },
        error: function (xhr, msg) {
            console.log(xhr);
            console.log(msg);
        }
    });
}
function base64Encode(str) {
    return btoa(unescape(encodeURIComponent(str)));
}
function base64Decode(encodedStr) {
    return decodeURIComponent(escape(atob(encodedStr)));
}
function SetToday() {
    var today = new Date();
    $('#JourneyDate').datepicker('setDate', today);

    $('.buttons a.button').removeClass('active');
    $('.buttons a.button:nth-child(1)').addClass('active');
}
function SetTomorrow() {
    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    $('#JourneyDate').datepicker('setDate', tomorrow);

    $('.buttons a.button').removeClass('active');
    $('.buttons a.button:nth-child(2)').addClass('active');
}


// localstorage Operations
$('#JourneyDate').change(function () {
    localStorage.setItem("departureDate", this.value);
});
$(".origin[name=origin]").change(function () {
    localStorage.setItem("origin", base64Encode(this.value + '|' + $(this).find(":selected").text()));

});
$(".destination[name=destination]").change(function () {
    localStorage.setItem("destination", base64Encode(this.value + '|' + $(this).find(":selected").text()));
});

// Index Operations
function Change() {

    var originValue = $(".origin[name=origin]").val();
    var originText = $(".origin[name=origin]").find(":selected").text();
    var destinationValue = $(".destination[name=destination]").val();
    var destinationText = $(".destination[name=destination]").find(":selected").text();

    var option = $('<option>', {
        value: originValue,
        text: originText
    });

    $(".destination[name=destination]").append(option);
    $(".destination[name=destination]").selectpicker('refresh');
    $(".destination[name=destination]").val(originValue).trigger('change');


    var option = $('<option>', {
        value: destinationValue,
        text: destinationText
    });

    $(".origin[name=origin]").append(option);
    $(".origin[name=origin]").selectpicker('refresh');
    $(".origin[name=origin]").val(destinationValue).trigger('change');

    //$(".origin[name=origin]").val(destinationValue).trigger("change");
    //$(".destination[name=destination]").val(originValue).trigger("change");

    //$(".origin[name=origin]").selectpicker('refresh');
    //$(".destination[name=destination]").selectpicker('refresh');
}
function SetOriginStorage() {
    var storedOrigin = localStorage.getItem("origin");
    if (storedOrigin !== null && storedOrigin !== "") {

        var decodedOrigin = base64Decode(storedOrigin);
        decodedOrigin = decodeURIComponent(decodedOrigin);

        var originParts = decodedOrigin.split('|');

        if (originParts.length === 2 && originParts[0] !== '' && originParts[1] !== '') {
            var option = $('<option>', {
                value: originParts[0],
                text: originParts[1]
            });

            $(".origin[name=origin]").append(option);
            $(".origin[name=origin]").selectpicker('refresh');
            $(".origin[name=origin]").val(originParts[0]).trigger('change');
        }
    }
}
function SetDestinationStorage() {
    var storedDestination = localStorage.getItem("destination");
    if (storedDestination !== null && storedDestination !== "") {

        var decodedDestination = base64Decode(storedDestination);
        decodedDestination = decodeURIComponent(decodedDestination);

        var destinationParts = decodedDestination.split('|');

        if (destinationParts.length === 2 && destinationParts[0] !== '' && destinationParts[1] !== '') {
            var option = $('<option>', {
                value: destinationParts[0],
                text: destinationParts[1]
            });

            $(".destination[name=destination]").append(option);
            $(".destination[name=destination]").selectpicker('refresh');
            $(".destination[name=destination]").val(destinationParts[0]).trigger('change');
        }
    }
}
function GetPopularLocationsRetVal(response, Idle) {
    var originOptions = $("." + Idle + "[name=" + Idle + "]");
    originOptions.empty();

    $.each(response, function (index, item) {
        var option = $('<option>', {
            value: item.id,
            text: item.name
        });
        originOptions.append(option);
    });

    originOptions.selectpicker('refresh');
}
function SearchLocationRetVal(response, Idle) {
    var options = $("." + Idle + "[name=" + Idle + "]");
    options.empty();

    $.each(response, function (index, item) {
        var option = $('<option>', {
            value: item.id,
            text: item.name
        });
        options.append(option);
    });

    options.selectpicker('refresh');
}

$(document).ready(function () {
    $('.origin').selectpicker('setStyle', 'originBtn', false);
    $('.destination').selectpicker('setStyle', 'destinationBtn', false);

    $('#JourneyDate').datepicker({
        todayHighlight: true,
        autoclose: true,
       // startDate: "today",
        language: "tr",
        format: "dd MM yyyy DD",

    });

    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    $('#JourneyDate').datepicker('setDate', tomorrow);

    SetOriginStorage();
    SetDestinationStorage();

    $('#JourneyDate').datepicker('setDate', localStorage.getItem("departureDate"));

    $(".originBtn").on('click', function () {

        $(".origin[name=origin]").find('option').remove();
        $(".origin[name=origin]").val('');
        $(".origin[name=origin]").selectpicker('refresh');

        AjaxCall('Home/GetPopularLocations', '', 'GetPopularLocationsRetVal(msg,"origin")', 'POST');
    });

    $(".destinationBtn").on('click', function () {
        $(".destination[name=destination]").find('option').remove();
        $(".destination[name=destination]").val('');
        $(".destination[name=destination]").selectpicker('refresh');

        AjaxCall('Home/GetPopularLocations', '', 'GetPopularLocationsRetVal(msg,"destination")', 'POST');
    });

    var delayTimer;
    $(".origin input").on("input", function () {
        var originSearchText = $(this).val();
        var data = {
            searchText: originSearchText
        };

        // Her klavyeye input girdisinde istek atmamak için, giriş işlemi bittikten yarım saniye sonra isteği gönder.
        clearTimeout(delayTimer);
        delayTimer = setTimeout(function () {
            AjaxCall('Home/SearchLocation', data, 'SearchLocationRetVal(msg,"origin")', 'POST');
        }, 500);



    });

});

