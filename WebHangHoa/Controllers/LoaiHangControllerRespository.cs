using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebHangHoa.DTO;
using WebHangHoa.Service;

namespace WebHangHoa.Controllers
{
    [Route("api/hehe")]
    [ApiController]
    public class LoaiHangControllerRespository : ControllerBase
    {
        private readonly ILoaiHangHoaRespository _loaiHangHoaRespository;

        public LoaiHangControllerRespository(ILoaiHangHoaRespository loaiHangHoaRespository)
        {
            _loaiHangHoaRespository = loaiHangHoaRespository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(_loaiHangHoaRespository.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var check = _loaiHangHoaRespository.GetById(id);
                if (check == null)
                    return NotFound();
                return Ok(check);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostNew(LoaiHangHoaDTO hehe)
        {
            try
            {
                return Ok(_loaiHangHoaRespository.Add(hehe));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(LoaiHangDTORespository hehe)
        {
            try
            {
                _loaiHangHoaRespository.Update(hehe);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public  async Task<IActionResult> Delete(int id)
        {
            try
            {
                _loaiHangHoaRespository.DeleteById(id);
                return NoContent();
            }
            catch { return BadRequest(); }
        }
    }
}
