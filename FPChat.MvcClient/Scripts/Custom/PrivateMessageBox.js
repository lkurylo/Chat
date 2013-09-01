/// <reference path="../Libraries/jquery-1.6.2-vsdoc.js" />

var FPChat = FPChat || {};
(function ()
{
    FPChat.PrivateMessageBox = (function ()
    {
        function _init()
        {
            $('#usersList li a').live('click', function (elem)
            {
                var selectedUser = $(elem.currentTarget).html();

                $('#pmMessageBox').load("/Messages/PrivateMessageBox", function ()
                {
                    $(this).dialog({
                        "modal": true,
                        "width": 384, //"395px", 
                        "height": 276, //"266px",
                        "resizable": false,
                        "title": "Private chat with " + selectedUser,
                        "buttons": {
                            "Close": function ()
                            {
                                $(this).dialog('destroy');
                            }
                        }
                    });
                    //return false;
                    $('#newPMMessage').focus();

                    $('#btnSendPM').prop('disabled', true);

                    $('#newPMMessage').keyup(function ()
                    {
                        if ($('#newPMMessage').val() === "")
                        {
                            $('#btnSendPM').prop('disabled', true);
                        } else
                        {
                            $('#btnSendPM').prop('disabled', false);
                        }
                    });

                    //apply the hacks for browsers
                    FPChat.Hacks.applyHacksForPMBox();
                });
            });

            $('#btnSendPM').live('click', function ()
            {
                var message = $('#newPMMessage').val();
                $.ajax({
                    url: "/Messages/AddNewPM",
                    data: "message=" + message,
                    type: "POST",
                    success: function (result)
                    {
                        $('#newPMMessage').val("");
                        $('#btnSendPM').prop('disabled', true);
                        $('#newPMMessage').focus();
                    }
                });
            });

            $('#btnClearPMBox').live('click', function ()
            {
                $('#taPMMessages').html("");
                $('#newPMMessage').focus();
            });
        }

        _init();

    })();
})();