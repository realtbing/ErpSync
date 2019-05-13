using AutoMapper;
using Model.DTO.Oracle;

namespace Model.Profiles.Oracle
{
    public class SKUProfile : Profile
    {
        public SKUProfile()
        {
            base.CreateMap<SKUBAKDTO, SKUStatusChangeDTO>().ForMember(destination => destination.Id, opt => opt.MapFrom(src => src.triggerData.Id))
                                                           .ForMember(destination => destination.SKUCode, opt => opt.MapFrom(src => src.sku.CODE))
                                                           .ForMember(destination => destination.Status, opt => opt.MapFrom(src => src.sku.STATUS > (int)SKUStatus.WaitEliminate ? (int)GoodsStatus.PullOff : (int)GoodsStatus.PutOn))
                                                           .ForMember(destination => destination.isSuccess, opt => opt.MapFrom(src => false));

            base.CreateMap<SKUDTO, SKUStatusChangeDTO>().ForMember(destination => destination.Id, opt => opt.MapFrom(src => src.triggerData.Id))
                                                        .ForMember(destination => destination.SKUCode, opt => opt.MapFrom(src => src.sku.CODE))
                                                        .ForMember(destination => destination.Status, opt => opt.MapFrom(src => src.sku.STATUS > (int)SKUStatus.WaitEliminate ? (int)GoodsStatus.PullOff : (int)GoodsStatus.PutOn))
                                                        .ForMember(destination => destination.isSuccess, opt => opt.MapFrom(src => false));
        }
    }
}
