using Business.Services;
using Microsoft.AspNetCore.Mvc;
using OnlineExamPaymentAPI.Dtos.Request;

namespace OnlineExamPaymentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly JwtTokenService _jwtTokenServices;

        public LoginController(JwtTokenService jwtTokenServices)
        {
            _jwtTokenServices = jwtTokenServices;   
        }

        [HttpPost]
        public async Task<string> Login(UserLoginDto userDto)
        {
           var result= await _jwtTokenServices.GenerateJwtTokenAsync(userDto.Email);

            return result;
        }
    }
}
