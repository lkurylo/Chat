using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FPChat.Domain.Utilities;

namespace FPChat.MvcClient.HttpHandlers
{
    /// <summary>
    /// Responsible for returning to the browser the specific css file.
    /// Prevents from caching the css files by the browser what 
    /// helps in testing the application after every one changes.
    /// </summary>
    public class CssHandler : BaseHttpHandler, IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string path = context.Request.Params["file"];

            string result = String.Empty;
            if (FileUtils.GetFileEntension(path) == FileUtils.FileExtension.CSS)
            {
                result = FileUtils.GetFileContent(path);
            }

            SetHeader(context, "text/css", 0, false, null);
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