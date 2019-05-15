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
    public class Status : IStatus
    {
        IdeaBankBaseEntities db;
        public Status()
        {
            db = new IdeaBankBaseEntities();
        }
        public IEnumerable<StatusDto> Get()
        {
            var status = db.Statuses
                    .Where(b => b.IsDeleted == false);
            IEnumerable<StatusDto> value = Mapper.Map<IEnumerable<StatusDto>>(status);

            return value;
        }

        public StatusDto Get(int id)
        {
            StatusDto value = Mapper.Map<StatusDto>(db.Statuses.Find(id));

            return value;
        }
        public async Task<bool> Create(StatusDto statusDto)
        {
            try
            {
                DataAccess.Models.Status value = Mapper.Map<DataAccess.Models.Status>(statusDto);

                db.Entry(value).State = EntityState.Added;

                db.Statuses.Add(value);

                await db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(StatusDto statusDto)
        {
            try
            {
                DataAccess.Models.Status value = Mapper.Map<DataAccess.Models.Status>(statusDto);

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

     

        public async Task<bool> Update(StatusDto statusDto)
        {
            DataAccess.Models.Status value = Mapper.Map<DataAccess.Models.Status>(statusDto);

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
