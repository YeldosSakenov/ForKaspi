using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Entity
{
   public interface IAspNetRole
    {
        IEnumerable<AspNetRoleDto> GetAspNetRoles();
        AspNetRoleDto GetAspNetRole(string id);
        Task<bool> Create(AspNetRoleDto aspNetRoleDto);
        Task<bool> Update(AspNetRoleDto aspNetRoleDto);
        Task<bool> Delete(AspNetRoleDto aspNetRoleDto);
    }
}
