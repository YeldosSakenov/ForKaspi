using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Dtos
{
    public  class ProblemDto
    {
        public int Id { get; set; }
        [Display(Name = nameof(Resources.Resources.ProblemName), ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDateTime { get; set; }
        public virtual ICollection<OfferDto> Offers { get; set; }        
        public virtual ICollection<UnitDto> Departments { get; set; }

        [NotMapped]
        [Display(Name = nameof(Resources.Resources.CategoryName), ResourceType = typeof(Resources.Resources))]
        public string CategoryName { get; set; }
        [NotMapped]
        public int DepartmentId { get; set; }
    }
}
