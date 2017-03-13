using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web.Mvc;
using FluentValidation.Mvc;
using iTextSharp.text.pdf.qrcode;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Enum;
using Davisoft_BDSProject.Web.Infrastructure.Filters;
using Davisoft_BDSProject.Web.Infrastructure.Helpers;
using Davisoft_BDSProject.Web.Models;
using Resources;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IUnitRepository _unitRepository;
        public ResourceController(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }
        #region edit resource
        public ActionResult Index()
        {
            var model = new ResourceViewModel();
            model.PrepareLanguages();
            return View(model);
        }
        //[HttpPost]
        //public ActionResult Index(ResourceViewModel model)
        //{
        //    var changes = model.ResourceModels.SelectMany(m => m.Resources.Where(r => r.IsChange)).ToList();
        //    var langs = changes.Select(m => m.LanguageCode).Distinct();
        //    foreach (var lang in langs)
        //    {
        //        var path1 = "";
        //        var path2 = "";
        //        if (lang.ToLower().Contains("en"))
        //        {
        //            path1 = Server.MapPath("~/App_GlobalResources/MenuResource.resx");
        //            path2 = Server.MapPath("~/App_GlobalResources/Resource.resx");
        //        }
        //        else
        //        {
        //            path1 = Server.MapPath("~/App_GlobalResources/MenuResource." + lang + ".resx");
        //            path2 = Server.MapPath("~/App_GlobalResources/Resource." + lang + ".resx");
        //        }
        //        model.EditAnyResourceByLanguage(changes.Where(m => m.LanguageCode == lang), path1, path2);
        //    }
        //    model.Prepare();
        //    return View(model);
        //}
        public JsonResult EditResource(string[] langs, string code)
        {
            var languages = _unitRepository.GetAllLanguages().ToList();
            var model = new ResourceModel();
            for (int i = 0; i < languages.Count(); i++)
            {
                if (!string.IsNullOrEmpty(langs[i]))
                {
                    var path1 = "";
                    var path2 = "";
                    if (languages[i].Value.ToLower().Contains("en"))
                    {
                        path1 = Server.MapPath("~/App_GlobalResources/MenuResource.resx");
                        path2 = Server.MapPath("~/App_GlobalResources/Resource.resx");
                    }
                    else
                    {
                        path1 = Server.MapPath("~/App_GlobalResources/MenuResource." + languages[i].Value + ".resx");
                        path2 = Server.MapPath("~/App_GlobalResources/Resource." + languages[i].Value + ".resx");
                    }
                    model.EditOneResource(langs[i], code, path1, path2);
                }
            }
            return Json(Resource.UpdateSuccessful);
        }
        public JsonResult EditAllResource(List<ResourceItemModel> models)
        {
            if (models == null)
                return Json(Resource.SaveError);
            var model = new ResourceViewModel();
            var langs = models.Select(m => m.LanguageCode).Distinct();
            foreach (var lang in langs)
            {
                var path1 = "";
                var path2 = "";
                if (lang.ToLower().Contains("en"))
                {
                    path1 = Server.MapPath("~/App_GlobalResources/MenuResource.resx");
                    path2 = Server.MapPath("~/App_GlobalResources/Resource.resx");
                }
                else
                {
                    path1 = Server.MapPath("~/App_GlobalResources/MenuResource." + lang + ".resx");
                    path2 = Server.MapPath("~/App_GlobalResources/Resource." + lang + ".resx");
                }
                model.EditAnyResourceByLanguage(models.Where(m => m.LanguageCode == lang), path1, path2);
            }
            return Json(Resource.UpdateSuccessful);
        }

        #endregion
        [AjaxOnly]
        public ActionResult AjaxGetResourcesIndexView()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            // ReSharper disable once AssignNullToNotNullAttribute
            var start = Request.Form.GetValues("start").FirstOrDefault();
            // ReSharper disable once AssignNullToNotNullAttribute
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find Order Column
            //var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            //// ReSharper disable once AssignNullToNotNullAttribute
            //var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            #region For Search

            var searchValue = "";
            // ReSharper disable once AssignNullToNotNullAttribute
            if (Request.Form.GetValues("search[value]").FirstOrDefault() != null)
            {
                // ReSharper disable once PossibleNullReferenceException
                searchValue = Request.Form.GetValues("search[value]").FirstOrDefault().Trim().ToLower();
            }

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var model = new ResourceViewModel();
            model.Prepare();
            if (!string.IsNullOrEmpty(searchValue))
            {
                model.ResourceModels = model.ResourceModels.Where(m =>
                    (m.Code.ToLower().Contains(searchValue)) ||
                    (m.Resources.Any(i => i.Description != null && i.Description.ToLower().Contains(searchValue)))
                    ).ToList();
            }
            #endregion For Search

            recordsTotal = model.ResourceModels.Count();
            if (pageSize > recordsTotal || pageSize <= 0)
            {
                pageSize = recordsTotal;
            }
            var data = model.ResourceModels.OrderBy(m => m.Code).Skip(skip).Take(pageSize).ToList();


            var json = new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = data.Select(m => new
                {
                    m.Code,
                    Items = m.Resources.Select(i => new
                    {
                        i.Code,
                        i.LanguageCode,
                        i.Description,
                    }),
                })
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Create()
        {
            return View(new Language());
        }

        [HttpPost]
        public ActionResult Create(Language language)
        {
            if (_unitRepository.GetLanguage(language.Value) != null)
                ModelState.AddModelError("Value", Resource.AlreadyExist);

            if (ModelState.IsValid)
            {
                if (_unitRepository.CreateLanguage(language) != null)
                {
                    if (!language.Value.Contains("en"))
                    {
                        var path1 = Server.MapPath("~/App_GlobalResources/MenuResource." + language.Value + ".resx");
                        if (!System.IO.File.Exists(path1))
                        {
                            System.IO.File.Copy(Server.MapPath("~/App_GlobalResources/MenuResource.resx"), path1);
                        }
                        var path2 = Server.MapPath("~/App_GlobalResources/Resource." + language.Value + ".resx");
                        if (!System.IO.File.Exists(path2))
                        {
                            System.IO.File.Copy(Server.MapPath("~/App_GlobalResources/Resource.resx"), path2);
                        }
                    }
                    return RedirectToAction("Index");
                }
            }
            return View(language);
        }
        [RuleSetForClientSideMessages("Edit")]
        public ActionResult Edit(int id)
        {
            return View(_unitRepository.GetLanguage(id));
        }
        [HttpPost]
        [RuleSetForClientSideMessages("Edit")]
        public ActionResult Edit(Language language)
        {
            if (ModelState.IsValid)
            {
                if (_unitRepository.UpdateLanguage(language))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(language);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            _unitRepository.DeleteLanguage(id);
            return RedirectToAction("Index");
        }
    }

}