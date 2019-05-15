using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Entity
{
   public interface IDirection
    {
        IEnumerable<DirectionDto> Get();
        DirectionDto Get(int id);
        Task<bool> Create(DirectionDto directionDto);
        Task<bool> Update(DirectionDto directionDto);
        Task<bool> Delete(DirectionDto directionDto);
    }
}
