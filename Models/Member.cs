using System;
using System.ComponentModel.DataAnnotations;

namespace AITUC.Models
{
    public class Member
    {
        [Key]
        public int MemberID { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Area { get; set; }
        public string? AreaCode { get; set; }
        public string? Mobile { get; set; }
        public string? Education { get; set; }
        public int Son { get; set; } = 0;
        public int Daughter { get; set; } = 0;
        public int? WorkYear { get; set; }
        public string? WorkDescription { get; set; }
        public bool BPLCard { get; set; } = false;
        public bool RationCard { get; set; } = false;
        public string? WorkPlaces { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
    }
}
