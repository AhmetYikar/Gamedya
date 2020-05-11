using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace GameAdmin.Helper
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetFullName(this IPrincipal user)
        {
            var fullNameClaim = ((ClaimsIdentity)user.Identity).FindFirst("FullName");
            if (fullNameClaim != null)
                return fullNameClaim.Value;
            return "";
        }

        public static string GetUserImage(this IPrincipal user)
        {
            var ImageClaim = ((ClaimsIdentity)user.Identity).FindFirst("Image");
            if (ImageClaim != null)
                return ImageClaim.Value;
            return "";
        }

        public static string GetUserId(this IPrincipal user)
        {
            var IdClaim = ((ClaimsIdentity)user.Identity).FindFirst("Id");
            if (IdClaim != null)
                return IdClaim.Value;
            return "";
        }


    }

}
