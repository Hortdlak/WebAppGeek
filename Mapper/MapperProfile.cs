using AutoMapper;
using WebAppGeek.Models;
using WebAppGeek.DTO;

namespace WebAppGeek.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductGroup, ProductGroupDTO>().ReverseMap();
        }
    }
}
