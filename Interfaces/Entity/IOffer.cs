using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Entity
{
  public  interface IOffer
    {
        IEnumerable<OfferDto> Get();
        IEnumerable<OfferDto> Get(string userid);
        OfferDto Get(int id);
        Task<bool> Create(OfferDto obj);
        Task<bool> Update(OfferDto obj);
        Task<bool> Delete(OfferDto obj);
    }
}
