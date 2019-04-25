using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.Oracle.Mapping
{
    public class POSV2_SaleOrderDetailMapping : IEntityTypeConfiguration<POSV2_SaleOrderDetail>
    {
        public void Configure(EntityTypeBuilder<POSV2_SaleOrderDetail> builder)
        {
            builder.ToTable("POSV2_SALEORDERDETAIL");

            builder.Property(x => x.ID).HasColumnType("nvarchar2(50)");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.STORECODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.STORENAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.POSCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.POSNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.ORDERID).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.ORDERCODE).HasColumnType("nvarchar2(100)");
            builder.Property(x => x.DETAILSEQNO).HasColumnType("number(5)");
            builder.Property(x => x.GOODSCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.GOODSBARCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.GOODSSCANBARCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.GOODSNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.UNIT).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.SPEC).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.TAX).HasColumnType("number(19,4)");
            builder.Property(x => x.CURRENCY).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.POINTUNIT).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.SCALETYPE).HasColumnType("number(5)");
            builder.Property(x => x.CATECODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.CATENAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.PRICETYPE).HasColumnType("number(5)");
            builder.Property(x => x.NORMALPRICE).HasColumnType("number(19,4)");
            builder.Property(x => x.ORIGINALPRICE).HasColumnType("number(19,4)");
            builder.Property(x => x.POINT).HasColumnType("number(19,4)");
            builder.Property(x => x.PRICE).HasColumnType("number(19,4)");
            builder.Property(x => x.ORIGINALTAREQTY).HasColumnType("number(19,4)");
            builder.Property(x => x.ORIGINALQTY).HasColumnType("number(19,4)");
            builder.Property(x => x.QTY).HasColumnType("number(19,4)");
            builder.Property(x => x.ORIGINALAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.GOODSAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.PLANAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.TOTALAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.TOTALPOINT).HasColumnType("number(19,4)");
            builder.Property(x => x.SHAREAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.PROMOTIONAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.DISCOUNTAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.LOSSAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.PAYMENTAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.DETAILSTATUS).HasColumnType("number(5)");
            builder.Property(x => x.ISCHGOODS).HasColumnType("number(5)");
            builder.Property(x => x.CHTAX).HasColumnType("number(12,4)");
            builder.Property(x => x.CHTAXAMT).HasColumnType("number(19,4)");
            builder.Property(x => x.PROMOTIONCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.PROMOTIONNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.GROUPCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.GROUPQTY).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.GIFTPOINTVALUE).HasColumnType("number(19,4)");
            builder.Property(x => x.ISGIFT).HasColumnType("number(19,4)");
            builder.Property(x => x.ORIGINALPRICETYPE).HasColumnType("number(9)");
            builder.Property(x => x.PROMOTIONPRICETYPE).HasColumnType("number(9)");
            builder.Property(x => x.UPRICE).HasColumnType("number(14,2)");
            builder.Property(x => x.UPOINT).HasColumnType("number(14,2)");
            builder.Property(x => x.TOTALWEIGHT).HasColumnType("number(14,4)");
            builder.Property(x => x.PLANPACKQTY).HasColumnType("number(9)");
            builder.Property(x => x.FACTPACKQTY).HasColumnType("number(9)");
        }
    }
}
