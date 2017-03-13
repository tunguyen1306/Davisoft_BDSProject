using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using NS.Entity;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Enum;
using Davisoft_BDSProject.Domain.Helpers;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFMembershipService : Repository, IMembershipService
    {
        public EFMembershipService(DbContext db)
            : base(db)
        {
        }

        #region IMembershipService Members

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return GetAll(predicate, u => new
            {
                Roles = u.Roles.Select(r => r.Permissions),
                u.Branch,
                u.Language
            }).OrderBy(u => u.DisplayName)
                .AsEnumerable();
        }
        public IEnumerable<User> GetUsers()
        {
            return GetAll<User>(u => new
                                     {
                                         Roles = u.Roles.Select(r => r.Permissions),
                                         u.Branch,
                                         u.Language
                                     })
                .OrderBy(u => u.DisplayName)
                .AsEnumerable();
        }
        public IEnumerable<User> GetUsersDeleted()
        {
            return GetAll<User>(u => new
            {
                Roles = u.Roles.Select(r => r.Permissions),
                u.Branch
            })
                .OrderBy(u => u.DisplayName)
                .AsEnumerable();
        }


        public bool UpdateUser(User userInfo)
        {
            //var user = Get<User>(userInfo.ID);
            //if (user == null) return false;

            //user.DisplayName = userInfo.DisplayName;
            //user.Email = userInfo.Email;
            //user.Phone = userInfo.Phone;
            //user.Picture = userInfo.Picture;
            //user.BranchID = userInfo.BranchID;
            //user.MobilePhone = userInfo.MobilePhone;
            //user.Active = userInfo.Active;
            //user.SalesmanType = userInfo.SalesmanType;
            //user.IncentiveDate = userInfo.IncentiveDate;
            ////user.Language = null;
            ////user.LanguageID = userInfo.LanguageID;
            //if (!string.IsNullOrEmpty(userInfo.Password))
            //{
            //    user.Password = EncryptHelper.EncryptPassword(userInfo.Password);
            //}
            //else
            //{
            //    DbPropertyEntry<User, string> property = _db.Entry(user).Property(u => u.Password);
            //    property.CurrentValue = property.OriginalValue;
            //}
            return Update(userInfo);
        }

        public void UpdateUserLanguage(int id, int languageID)
        {
            User user = GetUser(id);
            if (user == null) return;
            if (user.LanguageID != languageID)
            {
                user.LanguageID = languageID;
                user.Language = null;
                Update(user);
                //_db.SaveChanges();
            }
        }

        public void UpdateUserPicture(int id, string fileName)
        {
            User user = GetUser(id);
            if (user == null) return;

            user.Picture = fileName;
            Update(user);
            //_db.SaveChanges();
        }

        public virtual User GetUser(int id)
        {
            return Get<User>(u => u.ID == id,
                             u => u.Roles.Select(r => r.Permissions),
                             u => u.Branch,
                             u => u.Branches,
                             u => u.Language);
        }

        public User GetUserByName(string username)
        {
            return string.IsNullOrWhiteSpace(username)
                       ? null
                       : Get<User>(u => u.DisplayName != null &&
                                        u.DisplayName.ToLower() == username.ToLower(),
                                   u => u.Roles.Select(r => r.Permissions),
                                   u => u.Branch,
                                   u => u.Branches,
                                   u => u.Language);
        }
        public User GetUserByEmail(string email)
        {
            return string.IsNullOrWhiteSpace(email)
                       ? null
                       : Get<User>(u => u.Email != null &&
                                        u.Email.Trim().ToLower() == email.Trim().ToLower(),
                                   u => u.Roles.Select(r => r.Permissions),
                                   u => u.Branch,
                                   u => u.Branches,
                                   u => u.Language);
        }
        public IEnumerable<User> GetAllUserByEmail(string email)
        {
            return GetAll<User>(m => m.Email.Trim().ToLower() == email.Trim().ToLower()).ToList();
        }
        public User ValidateLogin(string email, string password)
        {
            password = EncryptHelper.EncryptPassword(password);
            return Get<User>(u => u.Email != null &&
                                  u.Email.ToLower() == email.ToLower() &&
                                  u.Password == password && u.Status == EntityStatus.Normal);
        }

        public User CreateUser(User user)
        {
            user.Email = user.Email.Trim();
            user.Password = EncryptHelper.EncryptPassword(user.Password);
            return Create(user);
        }

        public bool DeleteUser(int id)
        {
            var user = GetUser(id);
            user.Status = EntityStatus.Deleted;
            return Update(user);
        }
        public IEnumerable<User> GetAllUser()
        {
            return GetAll<User>();
        }

        public IEnumerable<User> GetAllUser(Func<User, bool> predicate)
        {
            return GetAll<User>(predicate);
        }

        public bool ReActiveUser(int id)
        {
            var user = GetUser(id);
            user.Status = EntityStatus.Normal;
            return Update(user);
        }
        public IEnumerable<User> GetUsersInRole(params string[] roleName)
        {
            return GetAll<User>(u => u.Roles
                                      .Select(r => r.Name.ToUpper())
                                      .Intersect(roleName.Select(r1 => r1.ToUpper()))
                                      .Any(),
                                u => u.Roles.Select(r => r.Permissions)).AsEnumerable();
        }

        public IEnumerable<User> GetUsersContainsInRole(params string[] roleName)
        {
            return GetAll<User>(u => u.Roles
                                      .Select(r => r.Name.ToUpper())
                                      .Intersect(roleName.Select(r1 => r1.ToUpper()))
                                      .Any(),
                                u => u.Roles.Select(r => r.Permissions)).AsEnumerable();
        }
        public IEnumerable<User> GetAllUserOfEmailTemplate(string type, string nameTemplate)
        {
            var usersResult = new List<User>();
            if (string.IsNullOrEmpty(nameTemplate) || string.IsNullOrEmpty(type))
            {
                return usersResult;
            }
            var receiveTemplate = Get<EmailReceiveSetting>(e => e.TemplateName.ToUpper().Trim() == nameTemplate.ToUpper().Trim() && e.Type.ToUpper().Trim() == type.ToUpper().Trim());
            if (receiveTemplate != null)
            {
                foreach (var role in receiveTemplate.ReceiveRoles)
                {
                    var usersByRole = GetAll<User>(m => m.Status == EntityStatus.Normal && m.Roles.Select(r => r.ID).Contains(role.RoleID));
                    if (usersByRole.Any())
                    {
                        usersResult.AddRange(usersByRole);
                    }
                }
                if (receiveTemplate.ReceiveUsers.Any())
                    usersResult.AddRange(receiveTemplate.ReceiveUsers.Where(m => m.User.Status == EntityStatus.Normal).Select(m => m.User));
            }
            return usersResult.DistinctBy(m => m.ID);
        }
        #endregion
    }
}
