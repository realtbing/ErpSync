using System;
using AutoMapper;
using Model.DTO.MsSql;

namespace Model.Profiles.MsSql
{
    public class WC_CrowdProfile : Profile
    {
        public WC_CrowdProfile()
        {
            base.CreateMap<WC_CrowdAndShopDTO, WC_CrowdDTO>().ForMember(destination => destination.name, opt => opt.MapFrom(src => src.name))
                                                             .ForMember(destination => destination.shopName, opt => opt.MapFrom(src => src.shopName))
                                                             .ForMember(destination => destination.lotteryTimeStr, opt => opt.MapFrom(src => DateTime.Now.Date.AddHours(src.lotteryTime.Hour).AddMinutes(src.lotteryTime.Minute).ToString("HH:mm")))
                                                             .ForMember(destination => destination.lotteryTimeStatus, opt => opt.MapFrom(src => DateTime.Now.Date.AddHours(src.lotteryTime.Hour)
                                                                                                                                                                 .AddMinutes(src.lotteryTime.Minute)
                                                                                                                                                                 .AddSeconds(src.lotteryTime.Second) < DateTime.Now ? 1 :
                                                                                                                                                DateTime.Now.Date.AddHours(src.lotteryTime.Hour)
                                                                                                                                                                 .AddMinutes(src.lotteryTime.Minute)
                                                                                                                                                                 .AddSeconds(src.lotteryTime.Second)
                                                                                                                                                                 .AddMinutes(src.lotteryMinute) > DateTime.Now ? 3 : 2));
        }
    }
}
