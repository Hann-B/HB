$(document).ready(function () {



    //$(".pageDown").click(function () {
    //    $('html,body').animate({
    //        scrollTop: $("#page2").offset().top
    //    },
    //        'slow');
    //});

    $(document).on('click', 'a.page-scroll', function (event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: ($($anchor.attr('href')).offset().top - 50)
        }, 1250, 'easeInOutExpo');
        event.preventDefault();
    });
});