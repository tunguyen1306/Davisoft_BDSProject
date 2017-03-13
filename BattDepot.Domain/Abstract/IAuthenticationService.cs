using System.Collections.Generic;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IAuthenticationService
    {
        User ValidateLogin(string email, string password);
    }
}