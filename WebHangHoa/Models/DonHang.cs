using System.ComponentModel.DataAnnotations;

namespace WebHangHoa.Models
{
    public enum TinhTrangDonHang
    {
        New =0,Payment=1,Complete =2, Cancel = -1
    }
    public class DonHang
    {
        [Key]
        public Guid MaDonHang { get; set; }
        [Required]
        [MaxLength(55)]
        public string NguoiNhan { get; set; }
        [Required]
        [MaxLength(55)]
        public string DiaChiGiao { get; set; }
        [Required]
        [MaxLength(25)]
        public string SoDienThoai { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayGiao { get; set; }
        [Range(0,double.MaxValue)]
        public double TongTien { get; set; }
        public TinhTrangDonHang TinhTrang { get; set; }
        public ICollection<DonHangChiTiet> DonHangChiTiet { get; set; }
        public DonHang()
        {
            DonHangChiTiet = new List<DonHangChiTiet>();
        }
    }
}
