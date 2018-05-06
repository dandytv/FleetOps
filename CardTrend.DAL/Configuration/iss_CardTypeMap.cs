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
    public class iss_CardTypeMap : EntityTypeConfiguration<iss_CardType>
    {
        public iss_CardTypeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.PlasticType, t.CardType });

            // Properties
            this.Property(t => t.CardLogo)
                .HasMaxLength(8);

            this.Property(t => t.PlasticType)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.CardType)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descp)
                .HasMaxLength(50);

            this.Property(t => t.ShortDescp)
                .HasMaxLength(10);

            this.Property(t => t.CardRangeId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CardCategory)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.VehInd)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("iss_CardType");
            this.Property(t => t.IssNo).HasColumnName("IssNo");
            this.Property(t => t.CardLogo).HasColumnName("CardLogo");
            this.Property(t => t.PlasticType).HasColumnName("PlasticType");
            this.Property(t => t.CardType).HasColumnName("CardType");
            this.Property(t => t.Descp).HasColumnName("Descp");
            this.Property(t => t.ShortDescp).HasColumnName("ShortDescp");
            this.Property(t => t.CardRangeId).HasColumnName("CardRangeId");
            this.Property(t => t.CardCategory).HasColumnName("CardCategory");
            this.Property(t => t.VehInd).HasColumnName("VehInd");
            this.Property(t => t.Attribute).HasColumnName("Attribute");
            this.Property(t => t.AuthCardType).HasColumnName("AuthCardType");
            this.Property(t => t.LastUpdDate).HasColumnName("LastUpdDate");
        }
    }
}
