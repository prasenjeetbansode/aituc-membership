using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITUC.Models
{
    public class Membership
    {
        [Key]
        public int MembershipID { get; set; }
        [Required]
        public int MemberID { get; set; }
        [ForeignKey(nameof(MemberID))]
        public Member Member { get; set; } // Navigation property
        [Required]
        public DateTime MembershipStartDate { get; set; }
        [Required]
        public DateTime MembershipEndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? RenewedOn { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Fees { get; set; }
        public string? PaidWith { get; set; }
    }
}
