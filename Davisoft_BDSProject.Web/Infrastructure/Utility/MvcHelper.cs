using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using Ninject.Infrastructure.Language;
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Filters;

namespace Davisoft_BDSProject.Web.Infrastructure.Utility
{
    public static class MvcHelper
    {
        public static List<Type> GetSubClasses<T>()
        {
            return Assembly.GetCallingAssembly()
                           .GetTypes()
                           .Where(type => type.IsSubclassOf(typeof(T)))
                           .ToList();
        }

        public static List<Type> GetControllers()
        {
            return GetSubClasses<Controller>().ToList();
        }

        // TODO: filter only method which are action AND return the whole page (note partial view)
        public static List<Tuple<string, string>> GetControllerActions(Type controller)
        {
            MethodInfo[] methods = controller.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            var list = new List<Tuple<string, string>>();
            foreach (MethodInfo info in methods)
            {
                object[] attributes = info.GetCustomAttributes(typeof(ApplyAuthorizeAttribute), true);
                bool any = attributes.Any();

                if (((any && ((ApplyAuthorizeAttribute)attributes[0]).IsAuthorize) || // Property 'authorize' is prior
                    (!any && (info.ReturnType == typeof(ActionResult) || info.ReturnType == typeof(ViewResult)))) && !info.HasAttribute(typeof(AjaxOnlyAttribute))) // Actionname is second prior
                {
                    string name = info.Name;
                    string displayname = info.Name;
                    object[] actionNameAttribute = info.GetCustomAttributes(typeof(ActionNameAttribute), true);
                    object[] actionDisplayAttribute = info.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    if (actionNameAttribute.Any())
                    {

                        name = ((ActionNameAttribute)actionNameAttribute[0]).Name;
                    }
                    if (actionDisplayAttribute.Any())
                    {
                        displayname = ((DisplayNameAttribute)actionDisplayAttribute[0]).DisplayName;
                    }
                    list.Add(new Tuple<string, string>(name, displayname));
                }
            }
            return list.Distinct().ToList();
        }

        public static Dictionary<string, List<Tuple<string, string>>> GetControllerTree()
        {
            var tree = new Dictionary<string, List<Tuple<string, string>>>();

            List<Type> controllers = GetControllers();
            controllers.ForEach(c => tree.Add(c.Name.Replace("Controller", ""), GetControllerActions(c)));

            return tree;
        }

        /// <summary>
        /// Get controller name from type
        /// </summary>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        public static string GetControllerName(this Type controllerType)
        {
            return controllerType.Name.Replace("Controller", "");
        }

        /// <summary>
        /// Get action name from lambda expression
        /// </summary>
        /// <param name="actionExpression"></param>
        /// <returns></returns>
        public static string GetActionName(this LambdaExpression actionExpression)
        {
            return ((MethodCallExpression)actionExpression.Body).Method.Name;
        }

        public static bool CheckActionIsAjaxOnly(string controllerName, string actionName)
        {
            var controller = GetSubClasses<Controller>().FirstOrDefault(m => m.Name == controllerName + "Controller");
            if (controller != null)
            {
                var action =
                    controller.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        .FirstOrDefault(m => m.Name == actionName);
                if (action != null && action.HasAttribute(typeof(AjaxOnlyAttribute)))
                    return true;
            }
            return false;
        }
    }
}
