using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Acquaint.Service.DataObjects;
using Microsoft.Azure.Mobile.Server.Tables;

namespace Acquaint.Service.Models
{
    public class AcquaintDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        private const string connectionStringName = "Name=MS_TableConnectionString";

        public AcquaintDbContext() : base(connectionStringName)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AcquaintDbContext>());
        } 

        public DbSet<Acquaintance> Acquaintances { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        }
    }

}
