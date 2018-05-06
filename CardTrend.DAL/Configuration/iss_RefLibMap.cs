using CardTrend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.Configuration
{
    public class iss_RefLibMap : EntityTypeConfiguration<iss_RefLib>
    {
        public iss_RefLibMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IssNo, t.RefType, t.RefCd });

            // Properties
            this.Property(t => t.IssNo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefType)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.RefCd)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.RefId)
                .HasMaxLength(10);

            this.Property(t => t.Descp)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("iss_RefLib");
            this.Property(t => t.IssNo).HasColumnName("IssNo");
            this.Property(t => t.RefType).HasColumnName("RefType");
            this.Property(t => t.RefCd).HasColumnName("RefCd");
            this.Property(t => t.RefNo).HasColumnName("RefNo");
            this.Property(t => t.RefInd).HasColumnName("RefInd");
            this.Property(t => t.RefId).HasColumnName("RefId");
            this.Property(t => t.MapInd).HasColumnName("MapInd");
            this.Property(t => t.Descp).HasColumnName("Descp");
        }
    }
}
