$(document).ready(() => {
    $("#downButton").click(function (e) {
        var el = $(this)
        e.preventDefault();
        console.log("got here");
        var page_index = $(this).data("index");
        el.moveTo(page_index);
    });

})