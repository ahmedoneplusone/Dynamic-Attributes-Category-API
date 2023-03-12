using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace Dynamic
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<CategoryForCreationDto, Category>();

            CreateMap<CategoryForUpdateDto, Category>().ReverseMap();

        }
    }
}
