using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Entity
{
  public  interface IProblem
    {
        IEnumerable<ProblemDto> Get();
        ProblemDto Get(int id);
        Task<bool> Create(ProblemDto problemDto);
        Task<bool> CreateReletionUnit(ProblemDto problemDto);
        Task<bool> Update(ProblemDto problemDto);
        Task<bool> Delete(ProblemDto problemDto);
    }
}
