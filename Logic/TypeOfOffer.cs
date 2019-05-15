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
    public class TypeOfOffer : ITypeOfOffer
    {
        IdeaBankBaseEntities db;
        public TypeOfOffer()
        {
            db = new IdeaBankBaseEntities();
        }

        public IEnumerable<TypeOfOfferDto> Get()
        {
            var typeOfOffers = db.TypeOfOffers
                      .Where(b => b.IsDeleted == false);
            IEnumerable<TypeOfOfferDto> value = Mapper.Map<IEnumerable<TypeOfOfferDto>>(typeOfOffers);

            return value;
        }

        public TypeOfOfferDto Get(int id)
        {
            TypeOfOfferDto value = Mapper.Map<TypeOfOfferDto>(db.TypeOfOffers.Find(id));

            return value;
        }
        public async Task<bool> Create(TypeOfOfferDto typeOfOfferDto)
        {
            try
            {              
                DataAccess.Models.TypeOfOffer value = Mapper.Map<DataAccess.Models.TypeOfOffer>(typeOfOfferDto);

                db.Entry(value).State = EntityState.Added;

                db.TypeOfOffers.Add(value);

                await db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(TypeOfOfferDto typeOfOfferDto)
        {
            try
            {
                DataAccess.Models.TypeOfOffer value = Mapper.Map<DataAccess.Models.TypeOfOffer>(typeOfOfferDto);

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

        public async Task<bool> Update(TypeOfOfferDto typeOfOfferDto)
        {
            DataAccess.Models.TypeOfOffer value = Mapper.Map<DataAccess.Models.TypeOfOffer>(typeOfOfferDto);

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
