using System.Web.Mvc;

namespace FPChat.MvcClient.Controllers
{
    /// <summary>
    /// Handles all application errors through Global.asax.
    /// </summary>
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
