using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Entity
{ 
  public  interface IUnit
    {
        IEnumerable<UnitDto> Get();
        UnitDto Get(int id);
        Task<bool> Create(UnitDto obj);
        Task<bool> Update(UnitDto obj);
        Task<bool> Delete(UnitDto obj);
    }
}
