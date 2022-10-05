using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebHangHoa.Data;
using WebHangHoa.DTO;
using WebHangHoa.Models;
using WebHangHoa.Service;

namespace WebHangHoa.Controllers
{
    [Route("api/hanghoa")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        private readonly HangHoaContext _context;
        private readonly IHangHoaResponsitory _hangHoa;

        public HangHoaController(HangHoaContext context, IHangHoaResponsitory hangHoa)
        {
            _context = context;
            _hangHoa = hangHoa;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try { 
            var listHangHoa = _context.HangHoas.ToList();
            return Ok(listHangHoa);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var temp = _context.HangHoas.SingleOrDefault(otp =>
            otp.MaHangHoa == id);
            if(temp==null)  return NotFound("deo co dau thang ngu");
            return Ok(temp);
        }

        [HttpGet("timkiem")]
        public async Task<IActionResult> GetByKeyWord(string? input)
        {
            try
            {
                var result=_hangHoa.get_by_keyword(input);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
            try
            {
                var check = await _context.HangHoas.FindAsync(id);
                if (check == null) return NotFound();
                _context.HangHoas.Remove(check);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
