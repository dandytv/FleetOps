using CardTrend.Domain.Entities;
using CardTrend.DAL.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardTrend.Domain.Dto;

namespace CardTrend.DAL.Contexts
{
    public class pdb_ccmsContext : BaseDbContext<pdb_ccmsContext>
    {     
        /*
        public DbSet<iac_Card> iac_Card { get; set; }
        public DbSet<iss_CardType> iss_CardType { get; set; }
        public DbSet<iac_Entity> iac_Entity { get; set; }
        public DbSet<iss_RefLib> iss_RefLib { get; set; }
        */
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public pdb_ccmsContext()
        {
        }
        public pdb_ccmsContext(string connectionString)
            : base(connectionString)
        {
        }
    }
}
