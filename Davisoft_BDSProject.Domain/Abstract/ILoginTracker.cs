using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface ILoginTracker
    {
        void SignIn(string email, string hashKey);
        void SignOut(string username, string hashKey = null);
        bool IsLogged(string username);

        /// <summary>
        /// Retrieve user from memory
        /// </summary>
        /// <param name="hashKey"></param>
        /// <returns></returns>
        User RetrieveUser(string hashKey);

        /// <summary>
        /// Update new info for all records associated with old username
        /// </summary>
        /// <param name="oldEmail"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        User ReloadUser(string oldEmail, User info);
    }
}
