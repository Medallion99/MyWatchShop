using AutoMapper;
using MyWatchShop.Models.Entity;
using MyWatchShop.Models.ViewModels;

namespace MyWatchShop.Settings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddProductViewModel, Product>().ReverseMap();
        }
    }
}
