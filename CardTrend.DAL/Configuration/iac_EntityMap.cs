using CardTrend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.Configuration
{
    public class iac_EntityMap : EntityTypeConfiguration<iac_Entity>
    {
        public iac_EntityMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityId);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(10);

            this.Property(t => t.FamilyName)
                .HasMaxLength(80);

            this.Property(t => t.GivenName)
                .HasMaxLength(50);

            this.Property(t => t.Gender)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Marital)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BloodGroup)
                .HasMaxLength(10);

            this.Property(t => t.OldIc)
                .HasMaxLength(15);

            this.Property(t => t.OldIcType)
                .HasMaxLength(10);

            this.Property(t => t.NewIc)
                .HasMaxLength(15);

            this.Property(t => t.NewIcType)
                .HasMaxLength(10);

            this.Property(t => t.PassportNo)
                .HasMaxLength(15);

            this.Property(t => t.LicNo)
                .HasMaxLength(10);

            this.Property(t => t.CmpyName)
                .HasMaxLength(50);

            this.Property(t => t.Dept)
                .HasMaxLength(30);

            this.Property(t => t.Occupation)
                .HasMaxLength(100);

            this.Property(t => t.BankName)
                .HasMaxLength(10);

            this.Property(t => t.BankAcctNo)
                .HasMaxLength(15);

            this.Property(t => t.Relationship)
                .HasMaxLength(10);

            this.Property(t => t.Sts)
                .HasMaxLength(10);

            this.Property(t => t.UserId)
                .HasMaxLength(100);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("iac_Entity");
            this.Property(t => t.EntityId).HasColumnName("EntityId");
            this.Property(t => t.IssNo).HasColumnName("IssNo");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.FamilyName).HasColumnName("FamilyName");
            this.Property(t => t.GivenName).HasColumnName("GivenName");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Marital).HasColumnName("Marital");
            this.Property(t => t.Dob).HasColumnName("Dob");
            this.Property(t => t.BloodGroup).HasColumnName("BloodGroup");
            this.Property(t => t.OldIc).HasColumnName("OldIc");
            this.Property(t => t.OldIcType).HasColumnName("OldIcType");
            this.Property(t => t.NewIc).HasColumnName("NewIc");
            this.Property(t => t.NewIcType).HasColumnName("NewIcType");
            this.Property(t => t.PassportNo).HasColumnName("PassportNo");
            this.Property(t => t.LicNo).HasColumnName("LicNo");
            this.Property(t => t.CmpyName).HasColumnName("CmpyName");
            this.Property(t => t.Dept).HasColumnName("Dept");
            this.Property(t => t.Occupation).HasColumnName("Occupation");
            this.Property(t => t.Income).HasColumnName("Income");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.BankAcctNo).HasColumnName("BankAcctNo");
            this.Property(t => t.PriEntityId).HasColumnName("PriEntityId");
            this.Property(t => t.Relationship).HasColumnName("Relationship");
            this.Property(t => t.ApplId).HasColumnName("ApplId");
            this.Property(t => t.AppcId).HasColumnName("AppcId");
            this.Property(t => t.Sts).HasColumnName("Sts");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.LastUpdDate).HasColumnName("LastUpdDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
        }
    }
}