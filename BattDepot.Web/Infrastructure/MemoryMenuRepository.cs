using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Concrete;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Web.Infrastructure
{
    public class MemoryMenuRepository : EFMenuRepository
    {
        private List<Menu> _menus = new List<Menu>();

        #region Factory

        public MemoryMenuRepository(DbContext db)
            : base(db)
        {
            RetrieveMenus();
        }

        #endregion

        #region Decorator Properties

        public EFMenuRepository MenuRepository
        {
            get { return DependencyHelper.GetService<EFMenuRepository>(); }
        }

        public IMembershipService MembershipService
        {
            get { return DependencyHelper.GetService<IMembershipService>(); }
        }

        #endregion

        private void RetrieveMenus()
        {
            _menus.Clear();
            _menus.AddRange(MenuRepository.GetAll().OrderBy(m => m.Order).ToList());
        }

        public override IEnumerable<Menu> GetAllChildren(int parentId = 0)
        {
            return _menus.Where(m => m.ParentID == parentId).OrderBy(m => m.Order).AsEnumerable();
        }

        public override IEnumerable<Menu> GetAllChildrenByUser(int userID, int parentID = 0)
        {
            User user = MembershipService.GetUser(userID);
            if (user == null || user.Roles.Count == 0)
                return null;

            List<int> userRoles = user.Roles.Select(r => r.ID).ToList();

            // Admin
            if (userRoles.Contains(1))
                return GetAllChildren(parentID);

            return _menus.Where(m => m.ParentID == parentID &&
                                     m.Roles.Select(r => r.ID).Intersect(userRoles).Any())
                         .OrderBy(m => m.Order)
                         .AsEnumerable();
        }

        public override Menu Get(int id)
        {
            return _menus.FirstOrDefault(m => m.ID == id);
        }

        public override Menu Create(Menu info, int[] menuRoles)
        {
            Menu menu = MenuRepository.Create(info, menuRoles);
            RetrieveMenus();
            return menu;
        }

        public override bool Update(Menu menu, int[] menuRoles)
        {
            bool result = MenuRepository.Update(menu, menuRoles);
            RetrieveMenus();
            return result;
        }

        public override bool Delete(int id)
        {
            _menus.Remove(Get(id));
            return MenuRepository.Delete(id);
        }
    }
}
