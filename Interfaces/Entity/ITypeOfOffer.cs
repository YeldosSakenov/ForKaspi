using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Entity
{
   public interface ITypeOfOffer
    {
        IEnumerable<TypeOfOfferDto> Get();
        TypeOfOfferDto Get(int id);
        Task<bool> Create(TypeOfOfferDto typeOfOfferDto);
        Task<bool> Update(TypeOfOfferDto typeOfOfferDto);
        Task<bool> Delete(TypeOfOfferDto typeOfOfferDto);
    }
}
