using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Web.Infrastructure.Filters;
using Davisoft_BDSProject.Web.Infrastructure.Utility;
using Davisoft_BDSProject.Web.Models;
using Resources;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRoleService _repo;

        public RolesController(IRoleService repo)
        {
            _repo = repo;
        }

        [DisplayName(@"Role management")]
        public ActionResult Index()
        {
            IEnumerable<Role> roles = _repo.GetAllRoles();
            return View(roles);
        }

        public ActionResult Create()
        {
            var model = new RoleModel
            {
                Role = new Role(),
                Permissions = RequestPermissionProvider.GetPermissions()
            };
            model.PrepareSettingPermission();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(RoleModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Role.Name))
                ModelState.AddModelError("Role.Name", Resource.TheFieldShouldNotBeEmpty);

            if (string.IsNullOrEmpty(model.Role.RoleLevel))
                ModelState.AddModelError("Role.RoleLevel", Resource.TheFieldShouldNotBeEmpty);

            Role role = _repo.GetRoleByName(model.Role.Name);
            if (role != null)
                ModelState.AddModelError("Role.Name", Resource.TheNameAlreadyExists);

            if (!ModelState.IsValid)
            {
                model = new RoleModel
                    {
                        Role = model.Role,
                        Permissions = RequestPermissionProvider.GetPermissions()
                    };
                model.PrepareSettingPermission();
                return View(model);
            }

            List<int> rolePermissions = (from string key in Request.Form.Keys
                                         //where key.StartsWith(RoleModel.PermissionNamePrefix) && Request.Form[key].Contains("true")
                                         where key.StartsWith(RoleModel.PermissionNamePrefix)
                                         select key.Replace(RoleModel.PermissionNamePrefix, "").Split(new[] { '.' })
                                             into segments
                                             let target = segments[0]
                                             let right = segments[1]
                                             select _repo.EnsurePermissionRecord(target, right)
                                                 into permission
                                                 select permission.ID).ToList();

            rolePermissions.AddRange(from string key in Request.Form.Keys
                                     where key.StartsWith(RoleModel.SettingPermissionNamePrefix)
                                     select key.Replace(RoleModel.SettingPermissionNamePrefix, "").Split(new[] { '.' })
                                         into segments
                                         let target = segments[0]
                                         let right = segments[1]
                                         let type = PermissionType.SettingPermission
                                         select _repo.EnsurePermissionRecord(target, right, type)
                                             into permission
                                             select permission.ID);

            _repo.CreateRole(model.Role, rolePermissions);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var model = new RoleModel
            {
                Role = _repo.GetRole(id),
                Permissions = RequestPermissionProvider.GetPermissions()
            };
            model.PrepareSettingPermission();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RoleModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Role.Name))
                ModelState.AddModelError("Role.Name", Resource.TheFieldShouldNotBeEmpty);

            if (string.IsNullOrEmpty(model.Role.RoleLevel))
                ModelState.AddModelError("Role.RoleLevel", Resource.TheFieldShouldNotBeEmpty);

            Role role = _repo.GetRoleByName(model.Role.Name);
            if (role != null && role.ID != model.Role.ID)
                ModelState.AddModelError("Role.Name", Resource.TheNameAlreadyExists);

            if (!ModelState.IsValid)
                return Edit(model.Role.ID);

            List<int> rolePermissions = (from string key in Request.Form.Keys
                //where key.StartsWith(RoleModel.PermissionNamePrefix) && Request.Form[key].Contains("true")
                where key.StartsWith(RoleModel.PermissionNamePrefix)
                select key.Replace(RoleModel.PermissionNamePrefix, "").Split(new[] {'.'})
                into segments
                let target = segments[0]
                let right = segments[1]
                select _repo.EnsurePermissionRecord(target, right)
                into permission
                select permission.ID).ToList();

            rolePermissions.AddRange(from string key in Request.Form.Keys
                where key.StartsWith(RoleModel.SettingPermissionNamePrefix)
                select key.Replace(RoleModel.SettingPermissionNamePrefix, "").Split(new[] {'.'})
                into segments
                let target = segments[0]
                let right = segments[1]
                let type = PermissionType.SettingPermission
                select _repo.EnsurePermissionRecord(target, right, type)
                into permission
                select permission.ID);

            _repo.UpdateRole(model.Role.ID, model.Role, rolePermissions);
            model.PrepareSettingPermission();

            ViewBag.Success = true;
            ViewBag.Message = Resource.SaveSuccessful;
            return Edit(model.Role.ID);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repo.DeleteRole(id);
            return RedirectToAction("Index");
        }
    }
}
