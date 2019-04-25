using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.MsSql.Mapping
{
    public class PP_TriggerDataMapping : IEntityTypeConfiguration<PP_GoodsStockAlerm>
    {
        public void Configure(EntityTypeBuilder<PP_GoodsStockAlerm> builder)
        {
            builder.ToTable("PP_GoodsStockAlerm");
            
            builder.HasKey(x => x.listId);

            builder.Property(x => x.goodsId).HasColumnType("int").IsRequired();
            builder.Property(x => x.goodsCode).HasColumnType("varchar(32)").IsRequired();
            builder.Property(x => x.goodsName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.shopId).HasColumnType("int").IsRequired();
            builder.Property(x => x.shopCode).HasColumnType("varchar(32)").IsRequired();
            builder.Property(x => x.shopName).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.createTime).HasColumnType("datetime").IsRequired();
        }
    }
}
