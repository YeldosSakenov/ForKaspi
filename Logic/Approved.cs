using Abstractions.Interfaces.Entity;
using AutoMapper;
using Core.Dtos;
using DataAccess.Ado.Models.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Logic
{
    public class Approved: IApproved
    {
        Approveds db;
        public Approved()
        {
            db = new Approveds();
        }
        public async Task<IEnumerable<ApprovedDto>> Get(ApprovedDto approvedDto)
        {
            var approved =await db.Get(Mapper.Map<DataAccess.Ado.Models.Entitys.Approved>(approvedDto));
            
            return Mapper.Map<IEnumerable<ApprovedDto>>(approved); 
        }               
        public async Task<bool> Create(ApprovedDto approvedDto)
        {
            try
            { 
                return await db.Insert(Mapper.Map<DataAccess.Ado.Models.Entitys.Approved>(approvedDto)); 
            }
            catch { return false; }
        }
        public async Task<bool> Delete(ApprovedDto approvedDto)
        {
            try
            {  
                return await db.Delete(Mapper.Map<DataAccess.Ado.Models.Entitys.Approved>(approvedDto));
            }
            catch { return false; }
        }
        public async Task<bool> Update(ApprovedDto approvedDto)
        {           
            try
            {   
                return await db.Update(Mapper.Map<DataAccess.Ado.Models.Entitys.Approved>(approvedDto));
            }
            catch { return false; }
        }
    }
}
