using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Infrastructure;
using Davisoft_BDSProject.Web.Models;
using NS.Mvc.ActionFilters;
using DependencyHelper = NS.Mvc.DependencyHelper;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class MenusController : Controller
    {
        private readonly IMenuRepository _menuRepo;
        private readonly IRoleService _roleService;

        public MenusController(IMenuRepository menuRepo, IRoleService roleService)
        {
            _menuRepo = menuRepo;
            _roleService = roleService;
        }
        [DisplayName(@"Menu management")]
        public ActionResult Index()
        {
            IEnumerable<Menu> menu = _menuRepo.GetAllChildren();
            return View(menu);
        }

        public ActionResult Create(int parentID = 0)
        {
            var model = new MenuEditModel
                        {
                            Menu = new Menu { ParentID = parentID, Order = 0 },
                            Roles = _roleService.GetAllRoles().ToList(),
                            MenuRoles = new List<Role>()
                        };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(MenuEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = _roleService.GetAllRoles().ToList();
                return View(model);
            }

            IEnumerable<int> menuRoles = StringHelper.Ensure(Request.Form["SelectedRoles"])
                                                     .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                     .Select(id => Convert.ToInt32(id));

            _menuRepo.Create(model.Menu, menuRoles.ToArray());

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Menu menu = _menuRepo.Get(id);
            var model = new MenuEditModel
                        {
                            Menu = menu,
                            Roles = _roleService.GetAllRoles().ToList(),
                            MenuRoles = menu.Roles.ToList()
                        };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MenuEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = _roleService.GetAllRoles().ToList();
                return View(model);
            }

            IEnumerable<int> menuRoles = StringHelper.Ensure(Request.Form["SelectedRoles"])
                                                     .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                     .Select(id => Convert.ToInt32(id));

            _menuRepo.Update(model.Menu, menuRoles.ToArray());

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Menu model = _menuRepo.Get(id);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete"), DisplayName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _menuRepo.Delete(id);
            return RedirectToAction("Index");
        }
        [AjaxOnly]
        public ActionResult UpdateMenuResources()
        {
            var memory = DependencyHelper.GetService<MemoryMenuRepository>();
            var menus = memory.GetAll().ToList();
            try
            {
                var path1 = Server.MapPath("~/App_GlobalResources/MenuResource.resx");
                var xmlDoc1 = new XmlDocument();
                xmlDoc1.Load(path1);
                bool ischanged = false;
                foreach (var menu in menus)
                {
                    var dataname = ("Menu" + StringHelper.RemoveSpecialCharacters(menu.Title)).ToLower();
                    XmlNodeList nodes = xmlDoc1.SelectNodes("//data[@name='" + dataname + "']");
                    if (nodes.Count == 0)
                    {
                        ischanged = true;
                        XmlElement data = xmlDoc1.CreateElement("data");
                        XmlElement value = xmlDoc1.CreateElement("value");
                        value.InnerText = menu.Title;
                        XmlElement comment = xmlDoc1.CreateElement("comment");
                        comment.InnerText = "";
                        data.SetAttribute("name", dataname);
                        data.SetAttribute("xml:space", "preserve");
                        data.AppendChild(value);
                        data.AppendChild(comment);
                        xmlDoc1.DocumentElement.AppendChild(data);
                    }
                }
                if (ischanged)
                    xmlDoc1.Save(path1);
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
}
