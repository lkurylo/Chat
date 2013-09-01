using System;
using Ninject;
using Ninject.Parameters;

namespace FPChat
{
    /// <summary>
    /// Concrete implementaton of the IServiceLocator.
    /// </summary>
    public class ServiceLocator : IServiceLocator
    {
        /// <summary>
        /// Ninject IKernel factory.
        /// </summary>
        public IKernel Kernel { private set; get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="kernel">Ninject IKernel factory.</param>
        public ServiceLocator(IKernel kernel)
        {
            Kernel = kernel;
        }

        /// <summary>
        /// Returns specific service.
        /// </summary>
        /// <typeparam name="T">Service to resolve.</typeparam>
        /// <returns>Resolved service.</returns>
        public T GetService<T>()
        {
            return Kernel.Get<T>();
        }

        /// <summary>
        /// Returns specific service.
        /// </summary>
        /// <param name="service">Service to resolve.</param>
        /// <returns>Resolved service.</returns>
        public object GetService(Type service)
        {
            return Kernel.Get(service);
        }

        /// <summary>
        /// Returns specific service which contains key metadata.
        /// </summary>
        /// <typeparam name="T">Service to resolve.</typeparam>
        /// <param name="key">The metadata key .</param>
        /// <returns>Resolved service.</returns>
        public T GetService<T>(Enum key)
        {
            string arg = key.ToString();
            return Kernel.Get<T>(x => x.Has(arg));
        }

        /// <summary>
        /// Returns specific service which contains key metadata and have exactly one argument in default constructor.
        /// </summary>
        /// <typeparam name="T">Service to resolve.</typeparam>
        /// <param name="key">The metadata key .</param>
        /// <param name="propertyName">Constructor argument name.</param>
        /// <param name="value">Constructor argument value.</param>
        /// <returns>Resolved service.</returns>
        public T GetService<T>(Enum key, string propertyName, object value)
        {
            string arg = key.ToString();
            return Kernel.Get<T>(x => x.Has(arg), new ConstructorArgument(propertyName, value));
        }
    }
}
