using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.Oracle.Mapping
{
    public class SKUTransformMapping : IEntityTypeConfiguration<SKUTransform>
    {
        public void Configure(EntityTypeBuilder<SKUTransform> builder)
        {
            builder.ToTable("SKUTRANSFORM");

            builder.Property(x => x.ID).HasColumnType("number(19)");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.MOTHERSKUID).HasColumnType("number(19)").IsRequired();
            builder.Property(x => x.MOTHERSKUCODE).HasColumnType("nvarchar2(50)").IsRequired();
            builder.Property(x => x.CHILDSKUID).HasColumnType("number(19)").IsRequired();
            builder.Property(x => x.CHILDSKUCODE).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(x => x.TYPE).HasColumnType("number(5)").IsRequired();
            builder.Property(x => x.QTYPERMOTHER).HasColumnType("number(24,6)");
            builder.Property(x => x.SEQNO).HasColumnType("number(19)");
            builder.Property(x => x.CREATEBYID).HasColumnType("number(19)");
            builder.Property(x => x.CREATEBYNAME).HasColumnType("nvarchar(50)");
            builder.Property(x => x.CREATEON).HasColumnType("date(7)");
            builder.Property(x => x.MODIFYBYID).HasColumnType("number(19)");
            builder.Property(x => x.MODIFYBYNAME).HasColumnType("nvarchar(50)");
            builder.Property(x => x.MODIFYON).HasColumnType("date(7)");
        }
    }
}
