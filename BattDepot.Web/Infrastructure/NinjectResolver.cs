using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Syntax;

namespace Davisoft_BDSProject.Web.Infrastructure
{
    public class NinjectResolver : IDependencyResolver
    {
        private readonly IResolutionRoot _kernel;

        public NinjectResolver(IResolutionRoot kernel)
        {
            _kernel = kernel;
        }

        #region IDependencyResolver Members

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        #endregion
    }
}
