using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Entity
{
  public  interface ICategory
    {
        IEnumerable<CategoryDto> Get();
        CategoryDto Get(int id);
        Task<bool> Create(CategoryDto categoryDto);
        Task<bool> Update(CategoryDto categoryDto);
        Task<bool> Delete(CategoryDto categoryDto);
    }
}
