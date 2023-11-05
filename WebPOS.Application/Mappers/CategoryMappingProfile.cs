using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPOS.Application.Dtos.Request;
using WebPOS.Application.Dtos.Response;
using WebPOS.Domain.Entities;
using WebPOS.Infrastructure.Commons.Foundation.Response;
using WebPOS.Utilitties.Statics.Enums;
using WebPOS.Utilitties.Statics.Strings;

namespace WebPOS.Application.Mappers
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryResponseDto>()
                .ForMember(x => x.StateCategory, x => x.MapFrom(y => y.State.Equals((int)StatusType.ACTIVE) ? CommonStrings.ACTIVE : CommonStrings.INACTIVE))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Category>, BaseEntityResponse<CategoryResponseDto>>()
                .ReverseMap();

            CreateMap<CategoryRequestDto, Category>();

            CreateMap<Category, CategorySelectResponseDto>()
                .ReverseMap();
        }
    }
}
