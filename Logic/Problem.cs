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
    public class Problem : IProblem
    {
        private IdeaBankBaseEntities db;
        public Problem()
        {
            db = new IdeaBankBaseEntities();
        }
        public IEnumerable<ProblemDto> Get()
        {
            IQueryable<DataAccess.Models.Problem> problems = db.Problems
                    .Where(b => b.IsDeleted == false);
            IEnumerable<ProblemDto> value = Mapper.Map<IEnumerable<ProblemDto>>(problems);

            return value;
        }
        public ProblemDto Get(int id)
        {
            ProblemDto value = Mapper.Map<ProblemDto>(db.Problems.Find(id));

            return value;
        }
        public async Task<bool> Create(ProblemDto problemDto)
        {
            try
            {
                DataAccess.Models.Problem value = Mapper.Map<DataAccess.Models.Problem>(problemDto);

                db.Entry(value).State = EntityState.Added;

                db.Problems.Add(value);

                await db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> CreateReletionUnit(ProblemDto problemDto)
        {
            try
            {
                DataAccess.Models.Problem value = Mapper.Map<DataAccess.Models.Problem>(problemDto);

                List<Department> problemDepartments = db.Departments.Where(d => d.Id == problemDto.DepartmentId).ToList();

                value.Departments = problemDepartments;

                db.Problems.Add(value);

                await db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete(ProblemDto problemDto)
        {
            try
            {
                DataAccess.Models.Problem value = Mapper.Map<DataAccess.Models.Problem>(problemDto);

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
        public async Task<bool> Update(ProblemDto problemDto)
        {
            DataAccess.Models.Problem value = Mapper.Map<DataAccess.Models.Problem>(problemDto);

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
