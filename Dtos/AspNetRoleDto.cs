using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class AspNetRoleDto
    {
        public string Id { get; set; }
        [Display(Name = nameof(Resources.Resources.RoleName), ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
    }
}
