using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.Oracle.Mapping
{
    public class StockSnapshotMapping : IEntityTypeConfiguration<StockSnapshot>
    {
        public void Configure(EntityTypeBuilder<StockSnapshot> builder)
        {
            builder.ToTable("STOCKSNAPSHOT");

            builder.Property(x => x.BACKUPDATE).HasColumnType("date(7)");
            builder.Property(x => x.ORGID).HasColumnType("number(19)");
            builder.Property(x => x.ORGCODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.STORAGEID).HasColumnType("number(19)");
            builder.Property(x => x.STORAGECODE).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.GOODSID).HasColumnType("number(19)");
            builder.Property(x => x.GOODSCODE).HasColumnType("nvarchar2(50)");
            builder.HasKey(x => new { x.BACKUPDATE , x.ORGID, x.ORGCODE, x.STORAGEID, x.STORAGECODE, x.GOODSID, x.GOODSCODE});
            builder.Property(x => x.PRICE).HasColumnType("number(24,6)");
            builder.Property(x => x.PASSAGEQTY).HasColumnType("number(24,6)");
            builder.Property(x => x.QTY).HasColumnType("number(24,6)");
            builder.Property(x => x.AMT).HasColumnType("number(24,6)");

            builder.Property(x => x.ENDINDATE).HasColumnType("date(7)");
            builder.Property(x => x.ENDSALEDATE).HasColumnType("date(7)");
            builder.Property(x => x.ENDINVENTORYDATE).HasColumnType("date(7)");
            builder.Property(x => x.INQTYDAY).HasColumnType("number(24,6)");
            builder.Property(x => x.LOSSQTYDAY).HasColumnType("number(24,6)");
            builder.Property(x => x.SALEQTYDAY).HasColumnType("number(24,6)");
            builder.Property(x => x.INQTY3DAY).HasColumnType("number(24,6)");
            builder.Property(x => x.SALEQTY3DAY).HasColumnType("number(24,6)");
            builder.Property(x => x.LOSSQTY3DAY).HasColumnType("number(24,6)");
            builder.Property(x => x.INQTY7DAY).HasColumnType("number(24,6)");
            builder.Property(x => x.SALEQTY7DAY).HasColumnType("number(24,6)");
            builder.Property(x => x.LOSSQTY7DAY).HasColumnType("number(24,6)");
            builder.Property(x => x.INQTY28DAY).HasColumnType("number(24,6)");
            builder.Property(x => x.SALEQTY28DAY).HasColumnType("number(24,6)");
            builder.Property(x => x.LOSSQTY28DAY).HasColumnType("number(24,6)");
            builder.Property(x => x.STATUS).HasColumnType("number(5)");
            builder.Property(x => x.DESCRIPTION).HasColumnType("nvarchar2(200)");
            builder.Property(x => x.CREATEBYID).HasColumnType("number(19)");
            builder.Property(x => x.CREATEON).HasColumnType("date(7)");
            builder.Property(x => x.CREATEBYNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.MODIFYBYID).HasColumnType("number(19)");
            builder.Property(x => x.MODIFYBYNAME).HasColumnType("nvarchar2(50)");
            builder.Property(x => x.MODIFYON).HasColumnType("date(7)");
            builder.Property(x => x.REALQTY).HasColumnType("number(24,6)");
            builder.Property(x => x.LOCKQTY).HasColumnType("number(24,6)");
            builder.Property(x => x.PREQTY).HasColumnType("number(24,6)");
        }
    }
}
