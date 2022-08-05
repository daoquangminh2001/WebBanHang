using System.Security.Claims;
using System.Security.Cryptography;

namespace WebHangHoa.Service
{
    public class RegisterUser : IRegisterUser
    {
        private readonly IHttpContextAccessor _httpContext;
        public RegisterUser(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string getUserName()
        {
            var result = string.Empty;
            if(_httpContext.HttpContext != null)
            {
                result = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }

    }
}
