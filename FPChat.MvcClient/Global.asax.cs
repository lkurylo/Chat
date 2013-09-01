using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FPChat.MvcClient.Controllers.Factory;
using FPChat.MvcClient.Controllers;

namespace FPChat.MvcClient
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapRoute(
            //                           "LogOut", // Route name
            //                           "Users/LogOut", // URL with parameters
            //                           new { controller = "Users", action = "LogOut" }
            //                       );
            routes.MapRoute(
                      "Default", // Route name
                      "{controller}/{action}/{id}", // URL with parameters
                      new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                  );
            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}/{action}", // URL with parameters
            //    new { controller = "Home", action = "Index" } // Parameter defaults
            //);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //inject new default controller factory 
            //ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }

        protected void Session_End(Object sender, EventArgs e)
        {
            //var r = HttpContext.Current.User.Identity.IsAuthenticated;
            //var rr = HttpContext.Current.User.Identity.Name;

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Users");
            routeData.Values.Add("action", "LogOut");

            IController errorController = Bootstrapper.ServiceLocator.GetService<UsersController>();
            errorController.Execute(new RequestContext(
                 new HttpContextWrapper(Context), routeData));
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //when any error occurs on the server, user
            //is redirected to the error controller without any
            //error/trace details
            //TODO: logging
            //Response.Clear();

            //RouteData routeData = new RouteData();
            //routeData.Values.Add("controller", "Error");
            //routeData.Values.Add("action", "Index");

            //Server.ClearError();

            ////error controller is created manually, but
            ////it is a good point to move it to the DI/IoC 
            //IController errorController = new ErrorController();
            //errorController.Execute(new RequestContext(
                 //new HttpContextWrapper(Context), routeData));
        }
    }
}