using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.Oracle.Mapping
{
    public class SKUMapping : IEntityTypeConfiguration<SKU>
    {
        public void Configure(EntityTypeBuilder<SKU> builder)
        {
            builder.ToTable("SKU");

            builder.Property(x => x.ID).HasColumnType("number(19)");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.CATEGORY_ID).HasColumnType("number(19)");
            builder.Property(x => x.SHORTNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.NAME).HasColumnType("nvarchar2(50)").IsRequired();
            builder.Property(x => x.WBCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.PYCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.BRAND_ID).HasColumnType("number(19)");
            builder.Property(x => x.MODEL).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.RANK).HasColumnType("number(19)");
            builder.Property(x => x.PRICETYPE).HasColumnType("number(19)");
            builder.Property(x => x.STATUS).HasColumnType("number(5)").IsRequired();
            builder.Property(x => x.BILLTYPE).HasColumnType("number(19)").IsRequired();
            builder.Property(x => x.MANAGETYPE).HasColumnType("number(19)");
            builder.Property(x => x.INPUTVATRATE).HasColumnType("number(38,4)").IsRequired();
            builder.Property(x => x.OUTPUTVATRATE).HasColumnType("number(38,4)").IsRequired();
            builder.Property(x => x.SPEC).HasColumnType("number(38,3)");
            builder.Property(x => x.UNIT).HasColumnType("nvarchar(50)");
            builder.Property(x => x.SALESPEC).HasColumnType("number(38,3)").IsRequired();
            builder.Property(x => x.SALEUNIT).HasColumnType("nvarchar2(50)").IsRequired();
            builder.Property(x => x.GROUPID).HasColumnType("number(19)");
            builder.Property(x => x.SUPPLIER_ID).HasColumnType("number(19)");
            builder.Property(x => x.IMGURL).HasColumnType("nvarchar2(200)");
            builder.Property(x => x.PPRICE).HasColumnType("number(0)");
            builder.Property(x => x.SPRICE).HasColumnType("number(0)");
            builder.Property(x => x.MPRICE).HasColumnType("number(0)");
            builder.Property(x => x.DESCRIPTION).HasColumnType("nvarchar2(200)");
            builder.Property(x => x.CREATEON).HasColumnType("date(7)");
            builder.Property(x => x.CREATEBYID).HasColumnType("nvarchar2(50)");
        }
    }
}
