using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebHangHoa.Data;
using WebHangHoa.DTO;
using WebHangHoa.Models;

namespace WebHangHoa.Controllers
{
    [Route("api/loai")]
    [ApiController]
    public class LoaiHangHoaController : ControllerBase
    {
        private readonly HangHoaContext _context;
        public LoaiHangHoaController(HangHoaContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ListLoaiHang = _context.loaiHangHoas.ToList();
            return Ok(ListLoaiHang);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var LoaiHang = _context.loaiHangHoas.FirstOrDefault(otp => otp.MaLoai == id);
            if (LoaiHang == null)   return NotFound();
            return Ok(LoaiHang);
        }
        [HttpPost]
        public async Task<IActionResult> PostHangHoa(LoaiHangHoaDTO Ldto)
        {
            try
            {
                var hang = new LoaiHangHoa
                {
                    TenLoai = Ldto.TenLoai
                };
                _context.loaiHangHoas.Add(hang);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHH(int id , LoaiHangHoaDTO Ldto)
        {
            var Check = _context.loaiHangHoas.FirstOrDefault(otp=>otp.MaLoai==id);
            if(Check == null) return NotFound();
            Check.TenLoai = Ldto.TenLoai;
            
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHH(int id)
        {
            var Check = _context.loaiHangHoas.FirstOrDefault(otp => otp.MaLoai == id);
            if (Check == null) return NotFound();

            _context.loaiHangHoas.Remove(Check);
            await _context.SaveChangesAsync();
            return Ok("Delete Successfull");
        }
    }
}
