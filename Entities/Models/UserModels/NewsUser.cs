using Entites.Models.BlogModels;
using Entites.Models.ForumModels;
using Entites.Models.GameModels;
using Entites.Models.MessageModels;
using Entites.Models.NewsModels;
using Entities.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models.UserModels
{
    public class NewsUser : IdentityUser
    {

        /// <summary>
        /// get in user full name
        /// </summary>
        [Display(Name = "Tam Adı")]
        public string FullName { get; set; }
        [Display(Name = "Profil Foto")]
        public string Image { get; set; }



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<NewsUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            userIdentity.AddClaim(new Claim("FullName", this.FullName));
            if (this.Image != null)
            {
                userIdentity.AddClaim(new Claim("Image", this.Image));
            }
            userIdentity.AddClaim(new Claim("Id", this.Id));

            return userIdentity;
        }

        public ICollection<BlogPost> BlogPosts { get; set; }
        public ICollection<BlogComment> BlogComments { get; set; }
        public ICollection<ForumPost> ForumPosts { get; set; }
        public ICollection<ForumReply> ForumReplies { get; set; }
        public ICollection<NewsComment> NewsComments { get; set; }
        public ICollection<Game> MyFavouriteGames { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<MessageRecipient> MessageRecipients { get; set; }
        public ICollection<Notification> Notifications { get; set; }




    }
}
