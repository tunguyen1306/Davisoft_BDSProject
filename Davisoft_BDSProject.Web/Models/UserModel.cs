using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NS.Mvc;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Enum;
using Resources;

namespace Davisoft_BDSProject.Web.Models
{
    public class CreateUserModel
    {
        public List<int> UserRoles { get; set; }
        public IEnumerable<Role> Roles { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldShouldNotBeEmpty")]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldShouldNotBeEmpty")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldPasswordMustBeaMinimumLengthOf6")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldShouldNotBeEmpty")]
        [Compare("Password", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ConfirmPasswordAndPasswordDoNotMatch")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldShouldNotBeEmpty")]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheEmailAddressEnteredIsInvalid")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldShouldNotBeEmpty")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9\+]{1,}[0-9\-\ ]{3,15}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldIsInvalid")]
        public string Phone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9\+]{1,}[0-9\-\ ]{3,15}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldIsInvalid")]
        public string MobilePhone { get; set; }

        public HttpPostedFileBase Picture { get; set; }

        public int? BranchID { get; set; }

        public List<Branch> Branches { get; set; }
        public List<Branch> UserBranches { get; set; }

        public string Address { get; set; }
        public string AddressEnglish { get; set; }
        public string FaxNo { get; set; }
        public string Website { get; set; }
    }

    public class EditUserModel
    {
        public EditUserModel()
        {
        }

        public EditUserModel(User user)
        {
            ID = user.ID;
            Username = user.DisplayName;
            Email = user.Email;
            Phone = user.Phone;
            MobilePhone = user.MobilePhone;
            UserRoles = user.Roles;
            UserPicture = user.Picture;
            LastAccess = user.LastAccess;
            UserBranches = user.Branches.ToList();
            Address = user.Address;
            AddressEnglish = user.AddressEnglish;
            FaxNo = user.FaxNo;
            Website = user.Website;
        }

        public IEnumerable<Role> UserRoles { get; set; }
        public IEnumerable<Role> Roles { get; set; }

        [HiddenInput]
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldShouldNotBeEmpty")]
        public string Username { get; set; }

        [MinLength(6, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldPasswordMustBeaMinimumLengthOf6")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ConfirmPasswordAndPasswordDoNotMatch")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldShouldNotBeEmpty")]
        //[RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheEmailAddressEnteredIsInvalid")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldShouldNotBeEmpty")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9\+]{1,}[0-9\-\ ]{3,15}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldIsInvalid")]
        public string Phone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9\+]{1,}[0-9\-\ ]{3,15}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldIsInvalid")]
        public string MobilePhone { get; set; }

        public HttpPostedFileBase Picture { get; set; }

        public string UserPicture { get; set; }
        public int BranchID { get; set; }
        //public Branch Branch { get; set; }
        public List<Branch> Branches { get; set; }
        public List<Branch> UserBranches { get; set; }

        public DateTime? LastAccess { get; set; }
        public string Address { get; set; }
        public string AddressEnglish { get; set; }
        public string FaxNo { get; set; }
        public string Website { get; set; }
    }
    public class AccountModel
    {
        public AccountModel()
        {
        }

        public AccountModel(User user)
        {
            Username = user.DisplayName;
            Email = user.Email;
            Phone = user.Phone;
            MobilePhone = user.MobilePhone;
            Branch = user.Branch;
        }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldShouldNotBeEmpty")]
        public string Username { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldShouldNotBeEmpty")]
        public string OldPassword { get; set; }

        [MinLength(6, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldPasswordMustBeaMinimumLengthOf6")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ConfirmPasswordAndPasswordDoNotMatch")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldShouldNotBeEmpty")]
        //[RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheEmailAddressEnteredIsInvalid")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldShouldNotBeEmpty")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9\+]{1,}[0-9\-\ ]{3,15}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldIsInvalid")]
        public string Phone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9\+]{1,}[0-9\-\ ]{3,15}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "TheFieldIsInvalid")]
        public string MobilePhone { get; set; }

        public HttpPostedFileBase Picture { get; set; }

        public Branch Branch { get; set; }
    }
}
