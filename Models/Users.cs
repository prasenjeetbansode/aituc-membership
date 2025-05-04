using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITUC.Models
{
    public class Users
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)] // Optional: set a limit for UI or validation
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [RegularExpression("admin|user", ErrorMessage = "Role must be 'admin' or 'user'.")]
        public required string Role { get; set; }

        [Required]
        public bool CanRead { get; set; } = true;

        [Required]
        public bool CanWrite { get; set; } = false;
    }
}
