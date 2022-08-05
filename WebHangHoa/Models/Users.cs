using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHangHoa.Models
{
    [Table("User")]
    public class Users
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
    }
}
