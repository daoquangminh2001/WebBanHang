using System.ComponentModel.DataAnnotations;

namespace WebHangHoa.DTO
{
    public class HangHoaDTO
    {
        [Required]
        [MaxLength(50)]
        public string TenHangHoa { get; set; }
        public string Mota { get; set; }
        [Range(0, double.MaxValue)]
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }
        public int? MaLoai { get; set; }
    }
}
