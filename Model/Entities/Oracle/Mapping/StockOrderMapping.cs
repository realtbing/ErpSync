using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.Oracle.Mapping
{
    public class StockOrderMapping : IEntityTypeConfiguration<StockOrder>
    {
        public void Configure(EntityTypeBuilder<StockOrder> builder)
        {
            builder.ToTable("STOCKORDER");

            builder.Property(x => x.ID).HasColumnType("number(19)");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.CODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.ORGID).HasColumnType("number(19)");
            builder.Property(x => x.ORGCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.STORAGEID).HasColumnType("number(19)");
            builder.Property(x => x.STORAGECODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.ORDERTYPE).HasColumnType("number(5)");
            builder.Property(x => x.REFORGTYPE).HasColumnType("number(5)");
            builder.Property(x => x.REFORGID).HasColumnType("number(19)");
            builder.Property(x => x.REFORGCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.REFSTORAGEID).HasColumnType("number(19)");
            builder.Property(x => x.REFSTORAGECODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.REFORGTYPE2).HasColumnType("number(5)");
            builder.Property(x => x.REFORGID2).HasColumnType("number(19)");
            builder.Property(x => x.REFORGCODE2).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.REFORDERTYPE).HasColumnType("number(5)");
            builder.Property(x => x.REFORDERCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.PLANAMT).HasColumnType("number(24,6)");
            builder.Property(x => x.FACTAMT).HasColumnType("number(24,6)");
            builder.Property(x => x.STATUS).HasColumnType("number(5)");
            builder.Property(x => x.DESCRIPTION).HasColumnType("nvarchar2(200)");
            builder.Property(x => x.CREATEBYID).HasColumnType("number(19)");
            builder.Property(x => x.CREATEON).HasColumnType("date(7)");
            builder.Property(x => x.CREATEBYNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.MODIFYBYID).HasColumnType("number(19)");
            builder.Property(x => x.MODIFYBYNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.MODIFYON).HasColumnType("date(7)");
            builder.Property(x => x.AUDITBYID).HasColumnType("number(19)");
            builder.Property(x => x.AUDITON).HasColumnType("date(7)");
            builder.Property(x => x.AUDITBYNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.APPROVEBYID).HasColumnType("number(19)");
            builder.Property(x => x.APPROVEON).HasColumnType("date(7)");
            builder.Property(x => x.APPROVEBYNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.PRINTCOUNT).HasColumnType("number(5)");
            builder.Property(x => x.ISIMPACT).HasColumnType("number(5)");
            builder.Property(x => x.VOUCHINGUSER).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.VOUCHINGDATE).HasColumnType("date(7)");
            builder.Property(x => x.FINANCEID).HasColumnType("number(19)");
            builder.Property(x => x.FINANCECODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.RJDATE).HasColumnType("date(7)");
            builder.Property(x => x.CHECKBILLUSER).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.CHECKBILLDATE).HasColumnType("date(7)");
            builder.Property(x => x.PURCHASEBYID).HasColumnType("number(19)");
            builder.Property(x => x.PURCHASEBYNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.PURCHASEDATE).HasColumnType("date(7)");
            builder.Property(x => x.STATEMENTID).HasColumnType("number(19)");
            builder.Property(x => x.STATEMENTCODE).HasColumnType("nvarchar2(100)");
            builder.Property(x => x.REALAUDITON).HasColumnType("date(7)");
            builder.Property(x => x.GETORDERTYPEID).HasColumnType("number(19)");
            builder.Property(x => x.ISLOSSBILL).HasColumnType("number(5)");
            builder.Property(x => x.EFFECTDATE).HasColumnType("date(7)");
            builder.Property(x => x.RELATIONORDERID).HasColumnType("number(19)");
            builder.Property(x => x.RELATIONORDERCODE).HasColumnType("nvarchar2(100)");
            builder.Property(x => x.PASTEDAMT).HasColumnType("number(24,6)");
            builder.Property(x => x.IMPACTTYPE).HasColumnType("number(5)");
            builder.Property(x => x.MANUALORDERCODE).HasColumnType("varchar2(100)");
            builder.Property(x => x.TAXDEDUCTIONAMT).HasColumnType("number(24,6)");
        }
    }
}
