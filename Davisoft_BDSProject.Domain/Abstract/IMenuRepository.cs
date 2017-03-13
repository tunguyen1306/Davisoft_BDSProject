using System.Collections.Generic;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IMenuRepository
    {
        IEnumerable<Menu> GetAll();
        IEnumerable<Menu> GetAllChildren(int parentId = 0);
        IEnumerable<Menu> GetAllChildrenByUser(int userID, int parentID = 0);
        Menu Get(int id);
        Menu Create(Menu info, int[] menuRoles);
        bool Update(Menu menu, int[] menuRoles);
        bool Delete(int id);
    }
}
