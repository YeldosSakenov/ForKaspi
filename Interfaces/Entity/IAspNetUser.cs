using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Abstractions.Interfaces.Entity
{
   public interface IAspNetUser
    {
        IEnumerable<AspNetUserDto> GetAspNetUsers();
        Task<bool> Delete(AspNetUserDto aspNetUserDto);
    }
}
