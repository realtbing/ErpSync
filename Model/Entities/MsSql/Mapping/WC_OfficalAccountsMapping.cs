using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.MsSql.Mapping
{
    public class WC_OfficalAccountsMapping : IEntityTypeConfiguration<WC_OfficalAccounts>
    {
        public void Configure(EntityTypeBuilder<WC_OfficalAccounts> builder)
        {
            builder.ToTable("WC_OfficalAccounts");

            /**
             * 多对多:不用中间实体的多对多还不支持，要自己拆成用中间实体的两对一对多。
             * EF中（一对多）写法：builder.HasRequired(e => e.Role).WithMany();
             */
            //主键(默认为int)
            builder.Property(x => x.Id).HasColumnType("varchar(50)");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OfficalId).HasColumnType("varchar(200)");
            builder.Property(x => x.OfficalName).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(x => x.OfficalCode).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.OfficalPhoto).HasColumnType("varchar(1000)");
            builder.Property(x => x.OfficalKey).HasColumnType("varchar(500)");
            builder.Property(x => x.ApiUrl).HasColumnType("varchar(1000)");
            builder.Property(x => x.Token).HasColumnType("varchar(200)");
            builder.Property(x => x.AppId).HasColumnType("varchar(200)");
            builder.Property(x => x.AppSecret).HasColumnType("varchar(200)");
            builder.Property(x => x.AccessToken).HasColumnType("varchar(200)");
            builder.Property(x => x.Remark).HasColumnType("nvarchar(2000)");
            builder.Property(x => x.Enable).HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsDefault).HasColumnType("bit").IsRequired();
            builder.Property(x => x.Category).HasColumnType("int").IsRequired();
            builder.Property(x => x.CreateTime).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.CreateBy).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.ModifyTime).HasColumnType("datetime");
            builder.Property(x => x.ModifyBy).HasColumnType("varchar(50)");

            builder.HasMany(x => x.Users);
        }
    }
}
