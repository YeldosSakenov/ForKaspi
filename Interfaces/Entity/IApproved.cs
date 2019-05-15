using Core.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Entity
{
    public interface IApproved
    {
        Task<IEnumerable<ApprovedDto>> Get(ApprovedDto approvedDto);
        Task<bool> Create(ApprovedDto approvedDto);
        Task<bool> Delete(ApprovedDto approvedDto);
        Task<bool> Update(ApprovedDto approvedDto);
    }
}
