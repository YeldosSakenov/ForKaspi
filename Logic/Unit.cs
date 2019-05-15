using Abstractions.Interfaces.Entity;
using AutoMapper;
using Core.Dtos;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic
{
    public class Unit : IUnit
    {
        IdeaBankBaseEntities db;
        public Unit()
        {
            db = new IdeaBankBaseEntities();
        }
        public IEnumerable<UnitDto> Get()
        {
            var units = db.Departments
                   .Where(b => b.IsDeleted == false);
            IEnumerable<UnitDto> value = Mapper.Map<IEnumerable<UnitDto>>(units);

            return value;
        }

        public UnitDto Get(int id)
        {
            UnitDto value = Mapper.Map<UnitDto>(db.Departments.Find(id));

            return value;
        }
        public async Task<bool> Create(UnitDto obj)
        {
            try
            {               
               Department value = Mapper.Map<Department>(obj);

                db.Entry(value).State = EntityState.Added;

                db.Departments.Add(value);               

                await db.SaveChangesAsync();              

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(UnitDto obj)
        {
            try
            {
                Department value = Mapper.Map<Department>(obj);

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
        public async Task<bool> Update(UnitDto obj)
        {
            Department value = Mapper.Map<Department>(obj);

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
