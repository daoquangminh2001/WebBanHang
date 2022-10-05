using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebHangHoa.Service;

namespace WebHangHoa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangController : ControllerBase
    {
        private readonly IHangHoaResponsitory _hangHoaResponsitory;

        public HangController(IHangHoaResponsitory hangHoaResponsitory)
        {
            _hangHoaResponsitory = hangHoaResponsitory;
        }

        [HttpGet(Name ="Hehe")/*,Authorize(Roles = "Admin")*/]
        public IActionResult Getallproduct(string keyword,string? sortBy,double? from , double? to, int page =1)
        {
            try
            {
                var result = _hangHoaResponsitory.getall(keyword, sortBy, from, to, page);
                if (ModelState.IsValid)
                {
                    
                    return Ok(result);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
