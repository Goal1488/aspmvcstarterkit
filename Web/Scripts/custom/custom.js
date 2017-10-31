$(document).ready(function() {
    debugger;

    initTyped();
});

var initTyped = function() {
    var stringsToPrint = 
        ["harrasing you via phone..", "abusing you with hundreds of calls per day", "making a lot of unwanted calls, so u might be asking: 'how to stop that shit?'"];

    $(".js-typed-placeholder").typed({
        strings: stringsToPrint,
        typeSpeed: 100,// time before typing starts, ms
        startDelay: 0,
        backSpeed: 25,// backspacing speed
        backDelay: 500,//// time before backspacing
        loop: false//// loop
    });
};

