using System;

namespace Davisoft_BDSProject.Web.Infrastructure.Utility
{
    class ApplyAuthorizeAttribute : Attribute
    {
        /// <summary>
        /// <para>Config the method to apply authority to authorize.</para>
        /// <para>Example: [Infrastructure.Utility.ApplyAuthorize(true)]</para> 
        /// <para>Design by ...</para>
        /// </summary>
        /// <param name="isAuthorize">parameter use to setup</param>
        public ApplyAuthorizeAttribute(bool isAuthorize)
        {
            IsAuthorize = isAuthorize;
        }

        public bool IsAuthorize { get; set; }
    }
}