using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.MsSql.Mapping
{
    public class POSV2_SaleOrderDetailMapping : IEntityTypeConfiguration<PP_GoodsSKU>
    {
        public void Configure(EntityTypeBuilder<PP_GoodsSKU> builder)
        {
            builder.ToTable("PP_GoodsSKU");
            
            builder.HasKey(x => x.skuId);
            
            builder.Property(x => x.skuCode).HasColumnType("varchar(32)");
            builder.Property(x => x.goodsCode).HasColumnType("varchar(32)");
            builder.Property(x => x.attrIds).HasColumnType("varchar(1024)");
            builder.Property(x => x.unit).HasColumnType("varchar(10)");
        }
    }
}
