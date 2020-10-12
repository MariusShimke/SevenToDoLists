// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    document.getElementById("createDescription").value = "";

    function getDayName(dateStr, locale) {
        var date = new Date(dateStr);
        return date.toLocaleDateString(locale, { weekday: 'long' });
    }

    var d = new Date();
    var weekday = new Array(7);
    weekday[0] = "Sunday";
    weekday[1] = "Monday";
    weekday[2] = "Tuesday";
    weekday[3] = "Wednesday";
    weekday[4] = "Thursday";
    weekday[5] = "Friday";
    weekday[6] = "Saturday";

    var dayNum = weekday[d.getDay()];
    if (dayNum == 0) {
        dayNum = 7
    }
   
    var dateStr = d.toString();
    var day = getDayName(dateStr, 'en-US');

    const dayIndex = weekday.indexOf(dayNum);

    document.getElementById("createWeekDay").value = dayIndex;
   //$('#createWeekDay input[type="range"]').val(dayIndex).trigger('change'); 

    const $valueSpan = $('.valueSpan2');
    const $value = $('#createWeekDay');
 

    $valueSpan.html(day);
    $value.on('input change', () => {

        if ($value.val() == 1) {
            $valueSpan.html('Monday');
        } else if ($value.val() == 2) {
            $valueSpan.html('Tuesday');
        } else if ($value.val() == 3) {
            $valueSpan.html('Wednesday');
        } else if ($value.val() == 4) {
            $valueSpan.html('Thursday');
        } else if ($value.val() == 5) {
            $valueSpan.html('Friday');
        } else if ($value.val() == 6) {
            $valueSpan.html('Saturday');
        } else if ($value.val() == 7) {
            $valueSpan.html('Sunday');
        }   
    });

    const $valueSpan2 = $('.valueSpan1');
    const $value2 = $('#createPriority');
    $valueSpan2.html($value2.val());
    $value2.on('input change', () => {
        $valueSpan2.html($value2.val());
    });

    
    //var dd = new Date();
    //var nn = dd.getDay()
    //alert(weekday[dd.getDay()]);
    if ($('#carousel-vertical').carousel(dayIndex).hasClass('active')) {
       
    }
});

function changeBoxValue(cb) {    
    if (cb.checked) {
        
        document.getElementById("isDoneNumValue").value = "1";
     
    } else {
       
        document.getElementById("isDoneNumValue").value = "0";
    
    }
}

if (window.history.replaceState) {
    window.history.replaceState(null, null, window.location.href);
}

//function changeToRegForm() {
    
//    document.getElementById("loginForm").classList.add("hidden");
//    document.getElementById("registerForm").classList.remove("hidden");
//};

//function changeToLiginForm() {

//    document.getElementById("registerForm").classList.add("hidden");
//    document.getElementById("loginForm").classList.remove("hidden");
//};



