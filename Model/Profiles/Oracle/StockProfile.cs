using AutoMapper;
using Model.DTO.Oracle;
using Model.Entities.Oracle;

namespace Model.Profiles.Oracle
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            base.CreateMap<Stock, SKUQTYChangeDTO>().ForMember(destination => destination.Id, opt => opt.Ignore())
                                                    .ForMember(destination => destination.ShopCode, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.ORGCODE) ? src.ORGCODE : ""))
                                                    .ForMember(destination => destination.SKUCode, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.GOODSCODE) ? src.GOODSCODE : ""))
                                                    .ForMember(destination => destination.Qty, opt => opt.MapFrom(src => src.QTY.HasValue ? src.QTY.Value : 0))
                                                    .ForMember(destination => destination.isSuccess, opt => opt.MapFrom(src => false));
        }
    }
}
