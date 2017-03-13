jQuery(document).ready(function () {
    //cache DOM elements
    var mainContent = $('.cd-main-content'),
		header = $('.cd-main-header'),
		sidebar = $('.cd-side-nav'),
		sidebarTrigger = $('.cd-nav-trigger'),
		topNavigation = $('.cd-top-nav'),
		searchForm = $('.cd-search'),
		accountInfo = $('.account'),
		languageInfo = $('.language');

    //on resize, move search and top nav position according to window width
    var resizing = false;
    moveNavigation();
    $(window).on('resize', function () {
        if (!resizing) {
            (!window.requestAnimationFrame) ? setTimeout(moveNavigation, 300) : window.requestAnimationFrame(moveNavigation);
            resizing = true;
        }
    });

    //on window scrolling - fix sidebar nav
    var scrolling = false;
    checkScrollbarPosition();
    $(window).on('scroll', function () {
        if (!scrolling) {
            (!window.requestAnimationFrame) ? setTimeout(checkScrollbarPosition, 300) : window.requestAnimationFrame(checkScrollbarPosition);
            scrolling = true;
        }
    });

    //mobile only - open sidebar when user clicks the hamburger menu
    sidebarTrigger.on('click', function (event) {
        event.preventDefault();
        $([sidebar, sidebarTrigger]).toggleClass('nav-is-visible');
    });

    //click on item and show submenu
    $('.has-children > a').on('click', function (event) {
        var mq = checkMQ(),
			selectedItem = $(this);
        if (mq == 'mobile' || mq == 'tablet') {
            event.preventDefault();
            if (selectedItem.parent('li').hasClass('selected')) {
                selectedItem.parent('li').removeClass('selected');
            } else {
                sidebar.find('.has-children.selected').removeClass('selected');
                accountInfo.removeClass('selected');
                languageInfo.removeClass('selected');
                selectedItem.parent('li').addClass('selected');
            }
        }
    });

    //click on language and show submenu - desktop version only
    languageInfo.children('a').on('click', function (event) {
        accountInfo.removeClass('selected');
        var mq = checkMQ(),
			selectedItem = $(this);
        if (mq == 'desktop') {
            event.preventDefault();
            languageInfo.toggleClass('selected');
            sidebar.find('.has-children.selected').removeClass('selected');
        }
    });

    $(document).on('click', function (event) {
        if (!$(event.target).is('.has-children a')) {
            sidebar.find('.has-children.selected').removeClass('selected');
            languageInfo.removeClass('selected');
        }
    });

    //click on account and show submenu - desktop version only
    accountInfo.children('a').on('click', function (event) {
        languageInfo.removeClass('selected');
        var mq = checkMQ(),
			selectedItem = $(this);
        if (mq == 'desktop') {
            event.preventDefault();
            accountInfo.toggleClass('selected');
            sidebar.find('.has-children.selected').removeClass('selected');
        }
    });

    $(document).on('click', function (event) {
        if (!$(event.target).is('.has-children a')) {
            sidebar.find('.has-children.selected').removeClass('selected');
            accountInfo.removeClass('selected');
        }
    });

    //on desktop - differentiate between a user trying to hover over a dropdown item vs trying to navigate into a submenu's contents
    sidebar.children('ul').menuAim({
        activate: function (row) {
            $(row).addClass('hover');
        },
        deactivate: function (row) {
            $(row).removeClass('hover');
        },
        exitMenu: function () {
            sidebar.find('.hover').removeClass('hover');
            return true;
        },
        submenuSelector: ".has-children",
    });

    function checkMQ() {
        //check if mobile or desktop device
        if (document.querySelector('.cd-main-content') != null) {
            return window.getComputedStyle(document.querySelector('.cd-main-content'), '::before').getPropertyValue('content').replace(/'/g, "").replace(/"/g, "");
        }
        return null;
    }

    function moveNavigation() {
        var mq = checkMQ();

        if (mq == 'mobile' && topNavigation.parents('.cd-side-nav').length == 0) {
            detachElements();
            topNavigation.appendTo(sidebar);
            searchForm.removeClass('is-hidden').prependTo(sidebar);
        } else if ((mq == 'tablet' || mq == 'desktop') && topNavigation.parents('.cd-side-nav').length > 0) {
            detachElements();
            searchForm.insertAfter(header.find('.cd-logo'));
            topNavigation.appendTo(header.find('.cd-nav'));
        }
        checkSelected(mq);
        resizing = false;
    }

    function detachElements() {
        topNavigation.detach();
        searchForm.detach();
    }

    function checkSelected(mq) {
        //on desktop, remove selected class from items selected on mobile/tablet version
        if (mq == 'desktop') $('.has-children.selected').removeClass('selected');
    }

    function checkScrollbarPosition() {
        var mq = checkMQ();

        if (mq != 'mobile') {
            var sidebarHeight = sidebar.outerHeight(),
				windowHeight = $(window).height(),
				mainContentHeight = mainContent.outerHeight(),
				scrollTop = $(window).scrollTop();

            //			( ( scrollTop + windowHeight > sidebarHeight ) && ( mainContentHeight - sidebarHeight != 0 ) ) ? sidebar.addClass('is-fixed').css('bottom', 0) : sidebar.removeClass('is-fixed').attr('style', '');
        }
        scrolling = false;
    }

    //$(".select2").select2({ width: '100%' });

    $('.inputfile').each(function () {
        var $input = $(this),
			$label = $input.next('label'),
			labelVal = $label.html();

        $input.on('change', function (e) {
            var fileName = '';

            if (this.files && this.files.length > 1)
                fileName = (this.getAttribute('data-multiple-caption') || '').replace('{count}', this.files.length);
            else if (e.target.value)
                fileName = e.target.value.split('\\').pop();

            if (fileName)
                $label.find('span').html(fileName);
            else
                $label.html(labelVal);
        });

        // Firefox bug fix
        $input
		.on('focus', function () { $input.addClass('has-focus'); })
		.on('blur', function () { $input.removeClass('has-focus'); });
    });

    //$('.i-checks label').each(function () {
    //    var $label = $(this);
    //    $label.on('click', function (e) {
    //        e.preventDefault();
    //        var $input = $(this).find("input[type='checkbox']");
    //        if ($input.attr("readonly") != "readonly" && $input.attr("readonly") != "True" && $input.attr("disabled") != "disabled" && $input.attr("readonly") != "True") {
    //            if ($input.val() == "true") {
    //                $input.attr("value", "false");
    //            } else {
    //                if ($input.val() == "false") {
    //                    $input.attr("value", "true");
    //                }
    //            }
    //        }
    //    });
    //});

    //$(document).on('click', '.i-checks label', function (e) {
    //    e.preventDefault();
    //    var $input = $(this).find("input[type='checkbox']");
    //    if ($input.attr("readonly") != "readonly" && $input.attr("readonly") != "True" && $input.attr("disabled") != "disabled" && $input.attr("readonly") != "True") {
    //        if ($input.val() == "true") {
    //            $input.attr("value", "false");
    //        } else {
    //            if ($input.val() == "false") {
    //                $input.attr("value", "true");
    //            }
    //        }
    //    }
    //});

    ChangeUIListCheckBox();
    //$(":not(.icheck) > label.checkbox").each(function () {
    //    var container = $(this).parent();
    //    container.addClass("i-checks");
    //    var input = $(this).find("input[type=checkbox]");
    //    input.after("<i></i>");
    //});

    $("body").on('change', 'input[type="checkbox"][readonly="readonly"]', function (e) {
        e.preventDefault();
        //if ($(this).attr("readonly") == "readonly" || $(this).attr("readonly") == "True" || $(this).attr("disabled") == "disabled" || $(this).attr("readonly") == "True") {
        $(this)[0].checked = !$(this)[0].checked;
        //}
    });
    $("body").on('change', 'input[type="checkbox"][disabled="disabled"]', function (e) {
        e.preventDefault();
        //if ($(this).attr("readonly") == "readonly" || $(this).attr("readonly") == "True" || $(this).attr("disabled") == "disabled" || $(this).attr("readonly") == "True") {
        $(this)[0].checked = !$(this)[0].checked;
        //}
    });
});


function ChangeUIListCheckBox() {
    $(":not(.i-checks) > label.checkbox").each(function () {
        var container = $(this).parent();
        container.addClass("i-checks");
        var input = $(this).find("input[type=checkbox]");
        input.after("<i></i>");
    });
}