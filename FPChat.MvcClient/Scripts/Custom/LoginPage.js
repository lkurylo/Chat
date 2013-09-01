/// <reference path="../Libraries/jquery-1.6.2-vsdoc.js" />

var FPChat = FPChat || {};
(function ()
{
    //contains logic for login page
    FPChat.LoginPage = (function ()
    {
        function _init()
        {
            //set the focus to the login textbox after the page is loaded
            $('#txtLogin').focus();

            //at the beginning, when the login textbox is empty, disable the submit button
            $('#selectLogin').prop('disabled', true);

            //check if the login textbox is filled with text
            //if not, disable the submit button
            $('#txtLogin').keyup(function ()
            {
                if ($('#txtLogin').val() === "")
                {
                    $('#selectLogin').prop('disabled', true);
                } else
                {
                    $('#selectLogin').prop('disabled', false);
                }
            });

            //send request to the server to check, if the specified login is unique or not
            $('#selectLogin').bind('click', function ()
            {
                $.ajax({
                    url: "/Users/CheckIfSpecifiedLoginIsUsed",
                    type: "POST",
                    data: "login=" + $('#txtLogin').val(),
                    success: function (result)
                    {
                        //if we get the true here, that means the login is already in use
                        if (result === true)
                        {
                            $('#loginExists').text("Login already exists. Please insert some other.");
                            $('#selectLogin').prop('disabled', true);
                            $('#txtLogin').text("");
                        } else
                        {
                            //login is free, so save it to the db and redirect to rooms list
                            //save to db
                            $.ajax({
                                url: "/Users/SaveLogin",
                                type: "POST",
                                data: "login=" + $('#txtLogin').val(),
                                success: function ()
                                {
                                    //redirect to rooms
                                    window.location.replace("/Home/Index");
                                }
                            });
                        }
                    }
                });
            });
        }

        _init();

    })();
})();