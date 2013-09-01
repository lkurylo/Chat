using System;
using System.Text;
using System.Web;

namespace FPChat.MvcClient.HttpHandlers
{
    /// <summary>
    /// This is a base class for all http handlers.
    /// </summary>
    public abstract class BaseHttpHandler
    {
        /// <summary>
        /// Sets header for specific files.
        /// </summary>
        /// <param name="context">The HttpContext for whitch the header must be set.</param>
        /// <param name="mimeType">The mime type to set.</param>
        /// <param name="expiresInDays">Indicates after how many days the header must expires.</param>
        /// <param name="varyByParams">Indicates that vary header by parameters or not.</param>
        /// <param name="parameters">The array of parameters. To use it, the varyByParams must be set to true.</param>
        public void SetHeader(HttpContext context, string mimeType, double expiresInDays,
            bool varyByParams, params string[] parameters)
        {
            if (varyByParams)
            {
                foreach (string p in parameters)
                    context.Response.Cache.VaryByParams[p] = true;
            }

            context.Response.ContentType = mimeType;
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Cache.SetValidUntilExpires(false);
            context.Response.Cache.SetLastModifiedFromFileDependencies();
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(expiresInDays));
        }
    }
}