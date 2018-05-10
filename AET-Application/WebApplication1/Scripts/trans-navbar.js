/*TRANSNAVBAR*/
$(window).scroll(function () {
    if ($(this).scrollTop() > 5)  /*height in pixels when the navbar becomes non opaque*/ {
        $('.opaque-navbar').addClass('opaque');
        $('.navbar').addClass('FadeInDown');
    } else {
        $('.opaque-navbar').removeClass('opaque');
        $('.navbar').removeClass('FadeInDown');

    }
});

$(document).ready(function () {
    $('[data-toggle="offcanvas"]').click(function () {
        $('#side-menu').toggleClass('hidden-xs');
    });
});