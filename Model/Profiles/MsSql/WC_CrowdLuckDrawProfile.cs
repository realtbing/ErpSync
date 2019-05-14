using AutoMapper;
using Model.DTO.MsSql;
using Model.Entities;

namespace Model.Profiles.MsSql
{
    public class WC_CrowdLuckDrawProfile : Profile
    {
        public WC_CrowdLuckDrawProfile()
        {
            base.CreateMap<WC_CrowdLuckDraw, WC_CrowdLuckDrawDTO>().ForMember(destination => destination.userName, opt => opt.MapFrom(src => src.userName))
                                                                   .ForMember(destination => destination.headPicture, opt => opt.MapFrom(src => src.headPicture))
                                                                   .ForMember(destination => destination.luckyNumber, opt => opt.MapFrom(src => src.luckyNumber))
                                                                   .ForMember(destination => destination.winning, opt => opt.MapFrom(src => src.winning))
                                                                   .ForMember(destination => destination.time, opt => opt.MapFrom(src => src.createTime.ToString("HH:mm")));
        }
    }
}
