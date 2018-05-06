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
    public class iac_CardMap : EntityTypeConfiguration<iac_Card>
    {
        public iac_CardMap()
        {
            // Primary Key
            this.HasKey(t => t.CardNo);

            // Properties
            this.Property(t => t.CardNo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Sts)
                .HasMaxLength(10);

            this.Property(t => t.CardLogo)
                .HasMaxLength(8);

            this.Property(t => t.PlasticType)
                .HasMaxLength(8);

            this.Property(t => t.CardType)
                .HasMaxLength(10);

            this.Property(t => t.CostCentre)
                .HasMaxLength(10);

            this.Property(t => t.PriSec)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.VehRegsNo)
                .HasMaxLength(20);

            this.Property(t => t.SmartSerialNo)
                .HasMaxLength(20);

            this.Property(t => t.EmbName)
                .HasMaxLength(26);

            this.Property(t => t.RenewalInd)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.VIPInd)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Cvc)
                .HasMaxLength(3);

            this.Property(t => t.Cvc2)
                .HasMaxLength(3);

            this.Property(t => t.OdometerInd)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PinInd)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PinBlock)
                .IsFixedLength()
                .HasMaxLength(16);

            this.Property(t => t.PVV)
                .HasMaxLength(16);

            this.Property(t => t.PinOffSet)
                .HasMaxLength(16);

            this.Property(t => t.TempPVV)
                .HasMaxLength(16);

            this.Property(t => t.DialogueInd)
                .HasMaxLength(10);

            this.Property(t => t.SKDSInd)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SKDSNo)
                .HasMaxLength(20);

            this.Property(t => t.ProdGroup)
                .HasMaxLength(50);

            this.Property(t => t.PartnerRefNo)
                .HasMaxLength(50);

            this.Property(t => t.JoiningFeeCd)
                .HasMaxLength(10);

            this.Property(t => t.AnnlFeeCd)
                .HasMaxLength(10);

            this.Property(t => t.DriverCd)
                .HasMaxLength(15);

            this.Property(t => t.StaffNo)
                .HasMaxLength(10);

            this.Property(t => t.GovernmentLevyFeeCd)
                .HasMaxLength(10);

            this.Property(t => t.BranchCd)
                .HasMaxLength(10);

            this.Property(t => t.DivisionCd)
                .HasMaxLength(10);

            this.Property(t => t.DeptCd)
                .HasMaxLength(10);

            this.Property(t => t.ReasonCd)
                .HasMaxLength(10);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(100);

            this.Property(t => t.UserId)
                .HasMaxLength(100);

            this.Property(t => t.SKDSProdGroup)
                .HasMaxLength(10);

            this.Property(t => t.SKDSInstantInd)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("iac_Card");
            this.Property(t => t.CardNo).HasColumnName("CardNo");
            this.Property(t => t.CardId).HasColumnName("CardId");
            this.Property(t => t.IssNo).HasColumnName("IssNo");
            this.Property(t => t.Sts).HasColumnName("Sts");
            this.Property(t => t.BlockDate).HasColumnName("BlockDate");
            this.Property(t => t.AcctNo).HasColumnName("AcctNo");
            this.Property(t => t.CardLogo).HasColumnName("CardLogo");
            this.Property(t => t.PlasticType).HasColumnName("PlasticType");
            this.Property(t => t.CardType).HasColumnName("CardType");
            this.Property(t => t.CostCentre).HasColumnName("CostCentre");
            this.Property(t => t.PriCardNo).HasColumnName("PriCardNo");
            this.Property(t => t.XrefCardNo).HasColumnName("XrefCardNo");
            this.Property(t => t.EntityId).HasColumnName("EntityId");
            this.Property(t => t.PriSec).HasColumnName("PriSec");
            this.Property(t => t.VehRegsNo).HasColumnName("VehRegsNo");
            this.Property(t => t.SmartSerialNo).HasColumnName("SmartSerialNo");
            this.Property(t => t.EmbName).HasColumnName("EmbName");
            this.Property(t => t.MemSince).HasColumnName("MemSince");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");
            this.Property(t => t.OldExpiryDate).HasColumnName("OldExpiryDate");
            this.Property(t => t.TerminationDate).HasColumnName("TerminationDate");
            this.Property(t => t.CardMedia).HasColumnName("CardMedia");
            this.Property(t => t.RenewalInd).HasColumnName("RenewalInd");
            this.Property(t => t.VIPInd).HasColumnName("VIPInd");
            this.Property(t => t.Cvc).HasColumnName("Cvc");
            this.Property(t => t.Cvc2).HasColumnName("Cvc2");
            this.Property(t => t.OdometerInd).HasColumnName("OdometerInd");
            this.Property(t => t.PinInd).HasColumnName("PinInd");
            this.Property(t => t.PinBlock).HasColumnName("PinBlock");
            this.Property(t => t.PVV).HasColumnName("PVV");
            this.Property(t => t.PinOffSet).HasColumnName("PinOffSet");
            this.Property(t => t.TempPVV).HasColumnName("TempPVV");
            this.Property(t => t.BDKIdx).HasColumnName("BDKIdx");
            this.Property(t => t.CycNo).HasColumnName("CycNo");
            this.Property(t => t.DialogueInd).HasColumnName("DialogueInd");
            this.Property(t => t.SKDSInd).HasColumnName("SKDSInd");
            this.Property(t => t.SKDSNo).HasColumnName("SKDSNo");
            this.Property(t => t.SKDSQuota).HasColumnName("SKDSQuota");
            this.Property(t => t.SKDSEffectiveDate).HasColumnName("SKDSEffectiveDate");
            this.Property(t => t.SKDSEndDate).HasColumnName("SKDSEndDate");
            this.Property(t => t.PriorityNo).HasColumnName("PriorityNo");
            this.Property(t => t.ActivationDate).HasColumnName("ActivationDate");
            this.Property(t => t.FirstTxnDate).HasColumnName("FirstTxnDate");
            this.Property(t => t.ProdGroup).HasColumnName("ProdGroup");
            this.Property(t => t.PartnerRefNo).HasColumnName("PartnerRefNo");
            this.Property(t => t.JoiningFeeCd).HasColumnName("JoiningFeeCd");
            this.Property(t => t.AnnlFeeCd).HasColumnName("AnnlFeeCd");
            this.Property(t => t.DriverCd).HasColumnName("DriverCd");
            this.Property(t => t.GroupId).HasColumnName("GroupId");
            this.Property(t => t.StaffNo).HasColumnName("StaffNo");
            this.Property(t => t.GovernmentLevyFeeCd).HasColumnName("GovernmentLevyFeeCd");
            this.Property(t => t.BranchCd).HasColumnName("BranchCd");
            this.Property(t => t.DivisionCd).HasColumnName("DivisionCd");
            this.Property(t => t.DeptCd).HasColumnName("DeptCd");
            this.Property(t => t.ApplId).HasColumnName("ApplId");
            this.Property(t => t.AppcId).HasColumnName("AppcId");
            this.Property(t => t.MigrateId).HasColumnName("MigrateId");
            this.Property(t => t.ReasonCd).HasColumnName("ReasonCd");
            this.Property(t => t.PrcsId).HasColumnName("PrcsId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.LastUpdDate).HasColumnName("LastUpdDate");
            this.Property(t => t.SKDSProdGroup).HasColumnName("SKDSProdGroup");
            this.Property(t => t.SKDSInstantInd).HasColumnName("SKDSInstantInd");
        }
    }
}
