/// <reference path="../Libraries/jquery-1.6.2-vsdoc.js" />

var FPChat = FPChat || {};
(function ()
{
    //contains logic for users and messages
    FPChat.MessagesAndUsers = (function ()
    {
        //gets users list from the server in loop 
        //and replaces the old list values with the new one
        //this method should be refactored, because is high inefficient
        //when there will be more users
        function _updateUserList() //_checkIfUsersListWasChanged()
        {
            var interval = window.setInterval(function ()
            {
                $.ajax({
                    url: "/Users/GetLoggedUsers",
                    type: "POST",
                    success: function (result)
                    {
                        //this is for sure not the best real-world solution but 
                        //should work good in this simple project
                        $('#usersList').children().remove();
                        $('#usersList').html(result);
                    }
                });
                // if (false) window.clearInterval(interval);
            }, FPChat.options.refreshUserListTimeout);
        }

        //holds id of the last received message
        var lastMessageId = '';

        //gets new messages from the server
        function _getNewestMessages()
        {
            var interval = window.setInterval(function ()
            {
                if (lastMessageId == '')
                {
                    $.ajax({
                        url: "/Messages/GetFirstFromNewestMessages",
                        type: "POST",
                        success: function (result)
                        {
                            if (result === "empty")
                            {
                                //there is no new message on the server
                                //so here is nothing more to do 
                            }
                            else
                            {
                                //if new message is available, the 
                                //appendNewLine method is executed from the server
                                //so here is nothing more to do 
                                // $.post("","messageId="+
                            }
                        }
                    });
                } else
                {
                    $.ajax({
                        url: "/Messages/GetNewestMessages",
                        type: "POST",
                        data: "lastReceivedMessage=" + lastMessageId,
                        success: function (result)
                        {
                            if (result === "empty")
                            {
                                //there are no new messages on the server
                                //so here is nothing more to do 
                            }
                            else
                            {
                                //messages are returned from the server
                                //but this time we must manually append the new messages
                                //to the textarea on the main page                             
                                for (var i = 0; i < result.length; i++)
                                {
                                    var id = result[i].Id;
                                    _markerTheMessageAsLast(id);
                                    var parsedTime = new Date(parseInt(result[i].CreatedDate.replace("/Date(",
                                    "").replace(")/", ""), 10)).toLocaleTimeString();
                                    var author = result[i].Author.Login;
                                    var message = result[i].Content;

                                    _appendNewLine(author + "(" + parsedTime + "): " + message);
                                }
                            }
                        }
                    });
                }

                // if (false) window.clearInterval(interval);
            }, FPChat.options.refreshMessagesBoxTimeout);
        }

        //private implementation of method responsible for marking 
        //specific messages and send to server info that message 
        //was received correctly from the server
        function _markerTheMessageAsLast(messageId)
        {
            lastMessageId = messageId;
            $.post("/Messages/MarkMessageAsReceived", "messageId=" + messageId);
        }

        //private contructor implementation
        function _init()
        {
            $('#sendMessage').bind('click', function ()
            {
                var message = $('#newMessage').val();
                $.ajax({
                    url: "/Messages/AddNew",
                    data: "message=" + message,
                    type: "POST",
                    success: function (result)
                    {
                        $('#newMessage').val("");
                        $('#sendMessage').prop('disabled', true);
                        $('#newMessage').focus();
                    }
                });
            });

//            _updateUserList();
//            _getNewestMessages();
        }

        //private method responsible for appending a piece of text to 
        //the main chat screen
        function _appendNewLine(newMessage)
        {
            if (typeof newMessage === 'string')
            {
                $('#taMessages').append(newMessage + _newLine());
            }
        }

        //private helper method returning
        //correct new line depending on the browser 
        function _newLine()
        {
            if ($.browser.msie)
            {
                return '\n\r';
            } else
            {
                return '\n';
            }
        }

        return {
            //public method responsible for appending a new message to
            //main chat textarea
            appendNewLine: function (newMessage)
            {
                _appendNewLine(newMessage);
            },

            //public method responsible for marking the specific message 
            //as last received from the server
            markerTheMessageAsLast: function (messageId)
            {
                _markerTheMessageAsLast(messageId);
            },

            //public constructor
            //it's public, because otherwise object is constructed 
            //even on the page to log in users
            //now object is constructed on the main page after logging
            init: function ()
            {
                _init();
            }
        };
    })();
})();