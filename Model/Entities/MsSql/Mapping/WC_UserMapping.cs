using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.MsSql.Mapping
{
    public class WC_UserMapping : IEntityTypeConfiguration<WC_User>
    {
        public void Configure(EntityTypeBuilder<WC_User> builder)
        {
            builder.ToTable("WC_User");

            /**
             * 多对多:不用中间实体的多对多还不支持，要自己拆成用中间实体的两对一对多。
             * EF中（一对多）写法：builder.HasRequired(e => e.Role).WithMany();
             */
            //主键(默认为int)
            builder.Property(x => x.Id).HasColumnType("varchar(50)");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OpenId).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.NickName).HasColumnType("varchar(200)");
            builder.Property(x => x.Sex).HasColumnType("int").IsRequired();
            builder.Property(x => x.Language).HasColumnType("nvarchar(50)");
            builder.Property(x => x.City).HasColumnType("nvarchar(50)");
            builder.Property(x => x.Province).HasColumnType("nvarchar(50)");
            builder.Property(x => x.Country).HasColumnType("nvarchar(50)");
            builder.Property(x => x.HeadImgUrl).HasColumnType("varchar(1000)");
            builder.Property(x => x.SubscribeTime).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.UnionId).HasColumnType("varchar(200)");
            builder.Property(x => x.Remark).HasColumnType("nvarchar(1000)");
            builder.Property(x => x.GroupId).HasColumnType("varchar(50)");
            builder.Property(x => x.TagidList).HasColumnType("nvarchar(1000)");
            builder.Property(x => x.Subscribe).HasColumnType("int").IsRequired();
            builder.Property(x => x.OfficalAccountId).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.SessionKey).HasColumnType("varchar(50)");

            builder.HasOne(x => x.OfficalAccount).WithMany().HasForeignKey(x => x.OfficalAccountId).IsRequired();
        }
    }
}
