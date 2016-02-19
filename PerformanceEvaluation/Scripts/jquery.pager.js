
/*
* @autor:WYQ 王永清
* @修改自 pager
* Example at: http://www.2fool.cn/mywork/Pager/PagerDemo.html
*show{ per每页显示多少？ index {n：首页 m:first(prev,next last)}→首页名字 0 不显示， 上一页下一页 pre next 显示名字 last 名字 0为不显示
*}
*/
(function($) {

    $.fn.pager = function(options) {

        var opts = $.extend({}, $.fn.pager.defaults, options);

        return this.each(function() {

        // empty out the destination element and then render out the pager with the supplied options
            $(this).empty().append(renderpager(parseInt(options.pagenumber), parseInt(options.pagecount), options.buttonClickCallback, options.show));
            
            // specify correct cursor activity
            $('.pages li').mouseover(function() { document.body.style.cursor = "pointer"; }).mouseout(function() { document.body.style.cursor = "auto"; });
        });
    };

    // render and return the pager with the supplied options
    function renderpager(pagenumber, pagecount, buttonClickCallback, show) {

        // setup $pager to hold render
        var $pager = $('<ul class="pages"></ul>');

        // add in the previous and next buttons
		typeof show.index!="undefined"&&show.index.n?$pager.append(renderButton(show.index, pagenumber, pagecount, buttonClickCallback)):null;
        $pager.append(renderButton(show.pre, pagenumber, pagecount, buttonClickCallback));

        // pager currently only handles 10 viewable pages ( could be easily parameterized, maybe in next version ) so handle edge cases
        var startPoint = 1;
        var endPoint = parseInt(show.per);
		var pd=Math.floor(parseInt(show.per)/2);		

        if (pagenumber > pd) {
            startPoint = parseInt(show.per)%2?pagenumber-pd:pagenumber-pd+1;//pagenumber - pd<0?1:(pagenumber-pd>pagecount-parseInt(show.per)?pagecount-parseInt(show.per)+1:pagenumber-pd);
			
            endPoint = pagenumber +pd;//>pagecount?pagecount:(pagenumber+pd);
        }

        if (endPoint > pagecount) {
            startPoint = pagecount - parseInt(show.per)+1;
            endPoint = pagecount;
        }

        if (startPoint < 1) {
            startPoint = 1;
        }

        // loop thru visible pages and render buttons
        for (var page = startPoint; page <= endPoint; page++) {

            var currentButton = $('<li class="page-number">' + (page) + '</li>');

            page == pagenumber ? currentButton.addClass('pgCurrent') : currentButton.click(function() { buttonClickCallback(this.firstChild.data); });
            currentButton.appendTo($pager);
        }

        // render in the next and last buttons before returning the whole rendered control back.
        $pager.append(renderButton(show.next, pagenumber, pagecount, buttonClickCallback));
		typeof show.last!="undefined"&&show.last.n?$pager.append(renderButton(show.last, pagenumber, pagecount, buttonClickCallback)):null;

        return $pager;
    }

    // renders and returns a 'specialized' button, ie 'next', 'previous' etc. rather than a page number button
    function renderButton(buttonLabel, pagenumber, pagecount, buttonClickCallback) {

        var $Button = $('<li class="pgNext">' + buttonLabel.n + '</li>');

        var destPage = 1;

        // work out destination page for required button type
        switch (buttonLabel.m) {
            case "first":
                destPage = 1;
                break;
            case "prev":
                destPage = pagenumber - 1;
                break;
            case "next":
                destPage = pagenumber + 1;
                break;
            case "last":
                destPage = pagecount;
                break;
        }

        // disable and 'grey' out buttons if not needed.
        if (buttonLabel.m == "first" || buttonLabel.m == "prev") {
            pagenumber <= 1 ? $Button.addClass('pgEmpty') : $Button.click(function() { buttonClickCallback(destPage); });
        }
        else {
            pagenumber >= pagecount ? $Button.addClass('pgEmpty') : $Button.click(function() { buttonClickCallback(destPage); });
        }

        return $Button;
    }

    // pager defaults. hardly worth bothering with in this case but used as placeholder for expansion in the next version
    $.fn.pager.defaults = {
        pagenumber: 1,
        pagecount: 1
    };

})(jQuery);





