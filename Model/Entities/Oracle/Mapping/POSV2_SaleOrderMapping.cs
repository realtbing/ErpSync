using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.Oracle.Mapping
{
    public class POSV2_SaleOrderMapping : IEntityTypeConfiguration<POSV2_SaleOrder>
    {
        public void Configure(EntityTypeBuilder<POSV2_SaleOrder> builder)
        {
            builder.ToTable("POSV2_SALEORDER");

            builder.Property(x => x.ID).HasColumnType("nvarchar2(50)");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.CODE).HasColumnType("nvarchar2(100)");
            builder.Property(x => x.SEQNO).HasColumnType("number");
            builder.Property(x => x.SERIALNO).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.SALEORDERTYPE).HasColumnType("number(5)");
            builder.Property(x => x.STORECODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.STORENAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.POSCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.POSNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.STATUS).HasColumnType("number(5)");
            builder.Property(x => x.SHIFT).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.SALERCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.SALERNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.UPLOADSTATUS).HasColumnType("number(5)");
            builder.Property(x => x.UPLOADON).HasColumnType("date(7)");
            builder.Property(x => x.UPLOADMSG).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.GOODSAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.PROMOTIONAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.DISCOUNTAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.ORDERDISCOUNTAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.TOTALAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.PAYMENTAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.CHANGEAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.LOSSAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.VIPAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.DISCOUNTAUTHORCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.DISCOUNTAUTHORNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.CUSTOMERCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.CARDCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.TOTALPOINT).HasColumnType("number(19,4)");
            builder.Property(x => x.SOURCETYPE).HasColumnType("number(5)");
            builder.Property(x => x.REFSTORECODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.REFPOSCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.REFORDERCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.DESCRIPTION).HasColumnType("nvarchar2(200)");
            builder.Property(x => x.CREATEON).HasColumnType("date(7)");
            builder.Property(x => x.CREATEBYID).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.MODIFYON).HasColumnType("date(7)");
            builder.Property(x => x.MODIFYBYID).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.MODIFYBYNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.CREATEBYNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.PRINTCUSTOMERBALANCE).HasColumnType("number(19,4)");
            builder.Property(x => x.PRINTCUSTOMERPOINT).HasColumnType("number(19,4)");
            builder.Property(x => x.CUSTOMERTYPECODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.CUSTOMERTYPENAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.PAYMENTPOINT).HasColumnType("number(19,4)");
            builder.Property(x => x.GIVEPOINT).HasColumnType("number(19,4)");
            builder.Property(x => x.MATHAMT).HasColumnType("number(19,2)");
            builder.Property(x => x.OFFLINE).HasColumnType("number(19)");
            builder.Property(x => x.MATHRATE).HasColumnType("number(19,2)");
            builder.Property(x => x.CUSTOMERLEVEL).HasColumnType("number(19)");
            builder.Property(x => x.CUSTOMERLEVELNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.EMPLOYEECODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.EMPLOYEENAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.UPLOADIP).HasColumnType("varchar2(64)");
            builder.Property(x => x.CREATEBYIP).HasColumnType("varchar2(64)");
            builder.Property(x => x.CREATEBYMAC).HasColumnType("varchar2(64)");
            builder.Property(x => x.ISCHORDER).HasColumnType("number(5)");
            builder.Property(x => x.CHTAXAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.REGIONNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.REGIONCODE).HasColumnType("nvarchar2(10)");
            builder.Property(x => x.REGIONPATH).HasColumnType("nvarchar2(20)");
            builder.Property(x => x.DELIVERYNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.DELIVERYADDRESS).HasColumnType("nvarchar2(200)");
            builder.Property(x => x.DELIVERYPHONE).HasColumnType("nvarchar2(20)");
            builder.Property(x => x.SERVICETYPECODE).HasColumnType("varchar2(50)");
            builder.Property(x => x.SERVICETYPENAME).HasColumnType("varchar2(50)");
            builder.Property(x => x.CUSTOMERPHONE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.TABLECODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.TABLENAME).HasColumnType("nvarchar2(50)");
        }
    }
}
