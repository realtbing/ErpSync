using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.MsSql.Mapping
{
    public class OrganizeSKUMapping : IEntityTypeConfiguration<PP_Goods>
    {
        public void Configure(EntityTypeBuilder<PP_Goods> builder)
        {
            builder.ToTable("PP_Goods");

            /**
             * 多对多:不用中间实体的多对多还不支持，要自己拆成用中间实体的两对一对多。
             * EF中（一对多）写法：builder.HasRequired(e => e.Role).WithMany();
             */
            //主键(默认为int)
            builder.HasKey(x => x.goodsId);

            builder.Property(x => x.goodsCode).HasColumnType("varchar(32)");
            builder.Property(x => x.skuGoodsCode).HasColumnType("varchar(32)");
            builder.Property(x => x.goodsName).HasColumnType("varchar(100)");
            builder.Property(x => x.goodsTypeCode).HasColumnType("varchar(32)");
            builder.Property(x => x.subTypeCode).HasColumnType("varchar(32)");
            builder.Property(x => x.groupCode).HasColumnType("varchar(2000)");
            builder.Property(x => x.barCode).HasColumnType("varchar(32)");
            builder.Property(x => x.goodsPicture).HasColumnType("varchar(2000)");
            builder.Property(x => x.thumbnail).HasColumnType("varchar(200)");
            builder.Property(x => x.sharePicture).HasColumnType("varchar(200)");
            builder.Property(x => x.goodsDetailInfo).HasColumnType("text");
            builder.Property(x => x.sort).HasColumnType("int");
            builder.Property(x => x.weCharShare).HasColumnType("text");
            builder.Property(x => x.createTime).HasColumnType("datetime");
            builder.Property(x => x.editTime).HasColumnType("datetime");
        }
    }
}
