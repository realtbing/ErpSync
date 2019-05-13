using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.MsSql.Mapping
{
    public class PP_TriggerDataBakMapping : IEntityTypeConfiguration<PP_ShopGoods>
    {
        public void Configure(EntityTypeBuilder<PP_ShopGoods> builder)
        {
            builder.ToTable("PP_ShopGoods");
            
            builder.HasKey(x => x.sgid);

            builder.Property(x => x.shopId).HasColumnType("int").IsRequired();
            builder.Property(x => x.skuId).HasColumnType("int").IsRequired();
            builder.Property(x => x.goodsPrice).HasColumnType("float");
            builder.Property(x => x.publicPrice).HasColumnType("float");
            builder.Property(x => x.purchasePrice).HasColumnType("float");
            builder.Property(x => x.integralCost).HasColumnType("int");
            builder.Property(x => x.inventory).HasColumnType("float");
            builder.Property(x => x.salesCnt).HasColumnType("int");
            builder.Property(x => x.commissionRate).HasColumnType("float");
            builder.Property(x => x.limitType).HasColumnType("int");
            builder.Property(x => x.limitPrice).HasColumnType("float");
            builder.Property(x => x.limitQty).HasColumnType("int");
            builder.Property(x => x.browseNo).HasColumnType("int");
            builder.Property(x => x.collectionNo).HasColumnType("int");
            builder.Property(x => x.shareNo).HasColumnType("int");
            builder.Property(x => x.status).HasColumnType("int");
            builder.Property(x => x.isOfflineSKU).HasColumnType("bit");
            builder.Property(x => x.isOfflineSKUPrice).HasColumnType("bit");
            builder.Property(x => x.shelveTime).HasColumnType("datetime");
            builder.Property(x => x.daysAfterSale).HasColumnType("int");
            builder.Property(x => x.createTime).HasColumnType("datetime").IsRequired();
        }
    }
}
