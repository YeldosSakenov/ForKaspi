using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
  public  class CategoryDto
    {
        public int Id { get; set; }
        [Display(Name = nameof(Resources.Resources.CategoryName), ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDateTime { get; set; }
    }
}
