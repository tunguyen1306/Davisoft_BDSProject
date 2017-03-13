using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Internal;
using FluentValidation.Mvc;
using FluentValidation.Validators;

namespace Davisoft_BDSProject.Web.Infrastructure
{
    public class GreaterThanValidatorEx : FluentValidationPropertyValidator
    {
        public GreaterThanValidatorEx(ModelMetadata metadata, ControllerContext controllerContext, PropertyRule rule, IPropertyValidator validator)
            : base(metadata, controllerContext, rule, validator)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            if (!this.ShouldGenerateClientSideRules())
            {
                yield break;
            }

            var validator = Validator as LessThanOrEqualValidator;

            var errorMessage = new MessageFormatter().AppendPropertyName(this.Rule.GetDisplayName())
                                                     .BuildMessage(validator.ErrorMessageSource.GetString());

            var rule = new ModelClientValidationRule
            {
                ErrorMessage = errorMessage,
                ValidationType = (this.Metadata.DataTypeName == "DateTime" ? "greaterthandate" : "greaterthaninteger")
            };

            rule.ValidationParameters["other"] = CompareAttribute.FormatPropertyForClientValidation(validator.MemberToCompare.Name);
            
            yield return rule;
        }
    }
}