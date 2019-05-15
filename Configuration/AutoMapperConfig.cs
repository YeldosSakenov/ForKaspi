using AutoMapper;
using Core.Dtos;
using DataAccess.Ado.Models.Entitys;
using DataAccess.Models;
using System.Collections.Generic;

namespace Domain.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<AspNetUser, IEnumerable<AspNetUserDto>>();
                //cfg.CreateMap < IEnumerable < AspNetUserDto >, AspNetUser>();
                cfg.CreateMap<AspNetUser, AspNetUserDto>();
                cfg.CreateMap<AspNetUserDto, AspNetUser>();


                cfg.CreateMap<AspNetRole, IEnumerable<AspNetRoleDto>>();
                cfg.CreateMap< AspNetRoleDto,AspNetRole>();

                cfg.CreateMap<TypeOfOfferDto, TypeOfOffer>();
                cfg.CreateMap<TypeOfOffer, TypeOfOfferDto>();

                cfg.CreateMap<CategoryDto, Category>();
                cfg.CreateMap<Category, CategoryDto>();

                cfg.CreateMap<UnitDto, Department>();
                cfg.CreateMap<Department, UnitDto>();

                cfg.CreateMap<DirectionDto, Direction>();
                cfg.CreateMap<Direction, DirectionDto>();

                cfg.CreateMap<StatusDto, Status>();
                cfg.CreateMap<Status, StatusDto>();

                cfg.CreateMap<ProblemDto, Problem>();
                cfg.CreateMap<Problem, ProblemDto>();

                cfg.CreateMap<OfferDto, Offer>();
                cfg.CreateMap<Offer, OfferDto>();

                cfg.CreateMap<Offer, IEnumerable<OfferDto>>();

                cfg.CreateMap<ApprovedDto, DataAccess.Ado.Models.Entitys.Approved>();
                cfg.CreateMap<DataAccess.Ado.Models.Entitys.Approved, ApprovedDto>();

                cfg.CreateMap<AspNetUserRolesDto, AspNetUserRoles>();
                cfg.CreateMap<AspNetUserRoles, AspNetUserRolesDto>();

                cfg.CreateMap<FileDto, File>();
                cfg.CreateMap<File, FileDto>();


            });
        }
    }
}
