using AutoMapper;
using CloudinaryImageCrudHandler.DTO;
using ContactManagement.Domain.Entities;
using ContentManagement.Application.DataTransfer;
using ContentManagement.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudinaryImageCrudHandler.Mapper
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
