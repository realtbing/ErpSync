using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Oracle;
using Model.Extensions;

namespace Model.DbContext
{
    public class OracleDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public OracleDbContext(DbContextOptions<OracleDbContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// 触发器触发的对象数据
        /// </summary>
        //public DbSet<ApplyOrder> ApplyOrder { get; set; }
        //public DbSet<ApplyOrderDetail> ApplyOrderDetail { get; set; }
        //public DbSet<DeliveryOrder> DeliveryOrder { get; set; }
        //public DbSet<DeliveryOrderDetail> DeliveryOrderDetail { get; set; }
        public DbSet<OrganizeSKU> OrganizeSKUs { get; set; }
        public DbSet<POSV2_SaleOrder> POSV2_SaleOrders { get; set; }
        public DbSet<POSV2_SaleOrderDetail> POSV2_SaleOrderDetails { get; set; }
        //public DbSet<PP_TriggerData> PP_TriggerData { get; set; }
        public DbSet<PP_TriggerDataBak> PP_TriggerDataBaks { get; set; }
        public DbSet<SKU> SKUs { get; set; }
        public DbSet<SKUTransform> SKUTransforms { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        //public DbSet<StockBak> StockBak { get; set; }
        public DbSet<StockOrder> StockOrders { get; set; }
        public DbSet<StockOrderDetail> StockOrderDetails { get; set; }
        public DbSet<StockSnapshot> StockSnapshots { get; set; }

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("CLZ");
            //modelBuilder.RemovePluralizingTableNameConvention();

            //modelBuilder.Entity<StockSnapshot>().HasKey(x => new { x.BACKUPDATE, x.ORGID, x.ORGCODE, x.STORAGEID, x.STORAGECODE, x.GOODSID, x.GOODSCODE });
            base.OnModelCreating(modelBuilder);

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(q => q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
