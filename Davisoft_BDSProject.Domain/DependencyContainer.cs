using System;
using Ninject;

namespace Davisoft_BDSProject.Domain
{
    public static class DependencyContainer
    {
        private static readonly IKernel _kernel;

        static DependencyContainer()
        {
            _kernel = new StandardKernel();
            RegisterServices(_kernel);
        }

        public static IKernel Current
        {
            get { return _kernel; }
        }

        private static void RegisterServices(IKernel kernel)
        {
        }
    }
}
