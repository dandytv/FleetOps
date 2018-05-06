using CardTrend.DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.Contexts
{
    public  class BaseDbContext<TContext> : DbContext where TContext : DbContext
    {

       static BaseDbContext()
       {
           Database.SetInitializer<TContext>(null);
       }

       protected BaseDbContext()
           : base("name=pdb_ccmsContext")
       {
       }

       public BaseDbContext(string connectionString) : base(connectionString) { }

        //protected BaseDbContext() : base() { }
        //protected BaseDbContext(string connectionString) : base(connectionString) { }

        //public void Commit()
        //{
        //    SaveChanges();
        //}
        //public DbContext CurrentContext
        //{
        //    get { return this; }
        //}
        //protected override void Dispose(bool disposing)
        //{
        //    base.Dispose(disposing);
        //    Debug.WriteLine("---DBContext is disposed ---");
        //}
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    // Entity-specific configuration
        //    ConfigureEntityTypes(modelBuilder);
        //    base.OnModelCreating(modelBuilder);
        //}
        //protected abstract void ConfigureEntityTypes(DbModelBuilder modelBuilder);
    }
}
