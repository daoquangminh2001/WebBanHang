using WebHangHoa.Data;
using WebHangHoa.DTO;
using WebHangHoa.Models;

namespace WebHangHoa.Service
{
    public class LoaiHangHoaRespository : ILoaiHangHoaRespository
    {
        private readonly HangHoaContext _context;

        public LoaiHangHoaRespository(HangHoaContext context)
        {
            _context = context;
        }
        public LoaiHangDTORespository Add(LoaiHangHoaDTO Loai)
        {
            var temp = new LoaiHangHoa
            {
                TenLoai = Loai.TenLoai,
            };
            _context.Add(temp);
            _context.SaveChanges();
            return new LoaiHangDTORespository
            {
                MaLoai = temp.MaLoai,
                TenLoai = temp.TenLoai
            };
        }

        public void DeleteById(int id)
        {
           var check = _context.loaiHangHoas.SingleOrDefault(otp =>otp.MaLoai==id);
            if (check != null)
            {
                _context.Remove(check);
                _context.SaveChanges();
            }
        }

        public List<LoaiHangDTORespository> GetAll()
        {
            var temp = _context.loaiHangHoas.Select(h => new LoaiHangDTORespository
            {
                MaLoai = h.MaLoai,
                TenLoai = h.TenLoai,
            });
            return temp.ToList();
        }

        public LoaiHangDTORespository GetById(int id)
        {
            var check = _context.loaiHangHoas.SingleOrDefault(otp => otp.MaLoai == id);
            if (check != null)
            {
                return new LoaiHangDTORespository
                {
                    MaLoai = check.MaLoai,
                    TenLoai = check.TenLoai,
                };
            }
            return null;
        }

        public void Update(LoaiHangDTORespository Loai)
        {
            var check = _context.loaiHangHoas.SingleOrDefault(otp=>otp.MaLoai== Loai.MaLoai);
            check.TenLoai=Loai.TenLoai;
            _context.SaveChanges();
        }
    }
}
