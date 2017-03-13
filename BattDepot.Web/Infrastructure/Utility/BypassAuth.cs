using System;
using System.Web.Mvc;
using Davisoft_BDSProject.Domain.Helpers.Encryption;
using UrlHelper = Davisoft_BDSProject.Web.Infrastructure.Helpers.UrlHelper;

namespace Davisoft_BDSProject.Web.Infrastructure.Utility
{
    public static class BypassAuth
    {
        private static IStringEncryptor Encryptor
        {
            get { return DependencyResolver.Current.GetService<IStringEncryptor>(); }
        }

        public static string Encrypt(string url, string username)
        {
            string data = url + "::" + username;
            string token = Encryptor.Encrypt(data);

            return UrlHelper.Absolute("/n/" + token);
        }

        public static string Decrypt(string token)
        {
            string data = Encryptor.Decrypt(token);

            string[] segments = data.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
            string url = segments[0];
            string username = segments[1];

            LoginPersister.SignIn(username);

            return url;
        }
    }
}
