/// <reference path="../Libraries/jquery-1.6.2-vsdoc.js" />

var FPChat = FPChat || {};
(function ()
{
    //class contains hacks for all browsers
    FPChat.Hacks = (function ()
    {
        function _init()
        {
            if ($.browser.opera)
            {
                $('#taMessages').prop('rows', 20);
                $('#clearMessages').width(170);
                $('fieldset').css('border', '0px');
                $('fieldset').css('box-shadow', '0 0 2px rgba(0, 0, 0, 0.8)');
                $('fieldset').css('border-radius', '15px');
            }

            if ($.browser.msie)
            {
                $('#taMessages').prop('rows', 27);
                $('#taPMMessages').prop('rows', 7);
                //                $('fieldset').css('border', '5px');
                $('fieldset').css('box-shadow', '0 0 2px rgba(0, 0, 0, 0.8)');
                $('fieldset').css('border-radius', '15px');
                $('#taMessages').css('overflow', 'auto');
            }

            if ($.browser.webkit)
            {
                $('#btnLogOut').hide();
            }

            //don't know why but the users logging out link work ok only in chrome
            //this is hack for other browsers..
            if (!$.browser.webkit)
            {
//                $('#btnLogOut').hide();
//                $('#loggedInfo a').bind('click', function (e)
//                {
//                    e.preventDefault();
//                    $('#btnLogOut').click();
//                });
            }
        }

        _init();

        return {
            //public method to applying hacks for PM box
            applyHacksForPMBox: function ()
            {
                if ($.browser.msie)
                {
                    $('#taPMMessages').prop('rows', 7);
                }

                if ($.browser.webkit)
                {
                    $('#taPMMessages').prop('rows', 6);
                }

                if ($.browser.opera)
                {
                    $('#pmMessageBox').dialog("option", "width", 405);
                    $('#taPMMessages').width(355);
                    $('#btnSendPM').css("margin-left", "13px");
                    $('#btnSendPM').css("margin-right", "12px");
                    $('#taPMMessages').prop('rows', 3);
                }
            }
        };
    })();
})();