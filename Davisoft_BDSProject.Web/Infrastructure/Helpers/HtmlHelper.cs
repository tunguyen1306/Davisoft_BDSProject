using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Davisoft_BDSProject.Web.Infrastructure.Helpers
{
    public static class BootstrapHtmlHelper
    {
        public static MvcHtmlString DatePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            return htmlHelper.DatePickerFor(expression, null, null);
        }

        public static MvcHtmlString DatePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string format)
        {
            return htmlHelper.DatePickerFor(expression, format, null);
        }

        public static MvcHtmlString DatePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return htmlHelper.DatePickerFor(expression, null, htmlAttributes);
        }

        public static MvcHtmlString DatePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string format, object htmlAttributes)
        {
            var attributes = new RouteValueDictionary(htmlAttributes);
            attributes["class"] += " date-picker";
            attributes["placeholder"] = "dd/mm/yyyy";
            //attributes["data-provide"] = "datetimepicker";
            attributes["data-date-format"] = "dd/mm/yyyy";

            if (string.IsNullOrEmpty(format))
                format = "{0:dd/MM/yyyy}";
            else
                format = "{0:" + format + "}";

            string name = ExpressionHelper.GetExpressionText(expression);
            var date = (DateTime) (htmlHelper.ViewData.Eval(name) ?? default(DateTime));
            object value = date == default(DateTime)
                               ? (object) ""
                               : date;

            return htmlHelper.TextBox(name, value, format, attributes);
        }

        public static MvcHtmlString DateTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string format, object htmlAttributes)
        {
            if (string.IsNullOrEmpty(format))
                format = "{0:dd/MM/yyyy}";
            else
                format = "{0:" + format + "}";

            string name = ExpressionHelper.GetExpressionText(expression);
            var date = (DateTime) (htmlHelper.ViewData.Eval(name) ?? default(DateTime));
            object value = date == default(DateTime)
                               ? (object) ""
                               : date;

            return htmlHelper.TextBox(name, value, format, htmlAttributes);
        }
    }
}
