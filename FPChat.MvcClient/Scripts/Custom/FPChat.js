/// <reference path="../Libraries/jquery-1.6.2-vsdoc.js" />

var FPChat = FPChat || {};
(function ()
{
    //contains logic for main page
    FPChat.MainWindow = (function ()
    {
        //private initialize method (main form constructor)
        function _init()
        {
            _setNewMessageTextBoxWidth();

            var stopResize = false;
            $(window).resize(function ()
            {
                //the newMessage width must be reduced
                //because this element breaking the layout during resizing
                $('#newMessage').width(100);

                if (stopResize !== false)
                {
                    clearTimeout(stopResize);
                }
                
                stopResize = setTimeout(function ()
                {
                    _setNewMessageTextBoxWidth();
                }, 10);
            });

            $('#newMessage').focus();

            $('#sendMessage').prop('disabled', true);

            $('#newMessage').keyup(function ()
            {
                if ($('#newMessage').val() === "")
                {
                    $('#sendMessage').prop('disabled', true);
                } else
                {
                    $('#sendMessage').prop('disabled', false);
                }
            });

            //clear the chat window
            $('#clearMessages').bind('click', function ()
            {
                $('#taMessages').html("");
                $('#newMessage').focus();
            });
        }

        //private method responsible for setting the correct width
        //of newMessage textbox element
        function _setNewMessageTextBoxWidth()
        {
            //set the newMessage input text element a proper width, the same as the parent
            var parentWidth = $('#newMessage').parent().width();
            $('#newMessage').width(parentWidth);
        }

        _init();

        return {
            unloadPageHandler: function ()
            {
                //            if(window.location.pathname==="/Home/Index" || window.location.pathname==="/"){
                //                $.ajax({
                //                    url: '/Users/LogOut'
                //                });
                //            }
            }
        };
    })();
})();