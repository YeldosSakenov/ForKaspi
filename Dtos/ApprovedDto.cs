using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
   public class ApprovedDto
    {
        public int OfferId { get; set; }
        public string UserId { get; set; }
        public int StatusId { get; set; }
        [Display(Name = nameof(Resources.Resources.OfferName), ResourceType = typeof(Resources.Resources))]
        public string OfferName { get; set; }
        [Display(Name = nameof(Resources.Resources.UserName), ResourceType = typeof(Resources.Resources))]
        public string UserName { get; set; }
        [Display(Name = nameof(Resources.Resources.StatusName), ResourceType = typeof(Resources.Resources))]
        public string StatusName { get; set; }
        [Display(Name = nameof(Resources.Resources.DateApproved), ResourceType = typeof(Resources.Resources))]
        public DateTime DateApproved { get; set; }
    }
}
