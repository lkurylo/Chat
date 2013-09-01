using System.Web.Mvc;

namespace FPChat.MvcClient.Controllers.Factory
{
    /// <summary>
    /// Custom implementation of controller factory.
    /// Created to use the DI/IoC to creating the controllers.
    /// </summary>
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, 
            System.Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)Bootstrapper.ServiceLocator.GetService(controllerType);
        }
    }
}