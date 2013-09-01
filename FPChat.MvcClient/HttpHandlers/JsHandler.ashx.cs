using System;
using System.Web;
using FPChat.Domain.Utilities;

namespace FPChat.MvcClient.HttpHandlers
{
    /// <summary>
    /// Http handler returns a specific js file.
    /// This handler is used, to ensure that the file isn't cached by browser.
    /// </summary>
    public class JsHandler : BaseHttpHandler, IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string path = context.Request.Params["file"]; 

            string result = String.Empty;
            if (FileUtils.GetFileEntension(path) == FileUtils.FileExtension.JS)
            {
                result = FileUtils.GetFileContent(path);
            }

            SetHeader(context, "text/javascript", 0, false, null);
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}