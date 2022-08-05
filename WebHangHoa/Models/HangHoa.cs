using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHangHoa.Models
{
    public class HangHoa
    {
        [Key]
        public Guid MaHangHoa { get; set; }
        [Required]
        [MaxLength(50)]
        public string TenHangHoa { get; set; }
        public string Mota { get; set; }
        [Range(0,double.MaxValue)]
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }
        public int? MaLoai { get; set; }
        [ForeignKey("MaLoai")]
        public LoaiHangHoa LoaiHangHoa { get; set; }
        public ICollection<DonHangChiTiet> DonHangChiTiet { get; set; }
        public HangHoa()
        {
            DonHangChiTiet = new List<DonHangChiTiet>();
        }
    }
}
