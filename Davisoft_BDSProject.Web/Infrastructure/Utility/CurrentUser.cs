using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Enum;

namespace Davisoft_BDSProject.Web.Infrastructure.Utility
{
    public class CurrentUser
    {
        #region Repository

        private static IAuthenticationService AuthenticationService
        {
            get { return DependencyResolver.Current.GetService<IAuthenticationService>(); }
        }

        private static IAuthorizationService AuthorizationService
        {
            get { return DependencyResolver.Current.GetService<IAuthorizationService>(); }
        }

        private static IUnitRepository UnitRepo
        {
            get { return DependencyHelper.GetService<IUnitRepository>(); }
        }

        #endregion

        public static bool IsAuthenticated
        {
            get
            {
                return Identity != null;
            }
        }

        public static User Identity
        {
            get { return LoginPersister.RetrieveUser(); }
        }

        public static bool HasRole(string role)
        {
            if (Identity == null) return false;

            return Identity.Roles.Any(Role.HasName(role));
        }

        public static bool HasRoleLevel(RoleLevel roleLevel)
        {
            if (Identity == null) return false;

            return Identity.Roles.Any(m => m.RoleLevel == roleLevel);
        }

        public static bool HasPermission(string controller, string action)
        {
            if (Identity == null) return false;
            return AuthorizationService.CheckAccess(Identity.ID, controller + "." + action);
        }

        public static bool HasSettingPermission(string module, string name)
        {
            if (Identity == null) return false;
            return AuthorizationService.CheckAccess(Identity.ID, module + "." + name, PermissionType.SettingPermission);
        }

        public static bool Can<TController>(Expression<Func<TController, object>> action) where TController : IController
        {
            string actionName = action.GetActionName();
            if (string.IsNullOrEmpty(actionName))
                return true;

            string controllerName = typeof(TController).GetControllerName();

            return HasPermission(controllerName, actionName);
        }
        public static bool Login(string email, string password, bool rememberMe = false)
        {
            if (string.IsNullOrEmpty(email)) return false;
            User user = AuthenticationService.ValidateLogin(email, password);
            if (user != null)
            {
                LoginPersister.SignIn(email, rememberMe);
            }

            return Identity != null;
        }

        public static void Logout()
        {
            //PermissionCurrent.RemovePermissinUser();
            LoginPersister.SignOut();
        }

        //// Dirty hacks
        //// It's time racing. I can't help it :(
        //public static RuleResult<Indent> Can(IndentStatus status, Indent indent)
        //{
        //    return new IndentRule(indent).Check(status);
        //}
    }
}
