using AutoMapper;
using MealOrderingApp.Server.Data.Models;
using MealOrderingApp.Shared.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace MealOrderingApp.Server.Services.Extensions
{
    public static class ConfigureMappingExtension
    {
        public static IServiceCollection ConfigureMapping(this IServiceCollection service)
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

            IMapper mapper = mappingConfig.CreateMapper();

            service.AddSingleton(mapper);

            return service;
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AllowNullDestinationValues = true;
            AllowNullCollections = true;
            CreateMap<Supplier, SupplierDTO>()
                .ReverseMap();

            CreateMap<User, UserDTO>();

            //CreateMap<UserDTO, User>()
            //    .ForMember(x => x.Password, y => y.MapFrom(z => PasswordEncrypter.Encrypt(z.Password)));

            CreateMap<Order, OrderDTO>()
                .ForMember(x => x.SupplierName, y => y.MapFrom(z => z.Supplier.Name))
                .ForMember(x => x.CreatedUserFullName, y => y.MapFrom(z => z.User.FirstName + " " + z.User.LastName));

            CreateMap<OrderDTO, Order>();



            CreateMap<OrderItem, OrderItemsDTO>()
                .ForMember(x => x.CreatedUserFullName, y => y.MapFrom(z => z.User.FirstName + " " + z.User.LastName))
                .ForMember(x => x.OrderName, y => y.MapFrom(z => z.Order.Name ?? ""));

            CreateMap<OrderItemsDTO, OrderItem>();
        }
    }
}
