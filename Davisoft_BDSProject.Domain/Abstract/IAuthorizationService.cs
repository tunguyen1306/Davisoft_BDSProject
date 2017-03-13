namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IAuthorizationService
    {
        bool CheckAccess(int userID, string feature, string permissionType = null);
    }
}
