namespace WebHangHoa.Models
{
    public class DonHangChiTiet
    {
        public Guid MaDonHang { get; set; }
        public Guid MaHangHoa { get; set; }
        public int SoLuongMua { get; set; }
        public double DonGia { get; set; }
        public DonHang DonHang { get; set; }
        public HangHoa HangHoa { get; set; }
    }
}
