using Abstractions.Interfaces.Entity;
using AutoMapper;
using Core.Dtos;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Logic
{
    public class Category : ICategory
    {
        IdeaBankBaseEntities db;
        public Category()
        {
            db = new IdeaBankBaseEntities();
        }
        public IEnumerable<CategoryDto> Get()
        {
            var categories = db.Categories
                     .Where(b => b.IsDeleted == false);
            IEnumerable<CategoryDto> value = Mapper.Map<IEnumerable<CategoryDto>>(categories);

            return  value;
        }

        public CategoryDto Get(int id)
        {
            CategoryDto value = Mapper.Map<CategoryDto>(db.Categories.Find(id));

            return value;
        }
        public async Task<bool> Create(CategoryDto categoryDto)
        {
            try
            {
                DataAccess.Models.Category value = Mapper.Map<DataAccess.Models.Category>(categoryDto);

                db.Entry(value).State = EntityState.Added;

                db.Categories.Add(value);

                await db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(CategoryDto categoryDto)
        {
            try
            {
                DataAccess.Models.Category value = Mapper.Map<DataAccess.Models.Category>(categoryDto);

                value.DeletedDateTime = DateTime.Now;
                value.IsDeleted = true;

                db.Entry(value).State = EntityState.Modified;

                await db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

       

        public async Task<bool> Update(CategoryDto categoryDto)
        {
            DataAccess.Models.Category value = Mapper.Map<DataAccess.Models.Category>(categoryDto);

            try
            {
                db.Entry(value).State = EntityState.Modified;

                await db.SaveChangesAsync();

                return true;
            }
            catch { return false; }
        }
    }
}
