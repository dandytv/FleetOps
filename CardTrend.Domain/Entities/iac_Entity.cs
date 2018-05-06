using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Entities
{
   public class iac_Entity
    {
        [Key]
        public int EntityId { get; set; }
        public short IssNo { get; set; }
        public string Title { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Gender { get; set; }
        public string Marital { get; set; }
        public Nullable<System.DateTime> Dob { get; set; }
        public string BloodGroup { get; set; }
        public string OldIc { get; set; }
        public string OldIcType { get; set; }
        public string NewIc { get; set; }
        public string NewIcType { get; set; }
        public string PassportNo { get; set; }
        public string LicNo { get; set; }
        public string CmpyName { get; set; }
        public string Dept { get; set; }
        public string Occupation { get; set; }
        public Nullable<int> Income { get; set; }
        public string BankName { get; set; }
        public string BankAcctNo { get; set; }
        public Nullable<int> PriEntityId { get; set; }
        public string Relationship { get; set; }
        public Nullable<int> ApplId { get; set; }
        public Nullable<int> AppcId { get; set; }
        public string Sts { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> LastUpdDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
    }
}
