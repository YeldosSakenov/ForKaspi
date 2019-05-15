using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Entity
{
   public interface IAspNetUserRoles
    {
        Task<IEnumerable<AspNetUserRolesDto>> Get(AspNetUserRolesDto approvedDto);
        Task<bool> Create(AspNetUserRolesDto aspNetUserRolesDto);
        Task<bool> Delete(AspNetUserRolesDto aspNetUserRolesDto);
    }
}
