using System;
using System.Collections.Generic;
using System.Linq;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class Menu : EntityBase
    {
        public Menu()
        {
            Roles = new HashSet<Role>();
        }

        public string Title { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public int ParentID { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        //public string Controller
        //{
        //    get
        //    {
        //        var items = Url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        //        return items.Any() ? items[0] : null;
        //    }
        //}
        //public string Action
        //{
        //    get
        //    {
        //        var items = Url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        //        return items.Count() > 1 ? items[1] : "Index";
        //    }
        //}
    }
}
