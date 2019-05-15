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
    public class Offer : IOffer
    {
        IdeaBankBaseEntities db;
        public Offer()
        {
            db = new IdeaBankBaseEntities();
        }
        public IEnumerable<OfferDto> Get()
        {
            
            var list = db.Offers
                     .Where(b => b.IsDeleted == false);

            IEnumerable<OfferDto> value = Mapper.Map<IEnumerable<OfferDto>>(list);

            return value;
        }
        public IEnumerable<OfferDto> Get(string userId)
        {
            var user = db.AspNetUsers.ToList().Where(u => u.Email == userId).First().Id;
            var list = db.Offers
                     .Where(b => b.IsDeleted == false&& b.UserId== user);

            IEnumerable<OfferDto> value = Mapper.Map<IEnumerable<OfferDto>>(list);

            return value;
        }

        public OfferDto Get(int id)
        {
            //db.Configuration.ProxyCreationEnabled = false;
            var getFirstVersion = db.Offers.Find(id);
            //var get = db.Offers.Include(i=>i.Files).AsNoTracking().Where(f => f.Id == id).First();

            OfferDto value = Mapper.Map<OfferDto>(getFirstVersion);

            return value;
        }
        public async Task<bool> Create(OfferDto obj)
        {
            try
            {
                DataAccess.Models.Offer value = Mapper.Map<DataAccess.Models.Offer>(obj);

                value.DatePublication = DateTime.Now;

                db.Entry(value).State = EntityState.Added;

                db.Offers.Add(value);

                await db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(OfferDto obj)
        {
            try
            {
                DataAccess.Models.Offer value = Mapper.Map<DataAccess.Models.Offer>(obj);

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

        public async Task<bool> Update(OfferDto obj)
        {
            DataAccess.Models.Offer value = Mapper.Map<DataAccess.Models.Offer>(obj);

            try
            {
                db.Entry(value).State = EntityState.Modified;

                foreach (var files in db.Files)
                {
                    db.Entry(files).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)  { return false; }
        }
    }
}
