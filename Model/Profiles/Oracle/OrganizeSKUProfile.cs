using AutoMapper;
using Model.Entities.Oracle;
using Model.DTO.Oracle;

namespace Model.Profiles.Oracle
{
    public class OrganizeSKUProfile : Profile
    {
        public OrganizeSKUProfile()
        {
            base.CreateMap<OrganizeSKUBAKDTO, SKUPriceChangeDTO>().ForMember(destination => destination.Id, opt => opt.MapFrom(src => src.triggerData.Id))
                                                                  .ForMember(destination => destination.BarCode, opt => opt.Ignore())
                                                                  .ForMember(destination => destination.PluCode, opt => opt.Ignore())
                                                                  .ForMember(destination => destination.ShopCode, opt => opt.MapFrom(src => src.sku.ORGANIZE_CODE))
                                                                  .ForMember(destination => destination.SKUCode, opt => opt.MapFrom(src => src.sku.SKU_CODE))
                                                                  .ForMember(destination => destination.Price, opt => opt.MapFrom(src => src.sku.SPRICE.HasValue ? src.sku.SPRICE.Value : 0))
                                                                  .ForMember(destination => destination.isSuccess, opt => opt.MapFrom(src => false));

            base.CreateMap<OrganizeSKUDTO, SKUPriceChangeDTO>().ForMember(destination => destination.Id, opt => opt.MapFrom(src => src.triggerData.Id))
                                                               .ForMember(destination => destination.BarCode, opt => opt.Ignore())
                                                               .ForMember(destination => destination.PluCode, opt => opt.Ignore())
                                                               .ForMember(destination => destination.ShopCode, opt => opt.MapFrom(src => src.sku.ORGANIZE_CODE))
                                                               .ForMember(destination => destination.SKUCode, opt => opt.MapFrom(src => src.sku.SKU_CODE))
                                                               .ForMember(destination => destination.Price, opt => opt.MapFrom(src => src.sku.SPRICE.HasValue ? src.sku.SPRICE.Value : 0))
                                                               .ForMember(destination => destination.isSuccess, opt => opt.MapFrom(src => false));

            base.CreateMap<OrganizeSKU, SKUPriceChangeDTO>().ForMember(destination => destination.Id, opt => opt.Ignore())
                                                            .ForMember(destination => destination.BarCode, opt => opt.Ignore())
                                                            .ForMember(destination => destination.PluCode, opt => opt.Ignore())
                                                            .ForMember(destination => destination.ShopCode, opt => opt.MapFrom(src => src.ORGANIZE_CODE))
                                                            .ForMember(destination => destination.SKUCode, opt => opt.MapFrom(src => src.SKU_CODE))
                                                            .ForMember(destination => destination.Price, opt => opt.MapFrom(src => src.SPRICE.HasValue ? src.SPRICE.Value : 0))
                                                            .ForMember(destination => destination.isSuccess, opt => opt.MapFrom(src => true));
        }
    }
}