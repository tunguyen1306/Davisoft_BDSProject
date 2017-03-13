using System.Text.RegularExpressions;
using FluentValidation;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Resources;

namespace Davisoft_BDSProject.Web.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        private readonly IMembershipService _membershipService;
        private readonly IRoleService _roleService;

        public UserValidator(IMembershipService membershipService, IRoleService roleService)
        {
            _membershipService = membershipService;
             _roleService = roleService;

             RuleFor(u => u.DisplayName).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
             RuleFor(u => u.DisplayName).Must(BeAUniqueName).WithMessage("'" + Resource.UserName + "' " + Resource.AlreadyExist);
            RuleFor(u => u.Email).NotEmpty().WithMessage(Resource.TheFieldShouldNotBeEmpty);
            RuleFor(u => u.Email).Matches(new Regex(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")).WithMessage(Resource.TheEmailAddressEnteredIsInvalid);
            RuleFor(u => u.Email).Must(BeAUniqueEmail).WithMessage("'" + Resource.Email + "' " + Resource.AlreadyExist);
            RuleFor(u => u.MobilePhone).Matches(new Regex("^[0-9]*$")).WithMessage(Resource.OnlyNumberAllowed);
            RuleFor(u => u.Phone).Matches(new Regex("^[0-9]*$")).WithMessage(Resource.OnlyNumberAllowed);
        }

        private bool BeAUniqueEmail(User user, string email)
        {
            User existUser = _membershipService.GetUserByEmail(email);
            return existUser == null || existUser.ID == user.ID;
        }

        private bool BeAUniqueName(User user, string username)
        {
            User existUser = _membershipService.GetUserByName(username);
            return existUser == null || existUser.ID == user.ID;
        }
    }
}
