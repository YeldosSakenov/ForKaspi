using Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Resources;
using System.Web;


namespace Core.Dtos
{
    public class OfferDto
    {
        public int Id { get; set; }
        [Display(Name = nameof(Resources.Resources.Title), ResourceType = typeof(Resources.Resources))]
        public string Title { get; set; }
        [Display(Name = nameof(Resources.Resources.Note), ResourceType = typeof(Resources.Resources))]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }
        [Display(Name = nameof(Resources.Resources.Coauthor), ResourceType = typeof(Resources.Resources))]
        public string Coauthor { get; set; }
        [Display(Name = nameof(Resources.Resources.DatePublication), ResourceType = typeof(Resources.Resources))]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime DatePublication { get; set; }
        [Display(Name = nameof(Resources.Resources.IsShow), ResourceType = typeof(Resources.Resources))]
        public bool IsShow { get; set; }
        [Display(Name = nameof(Resources.Resources.IsRead), ResourceType = typeof(Resources.Resources))]
        public bool IsRead { get; set; }
      
        public int OfferId { get; set; }      
        public int ProblemsId { get; set; }      
        public Nullable<int> StatusesId { get; set; }       
        public string UserId { get; set; }       
        public int DirectionId { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDateTime { get; set; }

        //public virtual ICollection<ApprovedDto> Approveds { get; set; }
        public virtual DirectionDto Direction { get; set; }

        public virtual ICollection<FileDto> Files { get; set; }
        public virtual ProblemDto Problem { get; set; }
        public virtual StatusDto Status { get; set; }
        public virtual TypeOfOfferDto TypeOfOffer { get; set; }
        [Display(Name = nameof(Resources.Resources.DirectionName), ResourceType = typeof(Resources.Resources))]
        [NotMapped]
        public string DirectionName { get; set; }
        [Display(Name = nameof(Resources.Resources.ProblemName), ResourceType = typeof(Resources.Resources))]
        [NotMapped]
        public string ProblemName { get; set; }
        [Display(Name = nameof(Resources.Resources.StatusName), ResourceType = typeof(Resources.Resources))]
        [NotMapped]
        public string StatusName { get; set; }
        [Display(Name = nameof(Resources.Resources.TypeOfOfferName), ResourceType = typeof(Resources.Resources))]
        [NotMapped]
        public string TypeOfOfferName { get; set; }
        [Display(Name = nameof(Resources.Resources.FileContent), ResourceType = typeof(Resources.Resources))]
        [NotMapped]
        public IEnumerable<HttpPostedFileBase> FileContent { get; set; }
    }
}
