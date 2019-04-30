using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.Oracle.Mapping
{
    public class OrganizeSKUMapping : IEntityTypeConfiguration<OrganizeSKU>
    {
        public void Configure(EntityTypeBuilder<OrganizeSKU> builder)
        {
            builder.ToTable("ORGANIZESKU");

            builder.Property(x => x.ID).HasColumnType("number(19)");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.SKU_ID).HasColumnType("number(19)").IsRequired();
            builder.Property(x => x.ORGANIZE_ID).HasColumnType("number(19)").IsRequired();
            builder.Property(x => x.SALESTATUS).HasColumnType("number(19)");
            builder.Property(x => x.ORDERSTATUS).HasColumnType("number(19)");
            builder.Property(x => x.ORDERTYPE).HasColumnType("number(19)");
            builder.Property(x => x.RECEIPTDC).HasColumnType("number(19)");
            builder.Property(x => x.SHIPDC).HasColumnType("number(19)");
            builder.Property(x => x.DELIVERYTYPE).HasColumnType("number(19)");
            builder.Property(x => x.RETURNTYPE).HasColumnType("number(19)");
            builder.Property(x => x.INPUTVATRATE).HasColumnType("number(10,3)");
            builder.Property(x => x.OUTPUTVATRATE).HasColumnType("number(10,3)");
            builder.Property(x => x.SUPPLIER_ID).HasColumnType("number(19)");
            builder.Property(x => x.PPRICE).HasColumnType("number(24,6)");
            builder.Property(x => x.SPRICE0).HasColumnType("number(24,6)");
            builder.Property(x => x.SPRICE).HasColumnType("number(24,6)");
            builder.Property(x => x.MPRICE0).HasColumnType("number(24,6)");
            builder.Property(x => x.MPRICE).HasColumnType("number(24,6)");
            builder.Property(x => x.DCRATE).HasColumnType("number(10,3)");
            builder.Property(x => x.INCRATE).HasColumnType("number(10,3)");
            builder.Property(x => x.LOSERATE).HasColumnType("number(10,3)");
            builder.Property(x => x.DESCRIPTION).HasColumnType("nvarchar2(200)");
            builder.Property(x => x.CREATEON).HasColumnType("date(7)");
            builder.Property(x => x.CREATEBYID).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.CREATEBYNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.MODIFYON).HasColumnType("date(7)");
            builder.Property(x => x.MODIFYBYID).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.MODIFYBYNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.DCPLACERATE).HasColumnType("number(10,3)");
            builder.Property(x => x.BUSINESSTYPE).HasColumnType("number(19)");
            builder.Property(x => x.SKU_CODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.ORGANIZE_CODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.DISPLAYQTY).HasColumnType("number(24,6)").IsRequired();
            builder.Property(x => x.ISCHGOODS).HasColumnType("number(5)");
            builder.Property(x => x.CHTAX).HasColumnType("number(12,4)");
            builder.Property(x => x.PURCHASEBYID).HasColumnType("number(19)");
            builder.Property(x => x.PURCHASEBYNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.MAXSALEDIFFQTY).HasColumnType("number(24,6)");
            builder.Property(x => x.RECEIPTDIFFRATE).HasColumnType("number(12,4)");
            builder.Property(x => x.ISSCALEWEIGHT).HasColumnType("number(5)");
            builder.Property(x => x.ISPUTON).HasColumnType("number(5)");
            builder.Property(x => x.MINSTOCK).HasColumnType("number(24,6)");
            builder.Property(x => x.MAXSTOCK).HasColumnType("number(24,6)");
            builder.Property(x => x.PICKDIFFRATE).HasColumnType("number(19,6)");
            builder.Property(x => x.SALEBONUSRATE).HasColumnType("number(19,6)");
            builder.Property(x => x.STOCKFIXDIFFRATE).HasColumnType("number(19,6)");
            builder.Property(x => x.SALEDIFFRATE).HasColumnType("number(19,6)");
            builder.Property(x => x.PURCHASEBONUSRATE).HasColumnType("number(19,6)");
            builder.Property(x => x.PPRICE0).HasColumnType("number(24,6)");
            builder.Property(x => x.STOCKFIXDIFFRATE2).HasColumnType("number(19,6)");
            builder.Property(x => x.MAXSALEDIFFQTY2).HasColumnType("number(24,6)");
            builder.Property(x => x.PICKDIFFRATE2).HasColumnType("number(19,6)");
            builder.Property(x => x.RECEIPTDIFFRATE2).HasColumnType("number(12,4)");
            builder.Property(x => x.SALEDIFFPUNISH).HasColumnType("number(19,2)");
            builder.Property(x => x.ORIGIN).HasColumnType("nvarchar2(150)");
            builder.Property(x => x.STOCKDIFFPUNISH).HasColumnType("number(19,2)");
            builder.Property(x => x.STOCKDIFFPUNISH2).HasColumnType("number(19,2)");
            builder.Property(x => x.RETURNWAY).HasColumnType("number(19)");
        }
    }
}
