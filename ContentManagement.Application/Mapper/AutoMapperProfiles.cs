using AutoMapper;
using ContactManagement.Domain.Entities;
using ContentManagement.Application.DataTransfer;
using ContentManagement.Domain.Entities;
using ContentManagement.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentManagement.Application.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, CreateUserDTO>().ReverseMap();
            CreateMap<AddPhoneNumberDTO, PhoneNumber>().ReverseMap();
            CreateMap<AddAddressDTO, Address>().ReverseMap();
        }
    }
}
