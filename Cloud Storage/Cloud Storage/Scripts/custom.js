(function ($, window) {
    
    $.fn.contextMenu = function (settings) {
        console.log('context')
        return this.each(function () {

            // Open context menu
            $(this).on("contextmenu", function (e) {
                // return native menu if pressing control
                if (e.ctrlKey) return;

                //open menu
                //var $parent = $(e.target).parents('#profile');

                //if (!$parent.hasClass('btn')) {
                //    $parent = $(e.target);
                //}

                var $targetChild = $(e.target)//.children('a');//.eq(0).children('h6').text();
                console.log($targetChild)

                var $menu = $(settings.menuSelector)
                    .data("invokedOn", $.trim($targetChild))
                    .show()
                    .css({
                        position: "absolute",
                        left: getMenuPosition(e.clientX, 'width', 'scrollLeft'),
                        top: getMenuPosition(e.clientY, 'height', 'scrollTop')
                    })
                    .off('click')
                    .on('click', 'a', function (e) {
                        $menu.hide();

                        var $invokedOn = $menu.data("invokedOn");
                        var $selectedMenu = $(e.target);

                        settings.menuSelected.call(this, $invokedOn, $selectedMenu);
                    });

                return false;
            });

            //make sure menu closes on any click
            $('body').click(function () {
                $(settings.menuSelector).hide();
            });
        });

        function getMenuPosition(mouse, direction, scrollDir) {
            var win = $(window)[direction](),
                scroll = $(window)[scrollDir](),
                menu = $(settings.menuSelector)[direction](),
                position = mouse + scroll;

            // opening menu would pass the side of the page
            if (mouse + menu > win && menu < mouse)
                position -= menu;

            return position;
        }

    };
})(jQuery, window);

$("#Grid").contextMenu({
    menuSelector: "#contextMenu",
    menuSelected: function (invokedOn, selectedMenu) {
        console.log(invokedOn, selectedMenu)
        //$.get('File/' + selectedMenu.text(), { name: invokedOn });
    }
});