using Abstractions.Interfaces.Entity;
using AutoMapper;
using Core.Dtos;
using DataAccess.Ado.Models.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Logic
{
    public class AspNetUserRoles: IAspNetUserRoles
    {
        AspNetUserRolesDb db;
        public AspNetUserRoles()
        {
            db = new AspNetUserRolesDb();
        }
        public async Task<IEnumerable<AspNetUserRolesDto>> Get(AspNetUserRolesDto approvedDto)
        {
            var approved = await db.Get(Mapper.Map<DataAccess.Ado.Models.Entitys.AspNetUserRoles>(approvedDto));

            return Mapper.Map<IEnumerable<AspNetUserRolesDto>>(approved);
        }
        public async Task<bool> Create(AspNetUserRolesDto aspNetUserRolesDto)
        {
            try
            {
                return await db.Insert(Mapper.Map<DataAccess.Ado.Models.Entitys.AspNetUserRoles>(aspNetUserRolesDto));
            }
            catch { return false; }
        }
        public async Task<bool> Delete(AspNetUserRolesDto aspNetUserRolesDto)
        {
            try
            {
                return await db.Delete(Mapper.Map<DataAccess.Ado.Models.Entitys.AspNetUserRoles>(aspNetUserRolesDto));
            }
            catch { return false; }
        }       
    }
}
