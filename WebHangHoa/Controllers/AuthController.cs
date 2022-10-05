using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using WebHangHoa.Data;
using WebHangHoa.DTO;
using WebHangHoa.Models;
namespace WebHangHoa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HangHoaContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(HangHoaContext context,IConfiguration configuration)
        {
            _context = context; 
            _configuration = configuration;
        }

        [HttpPost("Register")]
        #region Register User
        public async Task<ActionResult<Users>> Register(UserRegisterDTO user)
        {
            var value = new Users();
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            value.UserName= user.UserName;
            value.Password = user.Password;
            value.PasswordSalt = passwordSalt;
            value.PasswordHash = passwordHash;
            value.Email = user.Email;
            _context.Add(value);
            await _context.SaveChangesAsync();
            return Ok(value+ "hehe");
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        #endregion
        [HttpPost("Login")]
        #region Login
        public async Task<ActionResult<string>> Login(UserDTO user)
        {
            var check = _context.User.FirstOrDefault(opt=>opt.UserName ==user.UserName );
            if (check == null) return BadRequest("Không tìm thấy User Name");
            if (!VerifiPassWord(check.Password, check.PasswordSalt, check.PasswordHash))    return BadRequest("sai mat khau");
            
            string token = CreateToken(check);
            return Ok(token);
        }
        private bool VerifiPassWord(string pass, byte[] Salt, byte[] Hash)
        {
            using(var temp= new HMACSHA512(Salt))
            {
                var check = temp.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));
                return check.SequenceEqual(Hash);
            }
        }
        private string CreateToken(Users user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role,"Admin")
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims:claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        #endregion
    }
}
