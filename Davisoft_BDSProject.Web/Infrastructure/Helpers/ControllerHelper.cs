using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;

namespace Davisoft_BDSProject.Web.Infrastructure.Helpers
{
    public static class ControllerHelper
    {
        #region JsonCommand

        public static string RenderRazorViewToString(this ControllerBase controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        public static void RemoveFor<TModel>(this ModelStateDictionary modelState,
                                         Expression<Func<TModel, object>> expression)
        {
            string expressionText = ExpressionHelper.GetExpressionText(expression);

            foreach (var ms in modelState.ToArray())
            {
                if (ms.Key.StartsWith(expressionText + ".") || ms.Key == expressionText)
                {
                    modelState.Remove(ms);
                }
            }
        }
        public static JsonResult JsonCommand(this ControllerBase controller, string success, string failure, object model)
        {
            var result = new JsonCommandObject
                         {
                             Errors = controller.ViewData.ModelState.Count(e => e.Value.Errors.Any()),
                             Model = model
                         };

            string view = controller.ViewData.ModelState.IsValid ? success : failure;
            result.View = !string.IsNullOrEmpty(view)
                              ? RenderRazorViewToString(controller, view, model)
                              : string.Empty;

            return JsonNet(controller, result, JsonRequestBehavior.AllowGet);
        }

        public class JsonCommandObject
        {
            public int Errors { get; set; }
            public object Model { get; set; }
            public string View { get; set; }
        }

        #endregion

        #region JsonNet

        public static JsonNetResult JsonNet(this ControllerBase controller, object model, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return new JsonNetResult { Data = model };
        }

        public class JsonNetResult : JsonResult
        {
            public override void ExecuteResult(ControllerContext context)
            {
                if (context == null)
                    throw new ArgumentNullException("context");

                HttpResponseBase response = context.HttpContext.Response;

                response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

                if (ContentEncoding != null)
                    response.ContentEncoding = ContentEncoding;

                if (Data == null)
                    return;

                // If you need special handling, you can call another form of SerializeObject below
                string serializedObject = JsonConvert.SerializeObject(Data, Formatting.Indented);
                response.Write(serializedObject);
            }
        }

        #endregion

        #region AutoMapped

        public static AutoMappedViewResult View<TModel>(this ControllerBase controller, object model) where TModel : class
        {
            controller.ViewData.Model = model;
            return new AutoMappedViewResult(typeof (TModel))
                   {
                       ViewData = controller.ViewData,
                       TempData = controller.TempData,
                   };
        }


        public class AutoMappedViewResult : ViewResult
        {
            public AutoMappedViewResult(Type type)
            {
                DestinationType = type;
            }

            public Type DestinationType { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                if (ViewData.Model != null)
                    ViewData.Model = Mapper.Map(ViewData.Model,
                                                ViewData.Model.GetType(),
                                                DestinationType);

                base.ExecuteResult(context);
            }
        }

        #endregion

        #region Static-type RedirectToAction

        //public static RedirectToRouteResult RedirectToAction<TController>(this TController controller,
        //                                                                  Expression<Func<TController, object>> actionExpression) where TController : Controller
        //{
        //    string controllerName = typeof(TController).GetControllerName();
        //    string actionName = actionExpression.GetActionName();

        //    return RedirectToAction(controller, actionName, controllerName, null);
        //}

        //public static RedirectToRouteResult RedirectToAction<TController>(this TController controller,
        //                                                                  Expression<Func<TController, object>> actionExpression,
        //                                                                  object values) where TController : Controller
        //{
        //    string controllerName = typeof(TController).GetControllerName();
        //    string actionName = actionExpression.GetActionName();

        //    return RedirectToAction(controller, actionName, controllerName, new RouteValueDictionary(values));
        //}

        //public static RedirectToRouteResult RedirectToAction<TController>(this TController controller,
        //                                                                  Expression<Func<TController, object>> actionExpression,
        //                                                                  AutoMapper.Internal.IDictionary<string, object> dictionary) where TController : Controller
        //{
        //    string controllerName = typeof(TController).GetControllerName();
        //    string actionName = actionExpression.GetActionName();

        //    return RedirectToAction(controller, actionName, controllerName, new RouteValueDictionary(dictionary));
        //}

        //private static RedirectToRouteResult RedirectToAction(Controller controller, string actionName, string controllerName, RouteValueDictionary routeValues)
        //{
        //    RouteValueDictionary routeValues1;
        //    if (controller.RouteData == null)
        //    {
        //        const bool includeImplicitMvcValues = true;
        //        routeValues1 = RouteValuesHelpers.MergeRouteValues(actionName, controllerName, null, routeValues, includeImplicitMvcValues);
        //    }
        //    else
        //    {
        //        const bool includeImplicitMvcValues = true;
        //        routeValues1 = RouteValuesHelpers.MergeRouteValues(actionName, controllerName, controller.RouteData.Values, routeValues, includeImplicitMvcValues);
        //    }
        //    return new RedirectToRouteResult(routeValues1);
        //}


        //public static class RouteValuesHelpers
        //{
        //    public static RouteValueDictionary GetRouteValues(RouteValueDictionary routeValues)
        //    {
        //        if (routeValues == null)
        //            return new RouteValueDictionary();

        //        return new RouteValueDictionary(routeValues);
        //    }

        //    public static RouteValueDictionary MergeRouteValues(string actionName, string controllerName, RouteValueDictionary implicitRouteValues, RouteValueDictionary routeValues, bool includeImplicitMvcValues)
        //    {
        //        var routeValueDictionary = new RouteValueDictionary();
        //        if (includeImplicitMvcValues)
        //        {
        //            object obj;
        //            if (implicitRouteValues != null && implicitRouteValues.TryGetValue("action", out obj))
        //                routeValueDictionary["action"] = obj;
        //            if (implicitRouteValues != null && implicitRouteValues.TryGetValue("controller", out obj))
        //                routeValueDictionary["controller"] = obj;
        //        }
        //        if (routeValues != null)
        //        {
        //            foreach (KeyValuePair<string, object> keyValuePair in GetRouteValues(routeValues))
        //                routeValueDictionary[keyValuePair.Key] = keyValuePair.Value;
        //        }
        //        if (actionName != null)
        //            routeValueDictionary["action"] = actionName;
        //        if (controllerName != null)
        //            routeValueDictionary["controller"] = controllerName;
        //        return routeValueDictionary;
        //    }
        //}

        #endregion

        #region Debug helper

        public class FieldInfo
        {
            public string Key { get; set; }
            public string Message { get; set; }

            public FieldInfo(string key, string message)
            {
                Key = key;
                Message = message;
            }

            public override string ToString()
            {
                return Key + " : " + Message;
            }
        }

        public static List<FieldInfo> TraceValidation(this ControllerBase controller)
        {
            var states = controller.ViewData.ModelState.Where(f => f.Value.Errors.Any()).ToList();
            var errors = (from state in states
                          from error in state.Value.Errors
                          select new FieldInfo(state.Key, error.ErrorMessage)).ToList();

            Debug.Print("----- VALIDATION DETAILS -----");
            foreach (var error in errors)
                Debug.Print(error.ToString());

            return errors;
        }

        #endregion
    }
}
