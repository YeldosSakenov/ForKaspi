using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{

    public class AspNetUserDto
    {
        public string Id { get; set; }
        [Display(Name = nameof(Resources.Resources.Email), ResourceType = typeof(Resources.Resources))]
        public string Email { get; set; }
        [Display(Name = nameof(Resources.Resources.EmailConfirmed), ResourceType = typeof(Resources.Resources))]
        public bool EmailConfirmed { get; set; }
        [Display(Name = nameof(Resources.Resources.PasswordHash), ResourceType = typeof(Resources.Resources))]
        public string PasswordHash { get; set; }
        [Display(Name = nameof(Resources.Resources.SecurityStamp), ResourceType = typeof(Resources.Resources))]
        public string SecurityStamp { get; set; }
        [Display(Name = nameof(Resources.Resources.PhoneNumber), ResourceType = typeof(Resources.Resources))]
        public string PhoneNumber { get; set; }
        [Display(Name = nameof(Resources.Resources.PhoneNumberConfirmed), ResourceType = typeof(Resources.Resources))]
        public bool PhoneNumberConfirmed { get; set; }
        [Display(Name = nameof(Resources.Resources.TwoFactorEnabled), ResourceType = typeof(Resources.Resources))]
        public bool TwoFactorEnabled { get; set; }
        [Display(Name = nameof(Resources.Resources.LockoutEndDateUtc), ResourceType = typeof(Resources.Resources))]
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        [Display(Name = nameof(Resources.Resources.LockoutEnabled), ResourceType = typeof(Resources.Resources))]
        public bool LockoutEnabled { get; set; }
        [Display(Name = nameof(Resources.Resources.AccessFailedCount), ResourceType = typeof(Resources.Resources))]
        public int AccessFailedCount { get; set; }
        [Display(Name = nameof(Resources.Resources.UserName), ResourceType = typeof(Resources.Resources))]
        public string UserName { get; set; }


        //public virtual ICollection<ApprovedDto> Approveds { get; set; }

        //public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }

        //public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }

        public virtual ICollection<AspNetRoleDto> AspNetRoles { get; set; }

        //public virtual ICollection<OfferDto> Offers { get; set; }

    }
}
