using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using System.Linq;
using MT.OnlineRestaurant.BusinessEntities.Enums;
using System.Diagnostics.CodeAnalysis;

namespace MT.OnlineRestaurant.BusinessLayer
{
    [ExcludeFromCodeCoverage]
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<OrderEntity, TblFoodOrder>()
                .ForMember(dest => dest.TblRestaurantId, opt => opt.MapFrom(src => src.RestaurantId))
                .ForMember(dest => dest.TblCustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.TblOrderStatusId, opt => opt.MapFrom(src => (int)Status.Initiated))
                .ForMember(dest => dest.TblPaymentTypeId, opt => opt.MapFrom(src => (int)PaymentType.NoPayment))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.OrderMenuDetails.Sum(m => m.Price)))
                .ForMember(dest => dest.RecordTimeStamp, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<OrderMenus, TblFoodOrderMapping>()
                .ForMember(dest => dest.TblMenuId, opt => opt.MapFrom(src => src.MenuId));

            CreateMap<ICollection<OrderMenus>, ICollection<TblFoodOrderMapping>>();

            
        }
    }
}
