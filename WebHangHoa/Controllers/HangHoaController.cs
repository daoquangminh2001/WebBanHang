using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebHangHoa.Data;
using WebHangHoa.DTO;
using WebHangHoa.Models;

namespace WebHangHoa.Controllers
{
    [Route("api/hanghoa")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        private readonly HangHoaContext _context;
        public HangHoaController(HangHoaContext context) {
        _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var listHangHoa = _context.HangHoas.ToList();
            return Ok(listHangHoa);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var temp = _context.HangHoas.SingleOrDefault(otp =>
            otp.MaHangHoa == id);
            if(temp==null)  return NotFound("deo co dau thang ngu");
            return Ok(temp);
        }
        [HttpPost]
        public IActionResult Post(HangHoaDTO _hanghoa)
        {
            try
            {
                var hang = new HangHoa
                {
                    TenHangHoa = _hanghoa.TenHangHoa,
                    Mota = _hanghoa.Mota,
                    DonGia = _hanghoa.DonGia,
                    GiamGia = _hanghoa.GiamGia,
                    MaLoai = _hanghoa.MaLoai
                };
                _context.HangHoas.Add(hang);
                _context.SaveChanges();
                return Ok(hang);
            }
            catch
            {
                return BadRequest("deo them duoc anh oi, loi me r");
            }

        }/*
        public async Task<IActionResult> Post(HangHoa hangHoaDTO)
        {

            _context.HangHoas.Add(hangHoaDTO);
            await _context.SaveChangesAsync();
            return Ok(CreatedAtAction("Hang Hoa Moi", hangHoaDTO));
        }*/
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, HangHoaDTO _hhDTO)
        {
            var check = _context.HangHoas.SingleOrDefault(otp => otp.MaHangHoa == id);
            if(check!=null)
            {
                check.TenHangHoa = _hhDTO.TenHangHoa;
                check.Mota = _hhDTO.Mota;
                check.DonGia = _hhDTO.DonGia;
                check.GiamGia = _hhDTO.GiamGia;
                _context.SaveChanges();
                return NoContent();
            }
        return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaHang(Guid id)
        {
            var check = await _context.HangHoas.FindAsync(id);
            if(check==null) return NotFound();
            _context.HangHoas.Remove(check);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
