using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.MsSql.Mapping
{
    public class WC_CrowdMapping : IEntityTypeConfiguration<WC_Crowd>
    {
        public void Configure(EntityTypeBuilder<WC_Crowd> builder)
        {
            builder.ToTable("WC_Crowd");

            /**
             * 多对多:不用中间实体的多对多还不支持，要自己拆成用中间实体的两对一对多。
             * EF中（一对多）写法：builder.HasRequired(e => e.Role).WithMany();
             */
            //主键(默认为int)
            builder.HasKey(x => x.cid);

            builder.Property(x => x.openGId).HasColumnType("varchar(120)").IsRequired();
            builder.Property(x => x.name).HasColumnType("varchar(32)").IsRequired();
            builder.Property(x => x.shopCode).HasColumnType("varchar(32)").IsRequired();
            builder.Property(x => x.status).HasColumnType("int").IsRequired();
            builder.Property(x => x.lotteryTime).HasColumnType("datetime");
            builder.Property(x => x.joinPeople).HasColumnType("int").IsRequired();
            builder.Property(x => x.winners).HasColumnType("int").IsRequired();
            builder.Property(x => x.createTime).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.operatorStaff).HasColumnType("varchar(50)");
            builder.Property(x => x.lastTime).HasColumnType("datetime").IsRequired();
        }
    }
}
