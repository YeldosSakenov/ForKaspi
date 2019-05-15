using Abstractions.Interfaces.Entity;
using AutoMapper;
using Core.Dtos;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Domain.Logic
{
    public class AspNetRole : IAspNetRole
    {
        private IdeaBankBaseEntities db;
        public AspNetRole()
        {
            db = new IdeaBankBaseEntities();
        }


        public IEnumerable<AspNetRoleDto> GetAspNetRoles()
        {
            IEnumerable<AspNetRoleDto> roles = Mapper.Map<IEnumerable<AspNetRoleDto>>(db.AspNetRoles);

            return roles;
        }
        public AspNetRoleDto GetAspNetRole(string id)
        {
            AspNetRoleDto roles = Mapper.Map<AspNetRoleDto>(db.AspNetRoles.Find(id));

            return roles;
        }
        public async Task<bool> Create(AspNetRoleDto aspNetRoleDto)
        {
            try
            {
                aspNetRoleDto.Id = Guid.NewGuid().ToString();
                DataAccess.Models.AspNetRole roles = Mapper.Map<DataAccess.Models.AspNetRole>(aspNetRoleDto);

                //db.Entry(aspNetRoleDto).State = EntityState.Added;
                db.AspNetRoles.Add(roles);
                await db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(AspNetRoleDto role)
        {          
            DataAccess.Models.AspNetRole roles = Mapper.Map<DataAccess.Models.AspNetRole>(role);

            try
            {
                db.Entry(roles).State = EntityState.Modified;

                await db.SaveChangesAsync();

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Delete(AspNetRoleDto aspNetRoleDto)
        {
            try
            {              
                DataAccess.Models.AspNetRole roles = Mapper.Map<DataAccess.Models.AspNetRole>(aspNetRoleDto);

                db.Entry(roles).State = EntityState.Deleted;

                db.AspNetRoles.Remove(roles);

                await db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

      
    }
}
