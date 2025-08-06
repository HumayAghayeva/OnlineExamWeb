using Abstraction.Interfaces;
using Business.Services;
using Domain.Dtos.Read;
using Microsoft.AspNetCore.Mvc;
using OnlineExamPaymentAPI.Dtos.Request;

namespace OnlineExamPaymentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IJwtTokenService _jwtTokenServices;

        public LoginController(IJwtTokenService jwtTokenServices)
        {
            _jwtTokenServices = jwtTokenServices;   
        }

        [HttpPost]
        public async Task<JWTResponseDto> Login(UserLoginDto userDto)
        {
           var result= await _jwtTokenServices.GenerateJwtTokenAsync(userDto.Email);

            return result;
        }
    }
}
