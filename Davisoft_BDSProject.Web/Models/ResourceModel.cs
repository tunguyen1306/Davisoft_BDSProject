using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Web;
using System.Xml;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using NS.Mvc;

namespace Davisoft_BDSProject.Web.Models
{
    public class ResourceModel
    {
        public ICollection<ResourceItemModel> Resources { get; set; }
        public string Code { get; set; }
        public ResourceModel()
        {
            Resources = new List<ResourceItemModel>();
        }
        internal void EditOneResource(string newValueEdit, string dataname, string PathResx1, string PathResx2)
        {
            if (dataname.ToLower().StartsWith("menu"))
            {
                var xmlDoc1 = new XmlDocument();
                xmlDoc1.Load(PathResx1);
                XmlNodeList nodes = xmlDoc1.SelectNodes("//data[@name='" + dataname + "']");
                if (nodes.Count != 0)
                {
                    var child = nodes.Item(0).ChildNodes.Item(1);
                    if (child.FirstChild.Value != newValueEdit)
                    {
                        var oldchild = nodes.Item(0).ChildNodes.Item(1);
                        child.FirstChild.Value = newValueEdit;
                        child.ReplaceChild(child.FirstChild, oldchild.FirstChild);
                        xmlDoc1.Save(PathResx1);
                    }

                }
                else
                {
                    XmlElement data = xmlDoc1.CreateElement("data");
                    XmlElement value = xmlDoc1.CreateElement("value");
                    value.InnerText = newValueEdit;
                    XmlElement comment = xmlDoc1.CreateElement("comment");
                    comment.InnerText = "";
                    data.SetAttribute("name", dataname);
                    data.SetAttribute("xml:space", "preserve");
                    data.AppendChild(value);
                    data.AppendChild(comment);
                    xmlDoc1.DocumentElement.AppendChild(data);
                    xmlDoc1.Save(PathResx1);
                }
            }
            else
            {
                var xmlDoc2 = new XmlDocument();
                xmlDoc2.Load(PathResx2);
                XmlNodeList nodes2 = xmlDoc2.SelectNodes("//data[@name='" + dataname + "']");
                if (nodes2.Count != 0)
                {
                    var child = nodes2.Item(0).ChildNodes.Item(1);
                    if (child.FirstChild.Value != newValueEdit)
                    {
                        var oldchild = nodes2.Item(0).ChildNodes.Item(1);
                        child.FirstChild.Value = newValueEdit;
                        child.ReplaceChild(child.FirstChild, oldchild.FirstChild);
                        xmlDoc2.Save(PathResx2);
                    }
                }
                else
                {
                    XmlElement data = xmlDoc2.CreateElement("data");
                    XmlElement value = xmlDoc2.CreateElement("value");
                    value.InnerText = newValueEdit;
                    XmlElement comment = xmlDoc2.CreateElement("comment");
                    comment.InnerText = "";
                    data.SetAttribute("name", dataname);
                    data.SetAttribute("xml:space", "preserve");
                    data.AppendChild(value);
                    data.AppendChild(comment);
                    xmlDoc2.DocumentElement.AppendChild(data);
                    xmlDoc2.Save(PathResx2);
                }
            }
        }

    }

    public class ResourceViewModel
    {
        public ICollection<ResourceModel> ResourceModels { get; set; }
        public IEnumerable<Language> Languages { get; set; }

        public void PrepareLanguages()
        {
            var unitRepository = DependencyHelper.GetService<IUnitRepository>();
            Languages = unitRepository.GetAllLanguages();
        }
        public void Prepare()
        {
            var unitRepository = DependencyHelper.GetService<IUnitRepository>();
            Languages = unitRepository.GetAllLanguages();
            ResourceModels = new List<ResourceModel>();
            var languageResourceModels = new List<LanguageResourceModel>();
            foreach (var language in Languages)
            {
                var path1 = "";
                var path2 = "";
                if (language.Value.ToLower().Contains("en"))
                {
                    path1 = HttpContext.Current.Server.MapPath("~/App_GlobalResources/Resource.resx");
                    path2 = HttpContext.Current.Server.MapPath("~/App_GlobalResources/MenuResource.resx");
                }
                else
                {
                    path1 = HttpContext.Current.Server.MapPath("~/App_GlobalResources/Resource." + language.Value + ".resx");
                    path2 = HttpContext.Current.Server.MapPath("~/App_GlobalResources/MenuResource." + language.Value + ".resx");
                }
                var res = new LanguageResourceModel(path1, path2, language.Value);
                languageResourceModels.Add(res);
            }

            var languageResourceModel = languageResourceModels.FirstOrDefault(m => m.LanguageCode.ToLower().Contains("en"));
            if (languageResourceModel != null)
                foreach (XmlNode nodeRoot in languageResourceModel.Resources)
                {
                    var resourceModel = new ResourceModel();
                    resourceModel.Code = nodeRoot.Attributes.Item(0).Value;
                    try
                    {

                    //add for english default
                    resourceModel.Resources.Add(new ResourceItemModel
                    {
                        Code = nodeRoot.Attributes.Item(0).Value,
                        Description = nodeRoot.ChildNodes.Item(1).FirstChild.Value,
                        LanguageCode = "en"
                    });

                    }
                    catch (Exception)
                    {
                        //add for english default
                        resourceModel.Resources.Add(new ResourceItemModel
                        {
                            Code = nodeRoot.Attributes.Item(0).Value,
                            Description = nodeRoot.ChildNodes.Item(1).FirstChild.Value,
                            LanguageCode = "en"
                        });
                        throw;
                    }
                    //
                    foreach (var model in languageResourceModels.Where(m => !m.LanguageCode.ToLower().Contains("en")))
                    {
                        var node = model.Resources.FirstOrDefault(m => m.Attributes.Item(0).Value == nodeRoot.Attributes.Item(0).Value);
                        if (node != null)
                        {
                            resourceModel.Resources.Add(new ResourceItemModel
                            {
                                Code = nodeRoot.Attributes.Item(0).Value,
                                Description = node.ChildNodes.Item(1).FirstChild.Value,
                                LanguageCode = model.LanguageCode
                            });
                        }
                        else
                        {
                            resourceModel.Resources.Add(new ResourceItemModel
                            {
                                Code = nodeRoot.Attributes.Item(0).Value,
                                Description = nodeRoot.ChildNodes.Item(1).FirstChild.Value,
                                LanguageCode = model.LanguageCode
                            });
                        }
                    }
                    ResourceModels.Add(resourceModel);
                }

        }


        internal void EditAnyResourceByLanguage(IEnumerable<ResourceItemModel> items, string pathResx1, string pathResx2)
        {
            var xmlDoc1 = new XmlDocument();
            xmlDoc1.Load(pathResx1);
            var xmlDoc2 = new XmlDocument();
            xmlDoc2.Load(pathResx2);
            var changedPath1 = false;
            var changedPath2 = false;
            foreach (var resourceItemModel in items)
            {
                XmlNodeList nodes = xmlDoc1.SelectNodes("//data[@name='" + resourceItemModel.Code + "']");
                if (nodes.Count != 0)
                {
                    var child = nodes.Item(0).ChildNodes.Item(1);
                    if (child.FirstChild.Value != resourceItemModel.Description)
                    {
                        var oldchild = nodes.Item(0).ChildNodes.Item(1);
                        child.FirstChild.Value = resourceItemModel.Description;
                        child.ReplaceChild(child.FirstChild, oldchild.FirstChild);
                        changedPath1 = true;
                    }
                }
                else
                {
                    XmlNodeList nodes2 = xmlDoc2.SelectNodes("//data[@name='" + resourceItemModel.Code + "']");
                    if (nodes2.Count != 0)
                    {
                        var child = nodes2.Item(0).ChildNodes.Item(1);
                        if (child.FirstChild.Value != resourceItemModel.Description)
                        {
                            var oldchild = nodes2.Item(0).ChildNodes.Item(1);
                            child.FirstChild.Value = resourceItemModel.Description;
                            child.ReplaceChild(child.FirstChild, oldchild.FirstChild);
                            changedPath2 = true;
                        }
                    }
                    // add new if not exist
                    else if (resourceItemModel.Code.ToLower().StartsWith("menu"))
                    {
                        XmlElement data = xmlDoc1.CreateElement("data");
                        XmlElement value = xmlDoc1.CreateElement("value");
                        value.InnerText = resourceItemModel.Description;
                        XmlElement comment = xmlDoc1.CreateElement("comment");
                        comment.InnerText = "";
                        data.SetAttribute("name", resourceItemModel.Code);
                        data.SetAttribute("xml:space", "preserve");
                        data.AppendChild(value);
                        data.AppendChild(comment);
                        xmlDoc1.DocumentElement.AppendChild(data);
                        changedPath1 = true;
                    }
                    else
                    {
                        XmlElement data = xmlDoc2.CreateElement("data");
                        XmlElement value = xmlDoc2.CreateElement("value");
                        value.InnerText = resourceItemModel.Description;
                        XmlElement comment = xmlDoc2.CreateElement("comment");
                        comment.InnerText = "";
                        data.SetAttribute("name", resourceItemModel.Code);
                        data.SetAttribute("xml:space", "preserve");
                        data.AppendChild(value);
                        data.AppendChild(comment);
                        xmlDoc2.DocumentElement.AppendChild(data);
                        changedPath2 = true;
                    }
                }
            }
            if (changedPath1)
            {
                xmlDoc1.Save(pathResx1);
            }
            if (changedPath2)
            {
                xmlDoc2.Save(pathResx2);
            }
        }

        internal void EditAnyResourceByLanguage(IEnumerable<ResourceItemModel> items, string pathResx)
        {

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(pathResx);

            var changedPath = false;
            foreach (var resourceItemModel in items)
            {

                {
                    XmlNodeList nodes2 = xmlDoc.SelectNodes("//data[@name='" + resourceItemModel.Code + "']");
                    if (nodes2.Count != 0)
                    {
                        var child = nodes2.Item(0).ChildNodes.Item(1);
                        if (child.FirstChild.Value != resourceItemModel.Description)
                        {
                            var oldchild = nodes2.Item(0).ChildNodes.Item(1);
                            child.FirstChild.Value = resourceItemModel.Description;
                            child.ReplaceChild(child.FirstChild, oldchild.FirstChild);
                            changedPath = true;
                        }
                    }
                    else
                    {
                        XmlElement data = xmlDoc.CreateElement("data");
                        XmlElement value = xmlDoc.CreateElement("value");
                        value.InnerText = resourceItemModel.Description;
                        XmlElement comment = xmlDoc.CreateElement("comment");
                        comment.InnerText = "";
                        data.SetAttribute("name", resourceItemModel.Code);
                        data.SetAttribute("xml:space", "preserve");
                        data.AppendChild(value);
                        data.AppendChild(comment);
                        xmlDoc.DocumentElement.AppendChild(data);
                        changedPath = true;
                    }
                }
            }
            if (changedPath)
            {
                xmlDoc.Save(pathResx);
            }

        }
    }
}