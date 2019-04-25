using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.MsSql.Mapping
{
    public class SKUMapping : IEntityTypeConfiguration<PP_Shop>
    {
        public void Configure(EntityTypeBuilder<PP_Shop> builder)
        {
            builder.ToTable("PP_Shop");
            
            builder.HasKey(x => x.shopId);

            builder.Property(x => x.shopCode).HasColumnType("varchar(32)");
            builder.Property(x => x.shopName).HasColumnType("varchar(50)");
            builder.Property(x => x.province).HasColumnType("varchar(10)");
            builder.Property(x => x.city).HasColumnType("varchar(10)");
            builder.Property(x => x.area).HasColumnType("varchar(10)");
            builder.Property(x => x.fullAddress).HasColumnType("varchar(100)");
            builder.Property(x => x.longitude).HasColumnType("varchar(32)");
            builder.Property(x => x.latitude).HasColumnType("varchar(32)");
            builder.Property(x => x.electricFence).HasColumnType("varchar(2000)");
            builder.Property(x => x.shopPicture).HasColumnType("varchar(500)");
            builder.Property(x => x.shopTel).HasColumnType("varchar(50)");
            builder.Property(x => x.status).HasColumnType("int");
            builder.Property(x => x.sendType).HasColumnType("varchar(10)");
            builder.Property(x => x.unitPrice).HasColumnType("float");
            builder.Property(x => x.createTime).HasColumnType("datetime");
        }
    }
}
