using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.MsSql.Mapping
{
    public class WC_CrowdUserMapping : IEntityTypeConfiguration<WC_CrowdUser>
    {
        public void Configure(EntityTypeBuilder<WC_CrowdUser> builder)
        {
            builder.ToTable("WC_CrowdUser");

            /**
             * 多对多:不用中间实体的多对多还不支持，要自己拆成用中间实体的两对一对多。
             * EF中（一对多）写法：builder.HasRequired(e => e.Role).WithMany();
             */
            //主键(默认为int)
            builder.HasKey(x => x.cuid);

            builder.Property(x => x.openGid).HasColumnType("varchar(120)").IsRequired();
            builder.Property(x => x.code).HasColumnType("varchar(32)").IsRequired();
            builder.Property(x => x.openId).HasColumnType("varchar(120)").IsRequired();
            builder.Property(x => x.userName).HasColumnType("varchar(50)");
            builder.Property(x => x.headPicture).HasColumnType("varchar(200)");
            builder.Property(x => x.status).HasColumnType("int").IsRequired();
            builder.Property(x => x.createTime).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.exitTime).HasColumnType("datetime");
        }
    }
}
