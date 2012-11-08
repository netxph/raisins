$().ready(function () {
    $("ul#teamscontent li a").hover(
        function () {
            $(this).parent().find('h4').show("slide", { direction: "down", easing: 'easeOutBounce' });
        },
        function () {
            $(this).parent().find('h4').hide("slide", { direction: "down", easing: 'easeOutBounce' });
        }

    );

    $("#timer").kkcountdown({
        dayText: ':',
        daysText: ':',
        hoursText: ':',
        minutesText: ':',
        secondsText: '',
        displayZeroDays: false,
        oneDayClass: 'one-day'
    });
    equalHeight($("ul#bandmembers li"));

    function equalHeight(group) {
        var tallest = 0;
        group.each(function () {
            var thisHeight = $(this).height();
            if (thisHeight > tallest) {
                tallest = thisHeight;
            }
        });
        group.height(tallest);
    }
});