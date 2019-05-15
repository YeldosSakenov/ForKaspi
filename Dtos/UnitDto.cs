using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class UnitDto
    {

        public int Id { get; set; }
        [Display(Name = nameof(Resources.Resources.UnitName), ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedDateTime { get; set; }
        public virtual ICollection<ProblemDto> Problems { get; set; }
    }
}
