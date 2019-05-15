using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
  public  class TypeOfOfferDto
    {
        public int Id { get; set; }
        [Display(Name = nameof(Resources.Resources.TypeOfOfferName), ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDateTime { get; set; }
    }
}
