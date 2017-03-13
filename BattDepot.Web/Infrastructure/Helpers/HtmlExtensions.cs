using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using System.Web.WebPages;
using Davisoft_BDSProject.Domain.Abstract;
using System.IO;

namespace Davisoft_BDSProject.Web.Infrastructure.Helpers
{
    public static class HtmlExtensions
    {

        public static MvcHtmlString EditorForMany<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, IEnumerable<TValue>>> expression, string templateName = null) where TModel : class
        {
            var sb = new StringBuilder();

            // Get the items from ViewData
            var items = expression.Compile()(html.ViewData.Model);
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var htmlFieldPrefix = html.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix;
            var fullHtmlFieldPrefix = String.IsNullOrEmpty(htmlFieldPrefix) ? fieldName : String.Format("{0}.{1}", htmlFieldPrefix, fieldName);
            int index = 0;

            foreach (TValue item in items)
            {
                // Much gratitude to Matt Hidinger for getting the singleItemExpression.
                // Current html.DisplayFor() throws exception if the expression is anything that isn't a "MemberAccessExpression"
                // So we have to trick it and place the item into a dummy wrapper and access the item through a Property
                var dummy = new { Item = item };

                // Get the actual item by accessing the "Item" property from our dummy class
                var memberExpression = Expression.MakeMemberAccess(Expression.Constant(dummy), dummy.GetType().GetProperty("Item"));

                // Create a lambda expression passing the MemberExpression to access the "Item" property and the expression params
                var singleItemExpression = Expression.Lambda<Func<TModel, TValue>>(memberExpression,
                                                                                   expression.Parameters);

                // Now when the form collection is submitted, the default model binder will be able to bind it exactly as it was.
                var itemFieldName = String.Format("{0}[{1}]", fullHtmlFieldPrefix, index++);
                string singleItemHtml = html.EditorFor(singleItemExpression, templateName, itemFieldName).ToString();
                sb.AppendFormat(singleItemHtml);
            }

            return new MvcHtmlString(sb.ToString());
        }

        //public static string RenderRazorViewToString(this Controller controller, string viewName, object model)
        //{
        //    controller.ViewData.Model = model;
        //    using (var sw = new StringWriter())
        //    {
        //        var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
        //        var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
        //        viewResult.View.Render(viewContext, sw);
        //        viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
        //        return sw.GetStringBuilder().ToString();
        //    }
        //}
        public static string RenderPartialToString(this ControllerContext context, string partialViewName, object model)
        {
            return RenderPartialToStringMethod(context, partialViewName, model);
        }

        /// <summary>
        /// render PartialView and return string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="partialViewName"></param>
        /// <param name="viewData"></param>
        /// <param name="tempData"></param>
        /// <returns></returns>
        public static string RenderPartialToString(ControllerContext context, string partialViewName, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            return RenderPartialToStringMethod(context, partialViewName, viewData, tempData);
        }

        public static string RenderPartialToStringMethod(ControllerContext context, string partialViewName, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            ViewEngineResult result = ViewEngines.Engines.FindPartialView(context, partialViewName);

            if (result.View != null)
            {
                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    using (HtmlTextWriter output = new HtmlTextWriter(sw))
                    {
                        ViewContext viewContext = new ViewContext(context, result.View, viewData, tempData, output);
                        result.View.Render(viewContext, output);
                    }
                }

                return sb.ToString();
            }
            return String.Empty;
        }

        public static string RenderPartialToStringMethod(ControllerContext context, string partialViewName, object model)
        {
            ViewDataDictionary viewData = new ViewDataDictionary(model);
            TempDataDictionary tempData = new TempDataDictionary();
            return RenderPartialToStringMethod(context, partialViewName, viewData, tempData);
        }
    }
}
