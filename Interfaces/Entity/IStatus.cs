using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Entity
{
   public interface IStatus
    {
        IEnumerable<StatusDto> Get();
        StatusDto Get(int id);
        Task<bool> Create(StatusDto statusDto);
        Task<bool> Update(StatusDto statusDto);
        Task<bool> Delete(StatusDto statusDto);
    }
}
