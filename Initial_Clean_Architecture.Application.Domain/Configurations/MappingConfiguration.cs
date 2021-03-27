using AutoMapper;
using Initial_Clean_Architecture.Application.Domain.ContractsModels.Requests;
using Initial_Clean_Architecture.Data.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Domain.Configurations
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<AppUser, UserRegistrationRequest>()
                .ForMember(dest => dest.Email,
                         opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
        }
    }
}
