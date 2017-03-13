using System;
using System.Data.Entity;
using System.Linq;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class ConcurrentLoginTracker : InMemLoginTracker
    {
        public ConcurrentLoginTracker(DbContext db, int concurrentMax) : base(db)
        {
            ConcurrentMax = concurrentMax;
        }

        public int ConcurrentMax { get; set; }

        protected override bool CanSignIn(string username)
        {
            if (username == null) return false;

            // check if concurrent login of user is reach limit
            if (ConcurrentMax > 0)
                return LoginRecords.Count(r => r.User.DisplayName.ToLower() == username.ToLower()) < ConcurrentMax;

            return base.CanSignIn(username);
        }

        public override void SignIn(string email, string hashKey)
        {
            if (!CanSignIn(email))
                throw new ConcurrentLoginLimitException(email);

            base.SignIn(email, hashKey);
        }
    }

    public class ConcurrentLoginLimitException : Exception
    {
        public ConcurrentLoginLimitException(string username, string message = "Concurrent login limit reached")
            : base(message)
        {
            Username = username;
        }

        public string Username { get; set; }
    }
}
