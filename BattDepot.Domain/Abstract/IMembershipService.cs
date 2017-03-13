using System;
using System.Collections.Generic;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IMembershipService : IAuthenticationService
    {
        IEnumerable<User> GetUsers(Func<User, bool> predicate);
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsersDeleted();
        User GetUser(int id);
        User GetUserByName(string username);
        User GetUserByEmail(string email);
        User CreateUser(User userInfo);
        bool UpdateUser(User userInfo);
        void UpdateUserLanguage(int id, int languageID);
        void UpdateUserPicture(int id, string fileName);
        bool DeleteUser(int id);
        IEnumerable<User> GetUsersInRole(params string[] roleName);
        IEnumerable<User> GetUsersContainsInRole(params string[] roleName);
        IEnumerable<User> GetAllUserOfEmailTemplate(string type, string nameTemplate);
        //Dealer CreateDealer(Dealer user, int countryID);
        //Dealer GetDealer(string dealercode);
        //Dealer GetDealer(int id);
        //bool UpdateDealer(Dealer item);
        bool ReActiveUser(int id);
        IEnumerable<User> GetAllUserByEmail(string email);
        IEnumerable<User> GetAllUser();
        IEnumerable<User> GetAllUser(Func<User, bool> predicate);
    }
    public interface IMembershipServiceBase
    {
        IEnumerable<User> GetAllUsers(Func<User, bool> predicate);
    }
}
