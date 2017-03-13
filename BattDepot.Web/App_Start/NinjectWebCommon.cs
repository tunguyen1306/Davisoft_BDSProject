using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using CsQuery.Engine.PseudoClassSelectors;
using FluentValidation;
using FluentValidation.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Davisoft_BDSProject.Web.Validation;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc.FluentValidation;
using Davisoft_BDSProject.Domain;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Concrete;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Helpers.Encryption;
using Davisoft_BDSProject.Web;
using Davisoft_BDSProject.Web.Infrastructure;
using Davisoft_BDSProject.Web.Models;
using Portable.Licensing;
using WebActivator;

[assembly: WebActivator.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace Davisoft_BDSProject.Web
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        ///     Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        ///     Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        ///     Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = DependencyContainer.Current ?? new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            var ninjectValidatorFactory = new NinjectValidatorFactory(kernel);
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(ninjectValidatorFactory));
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            RegisterServices(kernel);
            return kernel;
        }


        /// <summary>
        ///     Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Database context
            kernel.Bind<DbContext>().To<Davisoft_BDSProjectDb>().InRequestScope();
            // Logging
            kernel.Bind<IAuditTracker>().To<EFAuditTracker>()
                // initialize DbContext in another block so it will not persist temporary changes in business flow
                  .WithConstructorArgument("db", c => c.Kernel.BeginBlock().Get<DbContext>());

            // Repositories
            kernel.Bind<IAuthenticationService, IMembershipService>().To<InMemLoginTracker>();
            kernel.Bind<IAuthorizationService, IRoleService>().To<EFRoleBasedAuthorizer>();
            kernel.Bind<ILoginTracker>().To<InMemLoginTracker>()
                  .WithConstructorArgument("concurrentMax", Convert.ToInt32(ConfigurationManager.AppSettings["ConcurrentLoginMax"]));
            //_kernel.Bind<ILoginTracker>().To<InMemLoginTracker>();
            kernel.Bind<EFMenuRepository>().ToSelf();
            kernel.Bind<IMenuRepository>().To<MemoryMenuRepository>().InSingletonScope();
            kernel.Bind<IUnitRepository>().To<EFUnitRepository>();
            kernel.Bind<ISettingRepository>().To<EFSettingRepository>();


            kernel.Bind<IValidator<User>>().To<UserValidator>();
            kernel.Bind<IValidator<Menu>>().To<MenuValidator>();
            kernel.Bind<IValidator<Currency>>().To<CurrencyValidator>();
            kernel.Bind<IValidator<Language>>().To<LanguageValidator>();
            // cache configurations.
            kernel.Bind<ICacheStorageLocation>().To<RequestCacheSolution>().Named("InRequest");
            kernel.Bind<ICacheStorageLocation>().To<SessionCacheSolution>().Named("InSession");

            //

            DependencyResolver.SetResolver(new NinjectResolver(kernel));
        }
    }
}
