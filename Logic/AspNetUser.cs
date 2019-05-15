

using Abstractions.Interfaces.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Logic
{
    public class AspNetUser : IAspNetUser
    {
        IdeaBankBaseEntities db = new IdeaBankBaseEntities();

        public IEnumerable<AspNetUserDto> GetAspNetUsers()
        {
            var users = db.AspNetUsers.ToList();

            IEnumerable<AspNetUserDto> user = Mapper.Map<IEnumerable<AspNetUserDto>>(users);

            return user;
        }
        public async Task<bool> Delete(AspNetUserDto aspNetUserDto)
        {
            try
            {
                DataAccess.Models.AspNetUser value = Mapper.Map<DataAccess.Models.AspNetUser>(aspNetUserDto);

                db.Entry(value).State = EntityState.Deleted;               

                await db.SaveChangesAsync();

                return true;
            }
            catch { return false;}
        }
    }
}
