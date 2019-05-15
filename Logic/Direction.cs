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
    public class Direction:IDirection
    {
        IdeaBankBaseEntities db;
        public Direction()
        {
            db = new IdeaBankBaseEntities();
        }
        public IEnumerable<DirectionDto> Get()
        {
            var direction = db.Directions
                     .Where(b => b.IsDeleted == false);
            IEnumerable<DirectionDto> value = Mapper.Map<IEnumerable<DirectionDto>>(direction);

            return value;
        }

        public DirectionDto Get(int id)
        {
            DirectionDto value = Mapper.Map<DirectionDto>(db.Directions.Find(id));

            return value;
        }
        public async Task<bool> Create(DirectionDto directionDto)
        {
            try
            {
                DataAccess.Models.Direction value = Mapper.Map<DataAccess.Models.Direction>(directionDto);

                db.Entry(value).State = EntityState.Added;

                db.Directions.Add(value);

                await db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(DirectionDto directionDto)
        {
            try
            {
                DataAccess.Models.Direction value = Mapper.Map<DataAccess.Models.Direction>(directionDto);

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



        public async Task<bool> Update(DirectionDto directionDto)
        {
            DataAccess.Models.Direction value = Mapper.Map<DataAccess.Models.Direction>(directionDto);

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
