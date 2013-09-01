using Ninject;

namespace FPChat
{
    /// <summary>
    /// Holds access to the IoC container.
    /// </summary>
    public static class Bootstrapper
    {
        static Bootstrapper()
        {
            if (ServiceLocator == null)
            {
                var kernel = new StandardKernel(new NinjectConfig());
                Bootstrapper.ServiceLocator = kernel.Get<IServiceLocator>();
            }
        }

        /// <summary>
        /// Wrapper for the Ninject mechanism to access the IoC container.
        /// </summary>
        public static IServiceLocator ServiceLocator { get; set; }
    }
}
