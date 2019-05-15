using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class StatusDto
    {
        public int Id { get; set; }
        [Display(Name = nameof(Resources.Resources.StatusName), ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDateTime { get; set; }
    }
}
