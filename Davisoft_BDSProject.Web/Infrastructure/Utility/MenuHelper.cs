using System.Collections.Generic;
using System.Web.Mvc;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Web.Infrastructure.Utility
{
    public class MenuHelper
    {
        #region Repository

        private static IMenuRepository MenuRepository
        {
            get { return DependencyHelper.GetService<IMenuRepository>(); }
        }

        #endregion

        public static IEnumerable<Menu> GetAll(int parentId = 0)
        {
            if (!CurrentUser.IsAuthenticated)
                return new Menu[] { };

            return MenuRepository.GetAllChildrenByUser(CurrentUser.Identity.ID,parentId);
        }
    }
}
