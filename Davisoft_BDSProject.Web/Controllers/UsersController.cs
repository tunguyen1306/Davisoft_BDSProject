using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web.Mvc;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Enum;
using Davisoft_BDSProject.Web.Infrastructure;
using Davisoft_BDSProject.Web.Infrastructure.Filters;
using Davisoft_BDSProject.Web.Infrastructure.Utility;
using Davisoft_BDSProject.Web.Models;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using Resources;
using StringHelper = Davisoft_BDSProject.Web.Infrastructure.Utility.StringHelper;
using Davisoft_BDSProject.Domain.Helpers;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IMembershipService _membershipService;
        private readonly IRoleService _roleService;
        private readonly ILoginTracker _loginTracker;
        private readonly IUnitRepository _repoUnit;

        public UsersController(IMembershipService membershipService, IRoleService roleService, ILoginTracker loginTracker, IUnitRepository repoUnit)
        {
            _membershipService = membershipService;
            _roleService = roleService;
            _loginTracker = loginTracker;
            _repoUnit = repoUnit;
        }
        [DisplayName("User management")]
        public ActionResult Index(int role = 0)
        {
            IEnumerable<User> users = _membershipService.GetUsers().Where(u => (role == 0 || u.Roles.Select(m => m.ID).Contains(role)) && !u.Roles.Any(m => m.Name.ToUpper().Contains("DEALER")));
            ViewBag.roles = _roleService.GetAllRoles().Where(n => !n.Name.ToUpper().Contains("DEALER"));
            return View(users);
        }

        [DisplayName("List deleted user")]
        public ActionResult ListDeletedUsers()
        {
            IEnumerable<User> users = _membershipService.GetUsersDeleted();
            ViewBag.roles = _roleService.GetAllRoles();
            return View(users);
        }
        public ActionResult Create()
        {
            var brands = _repoUnit.GetAllBranchsofSalesman().ToList();

            brands.Insert(0, new Branch { ID = 0, Code = "-- " + Resource.SelectBranch + " --" });

            var model = new CreateUserModel
                {
                    UserRoles = new List<int>(),
                    Roles = _roleService.GetAllRoles(),
                    Branches = brands,
                    UserBranches = new List<Branch>(),
                };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateUserModel model)
        {
            if (_membershipService.GetUserByName(model.Username) != null)
                ModelState.AddModelError("DisplayName", Resource.UserNameExists);

            if (_membershipService.GetUserByEmail(model.Email) != null)
                ModelState.AddModelError("Email", Resource.UserEmailExists);

            IEnumerable<int> userRoles = StringHelper.Ensure(Request.Form["SelectedRoles"])
                                                     .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                     .Select(id => Convert.ToInt32(id));
            IEnumerable<int> branches = StringHelper.Ensure(Request.Form["SelectedBranches"])
                                                     .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                     .Select(id => Convert.ToInt32(id));

            if (!ModelState.IsValid)
            {
                model.UserRoles = userRoles.ToList();
                model.Roles = _roleService.GetAllRoles();
                var branches1 = _repoUnit.GetAllBranches().ToList();

                model.UserBranches = _repoUnit.GetAllBranches().Where(m => branches.Contains(m.ID)).ToList();
                branches1.Insert(0, new Branch { ID = 0, Code = "-- " + Resource.SelectBranch + " --" });
                model.Branches = branches1;
                return View(model);
            }
            var user = new User
                {
                    DisplayName = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    Phone = model.Phone,
                    MobilePhone = model.MobilePhone,
                    BranchID = model.BranchID == 0 ? null : (int?)model.BranchID,
                    Branches = new HashSet<Branch>(),
                };

            user = _membershipService.CreateUser(user);

            string userPicture = UserPicture.Upload(user.ID, model.Picture);
            if (!string.IsNullOrEmpty(userPicture))
                _membershipService.UpdateUserPicture(user.ID, userPicture);

            _roleService.AssignRoles(user, userRoles);
            //_roleService.AssignBranches(user, new List<int> {user.BranchID});
            TempData["message"] = Resource.AddSuccessful;
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var brands = _repoUnit.GetAllBranches().ToList();

            brands.Insert(0, new Branch { ID = 0, Code = "-- " + Resource.SelectBranch + " --" });
            User user = _membershipService.GetUser(id);
            if (user.BranchID == null)
            {
                user.BranchID = 0;
            }
            var model = new EditUserModel(user)
                {
                    Roles = _roleService.GetAllRoles(),
                    Branches = brands,
                    BranchID = Convert.ToInt32(user.BranchID)
                };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditUserModel model)
        {
            User user = _membershipService.GetUserByName(model.Username);
            if (user != null && user.ID != model.ID)
                ModelState.AddModelError("DisplayName", Resource.UserNameExists);

            user = _membershipService.GetUserByEmail(model.Email);
            if (user != null && user.ID != model.ID)
                ModelState.AddModelError("Email", Resource.UserEmailExists);

            if (string.IsNullOrEmpty(model.Password) && model.Password != model.ConfirmPassword)
                ModelState.AddModelError("User.Password", Resource.PasswordMismatch);

            IEnumerable<int> userRoles = StringHelper.Ensure(Request.Form["SelectedRoles"])
                                                     .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                     .Select(id => Convert.ToInt32(id));
            //IEnumerable<int> branches = StringHelper.Ensure(Request.Form["SelectedBranches"])
            //                                         .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            //                                         .Select(id => Convert.ToInt32(id));
          
            if (!ModelState.IsValid)
            {
                //return Edit(model.ID);
                var brands = _repoUnit.GetAllBranches().ToList();
                brands.Insert(0, new Branch { ID = 0, Code = "-- " + Resource.SelectBranch + " --" });
                if (user.BranchID == null)
                {
                    user.BranchID = 0;
                }
                var oldUser = _membershipService.GetUser(model.ID);

                model.UserRoles = _roleService.GetAllRoles().Where(m => userRoles.Contains(m.ID));
                model.UserBranches = oldUser.Branches.ToList();
                model.Roles = _roleService.GetAllRoles();
                model.Branches = brands;
                model.BranchID = Convert.ToInt32(user.BranchID);
                return View(model);
            }

            user = _membershipService.GetUser(model.ID);
            if (user.BranchID != null && user.BranchID != model.BranchID)
            {
                _repoUnit.AddToUserBranchList(model.ID, Convert.ToInt32(user.BranchID));
            }
            var oldBranches = user.Branches.Select(m => m.ID).ToList();
            if (user.BranchID != null && user.BranchID > 0 && user.BranchID != model.BranchID)
            {
                oldBranches.Add((int)user.BranchID);
                oldBranches = oldBranches.Distinct().ToList();
                _roleService.AssignBranches(user, oldBranches);
            }

            user.DisplayName = model.Username;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.MobilePhone = model.MobilePhone;
            if (!string.IsNullOrEmpty(model.Password))
            {
                user.Password = EncryptHelper.EncryptPassword(model.Password);
            }
            user.BranchID = model.BranchID == 0 ? null : (int?)model.BranchID;
            //user.LastAccess = model.LastAccess;
            var success = _membershipService.UpdateUser(user);
            string userPicture = UserPicture.Upload(model.ID, model.Picture);
            if (!string.IsNullOrEmpty(userPicture))
                _membershipService.UpdateUserPicture(user.ID, userPicture);

            _roleService.AssignRoles(user, userRoles);

            _loginTracker.ReloadUser(user.Email, user);
            if (success)
            {
                TempData["message"] = Resource.SaveSuccessful;
                return RedirectToAction("Index");
            }
            ViewBag.Success = true;
            ViewBag.Message = Resource.SaveFailed;
            return RedirectToAction("Edit", new { Id = model.ID });
        }
        [AjaxOnly]
        public ActionResult Delete_keeptrack(int id)
        {
            User user = _membershipService.GetUser(id);
            if (user != null)
            {
                _membershipService.DeleteUser(user.ID);
            }
            return Json(1);
        }
        [HttpPost, ActionName("ReActive")]
        public ActionResult ReActive(int id)
        {
            var user = _membershipService.GetUser(id);
            if (_membershipService.GetAllUserByEmail(user.Email).Count() == 1)
            {
                try
                {
                    _membershipService.ReActiveUser(id);
                }
                catch (Exception)
                {
                    TempData["Message"] = Resource.CannotReactiveUser;
                    return RedirectToAction("ListDeletedUsers");
                }
            }
            else
            {
                TempData["Message"] = Resource.UserEmailActive;
                return RedirectToAction("ListDeletedUsers");
            }

            return RedirectToAction("ListDeletedUsers");
        }
    }

}
