using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.Entities.Oracle.Mapping
{
    public class PP_TriggerDataMapping : IEntityTypeConfiguration<PP_TriggerData>
    {
        public void Configure(EntityTypeBuilder<PP_TriggerData> builder)
        {
            builder.ToTable("PP_TRIGGERDATA");

            builder.Property(x => x.Id).HasColumnName("ID").HasColumnType("char(32)");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type).HasColumnName("TYPE").HasColumnType("number(10)").IsRequired();
            builder.Property(x => x.Category).HasColumnName("CATEGORY").HasColumnType("number(10)").IsRequired();
            builder.Property(x => x.DataId).HasColumnName("DATAID").HasColumnType("varchar2(32)").IsRequired();
            builder.Property(x => x.ChangeValue).HasColumnName("CHANGEVALUE").HasColumnType("number(19,4)").IsRequired();
            builder.Property(x => x.DataStatus).HasColumnName("DATASTATUS").HasColumnType("number(10)").IsRequired();
            builder.Property(x => x.CreateTime).HasColumnName("CREATETIME").HasColumnType("date(7)").IsRequired();
        }
    }
}
