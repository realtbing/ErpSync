using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.Oracle.Mapping
{
    public class StockOrderDetailMapping : IEntityTypeConfiguration<StockOrderDetail>
    {
        public void Configure(EntityTypeBuilder<StockOrderDetail> builder)
        {
            builder.ToTable("STOCKORDERDETAIL");

            builder.Property(x => x.ID).HasColumnType("nvarchar2(36)");
            builder.HasKey(x => x.ID);
            
            builder.Property(x => x.ORGID).HasColumnType("number(19)");
            builder.Property(x => x.ORGCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.STORAGEID).HasColumnType("number(19)");
            builder.Property(x => x.STORAGECODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.ORDERTYPE).HasColumnType("number(5)");
            builder.Property(x => x.ORDERID).HasColumnType("number(19)");
            builder.Property(x => x.ORDERCODE).HasColumnType("number(5)");
            builder.Property(x => x.GOODSID).HasColumnType("number(19)");
            builder.Property(x => x.GOODSCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.GOODSBARCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.PLANPRICE).HasColumnType("number(24,6)");
            builder.Property(x => x.FACTPRICE).HasColumnType("number(24,6)");
            builder.Property(x => x.PLANAMT).HasColumnType("number(24,6)");
            builder.Property(x => x.FACTAMT).HasColumnType("number(24,6)");
            builder.Property(x => x.SPEC).HasColumnType("number(19,3)");
            builder.Property(x => x.TAX).HasColumnType("number(7,3)");
            builder.Property(x => x.SIGN).HasColumnType("number(5)");
            builder.Property(x => x.QTY).HasColumnType("number(24,6)");
            builder.Property(x => x.PRODUCEDATE).HasColumnType("date(7)");
            builder.Property(x => x.PRODUCEBATCH).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.EXPIRYDATE).HasColumnType("date(7)");
            builder.Property(x => x.SUPPLIERID).HasColumnType("number(19)");
            builder.Property(x => x.SUPPLIERCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.STATUS).HasColumnType("number(5)");
            builder.Property(x => x.DESCRIPTION).HasColumnType("nvarchar2(200)");
            builder.Property(x => x.CREATEBYID).HasColumnType("number(19)");
            builder.Property(x => x.CREATEON).HasColumnType("date(7)");
            builder.Property(x => x.CREATEBYNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.MODIFYBYID).HasColumnType("number(19)");
            builder.Property(x => x.MODIFYBYNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.MODIFYON).HasColumnType("date(7)");
            builder.Property(x => x.BATCHAMT).HasColumnType("number(24,6)");
            builder.Property(x => x.MARKUPAMT).HasColumnType("number(24,6)");
            builder.Property(x => x.ORIGINALGOODSID).HasColumnType("number(19)");
            builder.Property(x => x.ORIGINALGOODSCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.IMPACTQTY).HasColumnType("number(24,6)");
            builder.Property(x => x.ORIGINALDETAILID).HasColumnType("nvarchar2(36)");
            builder.Property(x => x.TAXDEDUCTION).HasColumnType("number(5)");
            builder.Property(x => x.STAX).HasColumnType("number(7,3)");
            builder.Property(x => x.RULEID).HasColumnType("number(19)");
            builder.Property(x => x.RULEAMT).HasColumnType("number(24,6)");
            builder.Property(x => x.FACTAMTNORULE).HasColumnType("number(24,6)");
            builder.Property(x => x.SPECIFIEDPRICE).HasColumnType("number(24,6)");
            builder.Property(x => x.TAREQTY).HasColumnType("number(24,6)");
            builder.Property(x => x.FIRSTPRICE).HasColumnType("number(24,6)");
            builder.Property(x => x.BUSINESSTYPE).HasColumnType("number(19)");
            builder.Property(x => x.REFORDERID).HasColumnType("number(19)");
            builder.Property(x => x.REFORDERCODE).HasColumnType("varchar2(50)");
            builder.Property(x => x.IMPACTAMT).HasColumnType("number(24,6)");
            builder.Property(x => x.SPECIFIEDAMT).HasColumnType("number(24,6)");
            builder.Property(x => x.TAREWEIGHT).HasColumnType("number(24,6)");
            builder.Property(x => x.SHIPPERTAREWEIGHT).HasColumnType("number(24,6)");
            builder.Property(x => x.GROSSWEIGHT).HasColumnType("number(24,6)");
            builder.Property(x => x.PURCHASETYPE).HasColumnType("number(5)");
            builder.Property(x => x.DCOUTPRICE).HasColumnType("number(24,6)");
            builder.Property(x => x.STOCKBATCHID).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.STOCKBATCHCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.PURCHASERID).HasColumnType("number(19)");
            builder.Property(x => x.PURCHASERNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.ORIBATCHID).HasColumnType("nvarchar2(36)");
            builder.Property(x => x.ORIBATCHCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.REFBATCHID).HasColumnType("nvarchar2(36)");
            builder.Property(x => x.REFBATCHCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.REALPRICE).HasColumnType("number(24,6)");
            builder.Property(x => x.REFORDERTYPE).HasColumnType("number(19)");
            builder.Property(x => x.REFORDERDETAILID).HasColumnType("varchar2(36)");
            builder.Property(x => x.TURNOVERID).HasColumnType("number(24,6)");
            builder.Property(x => x.TURNOVERCODE).HasColumnType("varchar2(50)");
            builder.Property(x => x.TURNOVERQTY).HasColumnType("number(24,6)");
            builder.Property(x => x.GROUPID).HasColumnType("number(19)");
            builder.Property(x => x.TAXDEDUCTIONAMT).HasColumnType("number(24,6)");
        }
    }
}
