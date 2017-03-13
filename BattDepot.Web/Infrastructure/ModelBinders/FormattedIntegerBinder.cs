using System.ComponentModel;
using System.Web.Mvc;

namespace Davisoft_BDSProject.Web.Infrastructure.ModelBinders
{
    public class FormattedIntegerBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            // need to skip properties that aren't part of the request, else we might hit a StackOverflowException
            string fullPropertyKey = CreateSubPropertyName(bindingContext.ModelName, propertyDescriptor.Name);
            if (!bindingContext.ValueProvider.ContainsPrefix(fullPropertyKey))
                return;

            // call base method to trigger validation
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);

            // retrieve interger value
            if (propertyDescriptor.PropertyType == typeof(int))
            {
                var decimalValue = (decimal) bindingContext.ValueProvider.GetValue(fullPropertyKey).ConvertTo(typeof (decimal));
                var intValue = (int) decimalValue;
                propertyDescriptor.SetValue(bindingContext.Model, intValue);
            }
        }
    }
}
