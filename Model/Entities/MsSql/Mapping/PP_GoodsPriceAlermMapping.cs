using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.MsSql.Mapping
{
    public class POSV2_SaleOrderMapping : IEntityTypeConfiguration<PP_GoodsPriceAlerm>
    {
        public void Configure(EntityTypeBuilder<PP_GoodsPriceAlerm> builder)
        {
            builder.ToTable("PP_GoodsPriceAlerm");
            
            builder.HasKey(x => x.listId);

            builder.Property(x => x.goodsId).HasColumnType("int").IsRequired();
            builder.Property(x => x.goodsPluCode).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.goodsBarCode).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.goodsCode).HasColumnType("varchar(32)").IsRequired();
            builder.Property(x => x.goodsName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.shopId).HasColumnType("int").IsRequired();
            builder.Property(x => x.shopCode).HasColumnType("varchar(32)").IsRequired();
            builder.Property(x => x.shopName).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.originPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.newPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.confirmPrice).HasColumnType("decimal(18,2)");
            builder.Property(x => x.createTime).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.confirmTime).HasColumnType("datetime");
        }
    }
}
