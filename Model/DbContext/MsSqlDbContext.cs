using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Model.Entities.MsSql;
using Model.Extensions;

namespace Model.DbContext
{
    public class MsSqlDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options)
            : base(options)
        {

        }

        public DbSet<PP_Goods> PP_Goodses { get; set; }
        public DbSet<PP_GoodsPriceAlerm> PP_GoodsPriceAlerms { get; set; }
        public DbSet<PP_GoodsSKU> PP_GoodsSKUs { get; set; }
        public DbSet<PP_GoodsStockAlerm> PP_GoodsStockAlerms { get; set; }
        public DbSet<PP_Shop> PP_Shops { get; set; }
        public DbSet<PP_ShopGoods> PP_ShopGoodses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.RemovePluralizingTableNameConvention();
            base.OnModelCreating(modelBuilder);

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(q => q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
            //var mappingInterface = typeof(IEntityTypeConfiguration<>);
            //// Types that do entity mapping
            //var mappingTypes = GetType().GetTypeInfo()
            //                            .Assembly
            //                            .GetTypes()
            //                            .Where(x => x.GetInterfaces()
            //                            .Any(y => y.GetTypeInfo().IsGenericType &&
            //                                      y.GetGenericTypeDefinition() == mappingInterface));

            //// Get the generic Entity method of the ModelBuilder type
            //var entityMethod = typeof(ModelBuilder).GetMethods()
            //                                       .Single(x => x.Name == "Entity" &&
            //                                                    x.IsGenericMethod &&
            //                                                    x.ReturnType.Name == "EntityTypeBuilder`1");

            //foreach (var mappingType in mappingTypes)
            //{
            //    // Get the type of entity to be mapped
            //    var genericTypeArg = mappingType.GetInterfaces().Single().GenericTypeArguments.Single();

            //    // Get the method builder.Entity<TEntity>
            //    var genericEntityMethod = entityMethod.MakeGenericMethod(genericTypeArg);

            //    // Invoke builder.Entity<TEntity> to get a builder for the entity to be mapped
            //    var entityBuilder = genericEntityMethod.Invoke(modelBuilder, null);

            //    // Create the mapping type and do the mapping
            //    var mapper = Activator.CreateInstance(mappingType);
            //    mapper.GetType().GetMethod("Configure").Invoke(mapper, new[] { entityBuilder });
            //}
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}
        }
    }
}
