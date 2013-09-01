using FPChat.Domain.Services.Concrete;
using FPChat.Domain.Services.Interfaces;
using Ninject;
using Ninject.Modules;
using FPChat.MvcClient.Controllers;

namespace FPChat
{
    /// <summary>
    /// Holds a ninject config for entire application.
    /// </summary>
    public class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            #region service locator

            Bind<IServiceLocator>()
               .To<ServiceLocator>()
               .InSingletonScope();

            Bind<IKernel>()
                .ToMethod(x => x.Kernel)
                .InSingletonScope();

            #endregion

            #region services

            Bind<IUsersService>()
                .To<UsersService>()
                .InRequestScope();       

            Bind<IMessagesService>()
                .To<MessagesService>()
                .InRequestScope();

            #endregion

            #region controllers

            //this binding is for Session_End in Global.asax only
            //for the controllers is created custom controller factory
            //Bind<UsersController>()
            //    .ToSelf()
            //    .InRequestScope();

            //Bind<ErrorController>()
            //    .ToSelf()
            //    .InRequestScope();

            //Bind<MessagesController>()
            //    .ToSelf()
            //    .InRequestScope();

            //Bind<HomeController>()
            //    .ToSelf()
            //    .InRequestScope();

            #endregion
        }
    }
}
