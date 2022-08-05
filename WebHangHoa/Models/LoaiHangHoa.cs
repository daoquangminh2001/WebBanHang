using System.ComponentModel.DataAnnotations;

namespace WebHangHoa.Models
{
    public class LoaiHangHoa
    {
        [Key]
        public int MaLoai { get; set; }
        [Required]
        [MaxLength(55)]
        public string TenLoai { get; set; }
        public virtual ICollection<HangHoa> HangHoas { get; set; }
    }
}
